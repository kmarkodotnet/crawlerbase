﻿using CrawlerBase.Logic.OperationPipeline.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace CrawlerBase.Logic.Dataflow
{
    public class PageContentProcessor : WorkerBase<ProcessableData, DownloadableData>
    {
        public static List<WorkerBase<ProcessableData, DownloadableData>> CreateInstances(int count)
        {
            var l = new List<WorkerBase<ProcessableData, DownloadableData>>();
            for (int i = 0; i < count; i++)
            {
                l.Add(new PageContentProcessor());
            }
            return l;
        }

        protected override Task<List<DownloadableData>> ProcessData(ProcessableData data)
        {
            var items = new List<DownloadableData>();
            try
            {
                if (data.OperationElement is IOperationElement<List<string>> opElement)
                {
                    var x = opElement.Process(data.Content);
                    x.ForEach(y => items.Add(new DownloadableData
                    {
                        Url = y,
                        OperationElement = opElement.NextOperation,
                        DownloadUtf7 = opElement.DownloadUtf7
                    }));
                }
                else if (data.OperationElement is IOperationElement<string> opElement1)
                {
                    var x = opElement1.Process(data.Content);
                    items.Add(new DownloadableData
                    {
                        Url = x,
                        OperationElement = opElement1.NextOperation,
                        DownloadUtf7 = opElement1.DownloadUtf7
                    });
                }
                else if (data.OperationElement is IContentElement<string> ce)
                {
                    ce.Process(data.SourceUrl, data.Content);
                }
                else if (data.OperationElement is IRootEnumOperationElement root1)
                {
                    items.Add(new DownloadableData
                    {
                        Url = root1.BaseUrl,
                        OperationElement = root1.NextOperation
                    });
                }
                else if (data.OperationElement is IRootOperationElement root)
                {
                    items.Add(new DownloadableData
                    {
                        Url = root.BaseUrl,
                        OperationElement = root.NextOperation
                    });
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
    }
}
