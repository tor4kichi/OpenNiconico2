using Mntone.Nico2.Searches.Video;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Mylist.MylistGroup
{
    public sealed class MylistGroupClient
    {
		// マイリストの詳細取得
		public static Task<string> GetMylistGroupDetailDataAsync(
			NiconicoContext context
			, string group_id
			, bool isNeedDetail
			)
		{
			var dict = new Dictionary<string, string>();

			dict.Add("__format", "json");

			dict.Add(nameof(group_id), group_id);
			dict.Add(nameof(isNeedDetail), isNeedDetail.ToString1Or0());

			return context.GetStringAsync(NiconicoUrls.NICOVIDEO_CE_NICOAPI_V1_MYLISTGROUP_GET, dict);
		}


		// マイリストに含まれる動画リストの取得
		public static Task<string> GetMylistGroupVideoDataAsync(
			NiconicoContext context
			, string group_id
			, uint from
			, uint limit
			, Sort sort
			, Order order
			)
		{
			var dict = new Dictionary<string, string>();

			dict.Add("__format", "json");

			dict.Add(nameof(group_id), group_id);
			dict.Add(nameof(from), from.ToString());
			dict.Add(nameof(limit), limit.ToString());
			dict.Add(nameof(order), order.ToShortString());
			dict.Add(nameof(sort), sort.ToShortString());

			return context.GetStringAsync(NiconicoUrls.NICOVIDEO_CE_NICOAPI_V1_MYLIST_LIST, dict);
		}


		private static MylistGroupDetailResponse ParseMylistGroupDetailResponseJson(string json)
		{
			var responseContainer = JsonSerializerExtensions.Load<MylistGroupDetailResponseContainer>(json);

			return responseContainer.nicovideo_mylistgroup_response;
		}


		private static MylistGroupVideoResponse ParseMylistVideoItemsResponseJson(string json)
		{
			var responseContainer = JsonSerializerExtensions.Load<MylistGroupVideoResponseContainer>(json);

			return responseContainer.niconico_response;
		}



		public static Task<MylistGroupDetailResponse> GetMylistGroupDetailAsync(
			NiconicoContext context
			, string group_id
			, bool isNeedDetail
			)
		{
			return GetMylistGroupDetailDataAsync(context, group_id, isNeedDetail)
				.ContinueWith(prevTask => ParseMylistGroupDetailResponseJson(prevTask.Result));
		}



		public static Task<MylistGroupVideoResponse> GetMylistGroupVideoAsync(
			NiconicoContext context
			, string group_id
			, uint from
			, uint limit
			, Sort sort
			, Order order
			)
		{
			return GetMylistGroupVideoDataAsync(context, group_id, from, limit, sort, order)
				.ContinueWith(prevTask => ParseMylistVideoItemsResponseJson(prevTask.Result));
		}
	}
}
