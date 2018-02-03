using System;
using core;
using Hangfire;

namespace common
{
    public class JobRunner : IJobRunner
    {
        private IBackgroundJobClient BackgroundJobClient { get; }

        public JobRunner(IBackgroundJobClient backgroundJobClient)
        {
            BackgroundJobClient = backgroundJobClient;
        }
        
        public void Queue<TJob, TJobOptions>(Action<TJobOptions> configureJob) where TJob : IJob<TJobOptions> where TJobOptions : IJobOptions
        {
            var jobOptions = Activator.CreateInstance<TJobOptions>();
            configureJob(jobOptions);
            BackgroundJobClient.Enqueue<TJob>(job => job.Perform(jobOptions));
        }
    }
}