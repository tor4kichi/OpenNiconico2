namespace Mntone.Nico2.Users.User
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class UserDetailResponse
    {
        [JsonProperty("userDetails")]
        public UserDetailsContainer Container { get; set; }


        public partial class UserDetailsContainer
        {
            [JsonProperty("userDetails")]
            public UserDetails Details { get; set; }


        }

        public partial class UserDetails
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("user")]
            public User User { get; set; }

            [JsonProperty("followStatus")]
            public FollowStatus FollowStatus { get; set; }
        }

        public partial class FollowStatus
        {
            [JsonProperty("isFollowing")]
            public bool IsFollowing { get; set; }
        }

        public partial class User
        {
            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("strippedDescription")]
            public string StrippedDescription { get; set; }

            [JsonProperty("isPremium")]
            public bool IsPremium { get; set; }

            [JsonProperty("registeredVersion")]
            public string RegisteredVersion { get; set; }

            [JsonProperty("followeeCount")]
            public long FolloweeCount { get; set; }

            [JsonProperty("followerCount")]
            public long FollowerCount { get; set; }

            [JsonProperty("userLevel")]
            public UserLevel UserLevel { get; set; }

            [JsonProperty("userChannel")]
            public object UserChannel { get; set; }

            [JsonProperty("isNicorepoReadable")]
            public bool IsNicorepoReadable { get; set; }

            [JsonProperty("sns")]
            public List<object> Sns { get; set; }

            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("nickname")]
            public string Nickname { get; set; }

            [JsonProperty("icons")]
            public Icons Icons { get; set; }
        }

        public partial class Icons
        {
            [JsonProperty("small")]
            public Uri Small { get; set; }

            [JsonProperty("large")]
            public Uri Large { get; set; }
        }

        public partial class UserLevel
        {
            [JsonProperty("currentLevel")]
            public long CurrentLevel { get; set; }

            [JsonProperty("nextLevelThresholdExperience")]
            public long NextLevelThresholdExperience { get; set; }

            [JsonProperty("nextLevelExperience")]
            public long NextLevelExperience { get; set; }

            [JsonProperty("currentLevelExperience")]
            public long CurrentLevelExperience { get; set; }
        }
    }

}

