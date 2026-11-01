using DepencyInjection.Configuration;
using DepencyInjection.Factories;
using DepencyInjection.Service;
using DepencyInjection.TaskQueue;
using DepencyInjection.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace DepencyInjection.Extensions
{
    public static class HostBuildConfiguration
    {
        private static IConfigurationRoot? _configurationRoot;
        public static IHostBuilder AddJsonConfiguration(this IHostBuilder hostBuilder) 
        {
            hostBuilder.ConfigureAppConfiguration((hostingContext, configuration) => 
            {
                configuration.Sources.Clear();
                IHostEnvironment ENV = hostingContext.HostingEnvironment;

                configuration.AddJsonFile($"config{Path.DirectorySeparatorChar}config.json",optional: true, reloadOnChange: true);

                _configurationRoot =  configuration.Build();
            });

            return hostBuilder ;
        }
        public static IHostBuilder RegistreDI(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context,services) => 
            {
                services.AddOptions<TaskQueueConfiguration>();
                services.AddApiConfiguration(context.Configuration);
                services.AddService();

                services.AddHostedService<WorkerBackgroundService>();
            });
            return hostBuilder;
        }

        public static IServiceCollection AddService(this IServiceCollection servicesCollection)
        {
            servicesCollection.TryAddTransient<IReadTaskQueue, ReadTaskQueue>();
            servicesCollection.TryAddTransient<ITaskQueueFactory, TaskQueueFactory>();

            servicesCollection.AddTransient<ITaskQueue, WriteHelloWorldTask>();
            servicesCollection.AddTransient<ITaskQueue, WriteEndWorldTask>();
            servicesCollection.AddTransient<ITaskQueue, write_by_by>();

            return servicesCollection;
        }

        public static IServiceCollection AddApiConfiguration(this IServiceCollection servicesCollection, IConfiguration configuration)
        {
            servicesCollection.Configure<TaskQueueConfiguration>(configuration.GetSection("TaskQueueConfiguration"));
            return servicesCollection;
        }
    }
}
