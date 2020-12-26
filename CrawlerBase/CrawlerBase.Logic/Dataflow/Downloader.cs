
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace CrawlerBase.Logic.Dataflow
{
    public class Downloader : WorkerBase<DownloadableData, ProcessableData>
    {
        PageDownloader _pd = new PageDownloader();

        public static List<WorkerBase<DownloadableData, ProcessableData>> CreateInstances(int count)
        {
            var l = new List<WorkerBase<DownloadableData, ProcessableData>>();
            for (int i = 0; i < count; i++)
            {
                l.Add(new Downloader());
            }
            return l;
        }

        protected override async Task<List<ProcessableData>> ProcessData(DownloadableData data)
        {
            var items = new List<ProcessableData>();

            try
            {
                var result = await _pd.Download(data.Url);
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
