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
        public string UserId { get; set; }

        public uint UserIdNumber => uint.Parse(UserId);


        [DataMember(Name = "open_time")]
        public string __OpenTime { get; set; }


        public DateTime? OpenTime => __OpenTime != null ? DateTime.Parse(__OpenTime) : default(DateTime?);

        [DataMember(Name = "start_time")]
        public string __StartTime { get; set; }

        public DateTime? StartTime => __StartTime != null ? DateTime.Parse(__StartTime) : default(DateTime?);


        [DataMember(Name = "end_time")]
        public string __EndTime { get; set; }

        public DateTime? EndTime => __EndTime != null ? DateTime.Parse(__EndTime) : default(DateTime?);


        [DataMember(Name = "provider_type")]
        public string __ProviderType { get; set; }

        public CommunityType ProviderType
        {
            get
            {
                if (__ProviderType == "official") { return CommunityType.Official; }
                if (__ProviderType == "community") { return CommunityType.Community; }
                if (__ProviderType == "channel") { return CommunityType.Channel; }

                throw new NotSupportedException("not support CommunityType, " + __ProviderType);
            }
        }

        [DataMember(Name = "related_channel_id")]
        public string RelatedChannelId { get; set; }

        [DataMember(Name = "_thumbnail_url")]
        public string ThumbnailUrl { get; set; }

        [DataMember(Name = "_picture_url")]
        public string PictureUrl { get; set; }

        [DataMember(Name = "_currentstatus")]
        public string Currentstatus { get; set; }

        [DataMember(Name = "hidescore_online")]
        public string HidescoreOnline { get; set; }

        [DataMember(Name = "hidescore_comment")]
        public string HidescoreComment { get; set; }

        [DataMember(Name = "community_only")]
        public string CommunityOnly { get; set; }

        public bool IsCommunityOnly => CommunityOnly.ToBooleanFrom1();

        [DataMember(Name = "channel_only")]
        public string ChannelOnly { get; set; }

        public bool IsChannelOnly => ChannelOnly.ToBooleanFrom1();

        [DataMember(Name = "view_counter")]
        public string __ViewCounter { get; set; }

        public int ViewCounter => int.Parse(__ViewCounter);

        [DataMember(Name = "comment_count")]
        public string __CommentCount { get; set; }

        public int CommentCount => int.Parse(__CommentCount);

        [DataMember(Name = "is_panorama")]
        public string IsPanorama { get; set; }

        [DataMember(Name = "_timeshift_limit")]
        public string __TimeshiftLimit { get; set; }

        public int TimeshiftLimit => int.Parse(__TimeshiftLimit);


        [DataMember(Name = "_ts_archive_released_time")]
        public string TsArchiveReleasedTime { get; set; }

        [DataMember(Name = "_use_tsarchive")]
        public string UseTsarchive { get; set; }

        [DataMember(Name = "_ts_archive_start_time")]
        public string TsArchiveStartTime { get; set; }

        [DataMember(Name = "_ts_archive_end_time")]
        public string TsArchiveEndTime { get; set; }

        [DataMember(Name = "_ts_view_limit_num")]
        public string TsViewLimitNum { get; set; }

        [DataMember(Name = "_ts_is_endless")]
        public string TsIsEndless { get; set; }

        [DataMember(Name = "_ts_reserved_count")]
        public string TsReservedCount { get; set; }

        [DataMember(Name = "timeshift_enabled")]
        public string __TimeshiftEnabled { get; set; }

        public bool IsTimeshiftEnabled => __TimeshiftEnabled.ToBooleanFrom1();

        [DataMember(Name = "is_hq")]
        public string __IsHq { get; set; }

        public bool IsHq => __IsHq.ToBooleanFrom1();
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
        public string Public { get; set; }

        public bool IsPublic => Public.ToBooleanFrom1();

        [DataMember(Name = "global_id")]
        public string GlobalId { get; set; }

        [DataMember(Name = "user_count")]
        public string __UserCount { get; set; }

        public int UserCount => int.Parse(__UserCount);

        [DataMember(Name = "level")]
        public string __Level { get; set; }

        public int Level => int.Parse(__Level);

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
        public IList<string> Livetag { get; set; } = new List<string>();
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
        public VideoInfo VideoInfo { get; set; }

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
