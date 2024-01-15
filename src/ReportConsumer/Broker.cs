using Microsoft.Extensions.Hosting;
using MQTTnet.Server;
using MQTTnet;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using MQTTBroker.MessageModel;

namespace MQTTBroker
{
    public class Broker : IHostedService
    {
        static MqttServer server;

        public async static Task StartBroker()
        {
            // MQTT Broker icin ayarlamalar
            var options = new MqttServerOptionsBuilder().WithDefaultEndpoint().WithDefaultEndpointPort(5004).WithMaxPendingMessagesPerClient(10000);

            server = new MqttFactory().CreateMqttServer(options.Build());

            //kullanılabilecek event tanımlamarı
            server.InterceptingPublishAsync += Server_InterceptingPublishAsync;
            server.ClientConnectedAsync += Server_ClientConnectedAsync;
            server.ClientDisconnectedAsync += Server_ClientDisconnectedAsync;
            server.ClientSubscribedTopicAsync += Server_ClientSubscribedTopicAsync;
            server.ClientAcknowledgedPublishPacketAsync += Server_ClientAcknowledgedPublishPacketAsync;
            // Brokeri baslat
            Console.WriteLine("Broker started.");
            await server.StartAsync();

        }

        private static Task Server_ClientDisconnectedAsync(ClientDisconnectedEventArgs arg)
        {
            Console.WriteLine("Client disconnected, Client name: " + arg.ClientId);
            return Task.CompletedTask;
        }

        private static Task Server_ClientAcknowledgedPublishPacketAsync(ClientAcknowledgedPublishPacketEventArgs arg)
        {
            return Task.CompletedTask;
        }

        private static Task Server_ClientSubscribedTopicAsync(ClientSubscribedTopicEventArgs arg)
        {
            return Task.CompletedTask;
        }

        private static Task Server_InterceptingPublishAsync(InterceptingPublishEventArgs arg)
        {
            // Broker tarafında mesaj ile ilgili bir publish disinda bir islem yapılacaksa buradan yapilabilir.
            // Simdilik gerek yok...
            var payload = arg.ApplicationMessage?.Payload == null ? null : Encoding.UTF8.GetString(arg.ApplicationMessage?.Payload);

            var msj= JsonConvert.DeserializeObject<MqttMessage>(payload);

            Console.WriteLine(msj.MessageId.ToString() + " " + msj.MessageTopic.ToString() + " " + msj.MessageType.ToString());
            return Task.CompletedTask;
        }

        /// <summary>
        /// Yeni baglanan Clientlar yakalanır.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private static Task Server_ClientConnectedAsync(ClientConnectedEventArgs arg)
        {
            var clients = server.GetClientsAsync();
            clients.Wait();
            Console.WriteLine("New client connected, Client name: " + arg.ClientId);
            return Task.CompletedTask;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await StartBroker();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;

        }
    }
}
