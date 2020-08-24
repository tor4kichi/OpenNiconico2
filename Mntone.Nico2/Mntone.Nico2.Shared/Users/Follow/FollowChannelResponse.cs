namespace Mntone.Nico2.Users.Follow
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class FollowChannelResponse
    {
        [JsonProperty("meta")]
        public FollowChannelMeta Meta { get; set; }

        [JsonProperty("data")]
        public List<FollowChannel> Data { get; set; }

        public partial class FollowChannel
        {
            [JsonProperty("session")]
            public Session Session { get; set; }

            [JsonProperty("id")]
            public long Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("isFree")]
            public bool IsFree { get; set; }

            [JsonProperty("screenName")]
            public string ScreenName { get; set; }

            [JsonProperty("ownerName")]
            public string OwnerName { get; set; }

            [JsonProperty("price")]
            public long Price { get; set; }

            [JsonProperty("bodyPrice")]
            public long BodyPrice { get; set; }

            [JsonProperty("url")]
            public Uri Url { get; set; }

            [JsonProperty("thumbnailUrl")]
            public Uri ThumbnailUrl { get; set; }

            [JsonProperty("thumbnailSmallUrl")]
            public Uri ThumbnailSmallUrl { get; set; }

            [JsonProperty("canAdmit")]
            public bool CanAdmit { get; set; }

            [JsonProperty("isAdult")]
            public bool IsAdult { get; set; }
        }

        public partial class Session
        {
            [JsonProperty("joining")]
            public bool Joining { get; set; }
        }

        public partial class FollowChannelMeta
        {
            [JsonProperty("status")]
            public long Status { get; set; }

            [JsonProperty("total")]
            public long Total { get; set; }

            [JsonProperty("count")]
            public long Count { get; set; }
        }
    }
}
