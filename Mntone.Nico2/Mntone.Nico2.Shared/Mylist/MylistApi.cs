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
		public Task<List<MylistGroup.MylistGroupData>> GetMylistGroupListAsync()
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
		public Task<ContentManageResult> CreateMylistGroupAsync(MylistGroup.MylistGroupData groupData)
		{
			return MylistGroup.MylistGroupClient.AddMylistGroupAsync(_context, groupData.Name, groupData.Description, groupData.IsPublic, groupData.DefaultSort, groupData.IconId);
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
		public Task<ContentManageResult> UpdateMylistGroupAsync(MylistGroup.MylistGroupData groupData)
		{
			return MylistGroup.MylistGroupClient.UpdateMylistGroupAsync(_context, groupData.Id, groupData.Name, groupData.Description, groupData.IsPublic, groupData.DefaultSort, groupData.IconId);
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
		public Task<ContentManageResult> RemoveMylistGroupAsync(MylistGroup.MylistGroupData groupData)
		{
			return MylistGroup.MylistGroupClient.RemoveMylistGroupAsync(_context, groupData.Id);
		}


		/// <summary>
		/// マイリストに登録されたアイテムの一覧を取得
		/// </summary>
		/// <param name="group_id">マイリストグループID</param>
		/// <returns></returns>
		public Task<List<Mylist.MylistData>> GetMylistItemListAsync(string group_id)
		{
			if (MylistGroup.MylistGroupData.IsDeflist(group_id))
			{
				return Mylist.Deflist.DeflistClient.GetDeflistAsync(_context);
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
			if (MylistGroup.MylistGroupData.IsDeflist(group_id))
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
			if (MylistGroup.MylistGroupData.IsDeflist(group_id))
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
		public Task<ContentManageResult> RemoveMylistItemAsync(MylistData mylistData)
		{
			if (MylistGroup.MylistGroupData.IsDeflist(mylistData.GroupId))
			{
				return Deflist.DeflistClient.RemoveDeflistAsync(_context, mylistData.ItemId);
			}
			else
			{
				return MylistItem.MylistItemClient.RemoveMylistItemAsync(_context, mylistData.GroupId, mylistData.ItemType, mylistData.ItemId);
			}
		}


		/// <summary>
		/// マイリストからアイテムを削除します
		/// </summary>
		/// <param name="mylistData"></param>
		/// <returns></returns>
		public Task<ContentManageResult> RemoveMylistItemAsync(IEnumerable<MylistData> datum)
		{
			var groupId = datum.First().GroupId;

			if (MylistGroup.MylistGroupData.IsDeflist(groupId))
			{
				return Deflist.DeflistClient.RemoveDeflistAsync(_context, datum.Select(x => x.ItemId));
			}
			else
			{
				return MylistItem.MylistItemClient.RemoveMylistItemAsync(_context, datum);
			}
		}



		/// <summary>
		/// マイリストアイテムを別のマイリストへコピーします
		/// </summary>
		/// <param name="targetMylistGroup"></param>
		/// <param name="datum"></param>
		/// <returns></returns>
		/// <remarks>ターゲットにはとりあえずマイリストを指定することはできません</remarks>
		public Task<ContentManageResult> CopyMylistItemAsync(MylistGroup.MylistGroupData targetMylistGroup, IEnumerable<MylistData> datum)
		{
			if (targetMylistGroup.IsDeflist())
			{
				// とりあえずマイリストへのコピーはサポートしていない
				throw new NotSupportedException("not support mylist item copy to Deflist(とりあえずマイリスト)");
			}

			var groupId = datum.First().GroupId;

			if (MylistGroup.MylistGroupData.IsDeflist(groupId))
			{
				return Deflist.DeflistClient.CopyDeflistAsync(_context, targetMylistGroup.Id,  datum.Select(x => x.ItemId));
			}
			else
			{
				return MylistItem.MylistItemClient.CopyMylistItemAsync(_context, targetMylistGroup.Id, datum);
			}
			
		}

		/// <summary>
		/// マイリストのアイテムを別のマイリストに移動します。
		/// </summary>
		/// <param name="targetMylistGroup"></param>
		/// <param name="datum"></param>
		/// <returns></returns>
		/// <remarks>ターゲットにはとりあえずマイリストを指定することは出来ません。</remarks>
		public Task<ContentManageResult> MoveMylistItemAsync(MylistGroup.MylistGroupData targetMylistGroup, IEnumerable<MylistData> datum)
		{
			if (targetMylistGroup.IsDeflist())
			{
				// とりあえずマイリストへの移動はサポートしていない
				throw new NotSupportedException("not support mylist item move to Deflist(とりあえずマイリスト)");
			}

			var groupId = datum.First().GroupId;

			if (MylistGroup.MylistGroupData.IsDeflist(groupId))
			{
				return Deflist.DeflistClient.MoveDeflistAsync(_context, targetMylistGroup.Id, datum.Select(x => x.ItemId));
			}
			else
			{
				return MylistItem.MylistItemClient.MoveMylistItemAsync(_context, targetMylistGroup.Id, datum);
			}
		}


		/// <summary>
		/// ユーザーのマイリストグループ一覧を取得します
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public Task<List<MylistGroup.MylistGroupData>> GetUserMylistGroupAsync(string userId)
		{
			return UserMylist.UserMylistClient.GetUserMylistAsync(_context, userId);
		}



		/// <summary>
		/// マイリストグループの詳細を取得します
		/// </summary>
		/// <param name="group_id"></param>
		/// <returns></returns>
		public Task<MylistGroup.MylistGroup> GetMylistGroupDetailAsync(string group_id)
		{
			return MylistGroup.MylistGroupClient.GetMylistGroupDetailAsync(_context, group_id);
		}


		#region field

		private NiconicoContext _context;

		#endregion
	}
}
