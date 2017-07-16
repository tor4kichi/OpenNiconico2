using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Videos.Dmc
{
    [DataContract]
    public class DmcSessionRequest
    {
        [DataMember(Name = "session")]
        public RequestSession Session { get; set; }
    }

    [DataContract]
    public class RequestSession
    {
        // res req
        [DataMember(Name = "recipe_id")]
        public string RecipeId { get; set; }

        // res req
        [DataMember(Name = "content_id")]
        public string ContentId { get; set; }

        // res req
        [DataMember(Name = "content_src_id_sets")]
        public IList<ContentSrcIdSet> ContentSrcIdSets { get; set; }

        // res req
        [DataMember(Name = "content_type")]
        public string ContentType { get; set; }

        // res req
        [DataMember(Name = "timing_constraint")]
        public string TimingConstraint { get; set; }

        // res req
        [DataMember(Name = "keep_method")]
        public KeepMethod KeepMethod { get; set; }

        // res req
        [DataMember(Name = "protocol")]
        public Protocol Protocol { get; set; }

        // res req (req is string.Empty)
        [DataMember(Name = "content_uri")]
        public string ContentUri { get; set; }

        // res req
        [DataMember(Name = "session_operation_auth")]
        public SessionOperationAuth_Request SessionOperationAuth { get; set; }

        // res req
        [DataMember(Name = "content_auth")]
        public ContentAuth_Request ContentAuth { get; set; }

        // res req
        [DataMember(Name = "client_info")]
        public ClientInfo ClientInfo { get; set; }

        // res req
        [DataMember(Name = "priority")]
        public double Priority { get; set; }

    }

   
}
