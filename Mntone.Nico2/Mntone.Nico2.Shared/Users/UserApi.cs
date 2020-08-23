using Mntone.Nico2.Mylist;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Mntone.Nico2.Users.Mylist;
using Mntone.Nico2.Users.Follow;

#if WINDOWS_UWP
using Windows.Foundation;
using Windows.Storage.Streams;
#else
#endif

namespace Mntone.Nico2.Users
{
	/// <summary>
	/// ニコニコ ユーザー API 群
	/// </summary>
	public sealed class UserApi
	{
		internal UserApi( NiconicoContext context )
		{
			this._context = context;
		}

		/// <summary>
		/// [非ログオン可] 非同期操作としてユーザー アイコンを取得します
		/// </summary>
		/// <param name="requestUserId">目的のユーザー ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		public IAsyncOperation<IBuffer> GetIconAsync( uint requestUserId )
		{
			return Icon.IconClient.GetIconAsync( this._context, requestUserId ).AsAsyncOperation();
		}
#else
		public Task<byte[]> GetIconAsync( uint requestUserId )
		{
			return Icon.IconClient.GetIconAsync( this._context, requestUserId );
		}
#endif

		/// <summary>
		/// 非同期操作としてユーザー情報を取得します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		public IAsyncOperation<Info.InfoResponse> GetInfoAsync()
		{
			return Info.InfoClient.GetInfoAsync( this._context ).AsAsyncOperation();
		}
#else
		public Task<Info.InfoResponse> GetInfoAsync()
		{
			return Info.InfoClient.GetInfoAsync( this._context );
		}
#endif


		/// <summary>
		/// 指定したユーザーIDのユーザー情報を取得します。
		/// </summary>
		/// <param name="requestUserId"></param>
		/// <returns></returns>
		/// <remarks>ログイン不要</remarks>
		public Task<User.UserResponse> GetUserAsync(string requestUserId)
		{
			return User.UserClient.GetUserAsync(_context, requestUserId);
		}


		/// <summary>
		/// ログイン中のユーザーのお気に入り登録しているユーザーの一覧を取得します。
		/// </summary>
		/// <returns></returns>
		public Task<Follow.FollowUsersResponse> GetFollowUsersAsync(uint pageSize = 25, Follow.FollowUsersResponse lastFollowUserRes = null)
		{
			return Follow.FollowClient.GetFollowUsersAsync(_context, pageSize, lastFollowUserRes);
		}



		/// <summary>
		/// ログイン中ユーザーがお気に入り登録しているタグの一覧を取得します。
		/// </summary>
		/// <returns></returns>
		public Task<FollowTagsResponse> GetFollowTagsAsync()
		{
			return Follow.FollowClient.GetFollowTagsAsync(_context);
		}


		/// <summary>
		/// ログイン中のユーザーがお気に入り登録しているマイリストの一覧を取得します。
		/// </summary>
		/// <returns></returns>
		public Task<FollowMylistResponse> GetFollowMylistsAsync(uint sampleItemsCount = 3)
		{
			return Follow.FollowClient.GetFollowMylistAsync(_context, sampleItemsCount);
		}


		/// <summary>
		/// 指定したアイテムがログイン中ユーザーにお気に入り登録されているかをチェックします。
		/// </summary>
		/// <param name="itemType"></param>
		/// <param name="item_id"></param>
		/// <returns></returns>
		public Task<ContentManageResult> ExistUserFollowAsync(NiconicoItemType itemType, string item_id)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// 指定したアイテムをログイン中ユーザーのお気に入りに登録します。
		/// </summary>
		/// <param name="itemType"></param>
		/// <param name="item_id"></param>
		/// <returns></returns>
		/// <remarks>tagのお気に入り登録は AddFollowTagAsync を利用してください</remarks>
		public Task<ContentManageResult> AddUserFollowAsync(NiconicoItemType itemType, string item_id)
		{
			throw new NotImplementedException();
		}


		/// <summary>
		/// 指定したアイテムをログイン中ユーザーのお気に入り登録から削除します。
		/// </summary>
		/// <param name="itemType"></param>
		/// <param name="item_id"></param>
		/// <returns></returns>
		/// <remarks>タグのお気に入り解除には RemoveFollowTagAsync を利用してください</remarks>
		public Task<ContentManageResult> RemoveUserFollowAsync(NiconicoItemType itemType, string item_id)
		{
			throw new NotImplementedException();
		}


		/// <summary>
		/// 指定されたタグをログイン中ユーザーのお気に入りに登録します。
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		/// <remarks>タグはコンテンツではないため、お気に入りAPIのアクセス手段が他と異なります。</remarks>
		public Task<ContentManageResult> AddFollowTagAsync(string tag)
		{
			throw new NotImplementedException();
		}


		/// <summary>
		/// 指定されたタグをログイン中ユーザーのお気に入り登録から削除します。
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		/// <remarks>Tagの文字列に全角の数字が含まれる場合は、すべて半角に変換して扱う必要があります。</remarks>
		public Task<ContentManageResult> RemoveFollowTagAsync(string tag)
		{
			throw new NotImplementedException();
		}




