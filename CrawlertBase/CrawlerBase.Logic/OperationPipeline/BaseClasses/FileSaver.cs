using CrawlerBase.Logic.OperationPipeline.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CrawlerBase.Logic.OperationPipeline.BaseClasses
{
    public class FileSaver : ISaver
    {
        public void Save(string name, string content)
        {
            try
            {
                var path = string.Format(".\\x\\{0}", name);
                File.WriteAllText(path, content);
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
    }
}
