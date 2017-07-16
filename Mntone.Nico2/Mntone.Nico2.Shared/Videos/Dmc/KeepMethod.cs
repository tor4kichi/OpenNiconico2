using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Videos.Dmc
{
    [DataContract]
    public class KeepMethod
    {

        [DataMember(Name = "heartbeat")]
        public Heartbeat Heartbeat { get; set; }
    }

    [DataContract]
    public class Heartbeat
    {

        [DataMember(Name = "lifetime")]
        public int Lifetime { get; set; }

        [DataMember(Name = "onetime_token")]
        public string OnetimeToken { get; set; }
    }
}