		/// <summary>
		/// ユーザーのプロフィールを含む詳細データを取得します。
		/// データ取得のためにニコニコ動画のユーザーページへアクセスします。
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public Task<User.UserDetail> GetUserDetail(uint userId)
		{
			return GetUserDetail(userId.ToString());
		}

		/// <summary>
		/// ユーザーのプロフィールを含む詳細データを取得します。
		/// データ取得のためにニコニコ動画のユーザーページへアクセスします。
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public Task<User.UserDetail> GetUserDetail(string userId)
		{
			return User.UserClient.GetUserDetailAsync(_context, userId);

		}


		public Task<Video.UserVideoResponse> GetUserVideos(uint userId, uint page, Sort sortMethod = Sort.FirstRetrieve, Order sortDir = Order.Descending)
		{
			return Video.UserVideoClient.GetUserAsync(_context, userId, page, sortMethod, sortDir);
		}





		public Task<NG.NGCommentResponse> GetNGComment()
		{
			return NG.NGClient.GetNGCommentAsync(_context);
		}


		public Task<NG.NGCommentResponseCore> AddNGComment(NG.NGCommentType type, string source)
		{
			return NG.NGClient.AddNGCommentAsync(_context, type, source);
		}

		public Task<NG.NGCommentResponseCore> DeleteNGComment(NG.NGCommentType type, string source)
		{
			return NG.NGClient.DeleteNGCommentAsync(_context, type, source);
		}



		#region Mylist




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
		public Task<MylistGroupsResponse> GetLoginUserMylistGroupsAsync()
		{
			return MylistClient.GetLoginUserMylistGroupsAsync(_context);
		}

		public Task<MylistGroupsResponse> GetMylistGroupsAsync(int userId)
		{
			return MylistClient.GetMylistGroupsAsync(_context, userId);
		}


		/// <summary>
		/// ログイン中のユーザーの新しいマイリストグループを作成
		/// </summary>
		/// <returns></returns>
		public Task<ContentManageResult> CreateMylistGroupAsync(string name, string description, bool is_public, MylistDefaultSort default_sort, IconType iconType)
		{
			return MylistClient.AddMylistGroupAsync(_context, name, description, is_public, default_sort, iconType);
		}


		/// <summary>
		/// ログイン中のユーザーの新しいマイリストグループを作成
		/// </summary>
		/// <returns></returns>
		public Task<ContentManageResult> CreateMylistGroupAsync(MylistGroupData groupData)
		{
			return MylistClient.AddMylistGroupAsync(_context, groupData.Name, groupData.Description, groupData.GetIsPublic(), MylistDefaultSort.FirstRetrieve_Descending, groupData.GetIconType());
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
			return MylistClient.UpdateMylistGroupAsync(_context, group_id, name, description, is_public, default_sort, iconType);
		}


		/// <summary>
		/// ログイン中のユーザーのマイリストグループを更新
		/// </summary>
		/// <param name="groupData"></param>
		/// <returns></returns>
		public Task<ContentManageResult> UpdateMylistGroupAsync(MylistGroupData groupData)
		{
			return MylistClient.UpdateMylistGroupAsync(_context, groupData.Id, groupData.Name, groupData.Description, groupData.GetIsPublic(), MylistDefaultSort.FirstRetrieve_Descending, groupData.GetIconType());
		}


		/// <summary>
		/// ログイン中のユーザーのマイリストグループを削除
		/// </summary>
		/// <param name="group_id">削除対象のマイリストグループID(</param>
		/// <returns></returns>
		public Task<ContentManageResult> RemoveMylistGroupAsync(string group_id)
		{
			return MylistClient.RemoveMylistGroupAsync(_context, group_id);
		}


		/// <summary>
		/// ログイン中のユーザーのマイリストグループを削除
		/// </summary>
		/// <param name="group_id">削除対象のマイリストグループID(</param>
		/// <returns></returns>
		public Task<ContentManageResult> RemoveMylistGroupAsync(MylistGroupData groupData)
		{
			return MylistClient.RemoveMylistGroupAsync(_context, groupData.Id);
		}


		public Task<WatchAfterMylistGroupItemsResponse> GetWatchAfterMylistGroupItemsAsync(MylistSortKey sortKey, MylistSortOrder sortOrder, uint pageSize = 25, uint page = 0)
		{
			return MylistClient.GetWatchAfterMylistGroupItemsAsync(_context, sortKey, sortOrder, pageSize, page);
		}

		/// <summary>
		/// マイリストに登録されたアイテムの一覧を取得
		/// </summary>
		/// <param name="group_id">マイリストグループID</param>
		/// <returns></returns>
		/// <remarks>http://api.ce.nicovideo.jp/nicoapi/v1 を利用してマイリストを取得します。</remarks>
		public Task<MylistGroupItemsResponse> GetLoginUserMylistGroupItemsAsync(long group_id, MylistSortKey sortKey, MylistSortOrder sortOrder, uint pageSize = 25, uint page = 0)
		{
			return MylistClient.GetLoginUserMylistGroupItemsAsync(_context, group_id, sortKey, sortOrder, pageSize, page);
		}


