using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

#if WINDOWS_UWP
using Windows.Web.Syndication;
#else 
using System.ServiceModel.Syndication;
#endif

namespace Mntone.Nico2
{
    public static class VideoRssContentHelper
    {
        public static async Task<RssVideoResponse> GetRssVideoResponseAsync(string url)
        {
            Debug.WriteLine(url);


            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);

                request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("ja", 0.5));
                request.Headers.UserAgent.Add(new ProductInfoHeaderValue("NicoPlayerHohoema_UWP", "1.0"));

                var result = await client.SendAsync(request);
#if WINDOWS_UWP
                var xml = await result.Content.ReadAsStringAsync();
                var feed = new SyndicationFeed();
                feed.Load(xml);

                var items = new List<RssVideoData>();
                foreach (var item in feed.Items)
                {
                    items.Add(new RssVideoData()
                    {
                        RawTitle = item.Title.Text,
                        WatchPageUrl = item.Links.FirstOrDefault()?.Uri,
                        Description = item.Summary.Text,
                        PubDate = item.PublishedDate
                    });
                }

                return new RssVideoResponse()
                {
                    IsOK = true,
                    Items = items,
                    Language = feed.Language,
                    Link = feed.Links.FirstOrDefault()?.Uri
                };
#else
                var text = await result.Content.ReadAsStringAsync();

                using (var contentStream = await result.Content.ReadAsStreamAsync())
                {
                    using (var reader = System.Xml.XmlReader.Create(contentStream))
                    {

                        var feed = SyndicationFeed.Load(reader);

                        var items = new List<RssVideoData>();
                        foreach (var item in feed.Items)
                        {
                            items.Add(new RssVideoData()
                            {
                                RawTitle = item.Title.Text,
                                WatchPageUrl = item.Links.FirstOrDefault()?.Uri,
                                Description = item.Summary.Text,
                                PubDate = item.PublishDate
                            });
                        }

                        return new RssVideoResponse()
                        {
                            IsOK = true,
                            Items = items,
                            Language = feed.Language,
                            Link = feed.Links.FirstOrDefault()?.Uri
                        };
                    }
                }
#endif


            }
        }
    }



    public class RssVideoResponse
    {
        public bool IsOK { get; set; }

        public Uri Link { get; set; }

        public string Language { get; set; }

        public List<RssVideoData> Items { get; set; }
    }

    public class RssVideoData
    {
        public string RawTitle { get; set; }

        public Uri WatchPageUrl { get; set; }

        public DateTimeOffset PubDate { get; set; }

        public string Description { get; set; }

    }
}
