using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.Dataflow
{
    public class PageContentProcessorThreadPool : WorkerThreadPool<ProcessableData, DownloadableData>
    {
        public PageContentProcessorThreadPool(ILogger logger) : base(logger)
        {
        }
    }
}
