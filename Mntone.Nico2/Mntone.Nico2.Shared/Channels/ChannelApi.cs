using System.Collections.Generic;
using System;
using System.Threading.Tasks;

#if WINDOWS_UWP
using Windows.Foundation;
using Windows.Storage.Streams;
#else
#endif

namespace Mntone.Nico2.Channels
{
	/// <summary>
	/// ニコニコ チャンネル API 群
	/// </summary>
	public sealed class ChannelApi
	{
		internal ChannelApi( NiconicoContext context )
		{
			this._context = context;
		}

		/// <summary>
		/// [非ログオン可] 非同期操作としてチャンネル アイコンを取得します
		/// </summary>
		/// <param name="requestChannelId">目的のチャンネル ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		public IAsyncOperation<IBuffer> GetIconAsync( string requestChannelId )
		{
			return Icon.IconClient.GetIconAsync( this._context, requestChannelId ).AsAsyncOperation();
		}
#else
		public Task<byte[]> GetIconAsync( string requestChannelId )
		{
			return Icon.IconClient.GetIconAsync( this._context, requestChannelId );
		}
#endif


        public Task<Channels.Info.ChannelInfo> GetChannelInfo(string channelId)
        {
            return Channels.Info.ChannelInfoClient.GetChannelInfoAsync(_context, channelId);
        }

        public Task<Channels.Video.ChannelVideoResponse> GetChannelVideosAsync(string channelId, int page)
        {
            return Channels.Video.ChannelVideoClient.GetChannelVideosAsync(_context, channelId, page);
        }


		#region field

		private NiconicoContext _context;

		#endregion
	}
}