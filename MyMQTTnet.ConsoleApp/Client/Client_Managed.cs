

using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Server;
using System.Text;
using System.Text.Json;

namespace MyMQTTnet.ConsoleApp.Client
{
    public class Client_Managed
    {
        public static async Task ManagedMQTTClient()
        {
            //
            string broker = "m16.cloudmqtt.com";
            int port = 18164;
            string clientId = Guid.NewGuid().ToString();
            string topicP = "mymqttnet/managed/Pmsg";
            string topicS = "mymqttnet/managed/Smsg";
            string username = "wcyciokj";
            string password = "UrDMT14vdGCj";

            var mqttFactory = new MqttFactory();

            using (var managedMqttClient = mqttFactory.CreateManagedMqttClient())
            {
                MqttClientOptionsBuilder builder = new MqttClientOptionsBuilder()
                    .WithTcpServer(broker, port) // MQTT broker address and port
                    .WithCredentials(username, password) // Set username and password
                    .WithClientId(clientId)
                    //.WithKeepAlivePeriod(TimeSpan.FromSeconds(DEFAULT_KEEPLIVE))
                    .WithCleanSession();

                ManagedMqttClientOptions options = new ManagedMqttClientOptionsBuilder()
                    .WithAutoReconnectDelay(TimeSpan.FromSeconds(30))
                    .WithClientOptions(builder.Build())
                    .Build();


                // ---------------------- Publish 
                await managedMqttClient.StartAsync(options);

                string json = JsonSerializer.Serialize(new
                {
                    message = "Hi Mqtt",
                    sent = DateTime.UtcNow,
                    tvalue = 31.4
                });

                // The application message is not sent. It is stored in an internal queue and
                // will be sent when the client is connected.
                await managedMqttClient.EnqueueAsync(topicP, json);

                Console.WriteLine("The managed MQTT client is connected.");

                // Wait until the queue is fully processed.
                SpinWait.SpinUntil(() => managedMqttClient.PendingApplicationMessagesCount == 0, 10000);

                Console.WriteLine($"Pending messages = {managedMqttClient.PendingApplicationMessagesCount}");
                // ---------------------- Publish


                // ---------------------- Subscribe
                await managedMqttClient.SubscribeAsync(topicS);

                // Callback function when a message is received
                managedMqttClient.ApplicationMessageReceivedAsync += e =>
                {
                    Console.WriteLine($"Received message: {Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment)}");
                    return Task.CompletedTask;
                };
                // ---------------------- Subscribe


                Console.ReadLine();


            }


            //await Task.Delay(TimeSpan.FromSeconds(1));
            //
        }


    }
}
