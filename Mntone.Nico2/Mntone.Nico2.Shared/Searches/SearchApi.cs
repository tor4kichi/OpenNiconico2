using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Mntone.Nico2.Searches.Live;
using System.Linq.Expressions;

#if WINDOWS_UWP
using Windows.Foundation;
#else
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
#if WINDOWS_UWP
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

        public Task<Video.VideoInfoResponse> GetVideoInfoAsync(
            string videoId
            )
        {
            return Video.VideoSearchClient.GetVideoInfoAsync(
                this._context
                , videoId
                );
        }

		public Task<Video.VideoInfoArrayResponse> GetVideoInfoArrayAsync(
			IEnumerable<string> videoIdList
			)
        {
			return Video.VideoSearchClient.GetVideoInfoArrayAsync(_context, videoIdList);
        }


        public Task<Video.VideoListingResponse> VideoSearchWithKeywordAsync(
			string keyword
			, uint from = 0
			, uint limit = 30
			, Sort? sort = null
			, Order? order = null
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
			, Sort? sort = null
			, Order? order = null
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
			, Sort? sort = null
			, Order? order = null
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



		/// <summary>
		/// 生放送情報の検索結果を取得します。
		/// </summary>
		/// <param name="q">検索キーワード</param>
		/// <param name="offset">返ってくるコンテンツの取得オフセット。最大:1600</param>
		/// <param name="limit">返ってくるコンテンツの最大数。最大:100</param>
		/// <param name="targets">検索対象のフィールド</param>
		/// <param name="fields">レスポンスに含みたいヒットしたコンテンツのフィールド</param>
		/// <param name="sortType">ソート順（デフォルトは降順/Decsending）。昇順指定する場合は LiveSearchSortType.SortAcsending を | で繋いで指定します。</param>
		/// <param name="filterExpression">検索結果をフィルタの条件にマッチするコンテンツだけに絞ります。</param>
		/// <see cref="https://site.nicovideo.jp/search-api-docs/search.html"/>
		public Task<Live.LiveSearchResponse> LiveSearchAsync(
			string q,
			int offset,
			int limit,
			SearchTargetType targets = SearchTargetType.All,
			LiveSearchFieldType fields = LiveSearchFieldType.All,
			LiveSearchSortType sortType = LiveSearchSortType.StartTime | LiveSearchSortType.SortDecsending,
			Expression<Func<SearchFilterField, bool>> filterExpression = null
			)
		{
			return Live.LiveSearchClient.GetLiveSearchAsync(_context,
				q,
				offset,
				limit,
				targets,
				fields,
				sortType,
				filterExpression
				);
		}

		public Task<Live.LiveSearchResponse> LiveSearchAsync(
			string q,
			int offset,
			int limit,
			SearchTargetType targets = SearchTargetType.All,
			LiveSearchFieldType fields = LiveSearchFieldType.All,
			LiveSearchSortType sortType = LiveSearchSortType.StartTime | LiveSearchSortType.SortDecsending,
			ISearchFilter searchFilter = null
			)
		{
			return Live.LiveSearchClient.GetLiveSearchAsync(_context,
				q,
				offset,
				limit,
				targets,
				fields,
				sortType,
				searchFilter
				);
		}


		#region field

		private NiconicoContext _context;

		#endregion
	}
}