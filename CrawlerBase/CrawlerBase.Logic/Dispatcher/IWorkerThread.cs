using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.Dispatcher
{
    public interface IWorkerThread
    {
        void Start();
        void Stop();
    }
}
