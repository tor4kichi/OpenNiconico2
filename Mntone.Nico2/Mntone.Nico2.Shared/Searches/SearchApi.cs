using System.Collections.Generic;

#if WINDOWS_APP
using System;
using Windows.Foundation;
#else
using System.Threading.Tasks;
#endif

namespace Mntone.Nico2.Searches
{
	/// <summary>
	/// ニコニコ検索 API 群
	/// </summary>
	public sealed class SearchApi
	{
		internal SearchApi( NiconicoContext context )
		{
			this._context = context;
		}

		/// <summary>
		/// 非同期操作として検索のサジェストを取得します
		/// </summary>
		/// <param name="targetWord">目的の単語</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<Suggestion.SuggestionResponse> GetSuggestionAsync( string targetWord )
		{
			return Suggestion.SuggestionClient.GetSuggestionAsync( this._context, targetWord ).AsAsyncOperation();
		}
#else
		public Task<Suggestion.SuggestionResponse> GetSuggestionAsync( string targetWord )
		{
			return Suggestion.SuggestionClient.GetSuggestionAsync( this._context, targetWord );
		}
#endif


		public Task<Video.VideoSearchResponse> VideoSearchAsync(
			string keyword
			, int from = 0
			, int limit = 100
			, Sort sort = Sort.FirstRetrieve
			, Order order = Order.Descending
			)
		{
			return Video.VideoSearchClient.GetKeywordSearchAsync(
				this._context
				, keyword
				, from
				, limit
				, sort
				, order
				);
		}


		public Task<Video.VideoSearchResponse> TagSearchAsync(
			string tag
			, int from = 0
			, int limit = 100
			, Sort sort = Sort.FirstRetrieve
			, Order order = Order.Descending
			)
		{
			return Video.VideoSearchClient.GetTagSearchAsync(
				this._context
				, tag
				, from
				, limit
				, sort
				, order
				);
		}



		#region field

		private NiconicoContext _context;

		#endregion
	}
}