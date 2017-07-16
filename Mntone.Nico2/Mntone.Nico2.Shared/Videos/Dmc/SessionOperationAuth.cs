using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Videos.Dmc
{
    [DataContract]
    public class SessionOperationAuth_Response
    {

        [DataMember(Name = "session_operation_auth_by_signature")]
        public SessionOperationAuthBySignature_Response SessionOperationAuthBySignature { get; set; }
    }

    [DataContract]
    public class SessionOperationAuthBySignature_Response : SessionOperationAuthBySignature_Request
    {
        // Res
        [DataMember(Name = "created_time")]
        public long CreatedTime { get; set; }

        // Res
        [DataMember(Name = "expire_time")]
        public long ExpireTime { get; set; }

    }


    [DataContract]
    public class SessionOperationAuth_Request
    {

        [DataMember(Name = "session_operation_auth_by_signature")]
        public SessionOperationAuthBySignature_Request SessionOperationAuthBySignature { get; set; }
    }

    [DataContract]
    public class SessionOperationAuthBySignature_Request
    {

        // Res/Req
        [DataMember(Name = "token")]
        public string Token { get; set; }

        // Res/Req
        [DataMember(Name = "signature")]
        public string Signature { get; set; }
    }

}