		public Task<MylistGroupItemsResponse> GetMylistGroupItemsAsync(long group_id, MylistSortKey sortKey, MylistSortOrder sortOrder, uint pageSize = 25, uint page = 0)
		{
			return MylistClient.GetMylistGroupItemsAsync(_context, group_id, sortKey, sortOrder, pageSize, page);
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
			if (LoginUserMylistGroupData.IsDeflist(group_id))
			{
				return MylistClient.AddDeflistAsync(_context, item_type, item_id, description);
			}
			else
			{
				return MylistClient.AddMylistItemAsync(_context, group_id, item_type, item_id, description);
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
			return MylistClient.AddMylistItemAsync(_context, mylistData.GroupId, mylistData.ItemType, mylistData.ItemId, mylistData.Description);
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
			if (LoginUserMylistGroupData.IsDeflist(group_id))
			{
				return MylistClient.UpdateDeflistAsync(_context, item_type, item_id, description);
			}
			else
			{
				return MylistClient.UpdateMylistItemAsync(_context, group_id, item_type, item_id, description);
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
			if (LoginUserMylistGroupData.IsDeflist(group_id))
			{
				return MylistClient.RemoveDeflistAsync(_context, item_type, itemIdList);
			}
			else
			{
				return MylistClient.RemoveMylistItemAsync(_context, group_id, item_type, itemIdList);
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

			if (LoginUserMylistGroupData.IsDeflist(target_group_id))
			{
				// とりあえずマイリストへのコピーはサポートしていない
				throw new NotSupportedException("not support mylist item copy to Deflist(とりあえずマイリスト)");
			}

			if (LoginUserMylistGroupData.IsDeflist(group_id))
			{
				return MylistClient.CopyDeflistAsync(_context, target_group_id, itemType, itemIdList);
			}
			else
			{
				return MylistClient.CopyMylistItemAsync(_context, group_id, target_group_id, itemType, itemIdList);
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

			if (LoginUserMylistGroupData.IsDeflist(target_group_id))
			{
				// とりあえずマイリストへの移動はサポートしていない
				throw new NotSupportedException("not support mylist item move to Deflist(とりあえずマイリスト)");
			}

			if (LoginUserMylistGroupData.IsDeflist(group_id))
			{
				return MylistClient.MoveDeflistAsync(_context, target_group_id, itemType, itemIdList);
			}
			else
			{
				return MylistClient.MoveMylistItemAsync(_context, group_id, target_group_id, itemType, itemIdList);
			}

		}

		/// <summary>
		/// マイリストグループの詳細を取得します
		/// </summary>
		/// <param name="group_id"></param>
		/// <returns></returns>
		public Task<MylistGroupDetail> GetMylistGroupDetailAsync(string group_id)
		{
			return MylistClient.GetMylistGroupDetailAsync(_context, group_id);
		}

        public Task<MylistGroupDetail> GetDeflistMylistGroupDetailAsync()
        {
            return MylistClient.GetMylistGroupDetailAsync(_context, DefMylistId);
        }

        /// <summary>
        /// とりあえずマイリストのマイリストID
        /// </summary>
        public const string DefMylistId = "0";



        #endregion


        #region Follow Community

        public Task<FollowCommunityResponse> GetFollowCommunityAsync(uint limit = 25, uint page = 0)
		{
			return Follow.FollowClient.GetFollowCommunityAsync(_context, limit, page);
		}


		public Task<bool> AddFollowCommunityAsync(string communityId, string title = "", string comment = "", bool notify = false)
		{
			return FollowCommunity.FollowCommunityClient.AddFollowCommunity(_context, communityId, title, comment, notify);
		}

		public Task<FollowCommunity.CommunityLeaveToken> GetFollowCommunityLeaveTokenAsync(string communityId)
		{
			return FollowCommunity.FollowCommunityClient.GetCommunityLeaveToken(_context, communityId);
		}

		public Task<bool> RemoveFollowCommunityAsync(FollowCommunity.CommunityLeaveToken leaveToken)
		{
			return FollowCommunity.FollowCommunityClient.RemoveFollowCommunity(_context, leaveToken);
		}

        #endregion


        #region Follow Channel


        public Task<FollowChannelResponse> GetFollowChannelAsync(uint limit = 25, uint page = 0)
        {
            return FollowClient.GetFollowChannelAsync(_context, limit, page);
        }


        public Task<Users.Follow.ChannelFollowResult> AddFollowChannelAsync(string channelId)
        {
            return Users.Follow.FollowClient.AddFollowChannelAsync(_context, channelId);
        }

        public Task<Users.Follow.ChannelFollowResult> DeleteFollowChannelAsync(string channelId)
        {
            return Users.Follow.FollowClient.DeleteFollowChannelAsync(_context, channelId);
        }

		#endregion


		#region Series

		public Task<Series.UserSeriesResponse> GetUserSeiresAsync(string userId, uint page, uint pageSize = 25)
		{
			return Series.SeriesClient.GetUserSeriesAsync(_context, userId, page, pageSize);
		}

		#endregion

		#region field

		private NiconicoContext _context;

		#endregion
	}
}