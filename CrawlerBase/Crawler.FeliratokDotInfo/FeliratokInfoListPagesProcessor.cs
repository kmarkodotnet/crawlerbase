﻿using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using CrawlerBase.Logic.OperationPipeline.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crawler.FeliratokDotInfo
{
    public class FeliratokInfoListPagesProcessor : PageProcessor<List<string>>
    {
        public FeliratokInfoListPagesProcessor(ISelector<List<string>> Selector, IOperationBaseElement OpPipe)
            :base(Selector, new HtmlParser(),  OpPipe)
        {
        }

        protected override List<string> PostProcessAfterSelected(List<string> selected)
        {
            return selected.Where(s => !string.IsNullOrEmpty(s)).Select(s => string.Format("{0}{1}", this.Parent.GetRoot().BaseUrl, s))
                //.Take(1)
                .ToList();
        }
    }
}