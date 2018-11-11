using HtmlAgilityPack;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mntone.Nico2.Live.Description
{
	internal sealed class DescriptionClient
	{
		public static Task<string> GetDescriptionDataAsync( NiconicoContext context, string requestId )
		{
			if( !NiconicoRegex.IsLiveId( requestId ) )
			{
				throw new ArgumentException();
			}

			return context.GetClient().GetConvertedStringAsync( NiconicoUrls.LiveGatePageUrl + requestId );
		}

		public static string ParseDescriptionData( string userInfoData )
		{
			var html = new HtmlDocument();
			html.LoadHtml( userInfoData );

			var htmlHtml = html.DocumentNode.Element( "html" );
			var language = htmlHtml.GetAttributeValue( "lang", "ja-jp" );
			return htmlHtml.SelectSingleNode("//*[@id=\"jsFollowingAdMain\"]")?.GetElementByClassName("gate_description_area").InnerHtml;
		}

		public static Task<string> GetDescriptionAsync( NiconicoContext context, string requestId )
		{
			return GetDescriptionDataAsync( context, requestId )
				.ContinueWith( prevTask => ParseDescriptionData( prevTask.Result ) );
		}
	}
}