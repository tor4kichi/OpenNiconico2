using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Nicoad.Video
{
    [DataContract]
    public class Meta
    {

        [DataMember(Name = "status")]
        public int Status { get; set; }
    }

    [DataContract]
    public class History
    {

        [DataMember(Name = "advertiserName")]
        public string AdvertiserName { get; set; }

        [DataMember(Name = "nicoadId")]
        public int NicoadId { get; set; }

        [DataMember(Name = "adPoint")]
        public int AdPoint { get; set; }

        [DataMember(Name = "contribution")]
        public int Contribution { get; set; }

        [DataMember(Name = "startedAt")]
        public int StartedAt { get; set; }

        [DataMember(Name = "endedAt")]
        public int EndedAt { get; set; }

        [DataMember(Name = "userId")]
        public int? UserId { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }
    }

    [DataContract]
    public class Data
    {

        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "serverTime")]
        public int ServerTime { get; set; }

        [DataMember(Name = "histories")]
        public IList<History> Histories { get; set; }
    }

    [DataContract]
    public class VideoAdHistoryResponse
    {

        [DataMember(Name = "meta")]
        public Meta Meta { get; set; }

        [DataMember(Name = "data")]
        public Data Data { get; set; }
    }



}
