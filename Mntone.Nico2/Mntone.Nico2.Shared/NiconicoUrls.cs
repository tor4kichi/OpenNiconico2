﻿using System;

namespace Mntone.Nico2
{
	/// <summary>
	/// ニコニコに関する URL 群
	/// </summary>
	public static class NiconicoUrls
	{
		private const string DomainBase = ".nicovideo.jp/";

		private const string LiveApiForCommunityUrlBase = "http://watch.live.nicovideo.jp/api/";
		private const string LiveApiForOfficialOrChannelUrlBase = "http://ow.live.nicovideo.jp/api/";
		private const string LiveApiForExternalUrlBase = "http://ext.live.nicovideo.jp/api/";

		/// <summary>
		/// ニコニコ トップ ページ URL テキスト
		/// </summary>
		public static string TopPageUrl { get { return VideoUrlBase; } }


		#region Authentication

		private const string AuthenticationBase = "https://secure" + DomainBase + "secure/";

		internal static string LogOnUrl { get { return AuthenticationBase + "login?site=niconico"; } }
		internal static string LogOffUrl { get { return AuthenticationBase + "logout"; } }

		#endregion


		#region Videos

		private const string VideoUrlBase = "http://www" + DomainBase;
		private const string VideoApiUrlBase = VideoUrlBase + "api/";
		private const string VideoFlapiUrlBase = "http://flapi" + DomainBase + "api/";

		/// <summary>
		/// ニコニコ動画 トップ ページ URL テキスト
		/// </summary>
		public static string VideoTopPageUrl { get { return VideoUrlBase + "my/top"; } }

		/// <summary>
		/// ニコニコ動画 マイ ページ URL テキスト
		/// </summary>
		public static string VideoMyPageUrl { get { return VideoUrlBase + "video_top"; } }

		/// <summary>
		/// ニコニコ動画 視聴ページ URL テキスト
		/// </summary>
		public static string VideoWatchPageUrl { get { return VideoUrlBase + "watch/"; } }

		internal static string VideoFlvUrl { get { return VideoFlapiUrlBase + "getflv/"; } }
		internal static string VideoThumbInfoUrl { get { return "http://ext.nicovideo.jp/api/getthumbinfo/"; } }
		internal static string VideoHistoryUrl { get { return VideoApiUrlBase + "videoviewhistory/list"; } }
		internal static string VideoRemoveUrl { get { return VideoApiUrlBase + "videoviewhistory/remove?token="; } }
		internal static string VideoThreadKeyApiUrl { get { return VideoFlapiUrlBase + "getthreadkey?thread="; } }


		private const string ExtUrlBase = "http://ext" + DomainBase;
		private const string ExtAPIUrlBase = ExtUrlBase + "api/";
		private const string ExtSearchUrlBase = ExtAPIUrlBase + "search/";

		public static string VideoKeywordSearchApiUrl = ExtSearchUrlBase + "search/";
		public static string VideoTagSearchApiUrl = ExtSearchUrlBase + "tag/";


		/// <summary>
		/// 
		/// </summary>
		/// <param name="keyword"></param>
		/// <param name="pageCount"></param>
		/// <param name="sortMethod">n/v/m/r/f/l/h</param>
		/// <returns></returns>
		/// <remarks>
		/// SortMethod
		/// n = new_comment | 
		/// v = view_counter | 
		/// m = mylist_counter | 
		/// r = num_res | 
		/// f = first_retrieve | 
		/// l = length | 
		/// h = popularity
		/// </remarks>
		public static string MakeKeywordSearchUrl(string keyword, uint pageCount, string sortMethod="h")
		{
			return $"{VideoKeywordSearchApiUrl}{keyword}?mode=watch&page={pageCount}&sort={sortMethod}&order=d";
		}


		public static string MakeTagSearchUrl(string tag, uint pageCount, string sortMethod = "h")
		{
			return $"{VideoTagSearchApiUrl}{tag}?mode=watch&page={pageCount}&sort={sortMethod}&order=d";
		}


		#endregion


		#region Live

		private const string LiveUrlBase = "http://live" + DomainBase;
		private const string LiveApiUrlBase = LiveUrlBase + "api/";

