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
		public static async Task<string> GetWatchApiDataAsync(NiconicoContext context, string requestId, bool forceQuality)
		{
			if (!NiconicoRegex.IsVideoId(requestId))
			{
//				throw new ArgumentException();
			}

			var url = $"{NiconicoUrls.VideoWatchPageUrl}{requestId}";

			if (forceQuality)
			{
				url += "?eco=1";
			}

			try
			{
				var client = context.GetClient();
				
				var res = await context.GetClient()
					.GetAsync(url);

				if (res.StatusCode == Windows.Web.Http.HttpStatusCode.Forbidden)
				{
					throw new WebException("require payment.");
				}

				var text = await res.Content.ReadAsStringAsync().AsTask();

				var htmlDocument = new HtmlAgilityPack.HtmlDocument();
				htmlDocument.LoadHtml(text);

				var videoInfoNode = htmlDocument.GetElementbyId("watchAPIDataContainer");
				var str = WebUtility.UrlDecode(WebUtility.HtmlDecode(videoInfoNode.InnerText));

				System.Diagnostics.Debug.WriteLine(str);

				return str;
			}
			catch (Exception e)
			{
				throw new WebException("access failed watch/" + requestId, e);
			}
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

		public static Task<WatchApiResponse> GetWatchApiAsync(NiconicoContext context, string requestId, bool forceLowQuality)
		{
			return GetWatchApiDataAsync(context, requestId, forceLowQuality)
				.ContinueWith(prevTask => ParseWatchApi(prevTask.Result));
		}

	}
}
