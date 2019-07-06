using System;
using Mntone.Nico2.Live.OnAirStreams;
using System.Collections.Generic;
using System.Threading.Tasks;

#if WINDOWS_UWP
using Windows.Foundation;
using Windows.Foundation.Metadata;
#else
#endif

namespace Mntone.Nico2.Live
{
	/// <summary>
	/// ニコニコ生放送 API 群
	/// </summary>
	public sealed class LiveApi
	{
		internal LiveApi( NiconicoContext context )
		{
			this._context = context;
		}

		/// <summary>
		/// 非同期操作としてプレイヤー情報を取得します
		/// </summary>
		/// <param name="requestId">目的の生放送 ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		public IAsyncOperation<PlayerStatus.PlayerStatusResponse> GetPlayerStatusAsync( string requestId )
		{
			return PlayerStatus.PlayerStatusClient.GetPlayerStatusAsync( this._context, requestId ).AsAsyncOperation();
		}
#else
		public Task<PlayerStatus.PlayerStatusResponse> GetPlayerStatusAsync( string requestId )
		{
			return PlayerStatus.PlayerStatusClient.GetPlayerStatusAsync( this._context, requestId );
		}
#endif

		/// <summary>
		/// 非同期操作として放送中の番組一覧を取得します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		[Overload( "GetOnAirStreamsIndexAsync" )]
		public IAsyncOperation<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsIndexAsync()
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsIndexAsync( this._context ).AsAsyncOperation();
		}
#else
		public Task<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsIndexAsync()
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsIndexAsync( this._context );
		}
#endif

		/// <summary>
		/// 非同期操作として放送中の番組一覧を取得します
		/// </summary>
		/// <param name="pageIndex">目的のページ番号</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		[Overload( "GetOnAirStreamsIndexWithPageIndexAsync" )]
		public IAsyncOperation<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsIndexAsync( ushort pageIndex )
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsIndexAsync( this._context, pageIndex ).AsAsyncOperation();
		}
#else
		public Task<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsIndexAsync( ushort pageIndex )
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsIndexAsync( this._context, pageIndex );
		}
#endif

		/// <summary>
		/// 非同期操作として放送中の番組一覧を取得します
		/// </summary>
		/// <param name="pageIndex">目的のページ番号</param>
		/// <param name="category">カテゴリー</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		[Overload( "GetOnAirStreamsRecentAsync" )]
		public IAsyncOperation<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsRecentAsync( ushort pageIndex, Category category )
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsRecentAsync( this._context, pageIndex, category ).AsAsyncOperation();
		}
#else
		public Task<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsRecentAsync( ushort pageIndex, Category category )
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsRecentAsync( this._context, pageIndex, category );
		}
#endif

		/// <summary>
		/// 非同期操作として放送中の番組一覧を取得します
		/// </summary>
		/// <param name="pageIndex">目的のページ番号</param>
		/// <param name="category">カテゴリー</param>
		/// <param name="direction">ソートの方向</param>
		/// <param name="type">ソートの種類</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		[Overload( "GetOnAirStreamsRecentWithSortMethodAsync" )]
		public IAsyncOperation<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsRecentAsync(
			ushort pageIndex, Category category, Order direction, SortType type )
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsRecentAsync( this._context, pageIndex, category, direction, type ).AsAsyncOperation();
		}
#else
		public Task<OnAirStreams.OnAirStreamsResponse> GetOnAirStreamsRecentAsync(
			ushort pageIndex, Category category, Order direction, SortType type )
		{
			return OnAirStreams.OnAirStreamsClient.GetOnAirStreamsRecentAsync( this._context, pageIndex, category, direction, type );
		}
#endif

		/// <summary>
		/// [非ログオン可] 非同期操作として指定した状態の番組一覧を取得します
		/// </summary>
		/// <param name="status">目的の状態</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		[Overload( "GetOtherStreamsAsync" )]
		public IAsyncOperation<OtherStreams.OtherStreamsResponse> GetOtherStreamsAsync( StatusType status )
		{
			return OtherStreams.OtherStreamsClient.GetOtherStreamsAsync( this._context, status, 1 ).AsAsyncOperation();
		}
