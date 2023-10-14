using MQTTnet.Client;
using MQTTnet;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace MyMQTTnet.ConsoleApp.Client
{
    public class Client_Publish
    {
        public static async Task Publish_Application_Message()
        {
            /*
             * This sample pushes a simple application message including a topic and a payload.
             *
             * Always use builders where they exist. Builders (in this project) are designed to be
             * backward compatible. Creating an _MqttApplicationMessage_ via its constructor is also
             * supported but the class might change often in future releases where the builder does not
             * or at least provides backward compatibility where possible.
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
                //var mqttClientOptions = new MqttClientOptionsBuilder()
                //    .WithTcpServer("broker.hivemq.com")
                //    .Build();
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
                        var response = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                        if (response.ResultCode == MqttClientConnectResultCode.Success)
                        {
                            Console.WriteLine("The MQTT client is connected.");

                            string json = JsonSerializer.Serialize(new { 
                                message = "Hi Mqtt", 
                                sent = DateTime.UtcNow, 
                                tvalue = 31.4 
                            });

                            var applicationMessage = new MqttApplicationMessageBuilder()
                                .WithTopic("mymqttnet/temperature/living_room")
                                .WithPayload(json)
                                .Build();


                            await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
                            Console.WriteLine("MQTT application message is published.");

                            await Task.Delay(TimeSpan.FromSeconds(2));
                            await mqttClient.DisconnectAsync();

                        }
                        else
                        {
                            Console.WriteLine($"Failed to connect to MQTT broker: {response.ResultCode}");
                        }
                        //
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Timeout while connecting.");
                }

                //
            }



            //
        }
    }
}
