using Mntone.Nico2.Live;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Nicocas.Live
{
    [DataContract]
    public class Meta
    {

        [DataMember(Name = "status")]
        public int Status { get; set; }
    }

    [DataContract]
    public class OnAirTime
    {

        [DataMember(Name = "beginAt")]
        public DateTime BeginAt { get; set; }

        [DataMember(Name = "endAt")]
        public DateTime EndAt { get; set; }
    }

    [DataContract]
    public class ShowTime
    {

        [DataMember(Name = "beginAt")]
        public DateTime BeginAt { get; set; }

        [DataMember(Name = "endAt")]
        public DateTime EndAt { get; set; }
    }

    [DataContract]
    public class Tag
    {

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "isLocked")]
        public bool IsLocked { get; set; }

        [DataMember(Name = "isDeletable")]
        public bool IsDeletable { get; set; }

        [DataMember(Name = "isExistNicopedia")]
        public bool IsExistNicopedia { get; set; }

        public bool IsCategoryTag => Type == "category";
    }

    [DataContract]
    public class BroadcastStreamSettings
    {

        [DataMember(Name = "maxQuality")]
        public string MaxQuality { get; set; }

        [DataMember(Name = "isPortrait")]
        public bool IsPortrait { get; set; }
    }

    [DataContract]
    public class Function
    {

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "enabled")]
        public bool Enabled { get; set; }

        [DataMember(Name = "permission")]
        public string Permission { get; set; }
    }

    [DataContract]
    public class Timeshift
    {

        [DataMember(Name = "enabled")]
        public bool Enabled { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }
    }

    [DataContract]
    public class DeviceFilter
    {

        [DataMember(Name = "isPlayable")]
        public bool IsPlayable { get; set; }

        [DataMember(Name = "isListing")]
        public bool IsListing { get; set; }

        [DataMember(Name = "isArchivePlayable")]
        public bool IsArchivePlayable { get; set; }

        [DataMember(Name = "isChasePlayable")]
        public bool IsChasePlayable { get; set; }
    }

    [DataContract]
    public class NicoCasLiveProgramData
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "onAirTime")]
        public OnAirTime OnAirTime { get; set; }

        [DataMember(Name = "showTime")]
        public ShowTime ShowTime { get; set; }

        [DataMember(Name = "viewers")]
        public int Viewers { get; set; }

        [DataMember(Name = "comments")]
        public int Comments { get; set; }

        [DataMember(Name = "timeshiftReservedCount")]
        public int TimeshiftReservedCount { get; set; }

        [DataMember(Name = "thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [DataMember(Name = "largeThumbnailUrl")]
        public string LargeThumbnailUrl { get; set; }

        [DataMember(Name = "large1920x1080ThumbnailUrl")]
        public string Large1920x1080ThumbnailUrl { get; set; }

        [DataMember(Name = "large352x198ThumbnailUrl")]
        public string Large352x198ThumbnailUrl { get; set; }

        [DataMember(Name = "liveCycle")]
        public string LiveCycle { get; set; }

        public StatusType LiveStatus => LiveCycle switch
        {
            "ended" => StatusType.Closed,
            "before_open" => StatusType.ComingSoon,
            "on_air" => StatusType.OnAir,
            var x => throw new NotSupportedException(x)
        };

        [DataMember(Name = "providerType")]
        public string ProviderType { get; set; }


        public CommunityType CommunityType => ProviderType switch
        {
            "user" => CommunityType.Community,
            "channel" => CommunityType.Channel,
            var x => throw new NotSupportedException(x)
        };

        [DataMember(Name = "providerId")]
        public string ProviderId { get; set; }

        [DataMember(Name = "socialGroupId")]
        public string SocialGroupId { get; set; }

        [DataMember(Name = "isChannelRelatedOfficial")]
        public bool IsChannelRelatedOfficial { get; set; }

        [DataMember(Name = "isMemberOnly")]
        public bool IsMemberOnly { get; set; }

        [DataMember(Name = "tags")]
        public IList<Tag> Tags { get; set; }

        [DataMember(Name = "isIchibaEditable")]
        public bool IsIchibaEditable { get; set; }

        [DataMember(Name = "isOnlyRegisteredCommentable")]
        public bool IsOnlyRegisteredCommentable { get; set; }

        [DataMember(Name = "isQuotable")]
        public bool IsQuotable { get; set; }

        [DataMember(Name = "isNicocasWebProgram")]
        public bool IsNicocasWebProgram { get; set; }

        [DataMember(Name = "isDmc")]
        public bool IsDmc { get; set; }

        [DataMember(Name = "isPayProgram")]
        public bool IsPayProgram { get; set; }

        [DataMember(Name = "isDomesticOnly")]
        public bool IsDomesticOnly { get; set; }

        [DataMember(Name = "isNicoAdEnabled")]
        public bool IsNicoAdEnabled { get; set; }

        [DataMember(Name = "isGiftEnabled")]
        public bool IsGiftEnabled { get; set; }

        [DataMember(Name = "isProductSerialEnabled")]
        public bool IsProductSerialEnabled { get; set; }

        [DataMember(Name = "broadcastStreamSettings")]
        public BroadcastStreamSettings BroadcastStreamSettings { get; set; }

        [DataMember(Name = "isSemiOfficial")]
        public bool IsSemiOfficial { get; set; }

        [DataMember(Name = "isTagOwnerLock")]
        public bool IsTagOwnerLock { get; set; }

        [DataMember(Name = "programType")]
        public string ProgramType { get; set; }

        [DataMember(Name = "functions")]
        public IList<Function> Functions { get; set; }

        [DataMember(Name = "timeshift")]
        public Timeshift Timeshift { get; set; }

        [DataMember(Name = "deviceFilter")]
        public DeviceFilter DeviceFilter { get; set; }

        [DataMember(Name = "advertisementType")]
        public string AdvertisementType { get; set; }

        [DataMember(Name = "twitterHashTag")]
        public string TwitterHashTag { get; set; }

        [DataMember(Name = "isPremiumAppeal")]
        public bool IsPremiumAppeal { get; set; }

        [DataMember(Name = "isEmotionEnabled")]
        public bool IsEmotionEnabled { get; set; }
    }

    [DataContract]
    public class NicoCasLiveProgramResponse
    {

        [DataMember(Name = "meta")]
        public Meta Meta { get; set; }

        [DataMember(Name = "data")]
        public NicoCasLiveProgramData Data { get; set; }


        public bool IsOK => Meta.Status == 200;
    }


}
