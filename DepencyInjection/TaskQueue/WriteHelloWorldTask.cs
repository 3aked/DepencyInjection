using System;
using System.Collections.Generic;
using System.Text;

namespace DepencyInjection.TaskQueue
{
    public class WriteHelloWorldTask : ITaskQueue
    {
        public string taskID => "write_hello_world";

        public Task RunTaskAsync()
        {
            Console.WriteLine("write_hello_world");
            return Task.CompletedTask;
        }
    }
}
