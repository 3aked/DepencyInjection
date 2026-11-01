using DepencyInjection.Models;

namespace DepencyInjection.Service
{
    public interface IReadTaskQueue
    {
        Task<IEnumerable<TaskQueueItem>> GetList (CancellationToken ct = default);
    }
}
