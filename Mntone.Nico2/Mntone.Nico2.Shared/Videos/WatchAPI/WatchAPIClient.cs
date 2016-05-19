using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Videos.WatchAPI
{
	internal sealed class WatchAPIClient
	{
		public static async Task<string> GetWatchApiDataAsync(NiconicoContext context, string requestId)
		{
			if (!NiconicoRegex.IsVideoId(requestId))
			{
				throw new ArgumentException();
			}

			var htmlWeb = new HtmlAgilityPack.HtmlWeb();
			var htmlDocument = await htmlWeb.LoadFromWebAsync($"{NiconicoUrls.VideoWatchPageUrl}{requestId}");

			var videoInfoNode = htmlDocument.GetElementbyId("watchAPIDataContainer");
			var str = WebUtility.UrlDecode(WebUtility.HtmlDecode(videoInfoNode.InnerText));

			System.Diagnostics.Debug.WriteLine(str);

			return str;
		}


		public static WatchApiResponse ParseWatchApi(string flvData)
		{
			var jsonSerializer = new JsonSerializer();
			jsonSerializer.NullValueHandling = NullValueHandling.Include;
			jsonSerializer.Error += JsonSerializer_Error;
			jsonSerializer.DefaultValueHandling = DefaultValueHandling.Include;
			
			var watchApi = jsonSerializer.Deserialize<WatchApiResponse>(new JsonTextReader(new StringReader(flvData)));

			return watchApi;
		}

		private static void JsonSerializer_Error(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine(e.ErrorContext.Path);

		}

		public static Task<WatchApiResponse> GetWatchApiAsync(NiconicoContext context, string requestId)
		{
			return GetWatchApiDataAsync(context, requestId)
				.ContinueWith(prevTask => ParseWatchApi(prevTask.Result));
		}

	}
}
