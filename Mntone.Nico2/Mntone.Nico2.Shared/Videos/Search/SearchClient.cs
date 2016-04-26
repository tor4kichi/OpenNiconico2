using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Videos.Search
{
	// ref http://qiita.com/sampleb3/items/f75173167e22fbbe4d90

	internal static class SearchClient
    {
		public static async Task<string> GetKeywordSearchDataAsync(NiconicoContext context, string keyword, uint pageCount)
		{
			return await context.GetClient()
				.GetStringAsync(NiconicoUrls.MakeKeywordSearchUrl(keyword, pageCount));
		}

		public static async Task<string> GetTagSearchDataAsync(NiconicoContext context, string tag, uint pageCount)
		{
			return await context.GetClient()
				.GetStringAsync(NiconicoUrls.MakeKeywordSearchUrl(tag, pageCount));
		}


		public static SearchResponse ParseSearchData(string searchJson)
		{
			return JsonSerializerExtensions.Load<SearchResponse>(searchJson);
		}


		public static Task<SearchResponse> GetKeywordSearchAsync(NiconicoContext context, string keyword, uint pageCount)
		{
			return GetKeywordSearchDataAsync(context, keyword, pageCount)
				.ContinueWith(prevTask => ParseSearchData(prevTask.Result));
		}

		public static Task<SearchResponse> GetTagSearchAsync(NiconicoContext context, string tag, uint pageCount)
		{
			return GetTagSearchDataAsync(context, tag, pageCount)
				.ContinueWith(prevTask => ParseSearchData(prevTask.Result));
		}
	}
}
