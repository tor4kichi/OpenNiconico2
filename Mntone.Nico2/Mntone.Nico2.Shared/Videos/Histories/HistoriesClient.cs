using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mntone.Nico2.Videos.Histories
{
	internal sealed class HistoriesClient
	{




		public static Task<string> GetHistoriesDataAsync( NiconicoContext context )
		{
			return context.PostAsync(NiconicoUrls.VideoHistoryUrl);
		}

		public static HistoriesResponse ParseHistoriesData( string historiesData )
		{
			return JsonSerializerExtensions.Load<HistoriesResponse>( historiesData );
		}

		public static Task<HistoriesResponse> GetHistoriesAsync( NiconicoContext context )
		{
			return GetHistoriesDataAsync( context )
				.ContinueWith( prevTask => ParseHistoriesData( prevTask.Result ) );
		}




        public static Task<string> GetHistoriesFromMyPageDataAsync(NiconicoContext context)
        {
            return context.GetStringAsync(NiconicoUrls.VideoHistoryMyPageUrl);
        }

        public static HistoriesResponse ParseMypageHistoriesData(string html)
        {

            var document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);

            var tokenAheadTrapText = "new VideoViewHistory.Action('";
            string token = null;
            foreach (var scriptNode in document.DocumentNode.Descendants("script"))
            {
                if (scriptNode.InnerText.Contains(tokenAheadTrapText))
                {
                    var index = scriptNode.InnerText.IndexOf(tokenAheadTrapText);
                    token = new string(scriptNode.InnerText.Skip(index + tokenAheadTrapText.Length).TakeWhile(c => c != '\'').ToArray());

                    System.Diagnostics.Debug.WriteLine("mypage history token: " + token);

                    break;
                }
            }

            if (token == null) return null;


            var hisotryListNode = document.DocumentNode.SelectSingleNode("//*[@id=\"historyList\"]");

            if (hisotryListNode == null) return null;

            Func<string, int> ExtractNumbersFunc = (text) => 
            {
                return int.Parse(new string(text.Where(x => x >= '0' && x <= '9').ToArray()));
            };

            var histories = new List<History>();
            foreach (var node in hisotryListNode.Elements("div"))
            {
                try
                {
                    var videoId = node.Id.Split('_').Last();
                    var imageNode = node.Descendants("img").First();
                    var thumbnailUrl = imageNode.GetAttributeValue("data-original", string.Empty);
                    if (thumbnailUrl == string.Empty)
                    {
                        thumbnailUrl = imageNode.GetAttributeValue("src", string.Empty);
                    }

                    var title = imageNode.GetAttributeValue("alt", string.Empty);

                    var thumbContainer = node.GetElementByClassName("VideoThumbnailContainer");
                    var videoTime = thumbContainer.GetElementByClassName("videoTime")?.InnerText.ToTimeSpan() ?? TimeSpan.Zero;
                    
                    var sectionNode = node.GetElementByClassName("VideoItem-videoDetail");
                    var watchTimeNode = sectionNode.GetElementByClassName("posttime");
                    var watchTimeText = watchTimeNode.InnerText.Split(' ', '年', '月', '日', ':').Select(x => int.TryParse(x, out var s) ? s : 0).ToArray();
                    var watchTime = new DateTime(watchTimeText[0], watchTimeText[1], watchTimeText[2], watchTimeText[4], watchTimeText[5], 0);

                    var watchCountNode = watchTimeNode.Element("span");
                    var watchCount = ExtractNumbersFunc(watchCountNode.InnerText);

                    var metaDataNode = sectionNode.Element("ul");
                    var viewCount = ExtractNumbersFunc(metaDataNode.GetElementByClassName("play").InnerText);
                    var commentCount = ExtractNumbersFunc(metaDataNode.GetElementByClassName("comment").InnerText);
                    var mylistCount = ExtractNumbersFunc(metaDataNode.GetElementByClassName("mylist").InnerText);
                    var postTimeNumbers = metaDataNode.GetElementByClassName("posttime").InnerText.Split(' ', '年', '月', '日', ':').Select(x => int.TryParse(x, out var s) ? s : 0).ToArray();
                    var postTime = new DateTime(postTimeNumbers[0] + 2000, postTimeNumbers[1], postTimeNumbers[2], postTimeNumbers[4], postTimeNumbers[5], 0);


                    var history = new History()
                    {
                        Id = videoId,
                        ItemId = videoId,
                        Title = title,
                        ThumbnailUrl = thumbnailUrl.ToUri(),
                        WatchCount = (uint)watchCount,
                        _Length = videoTime,
                        _WatchedAt = new DateTimeOffset(watchTime)
                    };
                    histories.Add(history);
                }
                catch { }
            }

            var res = new HistoriesResponse(token, histories);
            
            return res;
        }

        public static Task<HistoriesResponse> GetHistoriesFromMyPageAsync(NiconicoContext context)
        {
            return GetHistoriesFromMyPageDataAsync(context)
                .ContinueWith(prevTask => ParseMypageHistoriesData(prevTask.Result));
        }
    }
}