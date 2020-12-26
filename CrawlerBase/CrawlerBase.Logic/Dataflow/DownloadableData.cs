using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.Dataflow
{
    public class DownloadableData : OperationData
    {
        public string Url { get; set; }
        public bool DownloadUtf7 { get; set; }
    }
}
