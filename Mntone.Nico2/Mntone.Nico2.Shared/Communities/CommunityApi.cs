using System.Collections.Generic;

#if WINDOWS_APP
using System;
using Windows.Foundation;
using Windows.Storage.Streams;
#else
using System.Threading.Tasks;
#endif

namespace Mntone.Nico2.Communities
{
	/// <summary>
	/// ニコニコ コミュニティー API 群
	/// </summary>
	public sealed class CommunityApi
	{
		internal CommunityApi( NiconicoContext context )
		{
			this._context = context;
		}

		/// <summary>
		/// [非ログオン可] 非同期操作としてコミュニティー アイコンを取得します
		/// </summary>
		/// <param name="requestCommunityId">目的のコミュニティー ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<IBuffer> GetIconAsync( string requestCommunityId )
		{
			return Icon.IconClient.GetIconAsync( this._context, requestCommunityId ).AsAsyncOperation();
		}
#else
		public Task<byte[]> GetIconAsync( string requestCommunityId )
		{
			return Icon.IconClient.GetIconAsync( this._context, requestCommunityId );
		}
#endif



		/// <summary>
		/// コミュニティ情報を取得します
		/// </summary>
		/// <param name="communityId"></param>
		/// <returns></returns>
		public Task<Info.NicovideoCommunityResponse> GetCommunifyInfoAsync(string communityId)
		{
			return Info.InfoClient.GetCommunityInfoAsync(this._context, communityId);
		}


		/// <summary>
		/// コミュニティ情報を取得します
		/// </summary>
		/// <param name="communityId"></param>
		/// <returns></returns>
		public Task<Detail.CommunityDetailResponse> GetCommunityDetailAsync(string communityId)
		{
			return Detail.DetailClient.GetCommunityDetailAsync(this._context, communityId);
		}

		#region field

		private NiconicoContext _context;

		#endregion
	}
}