using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using Grains;
using Polly;
using Orleans;
using Orleans.Configuration;

namespace API
{
    class Program
    {
        static void Main(string[] args)
        {
            OrleansClient.ClusterClient = Policy<IClusterClient>
            .Handle<Exception>()
            .WaitAndRetry(new[]
            {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(3)
            })
            .Execute(() => {
                IClusterClient client;
                client = new ClientBuilder()
                    .UseLocalhostClustering()
                    .Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "dev";
                        options.ServiceId = "SmartCache";
                    })
                    .Build();

                client.Connect().Wait();
                Console.WriteLine("WebAPP successfully connected to silo host \n");
                return client;
            });
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
              WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
