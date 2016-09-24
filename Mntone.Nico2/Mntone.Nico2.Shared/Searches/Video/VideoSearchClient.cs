using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Searches.Video
{
    public sealed class VideoSearchClient
    {
		public static async Task<string> GetKeywordSearchDataAsync(
			NiconicoContext context
			, string str
			, uint from
			, uint limit
			, Sort sort
			, Order order
			)
		{
			var dict = new Dictionary<string, string>();

			dict.Add("__format", "json");

			dict.Add(nameof(str), str);
			dict.Add(nameof(from), from.ToString());
			dict.Add(nameof(limit), limit.ToString());
			dict.Add(nameof(order), order.ToShortString());
			dict.Add(nameof(sort), sort.ToShortString());

			return await context.GetStringAsync(NiconicoUrls.NICOVIDEO_CE_NICOAPI_V1_VIDEO_SEARCH, dict);
		}

		public static async Task<string> GetTagSearchDataAsync(
			NiconicoContext context
			, string tag
			, uint from
			, uint limit
			, Sort sort
			, Order order
			)
		{
			var dict = new Dictionary<string, string>();

			dict.Add("__format", "json");

			dict.Add(nameof(tag), tag);
			dict.Add(nameof(from), from.ToString());
			dict.Add(nameof(limit), limit.ToString());
			dict.Add(nameof(order), order.ToShortString());
			dict.Add(nameof(sort), sort.ToShortString());

			return await context.GetStringAsync(NiconicoUrls.NICOVIDEO_CE_NICOAPI_V1_TAG_SEARCH, dict);
		}


		private static VideoListingResponse ParseVideoResponseJson(string videoSearchResponseJson)
		{
			var responseContainer = JsonSerializerExtensions.Load<VideoListingResponseContainer>(videoSearchResponseJson);

			return responseContainer.nicovideo_video_response;
		}

		

		public static Task<VideoListingResponse> GetKeywordSearchAsync(
			NiconicoContext context
			, string keyword
			, uint from
			, uint limit
			, Sort sort
			, Order order
			)
		{
			return GetKeywordSearchDataAsync(context, keyword, from, limit, sort, order)
				.ContinueWith(prevTask => ParseVideoResponseJson(prevTask.Result));
		}

		public static Task<VideoListingResponse> GetTagSearchAsync(
			NiconicoContext context
			, string tag
			, uint from
			, uint limit
			, Sort sort
			, Order order
			)
		{
			return GetTagSearchDataAsync(context, tag, from, limit, sort, order)
				.ContinueWith(prevTask => ParseVideoResponseJson(prevTask.Result));
		}
	}
}
