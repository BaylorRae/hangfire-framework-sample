using System;
using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace services
{
    public interface ITaskService
    {
        void LongRunningTask();
        void RepeatingTask();
    }
        
    public class TaskService : ITaskService
    {
        public void LongRunningTask()
        {
            Console.WriteLine("LongRunningTask: Started");

            var stopWatch = Stopwatch.StartNew();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            
            Console.WriteLine($"LongRunningTask: Finished ({stopWatch.Elapsed})");
        }

        public void RepeatingTask()
        {
            Console.WriteLine($"RepeatingTask: {DateTime.Now}");
        }
    }
}