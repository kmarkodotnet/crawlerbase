using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using CrawlerBase.Logic.OperationPipeline.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler.FeliratokDotInfo
{
    public class FeliratokInfoRootProcessor : RootProcessor<string>
    {
        public FeliratokInfoRootProcessor(string baseUrl, IOperationBaseElement opPipe) 
            : base(baseUrl, opPipe)
        {
        }

        protected override string ConvertToString(string baseUrl)
        {
            return baseUrl;
        }
    }
}
