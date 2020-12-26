using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace CrawlerBase.Logic.Dataflow
{
    public class OperationEngine<T1, T2>
    {
        private readonly WorkerThreadPool<T1, T2> pageDownloaderThreadPool;
        private readonly WorkerThreadPool<T2, T1> pageDataProcessorThreadPool;

        public BufferBlock<T1> DownloadUrlQueue { get; set; }
        public BufferBlock<T2> ProcessDataQueue { get; set; }

        public OperationEngine(WorkerThreadPool<T1, T2> pageDownloaderThreadPool, WorkerThreadPool<T2, T1> pageDataProcessorThreadPool)
        {
            DownloadUrlQueue = new BufferBlock<T1>();
            ProcessDataQueue = new BufferBlock<T2>();

            this.pageDownloaderThreadPool = pageDownloaderThreadPool;

            this.pageDataProcessorThreadPool = pageDataProcessorThreadPool;
        }

        protected void InsertQ1(T1 insertData)
        {
            DownloadUrlQueue.Post<T1>(insertData);
        }
        protected void InsertQ2(T2 insertData)
        {
            ProcessDataQueue.Post<T2>(insertData);
        }

        public void Initialize(List<WorkerBase<T1, T2>> pageDownloaderWorkers, List<WorkerBase<T2, T1>> pageDataProcessorWorkers)
        {
            this.pageDownloaderThreadPool.InitializeWorkers(pageDownloaderWorkers);
            this.pageDownloaderThreadPool.InitializeQueues(DownloadUrlQueue, ProcessDataQueue);
            this.pageDataProcessorThreadPool.InitializeWorkers(pageDataProcessorWorkers);
            this.pageDataProcessorThreadPool.InitializeQueues(ProcessDataQueue, DownloadUrlQueue);
        }

        public void Start()
        {
            pageDownloaderThreadPool.Start();
            pageDataProcessorThreadPool.Start();
            //pageDownloaderThreadPool.Work();
            //pageDataProcessorThreadPool.Work();
        }
    }
}
