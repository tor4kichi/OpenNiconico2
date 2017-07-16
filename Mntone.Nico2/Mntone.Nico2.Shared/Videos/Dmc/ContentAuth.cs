using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Videos.Dmc
{
    [DataContract]
    public class ContentAuth_Response : ContentAuth_Request
    {
        // res
        [DataMember(Name = "max_content_count")]
        public int MaxContentCount { get; set; }

        // res
        [DataMember(Name = "content_auth_info")]
        public ContentAuthInfo ContentAuthInfo { get; set; }
    }

    [DataContract]
    public class ContentAuth_Request
    {
        // res req
        [DataMember(Name = "auth_type")]
        public string AuthType { get; set; }

        // res req
        [DataMember(Name = "content_key_timeout")]
        public int ContentKeyTimeout { get; set; }

        // res req
        [DataMember(Name = "service_id")]
        public string ServiceId { get; set; }

        // res req
        [DataMember(Name = "service_user_id")]
        public string ServiceUserId { get; set; }
    }

    [DataContract]
    public class ContentAuthInfo
    {

        [DataMember(Name = "method")]
        public string Method { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }


}
