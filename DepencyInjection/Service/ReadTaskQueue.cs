using DepencyInjection.Configuration;
using DepencyInjection.Models;
using Microsoft.Extensions.Options;

namespace DepencyInjection.Service
{
    public class ReadTaskQueue : IReadTaskQueue
    {
        private readonly string _queuePath;

        public ReadTaskQueue(IOptions<TaskQueueConfiguration> options)
        {
            _queuePath = Path.GetFullPath(options.Value.RootPath, AppContext.BaseDirectory);
        }
        public async Task<IEnumerable<TaskQueueItem>> GetList(CancellationToken ct = default)
        {
            string[] lines = await File.ReadAllLinesAsync(_queuePath, ct);
            List<TaskQueueItem> tasks = [];

            foreach (string line in lines)
            {
                string text = line.Trim();
                if (string.IsNullOrEmpty(text)) continue;
                tasks.Add(new TaskQueueItem
                { 
                    taskID = text[2..].Trim().Replace("\\_", "_")
                });
            }

            return tasks;
        }
    }
}
