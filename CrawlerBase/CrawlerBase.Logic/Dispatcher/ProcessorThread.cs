using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.Dispatcher
{
    public abstract class ProcessorThread<Q, QI> : BaseThread<Q, QI>
        where Q : WaiterQueue<QueueElement<QI>>
        where QI : class
    {
        protected int ThreadId { get; private set; }

        public ProcessorThread()
        {
        }

        public override void Start()
        {
            StartThread();
            ThreadId = _thead.ManagedThreadId;
            AfterStart();
        }

        /// <summary>
        /// többet már nem konfirmálunk, de ami még hátra van azt konfirmáljuk
        /// </summary>
        public override void Stop()
        {
            WaitForThreadTermination();
            AfterStop();
        }

        protected override void Processor()
        {
            while (true)
            {
                var processableElement = this.Queue.Get();

                if (processableElement.ProcessingInstruction == ProcessingInstruction.Stop)
                {
                    StopMessageReceived();
                    break;
                }
                else if (processableElement.ProcessingInstruction == ProcessingInstruction.Process)
                {
                    try
                    {
                        ProcessOne(processableElement);
                    }
                    catch (Exception ex)
                    {
                        //TODO 
                    }
                }
                else
                {
                    throw new Exception("ilyen nincs és mégis van");
                }
            }
        }

        protected abstract void ProcessOne(QueueElement<QI> processableElement);

        protected abstract void StopMessageReceived();
        protected abstract void AfterStart();
        protected abstract void AfterStop();
    }
}
