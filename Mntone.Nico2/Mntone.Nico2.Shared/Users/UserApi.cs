using System.Collections.Generic;

#if WINDOWS_APP
using System;
using Windows.Foundation;
using Windows.Storage.Streams;
#else
using System.Threading.Tasks;
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
#if WINDOWS_APP
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
#if WINDOWS_APP
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
		public Task<User.User> GetUserAsync(string requestUserId)
		{
			return User.UserClient.GetUserAsync(_context, requestUserId);
		}


		/// <summary>
		/// ログイン中のユーザーのお気に入り登録しているユーザーの一覧を取得します。
		/// </summary>
		/// <returns></returns>
		public Task<List<Fav.FavData>> GetFavUsersAsync()
		{
			return Fav.FavClient.GetFavUsersAsync(_context);
		}



		/// <summary>
		/// ログイン中ユーザーがお気に入り登録しているタグの一覧を取得します。
		/// </summary>
		/// <returns></returns>
		public Task<List<string>> GetFavTagsAsync()
		{
			return Fav.FavClient.GetFavTagsAsync(_context);
		}


		/// <summary>
		/// ログイン中のユーザーがお気に入り登録しているマイリストの一覧を取得します。
		/// </summary>
		/// <returns></returns>
		public Task<List<Fav.FavData>> GetFavMylistsAsync()
		{
			return Fav.FavClient.GetFavMylistAsync(_context);
		}


		/// <summary>
		/// 指定したアイテムがログイン中ユーザーにお気に入り登録されているかをチェックします。
		/// </summary>
		/// <param name="itemType"></param>
		/// <param name="item_id"></param>
		/// <returns></returns>
		public Task<ContentManageResult> ExistUserFavAsync(NiconicoItemType itemType, string item_id)
		{
			return Fav.FavClient.ExistFavAsync(_context, itemType, item_id);
		}

		/// <summary>
		/// 指定したアイテムをログイン中ユーザーのお気に入りに登録します。
		/// </summary>
		/// <param name="itemType"></param>
		/// <param name="item_id"></param>
		/// <returns></returns>
		/// <remarks>tagのお気に入り登録は AddFavtagAsync を利用してください</remarks>
		public Task<ContentManageResult> AddUserFavAsync(NiconicoItemType itemType, string item_id)
		{
			return Fav.FavClient.AddFavAsync(_context, itemType, item_id);
		}


		/// <summary>
		/// 指定したアイテムをログイン中ユーザーのお気に入り登録から削除します。
		/// </summary>
		/// <param name="itemType"></param>
		/// <param name="item_id"></param>
		/// <returns></returns>
		/// <remarks>タグのお気に入り解除には RemoveFavTagAsync を利用してください</remarks>
		public Task<ContentManageResult> RemoveUserFavAsync(NiconicoItemType itemType, string item_id)
		{
			return Fav.FavClient.RemoveFavAsync(_context, itemType, item_id);
		}


		/// <summary>
		/// 指定されたタグをログイン中ユーザーのお気に入りに登録します。
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		/// <remarks>タグはコンテンツではないため、お気に入りAPIのアクセス手段が他と異なります。</remarks>
		public Task<ContentManageResult> AddFavTagAsync(string tag)
		{
			return Fav.FavClient.AddFavTagAsync(_context, tag);
		}


		/// <summary>
		/// 指定されたタグをログイン中ユーザーのお気に入り登録から削除します。
		/// </summary>
		/// <param name="tag"></param>
		/// <returns></returns>
		/// <remarks>Tagの文字列に全角の数字が含まれる場合は、すべて半角に変換して扱う必要があります。</remarks>
		public Task<ContentManageResult> RemoveFavTagAsync(string tag)
		{
			return Fav.FavClient.RemoveFavTagAsync(_context, tag);
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



		#region field

		private NiconicoContext _context;

		#endregion
	}
}