using CrawlerBase.DataAccess;
using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler.FI
{
    public class FIDatabaseSaver : DatabaseSaver
    {
        protected override RawDataTypeEnum GetRawDataType()
        {
            return RawDataTypeEnum.FI;
        }
    }
}
