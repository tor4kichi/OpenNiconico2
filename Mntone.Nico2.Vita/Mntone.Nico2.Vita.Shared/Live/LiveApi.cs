﻿using System.Collections.Generic;

#if WINDOWS_APP
using System;
using Windows.Foundation;
using Windows.Foundation.Metadata;
#else
using System.Threading.Tasks;
#endif

namespace Mntone.Nico2.Vita.Live
{
	/// <summary>
	/// ニコニコ生放送 API 群
	/// </summary>
	public sealed class LiveApi
	{
		internal LiveApi( NiconicoVitaContext context )
		{
			this._context = context;
		}

		/// <summary>
		/// 非同期操作として番組情報を取得します
		/// </summary>
		/// <param name="requestId">取得したい番組の ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<Video.VideoResponse> GetVideoAsync( string requestId )
		{
			return Video.VideoClient.GetVideoAsync( this._context, requestId ).AsAsyncOperation();
		}
#else
		public Task<Video.VideoResponse> GetVideoAsync( string requestId )
		{
			return Video.VideoClient.GetVideoAsync( this._context, requestId );
		}
#endif

		/// <summary>
		/// 非同期操作として複数番組情報を取得します
		/// </summary>
		/// <param name="requestIds">取得したい複数の番組の ID</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<Videos.VideosResponse> GetVideosAsync( IReadOnlyList<string> requestIds )
		{
			return Videos.VideosClient.GetVideosAsync( this._context, requestIds ).AsAsyncOperation();
		}
#else
		public Task<Videos.VideosResponse> GetVideosAsync( IReadOnlyList<string> requestIds )
		{
			return Videos.VideosClient.GetVideosAsync( this._context, requestIds );
		}
#endif

		/// <summary>
		/// 非同期操作として放送中の番組一覧を取得します
		/// </summary>
		/// <param name="range">取得範囲</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		[Overload( "GetOnAirProgramsAsync" )]
		public IAsyncOperation<ProgramsResponse> GetOnAirProgramsAsync( Range range )
		{
			return OnAirPrograms.OnAirProgramsClient.GetOnAirProgramsAsync( this._context, null, SortDirection.Ascending, Live.OnAirPrograms.SortType.StartTime, range ).AsAsyncOperation();
		}
#else
		public Task<ProgramsResponse> GetOnAirProgramsAsync( Range range )
		{
			return OnAirPrograms.OnAirProgramsClient.GetOnAirProgramsAsync( this._context, null, SortDirection.Ascending, Live.OnAirPrograms.SortType.StartTime, range );
		}
#endif

		/// <summary>
		/// 非同期操作として放送中の番組一覧を取得します
		/// </summary>
		/// <param name="type">提供元の種類</param>
		/// <param name="range">取得範囲</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		[Overload( "GetOnAirProgramsWithCommunityTypeAsync" )]
		public IAsyncOperation<ProgramsResponse> GetOnAirProgramsAsync( Nico2.Live.CommunityType type, Range range )
		{
			return OnAirPrograms.OnAirProgramsClient.GetOnAirProgramsAsync( this._context, type, SortDirection.Ascending, Live.OnAirPrograms.SortType.StartTime, range ).AsAsyncOperation();
		}
#else
		public Task<ProgramsResponse> GetOnAirProgramsAsync( Nico2.Live.CommunityType type, Range range )
		{
			return OnAirPrograms.OnAirProgramsClient.GetOnAirProgramsAsync( this._context, type, SortDirection.Ascending, Live.OnAirPrograms.SortType.StartTime, range );
		}
#endif

		/// <summary>
		/// 非同期操作として放送中の番組一覧を取得します
		/// </summary>
		/// <param name="sortDirection">整列方向</param>
		/// <param name="sortType">整列方法</param>
		/// <param name="range">取得範囲</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		[Overload( "GetOnAirProgramsWithSortMethodAsync" )]
		public IAsyncOperation<ProgramsResponse> GetOnAirProgramsAsync( SortDirection sortDirection, Live.OnAirPrograms.SortType sortType, Range range )
		{
			return OnAirPrograms.OnAirProgramsClient.GetOnAirProgramsAsync( this._context, null, sortDirection, sortType, range ).AsAsyncOperation();
		}
#else
		public Task<ProgramsResponse> GetOnAirProgramsAsync( SortDirection sortDirection, Live.OnAirPrograms.SortType sortType, Range range )
		{
			return OnAirPrograms.OnAirProgramsClient.GetOnAirProgramsAsync( this._context, null, sortDirection, sortType, range );
		}
#endif

		/// <summary>
		/// 非同期操作として放送中の番組一覧を取得します
		/// </summary>
		/// <param name="type">提供元の種類</param>
		/// <param name="sortDirection">整列方向</param>
		/// <param name="sortType">整列方法</param>
		/// <param name="range">取得範囲</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		[Overload( "GetOnAirProgramsWithCommunityTypeAndSortMethodAsync" )]
		public IAsyncOperation<ProgramsResponse> GetOnAirProgramsAsync(
			Nico2.Live.CommunityType type, SortDirection sortDirection, Live.OnAirPrograms.SortType sortType, Range range )
		{
			return OnAirPrograms.OnAirProgramsClient.GetOnAirProgramsAsync( this._context, type, sortDirection, sortType, range ).AsAsyncOperation();
		}
#else
		public Task<ProgramsResponse> GetOnAirProgramsAsync(
			Nico2.Live.CommunityType type, SortDirection sortDirection, Live.OnAirPrograms.SortType sortType, Range range )
		{
			return OnAirPrograms.OnAirProgramsClient.GetOnAirProgramsAsync( this._context, type, sortDirection, sortType, range );
		}
#endif

		/// <summary>
		/// 非同期操作として放送予定の番組一覧を取得します
		/// </summary>
		/// <param name="type">提供元の種類</param>
		/// <param name="range">取得範囲</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		[Overload( "GetComingSoonProgramsWithCommunityTypeAsync" )]
		public IAsyncOperation<ProgramsResponse> GetComingSoonProgramsAsync( Nico2.Live.CommunityType type, Range range )
		{
			return ComingSoonPrograms.ComingSoonProgramsClient.GetOnAirProgramsAsync( this._context, type, range ).AsAsyncOperation();
		}
#else
		public Task<ProgramsResponse> GetComingSoonProgramsAsync( Nico2.Live.CommunityType type, Range range )
		{
			return ComingSoonPrograms.ComingSoonProgramsClient.GetOnAirProgramsAsync( this._context, type, range );
		}
#endif

		/// <summary>
		/// 非同期操作として検索番組情報を取得します
		/// </summary>
		/// <param name="request">検索リクエスト情報</param>
		/// <returns>非同期操作を表すオブジェクト</returns>
#if WINDOWS_APP
		public IAsyncOperation<SearchPrograms.SearchProgramsResponse> GetSearchProgramsAsync( SearchPrograms.SearchProgramsRequest request )
		{
			return SearchPrograms.SearchProgramsClient.GetSearchProgramsAsync( this._context, request ).AsAsyncOperation();
		}
#else
		public Task<SearchPrograms.SearchProgramsResponse> GetSearchProgramsAsync( SearchPrograms.SearchProgramsRequest request )
		{
			return SearchPrograms.SearchProgramsClient.GetSearchProgramsAsync( this._context, request );
		}
#endif

		#region field

		private NiconicoVitaContext _context;

		#endregion
	}
}