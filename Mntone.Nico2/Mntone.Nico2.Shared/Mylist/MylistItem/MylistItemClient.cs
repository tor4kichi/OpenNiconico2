﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mntone.Nico2.Mylist.MylistItem
{
    internal sealed class MylistItemClient
    {
		public static async Task<string> GetMylistItemDataAsync(NiconicoContext context, string group_id)
		{
			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistListUrl}?{nameof(group_id)}={group_id}");
		}

		public static async Task<string> AddMylistItemDataAsync(NiconicoContext context, string group_id, NiconicoItemType item_type, string item_id, string description)
		{
			var token = CSRFTokenHelper.GetToken(context);

			var no_prefix_id = NiconicoQueryHelper.RemoveIdPrefix(item_id);

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistAddUrl}?{nameof(group_id)}={group_id}&{nameof(item_type)}={(uint)item_type}&{nameof(item_id)}={no_prefix_id}&{nameof(description)}={description}&{nameof(token)}={token}");
		}

		public static async Task<string> UpdateMylistItemDataAsync(NiconicoContext context, string group_id, NiconicoItemType item_type, string item_id, string description)
		{
			var token = CSRFTokenHelper.GetToken(context);

			var no_prefix_id = NiconicoQueryHelper.RemoveIdPrefix(item_id);

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistUpdateUrl}?{nameof(group_id)}={group_id}&{nameof(item_type)}={(uint)item_type}&{nameof(item_id)}={no_prefix_id}&{nameof(description)}={description}&{nameof(token)}={token}");
		}

		public static async Task<string> RemoveMylistItemDataAsync(NiconicoContext context, string group_id, NiconicoItemType item_type, string item_id)
		{
			var token = CSRFTokenHelper.GetToken(context);

			var ids_query = NiconicoQueryHelper.Make_idlist_QueryString(item_type, item_id);

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistRemoveUrl}?{nameof(group_id)}={group_id}&{ids_query}&{nameof(token)}={token}");
		}


		public static async Task<string> RemoveMylistItemDataAsync(NiconicoContext context, IEnumerable<MylistData> datum)
		{
			var token = CSRFTokenHelper.GetToken(context);

			var ids_query = MylistQueryUtil.MylistDataToQueryString(datum);

			var group_id = datum.First().GroupId;

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistRemoveUrl}?{nameof(group_id)}={group_id}&{ids_query}&{nameof(token)}={token}");
		}

		public static async Task<string> MoveMylistItemDataAsync(NiconicoContext context, string target_group_id, IEnumerable<MylistData> datum)
		{
			var token = CSRFTokenHelper.GetToken(context);

			var ids_query = MylistQueryUtil.MylistDataToQueryString(datum);

			var group_id = datum.First().GroupId;

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistMoveUrl}?{nameof(group_id)}={group_id}&{nameof(target_group_id)}={target_group_id}&{ids_query}&{nameof(token)}={token}");
		}


		public static async Task<string> CopyMylistItemDataAsync(NiconicoContext context, string target_group_id, IEnumerable<MylistData> datum)
		{
			var token = CSRFTokenHelper.GetToken(context);

			var ids_query = MylistQueryUtil.MylistDataToQueryString(datum);

			var group_id = datum.First().GroupId;

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistCopyUrl}?{nameof(group_id)}={group_id}&{nameof(target_group_id)}={target_group_id}&{ids_query}&{nameof(token)}={token}");
		}




		public static async Task<string> GetMylistListDataAsync(NiconicoContext context, string group_id, uint from, uint limit, SortMethod sortMethod, SortDirection sortDir)
		{
			var dict = new Dictionary<string, string>();

			dict.Add(nameof(group_id), group_id);
			dict.Add(nameof(from), from.ToString());
			dict.Add(nameof(limit), limit.ToString());
			dict.Add(nameof(sortMethod), sortMethod.ToShortString());
			dict.Add(nameof(sortDir), sortDir.ToShortString());

			var query = HttpQueryExtention.DictionaryToQuery(dict);

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistListlApi}?{query}");
		}

		private static NicoVideoResponse ParseMylistListXml(string xml)
		{
			var serializer = new XmlSerializer(typeof(NicoVideoResponse));

			NicoVideoResponse response = null;
			using (var stream = new StringReader(xml))
			{
				response = (NicoVideoResponse)serializer.Deserialize(stream);
			}

			return response;

		}




		public static Task<NicoVideoResponse> GetMylistListAsync(NiconicoContext context, string group_id, uint from, uint limit, SortMethod sortMethod, SortDirection sortDir)
		{
			return GetMylistListDataAsync(context, group_id, from, limit, sortMethod, sortDir)
				.ContinueWith(prevTask => ParseMylistListXml(prevTask.Result));

		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <param name="group_id"></param>
		/// <returns></returns>
		public static Task<List<MylistData>> GetMylistItemAsync(NiconicoContext context, string group_id)
		{
			return GetMylistItemDataAsync(context, group_id)
				.ContinueWith(prevTask => MylistJsonSerializeHelper.ParseMylistItemResponse(prevTask.Result));
		}


		public static Task<ContentManageResult> AddMylistItemAsync(NiconicoContext context, string group_id, NiconicoItemType item_type, string item_id, string description)
		{
			return AddMylistItemDataAsync(context, group_id, item_type, item_id, description)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}


		public static Task<ContentManageResult> UpdateMylistItemAsync(NiconicoContext context, string group_id, NiconicoItemType item_type, string item_id, string description)
		{
			return UpdateMylistItemDataAsync(context, group_id, item_type, item_id, description)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}

		public static Task<ContentManageResult> RemoveMylistItemAsync(NiconicoContext context, string group_id, NiconicoItemType item_type, string item_id)
		{
			return RemoveMylistItemDataAsync(context, group_id, item_type, item_id)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}

		public static Task<ContentManageResult> RemoveMylistItemAsync(NiconicoContext context, IEnumerable<MylistData> datum)
		{
			return RemoveMylistItemDataAsync(context, datum)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}

		public static Task<ContentManageResult> MoveMylistItemAsync(NiconicoContext context, string target_group_id, IEnumerable<MylistData> datum)
		{
			return MoveMylistItemDataAsync(context, target_group_id, datum)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}

		public static Task<ContentManageResult> CopyMylistItemAsync(NiconicoContext context, string target_group_id, IEnumerable<MylistData> datum)
		{
			return CopyMylistItemDataAsync(context, target_group_id, datum)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}
	}
}
