using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.OperationPipeline.Interfaces
{
    /// <summary>
    /// Interface that defines objects' parent element
    /// </summary>
    public interface IPipeElement
    {
        /// <summary>
        /// Parent element
        /// </summary>
        IOperationBaseElement Parent { get; set; }
    }
}
