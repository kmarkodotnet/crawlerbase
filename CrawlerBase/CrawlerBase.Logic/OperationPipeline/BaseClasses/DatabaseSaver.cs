using CrawlerBase.DataAccess;
using CrawlerBase.Logic.OperationPipeline.Interfaces;
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
                }
            }
            catch (Exception ex)
            {
                var errorObject = new
                {
                    name,
                    ex
                };
                throw;
            }
        }

        protected abstract RawDataTypeEnum GetRawDataType();
    }
}
