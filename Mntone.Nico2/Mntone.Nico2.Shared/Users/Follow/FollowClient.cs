using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

#if WINDOWS_UWP
using Windows.Web;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using Windows.Web.Http.Headers;
#else
using System.Net;
using System.Net.Http;
#endif

namespace Mntone.Nico2.Users.Follow
{
	// お気に入りAPIのクライアント


    internal sealed class FollowClient
    {
		public static async Task<FollowUsersResponse> GetFollowUsersAsync(NiconicoContext context, uint pageSize, FollowUsersResponse lastUserResponse)
		{
			var uri = $"https://nvapi.nicovideo.jp/v1/users/me/following/users?pageSize={pageSize}";
			if (lastUserResponse != null)
            {
				uri += "&cursor=" + lastUserResponse.Data.Summary.Cursor;
			}

			await context.PrepareCorsAsscessAsync(HttpMethod.Get, uri);
			return await context.GetJsonAsAsync<FollowUsersResponse>(uri, UserFollowResponseConverter.Settings);
		}

		public static async Task<FollowTagsResponse> GetFollowTagsAsync(NiconicoContext context)
		{
			var uri = $"https://nvapi.nicovideo.jp/v1/users/me/following/tags";			
			await context.PrepareCorsAsscessAsync(HttpMethod.Get, uri);
			return await context.GetJsonAsAsync<FollowTagsResponse>(uri);
		}


		public static async Task<FollowMylistResponse> GetFollowMylistAsync(NiconicoContext context, uint sampleItemCount = 3)
		{
			var uri = $"https://nvapi.nicovideo.jp/v1/users/me/following/mylists?sampleItemCount={sampleItemCount}";
			await context.PrepareCorsAsscessAsync(HttpMethod.Get, uri);
			return await context.GetJsonAsAsync<FollowMylistResponse>(uri, Mylist.Converter.Settings);
		}


		public static async Task<FollowChannelResponse> GetFollowChannelAsync(NiconicoContext context, uint limit = 25, uint offset = 0)
		{
			var uri = $"https://public.api.nicovideo.jp/v1/user/followees/channels.json?limit={limit}&offset={offset}";
			await context.PrepareCorsAsscessAsync(HttpMethod.Get, uri);
			return await context.GetJsonAsAsync<FollowChannelResponse>(uri);
		}

		public static async Task<FollowCommunityResponse> GetFollowCommunityAsync(NiconicoContext context, uint limit = 25, uint page = 0)
		{
			var uri = $"https://public.api.nicovideo.jp/v1/user/followees/communities.json?limit={limit}&page={page}";
			await context.PrepareCorsAsscessAsync(HttpMethod.Get, uri);
			return await context.GetJsonAsAsync<FollowCommunityResponse>(uri, FollowCommunityResponseConverter.Settings);
		}

        public partial class FollowResultResponse
        {
            [Newtonsoft.Json.JsonProperty("meta")]
            public Meta Meta { get; set; }
        }

        public class FollowedResultResponce
        {
            [Newtonsoft.Json.JsonProperty("meta")]
            public Meta Meta { get; set; }

            [Newtonsoft.Json.JsonProperty("data")]
            public FollowedData Data { get; set; }
        }

        public class FollowedData
        {
            [Newtonsoft.Json.JsonProperty("following")]
            public bool IsFollowing { get; set; }
        }


        private static async Task<bool> GetFollowedInternalAsync(NiconicoContext context, string uri)
        {
            var result = await context.GetJsonAsAsync<FollowedResultResponce>(HttpMethod.Get, uri, headerModifier: headers =>
            {
                headers.Add("X-Request-With", "https://www.nicovideo.jp/my/follow");
            });
            return result.Data.IsFollowing;
        }

        private static async Task<ContentManageResult> AddFollowInternalAsync(NiconicoContext context, string uri)
        {
            await context.PrepareCorsAsscessAsync(HttpMethod.Post, uri);
            var result = await context.GetJsonAsAsync<FollowResultResponse>(HttpMethod.Post, uri, headerModifier: headers =>
            {
                headers.Add("X-Request-With", "https://www.nicovideo.jp/my/follow");
            });
            return result.Meta.Status == 200 || result.Meta.Status == 201 ? ContentManageResult.Success : ContentManageResult.Failed;
        }

