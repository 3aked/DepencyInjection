using System;
using System.Collections.Generic;
using System.Text;

namespace DepencyInjection.TaskQueue
{
    public interface ITaskQueue
    {
        public  string taskID { get; }
        Task RunTaskAsync();
    }
}
