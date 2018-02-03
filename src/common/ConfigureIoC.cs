using Microsoft.Extensions.DependencyInjection;
using services;

namespace common
{
    public static class ConfigureIoC
    {

        public static void AddHangfireFrameworkServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITaskService, TaskService>();
        }
    }
}