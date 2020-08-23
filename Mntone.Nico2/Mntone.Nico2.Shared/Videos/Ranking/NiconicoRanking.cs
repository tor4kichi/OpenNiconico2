using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
#if WINDOWS_UWP
using Windows.Web.Http;

#else 
using System.Net.Http;
#endif

namespace Mntone.Nico2.Videos.Ranking
{
    public static class EnumExtensions
    {
        public static string GetDescription<E>(this E enumValue)
            where E : Enum
        {
            var gm = enumValue.GetType().GetMember(enumValue.ToString());
            var attributes = gm[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            var description = ((DescriptionAttribute)attributes.ElementAt(0)).Description;
            return description;
        }
    }

   
	public sealed class NiconicoRanking
	{
        public static class Constants
        {
            public const string NiconicoRankingGenreDomain = "https://www.nicovideo.jp/ranking/genre/";
            public const string NiconicoRankingHotTopicDomain = "https://www.nicovideo.jp/ranking/hot-topic";

            public const int MaxPage = 10;
            public const int MaxPageWithTag = 3;
            public const int MaxPageHotTopic = 3;
            public const int MaxPageHotTopicWithKey = 1;

            public const int ItemsCountPerPage = 100;

            public static readonly RankingTerm[] AllRankingTerms = new[]
            {
                RankingTerm.Hour,
                RankingTerm.Day,
                RankingTerm.Week,
                RankingTerm.Month,
                RankingTerm.Total
            };


            public static readonly RankingTerm[] HotTopicAccepteRankingTerms = new[]
            {
                RankingTerm.Hour,
                RankingTerm.Day
            };

            public static readonly RankingTerm[] GenreWithTagAccepteRankingTerms = new[]
            {
                RankingTerm.Hour,
                RankingTerm.Day
            };

        }

        public static bool IsHotTopicAcceptTerm(RankingTerm term)
        {
            return Constants.HotTopicAccepteRankingTerms.Any(x => x == term);
        }

        public static bool IsGenreWithTagAcceptTerm(RankingTerm term)
        {
            return Constants.GenreWithTagAccepteRankingTerms.Any(x => x == term);
        }



        static async Task<List<RankingGenrePickedTag>> Internal_GetPickedTagAsync(string url)
        {
            string html = null;
            using (HttpClient client = new HttpClient())
            {
                html = await client.GetStringAsync(url);
            }

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            var root = doc.DocumentNode;

            // ページ上の .RankingFilterTag となる要素を列挙する
            var tagAnchorNodes = 
                root.SelectNodes(@"//section[@class=""RepresentedTagsContainer""]/ul/li/a")
                ?? root.SelectNodes(@"//section[@class=""HotTopicsContainer""]/ul/li/a");

            return tagAnchorNodes
                .Select(x => new RankingGenrePickedTag()
                {
                    DisplayName = x.InnerText.Trim('\n', ' '),
                    Tag = Uri.UnescapeDataString(x.GetAttributeValue("href", string.Empty).Split('=', '&').ElementAtOrDefault(1)?.Trim('\n') ?? String.Empty)
                })
                .ToList();
        }

