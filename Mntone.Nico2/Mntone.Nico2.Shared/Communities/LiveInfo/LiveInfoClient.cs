using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Communities.Live
{
    public sealed class LiveInfoClient
    {
		public static Task<string> GetCommunityLiveInfoDataAsync(NiconicoContext context, string community_id)
		{
			var dict = new Dictionary<string, string>();

			dict.Add("__format", "json");
			dict.Add(nameof(community_id), community_id);

			return context.GetStringAsync(NiconicoUrls.NICOVIDEO_CE_LIVEAPI_V1_COMMUNITY_VIDEO, dict);
		}


		private static NicoliveVideoResponse ParseCommunityLiveInfoJson(string json)
		{
			var res = JsonSerializerExtensions.Load<CommunityLiveInfoResponse>(json);

			return res.NicoliveVideoResponse;
		}


		public static Task<NicoliveVideoResponse> GetCommunityLiveInfo(NiconicoContext context, string communityId)
		{
			return GetCommunityLiveInfoDataAsync(context, communityId)
				.ContinueWith(prevTask => ParseCommunityLiveInfoJson(prevTask.Result));
		}
	}
}
