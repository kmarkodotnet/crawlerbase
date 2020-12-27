using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerBase.Logic
{
    public class PageDownloader
    {
        public enum PageDownloaderMode
        {
            Utf7,
            Utf8,
            String
        }

        private static readonly Encoding Utf8Encoder = Encoding.GetEncoding(
            "UTF-8",
            new EncoderReplacementFallback(string.Empty),
            new DecoderExceptionFallback()
        );


        public async Task<string> Download(string url, PageDownloaderMode mode)
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var resp = "";
                        switch (mode)
                        {
                            case PageDownloaderMode.String:
                                var rr = await client.GetAsync(url);
                                resp = await rr.Content.ReadAsStringAsync();
                                break;
                            case PageDownloaderMode.Utf7:
                                var r = await client.GetByteArrayAsync(url);
                                resp = Encoding.UTF7.GetString(r, 0, r.Length - 1);
                                break;
                            case PageDownloaderMode.Utf8:
                                var rr1 = await client.GetAsync(url);
                                var x1 = await rr1.Content.ReadAsStringAsync();
                                resp = Utf8Encoder.GetString(Utf8Encoder.GetBytes(x1));
                                break;
                        }
                        return resp;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }
    }
}