        private static async Task<ContentManageResult> RemoveFollowInternalAsync(NiconicoContext context, string uri)
        {
            await context.PrepareCorsAsscessAsync(HttpMethod.Delete, uri);
            var result = await context.GetJsonAsAsync<FollowResultResponse>(HttpMethod.Delete, uri, headerModifier: headers => 
            {
                headers.Add("X-Request-With", "https://www.nicovideo.jp/my/follow");
            });
            return result.Meta.Status == 200 ? ContentManageResult.Success : ContentManageResult.Failed;
        }



        public static Task<ContentManageResult> AddFollowUserAsync(NiconicoContext context, string userId)
        {
            return AddFollowInternalAsync(context, $"https://public.api.nicovideo.jp/v1/user/followees/niconico-users/{userId}.json");
        }

        public static Task<ContentManageResult> RemoveFollowUserAsync(NiconicoContext context, string userId)
        {
            return RemoveFollowInternalAsync(context, $"https://public.api.nicovideo.jp/v1/user/followees/niconico-users/{userId}.json");
        }

        public static Task<bool> IsFollowingUserAsync(NiconicoContext context, uint userId)
        {
            return GetFollowedInternalAsync(context, $"https://public.api.nicovideo.jp/v1/user/followees/niconico-users/{userId}.json");
        }



        public static Task<ContentManageResult> AddFollowTagAsync(NiconicoContext context, string tag)
        {
            return AddFollowInternalAsync(context, $"https://nvapi.nicovideo.jp/v1/users/me/following/tags?tag={tag}");
        }

        public static Task<ContentManageResult> RemoveFollowTagAsync(NiconicoContext context, string tag)
        {
            return RemoveFollowInternalAsync(context, $"https://nvapi.nicovideo.jp/v1/users/me/following/tags?tag={tag}");
        }

        /*
        public static Task<bool> IsFollowingTagAsync(NiconicoContext context, string tag)
        {
            return GetFollowedInternalAsync(context, $"https://nvapi.nicovideo.jp/v1/users/me/following/tags?tag={Uri.EscapeDataString(tag)}");
        }
        */



        public static Task<ContentManageResult> AddFollowMylistAsync(NiconicoContext context, string mylistId)
        {
            return AddFollowInternalAsync(context, $"https://nvapi.nicovideo.jp/v1/users/me/following/mylists/{mylistId}");
        }

        public static Task<ContentManageResult> RemoveFollowMylistAsync(NiconicoContext context, string mylistId)
        {
            return RemoveFollowInternalAsync(context, $"https://nvapi.nicovideo.jp/v1/users/me/following/mylists/{mylistId}");
        }

        /*
        public static Task<bool> IsFollowingMylistAsync(NiconicoContext context, string mylistId)
        {
            return GetFollowedInternalAsync(context, $"https://nvapi.nicovideo.jp/v1/users/me/following/mylists/{mylistId}");
        }
        */




        #region Community

        public static Task<UserOwnedCommunityResponse> GetUserOwnedCommunitiesAsync(NiconicoContext context, uint userId)
        {
            return context.GetJsonAsAsync<UserOwnedCommunityResponse>($"https://public.api.nicovideo.jp/v1/user/{userId}/communities.json");
        }


        public static async Task<CommunityAuthorityResponse> GetCommunityAuthorityAsync(NiconicoContext context, string communityId)
        {
            var communityIdWoCo = communityId.Substring(2);
            var communityJoinPageUrl = new Uri($"https://com.nicovideo.jp/motion/{communityId}");

            return await context.GetJsonAsAsync<CommunityAuthorityResponse>($"https://com.nicovideo.jp/api/v1/communities/{communityIdWoCo}/authority.json");
        }

