
namespace Mntone.Nico2.Users.Follow
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class FollowUsersResponse
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public FollowUserData Data { get; set; }
    }

    public partial class FollowUserData
    {
        [JsonProperty("items")]
        public List<UserFollowItem> Items { get; set; }

        [JsonProperty("summary")]
        public Summary Summary { get; set; }
    }

    public partial class UserFollowItem
    {
        [JsonProperty("type")]
        public FollowType Type { get; set; }

        [JsonProperty("relationships")]
        public Relationships Relationships { get; set; }

        [JsonProperty("isPremium")]
        public bool IsPremium { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("strippedDescription")]
        public string StrippedDescription { get; set; }

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

    public partial class Relationships
    {
        [JsonProperty("sessionUser")]
        public SessionUser SessionUser { get; set; }
    }

    public partial class SessionUser
    {
        [JsonProperty("isFollowing")]
        public bool IsFollowing { get; set; }
    }

    public partial class Summary
    {
        [JsonProperty("followees")]
        public long Followees { get; set; }

        [JsonProperty("followers")]
        public long Followers { get; set; }

        [JsonProperty("hasNext")]
        public bool HasNext { get; set; }

        [JsonProperty("cursor")]
        public string Cursor { get; set; }
    }

    public class Meta
    {
        [JsonProperty("status")]
        public long Status { get; set; }
    }

    public enum FollowType { Relationship };

    internal static class UserFollowResponseConverter
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
        public override bool CanConvert(Type t) => t == typeof(FollowType) || t == typeof(FollowType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "relationship")
            {
                return FollowType.Relationship;
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
            var value = (FollowType)untypedValue;
            if (value == FollowType.Relationship)
            {
                serializer.Serialize(writer, "relationship");
                return;
            }
            throw new Exception("Cannot marshal type TypeEnum");
        }

        public static readonly TypeEnumConverter Singleton = new TypeEnumConverter();
    }
}

