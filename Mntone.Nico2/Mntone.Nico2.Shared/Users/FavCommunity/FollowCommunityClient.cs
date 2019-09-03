using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if WINDOWS_UWP
using Windows.Web.Http;
#else
using System.Net.Http;
#endif

namespace Mntone.Nico2.Users.FollowCommunity
{
	public sealed class FollowCommunityClient
	{

		// お気に入りのCommunityを取得
		// マイページのCommunity欄にアクセス
		// http://www.nicovideo.jp/my/community

		#region Follow Community Listing

		public static Task<string> GetMyPageCommunityHtmlAsync(NiconicoContext context, int page)
		{
            var url = NiconicoUrls.UserFavCommunityPageUrl;
            if (page > 0)
            {
                url += "?page=" + (page + 1).ToString();
            }

            return context.GetConvertedStringAsync(url);
		}


		private static FollowCommunityResponse PerseFollowCommunityPageHtml(string html)
		{
			var followCommunityResponse = new FollowCommunityResponse();
			var doc = new HtmlAgilityPack.HtmlDocument();

			doc.LoadHtml(html);

			var rootHtml = doc.DocumentNode;

			var articlBodyElem = rootHtml
				.Element("html")
				.Element("body")
                .GetElementByClassName("BaseLayout")
				.GetElementsByClassName("wrapper").First()
				.GetElementById("favCommunity")
				.GetElementByClassName("articleBody");

			// コミュニティが登録されていない時の対応
			if (articlBodyElem.GetElementByClassName("noListMsg") != null)
			{
				return followCommunityResponse;
			}

			foreach (var commuContainer in articlBodyElem.GetElementsByClassName("outer"))
			{
				var favCommunity = new FollowCommunityInfo();

				var imgElem = commuContainer.GetElementByClassName("thumbContainer")
					.Element("a")
					.Element("img");
				{
					var iconUrl = imgElem.Attributes["src"].Value;
					favCommunity.IconUrl = iconUrl;
				}

				var sectionClassElem = commuContainer.GetElementByClassName("section");

                var commuNameAnchorElem = sectionClassElem.Element("h5")
                    .Element("a");

                var commuName = commuNameAnchorElem.InnerText;
                    favCommunity.CommunityName = commuName;

                var communityId = commuNameAnchorElem.Attributes["href"].Value.Split('/').Last();
                favCommunity.CommunityId = communityId;

                try
                {
                    var countElemItems = sectionClassElem.Element("ul").Elements("li");

                    var memberCountText = countElemItems.ElementAt(0).InnerText.Split(':').Last();
                    var memberCount = int.Parse(memberCountText.Replace(",", ""));
                    favCommunity.MemberCount = memberCount;

                    var descElem = sectionClassElem.Element("p");
                    favCommunity.ShortDescription = descElem.InnerText;
                }
                catch (Exception e)
                {
                    Debug.WriteLine("[OpenNico2] " + e.ToString());
                }

                followCommunityResponse.Items.Add(favCommunity);

            }


            return followCommunityResponse;
		}


		#endregion

		public static Task<FollowCommunityResponse> GetFollowCommunityAsync(NiconicoContext context, int page)
		{
			return GetMyPageCommunityHtmlAsync(context, page)
				.ContinueWith(prevTask => PerseFollowCommunityPageHtml(prevTask.Result));
		}
		



		// Communityへの登録
		// http://com.nicovideo.jp/motion/co1894176
		// mode:commit
		// title:登録申請
		// comment:
		// notify:
		// POST
		public static async Task<bool> AddFollowCommunity(
			NiconicoContext context
			, string communityId
			, string title = ""
			, string comment = ""
			, bool notify = false
			)
		{
			// http://com.nicovideo.jp/motion/id にアクセスして200かを確認

			var url = NiconicoUrls.CommunityJoinPageUrl + communityId;
			var res = await context.GetAsync(url);

            if (!res.IsSuccessStatusCode)
            {
                return false;
            }

			await Task.Delay(1000);

            // http://com.nicovideo.jp/motion/id に情報をpostする
            var dict = new Dictionary<string, string>()
            {
                { "mode", "commit" },
                { "title", title ?? "フォローリクエスト"},
                { "comment", comment ?? ""},
                { "notify", notify ? "1" : ""},
            };

            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("Referer", url);
            request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

#if WINDOWS_UWP
            request.Content = new HttpFormUrlEncodedContent(dict);
#else
            request.Content = new FormUrlEncodedContent(dict);
#endif

            var postResult = await context.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

			Debug.WriteLine(postResult);

			return postResult.IsSuccessStatusCode;
		}


		// 成功すると 200 
		// http://com.nicovideo.jp/motion/co2128730/done
		// にリダイレクトされる

		// 失敗すると
		// 404

		// 申請に許可が必要な場合は未調査


		// Communityからの登録解除
		// http://com.nicovideo.jp/leave/co2128730
		// にアクセスして、フォームから timeとcommit_keyを抽出して
		// time: UNIX_TIME
		// commit_key
		// commit
		// http://com.nicovideo.jp/leave/co2128730 にPOSTする
		// 成功したら200、失敗したら404

		public static async Task<CommunityLeaveToken> GetCommunityLeaveToken(NiconicoContext context, string communityId)
		{
			var url = NiconicoUrls.CommunityLeavePageUrl + communityId;
			var htmlText = await context.GetStringAsync(url);

			CommunityLeaveToken leaveToken = new CommunityLeaveToken()
			{
				CommunityId = communityId
			};

			HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
			document.LoadHtml(htmlText);

			var rootNode = document.DocumentNode;
			var hiddenInputs = rootNode.SelectNodes("//main//input");

			foreach (var hiddenInput in hiddenInputs)
			{
				var nameAttr = hiddenInput.GetAttributeValue("name", "");
				if (nameAttr == "time")
				{
					var timeValue = hiddenInput.GetAttributeValue("value", "");
					leaveToken.Time = timeValue;
				}
				else if (nameAttr == "commit_key")
				{
					var commit_key = hiddenInput.GetAttributeValue("value", "");
					leaveToken.CommitKey = commit_key;
				}
				else if (nameAttr == "commit")
				{
					var commit = hiddenInput.GetAttributeValue("value", "");
					leaveToken.Commit = commit;
				}
			}

			return leaveToken;
		}

		public static async Task<bool> RemoveFollowCommunity(NiconicoContext context, CommunityLeaveToken token)
		{
			var url = NiconicoUrls.CommunityLeavePageUrl + token.CommunityId;
			var dict = new Dictionary<string, string>();
			dict.Add("time", token.Time);
			dict.Add("commit_key", token.CommitKey);
			dict.Add("commit", token.Commit);

#if WINDOWS_UWP
            var content = new HttpFormUrlEncodedContent(dict);
#else
            var content = new FormUrlEncodedContent(dict);
#endif


            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("Referer", url);
            request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

            request.Content = content;
            var postResult = await context.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            Debug.WriteLine(postResult);

            return postResult.IsSuccessStatusCode;
		}




		
		
	}

	public class CommunityLeaveToken
	{
		public string CommunityId { get; set; }
		public string Time { get; set; }
		public string CommitKey { get; set; }
		public string Commit { get; set; }
	}
}
