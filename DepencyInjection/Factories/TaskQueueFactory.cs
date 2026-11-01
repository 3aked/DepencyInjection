using DepencyInjection.TaskQueue;
using System;
using System.Collections.Generic;
using System.Text;

namespace DepencyInjection.Factories
{
    public class TaskQueueFactory : ITaskQueueFactory
    {
        private readonly IEnumerable<ITaskQueue> _tasks;

        public TaskQueueFactory (IEnumerable<ITaskQueue> tasks)
        {
            _tasks = tasks;
        }

        public ITaskQueue Create(string taskID)
        {
            return _tasks.FirstOrDefault(e =>
                string.Equals(e.taskID, taskID)
            )?? throw new InvalidOperationException($"Tâche inconnue : {taskID}");
        }
    }
}
