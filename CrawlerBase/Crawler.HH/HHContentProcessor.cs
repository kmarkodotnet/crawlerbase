using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using CrawlerBase.Logic.OperationPipeline.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Crawler.FI
{
    public class HHContentProcessor : ContentProcessor<string>
    {
        public HHContentProcessor(ISaver<string> saver, IFileNameFormatter fileNameFormatter)
        {
            this.saver = saver;
            this.fileNameFormatter = fileNameFormatter;
        }

        private readonly ISaver<string> saver;
        private readonly IFileNameFormatter fileNameFormatter;

        protected override void ProcessData(string sourceUrl, string data)
        {
            var fileName = fileNameFormatter.GetFileName(sourceUrl);
            saver.Save(fileName, data);
        }
    }

    
}
