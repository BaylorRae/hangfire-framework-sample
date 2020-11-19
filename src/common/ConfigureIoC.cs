using core;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using services;

namespace common
{
    public static class ConfigureIoC
    {
        public static void AddHangfireFrameworkServices(this IServiceCollection services)
        {
            services.AddTransient<IBackgroundJobClient, BackgroundJobClient>();
            services.AddTransient<IJobRunner,JobRunner>();
            services.AddTransient<ITaskService, TaskService>();            
        }
    }
}