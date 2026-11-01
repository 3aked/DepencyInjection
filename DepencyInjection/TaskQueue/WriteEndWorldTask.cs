using System;
using System.Collections.Generic;
using System.Text;

namespace DepencyInjection.TaskQueue
{
    public class WriteEndWorldTask : ITaskQueue
    {
        public string taskID => "write_end_world";

        public Task RunTaskAsync()
        {
            Console.WriteLine("write_end_world");
            return Task.CompletedTask;
        }
    }
}
