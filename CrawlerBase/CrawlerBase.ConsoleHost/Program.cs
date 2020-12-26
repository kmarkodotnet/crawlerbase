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
            FIRootProcessor processedFeliratokInfoRootProcessor = InitProcessed();
            FIEnumRootProcessor computedFeliratokInfoRootProcessor = InitComputed();
            FIEnumRootProcessor computedConditionalFeliratokInfoRootProcessor = InitComputedConditional();
            FIEnumRootProcessor utf7ComputedConditionalFeliratokInfoRootProcessor = InitComputedConditionalUtf7();
            

            var doe = new DownloaderOperationEngine(new DownloaderThreadPool(), new PageContentProcessorThreadPool());
            doe.Initialize(Downloader.CreateInstances(7), PageContentProcessor.CreateInstances(1));
            doe.SetupRootElement(utf7ComputedConditionalFeliratokInfoRootProcessor);
            doe.Start();

            Console.ReadKey();
        }

        private static FIEnumRootProcessor InitComputedConditionalUtf7()
        {
            return new FIEnumRootProcessor(
                "https://www.feliratok.info/",
                new PageEnumeratorSelector("/index.php?page={0}&tab=all&sorrend=&irany=&search=&nyelv=&sid=&sorozatnev=&complexsearch=&evad=&epizod1=&elotag=&minoseg=&rlsr=", 1, 10),
                    new FIListPagesProcessor(
                        new FIConditionalPageListItemsSelector("/html/body/table/tr/td/table/tr/td/a/@href"),
                        new FISubtitleDataProcessor(new FileSaver(), new Url2FileNameFormatter()),
                        true
                    )
                );
        }

        private static FIEnumRootProcessor InitComputedConditional()
        {
            return new FIEnumRootProcessor(
                "https://www.feliratok.info/",
                new PageEnumeratorSelector("/index.php?page={0}&tab=all&sorrend=&irany=&search=&nyelv=&sid=&sorozatnev=&complexsearch=&evad=&epizod1=&elotag=&minoseg=&rlsr=", 1, 10),
                    new FIListPagesProcessor(
                        new FIConditionalPageListItemsSelector("/html/body/table/tr/td/table/tr/td/a/@href"),
                        new FISubtitleDataProcessor(new FileSaver(), new Url2FileNameFormatter())
                    )
                );
        }

        private static FIEnumRootProcessor InitComputed()
        {
            return new FIEnumRootProcessor(
                "https://www.feliratok.info/",
                new PageEnumeratorSelector("/index.php?page={0}&tab=all&sorrend=&irany=&search=&nyelv=&sid=&sorozatnev=&complexsearch=&evad=&epizod1=&elotag=&minoseg=&rlsr=", 3, 3),
                    new FIListPagesProcessor(
                        new PageListItemsSelector("/html/body/table/tr/td/table/tr/td/a/@href"),
                        new FISubtitleDataProcessor(new FileSaver(), new Url2FileNameFormatter())
                    )
                );
        }

        private static FIRootProcessor InitProcessed()
        {
            return new FIRootProcessor(
                "https://www.feliratok.info/",
                new FIListPagesProcessor(
                    new PageListItemsSelector("/html/body/div/div/a/@href"),
                    new FIListPagesProcessor(
                        new PageListItemsSelector("/html/body/table/tr/td/table/tr/td/a/@href"),
                        new FISubtitleDataProcessor(new FileSaver(), new Url2FileNameFormatter())
                    )
                ));
        }
    }

}
