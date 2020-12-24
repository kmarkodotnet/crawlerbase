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
        protected override Task<List<DownloadableData>> ProcessData(ProcessableData data)
        {
            var items = new List<DownloadableData>();
            if (data.OperationElement is IOperationElement<List<string>> opElement)
            {
                var x = opElement.Process(data.Content);
                x.ForEach(y => items.Add(new DownloadableData {
                    Url = y,
                    OperationElement = opElement.NextOperation
                }));                
            }
            else if (data.OperationElement is IOperationElement<string> opElement1)
            {
                var x = opElement1.Process(data.Content);
                items.Add(new DownloadableData
                {
                    Url = x,
                    OperationElement = opElement1.NextOperation
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

            return Task.Run(() => items);
        }
    }
}