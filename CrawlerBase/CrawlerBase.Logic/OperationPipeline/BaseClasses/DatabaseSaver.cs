using CrawlerBase.DataAccess;
using CrawlerBase.Logic.OperationPipeline.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CrawlerBase.Logic.OperationPipeline.BaseClasses
{
    public abstract class DatabaseSaver : ISaver<string>
    {
        public void Save(string name, string content)
        {
            try
            {
                using (var db = new CrawlerContext(
                    Configuration.Instance.DbConnectionString
                    ))
                {
                    db.RawData.Add(new RawData
                    {
                        RawDataType = GetRawDataType(),
                        Data = content,
                        DateDefined = DateTime.UtcNow,
                        SourceUrl = name
                    });
                    db.SaveChanges();
                    GetLogger().Info("Saved: " + name);
                }
            }
            catch (Exception ex)
            {
                GetLogger().Error(ex, name);
            }
        }

        protected abstract RawDataTypeEnum GetRawDataType();
        protected abstract ILogger GetLogger();
    }
}
