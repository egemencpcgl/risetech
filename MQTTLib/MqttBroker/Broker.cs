﻿using MQTTnet.Server;
using MQTTnet;
using System.Text.Json;
using System.Text;

namespace MqttBroker
{
    public class Broker
    {
        static MqttServer server;

        public async static void StartBroker()
        {
            // Create the options for MQTT Broker
            var options = new MqttServerOptionsBuilder()
                //Set endpoint to localhost
                .WithDefaultEndpoint().WithDefaultEndpointPort(5004);
            // Create a new mqtt server
            server = new MqttFactory().CreateMqttServer(options.Build());
            //Add Interceptor for logging incoming messages
            server.InterceptingPublishAsync += Server_InterceptingPublishAsync;
            server.ClientConnectedAsync += Server_ClientConnectedAsync;
            server.ClientSubscribedTopicAsync += Server_ClientSubscribedTopicAsync;
            server.ClientAcknowledgedPublishPacketAsync += Server_ClientAcknowledgedPublishPacketAsync;
            // Start the server
            await server.StartAsync();
            // Keep application running until user press a key
            Console.ReadLine();

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
            // Convert Payload to string
            var payload = arg.ApplicationMessage?.Payload == null ? null : Encoding.UTF8.GetString(arg.ApplicationMessage?.Payload);
            var messageModel = JsonSerializer.Deserialize<string>(payload);

            Console.WriteLine(
                " TimeStamp: {0} -- Message: ClientId = {1}, Topic = {2}, Value={3}",

                DateTime.Now,
                arg.ClientId,
                arg.ApplicationMessage?.Topic,
                messageModel);
            //arg.ApplicationMessage?.QualityOfServiceLevel,
            //arg.ApplicationMessage?.Retain);
            return Task.CompletedTask;
        }
        private static Task Server_ClientConnectedAsync(ClientConnectedEventArgs arg)
        {
            var clients = server.GetClientsAsync();
            clients.Wait();
            return Task.CompletedTask;
        }
    }
}