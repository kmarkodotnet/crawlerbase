using CrawlerBase.Logic.OperationPipeline.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler.HH
{
    public class HHUrl2FileNameFormatter : IFileNameFormatter
    {
        public string GetFileName(string sourceUrl)
        {
            var fn = "-";
            var startIndex = sourceUrl.LastIndexOf(fn) + fn.Length;
            var name = "hh." + sourceUrl.Substring(startIndex)+".txt";
            return name;
        }
    }
}
