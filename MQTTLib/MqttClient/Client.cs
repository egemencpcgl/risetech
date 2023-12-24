using System.Text.Json;
using System.Text;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet;
using MQTTnet.Client;
using MqttClient.MessageModel;
using MqttClient.Enums;

namespace MqttClient
{
    public class Client
    {
        public static IManagedMqttClient _mqttClient;

        public static async void StartClient(string ip, int port, string clientID)
        {
            _mqttClient = new MqttFactory().CreateManagedMqttClient();

            // Create client options object
            MqttClientOptionsBuilder builder = new MqttClientOptionsBuilder()
                                                    .WithClientId(clientID)
                                                    .WithTcpServer(ip, 5004);
            ManagedMqttClientOptions options = new ManagedMqttClientOptionsBuilder()
                                    .WithAutoReconnectDelay(TimeSpan.FromSeconds(60))
                                    .WithClientOptions(builder.Build())
                                    .Build();



            // Set up handlers
            _mqttClient.ConnectedAsync += _mqttClient_ConnectedAsync;


            _mqttClient.DisconnectedAsync += _mqttClient_DisconnectedAsync;


            _mqttClient.ConnectingFailedAsync += _mqttClient_ConnectingFailedAsync;

            // Connect to the broker
            await _mqttClient.StartAsync(options);
        }

        static Task _mqttClient_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            return Task.CompletedTask;
        }

        static Task _mqttClient_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            return Task.CompletedTask;
        }

        static Task _mqttClient_ConnectingFailedAsync(ConnectingFailedEventArgs arg)
        {
            Console.WriteLine("Connection failed check network or broker!");
            return Task.CompletedTask;
        }

        public static void SubscribeTopics(string topicName)
        {
            _mqttClient.SubscribeAsync(topicName, MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce);

        }
        public Task _mqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs arg)
        {
            var payload = arg.ApplicationMessage?.Payload == null ? null : Encoding.UTF8.GetString(arg.ApplicationMessage?.Payload);
            var messageModel = JsonSerializer.Deserialize<string>(payload);

            return Task.CompletedTask;
        }

        public static async Task SendTopicAsync(Guid messageId,MessageTopic topicName, MessageType messagetype, byte[] data=null)
        {
            try
            {
                MqttMessage mqttMessage = new MqttMessage()
                {
                    MessageId = messageId,
                    MessageType = messagetype,
                    MessageData = data
                };

                var rawmsg=JsonSerializer.Serialize(mqttMessage);
                await _mqttClient.EnqueueAsync(topicName.ToString(), rawmsg);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}