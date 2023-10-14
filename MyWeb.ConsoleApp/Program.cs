
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MyWeb.ConsoleApp;
using System;

namespace MyWeb.ConsoleApp
{
    public class Program
    {
        // dotnet MyWeb.ConsoleApp.dll --urls "http://*:8080"

        // 1
        //static void Main()
        //{
        //    CreateHostBuilder().Build().Run();
        //}

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // 1
        //public static IHostBuilder CreateHostBuilder()
        //{
        //    return Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webHost =>
        //    {
        //        webHost.UseStartup<Startup>();
        //    });
        //}

        // 2
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}