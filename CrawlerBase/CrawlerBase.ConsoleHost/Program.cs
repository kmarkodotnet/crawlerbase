using Crawler.FI;
using Crawler.HH;
using CrawlerBase.Logic;
using CrawlerBase.Logic.Dataflow;
using CrawlerBase.Logic.OperationPipeline.BaseClasses;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Threading.Tasks;
using static CrawlerBase.Logic.PageDownloader;

namespace CrawlertBase.ConsoleHost
{
    //TODO: storing downloaded data url-s
    //TODO: filtering downloadable url-s
    //TODO: DownloaderOperationEngine.Stop()
    //TODO: 'donothing' queue element type
    //TODO: removing unnessesary code
    //TODO: finishing HH selector: tuple, generic list, etc


    //private static IConfiguration _iconfiguration = null;
    class Program
    {
        static async Task Main(string[] args)
        {
            Configuration.Instance.Init();
            Logger log = LogManager.GetCurrentClassLogger();
            Operations(log);

            Console.ReadKey();
        }

        private static void SingleSelect(Logger log)
        {
            var doe = new DownloaderOperationEngine(new DownloaderThreadPool(log), new PageContentProcessorThreadPool(log));
            doe.Initialize(Downloader.CreateInstances(1, log), PageContentProcessor.CreateInstances(1, log));
            doe.InsertQ1(new DownloadableData
            {
                Url = "https://www.hasznaltauto.hu/szemelyauto/porsche/911/porsche_911_2_7-16356936",
                PageDownloaderMode = PageDownloaderMode.Utf8,
                OperationElement = new HHComplexListPagesProcessor(
                            new HHPageListComplexItemsSelector(new ComplexItems
                            {
                                new ComplexItem{
                                    XPath = "/html/body/div/div/div/div/div/div/div/div/div/div/div/div/table/tr/td[@class='bal pontos']"
                                },

                                new ComplexItem{
                                    //XPath = "/html/body/div/div/div/div/div/div/div/div/div/div/div/div/table/tr/td/strong"
                                    XPath = "/html/body/div/div/div/div/div/div/div/div/div/div/div/div/table/tr/td[not(@class='bal pontos')]"
                                }
                            }),
                            new HHContentProcessor(new SimpleFileSaver(log), new HHUrl2FileNameFormatter(), log),
                            log,
                            PageDownloaderMode.Utf8
                        )
            });
            doe.Start();
        }

        private static void Operations(Logger log)
        {
            log.Debug("Operations starting...");
            FIRootProcessor processedFeliratokInfoRootProcessor = InitProcessed(log);
            FIEnumRootProcessor computedFeliratokInfoRootProcessor = InitComputed(log);
            FIEnumRootProcessor computedConditionalFeliratokInfoRootProcessor = InitComputedConditional(log);
            FIEnumRootProcessor utf7ComputedConditionalFeliratokInfoRootProcessor = InitComputedConditionalUtf7(log);
            HHEnumRootProcessor hhInitComputedConditionalUtf7 = HHInitComputedConditionalUtf8(log);
            HHEnumRootProcessor hHInitComputedConditionalComplexSelector = HHInitComputedConditionalComplexSelector(log);

            FIEnumRootProcessor dbSaveUtf7ComputedConditionalFeliratokInfoRootProcessor = InitComputedConditionalUtf7Save2Db(log);

            var doe = new DownloaderOperationEngine(new DownloaderThreadPool(log), new PageContentProcessorThreadPool(log));
            doe.Initialize(Downloader.CreateInstances(7, log), PageContentProcessor.CreateInstances(1, log));
            doe.SetupRootElement(dbSaveUtf7ComputedConditionalFeliratokInfoRootProcessor);
            doe.Start();
            log.Debug("Operations started");
        }

