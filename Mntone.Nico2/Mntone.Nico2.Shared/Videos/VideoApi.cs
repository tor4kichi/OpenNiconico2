using System;
using Mntone.Nico2.Mylist;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mntone.Nico2.Users.Video;

#if WINDOWS_UWP
using Windows.Foundation;
using Windows.Foundation.Metadata;
#else
#endif

namespace Mntone.Nico2.Videos
{
	/// <summary>
	/// ニコニコ動画 API 群
	/// </summary>
	public sealed class VideoApi
	{
		internal VideoApi( NiconicoContext context )
		{
			this._context = context;
		}


		



		/// <summary>
		/// 非同期操作として flv 情報を取得します
		/// </summary>
		/// <param name="requestId">目的の動画 ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		[Overload( "GetFlvAsync" )]
		public IAsyncOperation<Flv.FlvResponse> GetFlvAsync( string requestId )
		{
			return Flv.FlvClient.GetFlvAsync( _context, requestId ).AsAsyncOperation();
		}
#else
		public Task<Flv.FlvResponse> GetFlvAsync( string requestId )
		{
			return Flv.FlvClient.GetFlvAsync( _context, requestId );
		}
#endif

		/// <summary>
		/// 非同期操作として videoviewhistory/list 情報を取得します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		public IAsyncOperation<Histories.HistoriesResponse> GetHistoriesAsync()
		{
			return Histories.HistoriesClient.GetHistoriesAsync( _context ).AsAsyncOperation();
		}
#else
		public Task<Histories.HistoriesResponse> GetHistoriesAsync()
		{
			return Histories.HistoriesClient.GetHistoriesAsync( _context );
		}
#endif


        /// <summary>
        /// マイページの視聴履歴から視聴履歴情報を取得します
        /// </summary>
        /// <returns></returns>
        public Task<Histories.HistoriesResponse> GetHistoriesFromMyPageAsync()
        {
            return Histories.HistoriesClient.GetHistoriesFromMyPageAsync(_context);
        }


        /// <summary>
        /// 非同期操作として videoviewhistory/remove で履歴を削除します
        /// </summary>
        /// <param name="token">視聴履歴を取得したときに取得したトークン</param>
        /// <param name="requestId">目的の動画 ID</param>
        /// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		public IAsyncOperation<RemoveHistory.RemoveHistoryResponse> RemoveHistoryAsync( string token, string requestId )
		{
			return RemoveHistory.RemoveHistoryClient.RemoveHistoryAsync( _context, token, requestId ).AsAsyncOperation();
		}
#else
        public Task<RemoveHistory.RemoveHistoryResponse> RemoveHistoryAsync( string token, string requestId )
		{
			return RemoveHistory.RemoveHistoryClient.RemoveHistoryAsync( _context, token, requestId );
		}
#endif


		/// <summary>
		/// 非同期操作として videoviewhistory/remove ですべての履歴を削除します
		/// </summary>
		/// <param name="token">視聴履歴を取得したときに取得したトークン</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_UWP
		public IAsyncOperation<RemoveHistory.RemoveHistoryResponse> RemoveAllHistoriesAsync( string token )
		{
			return RemoveHistory.RemoveHistoryClient.RemoveAllHistoriesAsync( _context, token ).AsAsyncOperation();
		}
#else
		public Task<RemoveHistory.RemoveHistoryResponse> RemoveAllHistoriesAsync( string token )
		{
			return RemoveHistory.RemoveHistoryClient.RemoveAllHistoriesAsync( _context, token );
		}
#endif

        /// <summary>
        /// 動画情報を元にして動画コメントを取得します。
        /// </summary>
        /// <param name="flvResponse"></param>
        /// <returns></returns>
        public Task<Comment.CommentResponse> GetCommentAsync(int userId, string commentServerUrl, int threadId, bool isKeyRequired)
		{
			return Comment.CommentClient.GetCommentAsync(_context, userId, commentServerUrl, threadId, isKeyRequired);
		}

       

        /// <summary>
        /// ニコニコ動画へのキーワード検索を行い結果を取得します。
        /// </summary>
        /// <param name="SearchResponse"></param>
        /// <returns></returns>
        public Task<Search.SearchResponse> GetKeywordSearchAsync(string keyword, uint pageCount, Sort sortMethod, Order sortDir = Order.Descending)
		{
			return Search.SearchClient.GetKeywordSearchAsync(_context, keyword, pageCount, sortMethod, sortDir);
		}

		/// <summary>
		/// ニコニコ動画へのキーワード検索を行い結果を取得します。
		/// </summary>
		/// <param name="SearchResponse"></param>
		/// <returns></returns>
		public Task<Search.SearchResponse> GetTagSearchAsync(string tag, uint pageCount, Sort sortMethod, Order sortDir = Order.Descending)
		{
			return Search.SearchClient.GetTagSearchAsync(_context, tag, pageCount, sortMethod, sortDir);
		}



		/// <summary>
		/// ニコニコ動画IDから関連する動画情報を取得します。
		/// </summary>
		/// <param name="SearchResponse"></param>
		/// <returns></returns>
		public Task<NicoVideoResponse> GetRelatedVideoAsync(string videoId, uint from, uint limit, Sort sortMethod, Order sortDir = Order.Descending)
		{
			return Related.RelatedClient.GetRelatedVideoAsync(_context, videoId, from, limit, sortMethod, sortDir);
		}

        public Task<Related.VideoPlaylistResponse> GetVideoPlaylistAsync(string videoId, string referer)
        {
            return Related.RelatedClient.GetRelatedPlaylistAsync(_context, videoId, referer);
        }




