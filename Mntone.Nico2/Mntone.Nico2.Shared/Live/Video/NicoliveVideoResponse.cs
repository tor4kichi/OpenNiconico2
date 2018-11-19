using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Live.Video
{
    [DataContract]
    public class Video
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "user_id")]
        private string __UserId { get; set; }

        private uint? _UserId;
        public uint UserId => (_UserId ?? (_UserId = uint.Parse(__UserId))).Value;


        [DataMember(Name = "open_time")]
        private string __OpenTime { get; set; }

        private DateTimeOffset? _OpenTime;
        public DateTimeOffset? OpenTime => _OpenTime ?? (_OpenTime = !string.IsNullOrEmpty(__OpenTime) ? DateTimeOffset.Parse(__OpenTime) : default(DateTimeOffset?));

        [DataMember(Name = "start_time")]
        private string __StartTime { get; set; }

        private DateTimeOffset? _StartTime;
        public DateTimeOffset? StartTime => _StartTime ?? (_StartTime = !string.IsNullOrEmpty(__StartTime) ? DateTimeOffset.Parse(__StartTime) : default(DateTimeOffset?));


        [DataMember(Name = "end_time")]
        private string __EndTime { get; set; }

        private DateTimeOffset? _EndTime;
        public DateTimeOffset? EndTime => _EndTime ?? (_EndTime = !string.IsNullOrEmpty(__EndTime) ? DateTimeOffset.Parse(__EndTime) : default(DateTimeOffset?));


        [DataMember(Name = "provider_type")]
        private string __ProviderType { get; set; }

        private CommunityType? _ProviderType;
        public CommunityType ProviderType
        {
            get
            {
                if (_ProviderType == null)
                {
                    switch (__ProviderType)
                    {
                        case "official":
                            _ProviderType = CommunityType.Official;
                            break;
                        case "community":
                            _ProviderType = CommunityType.Community;
                            break;
                        case "channel":
                            _ProviderType = CommunityType.Channel;
                            break;
                        default:
                            throw new NotSupportedException("not support CommunityType, " + __ProviderType);
                    }
                }

                return _ProviderType.Value;
            }
        }

        [DataMember(Name = "related_channel_id")]
        public string RelatedChannelId { get; set; }

        [DataMember(Name = "_thumbnail_url")]
        public string ThumbnailUrl { get; set; }

        public bool HasThumbnailUrl => !string.IsNullOrEmpty(ThumbnailUrl);

        [DataMember(Name = "_picture_url")]
        public string PictureUrl { get; set; }

        public bool HasPictureUrl => !string.IsNullOrEmpty(PictureUrl);


        [DataMember(Name = "_currentstatus")]
        private string __Currentstatus { get; set; }

        private StatusType? _CurrentStatus;
        public StatusType CurrentStatus
        {
            get
            {
                return (_CurrentStatus ?? (_CurrentStatus = StatusTypeExtensions.ToStatusType(__Currentstatus))).Value;
            }
        }
            

        

        [DataMember(Name = "hidescore_online")]
        public string HidescoreOnline { get; set; }



        [DataMember(Name = "hidescore_comment")]
        public string HidescoreComment { get; set; }

        [DataMember(Name = "community_only")]
        private string __IsCommunityOnly { get; set; }

        private bool? _IsCommunityOnly;
        public bool IsCommunityOnly => (_IsCommunityOnly ?? (_IsCommunityOnly = __IsCommunityOnly.ToBooleanFrom1())).Value;

        [DataMember(Name = "channel_only")]
        private string __IsChannelOnly { get; set; }

        private bool? _IsChannelOnly;
        public bool IsChannelOnly => (_IsChannelOnly ?? (_IsChannelOnly = __IsChannelOnly.ToBooleanFrom1())).Value;

        [DataMember(Name = "view_counter")]
        private string __ViewCount { get; set; }

        private int? _ViewCount;
        public int ViewCount => (_ViewCount ?? (_ViewCount = int.Parse(__ViewCount))).Value;

        [DataMember(Name = "comment_count")]
        private string __CommentCount { get; set; }

        private int? _CommentCount;
        public int CommentCount => (_CommentCount ?? (_CommentCount = int.Parse(__CommentCount))).Value;

        [DataMember(Name = "is_panorama")]
        private string __IsPanorama { get; set; }

        private bool? _IsPanorama;
        public bool IsPanorama => (_IsPanorama ?? (_IsPanorama = __IsPanorama.ToBooleanFrom1())).Value;


        [DataMember(Name = "_timeshift_limit")]
        private string __TimeshiftLimit { get; set; }

        private int? _TimeshiftLimit;
        public int TimeshiftLimit => (_TimeshiftLimit ?? (_TimeshiftLimit = int.Parse(__TimeshiftLimit))).Value;


        [DataMember(Name = "_ts_archive_released_time")]
        private string __TsArchiveReleasedTime { get; set; }

        private DateTimeOffset? _TsArchiveReleasedTime;
        public DateTimeOffset? TsArchiveReleasedTime => _TsArchiveReleasedTime ?? (_TsArchiveReleasedTime = !string.IsNullOrEmpty(__TsArchiveReleasedTime) ? DateTimeOffset.Parse(__TsArchiveReleasedTime) : default(DateTimeOffset?));


        [DataMember(Name = "_use_tsarchive")]
        public string UseTsarchive { get; set; }

        [DataMember(Name = "_ts_archive_start_time")]
        private string __TsArchiveStartTime { get; set; }

        private DateTimeOffset? _TsArchiveStartTime;
        public DateTimeOffset? TsArchiveStartTime => _TsArchiveStartTime ?? (_TsArchiveStartTime = !string.IsNullOrEmpty(__TsArchiveStartTime) ? DateTimeOffset.Parse(__TsArchiveStartTime) : default(DateTimeOffset?));


        [DataMember(Name = "_ts_archive_end_time")]
        private string __TsArchiveEndTime { get; set; }

        private DateTimeOffset? _TsArchiveEndTime;
        public DateTimeOffset? TsArchiveEndTime => _TsArchiveEndTime ?? (_TsArchiveEndTime = !string.IsNullOrEmpty(__TsArchiveEndTime) ? DateTimeOffset.Parse(__TsArchiveEndTime) : default(DateTimeOffset?));

        [DataMember(Name = "_ts_view_limit_num")]
        private string __TsViewLimitNum { get; set; }

        private int? _TsViewLimitNum;
        public int TsViewLimitNum => (_TsViewLimitNum ?? (_TsViewLimitNum = int.Parse(__TsViewLimitNum))).Value;


        [DataMember(Name = "_ts_is_endless")]
        private string __TsIsEndless { get; set; }

        private bool? _TsIsEndless;
        public bool TsIsEndless => (_TsIsEndless ?? (_TsIsEndless = __TsIsEndless.ToBooleanFrom1())).Value;

        [DataMember(Name = "_ts_reserved_count")]
        private string __TsReservedCount { get; set; }

        private int? _TsReservedCount;
        public int TsReservedCount => (_TsReservedCount ?? (_TsReservedCount = int.Parse(__TsReservedCount))).Value;


        [DataMember(Name = "timeshift_enabled")]
        private string __TimeshiftEnabled { get; set; }

        private bool? _TimeshiftEnabled;
        public bool TimeshiftEnabled => (_TimeshiftEnabled ?? (_TimeshiftEnabled = __TimeshiftEnabled.ToBooleanFrom1())).Value;

        [DataMember(Name = "is_hq")]
        private string __IsHq { get; set; }

        private bool? _IsHq;
        public bool IsHq => (_IsHq ?? (_IsHq = __IsHq.ToBooleanFrom1())).Value;
    }

    [DataContract]
    public class Community
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "public")]
        private string __IsPublic { get; set; }

        private bool? _IsPublic;
        public bool IsPublic => (_IsPublic ?? (_IsPublic = __IsPublic.ToBooleanFrom1())).Value;

        [DataMember(Name = "global_id")]
        public string GlobalId { get; set; }

        [DataMember(Name = "user_count")]
        private string __UserCount { get; set; }

        private int? _UserCount;
        public int UserCount => (_UserCount ?? (_UserCount = int.Parse(__UserCount))).Value;


        [DataMember(Name = "level")]
        private string __Level { get; set; }

        private int? _Level;
        public int Level => (_Level ?? (_Level = int.Parse(__Level))).Value;

        [DataMember(Name = "thumbnail")]
        public string Thumbnail { get; set; }

        [DataMember(Name = "thumbnail_small")]
        public string ThumbnailSmall { get; set; }
    }

    [DataContract]
    public class LivetagsItem
    {

        [DataMember(Name = "livetag")]
        [JsonConverter(typeof(SingleOrArrayConverter<string>))]
        public IList<string> Tags { get; set; } = new List<string>();
    }

    [DataContract]
    public class Livetags
    {
        [DataMember(Name = "category")]
        public LivetagsItem Category { get; set; }

        [DataMember(Name = "locked")]
        public LivetagsItem Locked { get; set; }

        [DataMember(Name = "free")]
        public LivetagsItem Free { get; set; }
    }

    [DataContract]
    public class VideoInfo
    {

        [DataMember(Name = "video")]
        public Video Video { get; set; }

        [DataMember(Name = "community")]
        public Community Community { get; set; }

        [DataMember(Name = "livetags")]
        public Livetags Livetags { get; set; }
    }

    [DataContract]
    public class NicoliveVideoInfoResponse
    {

        [DataMember(Name = "video_info")]
        public VideoInfo VideoInfo { get; set; }

        [DataMember(Name = "@status")]
        public string Status { get; set; }

        public bool IsOK => Status == "ok";
    }

    [DataContract]
    public class NicoliveVideoInfoResponseContainer
    {
        [DataMember(Name = "nicolive_video_response")]
        public NicoliveVideoInfoResponse NicoliveVideoResponse { get; set; }
    }






    [DataContract]
    public class NicoliveCommunityVideoResponse
    {

        [DataMember(Name = "video_info")]
        [JsonConverter(typeof(SingleOrArrayConverter<VideoInfo>))]
        public IList<VideoInfo> VideoInfo { get; set; }

        [DataMember(Name = "@status")]
        public string Status { get; set; }

        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "total_count")]
        public int TotalCount { get; set; }


        public bool IsOK => Status == "ok";
    }

    [DataContract]
    public class NicoliveCommunityVideoResponseContainer
    {
        [DataMember(Name = "nicolive_video_response")]
        public NicoliveCommunityVideoResponse NicoliveVideoResponse { get; set; }
    }

}
