using CrawlerBase.Logic.OperationPipeline.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static CrawlerBase.Logic.PageDownloader;

namespace CrawlerBase.Logic.Dataflow
{
    public class DownloadableData : OperationData
    {
        public string ParentUrl { get; set; }
        public string Url { get; set; }
        public PageDownloaderMode PageDownloaderMode { get; set; }
    }
}
