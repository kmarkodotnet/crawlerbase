using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler.HH
{

    public class HHPageListItemsSelector : PageListItemsSelector
    {
        public HHPageListItemsSelector(string xPath) : base(xPath)
        {
        }

    }
}
