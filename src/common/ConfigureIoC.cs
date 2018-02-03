using Microsoft.Extensions.DependencyInjection;
using services;

namespace common
{
    public static class ConfigureIoC
    {

        public static void AddHangefireFrameworkSerivces(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ITaskService, TaskService>();
        }
    }
}