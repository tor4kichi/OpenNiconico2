using Mntone.Nico2.Users.Mylist;

namespace Mntone.Nico2.Users.Follow
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class FollowMylistResponse
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public FollowMylistData Data { get; set; }
    }

    public partial class FollowMylistData
    {
        [JsonProperty("followLimit")]
        public long FollowLimit { get; set; }

        [JsonProperty("mylists")]
        public List<FollowMylist> Mylists { get; set; }
    }

    public partial class FollowMylist
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("status")]
        public ContentStatus Status { get; set; }

        [JsonProperty("detail", NullValueHandling = NullValueHandling.Ignore)]
        public Detail Detail { get; set; }
    }

    public partial class Detail
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("isPublic")]
        public bool IsPublic { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("defaultSortKey")]
        public MylistSortKey DefaultSortKey { get; set; }

        [JsonProperty("defaultSortOrder")]
        public MylistSortOrder DefaultSortOrder { get; set; }

        [JsonProperty("itemsCount")]
        public long ItemsCount { get; set; }

        [JsonProperty("owner")]
        public Owner Owner { get; set; }

        [JsonProperty("sampleItems")]
        public List<SampleItem> SampleItems { get; set; }

        [JsonProperty("followerCount")]
        public long FollowerCount { get; set; }

        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("isFollowing")]
        public bool IsFollowing { get; set; }
    }

    public partial class Owner
    {
        [JsonProperty("ownerType")]
        public OwnerType OwnerType { get; set; }

        [JsonProperty("id")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long? Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("iconUrl")]
        public Uri IconUrl { get; set; }
    }

    public partial class SampleItem
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
        public ContentStatus Status { get; set; }

        [JsonProperty("video")]
        public Video Video { get; set; }
    }

    public partial class Video
    {
        [JsonProperty("type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("registeredAt")]
        public DateTimeOffset RegisteredAt { get; set; }

        [JsonProperty("count")]
        public Count Count { get; set; }

        [JsonProperty("thumbnail")]
        public Thumbnail Thumbnail { get; set; }

        [JsonProperty("duration")]
        public long Duration { get; set; }

        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonProperty("latestCommentSummary")]
        public string LatestCommentSummary { get; set; }

        [JsonProperty("isChannelVideo")]
        public bool IsChannelVideo { get; set; }

        [JsonProperty("isPaymentRequired")]
        public bool IsPaymentRequired { get; set; }

        [JsonProperty("playbackPosition")]
        public object PlaybackPosition { get; set; }

        [JsonProperty("owner")]
        public Owner Owner { get; set; }

        [JsonProperty("9d091f87")]
        public bool The9D091F87 { get; set; }
    }

    public partial class Count
    {
        [JsonProperty("view")]
        public long View { get; set; }

        [JsonProperty("comment")]
        public long Comment { get; set; }

        [JsonProperty("mylist")]
        public long Mylist { get; set; }
    }

    public partial class Thumbnail
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("middleUrl")]
        public Uri MiddleUrl { get; set; }

        [JsonProperty("largeUrl")]
        public Uri LargeUrl { get; set; }

        [JsonProperty("listingUrl")]
        public Uri ListingUrl { get; set; }

        [JsonProperty("nHdUrl")]
        public Uri NHdUrl { get; set; }
    }

    public enum TypeEnum { Essential };

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }


}
