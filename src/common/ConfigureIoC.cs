using core;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using services;
using StructureMap;

namespace common
{
    public static class ConfigureIoC
    {
        public static void AddHangfireFrameworkServices(this IContainer container)
        {
            container.Configure(_ =>
            {
                _.For<IBackgroundJobClient>().Use<BackgroundJobClient>();
                _.For<IJobRunner>().Use<JobRunner>();
                _.For<ITaskService>().Use<TaskService>();
            });
        }
    }
}