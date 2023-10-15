using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MyMQTTnet.ConsoleApp.Helpers;
using System.Text;

namespace MyMQTTnet.ConsoleApp.Client
{
    public class Client_Subscribe
    {
        public static async Task Handle_Received_Application_Message()
        {
            /*
             * This sample subscribes to a topic and processes the received message.
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
                //var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("broker.hivemq.com").Build();
                var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer(broker, port) // MQTT broker address and port
                    .WithCredentials(username, password) // Set username and password
                    .WithClientId(clientId)
                    .WithCleanSession()
                    .Build();

                // Setup message handling before connecting so that queued messages
                // are also handled properly. When there is no event handler attached all
                // received messages get lost.
                mqttClient.ApplicationMessageReceivedAsync += e =>
                {
                    Console.WriteLine("Received application message.");
                    e.DumpToConsole();

                    Console.WriteLine($"Received message (1) : {Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment)}");

                    return Task.CompletedTask;
                };

                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                    .WithTopicFilter(
                        f =>
                        {
                            f.WithTopic("arduino_eth1/sensor/temperature_celsius");
                        })
                    .Build();

                await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

                Console.WriteLine("MQTT client subscribed to topic.");

                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();
            }
        }



        public static async Task Subscribe_Topic()
        {
            /*
             * This sample subscribes to a topic.
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
                //var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("broker.hivemq.com").Build();
                var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer(broker, port) // MQTT broker address and port
                    .WithCredentials(username, password) // Set username and password
                    .WithClientId(clientId)
                    .WithCleanSession()
                    .Build();

                // Callback function when a message is received
                mqttClient.ApplicationMessageReceivedAsync += e =>
                {
                    Console.WriteLine($"Received message (2) : {Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment)}");
                    return Task.CompletedTask;
                };

                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                    .WithTopicFilter(
                        f =>
                        {
                            f.WithTopic("arduino_eth1/sensor/temperature_celsius");
                        })
                    .Build();

                var response = await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

                Console.WriteLine("MQTT client subscribed to topic.");

                // The response contains additional data sent by the server after subscribing.
                response.DumpToConsole();
            }
        }



        //
    }
}
