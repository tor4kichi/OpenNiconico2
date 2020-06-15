using HtmlAgilityPack;

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			return context
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

			var htmlNode = doc.DocumentNode.Element("html");
			var body = htmlNode.Element("body");
			var main = body.Element("main");

			// 放送中
			var nowLiveElem = doc.DocumentNode.SelectNodes(@"//a[@class='now_live_inner']");

			if (nowLiveElem != null )
			{
				foreach (var liveItem in nowLiveElem)
				{
					var comLiveInfo = new CommunityLiveInfo();

					var liveTitleElem = liveItem.SelectSingleNode("//h2[@class='now_live_title']");
					comLiveInfo.LiveTitle = liveTitleElem.InnerText;

					var liveUrl = liveItem.Attributes["href"].Value;

					var withoutQuery = new string(liveUrl.TakeWhile(x => x != '?').ToArray());
					var liveId = withoutQuery.Split('/').LastOrDefault();

					if (liveId == null) { throw new Exception(); }
					comLiveInfo.LiveId = liveId;

					communityDetail.CurrentLiveList.Add(comLiveInfo);
				}
			}
			









			var header = body.Element("header");
			var communityData = header.SelectSingleNode("//div[@class='communityData']");
			var communityDetailElem = communityData.GetElementByClassName("communityDetail");

			try
			{
				var titleElem = communityData.Element("h2")
					.Element("a")
					;
				communityDetail.Name = titleElem.InnerText.Trim('\n', '\t');
			}
			catch { }

			// 開設日
			try
			{
				var createDateElem = communityDetailElem.SelectSingleNode("./tr[2]/td");
				var dateText = createDateElem.InnerText;
				var createDate = ParseDateTimeText(dateText);

				communityDetail.DateTime = createDate.ToString();

				var ownerNameAnchor = communityDetailElem.SelectSingleNode("./tr[1]/td/a");
				var id = ownerNameAnchor.Attributes["href"].Value.Split('/').LastOrDefault();
				communityDetail.OwnerUserId = id;

				var name = ownerNameAnchor.InnerText.Trim('\n', '\t');
				communityDetail.OwnerUserName = name;



			}
			catch { throw; }

			try
			{
				var tagElements = communityDetailElem.SelectNodes("./tr[3]//a");
				if (tagElements != null)
				{
					communityDetail.Tags = tagElements.Select(x => x.InnerText).ToList();
				}
			}
			catch { }



			var communityRegist = header.SelectSingleNode("//div[@class='communityRegist']");

			// プロフィール(数字やタグなど)
			{
				

				var levelElem = communityRegist.SelectSingleNode("./div/dl/dd[1]");
				var levelText = levelElem.InnerText;
				var level = uint.Parse(levelText);
				communityDetail.Level = level;


				var memberContainer = communityRegist.SelectSingleNode("./div/dl/dd[2]");
				var memberElem = memberContainer;
				var memberText = memberElem.FirstChild.InnerText.TrimStart('\n', '\t');
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
			try
			{
				var descContentElem = main.SelectSingleNode("id('profile_text_content')");
				communityDetail.ProfielHtml = descContentElem.InnerHtml;
			}
			catch
			{
			}

			// News
			var newsContainer = main.SelectSingleNode("//ul[@class='noticeList']");
			//			if (newsContainer.FirstChild.Name == "subbox")
			try
			{
				if (newsContainer != null)
				{
					foreach (var newsItem in newsContainer.Elements("li"))
					{
						var news = new CommunityNews();

						var newsHeaderElem = newsItem.SelectSingleNode(".//div[@class='noticeItemHeader']");

						var titleElem = newsHeaderElem.GetElementByClassName("noticeTitle");
						news.Title = titleElem.InnerText;

						var postDateElem = newsHeaderElem.SelectSingleNode(".//span[@class='date']");
						news.PostDate = ParseDateTimeText(postDateElem.InnerText);

						// 「（」から「）」までの文字列を取得する
						// 文字列自体に「（」「）」が含まれていても問題ないように取得している
						var postAuthorElem = newsHeaderElem.SelectSingleNode(".//span[@class='author']");
						news.PostAuthor = postAuthorElem.InnerText;


						var newsContentElem = newsItem.Element("p");
						news.ContentHtml = newsContentElem.InnerHtml;


						communityDetail.NewsList.Add(news);
					}
				}			
			}
			catch { }

			communitySammary.CommunityDetail = communityDetail;



			// 最近行われた生放送

			var sideContent = main.SelectSingleNode("//div[@class='area-sideContent']");
			try
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

				var liveInfoContainer = sideContent.SelectSingleNode("./section//h2[contains(.,'生放送のお知らせ')]");
				if (liveInfoContainer != null)
				{
					var recentLiveElem = liveInfoContainer.SelectSingleNode("../../ul[1]");
					var preservedLiveElem = liveInfoContainer.SelectSingleNode("../../ul[2]");

					var recentLiveItems = recentLiveElem.Elements("li").Where(x => !x.HasAttributes);
					foreach (var recentLiveItem in recentLiveItems)
					{
						var liveInfo = nodeToLiveInfo(recentLiveItem);

						communityDetail.RecentLiveList.Add(liveInfo);
					}

					var preservedLiveItems = preservedLiveElem.Elements("li").Where(x => !x.HasAttributes);
					foreach (var preservedLiveItem in preservedLiveItems)
					{
						var liveInfo = nodeToLiveInfo(preservedLiveItem);

						communityDetail.FutureLiveList.Add(liveInfo);
					}
				}				
			}
			catch { }

			// コミュニティメンバーのパース
			var communityFollowerContainerTitleElem = sideContent.SelectSingleNode("./section//h2[contains(.,'コミュニティフォロワー')]");
			var communityFollowerContainer = communityFollowerContainerTitleElem.ParentNode.ParentNode;

			try
			{
				//フォロワー数
				var followerCountElem = communityFollowerContainer.SelectSingleNode(".//span[@class='subinfo']");
				var followerCountAndMaxCountText = followerCountElem.InnerText;
				var followerCountText = new string(followerCountAndMaxCountText.TakeWhile(x => x >= '0' && x <= '9').ToArray());
				var followerCount = uint.Parse(followerCountText);
				communityDetail.MemberCount = followerCount;
			}
			catch { }

			// メンバーのサンプル
			try
			{
				var followerItems = communityFollowerContainer.SelectNodes("ul/li");
				foreach (var followerItem in followerItems)
				{
					var member = new CommunityMember();

					var memberAnchorElem = followerItem.Elements("a").FirstOrDefault();

					if (memberAnchorElem == null) { continue; }

					var videoUrl = memberAnchorElem.Attributes["href"].Value;
					var memberUserIdTemp = videoUrl.SkipWhile(x => !(x >= '0' && x <= '9'))
						.TakeWhile(x => x >= '0' && x <= '9')
						.ToArray();

					var memberUserId = new String(memberUserIdTemp);
					member.UserId = uint.Parse(memberUserId);

					var imgElem = memberAnchorElem.Element("img");
					member.IconUrl = new Uri(imgElem.Attributes["src"].Value);

					member.Name = imgElem.Attributes["title"].Value;

					communityDetail.SampleFollwers.Add(member);
				}
			}
			catch { }



			// コミュニティ動画
			var communityVideoContainerTitleElem = sideContent.SelectSingleNode("./section//h2[contains(.,'コミュニティ動画')]");
			var communityVideoContainer = communityVideoContainerTitleElem.ParentNode.ParentNode;

			try
			{
				// 動画数
				var videoCountElem = communityVideoContainer.SelectSingleNode(".//span[@class='subinfo']");
				var videoCountAndMaxCountText = videoCountElem.InnerText;
				var videoCountText = new string(videoCountAndMaxCountText.TakeWhile(x => x >= '0' && x <= '9').ToArray());
				var videoCount = uint.Parse(videoCountText);
				communityDetail.VideoCount = videoCount;
				communityDetail.VideoMaxCount = 10000;
			}
			catch { }

			try
			{
				var videoItems = communityVideoContainer.SelectNodes("ul/li");
				foreach (var videoItem in videoItems)
				{
					var video = new CommunityVideo();

					var videoAnchorElem = videoItem.Elements("a").FirstOrDefault();

					if (videoAnchorElem == null) { continue; }

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
			catch { }


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
