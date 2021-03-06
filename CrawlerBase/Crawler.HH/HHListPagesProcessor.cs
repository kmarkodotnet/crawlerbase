﻿using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using CrawlerBase.Logic.OperationPipeline.Interfaces;
using HtmlAgilityPack;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CrawlerBase.Logic.PageDownloader;

namespace Crawler.HH
{
    public class HHListPagesProcessor : PageProcessor<List<string>>
    {
        public HHListPagesProcessor(ISelector<List<string>> Selector, IOperationBaseElement OpPipe, ILogger logger, PageDownloaderMode pageDownloaderMode = PageDownloaderMode.String)
            :base(Selector, new HtmlParser(logger),  OpPipe)
        {
            base.PageDownloaderMode = pageDownloaderMode;
        }

        protected override List<string> PostProcessAfterSelected(List<string> selected)
        {
            return selected.Where(s => !string.IsNullOrEmpty(s))
                .Take(1)
                .ToList();
        }
    }
}
