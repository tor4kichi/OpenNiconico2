﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Videos.Search
{
	// ref http://qiita.com/sampleb3/items/f75173167e22fbbe4d90

	internal static class SearchClient
    {
		public static async Task<string> GetKeywordSearchDataAsync(NiconicoContext context, string keyword, uint pageCount, SortMethod sortMethod, SortDirection sortDir)
		{
			var sortMethodChar = sortMethod.ToChar().ToString();
			var sortDirChar = sortDir.ToChar().ToString();
			return await context.GetClient()
				.GetStringAsync(NiconicoUrls.MakeKeywordSearchUrl(keyword, pageCount, sortMethodChar, sortDirChar));
		}

		public static async Task<string> GetTagSearchDataAsync(NiconicoContext context, string tag, uint pageCount, SortMethod sortMethod, SortDirection sortDir)
		{
			var sortMethodChar = sortMethod.ToShortString();
			var sortDirChar = sortDir.ToShortString();
			return await context.GetClient()
				.GetStringAsync(NiconicoUrls.MakeTagSearchUrl(tag, pageCount, sortMethodChar, sortDirChar));
		}


		public static SearchResponse ParseSearchData(string searchJson)
		{
			return JsonSerializerExtensions.Load<SearchResponse>(searchJson);
		}


		public static Task<SearchResponse> GetKeywordSearchAsync(NiconicoContext context, string keyword, uint pageCount, SortMethod sortMethod = SortMethod.FirstRetrieve, SortDirection sortDir = SortDirection.Descending)
		{
			return GetKeywordSearchDataAsync(context, keyword, pageCount, sortMethod, sortDir)
				.ContinueWith(prevTask => ParseSearchData(prevTask.Result));
		}

		public static Task<SearchResponse> GetTagSearchAsync(NiconicoContext context, string tag, uint pageCount, SortMethod sortMethod = SortMethod.FirstRetrieve, SortDirection sortDir = SortDirection.Descending)
		{
			return GetTagSearchDataAsync(context, tag, pageCount, sortMethod, sortDir)
				.ContinueWith(prevTask => ParseSearchData(prevTask.Result));
		}
	}
}