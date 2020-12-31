using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.OperationPipeline.Interfaces
{
    public interface ISaver<T>
    {
        void Save(string name, T content);
    }
}
