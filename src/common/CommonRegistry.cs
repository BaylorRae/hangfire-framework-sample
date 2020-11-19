using core;
using Hangfire;
using Lamar;
using services;

namespace common
{    public class CommonRegistry : ServiceRegistry
    {
        public CommonRegistry()
        {
           For<IBackgroundJobClient>().Use<BackgroundJobClient>();
           For<IJobRunner>().Use<JobRunner>();
           For<ITaskService>().Use<TaskService>();
        }
    }
}