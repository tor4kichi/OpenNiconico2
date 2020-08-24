using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
			return context
				.GetConvertedStringAsync($"{NiconicoUrls.MakeUserPageUrl(user_id)}/video");
		}

		private static UserDetailResponse ParseUserDetailData(string rawHtml)
		{
			var html = new HtmlDocument();
			html.LoadHtml(rawHtml);
			var jsInitializeUserPageDataNode = html.GetElementbyId("js-initial-userpage-data");
			var initialDataAttr = jsInitializeUserPageDataNode.Attributes["data-initial-data"];
			return JsonConvert.DeserializeObject<UserDetailResponse>(WebUtility.HtmlDecode(initialDataAttr.Value));
		}


		public static async Task<UserDetailResponse.UserDetails> GetUserDetailAsync(NiconicoContext context, string user_id)
		{
			var htmlText = await context
				.GetConvertedStringAsync($"https://www.nicovideo.jp/user/{user_id}");			
			return ParseUserDetailData(htmlText).Container.Details;
		}






		private static Task<string> GetUserDataAsync(NiconicoContext context, string user_id)
		{
			return context
				.GetStringAsync($"{NiconicoUrls.CE_UserApiUrl}?{nameof(user_id)}={user_id}");
		}


		private static UserResponse ParseUserData(string xml)
		{
			var serializer = new XmlSerializer(typeof(UserResponse));

			using(var stream  = new StringReader(xml))
			{
				return (UserResponse)serializer.Deserialize(stream);
			}
		}

		public static Task<UserResponse> GetUserAsync(NiconicoContext context, string user_id)
		{
			return GetUserDataAsync(context, user_id)
				.ContinueWith(prevTask => ParseUserData(prevTask.Result));
		}


		
	}
}
