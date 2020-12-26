using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.OperationPipeline.Interfaces
{
    /// <summary>
    /// Interface that defines basic operations of the parser
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IParser<T>: IPipeElement
    {
        /// <summary>
        /// Parse operation
        /// </summary>
        /// <returns></returns>
        T Parse(string content);
    }
}
