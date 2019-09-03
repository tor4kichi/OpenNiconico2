using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2
{
    public static class VideoRssContentHelper
    {
        public static async Task<RssVideoResponse> GetRssVideoResponseAsync(string url)
        {
            Debug.WriteLine(url);

            var feed = await CodeHollow.FeedReader.FeedReader.ReadAsync(url);

            var items = new List<RssVideoData>();
            foreach (var item in feed.Items)
            {
                items.Add(new RssVideoData()
                {
                    RawTitle = item.Title,
                    WatchPageUrl = new Uri(item.Link),
                    Description = item.Description,
                    PubDate = item.PublishingDate?.Date ?? DateTime.MinValue
                });
            }

            return new RssVideoResponse()
            {
                IsOK = true,
                Items = items,
                Language = feed.Language,
                Link = new Uri(feed.Link)
            };
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
