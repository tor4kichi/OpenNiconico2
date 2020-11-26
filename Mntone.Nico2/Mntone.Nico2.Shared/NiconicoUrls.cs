using System;

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

        private const string CE_API_V1_BASE = "http://api.ce" + DomainBase;


		#region Authentication

		private const string AuthenticationBase = "https://secure" + DomainBase + "secure/";

        internal static string LogOnUrl { get { return AuthenticationBase + "login?site=niconico"; } }
        internal static string LogOffUrl { get { return AuthenticationBase + "logout"; } }


        internal static readonly string AccountApiBase = $"https://account{DomainBase}api/";
        internal static readonly string AccountApiV1 = $"{AccountApiBase}v1/";
        internal static readonly string LogOnApiUrl = $"{AccountApiV1}login";


        internal static readonly string MultiFactorAuthenticationPageBase = $"https://account{DomainBase}";
        internal static readonly string MultiFactorAuthenticationPage = $"{MultiFactorAuthenticationPageBase}mfa";
        internal static readonly string BackupCodeMultiFactorAuthenticationPage = $"{MultiFactorAuthenticationPage}/backup_code";

        #endregion


        #region Videos

        internal const string VideoUrlBase = "https://www" + DomainBase;
        internal const string VideoApiUrlBase = VideoUrlBase + "api/";
        internal const string VideoFlapiUrlBase = "https://flapi" + DomainBase + "api/";


        public static string VideoLoginUrl = VideoUrlBase + "login";


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


        public static string VideoHistoryMyPageUrl { get { return VideoUrlBase + "my/history/video"; } }

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

		public static string RelatedVideoApiUrl = "http://api.ce.nicovideo.jp/nicoapi/v1/video.relation";

        public static string VideoPlaylistApiUrl = VideoApiUrlBase + "watch/playlist";

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
		public static string MakeKeywordSearchUrl(string keyword, uint pageCount, string sortMethod, string sortDir)
		{
			return $"{VideoKeywordSearchApiUrl}{keyword}?mode=watch&page={pageCount}&sort={sortMethod}&order={sortDir}";
		}


		public static string MakeTagSearchUrl(string tag, uint pageCount, string sortMethod, string sortDir)
		{
			return $"{VideoTagSearchApiUrl}{tag}?mode=watch&page={pageCount}&sort={sortMethod}&order={sortDir}";
		}




        public const string NmsgCommentApiUrl = @"http://nmsg.nicovideo.jp/api.json/";




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

        #region Live2

        private const string Live2BaseUrl = "http://live2" + DomainBase;


        internal static string Live2WatchPageUrl = Live2BaseUrl + "watch/";

        #endregion

        #region api.ce.niocovideo.jp/liveapi/v1

        private const string API_CE_LIVEAPI = CE_API_V1_BASE + "liveapi/";
        private const string API_CE_LIVEAPI_V1 = API_CE_LIVEAPI + "v1/";


        internal static string CeLiveVideoInfoApi = API_CE_LIVEAPI_V1 + "video.info";
        internal static string CeLiveCommunityVideoApi = API_CE_LIVEAPI_V1 + "community.video";

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

		internal static string SearchSuggestionUrl { get { return "http://sug.search.nicovideo.jp/suggestion/expand/"; } }

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



		private const string CommunityUrlBase = "https://com" + DomainBase;

		public static string CommynitySearchPageUrl = CommunityUrlBase + "search/";

		public static string CommynitySammaryPageUrl = CommunityUrlBase + "community/";

		public static string CommynityVideoPageUrl = CommunityUrlBase + "video/";



		public static string CommunityJoinPageUrl = CommunityUrlBase + "motion/";

		public static string CommunityLeavePageUrl = CommunityUrlBase + "leave/";

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


        public const string ChannelUrlBase = "http://ch" + DomainBase;

        public const string ChannelApiUrlBase = ChannelUrlBase + "api/";

        public const string ChannelInfoApiUrl = ChannelApiUrlBase + "ch.info/";

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

		public static string UserPageUrlBase = $"{VideoUrlBase}user/";

		public static string MakeUserPageUrl(string user_id)
		{
			return $"{UserPageUrlBase}{user_id}";
		}


		public static string MakeUserMylistGroupListRssUrl(string user_id)
		{
			return $"{MakeUserPageUrl(user_id)}/mylist?rss=2.0";
		}

		/// <summary>
		/// ユーザーが投稿した動画を取得
		/// </summary>
		/// <param name="userId"></param>
		/// <param name="page"></param>
		/// <returns></returns>
		public static string MakeUserVideoRssUrl(string userId, uint page, string sortMethod = null, string sortDirection = null)
		{
			if (page <= 0)
			{
				throw new NotSupportedException("page is can not be lesser equal 0.");
			}

			var url = $"http://www.nicovideo.jp/user/{userId}/video?rss=2.0&page={page}";

			if (sortMethod != null)
			{
				url += $"&sort={sortMethod}";
			}

			if (sortDirection != null)
			{
				url += $"&order={sortDirection}";
			}

			return url;
		}


		public static string CE_UserApiUrl { get { return "http://api.ce.nicovideo.jp/api/v1/user.info"; } }


		private static string UserFavApiBase = VideoApiUrlBase + "watchitem/";

		public static string UserFavListApiUrl { get { return UserFavApiBase + "list"; } }
		public static string UserFavExistApiUrl { get { return UserFavApiBase + "exist"; } }
		public static string UserFavAddApiUrl { get { return UserFavApiBase + "add"; } }
		public static string UserFavRemoveApiUrl { get { return UserFavApiBase + "delete"; } }


		public static string UserFavUserPageUrl = UserPageUrl + "/fav/user";
		public static string UserFavMylistPageUrl = UserPageUrl + "/fav/mylist";
		public static string UserFavTagPageUrl = UserPageUrl + "/fav/tag";
		public static string UserFavCommunityPageUrl = UserPageUrl + "/community"; // コミュは/fav無し
        public static string UserFavChannelPageUrl = UserPageUrl + "/channel"; 



        // tags
        public const string UserFavTagBase = VideoApiUrlBase + "favtag/";
		public static string UserFavTagListUrl { get { return UserFavTagBase + "list"; } }
		public static string UserFavTagAddUrl { get { return UserFavTagBase + "add"; } }
		public static string UserFavTagRemoveUrl { get { return UserFavTagBase + "delete"; } }

		// ng comment
		public const string UserNGCommentUrl = VideoFlapiUrlBase + "configurengclient";

		public const string VideoPostKeyUrl = VideoFlapiUrlBase + "getpostkey";



        // Reccmmend
        public static string RecommendPageUrl = $"{VideoUrlBase}recommendations";
        public static string RecommendApiUrl = $"{VideoUrlBase}api/recommendations";


        // Series
        public static string MakeUserSeriesPageUrl(string userId) => $"{VideoUrlBase}user/{userId}/series";


        public static string MakeSeriesPageUrl(string seriesId) => $"{VideoUrlBase}series/{seriesId}";

        #endregion




        #region Mylist Deflist とりあえずマイリスト

        // とりあえずマイリスト
        // see@ http://web.archive.org/web/20140625053235/http://efcl.info/wiki/niconicoapi/


        public static string MylistDefListUrlBase = VideoApiUrlBase + "deflist/";

		public static string MylistDeflistListUrl	= MylistDefListUrlBase + "list";
		public static string MylistDeflistAddUrl	= MylistDefListUrlBase + "add";
		public static string MylistDeflistUpdateUrl = MylistDefListUrlBase + "update";
		public static string MylistDeflistRemoveUrl = MylistDefListUrlBase + "delete";
		public static string MylistDeflistMoveUrl	= MylistDefListUrlBase + "move";
		public static string MylistDeflistCopyUrl	= MylistDefListUrlBase + "copy";

		#endregion


		#region MylistGroup 

		public static string MylistGroupUrlBase	= VideoApiUrlBase + "mylistgroup/";

		public static string MylistGroupListUrl = MylistGroupUrlBase + "list";
		public static string MylistGroupGetUrl = MylistGroupUrlBase + "get";
		public static string MylistGroupAddUrl = MylistGroupUrlBase + "add";
		public static string MylistGroupUpdateUrl = MylistGroupUrlBase + "update";
		public static string MylistGroupRemoveUrl = MylistGroupUrlBase + "delete";
		public static string MylistGroupSortUrl = MylistGroupUrlBase + "sort";



		public static string MylistGroupDetailApi = "http://api.ce.nicovideo.jp/nicoapi/v1/mylistgroup.get";
		public static string MylistListlApi = "http://api.ce.nicovideo.jp/nicoapi/v1/mylist.list";

		#endregion

		#region Mylist


		public static string MylistMyPageUrl = VideoUrlBase + "my/mylist";

		public static string MakeMylistPageUrl(string group_id)
		{
			return VideoUrlBase + "mylist/" + group_id;
		}

		public static string MakeMylistCSRFTokenApiUrl(string group_id)
		{
			return $"{MylistMyPageUrl}/#/{group_id}";
		}


        public static string MakeMylistAddVideoTokenApiUrl(string videoId)
        {
            return $"{VideoUrlBase}mylist_add/video/{videoId}";
        }


        public static string MylistUrlBase    = VideoApiUrlBase + "mylist/";

		public static string MylistListUrl    = MylistUrlBase + "list";
		public static string MylistAddUrl     = MylistUrlBase + "add";
		public static string MylistUpdateUrl  = MylistUrlBase + "update";
		public static string MylistRemoveUrl  = MylistUrlBase + "delete";
		public static string MylistMoveUrl    = MylistUrlBase + "move";
		public static string MylistCopyUrl    = MylistUrlBase + "copy";



		#endregion


		#region WatchItem お気に入り

		public static string WatchItemUrlBase = VideoApiUrlBase + "watchitem/";

		public static string WatchItemListUrl   = WatchItemUrlBase + "list";
		public static string WatchItemExistUrl  = WatchItemUrlBase + "exist";
		public static string WatchItemAddUrl    = WatchItemUrlBase + "add";
		public static string WatchItemRemoveUrl = WatchItemUrlBase + "remove";

		#endregion


		#region Tag
		
		public static string MakeTagPageUrl(string tag)
		{
			return $"{VideoUrlBase}tag/{tag}";
		}

		#endregion




		#region api.ce.nicovideo

		public const string NICOVIDEO_CE_NICOAPI_BASE = "http://api.ce.nicovideo.jp/nicoapi/";

		public const string NICOVIDEO_CE_NICOAPI_V1 = NICOVIDEO_CE_NICOAPI_BASE + "v1/";

		public const string NICOVIDEO_CE_NICOAPI_V1_MYLISTGROUP = NICOVIDEO_CE_NICOAPI_V1 + "mylistgroup";
		public const string NICOVIDEO_CE_NICOAPI_V1_MYLISTGROUP_GET = NICOVIDEO_CE_NICOAPI_V1_MYLISTGROUP + ".get";

		public const string NICOVIDEO_CE_NICOAPI_V1_MYLIST        = NICOVIDEO_CE_NICOAPI_V1 + "mylist";
		public const string NICOVIDEO_CE_NICOAPI_V1_MYLIST_SEARCH = NICOVIDEO_CE_NICOAPI_V1_MYLIST + ".search";
		public const string NICOVIDEO_CE_NICOAPI_V1_MYLIST_LIST   = NICOVIDEO_CE_NICOAPI_V1_MYLIST + ".list";


		public const string NICOVIDEO_CE_NICOAPI_V1_VIDEO = NICOVIDEO_CE_NICOAPI_V1 + "video";

        public const string NICOVIDEO_CE_NICOAPI_V1_VIDEO_INFO = NICOVIDEO_CE_NICOAPI_V1_VIDEO + ".info";
		public const string NICOVIDEO_CE_NICOAPI_V1_VIDEO_INFO_ARRAY = NICOVIDEO_CE_NICOAPI_V1_VIDEO + ".array";
		public const string NICOVIDEO_CE_NICOAPI_V1_VIDEO_SEARCH = NICOVIDEO_CE_NICOAPI_V1_VIDEO + ".search";

		public const string NICOVIDEO_CE_NICOAPI_V1_TAG = NICOVIDEO_CE_NICOAPI_V1 + "tag";
		public const string NICOVIDEO_CE_NICOAPI_V1_TAG_SEARCH = NICOVIDEO_CE_NICOAPI_V1_TAG + ".search";






		// http://api.ce.nicovideo.jp/api/v1/community.info
		public const string NICOVIDEO_CE_API_BASE = "http://api.ce.nicovideo.jp/api/";

		public const string NICOVIDEO_CE_API_V1 = NICOVIDEO_CE_API_BASE + "v1/";

		public const string NICOVIDEO_CE_API_V1_COMMUNITY = NICOVIDEO_CE_API_V1 + "community";

		/// <summary>
		/// コミュニティ情報の取得
		/// </summary>
		/// <remarks>?id=co00000000</remarks>
		public const string NICOVIDEO_CE_API_V1_COMMUNITY_INFO = NICOVIDEO_CE_API_V1_COMMUNITY + ".info";


		// 生放送関連
		public const string NICOVIDEO_CE_LIVEAPI_BASE = "http://api.ce.nicovideo.jp/liveapi/";
		public const string NICOVIDEO_CE_LIVEAPI_BASE_V1 = NICOVIDEO_CE_LIVEAPI_BASE + "v1/";

		/// <summary>
		/// コミュニティの生放送情報の取得
		/// </summary>
		/// <remarks>?community_id=co00000000</remarks>
		public const string NICOVIDEO_CE_LIVEAPI_V1_COMMUNITY_VIDEO = NICOVIDEO_CE_LIVEAPI_BASE_V1 + "community.video";


		public const string NICOVIDEO_CE_LIVEAPI_V1_VIDEO_SEARCH = NICOVIDEO_CE_LIVEAPI_BASE_V1 + "video.search.solr";


		#endregion
	}
}