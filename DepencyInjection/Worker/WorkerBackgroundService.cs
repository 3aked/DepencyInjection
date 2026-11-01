using DepencyInjection.Factories;
using DepencyInjection.Service;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DepencyInjection.Worker
{
    public sealed class WorkerBackgroundService : BackgroundService
    {
        private readonly IReadTaskQueue _reader;
        private readonly ITaskQueueFactory _factory;
        private readonly IHostApplicationLifetime _lifetime;
        private readonly ILogger<WorkerBackgroundService> _logger;

        public WorkerBackgroundService (IReadTaskQueue reader, ITaskQueueFactory factory, IHostApplicationLifetime lifetime, ILogger<WorkerBackgroundService> logger)
        {
            _reader = reader;
            _factory = factory;
            _lifetime = lifetime;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var items = await _reader.GetList(stoppingToken);
                    foreach (var item in items)
                    {
                        _logger.LogInformation($"Exécution de {item.taskID}");
                        var task = _factory.Create(item.taskID);

                        await task.RunTaskAsync();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "L'exéctuion des tasks échoué.");
                }

                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
            
        }
    }
}
