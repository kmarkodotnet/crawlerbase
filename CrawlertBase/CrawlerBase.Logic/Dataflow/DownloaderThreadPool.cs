using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.Dataflow
{
    public class DownloaderThreadPool: WorkerThreadPool<DownloadableData, ProcessableData>
    {
    }
}
