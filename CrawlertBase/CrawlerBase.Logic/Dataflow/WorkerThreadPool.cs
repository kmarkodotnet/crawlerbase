using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace CrawlerBase.Logic.Dataflow
{
    public class WorkerThreadPool<T1, T2>
    {
        public List<WorkerBase<T1, T2>> Workers { get; set; }

        public void InitializeWorkers(List<WorkerBase<T1, T2>> workers)
        {
            Workers = workers;
        }

        public void Start()
        {
            Workers.ForEach(w => w.Start());
        }

        public void Stop()
        {
            Workers.ForEach(w => w.Stop());
        }
        public void InitializeQueues(IReceivableSourceBlock<T1> inputQueue, ITargetBlock<T2> outputQueue)
        {
            Workers.ForEach(w => w.Initialize(inputQueue, outputQueue));
        }
    }
}
