using Mntone.Nico2.Videos.Flv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Videos.WatchAPI
{
	public class MorningPremium
	{
		[DataMember(Name = "status")]
		public string status { get; set; }
		[DataMember(Name = "timing")]
		public string timing { get; set; }
		[DataMember(Name = "from_top")]
		public bool from_top { get; set; }
	}

	public class Flashvars
	{
		[DataMember(Name = "community_id")]
		public string community_id { get; set; }
		[DataMember(Name = "community_global_id")]
		public string community_global_id { get; set; }
		[DataMember(Name = "watchAuthKey")]
		public string watchAuthKey { get; set; }
		[DataMember(Name = "flvInfo")]
		public string flvInfo { get; set; }
		[DataMember(Name = "isDmc")]
		public int isDmc { get; set; }
		[DataMember(Name = "isBackComment")]
		public object isBackComment { get; set; }
		[DataMember(Name = "thumbTitle")]
		public string thumbTitle { get; set; }
		[DataMember(Name = "thumbDescription")]
		public string thumbDescription { get; set; }
		[DataMember(Name = "videoTitle")]
		public string videoTitle { get; set; }
		[DataMember(Name = "threadPublic")]
		public string threadPublic { get; set; }
		[DataMember(Name = "is_channel")]
		public int is_channel { get; set; }
		[DataMember(Name = "player_version_xml")]
		public int player_version_xml { get; set; }
		[DataMember(Name = "player_info_xml")]
		public int player_info_xml { get; set; }
		[DataMember(Name = "appli_promotion_info_xml")]
		public int appli_promotion_info_xml { get; set; }
		[DataMember(Name = "translation_version_json")]
		public int translation_version_json { get; set; }
		[DataMember(Name = "userPrefecture")]
		public int userPrefecture { get; set; }
		[DataMember(Name = "csrfToken")]
		public string csrfToken { get; set; }
		[DataMember(Name = "v")]
		public string v { get; set; }
		[DataMember(Name = "videoId")]
		public string videoId { get; set; }
		[DataMember(Name = "deleted")]
		public string deleted { get; set; }
		[DataMember(Name = "mylist_counter")]
		public string mylist_counter { get; set; }
		[DataMember(Name = "mylistcomment_counter")]
		public string mylistcomment_counter { get; set; }
		[DataMember(Name = "movie_type")]
		public string movie_type { get; set; }
		[DataMember(Name = "thumbImage")]
		public string thumbImage { get; set; }
		[DataMember(Name = "userSex")]
		public string userSex { get; set; }
		[DataMember(Name = "userAge")]
		public int userAge { get; set; }
		[DataMember(Name = "us")]
		public int us { get; set; }
		[DataMember(Name = "iee")]
		public string iee { get; set; }
		[DataMember(Name = "is_community_video")]
		public string is_community_video { get; set; }
		[DataMember(Name = "is_community_thread")]
		public string is_community_thread { get; set; }
		[DataMember(Name = "channelId")]
		public string channelId { get; set; }
		[DataMember(Name = "channelTopURL")]
		public string channelTopURL { get; set; }
		[DataMember(Name = "channelVideoURL")]
		public string channelVideoURL { get; set; }
		[DataMember(Name = "channelName")]
		public string channelName { get; set; }
		[DataMember(Name = "isNeedPayment")]
		public int isNeedPayment { get; set; }
		[DataMember(Name = "dicArticleURL")]
		public string dicArticleURL { get; set; }
		[DataMember(Name = "blogEmbedURL")]
		public string blogEmbedURL { get; set; }
		[DataMember(Name = "uadAdvertiseURL")]
		public string uadAdvertiseURL { get; set; }
		[DataMember(Name = "noBanner")]
		public string noBanner { get; set; }
		[DataMember(Name = "category")]
		public string category { get; set; }
		[DataMember(Name = "categoryGroupKey")]
		public string categoryGroupKey { get; set; }
		[DataMember(Name = "categoryGroup")]
		public string categoryGroup { get; set; }
		[DataMember(Name = "noAppli")]
		public string noAppli { get; set; }
		[DataMember(Name = "isWide")]
		public string isWide { get; set; }
		[DataMember(Name = "wv_id")]
		public string wv_id { get; set; }
		[DataMember(Name = "wv_title")]
		public string wv_title { get; set; }
		[DataMember(Name = "wv_code")]
		public string wv_code { get; set; }
		[DataMember(Name = "wv_time")]
		public int wv_time { get; set; }
		[DataMember(Name = "leaf_switch")]
		public string leaf_switch { get; set; }
		[DataMember(Name = "use_getrelateditem")]
		public bool use_getrelateditem { get; set; }
		[DataMember(Name = "tagHirobaId")]
		public int tagHirobaId { get; set; }
		[DataMember(Name = "language")]
		public string language { get; set; }
		[DataMember(Name = "area")]
		public string area { get; set; }
		[DataMember(Name = "commentLanguage")]
		public object commentLanguage { get; set; }
		[DataMember(Name = "isAuthenticationRequired")]
		public bool isAuthenticationRequired { get; set; }
		[DataMember(Name = "watchTrackId")]
		public string watchTrackId { get; set; }
		[DataMember(Name = "watchPageServerTime")]
		public long watchPageServerTime { get; set; }
		[DataMember(Name = "morningPremium")]
		public MorningPremium morningPremium { get; set; }

		[DataMember(Name = "ad")]
		public string ad { get; set; }
		[DataMember(Name = "communityPostURL")]
		public string communityPostURL { get; set; }

		[DataMember(Name = "videoUserId")]
		public string videoUserId { get; set; }
	}

	public class OwnerChannelInfo
	{
		[DataMember(Name = "id")]
		public int? id { get; set; }
		[DataMember(Name = "communityId")]
		public int? communityId { get; set; }
		[DataMember(Name = "name")]
		public string name { get; set; }
		[DataMember(Name = "description")]
		public string description { get; set; }
		[DataMember(Name = "isFree")]
		public bool? isFree { get; set; }
		[DataMember(Name = "screenName")]
		public string screenName { get; set; }
		[DataMember(Name = "ownerName")]
		public string ownerName { get; set; }
		[DataMember(Name = "price")]
		public int? price { get; set; }
		[DataMember(Name = "bodyPrice")]
		public int? bodyPrice { get; set; }
		[DataMember(Name = "url")]
		public string url { get; set; }
		[DataMember(Name = "thumbnailUrl")]
		public string thumbnailUrl { get; set; }
		[DataMember(Name = "thumbnailSmallUrl")]
		public string thumbnailSmallUrl { get; set; }
		[DataMember(Name = "canAdmit")]
		public bool? canAdmit { get; set; }
	}

	public class TagList
	{
		[DataMember(Name = "id")]
		public string id { get; set; }
		[DataMember(Name = "tag")]
		public string tag { get; set; }
		[DataMember(Name = "cat")]
		public bool? cat { get; set; }
		[DataMember(Name = "dic")]
		public bool? dic { get; set; }
		[DataMember(Name = "lck")]
		public string lck { get; set; }
	}

	public class VideoDetail
	{
		[DataMember(Name = "v")]
		public string v { get; set; }
		[DataMember(Name = "id")]
		public string id { get; set; }
		[DataMember(Name = "title")]
		public string title { get; set; }
		[DataMember(Name = "description")]
		public string description { get; set; }
		[DataMember(Name = "is_translated")]
		public bool is_translated { get; set; }
		[DataMember(Name = "title_original")]
		public string title_original { get; set; }
		[DataMember(Name = "description_original")]
		public string description_original { get; set; }
		[DataMember(Name = "thumbnail")]
		public string thumbnail { get; set; }
		[DataMember(Name = "postedAt")]
		public string postedAt { get; set; }
		[DataMember(Name = "length")]
		public int? length { get; set; }
		[DataMember(Name = "viewCount")]
		public int? viewCount { get; set; }
		[DataMember(Name = "mylistCount")]
		public int? mylistCount { get; set; }
		[DataMember(Name = "commentCount")]
		public int? commentCount { get; set; }
		[DataMember(Name = "mainCommunityId")]
		public int? mainCommunityId { get; set; }
		[DataMember(Name = "communityId")]
		public int? communityId { get; set; }
		[DataMember(Name = "channelId")]
		public int? channelId { get; set; }
		[DataMember(Name = "isDeleted")]
		public bool isDeleted { get; set; }
		[DataMember(Name = "isMymemory")]
		public bool isMymemory { get; set; }
		[DataMember(Name = "isMonetized")]
		public bool isMonetized { get; set; }
		[DataMember(Name = "isR18")]
		public bool isR18 { get; set; }
		[DataMember(Name = "is_adult")]
		public object is_adult { get; set; }
		[DataMember(Name = "language")]
		public string language { get; set; }
		[DataMember(Name = "area")]
		public string area { get; set; }
		[DataMember(Name = "can_translate")]
		public bool can_translate { get; set; }
		[DataMember(Name = "video_translation_info")]
		public bool video_translation_info { get; set; }
		[DataMember(Name = "category")]
		public string category { get; set; }
		[DataMember(Name = "thread_id")]
		public string thread_id { get; set; }
		[DataMember(Name = "main_genre")]
		public string main_genre { get; set; }
		[DataMember(Name = "has_owner_thread")]
		public object has_owner_thread { get; set; }
		[DataMember(Name = "is_video_owner")]
		public object is_video_owner { get; set; }
		[DataMember(Name = "is_uneditable_tag")]
		public bool is_uneditable_tag { get; set; }
		[DataMember(Name = "commons_tree_exists")]
		public object commons_tree_exists { get; set; }
		[DataMember(Name = "yesterday_rank")]
		public string yesterday_rank { get; set; }
		[DataMember(Name = "highest_rank")]
		public string highest_rank { get; set; }
		[DataMember(Name = "for_bgm")]
		public bool for_bgm { get; set; }
		[DataMember(Name = "is_nicowari")]
		public object is_nicowari { get; set; }
		[DataMember(Name = "is_public")]
		public bool is_public { get; set; }
		[DataMember(Name = "is_official")]
		public bool is_official { get; set; }
		[DataMember(Name = "no_ichiba")]
		public bool no_ichiba { get; set; }
		[DataMember(Name = "community_name")]
		public string community_name { get; set; }
		[DataMember(Name = "dicArticleURL")]
		public string dicArticleURL { get; set; }
		[DataMember(Name = "is_playable")]
		public bool is_playable { get; set; }
		[DataMember(Name = "tagList")]
		public List<TagList> tagList { get; set; }
		[DataMember(Name = "is_thread_owner")]
		public bool is_thread_owner { get; set; }
		[DataMember(Name = "width")]
		public int width { get; set; }
		[DataMember(Name = "height")]
		public int height { get; set; }

		[DataMember(Name = "ownerChannelInfo")]
		public OwnerChannelInfo ownerChannelInfo { get; set; }


		

	}

	public class ChannelInfo
	{
		[DataMember(Name = "id")]
		public string id { get; set; }
		[DataMember(Name = "name")]
		public string name { get; set; }
		[DataMember(Name = "favorite_token")]
		public string favorite_token { get; set; }
		[DataMember(Name = "favorite_token_time")]
		public int? favorite_token_time { get; set; }
		[DataMember(Name = "is_favorited")]
		public int? is_favorited { get; set; }
		[DataMember(Name = "icon_url")]
		public string icon_url { get; set; }


	}


	public class UploaderInfo
	{
		[DataMember(Name = "id")]
		public string id { get; set; }
		[DataMember(Name = "nickname")]
		public string nickname { get; set; }
		[DataMember(Name = "stamp_exp")]
		public string stamp_exp { get; set; }
		[DataMember(Name = "icon_url")]
		public string icon_url { get; set; }
		[DataMember(Name = "is_favorited")]
		public bool is_favorited { get; set; }
		[DataMember(Name = "is_uservideo_public")]
		public bool is_uservideo_public { get; set; }
		[DataMember(Name = "is_user_myvideo_public")]
		public bool is_user_myvideo_public { get; set; }
		[DataMember(Name = "is_user_openlist_public")]
		public bool is_user_openlist_public { get; set; }

		
	}

	public class ViewerInfo
	{
		[DataMember(Name = "id")]
		public int id { get; set; }
		[DataMember(Name = "nickname")]
		public string nickname { get; set; }
		[DataMember(Name = "isPremium")]
		public bool isPremium { get; set; }
		[DataMember(Name = "isPrivileged")]
		public bool isPrivileged { get; set; }
	}

	public class WatchApiResponse : FlvResponse
	{
		[DataMember(Name = "flashvars")]
		public Flashvars flashvars { get; set; }
		[DataMember(Name = "videoDetail")]
		public VideoDetail videoDetail { get; set; }
		[DataMember(Name = "channelInfo")]
		public ChannelInfo channelInfo { get; set; }
		[DataMember(Name = "uploaderInfo")]
		public UploaderInfo UploaderInfo { get; set; }
		[DataMember(Name = "viewerInfo")]
		public ViewerInfo viewerInfo { get; set; }
		[DataMember(Name = "tagRelatedMarquee")]
		public object tagRelatedMarquee { get; set; }
		[DataMember(Name = "googleAdNgReasons")]
		public List<string> googleAdNgReasons { get; set; }
		[DataMember(Name = "playlistToken")]
		public string playlistToken { get; set; }

		[DataMember(Name = "tagRelatedBanner")]
		public object tagRelatedBanner { get; set; }


		public Dictionary<string, string> GetFlvInfo()
		{
			var decoded = System.Net.WebUtility.UrlDecode(this.flashvars.flvInfo);
			return decoded.Split(new char[] { '&' }).ToDictionary(
					source => source.Substring(0, source.IndexOf('=')),
					source => Uri.UnescapeDataString(source.Substring(source.IndexOf('=') + 1)));
		}


		[OnDeserialized]
		private void SetValuesOnDeserialized(StreamingContext context)
		{
			SetupFlvData(GetFlvInfo());
		}
	}

}
