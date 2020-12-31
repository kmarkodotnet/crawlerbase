using CrawlerBase.Logic.OperationPipeline.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;

namespace CrawlerBase.Logic.OperationPipeline.BaseClasses
{
    public class ComplexItem
    {
        public string XPath { get; set; }
        public string Value { get; set; }
    }
    public class ComplexItems: List<ComplexItem> {
        public ComplexItems()
        {

        }
        public ComplexItems(List<ComplexItem> items)
        {
            this.AddRange(items);
        }
    }
    public abstract class PageListComplexItemsSelector : ISelector<ComplexItems>   
    {
        private readonly ComplexItems items;

        public IOperationBaseElement Parent { get; set; }
        
        public PageListComplexItemsSelector(ComplexItems items)
        {
            this.items = items;
        }

        public ComplexItems Select(HtmlDocument from)
        {
            var selected = new ComplexItems();
            foreach (var item in this.items)
            {
                var n = from.CreateNavigator();
                var nodes = n.Select(item.XPath);
                
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (FilterNode(nodes.Current.Clone()))
                    {
                        selected.Add(new ComplexItem {
                            XPath = item.XPath,
                            Value = nodes.Current.Value
                        });
                    }

                    nodes.MoveNext();
                }
            }            
            return selected;
        }

        protected abstract bool FilterNode(XPathNavigator current);
    }
}
