using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ServiceModel.Syndication;

namespace Mntone.Nico2.Videos.Ranking
{
    public static class EnumExtensions
    {
        public static string GetDescription<E>(this E enumValue)
            where E : Enum
        {
            var gm = enumValue.GetType().GetMember(enumValue.ToString());
            var attributes = gm[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            var description = ((DescriptionAttribute)attributes[0]).Description;
            return description;
        }
    }

   
	public sealed class NiconicoRanking
	{
        public const string NiconicoRankingGenreDomain = "https://www.nicovideo.jp/ranking/genre/";

		public static async Task<RssVideoResponse> GetRankingRssAsync(RankingGenre genre, RankingTerm? term = null, string tag = null, int page = 1)
		{
            var dict = new Dictionary<string, string>();
            if (term != null)
            {
                dict.Add(nameof(term), term?.GetDescription());
            }
            if (tag != null)
            {
                dict.Add(nameof(tag), Uri.EscapeUriString(tag));
            }
            if (page != 1)
            {
                dict.Add(nameof(page), page.ToString());
            }

            dict.Add("rss", "2.0");
            dict.Add("lang", "ja-jp");

            try
            {
                return await VideoRssContentHelper.GetRssVideoResponseAsync(
                    $"{NiconicoRankingGenreDomain}{genre.GetDescription()}?{HttpQueryExtention.DictionaryToQuery(dict)}"
                    );
            }
            catch
            {
                return new RssVideoResponse() { IsOK = false, Items = new List<RssVideoData>() };
            }
		}
	}

    

    

    public static class RankingRssDataExtensions
    {
        public static string GetVideoId(this RssVideoData data)
        {
            return data.WatchPageUrl.OriginalString.Substring(@"https://www.nicovideo.jp/watch/".Length);
        }

        public static string GetRankTrimmingTitle(this RssVideoData data)
        {
            var index = data.RawTitle.IndexOf('：');
            return data.RawTitle.Substring(index + 1);
        }

        public static RankingVideoMoreData GetMoreData(this RssVideoData data)
        {
            /* data.Description 
             
             <p class="nico-thumbnail"><img alt="異世界かるてっと 第12話" src="https://nicovideo.cdn.nimg.jp/thumbnails/35292329/35292329.7495360" width="94" height="70" border="0"/></p>
             <p class="nico-description">動画一覧はこちら第11話 watch/1560410886「Nアニメ」無料動画や最新情報・生放送・マ</p>
             <p class="nico-info"><small><strong class="nico-info-length">11:50</strong>｜<strong class="nico-info-date">2019年06月26日 00：45：00</strong> 投稿<br/><strong>合計</strong>&nbsp;&#x20;再生：<strong class="nico-info-total-view">81,344</strong>&nbsp;&#x20;コメント：<strong class="nico-info-total-res">3,555</strong>&nbsp;&#x20;マイリスト：<strong class="nico-info-total-mylist">600</strong></small></p>
             
             */

            var lines = data.Description.Split(separator: new char[] { '\n' }, options: StringSplitOptions.RemoveEmptyEntries);

            var thumbnailNode = HtmlAgilityPack.HtmlNode.CreateNode(lines[0].TrimStart());
            //var descriptionNode = HtmlAgilityPack.HtmlNode.CreateNode(lines[1]);
            var infoNode = HtmlAgilityPack.HtmlNode.CreateNode(lines[2].TrimStart());
            var img = thumbnailNode.Element("img");

            var infoContainer = infoNode.Element("small");

            // ex) 11:50
            var lengthNode = infoContainer.GetElementByClassName("nico-info-length");

            // ex) 2019年06月26日 00：45：00
            //var dateNode = infoContainer.GetElementByClassName("nico-info-date");

            // ex) 81,344
            var watchCountNode = infoContainer.GetElementByClassName("nico-info-total-view");
            var commentCountNode = infoContainer.GetElementByClassName("nico-info-total-res");
            var mylistCountNode = infoContainer.GetElementByClassName("nico-info-total-mylist");

            var result = new RankingVideoMoreData()
            {
                Title = img.GetAttributeValue("alt", string.Empty),
                ThumbnailUrl = img.GetAttributeValue("src", string.Empty),
                Length = lengthNode.InnerText.ToTimeSpan(),
                WatchCount = int.Parse(new string(watchCountNode.InnerText.Where(c => Char.IsDigit(c)).ToArray())),
                CommentCount = int.Parse(new string(commentCountNode.InnerText.Where(c => Char.IsDigit(c)).ToArray())),
                MylistCount = int.Parse(new string(mylistCountNode.InnerText.Where(c => Char.IsDigit(c)).ToArray())),
            };

            return result;
        }
    }




    public class RankingVideoMoreData
    {
        public string Title { get; set; }
        public TimeSpan Length { get; set; }
        public string ThumbnailUrl { get; set; }
        public int WatchCount { get; set; }
        public int CommentCount { get; set; }
        public int MylistCount { get; set; }
    }
	
}