        private static HHEnumRootProcessor HHInitComputedConditionalComplexSelector(Logger log)
        {
            return new HHEnumRootProcessor(
                "https://www.hasznaltauto.hu/",
                new PageEnumeratorSelector("/talalatilista/PDNG2VG3V3NEADH4S76QFLXFOR4WY5KUUQFFLKWU27EBAE4WXUMO2JJ2QD4PO6QTLKZDSPDBZV4O3MJ5AHSHYJLPIFHGOUTIRDMJAV5QYVLKYGM3JII242GRLQQEL2VAQUKGBSWEFXRW6UXMVBKRCY7MQH6LNRMIAZ6GLFOLN4TVFCBRAMPUGZRJAUTF6FPPW2XYR7DM7OIHHBDQOVQGE5ICN5JVOR4KNH33XEVCAEHW5L6AVDYCEYMWA4CEH3QFY46XP2GRDQY5ECZ2O5LL4ZSFUGCZUNMI6VHE4CUG33QRJYIEB6WTZZMB4VZ65JVG46LB5WDA3IQ7Q32SD3CWGSBGQ3OH3TRKE2JU5R63LDD3PYAPZ627C5T6SDC572C3WXD5AEZT52YE6IMS7WUUF7HFZFJFJZBWOVHX72M67XMWYHYDO6G3RDYFV6QVF5PAMRPRTJI2RXU6BBYTUI5KO4CCGM5DVNV4IWEC73HQT4U6ZQBDNOKT2VIU4KJR3WEMGFVDXLSUKBCSTFUZ3DEOALWQSQO4AGBYCONRNZCS3ZKH7QPOYZFE563YS3RVSH6POM6O7Y2FRYLX3PAU5M5QO7EXEO5AM7PST7TCZOOS7BOIDLKCXJRDOWQYZWKAYBX4JTYUZPDDQP3EUOWL47KFXN767SWVVCBQXQPCOUYIK7GYNVSDTRH22AQ52TDRBHBSRIDHLTGGBAK6YDHISEK4AJFWVBJUHGG5U2G3EMCKRVUM6WKP2GQ7X4FSE6XZVXFFDVZLYJYM46I7RFQ4DXEVGU2U4H5XCDLUC4GNLAL4APDBNCSW7U7Q37YDHTIXXP3OG34HPWRHRNXCB3H6C74B7EIH/page{0}", 1, 1),
                    new HHListPagesProcessor(
                        new HHPageListItemsSelector("/html/body/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/h3/a/@href"),
                        new HHComplexListPagesProcessor(
                            new HHPageListComplexItemsSelector(new ComplexItems
                            {
                                new ComplexItem{
                                    XPath = "/html/body/div/div/div/div/div/div/div/div/div/div/div/div"
                                }
                            }),
                            new HHContentProcessor(new SimpleFileSaver(log), new HHUrl2FileNameFormatter(), log),
                            log,
                            PageDownloaderMode.Utf8
                        ),
                        log,
                        PageDownloaderMode.String
                    )
                );
        }

        private static HHEnumRootProcessor HHInitComputedConditionalUtf8(Logger log)
        {
            return new HHEnumRootProcessor(
                "https://www.hasznaltauto.hu/",
                new PageEnumeratorSelector("/talalatilista/PDNG2VG3V3NEADH4S76QFLXFOR4WY5KUUQFFLKWU27EBAE4WXUMO2JJ2QD4PO6QTLKZDSPDBZV4O3MJ5AHSHYJLPIFHGOUTIRDMJAV5QYVLKYGM3JII242GRLQQEL2VAQUKGBSWEFXRW6UXMVBKRCY7MQH6LNRMIAZ6GLFOLN4TVFCBRAMPUGZRJAUTF6FPPW2XYR7DM7OIHHBDQOVQGE5ICN5JVOR4KNH33XEVCAEHW5L6AVDYCEYMWA4CEH3QFY46XP2GRDQY5ECZ2O5LL4ZSFUGCZUNMI6VHE4CUG33QRJYIEB6WTZZMB4VZ65JVG46LB5WDA3IQ7Q32SD3CWGSBGQ3OH3TRKE2JU5R63LDD3PYAPZ627C5T6SDC572C3WXD5AEZT52YE6IMS7WUUF7HFZFJFJZBWOVHX72M67XMWYHYDO6G3RDYFV6QVF5PAMRPRTJI2RXU6BBYTUI5KO4CCGM5DVNV4IWEC73HQT4U6ZQBDNOKT2VIU4KJR3WEMGFVDXLSUKBCSTFUZ3DEOALWQSQO4AGBYCONRNZCS3ZKH7QPOYZFE563YS3RVSH6POM6O7Y2FRYLX3PAU5M5QO7EXEO5AM7PST7TCZOOS7BOIDLKCXJRDOWQYZWKAYBX4JTYUZPDDQP3EUOWL47KFXN767SWVVCBQXQPCOUYIK7GYNVSDTRH22AQ52TDRBHBSRIDHLTGGBAK6YDHISEK4AJFWVBJUHGG5U2G3EMCKRVUM6WKP2GQ7X4FSE6XZVXFFDVZLYJYM46I7RFQ4DXEVGU2U4H5XCDLUC4GNLAL4APDBNCSW7U7Q37YDHTIXXP3OG34HPWRHRNXCB3H6C74B7EIH/page{0}", 1, 1),
                    new HHListPagesProcessor(
                        new HHPageListItemsSelector("/html/body/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/div/h3/a/@href"),
                        new HHContentProcessor(new SimpleFileSaver(log), new HHUrl2FileNameFormatter(), log),
                        log,
                        PageDownloaderMode.Utf8
                    )
                );
        }

