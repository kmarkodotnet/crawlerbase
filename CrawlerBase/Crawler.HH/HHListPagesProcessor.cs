using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using CrawlerBase.Logic.OperationPipeline.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crawler.HH
{
    public class HHListPagesProcessor : PageProcessor<List<string>>
    {
        public HHListPagesProcessor(ISelector<List<string>> Selector, IOperationBaseElement OpPipe, bool downloadUtf7 = false)
            :base(Selector, new HtmlParser(),  OpPipe)
        {
            base.DownloadUtf7 = downloadUtf7;
        }

        protected override List<string> PostProcessAfterSelected(List<string> selected)
        {
            return selected.Where(s => !string.IsNullOrEmpty(s))
                .Take(1)
                .ToList();
        }
    }
}
