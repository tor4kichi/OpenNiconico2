﻿#if WINDOWS_APP
using System;
using Windows.Foundation;
using Windows.Foundation.Metadata;
#else
using Mntone.Nico2.Mylist;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
#if WINDOWS_APP
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
		/// 非同期操作として thumbnail 情報を取得します
		/// </summary>
		/// <param name="requestId">目的の動画 ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<Thumbnail.ThumbnailResponse> GetThumbnailAsync( string requestId )
		{
			return Thumbnail.ThumbnailClient.GetThumbnailAsync( _context, requestId ).AsAsyncOperation();
		}
#else
		public Task<Thumbnail.ThumbnailResponse> GetThumbnailAsync( string requestId )
		{
			return Thumbnail.ThumbnailClient.GetThumbnailAsync( _context, requestId );
		}
#endif

		/// <summary>
		/// 非同期操作として videoviewhistory/list 情報を取得します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
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
		/// 非同期操作として videoviewhistory/remove で履歴を削除します
		/// </summary>
		/// <param name="token">視聴履歴を取得したときに取得したトークン</param>
		/// <param name="requestId">目的の動画 ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
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
#if WINDOWS_APP
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
		/// 動画ページ内にある動画情報を取得します。
		/// </summary>
		/// <param name="requestId">動画ID</param>
		/// <param name="forceLowQuality">強制的に低画質動画を再生する場合に true</param>
		/// <param name="harmfulReactType">ContentZoningExceptionをキャッチした時のみ使用、有害動画の視聴を継続する場合に None以外 を設定</param>
		/// <returns></returns>
		/// <exception cref="ContentZoningException"></exception>
		public Task<WatchAPI.InitialWatchData> GetWatchApiAsync(string requestId, bool forceLowQuality, HarmfulContentReactionType harmfulReactType = HarmfulContentReactionType.None)
		{
			return WatchAPI.WatchAPIClient.GetWatchApiAsync(_context, requestId, forceLowQuality, harmfulReactType);
		}



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




		
		public Task<Comment.PostCommentResponse> PostCommentAsync(string commentServerUrl, Comment.CommentThread thread, string comment, TimeSpan position, string commands)
		{
			return Comment.CommentClient.PostCommentAsync(_context, commentServerUrl, thread, comment, position, commands);
		}




		#region field

		private NiconicoContext _context;

		#endregion
	}
}