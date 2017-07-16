using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Videos.Dmc
{
    [DataContract]
    public class ContentSrcIdSet
    {
        // res req
        [DataMember(Name = "content_src_ids")]
        public IList<ContentSrcId> ContentSrcIds { get; set; }

        // res
        [DataMember(Name = "allow_subset")]
        public string AllowSubset { get; set; }
    }

    [DataContract]
    public class ContentSrcId
    {

        [DataMember(Name = "src_id_to_mux")]
        public SrcIdToMux SrcIdToMux { get; set; }
    }

    [DataContract]
    public class SrcIdToMux
    {

        [DataMember(Name = "video_src_ids")]
        public IList<string> VideoSrcIds { get; set; }

        [DataMember(Name = "audio_src_ids")]
        public IList<string> AudioSrcIds { get; set; }
    }
}
