
using Hangfire;
using Hangfire.MemoryStorage;
using Hangfire.Storage.SQLite;
using Microsoft.Extensions.Configuration;
using MyWebApi.Hangfire.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHangfire(configuration => configuration
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSQLiteStorage("Hangfire.db")); // You should pass C:\database\hangfireDb.db

//builder.Services.AddHangfire(config =>
//                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
//                .UseSimpleAssemblyNameTypeSerializer()
//                .UseDefaultTypeSerializer()
//                .UseMemoryStorage());


// Add the processing server as IHostedService
builder.Services.AddHangfireServer();

builder.Services.AddScoped<IServiceManagement, ServiceManagement>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// <<<
// <<<

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHangfireDashboard(); // <<<
app.MapHangfireDashboard(); // <<<
app.UseAuthorization();

app.MapControllers();

app.Run();
