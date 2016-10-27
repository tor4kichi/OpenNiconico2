using HtmlAgilityPack;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Mntone.Nico2.Users.Info
{
	[DataContract]
	public class UserMyPageJSInfo
	{
		[DataMember(Name = "id")]
		public string Id { get; set; }

		[DataMember(Name = "age")]
		public string Age { get; set; }

		[DataMember(Name = "isPremium")]
		public string IsPremium { get; set; }

		[DataMember(Name = "isMan")]
		public string IsMan { get; set; }
	}

	internal sealed class InfoClient
	{
		public static Task<string> GetInfoDataAsync( NiconicoContext context )
		{
			return context.GetClient().GetConvertedStringAsync( NiconicoUrls.UserPageUrl + "/top" );
		}

		public static InfoResponse ParseInfoData( string userInfoData )
		{
			var html = new HtmlDocument();
			html.LoadHtml( userInfoData );

			var htmlHtml = html.DocumentNode.Element( "html" );
			var language = htmlHtml.GetAttributeValue( "lang", "ja-jp" );


			var head = htmlHtml.Element("head");
			var userInfoJSStartString = "var User = ";
			var userInfoStartPos = head.InnerHtml.IndexOf(userInfoJSStartString);
			var realStartPos = userInfoJSStartString.Length + userInfoStartPos;

			var userInfoJsonCharArray = head.InnerHtml.Skip(realStartPos).TakeWhile(x => x != ';').ToArray();

			var json = new string(userInfoJsonCharArray);

			json = json.Replace("!!document.cookie.match(/nicoadult\\s*=\\s*1/)", "");

			UserMyPageJSInfo info = null;
			try
			{
				info = JsonSerializerExtensions.Load<UserMyPageJSInfo>(json);
			}
			catch (Exception ex)
			{
				ex.Data.Add("User Info Json", json);
				throw ex;
			}

			return new InfoResponse(htmlHtml.Element("body"), language, info);
		}

		public static Task<InfoResponse> GetInfoAsync( NiconicoContext context )
		{
			return GetInfoDataAsync( context )
				.ContinueWith( prevTask => ParseInfoData( prevTask.Result ) );
		}
	}
}