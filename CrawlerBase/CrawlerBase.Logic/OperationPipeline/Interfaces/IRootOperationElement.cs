using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.OperationPipeline.Interfaces
{
    /// <summary>
    /// Interface that represents the root element on download tree
    /// </summary>
    public interface IRootOperationElement : IOperationBaseElement
    {
        /// <summary>
        /// Base url of the downloadable content
        /// </summary>
        string BaseUrl { get; }
    }

    /// <summary>
    /// Interface that represents the root element on download tree
    /// </summary>
    public interface IRootEnumOperationElement : IRootOperationElement
    {
        ISelector<List<string>> Selector { get; set; }
    }
}
