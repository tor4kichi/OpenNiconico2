using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Searches.Video
{
    [DataContract]
    public class VideoInfoResponse
    {

        [DataMember(Name = "video")]
        public Video Video { get; set; }

        [DataMember(Name = "thread")]
        public Thread Thread { get; set; }

        [DataMember(Name = "tags")]
        public VideoInfoTags Tags { get; set; }

        [DataMember(Name = "@status")]
        public string Status { get; set; }
    }

    [DataContract]
    public class TagInfo
    {

        [DataMember(Name = "tag")]
        public string Tag { get; set; }

        [DataMember(Name = "area")]
        public string Area { get; set; }
    }

    [DataContract]
    public class VideoInfoTags
    {

        [DataMember(Name = "tag_info")]
        [JsonConverter(typeof(SingleOrArrayConverter<TagInfo>))]
        public List<TagInfo> TagInfo { get; set; }
    }

    [DataContract]
    public class VideoInfoResponseContainer
    {

        [DataMember(Name = "niconico_response")]
        public VideoInfoResponse NicovideoVideoResponse { get; set; }
    }

    [DataContract]
    public class VideoInfoArrayResponseContainer
    {

        [DataMember(Name = "niconico_response")]
        public VideoInfoArrayResponse DataContainer { get; set; }
    }

    [DataContract]
    public class VideoInfoArrayResponse
    {
        [DataMember(Name = "video_info")]
        [JsonConverter(typeof(SingleOrArrayConverter<VideoInfoResponse>))]
        public VideoInfoResponse[] Items { get; set; }

        [DataMember(Name = "count")]
        public string Count { get; set; }

        [DataMember(Name = "@status")]
        public string Status { get; set; }

    }
}
