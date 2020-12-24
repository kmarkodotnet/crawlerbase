using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace CrawlerBase.Logic.Dataflow
{
    public abstract class WorkerBase<T1, T2> : IWorker<T1, T2>
    {
        public IReceivableSourceBlock<T1> InputQueue { get; set; }
        public ITargetBlock<T2> OutputQueue { get; set; }

        public void Initialize(IReceivableSourceBlock<T1> inputQueue, ITargetBlock<T2> outputQueue)
        {
            this.InputQueue = inputQueue;
            this.OutputQueue = outputQueue;
        }

        public async Task Work()
        {
            while (await InputQueue.OutputAvailableAsync())
            {
                while (InputQueue.TryReceive(out T1 data))
                {
                    var processedData = await ProcessData(data);
                    processedData.ForEach(d => OutputQueue.Post(d));
                    //OutputQueue.Complete();
                }
            }
        }

        protected abstract Task<List<T2>> ProcessData(T1 data);

        void IWorker<T1, T2>.Work()
        {
            Work();
        }
    }
}
