using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Users.Series
{
    public static class SeriesClient
    {
        private static Task<string> GetUserSeriesHtmlAsync(NiconicoContext context, string userId)
        {
            return context.GetStringAsync(NiconicoUrls.MakeUserSeriesPageUrl(userId));
        }

        
        private static List<Series> ParseUserSeriesHtml(string html)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            var seriesRootNode = doc.DocumentNode;
            if (seriesRootNode == null) { throw new ArgumentException(); }

            var seriesListContainerItemNodes = seriesRootNode.SelectNodes("//*[@id='series-app']/div/div/div");

            
            return seriesListContainerItemNodes
                .Select(seriesContainer => 
                {
                    var series = new Series();
                    var bodyNode = seriesContainer.SelectSingleNode("//div[@class='SeriesMediaObject-body']");
                    var titleNode = bodyNode.Element("a");
                    series.Title = titleNode.InnerText;
                    var countNode = bodyNode.Element("span");
                    series.Count = int.Parse(new string(countNode.InnerText.Where(c => Char.IsDigit(c)).ToArray()));
                    var seriesUri = new Uri(titleNode.Attributes["href"].Value);
                    series.Id = new string(seriesUri.Segments.Last().TakeWhile(c => Char.IsDigit(c)).ToArray());

                    var thumbnailNode = seriesContainer.SelectSingleNode("//div[@class='Thumbnail-image']");
                    series.ThumbnailUrl = new Uri(new string(thumbnailNode.Attributes["style"].Value.SkipWhile(c => c != '"').TakeWhile(c => c != '"').ToArray()));

                    return series;
                })
                .ToList();
        }

        public static async Task<SeriesList> GetUserSeriesAsync(NiconicoContext context, string userId)
        {
            var html = await GetUserSeriesHtmlAsync(context, userId);
            return new SeriesList()
            {
                Series = ParseUserSeriesHtml(html),
                UserId = userId
            };
        }






        private static Task<string> GetUserSeriesDetailsHtmlAsync(NiconicoContext context, string seriesId)
        {
            return context.GetStringAsync(NiconicoUrls.MakeSeriesPageUrl(seriesId));
        }

        private static SeriesDetails ParseSeriesDetailsHtml(string html)
        {
            throw new NotImplementedException();
        }


        public static async Task<SeriesDetails> GetSeriesDetailsAsync(NiconicoContext context, string seriesId)
        {
            var html = await GetUserSeriesDetailsHtmlAsync(context, seriesId);
            return ParseSeriesDetailsHtml(html);
        }
    }
}
