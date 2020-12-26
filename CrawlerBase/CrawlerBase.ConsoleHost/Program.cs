using Crawler.FeliratokDotInfo;
using CrawlerBase.Logic;
using CrawlerBase.Logic.Dataflow;
using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace CrawlertBase.ConsoleHost
{
    //TODO: ex handling
    //TODO: storing downloaded data url-s
    //TODO: filtering downloadable url-s
    //TODO: logging
    //TODO: DownloaderOperationEngine.Stop()
    //TODO: 'donothing' queue element type
    //TODO: removing unnessesary code
    class Program
    {
        static async Task Main(string[] args)
        {
            FeliratokInfoRootProcessor processedFeliratokInfoRootProcessor = InitProcessed();
            FeliratokInfoEnumRootProcessor computedFeliratokInfoRootProcessor = InitComputed();
            FeliratokInfoEnumRootProcessor computedConditionalFeliratokInfoRootProcessor = InitComputedConditional();

            var doe = new DownloaderOperationEngine(new DownloaderThreadPool(), new PageContentProcessorThreadPool());
            doe.Initialize(Downloader.CreateInstances(7), PageContentProcessor.CreateInstances(1));
            doe.SetupRootElement(computedConditionalFeliratokInfoRootProcessor);
            doe.Start();

            Console.ReadKey();
        }

        private static FeliratokInfoEnumRootProcessor InitComputedConditional()
        {
            return new FeliratokInfoEnumRootProcessor(
                "https://www.feliratok.info/",
                new PageEnumeratorSelector("/index.php?page={0}&tab=all&sorrend=&irany=&search=&nyelv=&sid=&sorozatnev=&complexsearch=&evad=&epizod1=&elotag=&minoseg=&rlsr=", 1, 10),
                    new FeliratokInfoListPagesProcessor(
                        new FeliratokInfoConditionalPageListItemsSelector("/html/body/table/tr/td/table/tr/td/a/@href"),
                        new FeliratokInfoSubtitleDataProcessor(new FileSaver(), new Url2FileNameFormatter())
                    )
                );
        }

        private static FeliratokInfoEnumRootProcessor InitComputed()
        {
            return new FeliratokInfoEnumRootProcessor(
                "https://www.feliratok.info/",
                new PageEnumeratorSelector("/index.php?page={0}&tab=all&sorrend=&irany=&search=&nyelv=&sid=&sorozatnev=&complexsearch=&evad=&epizod1=&elotag=&minoseg=&rlsr=", 3, 3),
                    new FeliratokInfoListPagesProcessor(
                        new PageListItemsSelector("/html/body/table/tr/td/table/tr/td/a/@href"),
                        new FeliratokInfoSubtitleDataProcessor(new FileSaver(), new Url2FileNameFormatter())
                    )
                );
        }

        private static FeliratokInfoRootProcessor InitProcessed()
        {
            return new FeliratokInfoRootProcessor(
                "https://www.feliratok.info/",
                new FeliratokInfoListPagesProcessor(
                    new PageListItemsSelector("/html/body/div/div/a/@href"),
                    new FeliratokInfoListPagesProcessor(
                        new PageListItemsSelector("/html/body/table/tr/td/table/tr/td/a/@href"),
                        new FeliratokInfoSubtitleDataProcessor(new FileSaver(), new Url2FileNameFormatter())
                    )
                ));
        }
    }

}
