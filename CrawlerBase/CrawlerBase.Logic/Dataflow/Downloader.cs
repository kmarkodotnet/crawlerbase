
using NLog;
using System;
using System.Collections.Generic;
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

            }
            catch (Exception ex) 
            {
                var errorObject = new {
                    data.Url,
                    ex
                };
                throw;
            }
            return items;
        }
    }
}
