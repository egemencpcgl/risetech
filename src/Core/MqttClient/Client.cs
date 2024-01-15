using System.Text.Json;
using System.Text;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet;
using MQTTnet.Client;
using MqttClient.MessageModel;
using MqttClient.Enums;
using System.Timers;

namespace MqttClient
{
    public class Client
    {
        public static IManagedMqttClient _mqttClient;
        static System.Timers.Timer _timer;
        static string IP;
        static string ClientID;
        static int Port;

        static void InitConnection()
        {
            _timer=new System.Timers.Timer();
            _timer.Interval = 5000;
            _timer.Elapsed += _retryConnection_Elapsed;
            _timer.Start();
        }

        private static void _retryConnection_Elapsed(object? sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            try
            {
                if (!_mqttClient.IsConnected)
                {
                    StartClient(IP, Port, ClientID);
                }
            }
            catch (Exception)
            {
            }
            _timer.Start();
            
        }

        public static async void StartClient(string ip, int port, string clientID)
        {
            IP = ip;
            Port= port;
            ClientID=clientID;
            _mqttClient = new MqttFactory().CreateManagedMqttClient();

            // Client icin ayarlar
            MqttClientOptionsBuilder builder = new MqttClientOptionsBuilder()
                                                    .WithClientId(clientID)
                                                    .WithTcpServer(ip, 5004);
            ManagedMqttClientOptions options = new ManagedMqttClientOptionsBuilder()
                                    .WithMaxPendingMessages(10000)
                                    .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                                    .WithClientOptions(builder.Build())
                                    .Build();



            // Handler ayarlamaları
            _mqttClient.ConnectedAsync += _mqttClient_ConnectedAsync;


            _mqttClient.DisconnectedAsync += _mqttClient_DisconnectedAsync;


            _mqttClient.ConnectingFailedAsync += _mqttClient_ConnectingFailedAsync;

            // Broker baglantisi

            await _mqttClient.StartAsync(options);
        }

        static Task _mqttClient_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
        {
            //if (_timer != null)
            //{
            //    //_timer.Start();
            //}
            Console.WriteLine("Disconnect broker!");
            return Task.CompletedTask;
        }

        static Task _mqttClient_ConnectedAsync(MqttClientConnectedEventArgs arg)
        {
            //if (_timer != null)
            //{
            //    _timer.Stop();

            //}
            Console.WriteLine("Connectted broker!");
            return Task.CompletedTask;
        }

        static Task _mqttClient_ConnectingFailedAsync(ConnectingFailedEventArgs arg)
        {
            if (_timer == null)
            {
                InitConnection();
            }
            Console.WriteLine("Connection failed check network or broker!");
            return Task.CompletedTask;
        }
        /// <summary>
        /// Clientların ilgili topiclere subscribe islemini gerceklestirir.
        /// </summary>
        /// <param name="topicName"></param>
        public static void SubscribeTopics(string topicName)
        {
            _mqttClient.SubscribeAsync(topicName, MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce);

        }
        /// <summary>
        /// Clientlardan Brokera mesaj gönderilir.
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="topicName"></param>
        /// <param name="messagetype"></param>
        /// <param name="data"></param>
        /// <returns></returns>
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