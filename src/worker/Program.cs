using System;
using common;
using Hangfire;
using Lamar;
using StackExchange.Redis;

namespace worker
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(_ => {
                _.IncludeRegistry<CommonRegistry>();
            });
            
            var redis = ConnectionMultiplexer.Connect("redis");

            GlobalConfiguration.Configuration.UseRedisStorage(redis);
            GlobalConfiguration.Configuration.UseActivator(
                new WorkerActivator(container)
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
        private readonly IContainer _container;

        public WorkerActivator(IContainer container)
        {
            _container = container;
        }

        public override object ActivateJob(Type jobType) => _container.GetInstance(jobType);
    }
}
