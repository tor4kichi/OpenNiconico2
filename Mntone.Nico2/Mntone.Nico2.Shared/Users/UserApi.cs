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



		public Task<bool> IsFollowingUserAsync(uint userId)
        {
			return Follow.FollowClient.IsFollowingUserAsync(_context, userId);
		}


		public Task<ContentManageResult> AddFollowUserAsync(string userId)
		{
			return Follow.FollowClient.AddFollowUserAsync(_context, userId);
		}



		public Task<ContentManageResult> RemoveFollowUserAsync(string userId)
		{
			return Follow.FollowClient.RemoveFollowUserAsync(_context, userId);
		}



		/*
		public Task<bool> IsFollowingTagAsync(string tag)
		{
			return Follow.FollowClient.IsFollowingTagAsync(_context, tag);
		}
		*/

		public Task<ContentManageResult> AddFollowTagAsync(string tag)
		{
			return Follow.FollowClient.AddFollowTagAsync(_context, tag);
		}


		public Task<ContentManageResult> RemoveFollowTagAsync(string tag)
		{
			return Follow.FollowClient.RemoveFollowTagAsync(_context, tag);
		}


		/*
		public Task<bool> IsFollowingMylistAsync(string userId)
		{
			return Follow.FollowClient.IsFollowingMylistAsync(_context, userId);
		}
		*/

		public Task<ContentManageResult> AddFollowMylistAsync(string mylistId)
		{
			return Follow.FollowClient.AddFollowMylistAsync(_context, mylistId);
		}


		public Task<ContentManageResult> RemoveFollowMylistAsync(string mylistId)
		{
			return Follow.FollowClient.RemoveFollowMylistAsync(_context, mylistId);
		}


		/// <summary>
		/// ユーザーのプロフィールを含む詳細データを取得します。
		/// データ取得のためにニコニコ動画のユーザーページへアクセスします。
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public Task<User.UserDetailResponse.UserDetails> GetUserDetailAsync(uint userId)
		{
			return GetUserDetailAsync(userId.ToString());
		}

		/// <summary>
		/// ユーザーのプロフィールを含む詳細データを取得します。
		/// データ取得のためにニコニコ動画のユーザーページへアクセスします。
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public Task<User.UserDetailResponse.UserDetails> GetUserDetailAsync(string userId)
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
		public Task<string> CreateMylistGroupAsync(string name, string description, bool isPublic, MylistSortKey defaultSortKey, MylistSortOrder defaultSortOrder)
		{
			return MylistClient.AddMylistGroupAsync(_context, name, description, isPublic, defaultSortKey, defaultSortOrder);
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
		public Task<bool> UpdateMylistGroupAsync(string mylist, string name, string description, bool isPublic, MylistSortKey defaultSortKey, MylistSortOrder defaultSortOrder)
		{
			return MylistClient.UpdateMylistGroupAsync(_context, mylist, name, description, isPublic, defaultSortKey, defaultSortOrder);
		}

		/// <summary>
		/// ログイン中のユーザーのマイリストグループを削除
		/// </summary>
		/// <param name="group_id">削除対象のマイリストグループID(</param>
		/// <returns></returns>
		public Task<bool> RemoveMylistGroupAsync(string mylist)
		{
			return MylistClient.RemoveMylistGroupAsync(_context, mylist);
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
		public Task<ContentManageResult> RemoveMylistItemAsync(string group_id, params string[] itemIdList)
		{
			if (LoginUserMylistGroupData.IsDeflist(group_id))
			{
				return MylistClient.RemoveDeflistAsync(_context, itemIdList);
			}
			else
			{
				return MylistClient.RemoveMylistItemAsync(_context, group_id, itemIdList);
			}
		}







		/// <summary>
		/// マイリストアイテムを別のマイリストへコピーします
		/// </summary>
		/// <param name="targetMylistGroup"></param>
		/// <param name="datum"></param>
		/// <returns></returns>
		/// <remarks>ターゲットにはとりあえずマイリストを指定することはできません</remarks>
		public Task<ContentManageResult> CopyMylistItemAsync(string group_id, string target_group_id, params string[] itemIdList)
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
				return MylistClient.CopyDeflistAsync(_context, target_group_id, itemIdList);
			}
			else
			{
				return MylistClient.CopyMylistItemAsync(_context, group_id, target_group_id, itemIdList);
			}

		}

		/// <summary>
		/// マイリストのアイテムを別のマイリストに移動します。
		/// </summary>
		/// <param name="targetMylistGroup"></param>
		/// <param name="datum"></param>
		/// <returns></returns>
		/// <remarks>ターゲットにはとりあえずマイリストを指定することは出来ません。</remarks>
		public Task<ContentManageResult> MoveMylistItemAsync(string group_id, string target_group_id, params string[] itemIdList)
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
				return MylistClient.MoveDeflistAsync(_context, target_group_id, itemIdList);
			}
			else
			{
				return MylistClient.MoveMylistItemAsync(_context, group_id, target_group_id, itemIdList);
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

		public Task<UserOwnedCommunityResponse> GetUserOwnedCommunitiesAsync(uint userId)
        {
			return FollowClient.GetUserOwnedCommunitiesAsync(_context, userId);
        }

		public Task<CommunityAuthorityResponse> GetCommunityAuthorityAsync(string communityId)
        {
			return FollowClient.GetCommunityAuthorityAsync(_context, communityId);
		}

        public Task<FollowCommunityResponse> GetFollowCommunityAsync(uint limit = 25, uint offset = 0)
		{
			return Follow.FollowClient.GetFollowCommunityAsync(_context, limit, offset);
		}


		public Task<ContentManageResult> AddFollowCommunityAsync(string communityId)
		{
			return FollowClient.AddFollowCommunityAsync(_context, communityId);
		}

		public Task<ContentManageResult> RemoveFollowCommunityAsync(string communityId)
		{
			return FollowClient.RemoveFollowCommunityAsync(_context, communityId);
		}

		#endregion


		#region Follow Channel

		public Task<ChannelAuthorityResponse> GetChannelAuthorityAsync(uint channelNumberId)
		{
			return FollowClient.GetChannelAuthorityAsync(_context, channelNumberId);
		}

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


        #region Likes

		public Task<Likes.LikeActionResponse> DoLikeVideoAsync(string videoId)
        {
			return Likes.UserLikesClient.DoLikeVideoAsync(_context, videoId);
        }

		public Task<Likes.LikeActionResponse> UnDoLikeVideoAsync(string videoId)
		{
			return Likes.UserLikesClient.UnDoLikeVideoAsync(_context, videoId);
		}

		public Task<Likes.LikesListResponse> GetLikesAsync(int page, int pageSize = 100)
        {
			return Likes.UserLikesClient.GetLikesAsync(_context, page, pageSize);
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