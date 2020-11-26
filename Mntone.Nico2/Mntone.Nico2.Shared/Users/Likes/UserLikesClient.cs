using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


#if WINDOWS_UWP
using Windows.Web.Http;
#else
using System.Net.Http;
#endif

namespace Mntone.Nico2.Users.Likes
{
    public static class UserLikesClient
    {

        public const string LikeApiUrl = "https://nvapi.nicovideo.jp/v1/users/me/likes/items?videoId=";

        public static async Task<LikeActionResponse> DoLikeVideoAsync(NiconicoContext context, string videoId)
        {
            return await context.GetJsonAsAsync<LikeActionResponse>(httpMethod: HttpMethod.Post,  LikeApiUrl + videoId);
        }

        public static async Task<LikeActionResponse> UnDoLikeVideoAsync(NiconicoContext context, string videoId)
        {
            return await context.GetJsonAsAsync<LikeActionResponse>(httpMethod: HttpMethod.Delete, LikeApiUrl + videoId);
        }



        public const string LikeListupApiUrl = "https://nvapi.nicovideo.jp/v1/users/me/likes?";

        public static async Task<LikesListResponse> GetLikesAsync(NiconicoContext context, int page, int pageSize)
        {
            return await context.GetJsonAsAsync<LikesListResponse>($"{LikeListupApiUrl}pageSize={pageSize}&page={page + 1}");
        }
    }

    public sealed class LikeActionResponse
    {
        [DataMember(Name = "data")]
        public LikesResponseData Data { get; set; }

        [DataMember(Name = "meta")]
        public LikesResponseMeta Meta { get; set; }


        public bool IsOK => Meta?.Status == 201 || Meta?.Status == 200;

        public string ThanksMessage => Data?.ThanksMessage;

        public sealed class LikesResponseData
        {
            [DataMember(Name = "thanksMessage")]
            public string ThanksMessage { get; set; }
        }

        public sealed class LikesResponseMeta
        {
            [DataMember(Name = "status")]
            public int Status { get; set; }
        }
    }

    public sealed class LikesListResponse
    {
        [DataMember(Name = "data")]
        public LikesListResponseData Data { get; set; }

        [DataMember(Name = "meta")]
        public LikesListResponseMeta Meta { get; set; }


        public bool IsOK => Meta?.Status == 201 || Meta?.Status == 200;

        public partial class LikesListResponseData
        {
            [DataMember(Name = "items")]
            public Item[] Items { get; set; }

            [DataMember(Name = "summary")]
            public Summary Summary { get; set; }
        }

        public partial class Item
        {
            [DataMember(Name = "likedAt")]
            public DateTimeOffset LikedAt { get; set; }

            [DataMember(Name = "thanksMessage")]
            public string ThanksMessage { get; set; }

            [DataMember(Name = "video")]
            public Video Video { get; set; }

            [DataMember(Name = "status")]
            public string Status { get; set; }
        }

        public partial class Video
        {
            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "id")]
            public string Id { get; set; }

            [DataMember(Name = "title")]
            public string Title { get; set; }

            [DataMember(Name = "registeredAt")]
            public DateTimeOffset RegisteredAt { get; set; }

            [DataMember(Name = "count")]
            public Count Count { get; set; }

            [DataMember(Name = "thumbnail")]
            public Thumbnail Thumbnail { get; set; }

            [DataMember(Name = "duration")]
            public long Duration { get; set; }

            [DataMember(Name = "shortDescription")]
            public string ShortDescription { get; set; }

            [DataMember(Name = "latestCommentSummary")]
            public string LatestCommentSummary { get; set; }

            [DataMember(Name = "isChannelVideo")]
            public bool IsChannelVideo { get; set; }

            [DataMember(Name = "isPaymentRequired")]
            public bool IsPaymentRequired { get; set; }

            [DataMember(Name = "playbackPosition")]
            public long? PlaybackPosition { get; set; }

            [DataMember(Name = "owner")]
            public Owner Owner { get; set; }

            [DataMember(Name = "9d091f87")]
            public bool The9D091F87 { get; set; }

            [DataMember(Name = "acf68865")]
            public bool Acf68865 { get; set; }
        }

        public partial class Count
        {
            [DataMember(Name = "view")]
            public long View { get; set; }

            [DataMember(Name = "comment")]
            public long Comment { get; set; }

            [DataMember(Name = "mylist")]
            public long Mylist { get; set; }
        }

        public partial class Owner
        {
            [DataMember(Name = "ownerType")]
            public string OwnerType { get; set; }

            [DataMember(Name = "id")]
            public string Id { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "iconUrl")]
            public Uri IconUrl { get; set; }
        }

        public partial class Thumbnail
        {
            [DataMember(Name = "url")]
            public Uri Url { get; set; }

            [DataMember(Name = "middleUrl")]
            public Uri MiddleUrl { get; set; }

            [DataMember(Name = "largeUrl")]
            public Uri LargeUrl { get; set; }

            [DataMember(Name = "listingUrl")]
            public Uri ListingUrl { get; set; }

            [DataMember(Name = "nHdUrl")]
            public Uri NHdUrl { get; set; }
        }

        public partial class Summary
        {
            [DataMember(Name = "hasNext")]
            public bool HasNext { get; set; }

            [DataMember(Name = "canGetNextPage")]
            public bool CanGetNextPage { get; set; }

            [DataMember(Name = "getNextPageNgReason")]
            public string GetNextPageNgReason { get; set; }
        }



        public sealed class LikesListResponseMeta
        {
            [DataMember(Name = "status")]
            public int Status { get; set; }
        }
    }
}
