using HtmlAgilityPack;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Mntone.Nico2.Users.Info
{
	[DataContract]
	public class UserMyPageJSInfo
	{

		[DataMember(Name = "id")]
		public uint Id { get; set; }

		[DataMember(Name = "age")]
		public uint Age { get; set; }

		[DataMember(Name = "isPremium")]
		public bool IsPremium { get; set; }

		[DataMember(Name = "isOver18")]
		public bool IsOver18 { get; set; }

		[DataMember(Name = "isMan")]
		public bool IsMan { get; set; }
	}

	internal sealed class InfoClient
	{
		public static Task<string> GetInfoDataAsync( NiconicoContext context )
		{
			return context.GetClient().GetConvertedStringAsync( NiconicoUrls.UserPageUrl );
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

			var info = JsonSerializerExtensions.Load<UserMyPageJSInfo>(json);

			return new InfoResponse(htmlHtml.Element("body"), language, info);
		}

		public static Task<InfoResponse> GetInfoAsync( NiconicoContext context )
		{
			return GetInfoDataAsync( context )
				.ContinueWith( prevTask => ParseInfoData( prevTask.Result ) );
		}
	}
}