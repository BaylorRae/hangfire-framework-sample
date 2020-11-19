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
            var services = new ServiceCollection();            
            services.AddHangfireFrameworkServices();
            
            var redis = ConnectionMultiplexer.Connect("redis");

            GlobalConfiguration.Configuration.UseRedisStorage(redis);
            GlobalConfiguration.Configuration.UseActivator(
                new WorkerActivator(services)
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
        private readonly IServiceProvider _serviceProvider;

        public WorkerActivator(IServiceCollection services)
        {
            _serviceProvider = services.BuildServiceProvider();
        }

        public override object ActivateJob(Type jobType) => _serviceProvider.GetService(jobType);
    }
}
