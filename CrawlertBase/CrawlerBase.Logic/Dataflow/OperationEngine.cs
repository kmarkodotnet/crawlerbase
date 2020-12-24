using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace CrawlerBase.Logic.Dataflow
{
    public class OperationEngine<T1, T2>
    {
        private readonly IWorker<T1, T2> pageDownloader;
        private readonly IWorker<T2, T1> pageDataProcessor;

        public BufferBlock<T1> DownloadUrlQueue { get; set; }
        public BufferBlock<T2> ProcessDataQueue { get; set; }

        public OperationEngine(IWorker<T1, T2> pageDownloader, IWorker<T2, T1> pageDataProcessor)
        {
            DownloadUrlQueue = new BufferBlock<T1>();
            ProcessDataQueue = new BufferBlock<T2>();

            this.pageDownloader = pageDownloader;
            this.pageDownloader.Initialize(DownloadUrlQueue, ProcessDataQueue);

            this.pageDataProcessor = pageDataProcessor;
            this.pageDataProcessor.Initialize(ProcessDataQueue, DownloadUrlQueue);
        }

        protected void InsertQ1(T1 insertData)
        {
            DownloadUrlQueue.Post<T1>(insertData);
        }
        protected void InsertQ2(T2 insertData)
        {
            ProcessDataQueue.Post<T2>(insertData);
        }

        public void Start()
        {
            pageDownloader.Work();
            pageDataProcessor.Work();
        }
    }
}