#else
		public Task<OtherStreams.OtherStreamsResponse> GetOtherStreamsAsync( StatusType status )
		{
			return OtherStreams.OtherStreamsClient.GetOtherStreamsAsync( this._context, status, 1 );
		}
#endif

		/// <summary>
		/// [非ログオン可] 非同期操作として指定した状態の番組一覧を取得します
		/// </summary>
		/// <param name="status">目的の状態</param>
		/// <param name="pageIndex">目的のページ番号</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		[Overload( "GetOtherStreamsWithPageIndexAsync" )]
		public IAsyncOperation<OtherStreams.OtherStreamsResponse> GetOtherStreamsAsync( StatusType status, ushort pageIndex )
		{
			return OtherStreams.OtherStreamsClient.GetOtherStreamsAsync( this._context, status, pageIndex ).AsAsyncOperation();
		}
#else
		public Task<OtherStreams.OtherStreamsResponse> GetOtherStreamsAsync( StatusType status, ushort pageIndex )
		{
			return OtherStreams.OtherStreamsClient.GetOtherStreamsAsync( this._context, status, pageIndex );
		}
#endif

		/// <summary>
		/// 非同期操作としてタイムシフト予約している一覧を取得します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		public IAsyncOperation<IReadOnlyList<string>> GetReservationsAsync()
		{
			return Reservations.ReservationsClient.GetReservationsAsync( this._context ).AsAsyncOperation();
		}
#else
		public Task<IReadOnlyList<string>> GetReservationsAsync()
		{
			return Reservations.ReservationsClient.GetReservationsAsync( this._context );
		}
#endif

		/// <summary>
		/// 非同期操作としてタイムシフト予約している一覧 (詳細) を取得します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		public IAsyncOperation<ReservationsInDetail.ReservationsInDetailResponse> GetReservationsInDetailAsync()
		{
			return ReservationsInDetail.ReservationsInDetailClient.GetReservationsInDetailAsync( this._context ).AsAsyncOperation();
		}
#else
		public Task<ReservationsInDetail.ReservationsInDetailResponse> GetReservationsInDetailAsync()
		{
			return ReservationsInDetail.ReservationsInDetailClient.GetReservationsInDetailAsync( this._context );
		}
#endif

        /// <summary>
        /// タイムシフト予約の一覧を取得します。専ら、視聴権使用後の視聴期限を確認する目的で使います。
        /// </summary>
        /// <returns></returns>
        public Task<Reservation.MyTimeshiftListData> GetMyTimeshiftListAsync()
        {
            return Reservation.ReservationClient.GetMyTimeshiftListAsync(_context);
        }


        /// <summary>
        /// タイムシフト予約を行います。
        /// </summary>
        /// <param name="liveId">生放送コンテンツID（内部的にはlv無し数字のみのコンテンツIDが必要ですが、lvがあってもトリミングするよう対応してます）</param>
        /// <param name="isOverwrite">trueを指定すると、もしも予約可能な枠が無かった場合に一番古いタイムシフト予約が強制的に削除されます。</param>
        /// <returns></returns>
        public Task<Reservation.ReservationResponse> ReservationAsync(string liveId, bool isOverwrite = false)
        {
            return Reservation.ReservationClient.ReservationAsync(_context, liveId, isOverwrite);
        }

        /// <summary>
        /// タイムシフト予約を削除するのに必要なトークン文字列を取得します。<br />
        /// トークン取得のために http://live.nicovideo.jp/my_timeshift_list へアクセスします。
        /// </summary>
        /// <returns></returns>
        public Task<Reservation.ReservationToken> GetReservationTokenAsync()
        {
            return Reservation.ReservationClient.GetReservationToken(_context);
        }


        /// <summary>
        /// タイムシフト予約を削除します。<br />
        /// </summary>
        /// <param name="liveId">生放送コンテンツID（内部的にはlv無し数字のみのコンテンツIDが必要ですが、lvがあってもトリミングするよう対応してます）</param>
        /// <param name="token">タイムシフト予約の削除用トークン。トークン取得は <see cref="GetReservationTokenAsync"/> を利用してください。 </param>
        /// <returns></returns>
        /// <remarks>Getメソッドによる削除操作を行っている都合上、削除の成功/失敗は別途タイムシフト予約一覧を再取得するなどアプリサイドでの確認が必要です。</remarks>
        public Task DeleteReservationAsync(string liveId, Reservation.ReservationToken token)
        {
            return Reservation.ReservationClient.DeleteReservationAsync(_context, liveId, token);
        }


        /// <summary>
        /// タイムシフト予約の視聴権を使用して、指定されあた生放送のタイムシフト視聴を有効化します。
        /// </summary>
        /// <param name="liveId_wo_lv"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <remarks>視聴権は使用から放送時間＋24時間の間のみ有効です。（視聴権に関わらず視聴期限内いつでも視聴可能な場合は除く）</remarks>
        public Task UseReservationAsync(string liveId_wo_lv, Reservation.ReservationToken token)
        {
            return Reservation.ReservationClient.UseReservationAsync(_context, liveId_wo_lv, token);
        }




        /// <summary>
        /// 非同期操作としてタグ リビジョンを取得します
        /// </summary>
        /// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		public IAsyncOperation<ushort> GetTagRevisionAsync( string requestId )
		{
			return TagRevision.TagRevisionClient.GetTagRevisionAsync( this._context, requestId ).AsAsyncOperation();
		}
