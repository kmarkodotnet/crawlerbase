using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;

namespace Crawler.FI
{
    public class FIConditionalPageListItemsSelector : PageListItemsSelector
    {
        public FIConditionalPageListItemsSelector(string xPath) : base(xPath)
        {
        }

        protected override bool FilterNode(XPathNavigator current)
        {
            if (string.IsNullOrEmpty(current.Value))
                return false;

            try
            {
                current.MoveToParent();
                for (int i = 0; i < 8; i++)
                {
                    current.MoveToPrevious();
                }
                for (int i = 0; i < 2; i++)
                {
                    current.MoveToFirstChild();
                    current.MoveToNext();
                }
                return current.Value.ToLower() == "magyar";
            }
            catch (Exception ex)
            {

            }

            return base.FilterNode(current);
        }

        protected override bool FilterValue(string value)
        {
            return !value.ToLower().Contains(".zip");
        }
    }
}
