using HtmlAgilityPack;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Mntone.Nico2.Communities.Detail
{
    public sealed class DetailClient
    {
		public static Task<string> GetCommunitySammaryPageHtmlAsync(NiconicoContext context, string communityId)
		{
			return context.GetClient()
				.GetConvertedStringAsync(NiconicoUrls.CommynitySammaryPageUrl + communityId);
		}

		private static CommunityDetailResponse ParseCommunitySammaryPageHtml(string html)
		{
			CommunityDetailResponse res = new CommunityDetailResponse();

			var communitySammary = new CommunitySammary();
			var communityDetail = new CommunityDetail();

			var doc = new HtmlAgilityPack.HtmlDocument();

			doc.LoadHtml(html);



			// コンテンツの読み取り

			var body = doc.DocumentNode.Element("body");
			var main = body.Element("main");
			// 放送中
			var nowLiveElem = body.SelectNodes("main//a");
			foreach (var liveItem in nowLiveElem)
			{
				var comLiveInfo = new CommunityLiveInfo();


				comLiveInfo.LiveTitle = liveItem.GetElementByClassName("now_live_info")
					.Element("h2")
					.InnerText;

				var liveUrl = liveItem.Attributes["href"].Value;

				var withoutQuery = new string(liveUrl.TakeWhile(x => x != '?').ToArray());
				var liveId = withoutQuery.Split('/').LastOrDefault();

				if (liveId == null) { throw new Exception(); }
				comLiveInfo.LiveId = liveId;

				communityDetail.CurrentLiveList.Add(comLiveInfo);
			}









			var header = body.Element("header");
			var communityData = header.SelectSingleNode("[@class='communityData']");
			var communityDetailElem = communityData.GetElementByClassName("communityDetail");

			// 開設日
			try
			{
				
				var createDateElem = communityDetailElem.SelectSingleNode("tr[2]/[@class='content']");
				var dateText = createDateElem.InnerText;
				var createDate = ParseDateTimeText(dateText);

				communityDetail.DateTime = createDate.ToString();

				var ownerNameAnchor = communityDetailElem.SelectSingleNode("tr[1]/[@class='content']/a");
				var id = ownerNameAnchor.Attributes["href"].Value.Split('/').LastOrDefault();
				communityDetail.OwnerUserId = id;

				var name = ownerNameAnchor.Element("strong").InnerText;
				communityDetail.OwnerUserName = name;



			}
			catch { throw; }

			try
			{
				var tagElements = communityDetailElem.SelectNodes("tr[3]/[@class='content']/li/a");
				communityDetail.Tags = tagElements.Select(x => x.InnerText).ToList();
			}
			catch { }



			var communityRegist = header.GetElementById("communityRegist");

			// プロフィール(数字やタグなど)
			{
				

				var levelElem = communityRegist.SelectSingleNode("div/dl/[@class='content'][1]");
				var levelText = levelElem.InnerText;
				var level = uint.Parse(levelText);
				communityDetail.Level = level;


				var memberContainer = communityRegist.SelectSingleNode("div/dl/[@class='content'][2]");
				var memberElem = memberContainer;
				var memberText = memberElem.InnerText;
				var memberCount = uint.Parse(memberText);
				communityDetail.MemberCount = memberCount;

				// TODO: 
				//				var memberMaxElem = memberContainer.

				/*
				var optionContainer = container.ElementAt(2);
				var optionRoot = optionContainer.Elements("td").ElementAt(1).Element("div");
				
				foreach (var optElem in optionRoot.Elements("span"))
				{
					var classValue = optElem.Attributes["class"].Value.Split(' ');

					var optType = classValue[0];
					var optEnable = classValue[1] == "on";
					switch (optType)
					{
						case "auto_accept":
							communityDetail.Option.IsJoinAutoAccept = optEnable;
							break;
						case "userinfo_required":
							communityDetail.Option.IsJoinWithoutPrivacyInfo = optEnable;
							break;
						case "privvideo_post":
							communityDetail.Option.IsCanSubmitVideoOnlyPrivilege = optEnable;
							break;
						case "privuser_auth":
							communityDetail.Option.IsCanAcceptJoinOnlyPrivilege = optEnable;
							break;
						case "privlive_broadcast":
							communityDetail.Option.IsCanLiveOnlyPrivilege = optEnable;
							break;
						default:
							throw new NotSupportedException();
					}
				}

				// TODO: 特権を獲得していない場合への対応

				var privilegeContainer = container.ElementAt(5);
				var privilegeDescElem = privilegeContainer.Elements("td").ElementAt(1).Element("div").Element("p");
				var privilegeDescText = privilegeDescElem.InnerText;
				communityDetail.PrivilegeDescription = privilegeDescText;
				*/


			}


			// Htmlによるコミュニティの説明
			{
				var descContentElem = main.GetElementById("profile_text_content");
				communityDetail.ProfielHtml = descContentElem.InnerHtml;
			}

			// News
			var newsContainer = main.SelectSingleNode("ul[@class='noticeList']");
			//			if (newsContainer.FirstChild.Name == "subbox")
			try
			{
				foreach (var newsItem in newsContainer.GetElementsByClassName("item"))
				{
					var news = new CommunityNews();

					var newsHeaderElem = newsItem.SelectSingleNode("[@class='noticeItemHeader']");

					var titleElem = newsHeaderElem.GetElementByClassName("noticeTitle");
					news.Title = titleElem.InnerText;

					var postDateElem = newsHeaderElem.SelectSingleNode("[@class='date']");
					news.PostDate = ParseDateTimeText(postDateElem.InnerText);

					// 「（」から「）」までの文字列を取得する
					// 文字列自体に「（」「）」が含まれていても問題ないように取得している
					var postAuthorElem = newsHeaderElem.SelectSingleNode("[@class='author']");
					news.PostAuthor = postAuthorElem.InnerText;


					var newsContentElem = newsItem.Element("p");
					news.ContentHtml = newsContentElem.InnerHtml;


					communityDetail.NewsList.Add(news);
				}
				
			}
			catch { }

			communitySammary.CommunityDetail = communityDetail;



			// 最近行われた生放送

			var sideContent = main.SelectSingleNode("[@class='area-sideContent']");
			{


				Func<HtmlNode, LiveInfo> nodeToLiveInfo = (HtmlNode node) =>
				{
					var liveInfo = new LiveInfo();

					var dateElem = node.GetElementByClassName("liveDate");
					
					var date = ParseDateTimeText(dateElem.InnerText);
					liveInfo.StartTime = date;

					var titleAnchor = node.Element("a");
					liveInfo.Title = titleAnchor.InnerText;

					var livePageUrl = titleAnchor.Attributes["href"].Value;
					var pos = livePageUrl.IndexOf("lv");
					var endPos = livePageUrl.LastIndexOf('?');
					var id = livePageUrl.Substring(pos, endPos - pos);
					liveInfo.LiveId = id;

					var broadcasterNameElem = node.GetElementByClassName("liveBroadcaster");
					var streamerName = broadcasterNameElem.InnerText.Remove(0, 4); // remove "放送者："
					liveInfo.StreamerName = streamerName;
					return liveInfo;
				};


				var recentLiveElem = sideContent.SelectSingleNode("section[1]/ul[1]");
				var preservedLiveElem = sideContent.SelectSingleNode("section[1]/ul[2]");

				var recentLiveItems = recentLiveElem.ChildNodes.Where(x => !x.HasAttributes);
				foreach (var recentLiveItem in recentLiveItems)
				{
					var liveInfo = nodeToLiveInfo(recentLiveItem);

					communityDetail.RecentLiveList.Add(liveInfo);
				}



				var preservedLiveItems = preservedLiveElem.ChildNodes.Where(x => !x.HasAttributes);
				foreach (var preservedLiveItem in preservedLiveItems)
				{
					var liveInfo = nodeToLiveInfo(preservedLiveItem);

					communityDetail.FutureLiveList.Add(liveInfo);
				}
			}

			// TODO: コミュニティメンバーのパース

			// コミュニティ動画
			var communityVideoContainer = sideContent.SelectSingleNode("section[2]");

			{
				var videoItems = communityVideoContainer.SelectNodes("ul/li");
				foreach (var videoItem in videoItems)
				{
					var video = new CommunityVideo();

					var videoAnchorElem = videoItem.FirstChild;

					var videoUrl = videoAnchorElem.Attributes["href"].Value;
					var videoIdTemp = videoUrl.SkipWhile(x => !(x >= '0' && x <= '9'))
						.TakeWhile(x => x >= '0' && x <= '9')
						.ToArray();

					var videoId = new String(videoIdTemp);
					video.VideoId = videoId;

					var imgElem = videoAnchorElem.Element("img");
					video.ThumbnailUrl = imgElem.Attributes["src"].Value;

					video.Title = imgElem.Attributes["title"].Value;

					communityDetail.VideoList.Add(video);
				}


			}


			res.CommunitySammary = communitySammary;
			res.IsStatusOK = true;
			return res;
		}

		private static DateTime ParseDateTimeText(string str)
		{
			var treatmentText = str.Trim('\t', '\n');
			string[] JP_Date_Spliter = new[] { "年", "月", "日", ":", "：" };
			var split = treatmentText.Split(JP_Date_Spliter, 10, StringSplitOptions.None);

			int year = 0;
			int month = 0;
			int day = 0;
			int hour = 0;
			int minute = 0;
			if (split.Length >= 3)
			{
				year = int.Parse(split[0]);
				month = int.Parse(split[1]);
				day = int.Parse(split[2]);
			}

			if (split.Length >= 5)
			{
				int.TryParse(split[3], out hour);
				int.TryParse(split[4], out minute);
			}


			return new DateTime(year, month, day, hour, minute, 0);
		}

		public static Task<CommunityDetailResponse> GetCommunityDetailAsync(NiconicoContext context, string communityId)
		{
			return GetCommunitySammaryPageHtmlAsync(context, communityId)
				.ContinueWith(prevTask => ParseCommunitySammaryPageHtml(prevTask.Result));
		}
    }
}
