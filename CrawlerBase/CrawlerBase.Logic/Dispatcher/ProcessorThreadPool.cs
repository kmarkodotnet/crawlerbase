using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.Dispatcher
{
    public abstract class ProcessorThreadPool<BT, Q, QI> : List<BT>
        where BT : ProcessorThread<Q, QI>, new()
        where Q : WaiterQueue<QueueElement<QI>>
        where QI : class
    {
        protected int Size { get; set; }
        protected Q Queue { get; set; }

        public ProcessorThreadPool()
        {
        }

        public void Initialize(Q queue)
        {
            this.Queue = queue;
            foreach (var t in this)
            {
                t.Initialize(queue);
            }
        }

        public void SetSize(int size)
        {
            this.Size = size;
            for (int i = 0; i < size; i++)
            {
                this.Add(new BT());
            }
        }

        public void Start()
        {
            BeforeAllStart();
            this.ForEach(bpt => bpt.Start());
            AfterAllStart();
        }

        public void Stop()
        {
            BeforeAllStop();
            for (int i = 0; i < this.Size; i++)
            {
                this.Queue.Put(this.GetStopElement());
            }
            this.ForEach(bpt => bpt.Stop());
            AfterAllStop();
        }

        protected abstract void BeforeAllStart();
        protected abstract void BeforeAllStop();
        protected abstract void AfterAllStart();
        protected abstract void AfterAllStop();
        protected abstract QueueElement<QI> GetStopElement();
    }
}
