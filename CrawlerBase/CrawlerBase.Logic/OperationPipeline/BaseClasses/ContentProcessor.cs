using CrawlerBase.Logic.OperationPipeline.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.OperationPipeline.BaseClasses
{
    public abstract class ContentProcessor<T> : IContentElement<T>
    {
        public IOperationBaseElement Parent { get; set; }
        public IOperationBaseElement NextOperation { get; set; }

        public ContentProcessor()
        {
            this.NextOperation = NextOperation;
            if(this.NextOperation != null)
            {
                this.NextOperation.Parent = this;
            }
        }

        IRootOperationElement IOperationBaseElement.GetRoot()
        {
            return this.Parent.GetRoot();
        }

        public void Process(string sourceUrl, T data)
        {
            ProcessData(sourceUrl, data);
        }

        protected abstract void ProcessData(string sourceUrl, T data);
    }
}
