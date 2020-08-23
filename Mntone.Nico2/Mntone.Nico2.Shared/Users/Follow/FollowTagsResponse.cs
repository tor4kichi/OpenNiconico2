namespace Mntone.Nico2.Users.Follow
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class FollowTagsResponse
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public FollowTagsData Data { get; set; }

        public partial class FollowTagsData
        {
            [JsonProperty("tags")]
            public List<Tag> Tags { get; set; }
        }

        public partial class Tag
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("followedAt")]
            public DateTimeOffset FollowedAt { get; set; }

            [JsonProperty("nicodicSummary")]
            public string NicodicSummary { get; set; }
        }
    }
}
