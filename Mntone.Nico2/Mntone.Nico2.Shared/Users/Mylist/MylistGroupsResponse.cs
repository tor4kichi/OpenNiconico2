
namespace Mntone.Nico2.Users.Mylist
{

    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class MylistGroupsResponse
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public MylistGroupsData Data { get; set; }
    }

    public partial class MylistGroupsData
    {
        [JsonProperty("mylists")]
        public List<Mylist> Mylists { get; set; }
    }

    public partial class Mylist
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
        public string Id { get; set; }

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
        public string Type { get; set; }

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

    public partial class Meta
    {
        [JsonProperty("status")]
        public long Status { get; set; }
    }

    public enum MylistSortKey 
    {
        Title, 
        AddedAt,
        MylistComment, 
        RegisteredAt, 
        ViewCount, 
        LastCommentTime, 
        CommentCount, 
        MylistCount, 
        Duration 
    };

    internal static class EnumToQueryStringHelper
    {
        public static string ToQueryString(this MylistSortKey sortKey)
        {
            return sortKey switch
            {
                MylistSortKey.Title => "title",
                MylistSortKey.AddedAt => "addedAt",
                MylistSortKey.MylistComment => "mylistComment",
                MylistSortKey.RegisteredAt => "registeredAt",
                MylistSortKey.ViewCount => "viewCount",
                MylistSortKey.LastCommentTime => "lastCommentTime",
                MylistSortKey.CommentCount => "commentCount",
                MylistSortKey.MylistCount => "mylistCount",
                MylistSortKey.Duration => "duration",
            };
        }

        public static string ToQueryString(this MylistSortOrder sortOrder)
        {
            return sortOrder switch
            {
                MylistSortOrder.Asc => "asc",
                MylistSortOrder.Desc => "desc",
            };
        }
    }

    public enum MylistSortOrder { Asc, Desc };

    public enum OwnerType { Channel, Hidden, User };

    public enum ContentStatus { Deleted, Hidden, Public, Private };

    public enum TypeEnum { Essential };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                DefaultSortKeyConverter.Singleton,
                DefaultSortOrderConverter.Singleton,
                OwnerTypeConverter.Singleton,
                StatusConverter.Singleton,
                TypeEnumConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class DefaultSortKeyConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(MylistSortKey) || t == typeof(MylistSortKey?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "title":
                    return MylistSortKey.Title;
                case "addedAt":
                    return MylistSortKey.AddedAt;
                case "mylistComment":
                    return MylistSortKey.MylistComment;
                case "registeredAt":
                    return MylistSortKey.RegisteredAt;
                case "viewCount":
                    return MylistSortKey.ViewCount;
                case "lastCommentTime":
                    return MylistSortKey.LastCommentTime;
                case "commentCount":
                    return MylistSortKey.CommentCount;
                case "mylistCount":
                    return MylistSortKey.MylistCount;
                case "duration":
                    return MylistSortKey.Duration;
            }
            throw new Exception("Cannot unmarshal type DefaultSortKey");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (MylistSortKey)untypedValue;
            switch (value)
            {
                case MylistSortKey.Title:
                    serializer.Serialize(writer, "title");
                    return;
                case MylistSortKey.AddedAt:
                    serializer.Serialize(writer, "addedAt");
                    return;
                case MylistSortKey.MylistComment:
                    serializer.Serialize(writer, "mylistComment");
                    return;
                case MylistSortKey.RegisteredAt:
                    serializer.Serialize(writer, "registeredAt");
                    return;
                case MylistSortKey.ViewCount:
                    serializer.Serialize(writer, "viewCount");
                    return;
                case MylistSortKey.LastCommentTime:
                    serializer.Serialize(writer, "lastCommentTime");
                    return;
                case MylistSortKey.CommentCount:
                    serializer.Serialize(writer, "commentCount");
                    return;
                case MylistSortKey.MylistCount:
                    serializer.Serialize(writer, "mylistCount");
                    return;
                case MylistSortKey.Duration:
                    serializer.Serialize(writer, "duration");
                    return;
            }
            throw new Exception("Cannot marshal type DefaultSortKey");
        }

        public static readonly DefaultSortKeyConverter Singleton = new DefaultSortKeyConverter();
    }

    internal class DefaultSortOrderConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(MylistSortOrder) || t == typeof(MylistSortOrder?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "asc":
                    return MylistSortOrder.Asc;
                case "desc":
                    return MylistSortOrder.Desc;
            }
            throw new Exception("Cannot unmarshal type DefaultSortOrder");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (MylistSortOrder)untypedValue;
            switch (value)
            {
                case MylistSortOrder.Asc:
                    serializer.Serialize(writer, "asc");
                    return;
                case MylistSortOrder.Desc:
                    serializer.Serialize(writer, "desc");
                    return;
            }
            throw new Exception("Cannot marshal type DefaultSortOrder");
        }

        public static readonly DefaultSortOrderConverter Singleton = new DefaultSortOrderConverter();
    }

    internal class OwnerTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(OwnerType) || t == typeof(OwnerType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "channel":
                    return OwnerType.Channel;
                case "hidden":
                    return OwnerType.Hidden;
                case "user":
                    return OwnerType.User;
            }
            throw new Exception("Cannot unmarshal type OwnerType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (OwnerType)untypedValue;
            switch (value)
            {
                case OwnerType.Channel:
                    serializer.Serialize(writer, "channel");
                    return;
                case OwnerType.Hidden:
                    serializer.Serialize(writer, "hidden");
                    return;
                case OwnerType.User:
                    serializer.Serialize(writer, "user");
                    return;
            }
            throw new Exception("Cannot marshal type OwnerType");
        }

        public static readonly OwnerTypeConverter Singleton = new OwnerTypeConverter();
    }

    internal class StatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ContentStatus) || t == typeof(ContentStatus?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "deleted":
                    return ContentStatus.Deleted;
                case "hidden":
                    return ContentStatus.Hidden;
                case "public":
                    return ContentStatus.Public;
                case "private":
                    return ContentStatus.Private;
            }
            throw new Exception("Cannot unmarshal type Status");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ContentStatus)untypedValue;
            switch (value)
            {
                case ContentStatus.Deleted:
                    serializer.Serialize(writer, "deleted");
                    return;
                case ContentStatus.Hidden:
                    serializer.Serialize(writer, "hidden");
                    return;
                case ContentStatus.Public:
                    serializer.Serialize(writer, "public");
                    return;
                case ContentStatus.Private:
                    serializer.Serialize(writer, "private");
                    return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
    }

    internal class TypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(TypeEnum) || t == typeof(TypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "essential")
            {
                return TypeEnum.Essential;
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
            if (value == TypeEnum.Essential)
            {
                serializer.Serialize(writer, "essential");
                return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }
}