        /// <summary>
        /// 指定ジャンルの「人気のタグ」を取得します。 <br />
        /// RankingGenre.All を指定した場合のみ、常に空のListを返します。（RankingGenre.All は「人気のタグ」を指定できないため）
        /// </summary>
        /// <param name="genre">RankingGenre.All"以外"を指定します。</param>
        /// <remarks></remarks>
        /// <returns></returns>
        public static async Task<List<RankingGenrePickedTag>> GetGenrePickedTagAsync(RankingGenre genre)
        {
            if (genre == RankingGenre.All) { return new List<RankingGenrePickedTag>(); }

            if (genre != RankingGenre.HotTopic)
            {
                return await Internal_GetPickedTagAsync($"{Constants.NiconicoRankingGenreDomain}{genre.GetDescription()}");
            }
            else
            {
                return await Internal_GetPickedTagAsync($"{Constants.NiconicoRankingHotTopicDomain}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="genre"></param>
        /// <param name="tag"></param>
        /// <param name="term"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static async Task<RssVideoResponse> GetRankingRssAsync(RankingGenre genre, string tag = null, RankingTerm term = RankingTerm.Hour, int page = 1)
        {
            if (genre != RankingGenre.HotTopic)
            {
                if (string.IsNullOrEmpty(tag))
                {
                    return await Internal_GetRankingRssAsync(genre, term, page);
                }
                else
                {
                    return await Internal_GetRankingRssWithTagAsync(genre, tag, term, page);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(tag))
                {
                    return await Internal_GetHotTopicRankingRssAsync(term, page);
                }
                else
                {
                    return await Internal_GetHotTopicRankingRssWithKeyAsync(tag, term);
                }
            }
        }


        static async Task<RssVideoResponse> Internal_GetRankingRssAsync(RankingGenre genre, RankingTerm term, int page)
        {
            if (page == 0 || page > Constants.MaxPage)
            {
                throw new ArgumentOutOfRangeException($"out of range {nameof(page)}. inside btw from 1 to {Constants.MaxPage} in with tag.");
            }

            var dict = new Dictionary<string, string>()
            {
                { "rss", "2.0" },
                { "lang", "ja-jp" }
            };

            dict.Add(nameof(term), term.GetDescription());
            if (page != 1)
            {
                dict.Add(nameof(page), page.ToString());
            }

            try
            {
                return await VideoRssContentHelper.GetRssVideoResponseAsync(
                    $"{Constants.NiconicoRankingGenreDomain}{genre.GetDescription()}?{HttpQueryExtention.DictionaryToQuery(dict)}"
                    );
            }
            catch
            {
                return new RssVideoResponse() { IsOK = false, Items = new List<RssVideoData>() };
            }
        }


        static async Task<RssVideoResponse> Internal_GetRankingRssWithTagAsync(RankingGenre genre, string tag, RankingTerm term, int page)
        {
            if (!IsGenreWithTagAcceptTerm(term))
            {
                throw new ArgumentOutOfRangeException($"out of range {nameof(RankingTerm)}. accept with {string.Join(" or ", Constants.GenreWithTagAccepteRankingTerms)}.");
            }

            if (page == 0 || page > Constants.MaxPageWithTag)
            {
                throw new ArgumentOutOfRangeException($"out of range {nameof(page)}. inside btw from 1 to {Constants.MaxPageWithTag} in with tag.");
            }

            var dict = new Dictionary<string, string>()
            {
                { "rss", "2.0" },
                { "lang", "ja-jp" }
            };

            dict.Add(nameof(term), term.GetDescription());
            if (tag != null)
            {
                dict.Add(nameof(tag), Uri.EscapeDataString(tag));
            }
            if (page != 1)
            {
                dict.Add(nameof(page), page.ToString());
            }

            try
            {
                return await VideoRssContentHelper.GetRssVideoResponseAsync(
                    $"{Constants.NiconicoRankingGenreDomain}{genre.GetDescription()}?{HttpQueryExtention.DictionaryToQuery(dict)}"
                    );
            }
            catch
            {
                return new RssVideoResponse() { IsOK = false, Items = new List<RssVideoData>() };
            }
        }


        
        static async Task<RssVideoResponse> Internal_GetHotTopicRankingRssAsync(RankingTerm term, int page)
        {
            if (!IsHotTopicAcceptTerm(term))
            {
                throw new ArgumentOutOfRangeException($"out of range {nameof(RankingTerm)}. accept with {string.Join(" or ", Constants.HotTopicAccepteRankingTerms)}.");
            }

            if (page == 0 || page > Constants.MaxPageHotTopic)
            {
                throw new ArgumentOutOfRangeException($"out of range {nameof(page)}. inside btw from 1 to {Constants.MaxPageHotTopic} in with tag.");
            }

            var dict = new Dictionary<string, string>()
            {
                { "rss", "2.0" },
                { "lang", "ja-jp" }
            };

            dict.Add(nameof(term), term.GetDescription());
            if (page != 1)
            {
                dict.Add(nameof(page), page.ToString());
            }

            try
            {
                return await VideoRssContentHelper.GetRssVideoResponseAsync(
                    $"{Constants.NiconicoRankingHotTopicDomain}?{HttpQueryExtention.DictionaryToQuery(dict)}"
                    );
            }
            catch
            {
                return new RssVideoResponse() { IsOK = false, Items = new List<RssVideoData>() };
            }
        }

        static async Task<RssVideoResponse> Internal_GetHotTopicRankingRssWithKeyAsync(string key, RankingTerm term)
        {
            if (!IsHotTopicAcceptTerm(term))
            {
                throw new ArgumentOutOfRangeException($"out of range {nameof(RankingTerm)}. accept with {string.Join(" or ", Constants.HotTopicAccepteRankingTerms)}.");
            }

            var dict = new Dictionary<string, string>()
            {
                { "rss", "2.0" },
                { "lang", "ja-jp" }
            };

            dict.Add(nameof(key), Uri.EscapeDataString(key));
            dict.Add(nameof(term), term.GetDescription());

            try
            {
                return await VideoRssContentHelper.GetRssVideoResponseAsync(
                    $"{Constants.NiconicoRankingHotTopicDomain}?{HttpQueryExtention.DictionaryToQuery(dict)}"
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
            return data.WatchPageUrl.Segments.Last();
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
