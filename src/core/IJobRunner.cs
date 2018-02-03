using System;

namespace core
{
    public interface IJobOptions
    {
    }

    public interface IJob<in TJobOptions> where TJobOptions : IJobOptions
    {
        void Perform(TJobOptions jobOptions);
    }
    
    public interface IJobRunner
    {
        void Queue<TJob, TJobOptions>(Action<TJobOptions> configureJob)
            where TJobOptions : IJobOptions
            where TJob : IJob<TJobOptions>;
    }
}