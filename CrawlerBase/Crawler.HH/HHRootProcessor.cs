using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using CrawlerBase.Logic.OperationPipeline.Interfaces;
using System;

namespace Crawler.HH
{
    public class HHRootProcessor : RootProcessor<string>
    {
        public HHRootProcessor(string baseUrl, IOperationBaseElement opPipe)
            : base(baseUrl, opPipe)
        {
        }

        protected override string ConvertToString(string baseUrl)
        {
            return baseUrl;
        }
    }
}
