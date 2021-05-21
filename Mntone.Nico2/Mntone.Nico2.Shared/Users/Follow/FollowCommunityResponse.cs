namespace Mntone.Nico2.Users.Follow
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class FollowCommunityResponse
    {
        [JsonProperty("meta")]
        public FollowCommunityMeta Meta { get; set; }

        [JsonProperty("data")]
        public List<FollowCommunity> Data { get; set; }


        public partial class FollowCommunity
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("globalId")]
            public string GlobalId { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("status")]
            public CommunityStatus Status { get; set; }

            [JsonProperty("ownerId")]
            public long OwnerId { get; set; }

            [JsonProperty("createTime")]
            public DateTimeOffset CreateTime { get; set; }

            [JsonProperty("thumbnailUrl")]
            public ThumbnailUrl ThumbnailUrl { get; set; }

            [JsonProperty("tags")]
            public List<Tag> Tags { get; set; }

            [JsonProperty("userCount")]
            public long UserCount { get; set; }

            [JsonProperty("level")]
            public long Level { get; set; }

            [JsonProperty("threadMax")]
            public long ThreadMax { get; set; }

            [JsonProperty("threadCount")]
            public long ThreadCount { get; set; }

            [JsonProperty("optionFlags")]
            public OptionFlags OptionFlags { get; set; }
        }

        public partial class OptionFlags
        {
            [JsonProperty("communityAutoAcceptEntry")]
            public bool CommunityAutoAcceptEntry { get; set; }

            [JsonProperty("communityBlomaga")]
            public bool CommunityBlomaga { get; set; }

            [JsonProperty("communityHideLiveArchives")]
            public bool CommunityHideLiveArchives { get; set; }

            [JsonProperty("communityInvalidBbs")]
            public bool CommunityInvalidBbs { get; set; }

            [JsonProperty("communityPrivLiveBroadcastNew")]
            public bool CommunityPrivLiveBroadcastNew { get; set; }

            [JsonProperty("communityPrivUserAuth")]
            public bool CommunityPrivUserAuth { get; set; }

            [JsonProperty("communityPrivVideoPost")]
            public bool CommunityPrivVideoPost { get; set; }

            [JsonProperty("communityShownNewsNum")]
            public long CommunityShownNewsNum { get; set; }

            [JsonProperty("communityUserInfoRequired")]
            public bool CommunityUserInfoRequired { get; set; }

            [JsonProperty("communityIconInspectionMobile", NullValueHandling = NullValueHandling.Ignore)]
            public string CommunityIconInspectionMobile { get; set; }
        }

        public partial class Tag
        {
            [JsonProperty("text")]
            public string Text { get; set; }
        }

        public partial class ThumbnailUrl
        {
            [JsonProperty("normal")]
            public Uri Normal { get; set; }

            [JsonProperty("small")]
            public Uri Small { get; set; }
        }

        public partial class FollowCommunityMeta
        {
            [JsonProperty("status")]
            public long Status { get; set; }

            [JsonProperty("total")]
            public long Total { get; set; }

            [JsonProperty("count")]
            public long Count { get; set; }
        }

    }

    public enum CommunityStatus { Closed, Open };

    internal static class FollowCommunityResponseConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                StatusConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class StatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(CommunityStatus) || t == typeof(CommunityStatus?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "CLOSED":
                    return CommunityStatus.Closed;
                case "OPEN":
                    return CommunityStatus.Open;
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
            var value = (CommunityStatus)untypedValue;
            switch (value)
            {
                case CommunityStatus.Closed:
                    serializer.Serialize(writer, "CLOSED");
                    return;
                case CommunityStatus.Open:
                    serializer.Serialize(writer, "OPEN");
                    return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
    }
}