		/// <summary>
		/// ニコニコ生放送 トップ ページ URL テキスト
		/// </summary>
		public static string LiveTopPageUrl { get { return LiveUrlBase; } }

		/// <summary>
		/// ニコニコ生放送 マイ ページ URL テキスト
		/// </summary>
		public static string LiveMyPageUrl { get { return LiveUrlBase + "my"; } }

		/// <summary>
		/// ニコニコ生放送 視聴ページ URL テキスト
		/// </summary>
		public static string LiveWatchPageUrl { get { return LiveUrlBase + "watch/"; } }

		/// <summary>
		/// ニコニコ生放送 ゲート ページ URL テキスト
		/// </summary>
		public static string LiveGatePageUrl { get { return LiveUrlBase + "gate/"; } }

		internal static string LiveCKeyUrl { get { return LiveApiUrlBase + "getckey"; } }
		internal static string LivePlayerStatusUrl { get { return LiveApiUrlBase + "getplayerstatus/"; } }
		internal static string LivePostKeyUrl { get { return LiveApiUrlBase + "getpostkey"; } }
		internal static string LiveVoteUrl { get { return LiveApiUrlBase + "vote"; } }
		internal static string LiveHeartbeatUrl { get { return LiveApiUrlBase + "heartbeat"; } }
		internal static string LiveLeaveUrl { get { return LiveApiUrlBase + "leave"; } }
		internal static string LiveTagRevisionUrl { get { return LiveApiUrlBase + "tagrev/"; } }
		internal static string LiveZappingListIndexUrl { get { return LiveApiUrlBase + "getzappinglist?zroute=index"; } }
		internal static string LiveZappingListRecentUrl { get { return LiveApiUrlBase + "getzappinglist?zroute=recent"; } }
		internal static string LiveIndexZeroStreamListUrl { get { return LiveApiUrlBase + "getindexzerostreamlist?status="; } }
		internal static string LiveWatchingReservationListUrl { get { return LiveApiUrlBase + "watchingreservation?mode=list"; } }
		internal static string LiveWatchingReservationDetailListUrl { get { return LiveApiUrlBase + "watchingreservation?mode=detaillist"; } }

		#endregion


		#region Images

		private const string ImageUrlBase = "http://seiga" + DomainBase;
		private const string ImageApiUrlBase = ImageUrlBase + "api/";
		private const string ImageExtApiUrlBase = "http://ext.seiga" + DomainBase + "api/";

		/// <summary>
		/// ニコニコ静画 トップ ページ URL テキスト
		/// </summary>
		public static string ImageTopPageUrl { get { return ImageUrlBase; } }

		/// <summary>
		/// ニコニコ静画 マイ ページ URL テキスト
		/// </summary>
		public static string ImageMyPageUrl { get { return ImageUrlBase + "my"; } }
		
		/// <summary>
		/// ニコニコ静画 お題 トップ ページ URL テキスト
		/// </summary>
		public static string ImageThemeTopPageUrl { get { return ImageUrlBase + "theme/"; } }

		/// <summary>
		/// ニコニコ静画 イラスト トップ ページ URL テキスト
		/// </summary>
		public static string ImageIllustTopPageUrl { get { return ImageUrlBase + "illust/"; } }

		/// <summary>
		/// ニコニコ春画 トップ ページ URL テキスト
		/// </summary>
		public static string ImageIllustAdultTopPageUrl { get { return ImageUrlBase + "shunga/"; } }

		/// <summary>
		/// ニコニコ静画 漫画 トップ ページ URL テキスト
		/// </summary>
		public static string ImageMangaTopPageUrl { get { return ImageUrlBase + "manga/"; } }

		/// <summary>
		/// ニコニコ静画 電子書籍 トップ ページ URL テキスト
		/// </summary>
		public static string ImageElectronicBookTopPageUrl { get { return ImageUrlBase + "book/"; } }

		internal static string ImageBlogPartsUrl { get { return ImageExtApiUrlBase + "illust/blogparts?mode="; } }
		internal static string ImageUserInfoUrl { get { return ImageApiUrlBase + "user/info?id="; } }
		internal static string ImageUserDataUrl { get { return ImageApiUrlBase + "user/data?id="; } }

