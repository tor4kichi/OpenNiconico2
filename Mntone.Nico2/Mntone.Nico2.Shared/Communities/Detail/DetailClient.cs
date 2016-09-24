using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

			var doc = new HtmlAgilityPack.HtmlDocument();

			doc.LoadHtml(html);
			var root = doc.DocumentNode.Element("html");
			var siteBody = root.Element("body")
				.GetElementById("site-body");
			var cfix = siteBody.GetElementByClassName("cfix");
				


			// コンテンツの読み取り
			var comMain = cfix.GetElementById("community_main");
			var comMainContent = comMain.GetElementById("community_prof_frm")
				.GetElementById("community_prof_frm2");


			var communitySammary = new CommunitySammary();
			var communityDetail = new CommunityDetail();
			// 開設日
			try
			{
				var cfix_r = comMainContent.GetElementByClassName("cfix")
					.GetElementByClassName("r");

				var createDateElem = cfix_r.Elements("p").ElementAt(0).Element("strong");
				var dateText = createDateElem.InnerText;
				var createDate = ParseDateTimeText(dateText);

				communityDetail.DateTime = createDate.ToString();

				var ownerNameAnchor = cfix_r.Elements("p").ElementAt(1).Element("a");
				var id = ownerNameAnchor.Attributes["href"].Value.Split('/').LastOrDefault();
				communityDetail.OwnerUserId = id;

				var name = ownerNameAnchor.Element("strong").InnerText;
				communityDetail.OwnerUserName = name;

			}
			catch { throw; }



			var profileContainer = comMainContent.GetElementById("cbox_profile");

			// プロフィール(数字やタグなど)
			{
				var tbody = profileContainer
					.Element("table")/*.Element("tbody")*/.Element("tr").Element("td")
					.Element("table")/*.Element("tbody")*/;

				var container = tbody.Elements("tr");

				var levelContainer = container.ElementAt(0);
				var levelElem = levelContainer.Elements("td").ElementAt(1).Elements("strong").ElementAt(0);
				var levelText = levelElem.InnerText;
				var level = uint.Parse(levelText);
				communityDetail.Level = level;


				var memberContainer = container.ElementAt(1);
				var memberElem = memberContainer.Elements("td").ElementAt(1).Elements("strong").ElementAt(0);
				var memberText = memberElem.InnerText;
				var memberCount = uint.Parse(memberText);
				communityDetail.MemberCount = memberCount;

				// TODO: 
//				var memberMaxElem = memberContainer.


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

				// TODO: タグ未登録時の対応

				var tagsContainer = container.ElementAt(3);
				var tagsRoot = tagsContainer.Elements("td").ElementAt(1);

				foreach (var tagElem in tagsRoot.Elements("a"))
				{
					var tagText = tagElem.Element("strong").InnerText;

					communityDetail.Tags.Add(tagText);
				}

				// TODO: 特権を獲得していない場合への対応

				var privilegeContainer = container.ElementAt(5);
				var privilegeDescElem = privilegeContainer.Elements("td").ElementAt(1).Element("div").Element("p");
				var privilegeDescText = privilegeDescElem.InnerText;
				communityDetail.PrivilegeDescription = privilegeDescText;


				var videoContainer = container.ElementAt(7);
				var videoCountElem = videoContainer.Elements("td").ElementAt(1).Element("strong");
				var videoCount = uint.Parse(videoCountElem.InnerText);
				communityDetail.VideoCount = videoCount;
				communityDetail.VideoMaxCount = 10000;




			}


			// Htmlによるコミュニティの説明
			{
				var descriptionContainer = profileContainer.GetElementById("community_description");
				var descContentElem = descriptionContainer.GetElementByClassName("subbox")
					.GetElementByClassName("cnt")
					.GetElementByClassName("cnt2");

				communityDetail.ProfielHtml = descContentElem.InnerHtml;
			}

			// News

			var newsContainer = comMainContent.GetElementById("cbox_news");
			//			if (newsContainer.FirstChild.Name == "subbox")
			try
			{
				var newsElements = newsContainer.GetElementByClassName("subbox")?
					.GetElementByClassName("cnt")
					.GetElementByClassName("cnt2")
					.GetElementById("community_news");
				if (newsElements != null)
				{
					foreach (var newsItem in newsElements.GetElementsByClassName("item"))
					{
						var news = new CommunityNews();

						var titleElem = newsItem.GetElementByClassName("title");
						news.Title = titleElem.InnerText;

						var descElem = newsItem.GetElementByClassName("desc");
						news.ContentHtml = descElem.InnerHtml;

						var postInfoElem = newsItem.GetElementByClassName("date");
						news.PostDate = ParseDateTimeText(postInfoElem.InnerText);

						// 「（」から「）」までの文字列を取得する
						// 文字列自体に「（」「）」が含まれていても問題ないように取得している
						var postAuthorTemp = postInfoElem.InnerText.SkipWhile(x => x != '（').ToList();
						var removePos = postAuthorTemp.LastIndexOf('）');
						postAuthorTemp.RemoveAt(removePos);
						postAuthorTemp.RemoveAt(0);
						var postAuthor = new String(postAuthorTemp.ToArray());
						news.PostAuthor = postAuthor;

						communityDetail.NewsList.Add(news);
					}
				}
			}
			catch { }

			communitySammary.CommunityDetail = communityDetail;



			// 最近行われた生放送

			var communitySide = cfix.GetElementById("community_side");
			{
				var futureLiveElem = communitySide.GetElementById("future_live");
				var recentLiveItems = futureLiveElem
					.ChildNodes.SkipWhile(x => !x.HasAttributes || x.Attributes["class"]?.Value != "item")
					.TakeWhile(x => x.HasAttributes && x.Attributes["class"]?.Value == "item")
					.ToList();
				var itemCount = recentLiveItems.Count();


				Func<HtmlNode, LiveInfo> nodeToLiveInfo = (HtmlNode node) =>
				{
					var liveInfo = new LiveInfo();

					var dateElem = node.GetElementByClassName("date");
					
					var date = ParseDateTimeText(dateElem.FirstChild.InnerText);
					liveInfo.StartTime = date;

					var titleElem = node.GetElementByClassName("title");
					var titleAnchor = titleElem.Element("a");
					liveInfo.Title = titleAnchor.InnerText;

					var livePageUrl = titleAnchor.Attributes["href"].Value;
					var pos = livePageUrl.IndexOf("lv");
					var idCharList = livePageUrl.Skip(pos).TakeWhile(x => x == 'l' || x == 'v' || (x >= '0' && x <= '9'));
					var id = new String(idCharList.ToArray());
					liveInfo.LiveId = id;

					var userNameTemp = titleElem.InnerText.Skip(5).ToList();
					var removePos = userNameTemp.LastIndexOf('）');
					userNameTemp.RemoveAt(removePos);
					var streamerName = new string(userNameTemp.ToArray());
					liveInfo.StreamerName = streamerName;
					return liveInfo;
				};

				foreach (var recentLiveItem in recentLiveItems)
				{
					var liveInfo = nodeToLiveInfo(recentLiveItem);

					communityDetail.RecentLiveList.Add(liveInfo);
				}


				// TODO: 予約放送の情報パースは過去の放送と同じで問題ない？
				var skipCount = 4 + itemCount;
				var preservedLiveItems = futureLiveElem
					.ChildNodes.Skip(skipCount)
					.TakeWhile(x => x.HasAttributes && x.Attributes["class"]?.Value == "item");
					
				foreach (var preservedLiveItem in preservedLiveItems)
				{
					var liveInfo = nodeToLiveInfo(preservedLiveItem);

					communityDetail.FutureLiveList.Add(liveInfo);
				}
			}

			// TODO: コミュニティメンバーのパース

			// コミュニティ動画
			{
				// table要素をベースに検出する
				// community-sideのtable数が2以上の時、
				// 最後のtable要素をコミュニティ動画のコンテナとして扱う
				var hasCommunityVideoItem = communitySide.Elements("table").Count() >= 2;
				if (hasCommunityVideoItem)
				{
					var videoItemsContainer = communitySide.Elements("table")
						.Last()/*
						.Element("tbody")*/;

					// 奇数インデックスのアイテムは点線要素なのでスキップ
					foreach (var videoItem in videoItemsContainer.Elements("tr").Where((x, i) => i % 2 == 0))
					{
						var video = new CommunityVideo();

						var videoAnchorElem = videoItem
							.Elements("td")
							.ElementAt(0)
							.Element("a");

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
