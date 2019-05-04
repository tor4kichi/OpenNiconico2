using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Nicocas.Search
{
    public sealed class UserSearchResponse : NicocasSearchResponseBase<UserSearchResponse.UsersData>
    {
        [DataContract]
        public class Urls
        {

            [DataMember(Name = "150x150")]
            public string Icon_x150 { get; set; }

            [DataMember(Name = "50x50")]
            public string Icon_x50 { get; set; }
        }

        [DataContract]
        public class Icons
        {

            [DataMember(Name = "urls")]
            public Urls Urls { get; set; }
        }

        [DataContract]
        public class User
        {
            [DataMember(Name = "id")]
            public string Id { get; set; }

            [DataMember(Name = "nickname")]
            public string Nickname { get; set; }

            [DataMember(Name = "icons")]
            public Icons Icons { get; set; }

            [DataMember(Name = "description")]
            public string Description { get; set; }

            [DataMember(Name = "followerCount")]
            public int FollowerCount { get; set; }

            [DataMember(Name = "videoCount")]
            public int VideoCount { get; set; }

            [DataMember(Name = "liveCount")]
            public int LiveCount { get; set; }
        }

        [DataContract]
        public class UsersData
        {
            [DataMember(Name = "users")]
            public IList<User> Users { get; set; }
        }
    }

    
}
