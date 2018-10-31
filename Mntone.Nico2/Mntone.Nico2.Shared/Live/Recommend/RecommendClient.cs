using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Live.Recommend
{
    internal sealed class RecommendClient
    {
        // Note: 放送者によって取得するAPIが異なる
        // 「公式・チャンネル」と「コミュニティ」
        // コミュニティ放送の場合だけコミュニティIDが必要で
        // コミュニティ放送の生放送IDを間違って公式・チャンネル側のAPIに使うと
        // 何もリコメンドが得られない（jsonの空配列が返ってくる）

        // コミュニティ向けのAPIはコミュニティIDと生放送IDの両方が必須で
        // 正しく指定しなかった場合は エラーコード 400 が返される

        // データ構造は両方とも共通している



        public static Task<string> GetOfficialOrChannelLiveRecommendJsonAsync(NiconicoContext context, string liveId)
        {
            if (!liveId.StartsWith("lv"))
            {
                throw new ArgumentException($"liveId is must StartWith \"lv\".");
            }

            var dict = new Dictionary<string, string>();
            dict.Add("video_id", liveId);

            return context.GetStringAsync("http://live.nicovideo.jp/api/video.recommendation", dict);
        }


        public static Task<string> GetCommunityLiveRecommendJsonAsync(NiconicoContext context, string liveId, string communityId)
        {
            if (!communityId.StartsWith("co"))
            {
                throw new ArgumentException($"communityId is must StartWith \"co\".");
            }

            if (!liveId.StartsWith("lv"))
            {
                throw new ArgumentException($"liveId is must StartWith \"lv\".");
            }

            var dict = new Dictionary<string, string>();
            dict.Add("community_id", communityId);
            dict.Add("video_id", liveId);

            return context.GetStringAsync("http://live.nicovideo.jp/api/community.recommendation", dict);
        }


        private static LiveRecommendResponse ParseLiveRecommendJson(string json)
        {
            return new LiveRecommendResponse()
            {
                _RecommendItems = JsonSerializerExtensions.Load<List<LiveRecommendData>>(json)
            };
        }

        public static Task<LiveRecommendResponse> GetOfficialOrChannelLiveRecommendAsync(NiconicoContext context, string liveId)
        {
            return GetOfficialOrChannelLiveRecommendJsonAsync(context, liveId)
                .ContinueWith(prevTask => ParseLiveRecommendJson(prevTask.Result));
        }

        public static Task<LiveRecommendResponse> GetCommunityLiveRecommendAsync(NiconicoContext context, string liveId, string communityId)
        {
            return GetCommunityLiveRecommendJsonAsync(context, liveId, communityId)
                .ContinueWith(prevTask => ParseLiveRecommendJson(prevTask.Result));
        }
    }
}
