using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace CrawlerBase.Logic.Dataflow
{
    public interface IWorker<TIn, TOut>
    {
        IReceivableSourceBlock<TIn> InputQueue {get;set;}
        ITargetBlock<TOut> OutputQueue { get; set; }

        void Initialize(IReceivableSourceBlock<TIn> inputQueue, ITargetBlock<TOut> outputQueue);

        void Work();
    }
}
