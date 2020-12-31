using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using CrawlerBase.Logic.OperationPipeline.Interfaces;
using HtmlAgilityPack;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Crawler.FI
{
    public class FISubtitleDataProcessor : ContentProcessor<string>
    {
        public FISubtitleDataProcessor(ISaver<string> saver, IFileNameFormatter fileNameFormatter, ILogger logger)
            :base(logger)
        {
            this.saver = saver;
            this.fileNameFormatter = fileNameFormatter;
            Logger = logger;
        }

        private readonly ISaver<string> saver;
        private readonly IFileNameFormatter fileNameFormatter;

        public ILogger Logger { get; }

        protected override void ProcessData(string sourceUrl, string data)
        {
            var fileName = fileNameFormatter.GetFileName(sourceUrl);
            saver.Save(fileName, data);
        }
    }

    
}
