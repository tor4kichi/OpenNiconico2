using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mntone.Nico2.Videos.Flv
{
	internal sealed class FlvClient
	{
		public static async Task<string> GetFlvDataAsync( NiconicoContext context, string requestId )
		{
//			if( !NiconicoRegex.IsVideoId( requestId ) )
			{
//				throw new ArgumentException();
			}

			await context
					.GetAsync($"{NiconicoUrls.VideoWatchPageUrl}{requestId}");

			await Task.Delay(1000);

			return await context
				.GetStringAsync($"{NiconicoUrls.VideoFlvUrl}{requestId}?as3=1");
		}

		public static async Task<string> GetFlvDataAsync( NiconicoContext context, string requestId, string cKey )
		{
			await context
					.GetAsync($"{NiconicoUrls.VideoWatchPageUrl}{requestId}");

			await Task.Delay(1000);

			return await context
				.GetStringAsync($"{NiconicoUrls.VideoFlvUrl}{requestId}?as3=1");
		}

		public static FlvResponse ParseFlvData( string flvData )
		{
			var response = flvData.Split( new char[] { '&' } ).ToDictionary(
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

		public static Task<FlvResponse> GetFlvAsync( NiconicoContext context, string requestId, string cKey )
		{
			return GetFlvDataAsync( context, requestId, cKey )
				.ContinueWith( prevTask => ParseFlvData( prevTask.Result ) );
		}
	}
}