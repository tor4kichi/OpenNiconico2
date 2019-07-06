using System;
using Mntone.Nico2.Videos.Ranking;
using System.Collections.Generic;
using System.Threading.Tasks;

#if WINDOWS_UWP
using Windows.Foundation;
using Windows.Storage.Streams;
#else
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
#if WINDOWS_UWP
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


		/// <summary>
		/// コミュニティの生放送状況を取得します
		/// </summary>
		/// <param name="communityId"></param>
		/// <returns></returns>
		public Task<Live.NicoliveVideoResponse> GetCommunityLiveInfoAsync(string communityId)
		{
			return Live.LiveInfoClient.GetCommunityLiveInfo(this._context, communityId);
		}



		/// <summary>
		/// コミュニティに登録された動画を取得します。
		/// </summary>
		/// <param name="communityId">"co"から始まるコミュニティID</param>
		/// <param name="page">1 以上のページ数</param>
		/// <returns></returns>
		public Task<RssVideoResponse> GetCommunityVideoAsync(string communityId, uint page)
		{
			return Video.CommunityVideoClient.GetCommunityVideosAsync(_context, communityId, page);
		}

		#region field

		private NiconicoContext _context;

		#endregion
	}
}