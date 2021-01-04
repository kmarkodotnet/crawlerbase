using CrawlerBase.Logic.OperationPipeline.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;

namespace CrawlerBase.Logic.OperationPipeline.BaseClasses
{
    public class PageListItemsSelector : PageListItemsBaseSelector//ISelector<List<string>>   
    {
        public PageListItemsSelector(string xPath) : base(xPath)
        {
        }

        //public IOperationBaseElement Parent { get; set; }

        //protected readonly string xPath;

        //public PageListItemsSelector(string xPath)
        //{
        //    this.xPath = xPath;
        //}

        //public List<string> Select(HtmlDocument from)
        //{
        //    var selected = new List<string>();
        //    var n = from.CreateNavigator();
        //    var nodes = n.Select(xPath);
        //    for (int i = 0; i < nodes.Count; i++)
        //    {
        //        var x = nodes.Current.Value;
        //        selected.Add(x);
        //        nodes.MoveNext();
        //    }
        //    return selected;
        //}
        protected override bool FilterNode(XPathNavigator current)
        {
            return true;
        }

        protected override bool FilterValue(string value)
        {
            return true;
        }
    }
}