		#endregion


		#region Searches

		private const string SearchApiUrlBase = "http://api.search" + DomainBase + "api/";

		internal static string SearchSuggestionUrl { get { return "http://search" + DomainBase + "suggestion/complete/"; } }

		#endregion


		#region Dictionaries

		private const string DictionaryUrlBase = "http://dic" + DomainBase;
		private const string DictionaryApiUrlBase = "http://api.nicodic.jp/";

		/// <summary>
		/// ニコニコ大百科 トップ ページ URL テキスト
		/// </summary>
		public static string DictionaryTopPageUrl { get { return DictionaryUrlBase; } }

		internal static string DictionaryWordExistUrl { get { return DictionaryApiUrlBase + "e/json/"; } }
		internal static string DictionarySummarytUrl { get { return DictionaryApiUrlBase + "page.summary/json/a/"; } }
		internal static string DictionaryExistUrl { get { return DictionaryApiUrlBase + "page.exist/json/"; } }
		internal static string DictionaryRecentUrl { get { return DictionaryApiUrlBase + "page.created/json"; } }

		#endregion


		#region Apps

		private const string AppUrlBase = "http://app" + DomainBase;

		/// <summary>
		/// ニコニコアプリ トップ ページ URL テキスト
		/// </summary>
		public static string AppTopPageUrl { get { return AppUrlBase; } }

		/// <summary>
		/// ニコニコアプリ マイ ページ URL テキスト
		/// </summary>
		public static string AppMyPageUrl { get { return AppUrlBase + "my/apps"; } }

		#endregion


		#region Communities

		/// <summary>
		/// ニコニコ コミュニティー アイコン URL テキスト
		/// {0}: CommunityID / 10000
		/// {1}: CommunityID
		/// </summary>
		public static string CommunityIconUrl { get { return "http://icon.nimg.jp/community/{0}/co{1}.jpg"; } }

		/// <summary>
		/// ニコニコ コミュニティー 小アイコン URL テキスト
		/// {0}: CommunityID / 10000
		/// {1}: CommunityID
		/// </summary>
		public static string CommunitySmallIconUrl { get { return "http://icon.nimg.jp/community/s/{0}/co{1}.jpg"; } }

		/// <summary>
		/// ニコニコ コミュニティー アイコン未設定 URL テキスト
		/// </summary>
		public static string CommunityBlankIconUrl { get { return "http://icon.nimg.jp/404.jpg"; } }
		
		#endregion


		#region Channels

		/// <summary>
		/// ニコニコ チャンネル アイコン URL テキスト
		/// {0}: ChannelID
		/// </summary>
		public static string ChannelIconUrl { get { return "http://icon.nimg.jp/channel/ch{0}.jpg"; } }

		/// <summary>
		/// ニコニコ チャンネル 小アイコン URL テキスト
		/// {0}: ChannelID
		/// </summary>
		public static string ChannelSmallIconUrl { get { return "http://icon.nimg.jp/channel/s/ch{0}.jpg"; } }

		#endregion


		#region Users

		/// <summary>
		/// ニコニコ ユーザー ページ URL テキスト
		/// </summary>
		public static string UserPageUrl { get { return VideoUrlBase + "my"; } }

		/// <summary>
		/// ニコニコ ユーザー アイコン URL テキスト
		/// {0}: UserID / 10000
		/// {1}: UserID
		/// </summary>
		public static string UserIconUrl { get { return "http://usericon.nimg.jp/usericon/{0}/{1}.jpg"; } }

		/// <summary>
		/// ニコニコ ユーザー 小アイコン URL テキスト
		/// {0}: UserID / 10000
		/// {1}: UserID
		/// </summary>
		public static string UserSmallIconUrl { get { return "http://usericon.nimg.jp/usericon/s/{0}/{1}.jpg"; } }

		/// <summary>
		/// ニコニコ ユーザー アイコン未設定 URL テキスト
		/// </summary>
		public static string UserBlankIconUrl { get { return "http://uni.res.nimg.jp/img/user/thumb/blank.jpg"; } }
		
		#endregion
	}
}