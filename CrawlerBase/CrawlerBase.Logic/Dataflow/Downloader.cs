
using CrawlerBase.DataAccess;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace CrawlerBase.Logic.Dataflow
{
    public class Downloader : WorkerBase<DownloadableData, ProcessableData>
    {
        private readonly ILogger logger;
        PageDownloader _pd = new PageDownloader();
        public Downloader(ILogger logger)
        {
            this.logger = logger;
        }

        public static List<WorkerBase<DownloadableData, ProcessableData>> CreateInstances(int count, ILogger logger)
        {
            var l = new List<WorkerBase<DownloadableData, ProcessableData>>();
            for (int i = 0; i < count; i++)
            {
                l.Add(new Downloader(logger));
            }
            return l;
        }

        protected override ILogger GetLogger()
        {
            return logger;
        }

        protected override async Task<List<ProcessableData>> ProcessData(DownloadableData data)
        {
            var items = new List<ProcessableData>();

            try
            {
                var result = await _pd.Download(data.Url, data.PageDownloaderMode);

                items.Add(new ProcessableData
                {
                    Content = result,
                    SourceUrl = data.Url,
                    OperationElement = data.OperationElement
                });

                SetDownloadedContent(data.Url);
            }
            catch (Exception ex) 
            {
                logger.Error(ex, data.Url);
            }
            return items;
        }

        private void SetDownloadedContent(string sourceUrl)
        {
            using (var context = new CrawlerContext(Configuration.Instance.DbConnectionString))
            {
                context.SetDownloadedContent(sourceUrl);
            }
        }

        
    }
}