#else
        public Task<ushort> GetTagRevisionAsync( string requestId )
		{
			return TagRevision.TagRevisionClient.GetTagRevisionAsync( this._context, requestId );
		}
#endif

		/// <summary>
		/// 非同期操作としてタグの内容を取得します
		/// </summary>
		/// <param name="requestId">目的の生放送 ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		public IAsyncOperation<Tags.TagsResponse> GetTagsAsync( string requestId )
		{
			return Tags.TagsClient.GetTagsAsync( this._context, requestId ).AsAsyncOperation();
		}
#else
		public Task<Tags.TagsResponse> GetTagsAsync( string requestId )
		{
			return Tags.TagsClient.GetTagsAsync( this._context, requestId );
		}
#endif

		/// <summary>
		/// 非同期操作としてマイ ページの内容を取得します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		public IAsyncOperation<MyPage.MyPageResponse> GetMyPageAsync()
		{
			return MyPage.MyPageClient.GetMyPageAsync( this._context ).AsAsyncOperation();
		}
#else
		public Task<MyPage.MyPageResponse> GetMyPageAsync()
		{
			return MyPage.MyPageClient.GetMyPageAsync( this._context );
		}
#endif

		/// <summary>
		/// 非同期操作として投稿キーを取得します
		/// </summary>
		/// <param name="threadId">スレッド ID</param>
		/// <param name="blockNo">ブロック番号</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		public IAsyncOperation<string> GetPostKeyAsync( uint threadId, uint blockNo )
		{
			return PostKey.PostKeyClient.GetPostKeyAsync( this._context, threadId, blockNo ).AsAsyncOperation();
		}
#else
		public Task<string> GetPostKeyAsync( uint threadId, uint blockNo )
		{
			return PostKey.PostKeyClient.GetPostKeyAsync( this._context, threadId, blockNo );
		}
#endif

		/// <summary>
		/// 非同期操作としてアンケートを投票します
		/// </summary>
		/// <param name="requestId">目的の生放送 ID</param>
		/// <param name="choiceNumber">選択した番号 (0～8)</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		public IAsyncOperation<bool> VoteAsync( string requestId, ushort choiceNumber )
		{
			return Vote.VoteClient.VoteAsync( this._context, requestId, choiceNumber ).AsAsyncOperation();
		}
#else
		public Task<bool> VoteAsync( string requestId, ushort choiceNumber )
		{
			return Vote.VoteClient.VoteAsync( this._context, requestId, choiceNumber );
		}
