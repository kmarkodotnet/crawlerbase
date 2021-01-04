using CrawlerBase.DataAccess;
using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using CrawlerBase.Logic.OperationPipeline.Interfaces;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrawlerBase.Logic.Dataflow
{
    public class PageContentProcessor : WorkerBase<ProcessableData, DownloadableData>
    {
        private readonly ILogger logger;

        public PageContentProcessor(ILogger logger)
        {
            this.logger = logger;
        }
        public static List<WorkerBase<ProcessableData, DownloadableData>> CreateInstances(int count, ILogger logger)
        {
            var l = new List<WorkerBase<ProcessableData, DownloadableData>>();
            for (int i = 0; i < count; i++)
            {
                l.Add(new PageContentProcessor(logger));
            }
            return l;
        }

        protected override ILogger GetLogger()
        {
            return logger;
        }

        protected override Task<List<DownloadableData>> ProcessData(ProcessableData data)
        {
            var items = new List<DownloadableData>();
            try
            {
                if (data.OperationElement is IOperationElement<ComplexItems> complexOperationElement)
                {
                    var complexData = complexOperationElement.Process(data.Content);
                }
                else if (data.OperationElement is IOperationElement<List<string>> stringListOperationElement)
                {
                    var processedDataList = stringListOperationElement.Process(data.Content);
                    processedDataList.ForEach(processedData =>
                    {
                        items.Add(new DownloadableData
                        {
                            ParentUrl = data.SourceUrl,
                            Url = processedData,
                            OperationElement = stringListOperationElement.NextOperation,
                            PageDownloaderMode = stringListOperationElement.PageDownloaderMode
                        });
                        RegisterDownloadContent(data.SourceUrl, processedData);
                    }
                  );
                }
                else if (data.OperationElement is IOperationElement<string> stringOperationElement)
                {
                    var processedData = stringOperationElement.Process(data.Content);
                    items.Add(new DownloadableData
                    {
                        ParentUrl = data.SourceUrl,
                        Url = processedData,
                        OperationElement = stringOperationElement.NextOperation,
                        PageDownloaderMode = stringOperationElement.PageDownloaderMode
                    });
                    RegisterDownloadContent(data.SourceUrl, processedData);
                }
                else if (data.OperationElement is IContentElement<string> contentElement)
                {
                    contentElement.Process(data.SourceUrl, data.Content);
                }
                else if (data.OperationElement is IRootEnumOperationElement rootEnumOperationElement)
                {
                    items.Add(new DownloadableData
                    {
                        ParentUrl = data.SourceUrl,
                        Url = rootEnumOperationElement.BaseUrl,
                        OperationElement = rootEnumOperationElement.NextOperation
                    });
                    RegisterDownloadContent(string.Empty, data.SourceUrl);
                }
                else if (data.OperationElement is IRootOperationElement rootOperationElement)
                {
                    items.Add(new DownloadableData
                    {
                        ParentUrl = data.SourceUrl,
                        Url = rootOperationElement.BaseUrl,
                        OperationElement = rootOperationElement.NextOperation
                    });
                    RegisterDownloadContent(data.SourceUrl, rootOperationElement.BaseUrl);
                }
            }
            catch (Exception ex)
            {
                var errorObject = new
                {
                    data.Content,
                    ex
                };
                throw;
            }
            return Task.Run(() => items);
        }

        private void RegisterDownloadContent(string parentUrl, string url)
        {
            using (var context = new CrawlerContext(Configuration.Instance.DbConnectionString))
            {
                context.RegisterDownloadContent(parentUrl, url);
            }
        }
    }
}
