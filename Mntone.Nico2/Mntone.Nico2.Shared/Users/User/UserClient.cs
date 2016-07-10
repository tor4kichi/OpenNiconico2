using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mntone.Nico2.Users.User
{
    internal sealed class UserClient
    {
		private static Task<string> GetUserDetailDataAsync(NiconicoContext context, string user_id)
		{
			// 投稿動画件数を同時に取得するため、ユーザーのvideoページからHTMLを取得する
			return context.GetClient()
				.GetConvertedStringAsync($"{NiconicoUrls.MakeUserPageUrl(user_id)}/video");
		}

		private static UserDetail ParseUserDetailData(string rawHtml)
		{
			var html = new HtmlDocument();
			html.LoadHtml(rawHtml);

			UserDetail data = new UserDetail();

			var body = html.DocumentNode
				.GetElementByTagName("html")
				.GetElementByTagName("body");
			var userDetail = body.GetElementByClassName("userDetail");
			var avatar = userDetail.GetElementByClassName("avatar");
			var profile = userDetail.GetElementByClassName("profile");


			data.ThumbnailUri = avatar.GetElementByTagName("img")
				.GetAttributeValue("src", "");

			data.Nickname = profile.GetElementByTagName("h2")
				.InnerText;

			var accountItems = profile.GetElementByClassName("account")
				.GetElementsByTagName("p")
				.ToArray();

			data.IsPremium = accountItems[0].GetElementByTagName("span")
				.InnerText.EndsWith("プレミアム会員");

			switch (accountItems[1].GetElementByTagName("span").InnerText)
			{
				case "男性":
					data.Gender = Sex.Male;
					break;
				case "女性":
					data.Gender = Sex.Female;
					break;

				case "未公開":
				default:
					data.Gender = null;
					break;
			}

			try
			{
				data.BirthDay = accountItems[2].GetElementByTagName("span").InnerText;
				data.Region = accountItems.ElementAtOrDefault(3)?.GetElementByTagName("span").InnerText ?? "非公開";
			}
			catch (Exception) { }


			try
			{
				var stats = profile.GetElementByClassName("stats");
				data.FavCount = uint.Parse(stats.GetElementByClassName("fav").InnerText);
				data.StampCount = uint.Parse(stats.GetElementByClassName("exp").GetElementByTagName("a").InnerText.Replace("EXP", ""));
			}
			catch (Exception) { }

			try
			{
				data.Description = profile
					.GetElementByClassName("userDetailComment")
					.GetElementById("user_description")
					.GetElementById("description_full")
					.GetElementByTagName("span")
					.InnerHtml;
			}
			catch
			{
				data.Description = "";
			}


			var video = body
				.GetElementByClassName("wrapper")
				.GetElementById("video");



			// ex) 投稿動画（606件）
			// 先頭5文字をスキップして、以降の任意桁数の数字を抽出
			try
			{
				data.TotalVideoCount = uint.Parse(
					new String(video.GetElementByTagName("h3").InnerText.Skip(5).TakeWhile(x => x >= '0' && x <= '9').ToArray())
					);
			}
			catch
			{
				// 投稿動画非公開の場合
				data.TotalVideoCount = 0;
				data.IsOwnerVideoPrivate = true;
			}

			return data;
		}


		public static Task<UserDetail> GetUserDetailAsync(NiconicoContext context, string user_id)
		{
			return GetUserDetailDataAsync(context, user_id)
				.ContinueWith(prevTask => ParseUserDetailData(prevTask.Result));
		}






		private static Task<string> GetUserDataAsync(NiconicoContext context, string user_id)
		{
			return context.GetClient()
				.GetStringAsync($"{NiconicoUrls.UserPageUrl}?{nameof(user_id)}={user_id}");
		}


		private static User ParseUserData(string xml)
		{
			var serializer = new XmlSerializer(typeof(UserResponse));

			using(var stream  = new StringReader(xml))
			{
				return (User)serializer.Deserialize(stream);
			}
		}

		public static Task<User> GetUserAsync(NiconicoContext context, string user_id)
		{
			return GetUserDataAsync(context, user_id)
				.ContinueWith(prevTask => ParseUserData(prevTask.Result));
		}


		
	}
}
