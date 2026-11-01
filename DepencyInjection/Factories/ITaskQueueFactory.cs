using DepencyInjection.TaskQueue;
using System;
using System.Collections.Generic;
using System.Text;

namespace DepencyInjection.Factories
{
    public interface ITaskQueueFactory
    {
        ITaskQueue Create(string taskID);
    }
}
