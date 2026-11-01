using System;
using System.Collections.Generic;
using System.Text;

namespace DepencyInjection.TaskQueue
{
    public class write_by_by : ITaskQueue
    {
        public string taskID => "write_by_by";

        public Task RunTaskAsync()
        {
            Console.WriteLine("write_by_by");
            return Task.CompletedTask;
        }
    }
}
