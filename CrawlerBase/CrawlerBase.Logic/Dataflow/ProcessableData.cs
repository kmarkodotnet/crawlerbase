using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.Dataflow
{
    public class ProcessableData : OperationData
    {
        public string SourceUrl { get; set; }
        public string Content { get; set; }
    }
}
