using ConsoleTables;
using Iot.Device.CpuTemperature;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog;
using System.Text;
using UnitsNet;

namespace MySysInfo.ConsoleApp
{
    class Program
    {
        const string DeviceId = "MyDeviceID1010";
        static CpuTemperature temperature = new CpuTemperature();
        static int _msgId = 0;
        const double TemperatureThreshold = 42.0;

        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("Logs/Log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            // read from appsettings.json
            // Method 1: Get directly
            Console.WriteLine(configuration["AppSettings:Title"]);

            // Method 2: Get via GetSection method.
            var appSettings = configuration.GetSection("AppSettings");
            Console.WriteLine(appSettings["Version"]);

            // Method 3: Get via GetConnection method for connection string
            var mysqlConnString = configuration.GetConnectionString("MySql");
            Console.WriteLine($"MySQL Connection String: {mysqlConnString}");

            Log.Information("Application Started after reading config Data.");

            try
            {
                // See https://aka.ms/new-console-template for more information
                Console.WriteLine("Program Started ...");
                Console.WriteLine($"The System Date Time is : {DateTime.Now}");
                Console.WriteLine($"The Operation System is : {Environment.OSVersion}");
                Console.WriteLine($"The Processor Count is : {Environment.ProcessorCount}");
                Console.WriteLine($".NET Core {Environment.Version}");
                Console.WriteLine($"Environment.Is64BitOperatingSystem: {Environment.Is64BitOperatingSystem}");
                Console.WriteLine($"Environment.Is64BitProcess: {Environment.Is64BitProcess}");

                Console.WriteLine($"...{Environment.NewLine}");


                if (temperature.IsAvailable)
                {
                    Console.WriteLine($"The CPU temperature now is {temperature.Temperature.DegreesCelsius}");
                    //Console.WriteLine($"The CPU temperature is {Math.Round(temperature.Temperature.DegreesCelsius, 2)}");
                    await SendMsgIotHub(temperature.Temperature.DegreesCelsius,500);
                }
                Log.Information("CPU Temperature Reading Completed.");
                
                // console table
                // dotnet add package ConsoleTables
                Console.WriteLine(" On Premise vs IaaS vs PaaS vs SaaS");

                var table = new ConsoleTable("On Premise", "IaaS", "PaaS", "SaaS");
                table.AddRow("Applications*", "Applications*", "Applications*", "Applications");
                table.AddRow("Data*", "Data*", "Data*", "Data");
                table.AddRow("Runtime*", "Runtime*", "Runtime", "Runtime");
                table.AddRow("Middleware*", "Middleware*", "Middleware", "Middleware");
                table.AddRow("OS*", "OS*", "OS", "OS");
                table.AddRow("Virtualization*", "Virtualization", "Virtualization", "Virtualization");
                table.AddRow("Server*", "Server", "Server", "Server");
                table.AddRow("Storage*", "Storage", "Storage", "Storage");
                table.AddRow("Networking*", "Networking", "Networking", "Networking");
                table.Write(Format.Default);

                Console.WriteLine("* You Manage");
                Console.WriteLine("Without * Service Provider Manage");
                // console table
                Log.Information("Console Table Created Successful.");


                var person = new Person { Name = "", Age = 17 };

                var validator = new PersonValidator();
                var result = validator.Validate(person);


                if (result.IsValid)
                {
                    Console.WriteLine("The Validation was successfull!");
                }
                else
                {
                    Console.WriteLine("Validation error:");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"{error.PropertyName}: {error.ErrorMessage}");
                    }
                }


                Console.Read();


            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly !");
            }
            
        }
        //




        private static async Task SendMsgIotHub(double temperature, int milliseconds)
        {
            var telemetry = new Telemetry() { Temperature = Math.Round(temperature, 2), MessageId = _msgId++ };
            string json = JsonConvert.SerializeObject(telemetry);

            Console.WriteLine($"Sending {json}");
            await Task.Delay(milliseconds);

        }

        class Telemetry
        {
            [JsonPropertyAttribute(PropertyName = "temperature")]
            public double Temperature { get; set; } = 0;

            [JsonPropertyAttribute(PropertyName = "messageId")]
            public int MessageId { get; set; } = 0;

            [JsonPropertyAttribute(PropertyName = "deviceId")]
            public string DeviceId { get; set; } = Program.DeviceId;
        }
        
        //
    }

    public class Person
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
    }


}

