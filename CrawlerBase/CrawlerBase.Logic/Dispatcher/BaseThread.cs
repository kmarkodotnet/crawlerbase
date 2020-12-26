using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CrawlerBase.Logic.Dispatcher
{
    public abstract class BaseThread<Q, QI> : IWorkerThread
        where Q : WaiterQueue<QueueElement<QI>>
        where QI : class
    {
        protected Thread _thead;
        protected Q Queue { get; set; }

        public BaseThread()
        {
        }

        public void Initialize(Q queue)
        {
            this.Queue = queue;
        }

        public abstract void Start();
        public abstract void Stop();

        protected void StartThread()
        {
            _thead = new Thread(Processor);
            _thead.Start();
        }

        protected void WaitForThreadTermination()
        {
            _thead.Join();
        }

        protected abstract void Processor();
    }
}
