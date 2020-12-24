﻿using CrawlerBase.Logic.OperationPipeline.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerBase.Logic.OperationPipeline.BaseClasses
{
    public class Url2FileNameFormatter : IFileNameFormatter
    {
        public string GetFileName(string sourceUrl)
        {
            var fn = "fnev=";
            var startIndex = sourceUrl.IndexOf(fn) + fn.Length;
            var nameLength = sourceUrl.IndexOf("&", startIndex) - startIndex;
            var name = sourceUrl.Substring(startIndex, nameLength);
            return name;
        }
    }
}
