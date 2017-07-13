﻿using Newtonsoft.Json;
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
		public static async Task<string> GetWatchApiDataAsync(NiconicoContext context, string requestId, bool forceQuality, HarmfulContentReactionType harmfulReactType)
		{
			if (!NiconicoRegex.IsVideoId(requestId))
			{
//				throw new ArgumentException();
			}

			var dict = new Dictionary<string, string>();
			var url = $"{NiconicoUrls.VideoWatchPageUrl}{requestId}";

			if (forceQuality)
			{
				dict.Add("eco", "1");
			}
			if (harmfulReactType != HarmfulContentReactionType.None)
			{
				dict.Add("watch_harmful", ((uint)harmfulReactType).ToString());
			}

			url += "?" + HttpQueryExtention.DictionaryToQuery(dict);

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

				// 推定有害動画の視聴ブロックページかをチェック
				if (htmlDocument.GetElementbyId("PAGECONTAINER") != null)
				{
					throw new ContentZoningException("access once blocked, maybe harmful video.");
				}
				else
				{
					var videoInfoNode = htmlDocument.GetElementbyId("js-initial-watch-data");
					var rawStr = videoInfoNode.GetAttributeValue("data-api-data", "");
					var htmlDecoded = WebUtility.HtmlDecode(rawStr);
					//var str = WebUtility.UrlDecode(htmlDecoded);

					System.Diagnostics.Debug.WriteLine(htmlDecoded);

					return htmlDecoded;
				}
			}
			catch (ContentZoningException)
			{
				throw;
			}
			catch (Exception e)
			{
				throw new WebException("access failed watch/" + requestId, e);
			}
		}


		public static InitialWatchData ParseWatchApi(string flvData)
		{
			var jsonSerializer = new JsonSerializer();
			jsonSerializer.NullValueHandling = NullValueHandling.Include;
			jsonSerializer.Error += JsonSerializer_Error;
			jsonSerializer.DefaultValueHandling = DefaultValueHandling.Include;
			
			var watchApi = jsonSerializer.Deserialize<InitialWatchData>(new JsonTextReader(new StringReader(flvData)));

			return watchApi;
		}

		private static void JsonSerializer_Error(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine(e.ErrorContext.Path);

		}

		public static Task<InitialWatchData> GetWatchApiAsync(NiconicoContext context, string requestId, bool forceLowQuality, HarmfulContentReactionType harmfulReactType)
		{
			return GetWatchApiDataAsync(context, requestId, forceLowQuality, harmfulReactType)
				.ContinueWith(prevTask => ParseWatchApi(prevTask.Result));
		}

	}
}
