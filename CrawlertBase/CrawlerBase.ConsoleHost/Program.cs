using Crawler.FeliratokDotInfo;
using CrawlerBase.Logic;
using CrawlerBase.Logic.Dataflow;
using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace CrawlertBase.ConsoleHost
{
    //TODO: ex handling
    //TODO: storing downlaoded data url-s
    //TODO: filtering downloadable url-s
    //TODO: threadpool
    //TODO: logging
    //TODO: DownloaderOperationEngine.Stop()
    //TODO: 'donothing' queue element type
    //TODO: removing unnessesary code
    class Program
    {
        static async Task Main(string[] args)
        {
            var doe = new DownloaderOperationEngine(new Downloader(), new PageContentProcessor());

            FeliratokInfoRootProcessor processedFeliratokInfoRootProcessor = InitProcessed();

            FeliratokInfoEnumRootProcessor computedFeliratokInfoRootProcessor = InitComputed();

            FeliratokInfoEnumRootProcessor computedConditionalFeliratokInfoRootProcessor = InitComputedConditional();

            doe.Initialize(computedConditionalFeliratokInfoRootProcessor);
            doe.Start();

            Console.ReadKey();
        }

        private static FeliratokInfoEnumRootProcessor InitComputedConditional()
        {
            return new FeliratokInfoEnumRootProcessor(
                "https://www.feliratok.info/",
                new PageEnumeratorSelector("/index.php?page={0}&tab=all&sorrend=&irany=&search=&nyelv=&sid=&sorozatnev=&complexsearch=&evad=&epizod1=&elotag=&minoseg=&rlsr=", 3, 3),
                    new FeliratokInfoListPagesProcessor(
                        new FeliratokInfoConditionalPageListItemsSelector("/html/body/table/tr/td/table/tr/td/a/@href"),
                        new FeliratokInfoSubtitleDataProcessor(new Url2FileNameFormatter())
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
                        new FeliratokInfoSubtitleDataProcessor(new Url2FileNameFormatter())
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
                        new FeliratokInfoSubtitleDataProcessor(new Url2FileNameFormatter())
                    )
                ));
        }
    }

}
