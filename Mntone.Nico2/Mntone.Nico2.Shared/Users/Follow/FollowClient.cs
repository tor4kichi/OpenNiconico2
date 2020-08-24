using HtmlAgilityPack;
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


		public static async Task<FollowChannelResponse> GetFollowChannelAsync(NiconicoContext context, uint limit = 25, uint page = 0)
		{
			var uri = $"https://public.api.nicovideo.jp/v1/user/followees/channels.json?limit={limit}&page={page}";
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


        public static Task<ContentManageResult> AddFollowTagAsync(NiconicoContext context, string tag)
        {
            return AddFollowInternalAsync(context, $"https://nvapi.nicovideo.jp/v1/users/me/following/tags?tag={tag}");
        }

        public static Task<ContentManageResult> RemoveFollowTagAsync(NiconicoContext context, string tag)
        {
            return RemoveFollowInternalAsync(context, $"https://nvapi.nicovideo.jp/v1/users/me/following/tags?tag={tag}");
        }



        public static Task<ContentManageResult> AddFollowMylistAsync(NiconicoContext context, string mylistId)
        {
            return AddFollowInternalAsync(context, $"https://nvapi.nicovideo.jp/v1/users/me/following/mylists/{mylistId}");
        }

        public static Task<ContentManageResult> RemoveFollowMylistAsync(NiconicoContext context, string mylistId)
        {
            return RemoveFollowInternalAsync(context, $"https://nvapi.nicovideo.jp/v1/users/me/following/mylists/{mylistId}");
        }




        #region Community



        public static async Task<ContentManageResult> AddFollowCommunityAsync(NiconicoContext context, string communityId)
        {
            var communityIdWoCo = communityId.Substring(2);
            var communityJoinPageUrl = new Uri($"https://com.nicovideo.jp/motion/{communityId}");

            var uri = $"https://com.nicovideo.jp/api/v1/communities/{communityIdWoCo}/follows.json";
            //            await PrepareCorsAsscessAsync(HttpMethod.Post, uri);

            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(uri));
            request.Content = new HttpFormUrlEncodedContent(new Dictionary<string, string>());
            
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


    internal struct ChannelFollowApiInfo
    {
        public string AddApi { get; set; }
        public string DeleteApi { get; set; }
        public string Params { get; set; }
    }

}
