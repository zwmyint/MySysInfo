var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;

    services.AddCors();
    
    services.AddHealthChecks(); // https://medium.com/@sneigee/health-checks-in-net-c645a1eb2f18

    services.AddControllers();
}

var app = builder.Build();

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    app.MapHealthChecks("/health");// https://medium.com/@sneigee/health-checks-in-net-c645a1eb2f18

    app.MapControllers();
}

// http://localhost:5530/products/1
// http://192.168.0.28:5533/products
// dotnet MyWebApi.Minimal.dll --urls "http://*:5533"
app.Run();
