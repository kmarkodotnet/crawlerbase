using CrawlerBase.Logic.OperationPipeline.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.OperationPipeline.BaseClasses
{
    public class PageEnumeratorSelector : ISelector<List<string>>
    {
        public IOperationBaseElement Parent { get; set; }

        private readonly string urlPart;
        private readonly int fromIndex;
        private readonly int toIndex;

        public PageEnumeratorSelector(string urlPart, int fromIndex, int toIndex)
        {
            this.urlPart = urlPart;
            this.fromIndex = fromIndex;
            this.toIndex = toIndex;
        }
        

        public List<string> Select(HtmlDocument from)
        {
            var baseUrl = this.Parent.GetRoot().BaseUrl;
            var items = new List<string>();
            for (int i = this.fromIndex; i <= this.toIndex; i++)
            {
                items.Add(string.Format(baseUrl + urlPart, i));
            }
            return items;
        }
    }
}
