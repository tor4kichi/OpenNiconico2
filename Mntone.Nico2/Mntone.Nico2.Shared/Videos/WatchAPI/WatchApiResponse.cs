using Mntone.Nico2.Videos.Flv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mntone.Nico2.Videos.WatchAPI
{
    public class WatchApiResponse : FlvResponse
	{
		internal WatchApiResponse(WatchApiJson json)
			: base(json.GetFlvInfo())
		{
			
			

			
		}
	}

	public class MorningPremium
	{
		public string status { get; set; }
		public string timing { get; set; }
		public bool from_top { get; set; }
	}

	public class Flashvars
	{
		public string community_id { get; set; }
		public string community_global_id { get; set; }
		public string watchAuthKey { get; set; }
		public string flvInfo { get; set; }
		public int isDmc { get; set; }
		public object isBackComment { get; set; }
		public string thumbTitle { get; set; }
		public string thumbDescription { get; set; }
		public string videoTitle { get; set; }
		public string threadPublic { get; set; }
		public int is_channel { get; set; }
		public int player_version_xml { get; set; }
		public int player_info_xml { get; set; }
		public int appli_promotion_info_xml { get; set; }
		public int translation_version_json { get; set; }
		public int userPrefecture { get; set; }
		public string csrfToken { get; set; }
		public string v { get; set; }
		public string videoId { get; set; }
		public string deleted { get; set; }
		public string mylist_counter { get; set; }
		public string mylistcomment_counter { get; set; }
		public string movie_type { get; set; }
		public string thumbImage { get; set; }
		public string userSex { get; set; }
		public int userAge { get; set; }
		public int us { get; set; }
		public string iee { get; set; }
		public string is_community_video { get; set; }
		public string is_community_thread { get; set; }
		public string channelId { get; set; }
		public string channelTopURL { get; set; }
		public string channelVideoURL { get; set; }
		public string channelName { get; set; }
		public int isNeedPayment { get; set; }
		public string dicArticleURL { get; set; }
		public string blogEmbedURL { get; set; }
		public string uadAdvertiseURL { get; set; }
		public string noBanner { get; set; }
		public string category { get; set; }
		public string categoryGroupKey { get; set; }
		public string categoryGroup { get; set; }
		public string noAppli { get; set; }
		public string isWide { get; set; }
		public string wv_id { get; set; }
		public string wv_title { get; set; }
		public string wv_code { get; set; }
		public int wv_time { get; set; }
		public string leaf_switch { get; set; }
		public bool use_getrelateditem { get; set; }
		public int tagHirobaId { get; set; }
		public string language { get; set; }
		public string area { get; set; }
		public object commentLanguage { get; set; }
		public bool isAuthenticationRequired { get; set; }
		public string watchTrackId { get; set; }
		public long watchPageServerTime { get; set; }
		public MorningPremium morningPremium { get; set; }

		
		public string ad { get; set; }
		
		public string communityPostURL { get; set; }


		public string videoUserId { get; set; }
	}

	public class OwnerChannelInfo
	{
		public int? id { get; set; }
		public int? communityId { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public bool? isFree { get; set; }
		public string screenName { get; set; }
		public string ownerName { get; set; }
		public int? price { get; set; }
		public int? bodyPrice { get; set; }
		public string url { get; set; }
		public string thumbnailUrl { get; set; }
		public string thumbnailSmallUrl { get; set; }
		public bool? canAdmit { get; set; }
	}

	public class TagList
	{
		public string id { get; set; }
		public string tag { get; set; }
		public bool? cat { get; set; }
		public bool? dic { get; set; }
		public string lck { get; set; }
	}

	public class VideoDetail
	{
		public string v { get; set; }
		public string id { get; set; }
		public string title { get; set; }
		public string description { get; set; }
		public bool is_translated { get; set; }
		public string title_original { get; set; }
		public string description_original { get; set; }
		public string thumbnail { get; set; }
		public string postedAt { get; set; }
		public int? length { get; set; }
		public int? viewCount { get; set; }
		public int? mylistCount { get; set; }
		public int? commentCount { get; set; }
		public int? mainCommunityId { get; set; }
		public int? communityId { get; set; }
		public int? channelId { get; set; }
		public bool isDeleted { get; set; }
		public bool isMymemory { get; set; }
		public bool isMonetized { get; set; }
		public bool isR18 { get; set; }
		public object is_adult { get; set; }
		public string language { get; set; }
		public string area { get; set; }
		public bool can_translate { get; set; }
		public bool video_translation_info { get; set; }
		public string category { get; set; }
		public string thread_id { get; set; }
		public string main_genre { get; set; }
		public object has_owner_thread { get; set; }
		public object is_video_owner { get; set; }
		public bool is_uneditable_tag { get; set; }
		public object commons_tree_exists { get; set; }
		public string yesterday_rank { get; set; }
		public string highest_rank { get; set; }
		public bool for_bgm { get; set; }
		public object is_nicowari { get; set; }
		public bool is_public { get; set; }
		public bool is_official { get; set; }
		public bool no_ichiba { get; set; }
		public string community_name { get; set; }
		public string dicArticleURL { get; set; }
		public bool is_playable { get; set; }
		public List<TagList> tagList { get; set; }
		public bool is_thread_owner { get; set; }
		public int width { get; set; }
		public int height { get; set; }


		
		public OwnerChannelInfo ownerChannelInfo { get; set; }


		

	}

	public class ChannelInfo
	{
		public string id { get; set; }
		public string name { get; set; }
		public string favorite_token { get; set; }
		public int? favorite_token_time { get; set; }
		public int? is_favorited { get; set; }
		public string icon_url { get; set; }


	}


	public class UploaderInfo
	{
		public string id { get; set; }
		public string nickname { get; set; }
		public string stamp_exp { get; set; }
		public string icon_url { get; set; }
		public bool is_favorited { get; set; }
		public bool is_uservideo_public { get; set; }
		public bool is_user_myvideo_public { get; set; }
		public bool is_user_openlist_public { get; set; }

		
	}

	public class ViewerInfo
	{
		public int id { get; set; }
		public string nickname { get; set; }
		public bool isPremium { get; set; }
		public bool isPrivileged { get; set; }
	}

	public class TagRelatedMarquee
	{
		public string id { get; set; }
		public string title { get; set; }
		public string url { get; set; }
	}

	public class WatchApiJson
	{
		public Flashvars flashvars { get; set; }
		public VideoDetail videoDetail { get; set; }
		public ChannelInfo channelInfo { get; set; }
		public ViewerInfo viewerInfo { get; set; }
		public object tagRelatedMarquee { get; set; }
		public List<string> googleAdNgReasons { get; set; }
		public string playlistToken { get; set; }

		public object tagRelatedBanner { get; set; }


		public Dictionary<string, string> GetFlvInfo()
		{
			return this.flashvars.flvInfo.Split(new char[] { '&' }).ToDictionary(
					source => source.Substring(0, source.IndexOf('=')),
					source => Uri.UnescapeDataString(source.Substring(source.IndexOf('=') + 1)));
		}

	}

}
