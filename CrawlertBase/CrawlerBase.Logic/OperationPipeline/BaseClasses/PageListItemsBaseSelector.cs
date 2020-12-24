using CrawlerBase.Logic.OperationPipeline.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;

namespace CrawlerBase.Logic.OperationPipeline.BaseClasses
{
    public abstract class PageListItemsBaseSelector : ISelector<List<string>>   
    {
        public IOperationBaseElement Parent { get; set; }
        
        protected readonly string xPath;

        public PageListItemsBaseSelector(string xPath)
        {
            this.xPath = xPath;
        }

        public List<string> Select(HtmlDocument from)
        {
            var selected = new List<string>();
            var n = from.CreateNavigator();
            var nodes = n.Select(xPath);
            for (int i = 0; i < nodes.Count; i++)
            {
                if (FilterNode(nodes.Current))
                {
                    selected.Add(nodes.Current.Value);
                }
                
                nodes.MoveNext();
            }
            return selected;
        }

        protected abstract bool FilterNode(XPathNavigator current);
    }
}
