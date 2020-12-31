using CrawlerBase.Logic.OperationPipeline.Interfaces;
using HtmlAgilityPack;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.OperationPipeline.BaseClasses
{
    public class HtmlParser : IParser<HtmlDocument>
    {
        private readonly ILogger logger;

        public HtmlParser(ILogger logger)
        {
            this.logger = logger;
        }
        public IOperationBaseElement Parent { get; set; }

        public HtmlDocument Parse(string content)
        {
            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(content);
                return doc;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "HtmlParser");
                throw;
            }
        }
    }
}
