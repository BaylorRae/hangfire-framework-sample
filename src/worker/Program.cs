using System;
using Hangfire;
using StackExchange.Redis;

namespace worker
{
    class Program
    {
        static void Main(string[] args)
        {
            var redis = ConnectionMultiplexer.Connect("redis");

            GlobalConfiguration.Configuration.UseRedisStorage(redis);

            using (var server = new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started. Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
