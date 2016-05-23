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
			var token = CSRFTokenHelper.GetToken(context);

			var no_prefix_id = NiconicoQueryHelper.RemoveIdPrefix(item_id);

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistDeflistAddUrl}?{nameof(item_type)}={(uint)item_type}&{nameof(item_id)}={no_prefix_id}&{nameof(description)}={description}&{nameof(token)}={token}");
		}


		public static async Task<string> UpdateDeflistDataAsync(NiconicoContext context, NiconicoItemType item_type, string item_id, string description)
		{
			var token = CSRFTokenHelper.GetToken(context);

			var no_prefix_id = NiconicoQueryHelper.RemoveIdPrefix(item_id);

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistDeflistUpdateUrl}?{nameof(item_type)}={(uint)item_type}&{nameof(item_id)}={no_prefix_id}&{nameof(description)}={description}&{nameof(token)}={token}");
		}


		public static async Task<string> RemoveDeflistDataAsync(NiconicoContext context, NiconicoItemType itemType, string item_id)
		{
			var token = CSRFTokenHelper.GetToken(context);

			var id_list = NiconicoQueryHelper.Make_idlist_QueryString(itemType, item_id);

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistDeflistRemoveUrl}?{id_list}&{nameof(token)}={token}");
		}


		public static async Task<string> RemoveDeflistDataAsync(NiconicoContext context, IEnumerable<MylistData> datum)
		{
			var token = CSRFTokenHelper.GetToken(context);

			var id_list = MylistQueryUtil.MylistDataToQueryString(datum);

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistDeflistRemoveUrl}?{id_list}&{nameof(token)}={token}");
		}


		public static async Task<string> MoveDeflistDataAsync(NiconicoContext context, string target_group_id, NiconicoItemType itemType, string item_id)
		{
			var token = CSRFTokenHelper.GetToken(context);

			var id_list = NiconicoQueryHelper.Make_idlist_QueryString(itemType, item_id);

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistDeflistMoveUrl}?{nameof(target_group_id)}={target_group_id}&{id_list}&{nameof(token)}={token}");
		}

		public static async Task<string> MoveDeflistDataAsync(NiconicoContext context, string target_group_id, IEnumerable<MylistData> datum)
		{
			var token = CSRFTokenHelper.GetToken(context);

			var id_list = MylistQueryUtil.MylistDataToQueryString(datum);

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistDeflistMoveUrl}?{nameof(target_group_id)}={target_group_id}&{id_list}&{nameof(token)}={token}");
		}




		public static async Task<string> CopyDeflistDataAsync(NiconicoContext context, string target_group_id, NiconicoItemType itemType, string item_id)
		{
			var token = CSRFTokenHelper.GetToken(context);

			var id_list = NiconicoQueryHelper.Make_idlist_QueryString(itemType, item_id);

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistDeflistCopyUrl}?{nameof(target_group_id)}={target_group_id}&{id_list}&{nameof(token)}={token}");
		}

		public static async Task<string> CopyDeflistDataAsync(NiconicoContext context, string target_group_id, IEnumerable<MylistData> datum)
		{
			var token = CSRFTokenHelper.GetToken(context);

			var id_list = MylistQueryUtil.MylistDataToQueryString(datum);

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistDeflistCopyUrl}?{nameof(target_group_id)}={target_group_id}&{id_list}&{nameof(token)}={token}");
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

		public static Task<ContentManageResult> RemoveDeflistAsync(NiconicoContext context, NiconicoItemType item_type, string item_id)
		{
			return RemoveDeflistDataAsync(context, item_type, item_id)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}

		public static Task<ContentManageResult> RemoveDeflistAsync(NiconicoContext context, IEnumerable<MylistData> datum)
		{
			return RemoveDeflistDataAsync(context, datum)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}

		public static Task<ContentManageResult> MoveDeflistAsync(NiconicoContext context, string target_group_id, NiconicoItemType item_type, string item_id)
		{
			return MoveDeflistDataAsync(context, target_group_id, item_type, item_id)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}

		public static Task<ContentManageResult> MoveDeflistAsync(NiconicoContext context, string target_group_id, IEnumerable<MylistData> datum)
		{
			return MoveDeflistDataAsync(context, target_group_id, datum)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}

		public static Task<ContentManageResult> CopyDeflistAsync(NiconicoContext context, string target_group_id, NiconicoItemType item_type, string item_id)
		{
			return CopyDeflistDataAsync(context, target_group_id, item_type, item_id)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}

		public static Task<ContentManageResult> CopyDeflistAsync(NiconicoContext context, string target_group_id, IEnumerable<MylistData> datum)
		{
			return CopyDeflistDataAsync(context, target_group_id, datum)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}
	}
}
