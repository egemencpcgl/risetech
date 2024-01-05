using MQTTnet;
using MQTTnet.Server;
using System.Text;

namespace Broker
{
    public class Program
    {
        static MqttServer server;
        static void Main(string[] args)
        {
            Console.WriteLine("MQBroker Started...");
            StartBroker();
        }

        public async static void StartBroker()
        {
            // MQTT Broker icin ayarlamalar
            var options = new MqttServerOptionsBuilder()
                //localhost icin endpoint
                .WithDefaultEndpoint().WithDefaultEndpointPort(5004);

            server = new MqttFactory().CreateMqttServer(options.Build());

            //kullanılabilecek event tanımlamarı
            server.InterceptingPublishAsync += Server_InterceptingPublishAsync;
            server.ClientConnectedAsync += Server_ClientConnectedAsync;
            server.ClientDisconnectedAsync += Server_ClientDisconnectedAsync;
            server.ClientSubscribedTopicAsync += Server_ClientSubscribedTopicAsync;
            server.ClientAcknowledgedPublishPacketAsync += Server_ClientAcknowledgedPublishPacketAsync;
            // Brokeri baslat
            await server.StartAsync();


            Console.ReadLine();
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
    }
}