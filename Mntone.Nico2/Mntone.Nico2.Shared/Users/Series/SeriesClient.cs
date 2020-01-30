using HtmlAgilityPack;
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

        
        private static List<Videos.Series.Series> ParseUserSeriesHtml(string html)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            var seriesRootNode = doc.DocumentNode;
            if (seriesRootNode == null) { throw new ArgumentException(); }

            var seriesContainerNode = seriesRootNode.SelectSingleNode(@"//div[@id=""series-app""]");
            var seriesListContainerItemNodes = seriesContainerNode.SelectNodes("div/div/div");



            return seriesListContainerItemNodes
                .Select(seriesContainer => 
                {
                    var series = new Videos.Series.Series();
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

        public static async Task<UserSeriesList> GetUserSeriesAsync(NiconicoContext context, string userId)
        {
            var html = await GetUserSeriesHtmlAsync(context, userId);
            return new UserSeriesList()
            {
                Series = ParseUserSeriesHtml(html),
                UserId = userId
            };
        }



    }
}
