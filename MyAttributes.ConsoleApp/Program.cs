using Microsoft.Extensions.Configuration;
using MyAttributes.ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        
        IConfiguration config = builder.Build();
        string? brokerUri = config.GetSection("ActiveMQ").GetSection("BrokerUri").Value;
        string? requestQueueName = config.GetSection("ActiveMQ").GetSection("RequestQueueName").Value;
        Dictionary<string, string>? requestFilePath = config.GetSection("Path:RequestFilePath").Get<Dictionary<string, string>>();
        string xmlResponseContent = string.Empty;

        // Loop through the RequestFilePaths  if there are more than one
        foreach (var keyValuePair in requestFilePath)
        {
            string fileType = keyValuePair.Key;
            string filePath = keyValuePair.Value.ToString();
            xmlResponseContent = System.IO.File.ReadAllText(filePath);
            // Send messages to the queue
            //SendMessage(brokerUri, requestQueueName, xmlResponseContent);
        }
        Thread.Sleep(120);

        //var greeting = config.GetValue(typeof(String), "AppSettings:Greeting").ToString();
        var myConnectString = config.GetConnectionString("MyConnectionString");
        //Console.WriteLine(greeting);
        Console.WriteLine(myConnectString);

        var timeout = config.GetSection("ApiConfiguration:Timeout").Value;
        //var apiConfiguration = new ApiConfiguration();
        //config.Bind("ApiConfiguration", apiConfiguration);
        Console.WriteLine($"Hello, the value for the timeout is {timeout}.");
        //Console.WriteLine($"Hello, the value for the enabled is {apiConfiguration.Enabled}.");

        Console.Read();


        var checker = new ValueChecker
        {
            PositiveValue = 42,
            NegativeValue = -42
        };

        checker.CheckValue(checker);
    }
}
