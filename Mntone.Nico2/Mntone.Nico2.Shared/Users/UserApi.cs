﻿using System.Collections.Generic;

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

		#region field

		private NiconicoContext _context;

		#endregion
	}
}