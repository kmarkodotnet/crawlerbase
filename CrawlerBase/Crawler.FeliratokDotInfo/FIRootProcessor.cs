﻿using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using CrawlerBase.Logic.OperationPipeline.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler.FI
{
    public class FIRootProcessor : RootProcessor<string>
    {
        public FIRootProcessor(string baseUrl, IOperationBaseElement opPipe) 
            : base(baseUrl, opPipe)
        {
        }

        protected override string ConvertToString(string baseUrl)
        {
            return baseUrl;
        }
    }
}
