using Mntone.Nico2.Mylist;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
#if WINDOWS_UWP
using Windows.Web.Http;
#else
using System.Net.Http;
#endif

namespace Mntone.Nico2.Users.Mylist
{
    internal sealed class MylistClient
	{
		public static Task<string> GetMylistGroupDataAsync(NiconicoContext context, string group_id)
		{
			var dict = new Dictionary<string, string>();
			dict.Add(nameof(group_id), group_id);
			return context.PostAsync(NiconicoUrls.MylistGroupGetUrl, dict);
		}

		public class MylistCreateResultResponse
        {
			[JsonProperty("meta")]
			public Meta Meta { get; set; }

			[JsonProperty("data")]
			public MylistCreateResultData Data { get; set; }

			public class MylistCreateResultData
            {
				[JsonProperty("mylistId")]
				public uint MylistId { get; set; }
            }
		}

		public class MylistUpdateResultResponse
		{
			[JsonProperty("meta")]
			public Meta Meta { get; set; }
		}

		public static async Task<string> AddMylistGroupAsync(NiconicoContext context, string name, string description, bool isPublic, MylistSortKey defaultSortKey, MylistSortOrder defaultSortOrder)
		{
			var dict = new Dictionary<string, string>();

			dict.Add("name", name);
			dict.Add("description", description);
			dict.Add("isPublic", isPublic.ToString1Or0());
			dict.Add("defaultSortKey", defaultSortKey.ToQueryString());
			dict.Add("defaultSortOrder", defaultSortOrder.ToQueryString());

			var request = new HttpRequestMessage(HttpMethod.Post, new Uri($"https://nvapi.nicovideo.jp/v1/users/me/mylists"));
#if WINDOWS_UWP
			request.Content = new HttpFormUrlEncodedContent(dict);
			request.Headers.Referer = new Uri("https://www.nicovideo.jp/my/mylist");
#else
			request.Content = new FormUrlEncodedContent(dict);
			request.Headers.Referrer = new Uri($"https://www.nicovideo.jp/my/mylist");
#endif
			request.Headers.Add("X-Request-With", "https://www.nicovideo.jp");

			var res = await context.SendAsync(request);
			var json = await res.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<MylistCreateResultResponse>(json).Data?.MylistId.ToString();
		}


		public static async Task<bool> UpdateMylistGroupAsync(NiconicoContext context, string mylistId, string name, string description, bool isPublic, MylistSortKey defaultSortKey, MylistSortOrder defaultSortOrder)
		{
			var dict = new Dictionary<string, string>();
			dict.Add("name", name);
			dict.Add("description", description);
			dict.Add("isPublic", isPublic.ToString1Or0());
			dict.Add("defaultSortKey", defaultSortKey.ToQueryString());
			dict.Add("defaultSortOrder", defaultSortOrder.ToQueryString());

			var request = new HttpRequestMessage(HttpMethod.Put, new Uri($"https://nvapi.nicovideo.jp/v1/users/me/mylists/{mylistId}"));
#if WINDOWS_UWP
			request.Content = new HttpFormUrlEncodedContent(dict);
			request.Headers.Referer = new Uri($"https://www.nicovideo.jp/my/mylist/{mylistId}");
#else
			request.Content = new FormUrlEncodedContent(dict);
			request.Headers.Referrer = new Uri($"https://www.nicovideo.jp/my/mylist/{mylistId}");
#endif
			request.Headers.Add("X-Request-With", "https://www.nicovideo.jp");


			var res = await context.SendAsync(request);
			var json = await res.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<MylistUpdateResultResponse>(json);
			return result.Meta.Status == 200;
		}


