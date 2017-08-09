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

        public Task<Video.VideoInfoResponse> GetVideoInfoAsync(
            string videoId
            )
        {
            return Video.VideoSearchClient.GetVideoInfoAsync(
                this._context
                , videoId
                );
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
		/// <param name="word">キーワードまたはタグ</param>
		/// <param name="isTagSearch">タグ検索とするか</param>
		/// <param name="provider">検索対象の生放送提供者種別。無指定の場合すべて</param>
		/// <param name="from">検索開始位置</param>
		/// <param name="length">検索結果の希望取得数</param>
		/// <param name="order">順番。無指定の場合はAccending(古いものが先に来る）</param>
		/// <param name="sort"></param>
		/// <param name="mode"></param>
		/// <returns></returns>
		public Task<Live.NicoliveVideoResponse> LiveSearchAsync(
			string word,
			bool isTagSearch,
			Nico2.Live.CommunityType? provider = null,
			uint from = 0,
			uint length = 30,
			Order? order = null,
			Live.NicoliveSearchSort? sort = null,
			Live.NicoliveSearchMode? mode = null
			)
		{
			return Live.LiveSearchClient.GetLiveSearchAsync(_context,
				word, isTagSearch, provider,
				from,
				length,
				order,
				sort,
				mode
				);
		}



		#region field

		private NiconicoContext _context;

		#endregion
	}
}