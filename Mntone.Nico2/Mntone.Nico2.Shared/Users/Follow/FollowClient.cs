using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;



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

			await context.PrepareCorsAsscessAsync(uri);
			return await context.GetJsonAsAsync<FollowUsersResponse>(uri, UserFollowResponseConverter.Settings);
		}

		public static async Task<FollowTagsResponse> GetFollowTagsAsync(NiconicoContext context)
		{
			var uri = $"https://nvapi.nicovideo.jp/v1/users/me/following/tags";			
			await context.PrepareCorsAsscessAsync(uri);
			return await context.GetJsonAsAsync<FollowTagsResponse>(uri);
		}


		public static async Task<FollowMylistResponse> GetFollowMylistAsync(NiconicoContext context, uint sampleItemCount = 3)
		{
			var uri = $"https://nvapi.nicovideo.jp/v1/users/me/following/mylists?sampleItemCount={sampleItemCount}";
			await context.PrepareCorsAsscessAsync(uri);
			return await context.GetJsonAsAsync<FollowMylistResponse>(uri, Mylist.Converter.Settings);
		}


		public static async Task<FollowChannelResponse> GetFollowChannelAsync(NiconicoContext context, uint limit = 25, uint page = 0)
		{
			var uri = $"https://public.api.nicovideo.jp/v1/user/followees/channels.json?limit={limit}&page={page}";
			await context.PrepareCorsAsscessAsync(uri);
			return await context.GetJsonAsAsync<FollowChannelResponse>(uri);
		}

		public static async Task<FollowCommunityResponse> GetFollowCommunityAsync(NiconicoContext context, uint limit = 25, uint page = 0)
		{
			var uri = $"https://public.api.nicovideo.jp/v1/user/followees/communities.json?limit={limit}&page={page}";
			await context.PrepareCorsAsscessAsync(uri);
			return await context.GetJsonAsAsync<FollowCommunityResponse>(uri, FollowCommunityResponseConverter.Settings);
		}








        private static async Task<ChannelFollowApiInfo> GetFollowChannelApiInfo(NiconicoContext context, string channelId)
        {
            var html = await context.GetStringAsync("http://ch.nicovideo.jp/channel/" + channelId);
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
                Params = WebUtility.HtmlDecode(bookmarkAnchorNode.Attributes["params"].Value)
            };
        }

        public static async Task<string> __AddFollowChannelAsync(NiconicoContext context, string channelId)
        {
            var apiInfo = await GetFollowChannelApiInfo(context, channelId);
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
            var apiInfo = await GetFollowChannelApiInfo(context, channelId);
            return await context.GetStringAsync($"{apiInfo.DeleteApi}?{apiInfo.Params}");
        }
        public static Task<ChannelFollowResult> DeleteFollowChannelAsync(NiconicoContext context, string channelId)
        {
            return __DeleteFollowChannelAsync(context, channelId)
                .ContinueWith(prevTask => ParseChannelFollowResult(prevTask.Result));

        }
    }


    internal struct ChannelFollowApiInfo
    {
        public string AddApi { get; set; }
        public string DeleteApi { get; set; }
        public string Params { get; set; }
    }

}
