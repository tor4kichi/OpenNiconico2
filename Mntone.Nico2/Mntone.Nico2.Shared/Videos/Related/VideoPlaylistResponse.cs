using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Videos.Related
{
    [DataContract]
    public class PlaylistItem
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "requestId")]
        public string RequestId { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "thumbnailURL")]
        public string ThumbnailURL { get; set; }

        [DataMember(Name = "viewCounter")]
        public int ViewCounter { get; set; }

        [DataMember(Name = "numRes")]
        public int? NumRes { get; set; }

        [DataMember(Name = "mylistCounter")]
        public int MylistCounter { get; set; }

        [DataMember(Name = "firstRetrieve")]
        public string FirstRetrieve { get; set; }

        [DataMember(Name = "lengthSeconds")]
        public int LengthSeconds { get; set; }

        [DataMember(Name = "threadUpdateTime")]
        public string ThreadUpdateTime { get; set; }

        [DataMember(Name = "createTime")]
        public string CreateTime { get; set; }

        [DataMember(Name = "width")]
        public int? Width { get; set; }

        [DataMember(Name = "height")]
        public int? Height { get; set; }

        [DataMember(Name = "isTranslated")]
        public bool IsTranslated { get; set; }

        [DataMember(Name = "mylistComment")]
        public string MylistComment { get; set; }

        [DataMember(Name = "tkasType")]
        public int? TkasType { get; set; }

        [DataMember(Name = "hasData")]
        public bool HasData { get; set; }
    }

    [DataContract]
    public class PlaylistData
    {

        [DataMember(Name = "items")]
        public IList<PlaylistItem> Items { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "ref")]
        public string Ref { get; set; }

        [DataMember(Name = "option")]
        public IList<object> Option { get; set; }
    }

    [DataContract]
    public class VideoPlaylistResponse
    {

        [DataMember(Name = "data")]
        public PlaylistData Data { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }
    }
}