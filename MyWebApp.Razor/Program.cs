using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyWebApp.Razor.Data;
using MyWebApp.Razor.Interfaces;
using MyWebApp.Razor.Models;
using MyWebApp.Razor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//builder.Services.AddDbContext<MyContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("MyContext"));
//});
builder.Services.AddDbContext<NorthwindContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("Northwind")));

#region Singleton Dipendency Injection
// creates a single instance of the service and uses it throughout the lifetime of the application.
/*var serviceProvider = new ServiceCollection()
.AddSingleton<IMyService, MyService>()
            .BuildServiceProvider();

Console.WriteLine("   Dependency Injection Singleton");
var serviceObject1 = serviceProvider.GetService<IMyService>();
Console.WriteLine($"   Request 1 {serviceObject1.GetObjectCreationDateTime} ");
for (int i = 0; i < 5000; i++) ;

var serviceObject2 = serviceProvider.GetService<IMyService>();
Console.WriteLine($"   Request 2 {serviceObject2.GetObjectCreationDateTime} ");


for (int i = 0; i < 5000; i++) ;
var serviceObject3 = serviceProvider.GetService<IMyService>();
Console.WriteLine($"   Request 3 {serviceObject3.GetObjectCreationDateTime} "); */
#endregion

#region Transient Dipendency Injection
// creates a new instance of the service each time it is requested. one request one instance.
/*
var serviceProvider = new ServiceCollection()
            .AddTransient<IMyService, MyService>()
            .BuildServiceProvider();

Console.WriteLine(" Dependency Injection Transient");
var serviceObject1 = serviceProvider.GetService<IMyService>();
Console.WriteLine($" Request 1 {serviceObject1.GetObjectCreationDateTime} ");

for (int i = 0; i < 5000; i++) ;

var serviceObject2 = serviceProvider.GetService<IMyService>();
Console.WriteLine($" Request 2 {serviceObject2.GetObjectCreationDateTime} ");


for (int i = 0; i < 5000; i++) ;
var serviceObject3 = serviceProvider.GetService<IMyService>();
Console.WriteLine($" Request 3 {serviceObject3.GetObjectCreationDateTime} ");*/
#endregion

#region Scoped Dipendency Injection
// creates a new instance of the service for each HTTP request. one client one Instance
/*var serviceProvider = new ServiceCollection()
            .AddScoped<IMyService, MyService>()
            .BuildServiceProvider();


Console.WriteLine("   Dependency Injection Scoped");
Console.WriteLine("   Scope 1");

using (var scope = serviceProvider.CreateScope())
{
    Console.WriteLine($"   Request 1 {scope.ServiceProvider.GetService<IMyService>().GetObjectCreationDateTime}");
    for (int i = 0; i < 5000; i++);
    Console.WriteLine($"   Request 2 {scope.ServiceProvider.GetService<IMyService>().GetObjectCreationDateTime}");
}

Console.WriteLine("");
Console.WriteLine("   Scope 2");
using (var scope = serviceProvider.CreateScope())
{
    Console.WriteLine($"   Request 1 { scope.ServiceProvider.GetService<IMyService>().GetObjectCreationDateTime }" );
    for (int i = 0; i < 5000; i++) ;
    Console.WriteLine($"   Request 2 { scope.ServiceProvider.GetService<IMyService>().GetObjectCreationDateTime } ");
}*/
#endregion


builder.Services.AddScoped<ICustomer, CustomerService>();
builder.Services.AddScoped<IProduct, ProductService>();

builder.Services.AddSingleton<IDevice, Device>();
builder.Services.AddSingleton<ICarService, CarService>();

//use the first implementation
//builder.Services.AddScoped<ISayMyName,SayMyNameOne>();
//use the second implementation
builder.Services.AddScoped<ISayMyName, SayMyNameTwo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

//
app.MapGet("/api/cars", (ICarService service) =>
{
    return Results.Ok(service.GetAll());
});
app.MapGet("/api/car/{id:int}", (int id, ICarService service) =>
{
    var car = service.Get(id);
    return car;
});
app.MapMethods("/api/save", new[] { "POST", "PUT" }, (Car car, ICarService service) =>
{
    service.Save(car);
    return Results.Ok();
});

app.Run();
