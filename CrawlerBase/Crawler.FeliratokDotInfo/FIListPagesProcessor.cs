using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using CrawlerBase.Logic.OperationPipeline.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CrawlerBase.Logic.PageDownloader;

namespace Crawler.FI
{
    public class FIListPagesProcessor : PageProcessor<List<string>>
    {
        public FIListPagesProcessor(ISelector<List<string>> Selector, IOperationBaseElement OpPipe, PageDownloaderMode pageDownloaderMode = PageDownloaderMode.String)
            :base(Selector, new HtmlParser(),  OpPipe)
        {
            base.PageDownloaderMode = pageDownloaderMode;
        }

        protected override List<string> PostProcessAfterSelected(List<string> selected)
        {
            return selected.Where(s => !string.IsNullOrEmpty(s)).Select(s => string.Format("{0}{1}", this.Parent.GetRoot().BaseUrl, s))
                //.Take(1)
                .ToList();
        }
    }
}
