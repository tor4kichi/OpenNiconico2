using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Mylist.Deflist
{
    internal sealed class DeflistClient
    {
		public static async Task<string> GetDeflistDataAsync(NiconicoContext context)
		{
			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistDeflistListUrl}");
		}


		public static async Task<string> AddDeflistDataAsync(NiconicoContext context, NiconicoItemType item_type, string item_id, string description)
		{
			var token = MylistCSRFTokenHelper.GetMylistToken(context);

			var no_prefix_id = MylistQueryUtil.RemoveIdPrefix(item_id);

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistDeflistAddUrl}?{nameof(item_type)}={(uint)item_type}&{nameof(item_id)}={no_prefix_id}&{nameof(description)}={description}&{nameof(token)}={token}");
		}


		public static async Task<string> UpdateDeflistDataAsync(NiconicoContext context, NiconicoItemType item_type, string item_id, string description)
		{
			var token = MylistCSRFTokenHelper.GetMylistToken(context);

			var no_prefix_id = MylistQueryUtil.RemoveIdPrefix(item_id);

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistDeflistUpdateUrl}?{nameof(item_type)}={(uint)item_type}&{nameof(item_id)}={no_prefix_id}&{nameof(description)}={description}&{nameof(token)}={token}");
		}


		public static async Task<string> RemoveDeflistDataAsync(NiconicoContext context, IEnumerable<string> id_list)
		{
			var token = MylistCSRFTokenHelper.GetMylistToken(context);

			var ids_text = MylistQueryUtil.IdListToQueryString(id_list);

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistDeflistRemoveUrl}?{nameof(id_list)}={ids_text}&{nameof(token)}={token}");
		}


		public static async Task<string> MoveDeflistDataAsync(NiconicoContext context, string target_group_id, IEnumerable<string> id_list)
		{
			var token = MylistCSRFTokenHelper.GetMylistToken(context);

			var ids_text = MylistQueryUtil.IdListToQueryString(id_list);

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistDeflistMoveUrl}?{nameof(target_group_id)}={target_group_id}&{nameof(id_list)}={ids_text}&{nameof(token)}={token}");
		}

		public static async Task<string> CopyDeflistDataAsync(NiconicoContext context, string target_group_id, IEnumerable<string> id_list)
		{
			var token = MylistCSRFTokenHelper.GetMylistToken(context);

			var ids_text = MylistQueryUtil.IdListToQueryString(id_list);

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistDeflistCopyUrl}?{nameof(target_group_id)}={target_group_id}&{nameof(id_list)}={ids_text}&{nameof(token)}={token}");
		}




		



		public static Task<List<MylistData>> GetDeflistAsync(NiconicoContext context)
		{
			return GetDeflistDataAsync(context)
				.ContinueWith(prevTask => MylistJsonSerializeHelper.ParseMylistItemResponse(prevTask.Result));
		}



		public static Task<ContentManageResult> AddDeflistAsync(NiconicoContext context, NiconicoItemType item_type, string item_id, string description)
		{
			return AddDeflistDataAsync(context, item_type, item_id, description)
				.ContinueWith(prevTask => MylistJsonSerializeHelper.ParseMylistApiResult(prevTask.Result));
		}


		public static Task<ContentManageResult> UpdateDeflistAsync(NiconicoContext context, NiconicoItemType item_type, string item_id, string description)
		{
			return UpdateDeflistDataAsync(context, item_type, item_id, description)
				.ContinueWith(prevTask => MylistJsonSerializeHelper.ParseMylistApiResult(prevTask.Result));
		}

		public static Task<ContentManageResult> RemoveDeflistAsync(NiconicoContext context, string item_id)
		{
			return RemoveDeflistDataAsync(context, new [] { item_id })
				.ContinueWith(prevTask => MylistJsonSerializeHelper.ParseMylistApiResult(prevTask.Result));
		}

		public static Task<ContentManageResult> RemoveDeflistAsync(NiconicoContext context, IEnumerable<string> id_list)
		{
			return RemoveDeflistDataAsync(context, id_list)
				.ContinueWith(prevTask => MylistJsonSerializeHelper.ParseMylistApiResult(prevTask.Result));
		}


		public static Task<ContentManageResult> MoveDeflistAsync(NiconicoContext context, string target_group_id, IEnumerable<string> id_list)
		{
			return MoveDeflistDataAsync(context, target_group_id, id_list)
				.ContinueWith(prevTask => MylistJsonSerializeHelper.ParseMylistApiResult(prevTask.Result));
		}


		public static Task<ContentManageResult> CopyDeflistAsync(NiconicoContext context, string target_group_id, IEnumerable<string> id_list)
		{
			return CopyDeflistDataAsync(context, target_group_id, id_list)
				.ContinueWith(prevTask => MylistJsonSerializeHelper.ParseMylistApiResult(prevTask.Result));
		}
	}
}
