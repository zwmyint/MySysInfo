using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWeb.ConsoleApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            //app.UseEndpoints(endpoints => {
            //    endpoints.MapGet("/", async context => {
            //        await context.Response.WriteAsync("Hello from new web Api");
            //    });
            //});

            app.UseEndpoints(endpoints => {
                endpoints.MapGet("/", async context => {
                    await context.Response.WriteAsync($"Empower every person and every organization on the planet to achieve more{Environment.NewLine}" +
                                                      $".NET Core {Environment.Version}{Environment.NewLine}" +
                                                      $"Environment.OSVersion: {Environment.OSVersion}{Environment.NewLine}" +
                                                      $"Environment.Is64BitOperatingSystem: {Environment.Is64BitOperatingSystem}{Environment.NewLine}" +
                                                      $"Environment.Is64BitProcess: {Environment.Is64BitProcess}", Encoding.UTF8);
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }


    }
}
