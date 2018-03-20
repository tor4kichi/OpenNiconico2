using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Live.Video
{
    public static class LiveVideoClient
    {
        internal static class LiveVideoInfoSubClient
        {
            private static Task<string> GetLiveInfoJsonAsync(NiconicoContext context, string liveId)
            {
                var dict = new Dictionary<string, string>();
                dict.Add("v", liveId);
                dict.Add("__format", "json");
                return context.GetStringAsync(NiconicoUrls.CeLiveVideoInfoApi, dict);
            }

            public static async Task<NicoliveVideoInfoResponse> GetLiveInfoAsync(NiconicoContext context, string liveId)
            {
                var json = await GetLiveInfoJsonAsync(context, liveId);
                var resContainer = JsonSerializerExtensions.Load<NicoliveVideoInfoResponseContainer>(json);
                return resContainer.NicoliveVideoResponse;
            }
        }


        internal static class LiveCommunityVideoSubClient
        {
            private static Task<string> GetLiveCommunityVideoJsonAsync(NiconicoContext context, string communityOrChannelId)
            {
                var dict = new Dictionary<string, string>();
                dict.Add("community_id", communityOrChannelId);
                dict.Add("__format", "json");
                return context.GetStringAsync(NiconicoUrls.CeLiveCommunityVideoApi, dict);
            }

            public static async Task<NicoliveCommunityVideoResponse> GetLiveCommunityVideoAsync(NiconicoContext context, string communityOrChannelId)
            {
                var json = await GetLiveCommunityVideoJsonAsync(context, communityOrChannelId);
                var resContainer = JsonSerializerExtensions.Load<NicoliveCommunityVideoResponseContainer>(json);
                return resContainer.NicoliveVideoResponse;
            }
        }
    }
}
