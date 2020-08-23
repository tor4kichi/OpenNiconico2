﻿using Mntone.Nico2.Mylist;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
#if WINDOWS_UWP
using Windows.Web.Http;
#else
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

		public static Task<string> AddMylistGroupDataAsync(NiconicoContext context, string name, string description, bool is_public, MylistDefaultSort default_sort, IconType icon_id)
		{
			var dict = new Dictionary<string, string>();

			dict.Add(nameof(name), name);
			dict.Add(nameof(description), description);
			dict.Add(nameof(is_public), is_public.ToString1Or0());
			dict.Add(nameof(default_sort), ((uint)default_sort).ToString());
			dict.Add(nameof(icon_id), ((uint)icon_id).ToString());

			return context.PostAsync(NiconicoUrls.MylistGroupAddUrl, dict);
		}


		public static Task<string> UpdateMylistGroupDataAsync(NiconicoContext context, string group_id, string name, string description, bool is_public, MylistDefaultSort default_sort, IconType icon_id)
		{
			var dict = new Dictionary<string, string>();
			dict.Add(nameof(group_id), group_id);
			dict.Add(nameof(name), name);
			dict.Add(nameof(description), description);
			dict.Add(nameof(is_public), is_public.ToString1Or0());
			dict.Add(nameof(default_sort), ((uint)default_sort).ToString());
			dict.Add(nameof(icon_id), ((uint)icon_id).ToString());

			return context.PostAsync(NiconicoUrls.MylistGroupUpdateUrl, dict);
		}


		public static Task<string> RemoveMylistGroupDataAsync(NiconicoContext context, string group_id)
		{
			var dict = new Dictionary<string, string>();
			dict.Add(nameof(group_id), group_id);

			return context.PostAsync(NiconicoUrls.MylistGroupRemoveUrl, dict);
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




		
		public static async Task<MylistGroupsResponse> GetLoginUserMylistGroupsAsync(NiconicoContext context)
		{
			return await context.GetJsonAsAsync<MylistGroupsResponse>(@"https://nvapi.nicovideo.jp/v1/users/me/mylists", Converter.Settings);
		}


		public static async Task<MylistGroupsResponse> GetMylistGroupsAsync(NiconicoContext context, int userId)
		{
			return await context.GetJsonAsAsync<MylistGroupsResponse>($"https://nvapi.nicovideo.jp/v1/users/{userId}/mylists?sampleItemCount=3", Converter.Settings
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


		public static Task<ContentManageResult> AddMylistGroupAsync(NiconicoContext context, string name, string description, bool is_public, MylistDefaultSort default_sort, IconType iconType)
		{
			return AddMylistGroupDataAsync(context, name, description, is_public, default_sort, iconType)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}

		public static Task<ContentManageResult> UpdateMylistGroupAsync(NiconicoContext context, string group_id, string name, string description, bool is_public, MylistDefaultSort default_sort, IconType iconType)
		{
			return UpdateMylistGroupDataAsync(context, group_id, name, description, is_public, default_sort, iconType)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}


		public static Task<ContentManageResult> RemoveMylistGroupAsync(NiconicoContext context, string group_id)
		{
			return RemoveMylistGroupDataAsync(context, group_id)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
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
			await context.PrepareCorsAsscessAsync(uri);
			return await context.GetJsonAsAsync<MylistGroupItemsResponse>(uri, Converter.Settings);
		}

		public static Task<MylistGroupItemsResponse> GetMylistGroupItemsAsync(NiconicoContext context, long mylistId, MylistSortKey sortKey, MylistSortOrder sortOrder, uint pageSize, uint pageCount)
		{
			// Note: CORSのOPTIONSを先に送る奴が必要になるかも
			return context.GetJsonAsAsync<MylistGroupItemsResponse>($"https://nvapi.nicovideo.jp/v2/mylists/{mylistId}?sortKey={sortKey.ToQueryString()}&sortOrder={sortOrder.ToQueryString()}&pageSize={pageSize}&page={pageCount + 1}", Converter.Settings);
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

		public static async Task<ContentManageResult> RemoveMylistItemAsync(NiconicoContext context, string group_id, NiconicoItemType item_type, params string[] itemIdList)
		{
			var dict = new Dictionary<string, string>();

			dict.Add(nameof(group_id), group_id);

			var key = NiconicoQueryHelper.Make_idlist_QueryKeyString(item_type);

			foreach (var item_id in itemIdList)
			{
				dict.Add(key, item_id);
			}

			var json = await context.PostAsync(NiconicoUrls.MylistRemoveUrl, dict);
			return ContentManagerResultHelper.ParseJsonResult(json);
		}

		public static async Task<ContentManageResult> CopyMylistItemAsync(NiconicoContext context, string group_id, string target_group_id, NiconicoItemType itemType, params string[] itemIdList)
		{
			var dict = new Dictionary<string, string>();

			dict.Add(nameof(group_id), group_id);
			dict.Add(nameof(target_group_id), target_group_id);

			var key = NiconicoQueryHelper.Make_idlist_QueryKeyString(itemType);
			foreach (var item_id in itemIdList)
			{
				dict.Add(key, item_id);
			}

			var json = await context.PostAsync(NiconicoUrls.MylistCopyUrl, dict);
			return ContentManagerResultHelper.ParseJsonResult(json);
		}


		public static async Task<ContentManageResult> MoveMylistItemAsync(NiconicoContext context, string group_id, string target_group_id, NiconicoItemType itemType, params string[] itemIdList)
		{
			var dict = new Dictionary<string, string>();

			dict.Add(nameof(group_id), group_id);
			dict.Add(nameof(target_group_id), target_group_id);

			var key = NiconicoQueryHelper.Make_idlist_QueryKeyString(itemType);
			foreach (var item_id in itemIdList)
			{
				dict.Add(key, item_id);
			}

			var json = await context.PostAsync(NiconicoUrls.MylistMoveUrl, dict);

			return ContentManagerResultHelper.ParseJsonResult(json);
		}

#endregion


#region WatchAfter


		public static async Task<WatchAfterMylistGroupItemsResponse> GetWatchAfterMylistGroupItemsAsync(NiconicoContext context, MylistSortKey sortKey, MylistSortOrder sortOrder, uint pageSize, uint pageCount)
		{
			var uri = $"https://nvapi.nicovideo.jp/v1/users/me/watch-later?sortKey={sortKey.ToQueryString()}&sortOrder={sortOrder.ToQueryString()}&pageSize={pageSize}&page={pageCount + 1}";
			await context.PrepareCorsAsscessAsync(uri);
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

		public static async Task<ContentManageResult> RemoveDeflistAsync(NiconicoContext context, NiconicoItemType itemType, params string[] itemIdList)
		{
			var dict = new Dictionary<string, string>();

			var key = NiconicoQueryHelper.Make_idlist_QueryKeyString(itemType);

			foreach (var item_id in itemIdList)
			{
				dict.Add(key, item_id);
			}

			var json = await context.PostAsync(NiconicoUrls.MylistDeflistRemoveUrl, dict);
				return ContentManagerResultHelper.ParseJsonResult(json);
		}

		public static async Task<ContentManageResult> MoveDeflistAsync(NiconicoContext context, string target_group_id, NiconicoItemType itemType, params string[] itemIdList)
		{
			var dict = new Dictionary<string, string>();

			dict.Add(nameof(target_group_id), target_group_id);

			var key = NiconicoQueryHelper.Make_idlist_QueryKeyString(itemType);
			foreach (var item_id in itemIdList)
			{
				dict.Add(key, item_id);
			}

			var json = await context.PostAsync(NiconicoUrls.MylistDeflistMoveUrl, dict);
			return ContentManagerResultHelper.ParseJsonResult(json);
		}


		public static async Task<ContentManageResult> CopyDeflistAsync(NiconicoContext context, string target_group_id, NiconicoItemType itemType, params string[] itemIdList)
		{
			var dict = new Dictionary<string, string>();

			dict.Add(nameof(target_group_id), target_group_id);

			var key = NiconicoQueryHelper.Make_idlist_QueryKeyString(itemType);

			foreach (var item_id in itemIdList)
			{
				dict.Add(key, item_id);
			}

			var json = await context.PostAsync(NiconicoUrls.MylistDeflistCopyUrl, dict);
			return ContentManagerResultHelper.ParseJsonResult(json);
		}


#endregion
	}



}
