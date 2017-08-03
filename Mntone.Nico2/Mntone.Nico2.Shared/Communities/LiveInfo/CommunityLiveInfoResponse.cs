using Mntone.Nico2.Live;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;


// this code genereted with http://jsonutils.com/


namespace Mntone.Nico2.Communities.Live
{
	[DataContract]
	public class LiveVideo
	{

		[DataMember(Name = "id")]
		public string Id { get; set; }

		[DataMember(Name = "title")]
		public string Title { get; set; }

		[DataMember(Name = "open_time")]
		public string __OpenTime { get; set; }


		private DateTime? _OpenTime;
		public DateTime OpenTime => _OpenTime.HasValue ?
			_OpenTime.Value :
			(_OpenTime = DateTime.Parse(__OpenTime)).Value;

		[DataMember(Name = "start_time")]
		public string __StartTime { get; set; }

		private DateTime? _StartTime;
		public DateTime StartTime => _StartTime.HasValue ?
			_StartTime.Value :
			(_StartTime = DateTime.Parse(__StartTime)).Value;


		[DataMember(Name = "schedule_end_time")]
		public string __ScheduleEndTime { get; set; }


		public DateTime? GetShcduleEndTime()
		{
			if (!string.IsNullOrEmpty(__ScheduleEndTime))
			{
				return DateTime.Parse(__ScheduleEndTime);
			}
			else
			{
				return null;
			}
		}


		[DataMember(Name = "end_time")]
		public string __EndTime { get; set; }

		private DateTime? _EndTime;
		public DateTime EndTime => _EndTime.HasValue ?
			_EndTime.Value :
			(_EndTime = DateTime.Parse(__EndTime)).Value;



		[DataMember(Name = "provider_type")]
		public string __ProviderType { get; set; }


		private CommunityType? _ProviderType;
		public CommunityType ProviderType => _ProviderType.HasValue ?
			_ProviderType.Value :
			(_ProviderType = __ProviderType.ToCommunityType()).Value;


		[DataMember(Name = "related_channel_id")]
		public string RelatedChannelId { get; set; }

		public bool HasRelatedChannelId => !string.IsNullOrEmpty(RelatedChannelId);


		[DataMember(Name = "hidescore_online")]
		public string __HidescoreOnline { get; set; }


		private bool? _HidescoreOnline;
		public bool HidescoreOnline => _HidescoreOnline.HasValue ?
			_HidescoreOnline.Value :
			(_HidescoreOnline = __HidescoreOnline.ToBooleanFrom1()).Value;


		[DataMember(Name = "hidescore_comment")]
		public string __HidescoreComment { get; set; }

		private bool? _HidescoreComment;
		public bool HidescoreComment => _HidescoreComment.HasValue ?
			_HidescoreComment.Value :
			(_HidescoreComment = __HidescoreComment.ToBooleanFrom1()).Value;



		[DataMember(Name = "community_only")]
		public string __CommunityOnly { get; set; }

		private bool? _CommunityOnly;
		public bool CommunityOnly => _CommunityOnly.HasValue ?
			_CommunityOnly.Value :
			(_CommunityOnly = __CommunityOnly.ToBooleanFrom1()).Value;


		[DataMember(Name = "channel_only")]
		public string __ChannelOnly { get; set; }

		private bool? _ChannelOnly;
		public bool ChannelOnly => _ChannelOnly.HasValue ?
			_ChannelOnly.Value :
			(_ChannelOnly = __ChannelOnly.ToBooleanFrom1()).Value;


		[DataMember(Name = "view_counter")]
		public string ViewCounter { get; set; }


		[DataMember(Name = "comment_count")]
		public string CommentCount { get; set; }

		[DataMember(Name = "_ts_reserved_count")]
		public string TsReservedCount { get; set; }

		[DataMember(Name = "timeshift_enabled")]
		public string __TimeshiftEnabled { get; set; }

		private bool? _TimeshiftEnabled;
		public bool TimeshiftEnabled => _TimeshiftEnabled.HasValue ?
			_TimeshiftEnabled.Value :
			(_TimeshiftEnabled = __TimeshiftEnabled.ToBooleanFrom1()).Value;


		[DataMember(Name = "is_hq")]
		public string __IsHq { get; set; }


		private bool? _IsHq;
		public bool IsHq => _IsHq.HasValue ?
			_IsHq.Value :
			(_IsHq = __IsHq.ToBooleanFrom1()).Value;

        [DataMember(Name = "_thumbnail_url")]
        public string ThumbnailUrl { get; set; }

        [DataMember(Name = "_picture_url")]
        public string PictureUrl { get; set; }
    }

	[DataContract]
	public class Community
	{

		[DataMember(Name = "id")]
		public string Id { get; set; }

		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "channel_id")]
		public string ChannelId { get; set; }

		public bool HasChannelId => !string.IsNullOrEmpty(ChannelId);

		[DataMember(Name = "global_id")]
		public string GlobalId { get; set; }

		[DataMember(Name = "thumbnail")]
		public string Thumbnail { get; set; }

		[DataMember(Name = "thumbnail_small")]
		public string ThumbnailSmall { get; set; }
	}

	[DataContract]
	public class NicoliveVideoInfo
	{

		[DataMember(Name = "video")]
		public LiveVideo Video { get; set; }

		[DataMember(Name = "community")]
		public Community Community { get; set; }
	}

	[DataContract]
	public class NicoliveVideoResponse
	{

		[DataMember(Name = "video_info")]
		[JsonConverter(typeof(SingleOrArrayConverter<LiveVideo>))]
		public List<NicoliveVideoInfo> VideoInfo { get; set; }

		[DataMember(Name = "count")]
		public string __Count { get; set; }

		private int? _Count;
		public int Count => _Count.HasValue ?
			_Count.Value :
			(_Count = int.Parse(__Count)).Value;




		[DataMember(Name = "total_count")]
		public string __TotalCount { get; set; }

		private int? _TotalCount;
		public int TotalCount => _TotalCount.HasValue ?
			_TotalCount.Value :
			(_TotalCount = int.Parse(__TotalCount)).Value;



		[DataMember(Name = "@status")]
		public string Status { get; set; }


		public bool IsStatusOK => Status == "ok";
	}

	[DataContract]
	public class CommunityLiveInfoResponse
	{

		[DataMember(Name = "nicolive_video_response")]
		public NicoliveVideoResponse NicoliveVideoResponse { get; set; }
	}
}
