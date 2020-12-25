using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using CrawlerBase.Logic.OperationPipeline.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Crawler.FeliratokDotInfo
{
    public class FeliratokInfoSubtitleDataProcessor : ContentProcessor<string>
    {
        public FeliratokInfoSubtitleDataProcessor(IFileNameFormatter fileNameFormatter)
        {
            this.fileNameFormatter = fileNameFormatter;
        }

        FileSaver saver = new FileSaver();
        private readonly IFileNameFormatter fileNameFormatter;

        protected override void ProcessData(string sourceUrl, string data)
        {
            var fileName = fileNameFormatter.GetFileName(sourceUrl);
            saver.Save(fileName, data);
        }

        private string GetFileName(string sourceUrl)
        {
            var fn = "fnev=";
            var startIndex = sourceUrl.IndexOf(fn) + fn.Length;
            var nameLength = sourceUrl.IndexOf("&", startIndex)- startIndex;
            var name = sourceUrl.Substring(startIndex, nameLength);
            return name;
        }
    }

    public class FileSaver
    {
        public void Save(string name, string content)
        {
            var path = string.Format(".\\x\\{0}", name);
            File.WriteAllText(path, content);
        }
    }
}
