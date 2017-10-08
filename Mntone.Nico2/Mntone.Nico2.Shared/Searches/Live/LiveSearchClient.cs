using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Searches.Live
{
    public sealed class LiveSearchClient
    {
		public static Task<string> GetLiveSearchDataAsync(
			NiconicoContext context,
			string word,
			bool isTagSearch,
			Nico2.Live.CommunityType? pt = null,
			uint from = 0,
			uint limit = 30,
			Order? order = null,
			NicoliveSearchSort? sort = null,
			NicoliveSearchMode? search_mode = null
			)
		{
			var dict = new Dictionary<string, string>();

			var __format = "json";
			dict.Add(nameof(__format), __format);

			dict.Add(nameof(word), word);

			if (isTagSearch)
			{
				var kind = "tags";
				dict.Add(nameof(kind), kind);
			}

			if (pt.HasValue)
			{
				dict.Add(nameof(pt), pt.ToString().ToLower());
			}

			if (from > 0)
			{
				dict.Add(nameof(from), from.ToString());
			}

			if (limit >= 150)
			{
				limit = 149;
			}

			dict.Add(nameof(limit), limit.ToString());

			if (order.HasValue)
			{
				dict.Add(nameof(order), order.Value.ToChar().ToString());
			}

			if (sort.HasValue)
			{
				dict.Add(nameof(sort), sort.Value.ToString().ToLower());
			}

			if (search_mode.HasValue)
			{
				dict.Add(nameof(search_mode), search_mode.Value.ToString().ToLower());
			}

			return context.GetStringAsync(NiconicoUrls.NICOVIDEO_CE_LIVEAPI_V1_VIDEO_SEARCH, dict);
		}


		private static NicoliveVideoResponse ParseLiveSearchJson(string json)
		{
			var res = JsonSerializerExtensions.Load<NicoliveVideoResponseContainer>(json);

			return res.NicoliveVideoResponse;
		}


		public static Task<NicoliveVideoResponse> GetLiveSearchAsync(
			NiconicoContext context,
			string word,
			bool isTagSearch,
			Nico2.Live.CommunityType? provider = null,
			uint from = 0,
			uint length = 30,
			Order? order = null,
			NicoliveSearchSort? sort = null,
			NicoliveSearchMode? mode = null
			)
		{
			return GetLiveSearchDataAsync(
				context, word, isTagSearch,
				provider,
				from,
				length,
				order,
				sort,
				mode
				)
				.ContinueWith(prevTask => ParseLiveSearchJson(prevTask.Result));
		}
	}
}
