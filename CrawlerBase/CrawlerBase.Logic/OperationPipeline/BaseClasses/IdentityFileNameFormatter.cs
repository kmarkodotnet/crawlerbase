using CrawlerBase.Logic.OperationPipeline.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler.FI
{
    public class IdentityFileNameFormatter : IFileNameFormatter
    {
        public string GetFileName(string sourceUrl)
        {
            return sourceUrl;
        }
    }
}
