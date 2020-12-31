using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.Dataflow
{
    public class DownloaderThreadPool : WorkerThreadPool<DownloadableData, ProcessableData>
    {
        public DownloaderThreadPool(ILogger logger) : base(logger)
        {
        }
    }
}
