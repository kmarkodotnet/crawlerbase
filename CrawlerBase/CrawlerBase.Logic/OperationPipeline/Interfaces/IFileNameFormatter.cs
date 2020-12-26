using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.OperationPipeline.Interfaces
{
    public interface IFileNameFormatter
    {
        string GetFileName(string sourceName);
    }
}
