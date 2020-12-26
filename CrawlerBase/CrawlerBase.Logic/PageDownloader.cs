using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerBase.Logic
{
    public class PageDownloader
    {
        public async Task<string> Download(string url)
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(url))
                {
                    var readAsByte = false;
                    if (response.IsSuccessStatusCode)
                    {
                        if (readAsByte)
                        {
                            var r = await client.GetByteArrayAsync(url);
                            var responseString = Encoding.UTF7.GetString(r, 0, r.Length - 1);
                            return responseString;
                        }
                        else
                        {
                            var rr = await client.GetAsync(url);
                            var x = await rr.Content.ReadAsStringAsync();
                            return x;
                        }
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
