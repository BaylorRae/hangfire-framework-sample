using System;
using common;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace worker
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            
            serviceCollection.AddHangefireFrameworkSerivces();
            
            var redis = ConnectionMultiplexer.Connect("redis");

            GlobalConfiguration.Configuration.UseRedisStorage(redis);
            GlobalConfiguration.Configuration.UseActivator(
                new WorkerActivator(serviceCollection.BuildServiceProvider(false))
            );

            using (var server = new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started. Press any key to exit...");
                Console.ReadKey();
            }
        }
    }

    internal class WorkerActivator : JobActivator
    {
        private IServiceProvider ServiceProvider { get; }
        
        public WorkerActivator(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public override object ActivateJob(Type jobType) => ServiceProvider.GetService(jobType);
    }
}
