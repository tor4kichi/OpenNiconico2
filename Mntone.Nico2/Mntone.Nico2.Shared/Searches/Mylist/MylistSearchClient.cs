using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Searches.Mylist
{
	public sealed class MylistSearchClient
    {
		public static Task<string> GetMylistSearchDataAsync(
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

			return context.GetStringAsync(NiconicoUrls.NICOVIDEO_CE_API_V1_MYLIST_SEARCH, dict);
		}


		private static MylistSearchResponse ParseVideoResponseJson(string mylistSearchResponseJson)
		{
			var responseContainer = JsonSerializerExtensions.Load<MylistSearchResponseContainer>(mylistSearchResponseJson);

			return responseContainer.nicovideo_mylist_response;
		}

		public static Task<MylistSearchResponse> GetMylistSearchAsync(
			NiconicoContext context
			, string keyword
			, uint from
			, uint limit
			, Sort sort
			, Order order
			)
		{
			return GetMylistSearchDataAsync(context, keyword, from, limit, sort, order)
				.ContinueWith(prevTask => ParseVideoResponseJson(prevTask.Result));
		}
	}
}