        private static FIEnumRootProcessor InitComputedConditionalUtf7Save2Db(Logger log)
        {
            return new FIEnumRootProcessor(
                "https://www.feliratok.info/",
                new PageEnumeratorSelector("/index.php?page={0}&tab=all&sorrend=&irany=&search=&nyelv=&sid=&sorozatnev=&complexsearch=&evad=&epizod1=&elotag=&minoseg=&rlsr=", 1, 2857),
                    new FIListPagesProcessor(
                        new FIConditionalPageListItemsSelector("/html/body/table/tr/td/table/tr/td/a/@href"),
                        new FISubtitleDataProcessor(new FIDatabaseSaver(log), new IdentityFileNameFormatter(), log),
                        log,
                        PageDownloaderMode.Utf7
                    )
                );
        }
        private static FIEnumRootProcessor InitComputedConditionalUtf7(Logger log)
        {
            return new FIEnumRootProcessor(
                "https://www.feliratok.info/",
                new PageEnumeratorSelector("/index.php?page={0}&tab=all&sorrend=&irany=&search=&nyelv=&sid=&sorozatnev=&complexsearch=&evad=&epizod1=&elotag=&minoseg=&rlsr=", 1, 10),
                    new FIListPagesProcessor(
                        new FIConditionalPageListItemsSelector("/html/body/table/tr/td/table/tr/td/a/@href"),
                        new FISubtitleDataProcessor(new SimpleFileSaver(log), new FIUrl2FileNameFormatter(), log),
                        log,
                        PageDownloaderMode.Utf7
                    )
                );
        }

        private static FIEnumRootProcessor InitComputedConditional(Logger log)
        {
            return new FIEnumRootProcessor(
                "https://www.feliratok.info/",
                new PageEnumeratorSelector("/index.php?page={0}&tab=all&sorrend=&irany=&search=&nyelv=&sid=&sorozatnev=&complexsearch=&evad=&epizod1=&elotag=&minoseg=&rlsr=", 1, 10),
                    new FIListPagesProcessor(
                        new FIConditionalPageListItemsSelector("/html/body/table/tr/td/table/tr/td/a/@href"),
                        new FISubtitleDataProcessor(new SimpleFileSaver(log), new FIUrl2FileNameFormatter(), log),
                        log
                    )
                );
        }

        private static FIEnumRootProcessor InitComputed(Logger log)
        {
            return new FIEnumRootProcessor(
                "https://www.feliratok.info/",
                new PageEnumeratorSelector("/index.php?page={0}&tab=all&sorrend=&irany=&search=&nyelv=&sid=&sorozatnev=&complexsearch=&evad=&epizod1=&elotag=&minoseg=&rlsr=", 3, 3),
                    new FIListPagesProcessor(
                        new PageListItemsSelector("/html/body/table/tr/td/table/tr/td/a/@href"),
                        new FISubtitleDataProcessor(new SimpleFileSaver(log), new FIUrl2FileNameFormatter(), log),
                        log
                    )
                );
        }

        private static FIRootProcessor InitProcessed(Logger log)
        {
            return new FIRootProcessor(
                "https://www.feliratok.info/",
                new FIListPagesProcessor(
                    new PageListItemsSelector("/html/body/div/div/a/@href"),
                    new FIListPagesProcessor(
                        new PageListItemsSelector("/html/body/table/tr/td/table/tr/td/a/@href"),
                        new FISubtitleDataProcessor(new SimpleFileSaver(log), new FIUrl2FileNameFormatter(), log),
                        log
                    ),
                    log
                ));
        }
    }

}
