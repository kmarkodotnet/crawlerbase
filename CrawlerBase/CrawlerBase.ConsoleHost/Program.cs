using Crawler.FI;
using Crawler.HH;
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
            HHEnumRootProcessor hhInitComputedConditionalUtf7 = HHInitComputedConditionalUtf7();


            var doe = new DownloaderOperationEngine(new DownloaderThreadPool(), new PageContentProcessorThreadPool());
            doe.Initialize(Downloader.CreateInstances(7), PageContentProcessor.CreateInstances(1));
            doe.SetupRootElement(hhInitComputedConditionalUtf7);
            doe.Start();

            Console.ReadKey();
        }

        private static HHEnumRootProcessor HHInitComputedConditionalUtf7()
        {
            return new HHEnumRootProcessor(
                "https://www.hasznaltauto.hu/",
                new PageEnumeratorSelector("/talalatilista/PDNG2VG3V3NEADH4S76QFLXFOR4WY5KUUQFFLKWU27EBAE4WXUMO2JJ2QD4PO6QTLKZDSPDBZV4O3MJ5AHSHYJLPIFHGOUTIRDMJAV5QYVLKYGM3JII242GRLQQEL2VAQUKGBSWEFXRW6UXMVBKRCY7MQH6LNRMIAZ6GLFOLN4TVFCBRAMPUGZRJAUTF6FPPW2XYR7DM7OIHHBDQOVQGE5ICN5JVOR4KNH33XEVCAEHW5L6AVDYCEYMWA4CEH3QFY46XP2GRDQY5ECZ2O5LL4ZSFUGCZUNMI6VHE4CUG33QRJYIEB6WTZZMB4VZ65JVG46LB5WDA3IQ7Q32SD3CWGSBGQ3OH3TRKE2JU5R63LDD3PYAPZ627C5T6SDC572C3WXD5AEZT52YE6IMS7WUUF7HFZFJFJZBWOVHX72M67XMWYHYDO6G3RDYFV6QVF5PAMRPRTJI2RXU6BBYTUI5KO4CCGM5DVNV4IWEC73HQT4U6ZQBDNOKT2VIU4KJR3WEMGFVDXLSUKBCSTFUZ3DEOALWQSQO4AGBYCONRNZCS3ZKH7QPOYZFE563YS3RVSH6POM6O7Y2FRYLX3PAU5M5QO7EXEO5AM7PST7TCZOOS7BOIDLKCXJRDOWQYZWKAYBX4JTYUZPDDQP3EUOWL47KFXN767SWVVCBQXQPCOUYIK7GYNVSDTRH22AQ52TDRBHBSRIDHLTGGBAK6YDHISEK4AJFWVBJUHGG5U2G3EMCKRVUM6WKP2GQ7X4FSE6XZVXFFDVZLYJYM46I7RFQ4DXEVGU2U4H5XCDLUC4GNLAL4APDBNCSW7U7Q37YDHTIXXP3OG34HPWRHRNXCB3H6C74B7EIH/page{0}", 1, 1),
                    new HHListPagesProcessor(
                        new HHPageListItemsSelector("/html/body/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/h3/a/@href"),
                        new HHContentProcessor(new FileSaver(), new HHUrl2FileNameFormatter()),
                        true
                    )
                );
        }

        private static FIEnumRootProcessor InitComputedConditionalUtf7()
        {
            return new FIEnumRootProcessor(
                "https://www.feliratok.info/",
                new PageEnumeratorSelector("/index.php?page={0}&tab=all&sorrend=&irany=&search=&nyelv=&sid=&sorozatnev=&complexsearch=&evad=&epizod1=&elotag=&minoseg=&rlsr=", 1, 10),
                    new FIListPagesProcessor(
                        new FIConditionalPageListItemsSelector("/html/body/table/tr/td/table/tr/td/a/@href"),
                        new FISubtitleDataProcessor(new FileSaver(), new FIUrl2FileNameFormatter()),
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
                        new FISubtitleDataProcessor(new FileSaver(), new FIUrl2FileNameFormatter())
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
                        new FISubtitleDataProcessor(new FileSaver(), new FIUrl2FileNameFormatter())
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
                        new FISubtitleDataProcessor(new FileSaver(), new FIUrl2FileNameFormatter())
                    )
                ));
        }
    }

}