		public static async Task<bool> RemoveMylistGroupAsync(NiconicoContext context, string mylistId)
		{
			var request = new HttpRequestMessage(HttpMethod.Delete, new Uri($"https://nvapi.nicovideo.jp/v1/users/me/mylists/{mylistId}"));
#if WINDOWS_UWP
			request.Headers.Referer = new Uri($"https://www.nicovideo.jp/my/mylist/{mylistId}");
#else
			request.Headers.Referrer = new Uri($"https://www.nicovideo.jp/my/mylist/{mylistId}");
#endif
			request.Headers.Add("X-Request-With", "https://www.nicovideo.jp");

			var res = await context.SendAsync(request);
			var json = await res.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<MylistUpdateResultResponse>(json);
			return result.Meta.Status == 200;
		}


		



		public static Task<string> GetMylistGroupDetailDataAsync(NiconicoContext context, string group_id)
		{
			var dict = new Dictionary<string, string>();
			dict.Add(nameof(group_id), group_id);
			return context.GetStringAsync(NiconicoUrls.MylistGroupDetailApi, dict);
		}


		private static MylistGroupDetail ParseMylistGroupDetailXml(string xml)
		{
			var serializer = new XmlSerializer(typeof(MylistGroupResponse));

			MylistGroupResponse response = null;
			using (var stream = new StringReader(xml))
			{
				response = (MylistGroupResponse)serializer.Deserialize(stream);
			}

			return response.Mylistgroup;
		}




		
		public static async Task<MylistGroupsResponse> GetLoginUserMylistGroupsAsync(NiconicoContext context, int sampleItemsCount = 3)
		{
			return await context.GetJsonAsAsync<MylistGroupsResponse>(@$"https://nvapi.nicovideo.jp/v1/users/me/mylists?sampleItemCount={sampleItemsCount}", Converter.Settings);
		}


		public static async Task<MylistGroupsResponse> GetMylistGroupsAsync(NiconicoContext context, int userId, int sampleItemsCount = 3)
		{
			return await context.GetJsonAsAsync<MylistGroupsResponse>($"https://nvapi.nicovideo.jp/v1/users/{userId}/mylists?sampleItemCount={sampleItemsCount}", Converter.Settings
				, (headers) => 
				{
#if WINDOWS_UWP
					headers.Host = new Windows.Networking.HostName("nvapi.nicovideo.jp");
					headers.Add("Origin", "https://www.nicovideo.jp");
					headers.Referer = new Uri("https://www.nicovideo.jp/user/33969435/mylist?ref=pc_userpage_menu");

#else
					headers.Host = "nvapi.nicovideo.jp";
					headers.TryAddWithoutValidation("Origin", "https://www.nicovideo.jp");
					headers.Referrer = new Uri("https://www.nicovideo.jp/user/33969435/mylist?ref=pc_userpage_menu");
#endif
				});
		}




		public static Task<MylistGroupDetail> GetMylistGroupDetailAsync(NiconicoContext context, string group_id)
		{
			return GetMylistGroupDetailDataAsync(context, group_id)
				.ContinueWith(prevTask => ParseMylistGroupDetailXml(prevTask.Result));
		}



#region Mylist Items


		public static async Task<MylistGroupItemsResponse> GetLoginUserMylistGroupItemsAsync(NiconicoContext context, long mylistId, MylistSortKey sortKey, MylistSortOrder sortOrder, uint pageSize, uint pageCount)
		{
			var uri = $"https://nvapi.nicovideo.jp/v1/users/me/mylists/{mylistId}?sortKey={sortKey.ToQueryString()}&sortOrder={sortOrder.ToQueryString()}&pageSize={pageSize}&page={pageCount + 1}";
			await context.PrepareCorsAsscessAsync(HttpMethod.Get, uri);
			return await context.GetJsonAsAsync<MylistGroupItemsResponse>(uri, Converter.Settings);
		}

		public static async Task<MylistGroupItemsResponse> GetMylistGroupItemsAsync(NiconicoContext context, long mylistId, MylistSortKey sortKey, MylistSortOrder sortOrder, uint pageSize, uint pageCount)
		{
			// Note: CORSのOPTIONSを先に送る奴が必要になるかも
			var uri = $"https://nvapi.nicovideo.jp/v2/mylists/{mylistId}?sortKey={sortKey.ToQueryString()}&sortOrder={sortOrder.ToQueryString()}&pageSize={pageSize}&page={pageCount + 1}";
			await context.PrepareCorsAsscessAsync(HttpMethod.Get, uri);
			return await context.GetJsonAsAsync<MylistGroupItemsResponse>(uri, Converter.Settings);
		}

