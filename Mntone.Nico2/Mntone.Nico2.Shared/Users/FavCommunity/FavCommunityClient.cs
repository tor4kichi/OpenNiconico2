using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Users.FavCommunity
{
    public sealed class FavCommunityClient
    {

		// お気に入りのCommunityを取得
		// マイページのCommunity欄にアクセス
		// http://www.nicovideo.jp/my/community

		#region Fav Communit Listing

		public static Task<string> GetMyPageCommunityHtmlAsync(NiconicoContext context)
		{
			return context.GetClient()
				.GetConvertedStringAsync(NiconicoUrls.UserFavCommunityPageUrl);
		}


		private static FavCommunityResponse PerseFavCommunityPageHtml(string html)
		{
			var favCommunityResponse = new FavCommunityResponse();
			var doc = new HtmlAgilityPack.HtmlDocument();

			doc.LoadHtml(html);

			var rootHtml = doc.DocumentNode;

			var articlBodyElem = rootHtml
				.Element("html")
				.Element("body")
				.GetElementsByClassName("wrapper").First()
				.GetElementById("favCommunity")
				.GetElementByClassName("articleBody");

			// コミュニティが登録されていない時の対応
			if (articlBodyElem.GetElementByClassName("noListMsg") != null)
			{
				return favCommunityResponse;
			}

			foreach (var commuContainer in articlBodyElem.GetElementsByClassName("outer"))
			{
				var favCommunity = new FavCommunityInfo();

				var imgElem = commuContainer.GetElementByClassName("thumbContainer")
					.Element("a")
					.Element("img");
				{
					var iconUrl = imgElem.Attributes["src"].Value;
					favCommunity.IconUrl = iconUrl;
				}

				var sectionClassElem = commuContainer.GetElementByClassName("section");

				{
					var commuNameAnchorElem = sectionClassElem.Element("h5")
					.Element("a");

					var commuName = commuNameAnchorElem.InnerText;
					favCommunity.CommunityName = commuName;

					var communityId = commuNameAnchorElem.Attributes["href"].Value.Split('/').Last();
					favCommunity.CommunityId = communityId;

					var countElemItems = sectionClassElem.Element("ul").Elements("li");

					var videoCountText = countElemItems.ElementAt(0).InnerText.Split(':').Last();
					var videoCount = int.Parse(videoCountText);
					favCommunity.VideoCount = videoCount;

					var memberCountText = countElemItems.ElementAt(1).InnerText.Split(':').Last();
					var memberCount = int.Parse(memberCountText);
					favCommunity.MemberCount = memberCount;

					var descElem = sectionClassElem.Element("p");
					favCommunity.ShortDescription = descElem.InnerText;

					favCommunityResponse.Items.Add(favCommunity);
				}
				
			}


			return favCommunityResponse;
		}


		#endregion

		public static Task<FavCommunityResponse> GetFavCommunityAsync(NiconicoContext context)
		{
			return GetMyPageCommunityHtmlAsync(context)
				.ContinueWith(prevTask => PerseFavCommunityPageHtml(prevTask.Result));
		}




		// Communityへの登録
		// http://com.nicovideo.jp/motion/co1894176
		// mode:commit
		// title:登録申請
		// comment:
		// notify:
		// POST

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
	}
}
