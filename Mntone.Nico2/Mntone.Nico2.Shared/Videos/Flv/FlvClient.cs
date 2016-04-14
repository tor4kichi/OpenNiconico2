using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace Mntone.Nico2.Videos.Flv
{
	internal sealed class FlvClient
	{
		public static async Task<string> GetFlvDataAsync( NiconicoContext context, string requestId )
		{
			if( !NiconicoRegex.IsVideoId( requestId ) )
			{
				throw new ArgumentException();
			}

			var htmlWeb = new HtmlAgilityPack.HtmlWeb();
			var htmlDocument = await htmlWeb.LoadFromWebAsync($"{NiconicoUrls.VideoWatchPageUrl}{requestId}");

			var videoInfoNode = htmlDocument.GetElementbyId("watchAPIDataContainer");
			var str = WebUtility.UrlDecode(WebUtility.HtmlDecode(videoInfoNode.InnerText));

			System.Diagnostics.Debug.Write(str);

			return str;
		}

		
		public static FlvResponse ParseFlvData( string flvData )
		{
			var json = (dynamic)Newtonsoft.Json.JsonConvert.DeserializeObject(flvData);

			var info = (string)json.flvInfo;
			var response = info.Split( new char[] { '&' } ).ToDictionary(
				source => source.Substring( 0, source.IndexOf( '=' ) ),
				source => Uri.UnescapeDataString( source.Substring( source.IndexOf( '=' ) + 1 ) ) );

			if( response.ContainsKey( "error" ) )
			{
				throw new Exception( "Parse Error: " + response["error"] );
			}

			return new FlvResponse( response );
		}

		public static Task<FlvResponse> GetFlvAsync( NiconicoContext context, string requestId )
		{
			return GetFlvDataAsync( context, requestId )
				.ContinueWith( prevTask => ParseFlvData( prevTask.Result ) );
		}

	}
}