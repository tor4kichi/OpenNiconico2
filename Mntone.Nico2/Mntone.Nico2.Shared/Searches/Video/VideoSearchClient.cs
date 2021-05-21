using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Searches.Video
{
    public sealed class VideoSearchClient
    {
        #region Video Info

        public static async Task<string> GetVideoDataAsync(NiconicoContext context, string videoId)
        {
            var dict = new Dictionary<string, string>();
            dict.Add("__format", "json");
            dict.Add("v", videoId);

            return await context.GetStringAsync(NiconicoUrls.NICOVIDEO_CE_NICOAPI_V1_VIDEO_INFO, dict);
        }

        private static VideoInfoResponse ParseVideoInfoResponseJson(string json)
        {
            var responseContainer = JsonSerializerExtensions.Load<VideoInfoResponseContainer>(json);

            return responseContainer.NicovideoVideoResponse;
        }


        public static Task<VideoInfoResponse> GetVideoInfoAsync(
            NiconicoContext context
            , string videoId
            )
        {
            return GetVideoDataAsync(context, videoId)
                .ContinueWith(prevTask => ParseVideoInfoResponseJson(prevTask.Result));
        }


		#endregion

		#region VideoInfo Array

		public static async Task<string> GetVideoArrayDataAsync(NiconicoContext context, IEnumerable<string> videoIdList)
		{
			var dict = new Dictionary<string, string>();
			dict.Add("__format", "json");
			dict.Add("v", string.Join(",", videoIdList));

			return await context.GetStringAsync(NiconicoUrls.NICOVIDEO_CE_NICOAPI_V1_VIDEO_INFO_ARRAY, dict);
		}

		public static async Task<VideoInfoArrayResponse> GetVideoInfoArrayAsync(
			NiconicoContext context
			, IEnumerable<string> videoIdList
			)
		{
			var json = await GetVideoArrayDataAsync(context, videoIdList);
			var res = JsonSerializerExtensions.Load<VideoInfoArrayResponseContainer>(json);
			return res.DataContainer;
		}


		#endregion


		public static async Task<string> GetKeywordSearchDataAsync(
			NiconicoContext context
			, string str
			, uint from
			, uint limit
			, Sort? sort
			, Order? order
			)
		{
			var dict = new Dictionary<string, string>();

			dict.Add("__format", "json");

			dict.Add(nameof(str), str);
			dict.Add(nameof(from), from.ToString());
			dict.Add(nameof(limit), limit.ToString());
			if (order.HasValue)
			{
				dict.Add(nameof(order), order.Value == Order.Ascending ? "a" : "d");
			}
			if (sort.HasValue)
			{
				dict.Add(nameof(sort), sort.Value.ToShortString());
			}

			return await context.GetStringAsync(NiconicoUrls.NICOVIDEO_CE_NICOAPI_V1_VIDEO_SEARCH, dict);
		}

		public static async Task<string> GetTagSearchDataAsync(
			NiconicoContext context
			, string tag
			, uint from
			, uint limit
			, Sort? sort
			, Order? order
			)
		{
			var dict = new Dictionary<string, string>();

			dict.Add("__format", "json");

			dict.Add(nameof(tag), tag);
			dict.Add(nameof(from), from.ToString());
			dict.Add(nameof(limit), limit.ToString());
			if (order.HasValue)
			{
                dict.Add(nameof(order), order.Value == Order.Ascending ? "a" : "d");
            }
            if (sort.HasValue)
			{
				dict.Add(nameof(sort), sort.Value.ToShortString());
			}

			return await context.GetStringAsync(NiconicoUrls.NICOVIDEO_CE_NICOAPI_V1_TAG_SEARCH, dict);
		}


		private static VideoListingResponse ParseVideoResponseJson(string videoSearchResponseJson)
		{
			var responseContainer = JsonSerializerExtensions.Load<VideoListingResponseContainer>(videoSearchResponseJson);

			return responseContainer.niconico_response;
		}




        


        public static Task<VideoListingResponse> GetKeywordSearchAsync(
			NiconicoContext context
			, string keyword
			, uint from
			, uint limit
			, Sort? sort
			, Order? order
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
			, Sort? sort
			, Order? order
			)
		{
			return GetTagSearchDataAsync(context, tag, from, limit, sort, order)
				.ContinueWith(prevTask => ParseVideoResponseJson(prevTask.Result));
		}
	}
}
