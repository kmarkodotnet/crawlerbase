using CrawlerBase.DataAccess;
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
                    RegisterDownloadContent(string.Empty, url);
                    base.InsertQ1(new DownloadableData
                    {
                        ParentUrl = string.Empty,
                        Url = url,
                        OperationElement = rootElement.NextOperation
                    });
                });
                
            }
            else
            {
                RegisterDownloadContent(string.Empty, rootElement.BaseUrl);
                base.InsertQ1(new DownloadableData
                {
                    ParentUrl = string.Empty,
                    Url = rootElement.BaseUrl,
                    OperationElement = rootElement.NextOperation
                });
            }
        }

        private void RegisterDownloadContent(string parentUrl, string url)
        {
            using (var context = new CrawlerContext(Configuration.Instance.DbConnectionString))
            {
                context.RegisterDownloadContent(parentUrl, url);
            }
        }
    }
}
