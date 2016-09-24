using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Communities.Info
{
    public sealed class InfoClient
    {
		public static Task<string> GetCommunityInfoDataAsync(
			NiconicoContext context
			, string id
			)
		{
			var dict = new Dictionary<string, string>();

			dict.Add("__format", "json");

			dict.Add(nameof(id), id);
			return context.GetStringAsync(NiconicoUrls.NICOVIDEO_CE_API_V1_COMMUNITY_INFO, dict);
		}

		private static NicovideoCommunityResponse ParseCommunityInfoResponseJson(string json)
		{
			var responseContainer = JsonSerializerExtensions.Load<CommunityInfoResponseContainer>(json);

			return responseContainer.NicovideoCommunityResponse;
		}


		public static Task<NicovideoCommunityResponse> GetCommunityInfoAsync(
			NiconicoContext context,
			string communityId
			)
		{
			return GetCommunityInfoDataAsync(context, communityId)
				.ContinueWith(prevTask => ParseCommunityInfoResponseJson(prevTask.Result));
		}
	}
}