		public static async Task<ContentManageResult> AddMylistItemAsync(NiconicoContext context, string group_id, NiconicoItemType item_type, string item_id, string description)
		{
			var dict = new Dictionary<string, string>();

			var mylistToken = await context.GetMylistToken(group_id, item_id);

			dict.Add(nameof(group_id), group_id);
			dict.Add(nameof(item_type), ((uint)item_type).ToString());
			dict.Add(nameof(item_id), mylistToken.ItemId);
			if (mylistToken.Values.ContainsKey("item_amc"))
			{
				dict.Add("item_amc", mylistToken.ItemAmc);
			}
			dict.Add(nameof(description), description);
			dict.Add("token", mylistToken.Token);

			var json = await context.PostAsync(NiconicoUrls.MylistAddUrl, dict, withToken: false);
			return ContentManagerResultHelper.ParseJsonResult(json);
		}


		public static async Task<ContentManageResult> UpdateMylistItemAsync(NiconicoContext context, string group_id, NiconicoItemType item_type, string item_id, string description)
		{
			var dict = new Dictionary<string, string>();

			dict.Add(nameof(group_id), group_id);
			dict.Add(nameof(item_type), ((uint)item_type).ToString());
			dict.Add(nameof(item_id), item_id);
			dict.Add(nameof(description), description);

			var json = await context.PostAsync(NiconicoUrls.MylistUpdateUrl, dict);

			return ContentManagerResultHelper.ParseJsonResult(json);
		}

		public sealed class MylistContentResult
        {
			[DataMember(Name="meta")]
			public MylistContentResultMeta Meta { get; set; }
        }

		public sealed class MylistContentResultMeta
        {
			[DataMember(Name ="status")]
			public int Status { get; set; }
        }

		public static async Task<ContentManageResult> RemoveMylistItemAsync(NiconicoContext context, string group_id, params string[] itemIdList)
		{
			string url = $"https://nvapi.nicovideo.jp/v1/users/me/mylists/{group_id}/items?itemIds=" + string.Join("%2C", itemIdList);

			var res = await context.SendAsync(new HttpRequestMessage(HttpMethod.Delete, new Uri(url)));
			var json = await res.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<MylistContentResult>(json);
			return result.Meta.Status == 200 ? ContentManageResult.Success : ContentManageResult.Failed;
		}

		public static async Task<ContentManageResult> CopyMylistItemAsync(NiconicoContext context, string group_id, string target_group_id, params string[] itemIdList)
		{
			var url = $"https://nvapi.nicovideo.jp/v1/users/me/copy-mylist-items?from={group_id}&to={target_group_id}&itemIds={string.Join("%2C", itemIdList)}";

			var json = await context.PostAsync(url);
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<MylistContentResult>(json);
			return result.Meta.Status == 200 ? ContentManageResult.Success : ContentManageResult.Failed;
		}


		public static async Task<ContentManageResult> MoveMylistItemAsync(NiconicoContext context, string group_id, string target_group_id, params string[] itemIdList)
		{
			var url = $"https://nvapi.nicovideo.jp/v1/users/me/move-mylist-items?from={group_id}&to={target_group_id}&itemIds={string.Join("%2C", itemIdList)}";

			var json = await context.PostAsync(url);
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<MylistContentResult>(json);
			return result.Meta.Status == 200 ? ContentManageResult.Success : ContentManageResult.Failed;
		}

		#endregion


		#region WatchAfter