        public static async Task<ContentManageResult> AddFollowCommunityAsync(NiconicoContext context, string communityId)
        {
            var communityIdWoCo = communityId.Substring(2);
            var communityJoinPageUrl = new Uri($"https://com.nicovideo.jp/motion/{communityId}");

            var uri = $"https://com.nicovideo.jp/api/v1/communities/{communityIdWoCo}/follows.json";
            //            await PrepareCorsAsscessAsync(HttpMethod.Post, uri);

            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(uri));
            
#if WINDOWS_UWP
            request.Headers.Referer = communityJoinPageUrl;
            request.Headers.Host = new Windows.Networking.HostName("com.nicovideo.jp");
#else
            request.Headers.Referrer = communityJoinPageUrl;
            request.Headers.Host = "com.nicovideo.jp";
#endif
            request.Headers.Add("Origin", "https://com.nicovideo.jp");
            request.Headers.Add("X-Requested-By", communityJoinPageUrl.OriginalString);

            var message = await context.SendAsync(request);
            var json = await message.Content.ReadAsStringAsync();
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<FollowResultResponse>(json);


            return result.Meta.Status == 200|| result.Meta.Status == 201 ? ContentManageResult.Success : ContentManageResult.Failed;
        }


        // Communityへの登録
        // http://com.nicovideo.jp/motion/co1894176
        // mode:commit
        // title:登録申請
        // comment:
        // notify:
        // POST
        public static async Task<ContentManageResult> AddFollowCommunityAsync___(
            NiconicoContext context
            , string communityId
            , string title = ""
            , string comment = ""
            , bool notify = false
            )
        {
            // http://com.nicovideo.jp/motion/id にアクセスして200かを確認

            var url = NiconicoUrls.CommunityJoinPageUrl + communityId;
            var res = await context.GetAsync(url);

            if (!res.IsSuccessStatusCode)
            {
                return ContentManageResult.Failed;
            }

            await Task.Delay(1000);

            // http://com.nicovideo.jp/motion/id に情報をpostする
            var dict = new Dictionary<string, string>()
            {
                { "mode", "commit" },
                { "title", title ?? "フォローリクエスト"},
                { "comment", comment ?? ""},
                { "notify", notify ? "1" : ""},
            };

            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("Referer", url);
            request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

#if WINDOWS_UWP
            request.Content = new HttpFormUrlEncodedContent(dict);
#else
            request.Content = new FormUrlEncodedContent(dict);
#endif

            var postResult = await context.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            Debug.WriteLine(postResult);

            return postResult.IsSuccessStatusCode ? ContentManageResult.Success : ContentManageResult.Failed;
        }


        // 成功すると 200 
        // http://com.nicovideo.jp/motion/co2128730/done
        // にリダイレクトされる

        // 失敗すると
        // 404

        // 申請に許可が必要な場合は未調査


        // Communityからの登録解除
        // http://com.nicovideo.jp/leave/co2128730
        // にアクセスして、フォームから timeとcommit_keyを抽出して
        // time: UNIX_TIME
        // commit_key
        // commit
        // http://com.nicovideo.jp/leave/co2128730 にPOSTする
        // 成功したら200、失敗したら404

        private static async Task<CommunityLeaveToken> GetCommunityLeaveTokenAsync(NiconicoContext context, string communityId)
        {
            var url = NiconicoUrls.CommunityLeavePageUrl + communityId;
            var htmlText = await context.GetStringAsync(url);

            CommunityLeaveToken leaveToken = new CommunityLeaveToken()
            {
                CommunityId = communityId
            };

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(htmlText);

            var rootNode = document.DocumentNode;
            var hiddenInputs = rootNode.SelectNodes("//main//input");

            foreach (var hiddenInput in hiddenInputs)
            {
                var nameAttr = hiddenInput.GetAttributeValue("name", "");
                if (nameAttr == "time")
                {
                    var timeValue = hiddenInput.GetAttributeValue("value", "");
                    leaveToken.Time = timeValue;
                }
                else if (nameAttr == "commit_key")
                {
                    var commit_key = hiddenInput.GetAttributeValue("value", "");
                    leaveToken.CommitKey = commit_key;
                }
                else if (nameAttr == "commit")
                {
                    var commit = hiddenInput.GetAttributeValue("value", "");
                    leaveToken.Commit = commit;
                }
            }

            return leaveToken;
        }