#endif


        /// <summary>
        /// HTML5生放送ページからLeoPlayerPropsを取得します。
        /// </summary>
        /// <param name="liveId"></param>
        /// <returns></returns>
        /// <remarks>Deprecated</remarks>
        public Task<Watch.LeoPlayerProps> GetLeoPlayerPropsAsync(string liveId)
        {
            return Watch.WatchClient.GetLeoPlayerPropsAsync(_context, liveId);
        }


        /// <summary>
        /// ニコニコ動画（く）で変更されたHTML5生放送ページからLeoPlayerPropsを取得します
        /// </summary>
        /// <param name="liveId"></param>
        /// <returns></returns>
        public Task<Watch.Crescendo.CrescendoLeoProps> GetCrescendoLeoPlayerPropsAsync(string liveId)
        {
            return Watch.WatchClient.GetCrescendoLeoPlayerPropsAsync(_context, liveId);
        }


        /// <summary>
        /// 放送情報を取得します。<br />
        /// 配信終了した放送の情報も取得できるため、<see cref="GetLiveVideoInfoAsync"/> の代替として利用するケースが考えられます。
        /// </summary>
        /// <param name="liveId">ニコニコ生放送コンテンツID（先頭lv有り）</param>
        /// <returns></returns>
        /// <remarks>公式の放送では情報を取得できません。公式放送には <see cref="GetLiveVideoInfoAsync"/> を使用してください。</remarks>
        public Task<Watch.ProgramInfo> GetProgramInfoAsync(string liveId)
        {
            return Watch.WatchClient.GetProgramInfoAsync(_context, liveId);
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="liveId"></param>
        /// <returns></returns>
        public Task<Video.NicoliveVideoInfoResponse> GetLiveVideoInfoAsync(string liveId)
        {
            return Video.LiveVideoClient.LiveVideoInfoSubClient.GetLiveInfoAsync(_context, liveId);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="liveId"></param>
        /// <returns></returns>
        public Task<Video.NicoliveCommunityVideoResponse> GetLiveCommunityVideoAsync(string communityOrChannelId)
        {
            return Video.LiveVideoClient.LiveCommunityVideoSubClient.GetLiveCommunityVideoAsync(_context, communityOrChannelId);
        }


        /// <summary>
        /// 指定したIDの公式生放送やチャンネル生放送に関するオススメ生放送コンテンツを取得します。<br />
        /// ユーザー生放送の場合は <seealso cref="GetCommunityRecommendAsync"/> を使用してください。
        /// </summary>
        /// <param name="officialOrChannelLiveId">lvで始まるニコニコ生放送コンテンツID</param>
        /// <returns></returns>
        public Task<Recommend.LiveRecommendResponse> GetOfficialOrChannelLiveRecommendAsync(string officialOrChannelLiveId)
        {
            return Recommend.RecommendClient.GetOfficialOrChannelLiveRecommendAsync(_context, officialOrChannelLiveId);
        }

        /// <summary>
        /// 指定したIDのユーザー生放送に関するオススメ生放送コンテンツを取得します。<br />
        /// 公式生放送やチャンネル生放送の場合は <seealso cref="GetOfficialOrChannelLiveRecommendAsync"/> を使用してください。
        /// </summary>
        /// <param name="liveId">lvで始まるニコニコ生放送コンテンツID</param>
        /// <param name="communityId">coで始まるニコニコミュニティID</param>
        /// <returns></returns>
        public Task<Recommend.LiveRecommendResponse> GetCommunityRecommendAsync(string liveId, string communityId)
        {
            return Recommend.RecommendClient.GetCommunityLiveRecommendAsync(_context, liveId, communityId);
        }



        public Task<string> GetWaybackKeyAsync(string threadId)
        {
            return WaybackKey.WaybackKeyClient.GetWaybackKeyAsync(_context, threadId);
        }






        #region field

        private NiconicoContext _context;

		#endregion
	}
}