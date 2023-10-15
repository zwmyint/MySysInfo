using MQTTnet.Client;
using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMQTTnet.ConsoleApp.Helpers;

namespace MyMQTTnet.ConsoleApp.Client
{
    public static class Client_Connection
    {
        //
        public static async Task Connect_Client()
        {
            /*
             * This sample creates a simple MQTT client and connects to a public broker.
             *
             * Always dispose the client when it is no longer used.
             * The default version of MQTT is 3.1.1.
             */

            string broker = "m16.cloudmqtt.com";
            int port = 18164;
            string clientId = Guid.NewGuid().ToString();
            //string topic = "csharp/mqtt";
            string username = "wcyciokj";
            string password = "UrDMT14vdGCj";


            var mqttFactory = new MqttFactory();

            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                // Use builder classes where possible in this project.
                ////var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("broker.hivemq.com").Build();
                // Create MQTT client options
                var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer(broker, port) // MQTT broker address and port
                    .WithCredentials(username, password) // Set username and password
                    .WithClientId(clientId)
                    .WithCleanSession()
                    .Build();
                try
                {
                    using (var timeoutToken = new CancellationTokenSource(TimeSpan.FromSeconds(1)))
                    {
                        // This will throw an exception if the server is not available.
                        // The result from this message returns additional data which was sent 
                        // from the server. Please refer to the MQTT protocol specification for details.
                        var response = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                        Console.WriteLine("The MQTT client is connected.");

                        var applicationMessage = new MqttApplicationMessageBuilder()
                        .WithTopic("mymqttnet/init/msg")
                        .WithPayload("io01.app")
                        .Build();

                        await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);

                        Console.WriteLine("MQTT application message is published.");

                        response.DumpToConsole();

                        // Send a clean disconnect to the server by calling _DisconnectAsync_. Without this the TCP connection
                        // gets dropped and the server will handle this as a non clean disconnect (see MQTT spec for details).
                        var mqttClientDisconnectOptions = mqttFactory.CreateClientDisconnectOptionsBuilder().Build();

                        await Task.Delay(TimeSpan.FromSeconds(2));
                        await mqttClient.DisconnectAsync(mqttClientDisconnectOptions, CancellationToken.None);
                        
                        //
                    }
                }
                catch(OperationCanceledException)
                {
                    Console.WriteLine("Timeout while connecting.");
                }

                //
            }
        }



        //
    }
}