		public static async Task<WatchAfterMylistGroupItemsResponse> GetWatchAfterMylistGroupItemsAsync(NiconicoContext context, MylistSortKey sortKey, MylistSortOrder sortOrder, uint pageSize, uint pageCount)
		{
			var uri = $"https://nvapi.nicovideo.jp/v1/users/me/watch-later?sortKey={sortKey.ToQueryString()}&sortOrder={sortOrder.ToQueryString()}&pageSize={pageSize}&page={pageCount + 1}";
			await context.PrepareCorsAsscessAsync(HttpMethod.Get, uri);
			return await context.GetJsonAsAsync<WatchAfterMylistGroupItemsResponse>(uri, Converter.Settings,
				haaders => 
				{
#if WINDOWS_UWP
					haaders.Referer = new Uri("https://www.nicovideo.jp/my/watchlater?ref=pc_mypage_menu");
#else
					haaders.Referrer = new Uri("https://www.nicovideo.jp/my/watchlater?ref=pc_mypage_menu");
#endif
				});
		}


		public static async Task<ContentManageResult> AddDeflistAsync(NiconicoContext context, NiconicoItemType item_type, string item_id, string description)
		{
			var dict = new Dictionary<string, string>();

			var group_id = "default";
			var mylistToken = await context.GetMylistToken(group_id, item_id);

			dict.Add(nameof(group_id), group_id);
			dict.Add(nameof(item_type), ((uint)item_type).ToString());
			dict.Add(nameof(item_id), mylistToken.ItemId);
			dict.Add(nameof(description), description);
			dict.Add("token", mylistToken.Token);

			var json = await context.PostAsync(NiconicoUrls.MylistDeflistAddUrl, dict, withToken: false);
			return ContentManagerResultHelper.ParseJsonResult(json);
		}


		public static async Task<ContentManageResult> UpdateDeflistAsync(NiconicoContext context, NiconicoItemType item_type, string item_id, string description)
		{
			var dict = new Dictionary<string, string>();

			dict.Add(nameof(item_id), item_id);

			if (item_type != NiconicoItemType.Video)
			{
				dict.Add(nameof(item_type), ((uint)item_type).ToString());
			}
			if (!string.IsNullOrWhiteSpace(description))
			{
				dict.Add(nameof(description), description);
			}

			var json = await context.PostAsync(NiconicoUrls.MylistDeflistUpdateUrl, dict);
			return ContentManagerResultHelper.ParseJsonResult(json);
		}

		public static async Task<ContentManageResult> RemoveDeflistAsync(NiconicoContext context, params string[] itemIdList)
		{
			string url = $"https://nvapi.nicovideo.jp/v1/users/me/deflist/items?itemIds=" + string.Join("%2C", itemIdList);

			var req = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
			var res = await context.SendAsync(req);
			var json = await res.Content.ReadAsStringAsync();
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<MylistContentResult>(json);
			return result.Meta.Status == 200 ? ContentManageResult.Success : ContentManageResult.Failed;
		}

		public static async Task<ContentManageResult> MoveDeflistAsync(NiconicoContext context, string target_group_id, params string[] itemIdList)
		{
			var url = $"https://nvapi.nicovideo.jp/v1/users/me/move-mylist-items?from=deflist&to={target_group_id}&itemIds={string.Join("%2C", itemIdList)}";

			var json = await context.PostAsync(url);
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<MylistContentResult>(json);
			return result.Meta.Status == 200 ? ContentManageResult.Success : ContentManageResult.Failed;
		}


		public static async Task<ContentManageResult> CopyDeflistAsync(NiconicoContext context, string target_group_id, params string[] itemIdList)
		{
			var url = $"https://nvapi.nicovideo.jp/v1/users/me/copy-mylist-items?from=deflist&to={target_group_id}&itemIds={string.Join("%2C", itemIdList)}";

			var json = await context.PostAsync(url);
			var result = Newtonsoft.Json.JsonConvert.DeserializeObject<MylistContentResult>(json);
			return result.Meta.Status == 200 ? ContentManageResult.Success : ContentManageResult.Failed;
		}


		#endregion
	}



}
