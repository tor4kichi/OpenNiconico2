using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Mylist
{
    public sealed class MylistApi
    {
		internal MylistApi(NiconicoContext context)
		{
			this._context = context;
		}



		// Note: MylistGroupとMylistの違いについて

		// MylistGroupがいわゆるマイリストと呼ばれる大本のデータ
		// MylistGroupの持つgroup_idをAPIに渡すことでそのMylistGroupが持つ
		// 動画や静画などのマイリスト登録されたアイテムの一覧を取得することができます




		// Note: とりあえずマイリストと個別マイリストについて

		// GroupId=="0"は「とりあえずマイリスト」として扱われます。
		// とりあえずマイリストと個別マイリストは操作するためのAPIのURLが異なります。
		// とりあえずマイリスト /api/deflist
		// 個別マイリスト /api/mylist


		/// <summary>
		/// ログイン中のユーザーのマイリストグループ一覧を取得
		/// </summary>
		/// <returns></returns>
		public Task<List<MylistGroupData>> GetMylistGroupListAsync()
		{
			return MylistGroup.MylistGroupClient.GetMylistGroupListAsync(_context);
		}


		/// <summary>
		/// ログイン中のユーザーの新しいマイリストグループを作成
		/// </summary>
		/// <returns></returns>
		public Task<ContentManageResult> CreateMylistGroupAsync(string name, string description, bool is_public, MylistDefaultSort default_sort, IconType iconType)
		{
			return MylistGroup.MylistGroupClient.AddMylistGroupAsync(_context, name, description, is_public, default_sort, iconType);
		}


		/// <summary>
		/// ログイン中のユーザーの新しいマイリストグループを作成
		/// </summary>
		/// <returns></returns>
		public Task<ContentManageResult> CreateMylistGroupAsync(MylistGroupData groupData)
		{
			return MylistGroup.MylistGroupClient.AddMylistGroupAsync(_context, groupData.Name, groupData.Description, groupData.GetIsPublic(), MylistDefaultSort.FirstRetrieve_Descending, groupData.GetIconType());
		}

		/// <summary>
		/// ログイン中のユーザーのマイリストグループを更新
		/// </summary>
		/// <param name="group_id"></param>
		/// <param name="name"></param>
		/// <param name="description"></param>
		/// <param name="is_public"></param>
		/// <param name="default_sort"></param>
		/// <param name="iconType"></param>
		/// <returns></returns>
		public Task<ContentManageResult> UpdateMylistGroupAsync(string group_id, string name, string description, bool is_public, MylistDefaultSort default_sort, IconType iconType)
		{
			return MylistGroup.MylistGroupClient.UpdateMylistGroupAsync(_context, group_id, name, description, is_public, default_sort, iconType);
		}


		/// <summary>
		/// ログイン中のユーザーのマイリストグループを更新
		/// </summary>
		/// <param name="groupData"></param>
		/// <returns></returns>
		public Task<ContentManageResult> UpdateMylistGroupAsync(MylistGroupData groupData)
		{
			return MylistGroup.MylistGroupClient.UpdateMylistGroupAsync(_context, groupData.Id, groupData.Name, groupData.Description, groupData.GetIsPublic(), MylistDefaultSort.FirstRetrieve_Descending, groupData.GetIconType());
		}


		/// <summary>
		/// ログイン中のユーザーのマイリストグループを削除
		/// </summary>
		/// <param name="group_id">削除対象のマイリストグループID(</param>
		/// <returns></returns>
		public Task<ContentManageResult> RemoveMylistGroupAsync(string group_id)
		{
			return MylistGroup.MylistGroupClient.RemoveMylistGroupAsync(_context, group_id);
		}


		/// <summary>
		/// ログイン中のユーザーのマイリストグループを削除
		/// </summary>
		/// <param name="group_id">削除対象のマイリストグループID(</param>
		/// <returns></returns>
		public Task<ContentManageResult> RemoveMylistGroupAsync(MylistGroupData groupData)
		{
			return MylistGroup.MylistGroupClient.RemoveMylistGroupAsync(_context, groupData.Id);
		}

		/// <summary>
		/// マイリストに登録されたアイテムの一覧を取得
		/// </summary>
		/// <param name="group_id">マイリストグループID</param>
		/// <returns></returns>
		/// <remarks>http://api.ce.nicovideo.jp/nicoapi/v1 を利用してマイリストを取得します。</remarks>
		public Task<NicoVideoResponse> GetMylistListAsync(string group_id, uint from = 0, uint limit = 50, SortMethod sortMethod = SortMethod.FirstRetrieve, SortDirection sortDir = SortDirection.Descending)
		{
			return MylistItem.MylistItemClient.GetMylistListAsync(_context, group_id, from, limit, sortMethod, sortDir);
		}



		/// <summary>
		/// マイリストに登録されたアイテムの一覧を取得
		/// </summary>
		/// <param name="group_id">マイリストグループID</param>
		/// <returns></returns>
		public Task<List<MylistData>> GetMylistItemListAsync(string group_id)
		{
			if (MylistGroupData.IsDeflist(group_id))
			{
				return Deflist.DeflistClient.GetDeflistAsync(_context);
			}
			else
			{
				return MylistItem.MylistItemClient.GetMylistItemAsync(_context, group_id);
			}
		}

		/// <summary>
		/// マイリストにアイテムを登録する
		/// </summary>
		/// <param name="group_id">登録対象のマイリストグループID</param>
		/// <param name="item_type">アイテムの種類（動画、静画etc)</param>
		/// <param name="item_id">アイテムのID（smなどの接頭辞を含む）</param>
		/// <param name="description">登録アイテムに対するコメント</param>
		/// <returns></returns>
		public Task<ContentManageResult> AddMylistItemAsync(string group_id, NiconicoItemType item_type, string item_id, string description)
		{
			if (MylistGroupData.IsDeflist(group_id))
			{
				return Deflist.DeflistClient.AddDeflistAsync(_context, item_type, item_id, description);
			}
			else
			{
				return MylistItem.MylistItemClient.AddMylistItemAsync(_context, group_id, item_type, item_id, description);
			}
		}


		/// <summary>
		/// マイリストにアイテムを登録する
		/// </summary>
		/// <param name="group_id">登録対象のマイリストグループID</param>
		/// <param name="mylistData">マイリストデータ</param>
		/// <returns></returns>
		public Task<ContentManageResult> AddMylistItemAsync(string group_id, MylistData mylistData)
		{
			return MylistItem.MylistItemClient.AddMylistItemAsync(_context, mylistData.GroupId, mylistData.ItemType, mylistData.ItemId, mylistData.Description);
		}


		/// <summary>
		/// マイリストのアイテムの情報を更新する
		/// </summary>
		/// <param name="group_id">登録対象のマイリストグループID</param>
		/// <param name="item_type">アイテムの種類（動画、静画etc)</param>
		/// <param name="item_id">アイテムのID（smなどの接頭辞を含む）</param>
		/// <param name="description">登録アイテムに対するコメント</param>
		/// <returns></returns>
		public Task<ContentManageResult> UpdateMylistItemAsync(string group_id, NiconicoItemType item_type, string item_id, string description)
		{
			if (MylistGroupData.IsDeflist(group_id))
			{
				return Deflist.DeflistClient.UpdateDeflistAsync(_context, item_type, item_id, description);
			}
			else
			{
				return MylistItem.MylistItemClient.UpdateMylistItemAsync(_context, group_id, item_type, item_id, description);
			}
		}


		/// <summary>
		/// マイリストのアイテムの情報を更新する
		/// </summary>
		/// <param name="group_id"></param>
		/// <param name="mylistData"></param>
		/// <returns></returns>
		public Task<ContentManageResult> UpdateMylistItemAsync(string group_id, MylistData mylistData)
		{
			return UpdateMylistItemAsync(mylistData.GroupId, mylistData.ItemType, mylistData.ItemId, mylistData.Description);
		}


		/// <summary>
		/// マイリストからアイテムを削除します
		/// </summary>
		/// <param name="mylistData"></param>
		/// <returns></returns>
		public Task<ContentManageResult> RemoveMylistItemAsync(string group_id, NiconicoItemType item_type, params string[] itemIdList)
		{
			if (MylistGroupData.IsDeflist(group_id))
			{
				return Deflist.DeflistClient.RemoveDeflistAsync(_context, item_type, itemIdList);
			}
			else
			{
				return MylistItem.MylistItemClient.RemoveMylistItemAsync(_context, group_id, item_type, itemIdList);
			}
		}



		



		/// <summary>
		/// マイリストアイテムを別のマイリストへコピーします
		/// </summary>
		/// <param name="targetMylistGroup"></param>
		/// <param name="datum"></param>
		/// <returns></returns>
		/// <remarks>ターゲットにはとりあえずマイリストを指定することはできません</remarks>
		public Task<ContentManageResult> CopyMylistItemAsync(string group_id, string target_group_id, NiconicoItemType itemType, params string[] itemIdList)
		{
			if (group_id == target_group_id)
			{
				return Task.FromResult(ContentManageResult.Success);
			}

			if (MylistGroupData.IsDeflist(target_group_id))
			{
				// とりあえずマイリストへのコピーはサポートしていない
				throw new NotSupportedException("not support mylist item copy to Deflist(とりあえずマイリスト)");
			}

			if (MylistGroupData.IsDeflist(group_id))
			{
				return Deflist.DeflistClient.CopyDeflistAsync(_context, target_group_id, itemType, itemIdList);
			}
			else
			{
				return MylistItem.MylistItemClient.CopyMylistItemAsync(_context, group_id, target_group_id, itemType, itemIdList);
			}
			
		}

		/// <summary>
		/// マイリストのアイテムを別のマイリストに移動します。
		/// </summary>
		/// <param name="targetMylistGroup"></param>
		/// <param name="datum"></param>
		/// <returns></returns>
		/// <remarks>ターゲットにはとりあえずマイリストを指定することは出来ません。</remarks>
		public Task<ContentManageResult> MoveMylistItemAsync(string group_id, string target_group_id, NiconicoItemType itemType, params string[] itemIdList)
		{
			if (group_id == target_group_id)
			{
				return Task.FromResult(ContentManageResult.Success);
			}

			if (MylistGroupData.IsDeflist(target_group_id))
			{
				// とりあえずマイリストへの移動はサポートしていない
				throw new NotSupportedException("not support mylist item move to Deflist(とりあえずマイリスト)");
			}

			if (MylistGroupData.IsDeflist(group_id))
			{
				return Deflist.DeflistClient.MoveDeflistAsync(_context, target_group_id, itemType, itemIdList);
			}
			else
			{
				return MylistItem.MylistItemClient.MoveMylistItemAsync(_context, group_id, target_group_id, itemType, itemIdList);
			}

		}


		/// <summary>
		/// ユーザーのマイリストグループ一覧を取得します
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public Task<List<MylistGroupData>> GetUserMylistGroupAsync(string userId)
		{
			return UserMylist.UserMylistClient.GetUserMylistAsync(_context, userId);
		}



		/// <summary>
		/// マイリストグループの詳細を取得します
		/// </summary>
		/// <param name="group_id"></param>
		/// <returns></returns>
		public Task<MylistGroupDetail> GetMylistGroupDetailAsync(string group_id)
		{
			return MylistGroup.MylistGroupClient.GetMylistGroupDetailAsync(_context, group_id);
		}


		#region field

		private NiconicoContext _context;

		#endregion
	}
}
