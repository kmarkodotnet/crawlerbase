using CrawlerBase.Logic.OperationPipeline.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.OperationPipeline.BaseClasses
{
    public class HtmlParser : IParser<HtmlDocument>
    {
        public IOperationBaseElement Parent { get; set; }

        public HtmlDocument Parse(string content)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(content);
            return doc;
        }
    }
}
