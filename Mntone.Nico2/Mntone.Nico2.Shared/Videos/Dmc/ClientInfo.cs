using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Videos.Dmc
{
    [DataContract]
    public class ClientInfo
    {
        // res req
        [DataMember(Name = "player_id")]
        public string PlayerId { get; set; }

        // res
        [DataMember(Name = "remote_ip")]
        public string RemoteIp { get; set; }

        // res
        [DataMember(Name = "tracking_info")]
        public string TrackingInfo { get; set; }
    }
}
