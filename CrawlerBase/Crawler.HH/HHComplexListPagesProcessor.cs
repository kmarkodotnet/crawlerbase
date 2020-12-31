using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using CrawlerBase.Logic.OperationPipeline.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CrawlerBase.Logic.PageDownloader;

namespace Crawler.HH
{
    public class HHComplexListPagesProcessor : PageProcessor<ComplexItems>
    {
        public HHComplexListPagesProcessor(ISelector<ComplexItems> Selector, IOperationBaseElement OpPipe, PageDownloaderMode pageDownloaderMode = PageDownloaderMode.String)
            : base(Selector, new HtmlParser(), OpPipe)
        {
            base.PageDownloaderMode = pageDownloaderMode;
        }

        protected override ComplexItems PostProcessAfterSelected(ComplexItems selected)
        {
            return new ComplexItems(selected.Where(s => !string.IsNullOrEmpty(s.Value))
                .Take(1).ToList());
        }
    }
}
