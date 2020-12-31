using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CrawlerBase.Logic
{
    public sealed class Configuration
    {
        public Configuration()
        {
        }

        public string DbConnectionString { get; set; }

        private static Configuration instance = null;
        public static Configuration Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Configuration();
                }
                return instance;
            }
        }

        public void Init()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            DbConnectionString = configuration.GetSection("ConnectionStrings").GetSection("ConnectionString").Value;

            LogManager.Configuration = new NLogLoggingConfiguration(configuration.GetSection("NLog"));
        }
    }
}