        public static async Task<ContentManageResult> RemoveFollowCommunityAsync(NiconicoContext context, string communityId)
        {
            var token = await GetCommunityLeaveTokenAsync(context, communityId);
            var url = NiconicoUrls.CommunityLeavePageUrl + token.CommunityId;
            var dict = new Dictionary<string, string>();
            dict.Add("time", token.Time);
            dict.Add("commit_key", token.CommitKey);
            dict.Add("commit", token.Commit);

#if WINDOWS_UWP
            var content = new HttpFormUrlEncodedContent(dict);
#else
            var content = new FormUrlEncodedContent(dict);
#endif


            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(url));
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("Referer", url);
            request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");

            request.Content = content;
            var postResult = await context.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            Debug.WriteLine(postResult);

            return postResult.IsSuccessStatusCode ? ContentManageResult.Success : ContentManageResult.Failed;
        }

        public class CommunityLeaveToken
        {
            public string CommunityId { get; set; }
            public string Time { get; set; }
            public string CommitKey { get; set; }
            public string Commit { get; set; }
        }

        #endregion


        #region Channel


        public static async Task<ChannelAuthorityResponse> GetChannelAuthorityAsync(NiconicoContext context, uint channelNumberId)
        {
            return await context.GetJsonAsAsync<ChannelAuthorityResponse>($"https://public.api.nicovideo.jp/v1/channel/channelapp/channels/{channelNumberId}.json");
        }



        private static async Task<ChannelFollowApiInfo> GetFollowChannelApiInfo(NiconicoContext context, string channelScreenName)
        {
            var html = await context.GetStringAsync("http://ch.nicovideo.jp/" + channelScreenName);
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var bookmarkAnchorNode = document.DocumentNode.Descendants("a").Single(x =>
            {
                if (x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("bookmark"))
                {
                    return x.Attributes["class"].Value.Split(' ').FirstOrDefault() == "bookmark";
                }
                else
                {
                    return false;
                }
            });

            return new ChannelFollowApiInfo()
            {
                AddApi = bookmarkAnchorNode.Attributes["api_add"].Value,
                DeleteApi = bookmarkAnchorNode.Attributes["api_delete"].Value,
                Params = System.Net.WebUtility.HtmlDecode(bookmarkAnchorNode.Attributes["params"].Value)
            };
        }

        public static async Task<string> __AddFollowChannelAsync(NiconicoContext context, string channelId)
        {
            var info = await context.Channel.GetChannelInfo(channelId);
            var apiInfo = await GetFollowChannelApiInfo(context, info.ScreenName);
            return await context.GetStringAsync($"{apiInfo.AddApi}?{apiInfo.Params}");
        }

        public static ChannelFollowResult ParseChannelFollowResult(string json)
        {
            return JsonSerializerExtensions.Load<ChannelFollowResult>(json);
        }

        public static Task<ChannelFollowResult> AddFollowChannelAsync(NiconicoContext context, string channelId)
        {
            return __AddFollowChannelAsync(context, channelId)
                .ContinueWith(prevTask => ParseChannelFollowResult(prevTask.Result));
        }


        public static async Task<string> __DeleteFollowChannelAsync(NiconicoContext context, string channelId)
        {
            var info = await context.Channel.GetChannelInfo(channelId);
            var apiInfo = await GetFollowChannelApiInfo(context, info.ScreenName);
            return await context.GetStringAsync($"{apiInfo.DeleteApi}?{apiInfo.Params}");
        }
        public static Task<ChannelFollowResult> DeleteFollowChannelAsync(NiconicoContext context, string channelId)
        {
            return __DeleteFollowChannelAsync(context, channelId)
                .ContinueWith(prevTask => ParseChannelFollowResult(prevTask.Result));

        }

