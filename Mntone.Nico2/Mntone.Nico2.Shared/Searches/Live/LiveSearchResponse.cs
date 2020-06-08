using Mntone.Nico2.Live;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Searches.Live
{
    [DataContract]
    public class LiveSearchResultItem
    {
        [DataMember(Name = "communityId")]
        public int? CommunityId { get; set; }

        [DataMember(Name = "openTime")]
        public DateTime? OpenTime { get; set; }

        [DataMember(Name = "startTime")]
        public DateTime? StartTime { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "communityIcon")]
        public string CommunityIcon { get; set; }

        [DataMember(Name = "tags")]
        public string Tags { get; set; }

        [DataMember(Name = "liveEndTime")]
        public DateTime? LiveEndTime { get; set; }

        [DataMember(Name = "timeshiftEnabled")]
        public bool? TimeshiftEnabled { get; set; }

        [DataMember(Name = "categoryTags")]
        public string CategoryTags { get; set; }

        [DataMember(Name = "viewCounter")]
        public int? ViewCounter { get; set; }

        [DataMember(Name = "providerType")]
        public string ProviderType { get; set; }

        [DataMember(Name = "contentId")]
        public string ContentId { get; set; }

        [DataMember(Name = "userId")]
        public int? UserId { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "memberOnly")]
        public bool? MemberOnly { get; set; }

        [DataMember(Name = "scoreTimeshiftReserved")]
        public int? ScoreTimeshiftReserved { get; set; }

        [DataMember(Name = "commentCounter")]
        public int? CommentCounter { get; set; }

        [DataMember(Name = "communityText")]
        public string CommunityText { get; set; }

        [DataMember(Name = "channelId")]
        public int? ChannelId { get; set; }

        [DataMember(Name = "thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [DataMember(Name = "liveStatus")]
        public string LiveStatus { get; set; }



        public CommunityType GetCommunityType() => ProviderType switch
        {
            "community" => CommunityType.Community,
            "channel" => CommunityType.Channel,
            "official" => CommunityType.Official,
            _ => CommunityType.Community
        };
    }

    [DataContract]
    public class Meta
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "totalCount")]
        public int? TotalCount { get; set; }

        [DataMember(Name = "status")]
        public int Status { get; set; }

        [DataMember(Name = "errorCode")]
        public string ErrorCode { get; set; }

        [DataMember(Name = "errorMessage")]
        public string ErrorMessage { get; set; }
    }

    [DataContract]
    public class LiveSearchResponse
    {
        [DataMember(Name = "data")]
        public IList<LiveSearchResultItem> Data { get; set; }

        [DataMember(Name = "meta")]
        public Meta Meta { get; set; }


        public bool IsOK => Meta?.Status == 200;
        public bool IsQueryParseError => Meta?.Status == 400;
        public bool IsIsInternalServerError => Meta?.Status == 500;
        public bool IsMaintenance => Meta?.Status == 503;
    }




}
