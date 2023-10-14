using HashidsNet;
using LiteDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using MyWebApi.Minimal3;
using MyWebApi.Minimal3.Models;
using Serilog;

// Create the WebApplicationBuilder
var builder = WebApplication.CreateBuilder(args);
var envSetting = builder.Configuration.GetValue<string>("EnvSetting");

// Serilog
builder.Logging.ClearProviders();
////var logger = new LoggerConfiguration()
////    .MinimumLevel.Information()
////    .WriteTo.Console()
////    .WriteTo.File("logs.txt", rollingInterval: RollingInterval.Day)
////    .CreateLogger();
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

// Register Serilog
builder.Logging.AddSerilog(logger);


// Set correct port
builder.WebHost.UseUrls(new[] { "http://0.0.0.0:5042", "https://0.0.0.0:7248" });

// Add services to the container
builder.Services.AddSingleton<ILiteDatabase, LiteDatabase>(_ => new LiteDatabase("url.db"));
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
{
    Description = "MyWebApi.Minimal3 Description",
    Title = "Url Shorter",
    Version = "v1",
    Contact = new OpenApiContact()
    {
        Name = "XYZ io01 App",
        Url = new Uri("https://io01.app")
    }
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// -------------------------------------
//app.MapGet("/", () => "hello world from MyWebApi.Minimal3 ... [" + app.Environment.EnvironmentName + "] : " + envSetting);
app.MapGet("/", (ILoggerFactory loggerFactory) => {

    var logger = loggerFactory.CreateLogger("index");
    logger.LogInformation("Starting...(index called)");
    var rtnstring = "hello world from MyWebApi.Minimal3 ... [" + app.Environment.EnvironmentName + "] : " + envSetting;
    logger.LogInformation("End...(index called)");
    return rtnstring;
});
// -------------------------------------

// -------------------------------------
app.MapGet("/hello", ([FromQuery] string name) =>
{
    // https://192.168.0.29:7248/hello?name=ZAW
    return $"Hello {name}";
});
// -------------------------------------

// ------------------------------------------
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (ILoggerFactory loggerFactory) =>
{
    var logger = loggerFactory.CreateLogger("weatherforecast");
    logger.LogInformation("Starting...(weatherforecast called)");

    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)],
            app.Environment.EnvironmentName.ToString()
        ))
        .ToArray();

    logger.LogInformation("End...(weatherforecast called)");

    return forecast;
})
.WithName("GetWeatherForecast");
// ------------------------------------------

// ------------------------------------------
// Configure HashIds.NET
Hashids _hashIds = new("URLShortener", 5);

// Configure routes
app.MapPost("/add", (UrlInfoDto urlInfoDto, ILiteDatabase database, HttpContext httpContext) =>
{
    // check if an URL is provided
    if (urlInfoDto is null || string.IsNullOrEmpty(urlInfoDto.Url))
    {
        return Results.BadRequest("Please provide a valid UrlInfo object.");
    }

    // get the collection from the database
    ILiteCollection<UrlInfo> collection = database.GetCollection<UrlInfo>(BsonAutoId.Int32);

    // check if an entry with the corresponding url is already part of the database
    UrlInfo entry = collection.Query().Where(x => x.Url.Equals(urlInfoDto.Url)).FirstOrDefault();

    // if there is already an entry in the database, just return the hashed valued
    if (entry is not null)
    {
        return Results.Ok(_hashIds.Encode(entry.Id));
    }

    // otherwise just insert the url info into the database and return the hashed valued
    BsonValue documentId = collection.Insert(new UrlInfo(urlInfoDto.Url, 0));

    string encodedId = _hashIds.Encode(documentId);
    string url = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/{encodedId}";

    return Results.Created(url, encodedId);
})
    .Produces<string>(StatusCodes.Status200OK)
    .Produces<string>(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest);

// 
app.MapGet("/{shortUrl}", (string shortUrl, ILiteDatabase context) =>
{
    // decode the short url into the corresponding id
    int[] ids = _hashIds.Decode(shortUrl);
    int tempraryId = ids[0];

    // get the collection from the database
    ILiteCollection<UrlInfo> collection = context.GetCollection<UrlInfo>();

    // try to get the entry with the corresponding id from the database
    UrlInfo entry = collection.Query().Where(x => x.Id.Equals(tempraryId)).FirstOrDefault();

    // if the url info is present in the database, just return the url
    if (entry is not null)
    {
        return Results.Ok(entry.Url);
    }

    // otherwise return the status code 'not found'
    return Results.NotFound();
})
    .Produces<string>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);
// ------------------------------------------

// ------------------------------------------
app.MapGet("author/{id:int}", (IAuthorRepository authorRepository, HttpContext httpContext) =>
{
    string? requestId = httpContext.Request.RouteValues["id"]?.ToString();

    var author = authorRepository.GetAuthor(Convert.ToInt32(requestId));
    if (author == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(author);
})
    .Produces<string>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);
// ------------------------------------------

// How to use logging in Program.cs file		
app.Logger.LogInformation("The application started ...");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary, string? AppEnvName)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


/*
 The Web API provides two endpoints:

POST https://<YOUR-IP-ADDRESS>:5098/add - Creates a shortened version of an URL
Body: {"url":"YOUR URL"}
https://localhost:7248/add
Body:
{
  "url": "https://www.tsjdev-apps.de/xyz.aspx"
}
GET https://<YOUR-IP-ADDRESS>:5098/{ShortUrl} - Gets the long version of an URL
https://localhost:7248/KqQ8X
*/