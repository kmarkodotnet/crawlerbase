using CrawlerBase.Logic.OperationPipeline.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.Dataflow
{
    public class DownloaderOperationEngine : OperationEngine<DownloadableData, ProcessableData>
    {
        public DownloaderOperationEngine(
            WorkerThreadPool<DownloadableData, ProcessableData> pageDownloaderThreadPool, 
            WorkerThreadPool<ProcessableData, DownloadableData> pageDataProcessorThreadPool) 
            : base(pageDownloaderThreadPool, pageDataProcessorThreadPool)
        {
        }

        public void SetupRootElement(IRootOperationElement rootElement)
        {
            if (rootElement is IRootEnumOperationElement reo)
            {
                var urls = reo.Selector.Select(null);
                urls.ForEach(url =>
                {
                    base.InsertQ1(new DownloadableData
                    {
                        Url = url,
                        OperationElement = rootElement.NextOperation
                    });
                });
                
            }
            else
            {
                base.InsertQ1(new DownloadableData
                {
                    Url = rootElement.BaseUrl,
                    OperationElement = rootElement.NextOperation
                });
            }
        }
    }
}
