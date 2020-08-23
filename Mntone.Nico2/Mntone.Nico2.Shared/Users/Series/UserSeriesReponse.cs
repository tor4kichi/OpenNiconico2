namespace Mntone.Nico2.Users.Series
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class UserSeriesResponse
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("totalCount")]
        public long TotalCount { get; set; }

        [JsonProperty("items")]
        public List<UserSeries> Items { get; set; }
    }

    public partial class UserSeries
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("owner")]
        public Owner Owner { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("isListed")]
        public bool IsListed { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("thumbnailUrl")]
        public Uri ThumbnailUrl { get; set; }

        [JsonProperty("itemsCount")]
        public long ItemsCount { get; set; }
    }

    public partial class Owner
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class Meta
    {
        [JsonProperty("status")]
        public long Status { get; set; }
    }
}