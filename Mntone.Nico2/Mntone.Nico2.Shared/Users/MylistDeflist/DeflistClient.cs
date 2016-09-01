using Mntone.Nico2.Mylist;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Users.Deflist
{
    internal sealed class DeflistClient
    {
		public static Task<string> GetDeflistDataAsync(NiconicoContext context)
		{
			return context.PostAsync(NiconicoUrls.MylistDeflistListUrl);
		}


		public static Task<string> AddDeflistDataAsync(NiconicoContext context, NiconicoItemType item_type, string item_id, string description)
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

			return context.PostAsync(NiconicoUrls.MylistDeflistAddUrl, dict);
		}


		public static Task<string> UpdateDeflistDataAsync(NiconicoContext context, NiconicoItemType item_type, string item_id, string description)
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

			return context.PostAsync(NiconicoUrls.MylistDeflistUpdateUrl, dict);
		}


		public static Task<string> RemoveDeflistDataAsync(NiconicoContext context, NiconicoItemType itemType, params string[] itemIdList)
		{
			var dict = new Dictionary<string, string>();

			var key = NiconicoQueryHelper.Make_idlist_QueryKeyString(itemType);

			foreach (var item_id in itemIdList)
			{
				dict.Add(key, item_id);
			}

			return context.PostAsync(NiconicoUrls.MylistDeflistRemoveUrl, dict);
		}


		public static Task<string> MoveDeflistDataAsync(NiconicoContext context, string target_group_id, NiconicoItemType itemType, params string[] itemIdList)
		{
			var dict = new Dictionary<string, string>();

			dict.Add(nameof(target_group_id), target_group_id);

			var key = NiconicoQueryHelper.Make_idlist_QueryKeyString(itemType);
			foreach (var item_id in itemIdList)
			{
				dict.Add(key, item_id);
			}

			return context.PostAsync(NiconicoUrls.MylistDeflistMoveUrl, dict);
		}

		




		public static Task<string> CopyDeflistDataAsync(NiconicoContext context, string target_group_id, NiconicoItemType itemType, params string[] itemIdList)
		{
			var dict = new Dictionary<string, string>();

			dict.Add(nameof(target_group_id), target_group_id);

			var key = NiconicoQueryHelper.Make_idlist_QueryKeyString(itemType);

			foreach (var item_id in itemIdList)
			{
				dict.Add(key, item_id);
			}

			return context.PostAsync(NiconicoUrls.MylistDeflistCopyUrl, dict);
		}





		public static Task<List<MylistData>> GetDeflistAsync(NiconicoContext context)
		{
			return GetDeflistDataAsync(context)
				.ContinueWith(prevTask => MylistJsonSerializeHelper.ParseMylistItemResponse(prevTask.Result));
		}



		public static Task<ContentManageResult> AddDeflistAsync(NiconicoContext context, NiconicoItemType item_type, string item_id, string description)
		{
			return AddDeflistDataAsync(context, item_type, item_id, description)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}


		public static Task<ContentManageResult> UpdateDeflistAsync(NiconicoContext context, NiconicoItemType item_type, string item_id, string description)
		{
			return UpdateDeflistDataAsync(context, item_type, item_id, description)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}

		public static Task<ContentManageResult> RemoveDeflistAsync(NiconicoContext context, NiconicoItemType item_type, params string[] itemIdList)
		{
			return RemoveDeflistDataAsync(context, item_type, itemIdList)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}

		public static Task<ContentManageResult> MoveDeflistAsync(NiconicoContext context, string target_group_id, NiconicoItemType item_type, params string[] itemIdList)
		{
			return MoveDeflistDataAsync(context, target_group_id, item_type, itemIdList)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}


		public static Task<ContentManageResult> CopyDeflistAsync(NiconicoContext context, string target_group_id, NiconicoItemType item_type, params string[] itemIdList)
		{
			return CopyDeflistDataAsync(context, target_group_id, item_type, itemIdList)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}

	}
}
