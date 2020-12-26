using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.OperationPipeline.Interfaces
{
    /// <summary>
    /// Interface for content processing
    /// </summary>
    /// <typeparam name="TProcess"></typeparam>
    public interface IContentElement<TProcess> : IOperationBaseElement
    {
        /// <summary>
        /// Process operation
        /// </summary>
        /// <param name="data"></param>
        void Process(string sourceUrl, TProcess data);
    }
}
