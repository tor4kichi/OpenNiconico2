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


		public Task<Video.VideoListingResponse> VideoSearchWithKeywordAsync(
			string keyword
			, uint from = 0
			, uint limit = 30
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


		public Task<Video.VideoListingResponse> VideoSearchWithTagAsync(
			string tag
			, uint from = 0
			, uint limit = 30
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

		public Task<Mylist.MylistSearchResponse> MylistSearchAsync(
			string keyword
			, uint from = 0
			, uint limit = 30
			, Sort sort = Sort.FirstRetrieve
			, Order order = Order.Descending
			)
		{
			return Mylist.MylistSearchClient.GetMylistSearchAsync(
				this._context
				, keyword
				, from
				, limit
				, sort
				, order
				);
		}

		public Task<Community.CommunitySearchResponse> CommunitySearchAsync(
			string keyword
			, uint page = 1
			, Community.CommunitySearchSort sort = Community.CommunitySearchSort.CreatedAt
			, Order order = Order.Descending
			, Community.CommunitySearchMode mode = Community.CommunitySearchMode.Keyword
			)
		{
			return Community.CommunityClient.CommunitySearchAsync(
				this._context
				, keyword
				, page
				, sort
				, order
				, mode
				);
		}

		#region field

		private NiconicoContext _context;

		#endregion
	}
}