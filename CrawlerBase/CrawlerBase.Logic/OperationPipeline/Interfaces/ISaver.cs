using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.OperationPipeline.Interfaces
{
    public interface ISaver
    {
        void Save(string name, string content);
    }
}
