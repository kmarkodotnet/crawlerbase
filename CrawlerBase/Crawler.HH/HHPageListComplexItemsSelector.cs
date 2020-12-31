using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;

namespace Crawler.HH
{
    public class HHPageListComplexItemsSelector : PageListComplexItemsSelector
    {
        public HHPageListComplexItemsSelector(ComplexItems items) : base(items)
        {
        }

        protected override bool FilterNode(XPathNavigator current)
        {
            return true;
        }
    }
}
