
using CrawlerBase.DataAccess;
using CrawlerBase.Logic.OperationPipeline.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrawlerBase.Logic.OperationPipeline.BaseClasses
{
    public abstract class ContentProcessor<T> : IContentElement<T>
    {
        private readonly ILogger logger;

        public IOperationBaseElement Parent { get; set; }
        public IOperationBaseElement NextOperation { get; set; }

        public ContentProcessor(ILogger logger)
        {
            this.NextOperation = NextOperation;
            if(this.NextOperation != null)
            {
                this.NextOperation.Parent = this;
            }

            this.logger = logger;
        }

        IRootOperationElement IOperationBaseElement.GetRoot()
        {
            return this.Parent.GetRoot();
        }

        public void Process(string sourceUrl, T data)
        {
            try
            {
                ProcessData(sourceUrl, data);
                FinishDownloadedContent(sourceUrl);
                logger.Debug(string.Format("Content processed: {0}", sourceUrl));
            }
            catch (Exception ex)
            {
                logger.Error(ex, "ContentProcessor: " + sourceUrl);
            }
        }

        private void FinishDownloadedContent(string sourceUrl)
        {
            using (var context = new CrawlerContext(Configuration.Instance.DbConnectionString))
            {
                context.FinishDownloadedContent(sourceUrl);
            }
        }

        protected abstract void ProcessData(string sourceUrl, T data);
    }
}
