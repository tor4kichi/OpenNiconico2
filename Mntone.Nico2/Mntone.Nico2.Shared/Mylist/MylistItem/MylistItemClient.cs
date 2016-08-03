using Newtonsoft.Json;
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
		public static Task<string> GetMylistItemDataAsync(NiconicoContext context, string group_id)
		{
			var dict = new Dictionary<string, string>();
			dict.Add(nameof(group_id), group_id);
			return context.PostAsync(NiconicoUrls.MylistListUrl, dict);
		}

		public static Task<string> AddMylistItemDataAsync(NiconicoContext context, string group_id, NiconicoItemType item_type, string item_id, string description)
		{
			var dict = new Dictionary<string, string>();

			dict.Add(nameof(group_id), group_id);
			dict.Add(nameof(item_type), ((uint)item_type).ToString());
			dict.Add(nameof(item_id), item_id);
			dict.Add(nameof(description), description);

			return context.PostAsync(NiconicoUrls.MylistAddUrl, dict);
		}

		public static Task<string> UpdateMylistItemDataAsync(NiconicoContext context, string group_id, NiconicoItemType item_type, string item_id, string description)
		{
			var dict = new Dictionary<string, string>();

			dict.Add(nameof(group_id), group_id);
			dict.Add(nameof(item_type), ((uint)item_type).ToString());
			dict.Add(nameof(item_id), item_id);
			dict.Add(nameof(description), description);

			return context.PostAsync(NiconicoUrls.MylistUpdateUrl, dict);
		}

		public static Task<string> RemoveMylistItemDataAsync(NiconicoContext context, string group_id, NiconicoItemType item_type, string[] itemIdList)
		{
			var dict = new Dictionary<string, string>();

			dict.Add(nameof(group_id), group_id);

			var key = NiconicoQueryHelper.Make_idlist_QueryKeyString(item_type);

			foreach (var item_id in itemIdList)
			{
				dict.Add(key, item_id);
			}

			return context.PostAsync(NiconicoUrls.MylistRemoveUrl, dict);
		}

		public static Task<string> CopyMylistDataAsync(NiconicoContext context, string group_id, string target_group_id, NiconicoItemType itemType, params string[] itemIdList)
		{
			var dict = new Dictionary<string, string>();

			dict.Add(nameof(group_id), group_id);
			dict.Add(nameof(target_group_id), target_group_id);

			var key = NiconicoQueryHelper.Make_idlist_QueryKeyString(itemType);
			foreach (var item_id in itemIdList)
			{
				dict.Add(key, item_id);
			}

			return context.PostAsync(NiconicoUrls.MylistCopyUrl, dict);
		}


		public static Task<string> MoveMylistDataAsync(NiconicoContext context, string group_id, string target_group_id, NiconicoItemType itemType, params string[] itemIdList)
		{
			var dict = new Dictionary<string, string>();

			dict.Add(nameof(group_id), group_id);
			dict.Add(nameof(target_group_id), target_group_id);

			var key = NiconicoQueryHelper.Make_idlist_QueryKeyString(itemType);
			foreach (var item_id in itemIdList)
			{
				dict.Add(key, item_id);
			}

			return  context.PostAsync(NiconicoUrls.MylistMoveUrl, dict);
		}




		public static Task<string> GetMylistListDataAsync(NiconicoContext context, string group_id, uint from, uint limit, SortMethod sortMethod, SortDirection sortDir)
		{
			var dict = new Dictionary<string, string>();

			dict.Add(nameof(group_id), group_id);
			dict.Add(nameof(from), from.ToString());
			dict.Add(nameof(limit), limit.ToString());
			dict.Add(nameof(sortMethod), sortMethod.ToShortString());
			dict.Add(nameof(sortDir), sortDir.ToShortString());

			return context.GetStringAsync(NiconicoUrls.MylistListlApi, dict);
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

		public static Task<ContentManageResult> RemoveMylistItemAsync(NiconicoContext context, string group_id, NiconicoItemType item_type, params string[] itemIdList)
		{
			return RemoveMylistItemDataAsync(context, group_id, item_type, itemIdList)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}

		public static Task<ContentManageResult> CopyMylistItemAsync(NiconicoContext context, string group_id, string target_group_id, NiconicoItemType itemType, params string[] itemIdList)
		{
			return CopyMylistDataAsync(context, group_id, target_group_id, itemType, itemIdList)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}


		public static Task<ContentManageResult> MoveMylistItemAsync(NiconicoContext context, string group_id, string target_group_id, NiconicoItemType itemType, params string[] itemIdList)
		{
			return MoveMylistDataAsync(context, group_id, target_group_id, itemType, itemIdList)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}
	}
}
