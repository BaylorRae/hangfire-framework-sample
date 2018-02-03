using System;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using core;
using services.Jobs;

namespace services
{
    public interface ITaskService
    {
        void LongRunningTask();
        void RepeatingTask();
    }
        
    public class TaskService : ITaskService
    {
        private IJobRunner JobRunner { get; }

        public TaskService(IJobRunner jobRunner)
        {
            JobRunner = jobRunner;
        }
        
        public void LongRunningTask()
        {
            Console.WriteLine("Queue: LongRunningTask");
            JobRunner.QueueLongRunningJob(options => { options.TaskTime = 10; });
        }

        public void RepeatingTask()
        {
            Console.WriteLine($"RepeatingTask: {DateTime.Now}");
        }
    }
}