#if WINDOWS_APP
using System;
using Windows.Foundation;
using Windows.Foundation.Metadata;
#else
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
		/// <param name="requestId"></param>
		/// <returns></returns>
		public Task<WatchAPI.WatchApiResponse> GetWatchApiAsync(string requestId)
		{
			return WatchAPI.WatchAPIClient.GetWatchApiAsync(_context, requestId);
		}



		/// <summary>
		/// 動画情報を元にして動画コメントを取得します。
		/// </summary>
		/// <param name="flvResponse"></param>
		/// <returns></returns>
		public Task<Comment.CommentResponse> GetCommentAsync(Flv.FlvResponse flvResponse)
		{
			return Comment.CommentClient.GetCommentAsync(_context, flvResponse);
		}


		/// <summary>
		/// ニコニコ動画へのキーワード検索を行い結果を取得します。
		/// </summary>
		/// <param name="flvResponse"></param>
		/// <returns></returns>
		public Task<Search.SearchResponse> GetKeywordSearchAsync(string keyword, uint pageCount)
		{
			return Search.SearchClient.GetKeywordSearchAsync(_context, keyword, pageCount);
		}

		/// <summary>
		/// ニコニコ動画へのキーワード検索を行い結果を取得します。
		/// </summary>
		/// <param name="flvResponse"></param>
		/// <returns></returns>
		public Task<Search.SearchResponse> GetTagSearchAsync(string tag, uint pageCount)
		{
			return Search.SearchClient.GetTagSearchAsync(_context, tag, pageCount);
		}


		#region field

		private NiconicoContext _context;

		#endregion
	}
}