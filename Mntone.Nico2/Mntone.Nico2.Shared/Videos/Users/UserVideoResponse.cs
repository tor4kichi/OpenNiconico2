using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Mntone.Nico2.Videos.Users
{
    public class UserVideosResponse
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
        public List<Item> Items { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("type")]
        public TypeEnum Type { get; set; }

        [JsonProperty("isCommunityMemberOnly")]
        public bool IsCommunityMemberOnly { get; set; }

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
        public object Owner { get; set; }

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

    public partial class Meta
    {
        [JsonProperty("status")]
        public long Status { get; set; }
    }

    public enum TypeEnum { User };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                TypeEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "user")
            {
                return TypeEnum.User;
            }
            throw new Exception("Cannot unmarshal type TypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (TypeEnum)untypedValue;
            if (value == TypeEnum.User)
            {
                serializer.Serialize(writer, "user");
                return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }
}

