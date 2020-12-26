using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.OperationPipeline.Interfaces
{
    /// <summary>
    /// Interface that defines special pipe element that can find it's root and the descendant
    /// </summary>
    public interface IOperationBaseElement: IPipeElement
    {
        IOperationBaseElement NextOperation { get; set; }
        IRootOperationElement GetRoot();
        
    }
}
