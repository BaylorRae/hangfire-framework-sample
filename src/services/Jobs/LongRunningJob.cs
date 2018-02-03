using System;
using System.Diagnostics;
using System.Threading;
using core;

namespace services.Jobs
{
    public class LongRunningJobOptions : IJobOptions
    {
        public int TaskTime { get; set; } = 2;
    }
        
    public class LongRunningJob : IJob<LongRunningJobOptions>
    {
        public void Perform(LongRunningJobOptions options)
        {
            Console.WriteLine("LongRunningTask: Started");

            var stopWatch = Stopwatch.StartNew();
            Thread.Sleep(TimeSpan.FromSeconds(options.TaskTime));
            
            Console.WriteLine($"LongRunningTask: Finished ({stopWatch.Elapsed})");
        }
    }

    public static class LongRunningJobExtension
    {
        public static void QueueLongRunningJob(this IJobRunner jobRunner, Action<LongRunningJobOptions> configureJob)
        {
            jobRunner.Queue<LongRunningJob, LongRunningJobOptions>(configureJob);
        }
    }
}