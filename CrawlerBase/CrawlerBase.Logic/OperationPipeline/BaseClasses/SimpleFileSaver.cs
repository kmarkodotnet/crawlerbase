using CrawlerBase.Logic.OperationPipeline.Interfaces;
using NLog;
using System;
using System.IO;

namespace CrawlerBase.Logic.OperationPipeline.BaseClasses
{
    public class SimpleFileSaver : ISaver<string>
    {
        private readonly ILogger logger;

        public SimpleFileSaver(ILogger logger)
        {
            this.logger = logger;
        }
        public void Save(string name, string content)
        {
            try
            {
                var path = string.Format(".\\x\\{0}", name);
                File.WriteAllText(path, content);
                logger.Info("File saved: " + name);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "SimpleFileSaver");
                throw;
            }
        }
    }
}
