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

        protected override async Task<List<ProcessableData>> ProcessData(DownloadableData data)
        {
            var result = await _pd.Download(data.Url);

            var items = new List<ProcessableData>();
            items.Add(new ProcessableData
            {
                Content = result,
                SourceUrl = data.Url,
                OperationElement = data.OperationElement
            });

            return items;
        }
    }
}
