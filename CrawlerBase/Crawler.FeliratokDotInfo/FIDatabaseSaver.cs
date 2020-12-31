using CrawlerBase.DataAccess;
using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler.FI
{
    public class FIDatabaseSaver : DatabaseSaver
    {
        private readonly ILogger logger;

        public FIDatabaseSaver(ILogger logger)
        {
            this.logger = logger;
        }
        protected override ILogger GetLogger()
        {
            return logger;
        }

        protected override RawDataTypeEnum GetRawDataType()
        {
            return RawDataTypeEnum.FI;
        }
    }
}
