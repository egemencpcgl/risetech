using MQTTnet.Server;
using MQTTnet;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace MQTTBroker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .ConfigureServices((context,services)=>
                services.AddHostedService<Broker>()).Build().Run();

        }

    }
}