        public Task<Comment.PostCommentResponse> PostCommentAsync(
            string commentServerUrl,
            string threadId,
            string ticket,
            int commentCount,
            string comment, 
            TimeSpan position, 
            string commands
            )
        {
            return Comment.CommentClient.PostCommentAsync(_context, commentServerUrl, threadId, ticket, commentCount, comment, position, commands);
        }

        public Task<Comment.PostCommentResponse> PostCommentAsync(
            string commentServerUrl, 
            Comment.CommentThread thread, 
            string comment, 
            TimeSpan position, 
            string commands
            )
		{
			return Comment.CommentClient.PostCommentAsync(_context, commentServerUrl, thread._thread, thread.Ticket, int.Parse(thread.CommentCount)+1, comment, position, commands);
		}

        /// <summary>
		/// 動画ページ内にある動画情報を取得します。
		/// </summary>
		/// <param name="requestId">動画ID</param>
		/// <returns></returns>
		public Task<Dmc.DmcWatchData> GetDmcWatchResponseAsync(string requestId)
        {
            return Dmc.DmcClient.GetDmcWatchResponseAsync(_context, requestId);
        }


        public Task<Dmc.DmcWatchResponse> GetDmcWatchJsonAsync(string requestId, bool isLoggedIn, string actionTrackId)
        {
            return Dmc.DmcClient.GetDmcWatchJsonAsync(_context, requestId, isLoggedIn, actionTrackId);
        }


        /// <summary>
        /// DMCサーバー上の動画を再生するためのリクエストを送信し、
        /// 動画再生のセッション情報を取得します。
        /// </summary>
        /// <param name="watchData"></param>
        /// <param name="videoQuality"></param>
        /// <param name="audioQuality"></param>
        /// <returns></returns>
        public Task<Dmc.DmcSessionResponse> GetDmcSessionResponse(
            Dmc.DmcWatchResponse watchData,
            Dmc.VideoContent videoQuality = null,
            Dmc.AudioContent audioQuality = null,
            bool hlsMode = false
            )
        {
            return Dmc.DmcClient.GetDmcSessionResponseAsync(_context, watchData, videoQuality, audioQuality, hlsMode);
        }


        public Task DmcSessionFirstHeartbeatAsync(
            Dmc.DmcWatchResponse watch,
            Dmc.DmcSessionResponse sessionRes
            )
        {
            return Dmc.DmcClient.DmcSessionFirstHeartbeatAsync(_context, watch, sessionRes);
        }

        public Task DmcSessionHeartbeatAsync(
            Dmc.DmcWatchResponse watch,
            Dmc.DmcSessionResponse sessionRes
            )
        {
            return Dmc.DmcClient.DmcSessionHeartbeatAsync(_context, watch, sessionRes);
        }

        public Task DmcSessionLeaveAsync(
            Dmc.DmcWatchResponse watch,
            Dmc.DmcSessionResponse sessionRes
            )
        {
            return Dmc.DmcClient.DmcSessionLeaveAsync(_context, watch, sessionRes);
        }

        public Task DmcSessionExitHeartbeatAsync(
            Dmc.DmcWatchResponse watch,
            Dmc.DmcSessionResponse sessionRes
            )
        {
            return Dmc.DmcClient.DmcSessionExitHeartbeatAsync(_context, watch, sessionRes);
        }



        public Task<bool> SendOfficialHlsWatchAsync(string contentId, string trackId)
        {
            return Dmc.DmcClient.SendOfficialHlsWatchAsync(_context, contentId, trackId);
        }


        /// <summary>
        /// 動画のコメントをやりとりするJSON APIアクセスをラッピングしたCommentSessionContextを取得します。
        /// DMCサーバーに配置済みの動画で利用できます。Smileサーバーの動画では<see cref="GetCommentAsync(int, string, int, bool)"/>を利用してください。
        /// </summary>
        /// <param name="dmc"></param>
        /// <returns></returns>
        public Comment.CommentSessionContext GetCommentSessionContext(Dmc.DmcWatchResponse dmc)
        {
            return new Comment.CommentSessionContext(_context, dmc);
        }




        /// <summary>
        /// Get recommendations (video/etc)
        /// </summary>
        /// <returns></returns>
        /// <remarks>Require User Session</remarks>
        public Task<Recommend.RecommendResponse> GetRecommendFirstAsync()
        {
            return Recommend.RecommendClient.GetRecommendFirstAsync(_context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user_tags">Recommend.RecommendResponse.UserTagParam</param>
        /// <param name="seed">Recommend.RecommendResponse.Seed</param>
        /// <param name="page">Recommend.RecommendResponse.Page</param>
        /// <returns></returns>
        public Task<Recommend.RecommendContent> GetRecommendAsync(string user_tags, int seed, int page)
        {
            return Recommend.RecommendClient.GetRecommendAsync(_context, user_tags, seed, page);
        }


        #region UserVideos

        public Task<Mntone.Nico2.Videos.Users.UserVideosResponse> GetUserVideosAsync(uint userId, uint page)
        {
            return Mntone.Nico2.Videos.Users.UserVideosClient.GetUserVideoResponse(_context, userId, page);
        }

        #endregion


        #region Series

        /// <summary>
        /// Get series videos.
        /// </summary>
        /// <param name="seriesId"></param>
        /// <returns></returns>
        public Task<Series.SeriesDetails> GetSeriesVideosAsync(string seriesId)
        {
            return Series.SeriesVideosClient.GetSeriesDetailsAsync(_context, seriesId);
        }

        #endregion





        #region field

        private NiconicoContext _context;

        #endregion

    }
}