#endregion



    }


    public sealed class UserOwnedCommunityResponse
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public UserOwnedCommunity Data { get; set; }
    }

    public sealed class UserOwnedCommunity
    {
        [JsonProperty("owned")]
        public List<FollowCommunityResponse.FollowCommunity> OwnedCommunities { get; set; }
    }


    public class CommunityAuthorityResponse
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public CommunityAuthority Data { get; set; }
    }

    public class CommunityAuthority
    {
        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("is_owner")]
        public bool IsOwner { get; set; }

        [JsonProperty("is_member")]
        public bool IsMember { get; set; }

        [JsonProperty("can_post_content")]
        public bool CanPostContent { get; set; }
    }


    internal struct ChannelFollowApiInfo
    {
        public string AddApi { get; set; }
        public string DeleteApi { get; set; }
        public string Params { get; set; }
    }


    public partial class ChannelAuthorityResponse
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("session")]
        public Session Session { get; set; }

        [JsonProperty("hasVideo")]
        public bool HasVideo { get; set; }

        [JsonProperty("hasLive")]
        public bool HasLive { get; set; }

        [JsonProperty("hasOfficialLive")]
        public bool HasOfficialLive { get; set; }

        [JsonProperty("hasBlog")]
        public bool HasBlog { get; set; }

        [JsonProperty("hasEvent")]
        public bool HasEvent { get; set; }

        [JsonProperty("hasTwitter")]
        public bool HasTwitter { get; set; }

        [JsonProperty("hasYoutube")]
        public bool HasYoutube { get; set; }

        [JsonProperty("hasRss")]
        public bool HasRss { get; set; }

        [JsonProperty("hasSpecialContent")]
        public bool HasSpecialContent { get; set; }

        [JsonProperty("defaultContent")]
        public string DefaultContent { get; set; }

        [JsonProperty("channel")]
        public Channel Channel { get; set; }

        [JsonProperty("video")]
        public OfficialLive Video { get; set; }

        [JsonProperty("officialLive")]
        public OfficialLive OfficialLive { get; set; }
    }

    public partial class Channel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("descriptionHtml")]
        public string DescriptionHtml { get; set; }

        [JsonProperty("isFree")]
        public bool IsFree { get; set; }

        [JsonProperty("screenName")]
        public string ScreenName { get; set; }

        [JsonProperty("ownerName")]
        public string OwnerName { get; set; }

        [JsonProperty("price")]
        public long Price { get; set; }

        [JsonProperty("bodyPrice")]
        public long BodyPrice { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("thumbnailUrl")]
        public Uri ThumbnailUrl { get; set; }

        [JsonProperty("thumbnailSmallUrl")]
        public Uri ThumbnailSmallUrl { get; set; }

        [JsonProperty("canAdmit")]
        public bool CanAdmit { get; set; }

        [JsonProperty("isAdult")]
        public bool IsAdult { get; set; }

        [JsonProperty("lastPublishedAt")]
        public DateTimeOffset LastPublishedAt { get; set; }

        [JsonProperty("backgroundImage")]
        public BackgroundImage BackgroundImage { get; set; }

        [JsonProperty("rss")]
        public object[] Rss { get; set; }

        [JsonProperty("officialLiveTags")]
        public OfficialLiveTag[] OfficialLiveTags { get; set; }
    }

    public partial class BackgroundImage
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("repeatFlag")]
        public long RepeatFlag { get; set; }
    }

    public partial class OfficialLiveTag
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public partial class OfficialLive
    {
        [JsonProperty("lastPublishedAt")]
        public DateTimeOffset LastPublishedAt { get; set; }
    }

    public partial class Session
    {
        [JsonProperty("hasContentsAuthority")]
        public bool HasContentsAuthority { get; set; }

        [JsonProperty("isJoining")]
        public bool IsJoining { get; set; }

        [JsonProperty("isFollowing")]
        public bool IsFollowing { get; set; }

        [JsonProperty("subscribingTopics")]
        public object[] SubscribingTopics { get; set; }
    }

}
