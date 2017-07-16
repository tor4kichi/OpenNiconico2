using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Videos.Dmc
{
    [DataContract]
    public class DmcSessionResponse
    {

        [DataMember(Name = "meta")]
        public Meta Meta { get; set; }

        [DataMember(Name = "data")]
        public Data Data { get; set; }
    }

    [DataContract]
    public class Meta
    {

        [DataMember(Name = "status")]
        public int Status { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }
    }

    [DataContract]
    public class Data
    {

        [DataMember(Name = "session")]
        public ResponseSession Session { get; set; }
    }

    [DataContract]
    public class ResponseSession
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
        public SessionOperationAuth_Response SessionOperationAuth { get; set; }

        // res req
        [DataMember(Name = "content_auth")]
        public ContentAuth_Response ContentAuth { get; set; }

        // res req
        [DataMember(Name = "client_info")]
        public ClientInfo ClientInfo { get; set; }

        // res req
        [DataMember(Name = "priority")]
        public double Priority { get; set; }

        // res
        [DataMember(Name = "id")]
        public string Id { get; set; }
        
        // res
        [DataMember(Name = "play_seek_time")]
        public int PlaySeekTime { get; set; }

        // res
        [DataMember(Name = "play_speed")]
        public double PlaySpeed { get; set; }

        // res
        [DataMember(Name = "runtime_info")]
        public RuntimeInfo RuntimeInfo { get; set; }
        
        // res
        [DataMember(Name = "created_time")]
        public long CreatedTime { get; set; }

        // res
        [DataMember(Name = "modified_time")]
        public long ModifiedTime { get; set; }
        
        // res
        [DataMember(Name = "content_route")]
        public int ContentRoute { get; set; }

        // res
        [DataMember(Name = "version")]
        public string Version { get; set; }

        // res
        [DataMember(Name = "content_status")]
        public string ContentStatus { get; set; }
    }

    [DataContract]
    public class RuntimeInfo
    {

        [DataMember(Name = "node_id")]
        public string NodeId { get; set; }

        [DataMember(Name = "execution_history")]
        public IList<object> ExecutionHistory { get; set; }
    }


}
