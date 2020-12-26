using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using CrawlerBase.Logic.OperationPipeline.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler.FeliratokDotInfo
{
    public class FIEnumRootProcessor : RootProcessor<string>, IRootEnumOperationElement
    {
        public FIEnumRootProcessor(string baseUrl, ISelector<List<string>> selector, IOperationBaseElement opPipe) 
            : base(baseUrl, opPipe)
        {
            this.Selector = selector;
            this.Selector.Parent = this;
        }

        public ISelector<List<string>> Selector { get; set; }

        protected override string ConvertToString(string baseUrl)
        {
            return baseUrl;
        }
    }
}
