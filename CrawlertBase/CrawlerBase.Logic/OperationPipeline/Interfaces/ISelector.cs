using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.OperationPipeline.Interfaces
{
    /// <summary>
    /// Class to define selector data
    /// </summary>
    /// <typeparam name="TTo"></typeparam>
    public interface ISelector<TTo> : IPipeElement
    {
        /// <summary>
        /// Selects data from html
        /// </summary>
        /// <param name="from">Input html data</param>
        /// <returns></returns>
        TTo Select(HtmlDocument from);
    }
}
