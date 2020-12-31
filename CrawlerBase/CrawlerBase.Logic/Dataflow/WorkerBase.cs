using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace CrawlerBase.Logic.Dataflow
{
    public abstract class WorkerBase<T1, T2> : IWorker<T1, T2>
    {
        protected Thread _thead;
        protected int ThreadId { get; private set; }
        public IReceivableSourceBlock<T1> InputQueue { get; set; }
        public ITargetBlock<T2> OutputQueue { get; set; }

        public void Initialize(IReceivableSourceBlock<T1> inputQueue, ITargetBlock<T2> outputQueue)
        {
            this.InputQueue = inputQueue;
            this.OutputQueue = outputQueue;

        }

        public void Start()
        {
            _thead = new Thread(Processor);
            _thead.Start();
            ThreadId = _thead.ManagedThreadId;
            var typeName = this.GetType().Name;
            GetLogger().Debug(string.Format("{0} thread started ({1})", typeName, ThreadId));
        }

        public void Stop()
        {
            _thead.Abort();
        }
        public void Processor()
        {
            Work();
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
        void IWorker<T1, T2>.Work()
        {
            Work();
        }

        protected abstract Task<List<T2>> ProcessData(T1 data);
        protected abstract ILogger GetLogger();

    }
}
