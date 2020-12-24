using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.OperationPipeline.Interfaces
{
    /// <summary>
    /// Interface that defines the processing operation
    /// </summary>
    /// <typeparam name="TSelect"></typeparam>
    public interface IOperationElement<TSelect> : IOperationBaseElement
    {
        /// <summary>
        /// The operation that makes processing
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        TSelect Process(string content);
    }
}


