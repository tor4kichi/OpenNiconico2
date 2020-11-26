using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.NicoRepo
{
    [DataContract]
    public class NicoRepoMeta
    {

        [DataMember(Name = "status")]
        public int Status { get; set; }

        [DataMember(Name = "maxId")]
        public string MaxId { get; set; }

        [DataMember(Name = "minId")]
        public string MinId { get; set; }

        [DataMember(Name = "impressionId")]
        public string ImpressionId { get; set; }

        [DataMember(Name = "clientAppGroup")]
        public string ClientAppGroup { get; set; }

        [DataMember(Name = "_limit")]
        public int Limit { get; set; }
    }

    /*
    [DataContract]
    public class Sender
    {

        [DataMember(Name = "idType")]
        public string IdType { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }
    }

    [DataContract]
    public class MuteContext
    {

        [DataMember(Name = "sender")]
        public Sender Sender { get; set; }

        [DataMember(Name = "trigger")]
        public string Trigger { get; set; }
    }
    */

    [DataContract]
    public class Urls
    {

        [DataMember(Name = "s50x50")]
        public string S50x50 { get; set; }
    }

    [DataContract]
    public class DefaultValue
    {

        [DataMember(Name = "urls")]
        public Urls Urls { get; set; }
    }

    [DataContract]
    public class Tags
    {

        [DataMember(Name = "defaultValue")]
        public DefaultValue DefaultValue { get; set; }
    }

    [DataContract]
    public class Icons
    {

        [DataMember(Name = "tags")]
        public Tags Tags { get; set; }
    }

    [DataContract]
    public class SenderNiconicoUser
    {

        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "icons")]
        public Icons Icons { get; set; }
    }

    [DataContract]
    public class ThumbnailUrl
    {

        [DataMember(Name = "small")]
        public string Small { get; set; }

        [DataMember(Name = "normal")]
        public string Normal { get; set; }
    }

    [DataContract]
    public class Community
    {

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "thumbnailUrl")]
        public ThumbnailUrl ThumbnailUrl { get; set; }
    }

    [DataContract]
    public class Program
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "beginAt")]
        public DateTime BeginAt { get; set; }

        [DataMember(Name = "isPayProgram")]
        public bool IsPayProgram { get; set; }

        [DataMember(Name = "thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }
    }

    [DataContract]
    public class Resolution
    {

        [DataMember(Name = "width")]
        public int Width { get; set; }

        [DataMember(Name = "height")]
        public int Height { get; set; }
    }

    [DataContract]
    public class Video
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "resolution")]
        public Resolution Resolution { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "thumbnailUrl")]
        public ThumbnailUrl ThumbnailUrl { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "videoWatchPageId")]
        public string VideoWatchPageId { get; set; }
    }

    [DataContract]
    public class CommunityForFollower
    {

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "thumbnailUrl")]
        public ThumbnailUrl ThumbnailUrl { get; set; }
    }

    [DataContract]
    public class MemberOnlyVideo
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "resolution")]
        public Resolution Resolution { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "thumbnailUrl")]
        public ThumbnailUrl ThumbnailUrl { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "videoWatchPageId")]
        public string VideoWatchPageId { get; set; }
    }

    [DataContract]
    public class SenderChannel
    {

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "thumbnailUrl")]
        public string ThumbnailUrl { get; set; }
    }

    [DataContract]
    public class WatchUrls
    {

        [DataMember(Name = "pcUrl")]
        public string PcUrl { get; set; }
    }

    [DataContract]
    public class ChannelArticle
    {

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "watchUrls")]
        public WatchUrls WatchUrls { get; set; }
    }

    [DataContract]
    public class NicoRepoTimelineItem
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "topic")]
        public string Topic { get; set; }

        [DataMember(Name = "createdAt")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "isVisible")]
        public bool IsVisible { get; set; }

        [DataMember(Name = "isMuted")]
        public bool IsMuted { get; set; }

        [DataMember(Name = "isDeletable")]
        public bool? IsDeletable { get; set; }

        [DataMember(Name = "muteContext")]
        public MuteContext MuteContext { get; set; }

        [DataMember(Name = "senderChannel")]
        public SenderChannel SenderChannel { get; set; }

        [DataMember(Name = "senderNiconicoUser")]
        public SenderNiconicoUser SenderNiconicoUser { get; set; }

//        [DataMember(Name = "actionLog")]
//        public IList<object> ActionLog { get; set; }

        [DataMember(Name = "channelArticle")]
        public ChannelArticle ChannelArticle { get; set; }

        [DataMember(Name = "community")]
        public Community Community { get; set; }

        [DataMember(Name = "program")]
        public Program Program { get; set; }

        [DataMember(Name = "video")]
        public Video Video { get; set; }

        [DataMember(Name = "communityForFollower")]
        public CommunityForFollower CommunityForFollower { get; set; }

        [DataMember(Name = "memberOnlyVideo")]
        public MemberOnlyVideo MemberOnlyVideo { get; set; }
    }

    [DataContract]
    public class NicoRepoResponse
    {

        [DataMember(Name = "meta")]
        public NicoRepoMeta Meta { get; set; }

        [DataMember(Name = "data")]
        [JsonConverter(typeof(SingleOrArrayConverter<NicoRepoTimelineItem>))]
        public IList<NicoRepoTimelineItem> TimelineItems { get; set; }

//        [DataMember(Name = "errors")]
//        [JsonConverter(typeof(SingleOrArrayConverter<object>))]
//        public IList<object> Errors { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }



        public bool IsStatusOK => Status == "ok";

//        public bool HasError => Errors?.Any() ?? false;

        public NicoRepoTimelineItem LastTimelineItem => TimelineItems?.LastOrDefault();



        static public NicoRepoResponse ParseNicoRepoJson(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<NicoRepoResponse>(json);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
                return null;
            }
        }
    }


    

}
