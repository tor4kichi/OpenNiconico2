namespace Mntone.Nico2.Users.Mylist
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;


    public partial class WatchAfterMylistGroupItemsResponse
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public WatchAfterMylistGroupItemsData Data { get; set; }
    }

    public partial class WatchAfterMylistGroupItemsData
    {
        [JsonProperty("watchLater")]
        public WatchAfterMylist Mylist { get; set; }
    }

    public partial class WatchAfterMylist
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("totalCount")]
        public long TotalItemCount { get; set; }

        [JsonProperty("hasNext")]
        public bool HasNext { get; set; }

        [JsonProperty("hasInvisibleItems")]
        public bool HasInvisibleItems { get; set; }
    }



    public partial class MylistGroupItemsResponse
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public MylistGroupItemsData Data { get; set; }
    }

    public partial class MylistGroupItemsData
    {
        [JsonProperty("mylist")]
        public Mylist Mylist { get; set; }
    }

    public partial class Mylist
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("totalItemCount")]
        public long TotalItemCount { get; set; }

        [JsonProperty("hasNext")]
        public bool HasNext { get; set; }

        [JsonProperty("hasInvisibleItems")]
        public bool HasInvisibleItems { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("itemId")]
        public long ItemId { get; set; }

        [JsonProperty("watchId")]
        public string WatchId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("addedAt")]
        public DateTimeOffset AddedAt { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("video")]
        public Video Video { get; set; }


        public bool IsDeleted => Status == "deleted";
    }

}
