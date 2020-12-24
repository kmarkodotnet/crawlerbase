using CrawlerBase.Logic.OperationPipeline.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.OperationPipeline.BaseClasses
{
    public abstract class RootProcessor<TSelect> : IRootOperationElement
    {
        private readonly TSelect baseUrl;

        public IOperationBaseElement Parent { get; set; }
        public string BaseUrl => ConvertToString(baseUrl);

        protected abstract string ConvertToString(TSelect baseUrl);

        public IOperationBaseElement NextOperation { get; set; }
        
        public RootProcessor(TSelect baseUrl, IOperationBaseElement opPipe)
        {
            this.baseUrl = baseUrl;
            this.NextOperation = opPipe;
            this.NextOperation.Parent = this;
        }
        
        IRootOperationElement IOperationBaseElement.GetRoot()
        {
            return this;
        }
    }
}
