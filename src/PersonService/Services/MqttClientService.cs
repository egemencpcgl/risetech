using System.Text.Json;
using System.Text;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet;
using MQTTnet.Client;
using MqttClient;
using PersonServices.Interfaces;
using MqttClient.Enums;
using MQTTnet.Server;
using MqttClient.MessageModel;
using PersonServices.Context;

namespace PersonServices.Services
{
    public class MqttClientService : IMqttClientService
    {

        public readonly PgDbContext PgDbContext;
        public MqttClientService(PgDbContext pgDbContext)
        {
            PgDbContext = pgDbContext;
            //InitMqttClient();
        }

        public void InitMqttClient()
        {            
            Client.StartClient(System.Environment.GetEnvironmentVariable("BrokerIp"), 5004, "personClient");
            Client.SubscribeTopics(MessageTopic.Request.ToString());
            Client._mqttClient.ApplicationMessageReceivedAsync += _mqttClient_ApplicationMessageReceivedAsync;
        }

        private Task _mqttClient_ApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs args)
        {
            var payload = args.ApplicationMessage?.Payload == null ? null : Encoding.UTF8.GetString(args.ApplicationMessage?.Payload);
            var messageModel = JsonSerializer.Deserialize<MqttMessage>(payload);
            
            switch (messageModel.MessageType)
            {
                case MessageType.StatisticByLocation:
                    string location = Encoding.UTF8.GetString(messageModel.MessageData);
                    StatisticByLocation(messageModel.MessageId, location);
                    break;
                case MessageType.GetStatisticsAllLocation:
                    StatisticAllLocation(messageModel.MessageId);
                    break;
                default:
                    break;
            }
            return Task.CompletedTask;
        }

        Task StatisticAllLocation(Guid messageId)
        {
            try
            {
                List<string> cityList = new List<string>();
                foreach (var person in PgDbContext.Persons.ToList())
                {
                    var list = PgDbContext.Contacts.Where(x => x.PersonId == person.Id).Select(x => x.Location);
                    cityList.AddRange(list);
                }
                cityList = cityList.Distinct().ToList();
                List<Tuple<string, int, int>> reportlist = new List<Tuple<string, int, int>>();
                foreach (var city in cityList)
                {
                    var cityElement = PgDbContext.Contacts.Where(x => x.Location == city);

                    if (cityElement != null)
                    {
                        int personCount = cityElement.Select(x => x.PersonId).Distinct().Count();
                        int phoneCount = cityElement.Select(x => x.PhoneNumber).Distinct().Count();
                        Tuple<string, int, int> report = new Tuple<string, int, int>(city, personCount, phoneCount);
                        reportlist.Add(report);
                    }
                }
                return PrepareRawData(messageId, reportlist);

            }
            catch (Exception)
            {
                return PrepareRawData(messageId, new List<Tuple<string, int, int>>());

            }

        }


        Task StatisticByLocation(Guid messageId, string location)
        {
            try
            {
                var list = PgDbContext.Contacts.Where(x => x.Location == location).ToList();

                Tuple<string, int, int> report;

                if (list != null&& list.Count>0)
                {
                    int personCount = list.Select(x => x.PersonId).Distinct().Count();
                    int phoneCount = list.Select(x => x.PhoneNumber).Distinct().Count();
                    report = new Tuple<string, int, int>(location, personCount, phoneCount);

                }
                else
                {
                    report = new Tuple<string, int, int>("", 0, 0);
                }
                return PrepareRawData(messageId, report);

            }
            catch (Exception)
            {
                return PrepareRawData(messageId, new Tuple<string, int, int>("", 0, 0));

            }
            
        }


        static Task PrepareRawData(Guid messageId, List<Tuple<string, int, int>> report)
        {
            var json = JsonSerializer.Serialize(report);
            byte[] result = Encoding.UTF8.GetBytes(json);
            return Client.SendTopicAsync(messageId, MessageTopic.Response, MessageType.GetStatisticsAllLocation, result);
        }

        static Task PrepareRawData(Guid messageId, Tuple<string, int, int> report)
        {
            var json = JsonSerializer.Serialize(report);
            byte[] result = Encoding.UTF8.GetBytes(json);
            return Client.SendTopicAsync(messageId, MessageTopic.Response, MessageType.StatisticByLocation, result);
        }


        public void StartClient()
        {
            Client.StartClient(System.Environment.GetEnvironmentVariable("BrokerIp"), 5004, "personClient");
        }

        public void SubscribeTopic()
        {
            Client.SubscribeTopics(MessageTopic.Request.ToString());
        }

        public object? GetService(Type serviceType)
        {
            return this;
        }
    }
}
