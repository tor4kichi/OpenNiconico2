using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.NicoRepo
{
    using System.Globalization;
    using System.Linq;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    

    public enum NicoRepoDisplayTarget
    {
        All,
        Self,
        User,
        Channel,
        Community,
        Mylist,
    }

    public partial class NicoRepoEntriesResponse
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public NicoRepoEntry[] Data { get; set; }

        [JsonProperty("errors")]
        public Error[] Errors { get; set; }
    }

    public partial class Error
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("errorReasonCodes")]
        public string[] ErrorReasonCodes { get; set; }
    }


    public partial class NicoRepoEntry
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("updated")]
        public DateTimeOffset Updated { get; set; }

        [JsonProperty("watchContext")]
        public WatchContext WatchContext { get; set; }

        [JsonProperty("muteContext")]
        public MuteContext MuteContext { get; set; }

        [JsonProperty("actor")]
        public Actor Actor { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("object")]
        public Object Object { get; set; }

        public string GetContentId()
        {
            return Object.Url.Segments.Last();
        }
    }

    public partial class Actor
    {
        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("icon")]
        public Uri Icon { get; set; }
    }

    public class MuteContext
    {
        [JsonProperty("task")]
        public Task Task { get; set; }

        [JsonProperty("sender")]
        public Sender Sender { get; set; }

        [JsonProperty("trigger")]
        public string Trigger { get; set; }
    }

    public class Sender
    {
        [JsonProperty("idType")]
        public SenderIdTypeEnum IdType { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public SenderTypeEnum Type { get; set; }
    }

    public partial class Object
    {
        [JsonProperty("type")]
        public NicoRepoType Type { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("image")]
        public Uri Image { get; set; }
    }

    public partial class WatchContext
    {
        [JsonProperty("parameter")]
        public Parameter Parameter { get; set; }
    }

    public partial class Parameter
    {
        [JsonProperty("nicorepo")]
        public string Nicorepo { get; set; }
    }

    public partial class Meta
    {
        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("hasNext")]
        public bool HasNext { get; set; }

        [JsonProperty("maxId")]
        public string MaxId { get; set; }

        [JsonProperty("minId")]
        public string MinId { get; set; }

        [JsonProperty("errors")]
        public object[] Errors { get; set; }
    }

    public enum SenderIdTypeEnum { User, Channel };

    public enum Task { Nicorepo };

    public enum SenderTypeEnum { User, Channel };

    public enum NicoRepoType
    {
        All,

        /// <summary>
        /// 動画投稿
        /// </summary>
        Video,

        /// <summary>
        /// 生放送開始
        /// </summary>
        Program,

        /// <summary>
        /// イラスト投稿
        /// </summary>
        Image,

        /// <summary>
        /// マンガ投稿
        /// </summary>
        ComicStory,

        /// <summary>
        /// ブロマガの記事投稿
        /// </summary>
        Article,

        Game,
    }

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

    internal class IdTypeEnumConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(SenderIdTypeEnum) || t == typeof(SenderIdTypeEnum?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "user")
            {
                return SenderIdTypeEnum.User;
            }
            throw new Exception("Cannot unmarshal type IdTypeEnum");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (SenderIdTypeEnum)untypedValue;
            if (value == SenderIdTypeEnum.User)
            {
                serializer.Serialize(writer, "user");
                return;
            }
            throw new Exception("Cannot marshal type IdTypeEnum");
        }

        public static readonly IdTypeEnumConverter Singleton = new IdTypeEnumConverter();
    }

    internal class TaskConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Task) || t == typeof(Task?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "nicorepo")
            {
                return Task.Nicorepo;
            }
            throw new Exception("Cannot unmarshal type Task");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Task)untypedValue;
            if (value == Task.Nicorepo)
            {
                serializer.Serialize(writer, "nicorepo");
                return;
            }
            throw new Exception("Cannot marshal type Task");
        }

        public static readonly TaskConverter Singleton = new TaskConverter();
    }

    internal class ObjectTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(NicoRepoType) || t == typeof(NicoRepoType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {

            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            return value switch
            {
                "comicStory" => NicoRepoType.ComicStory,
                "program" => NicoRepoType.Program,
                "video" => NicoRepoType.Video,
                "image" => NicoRepoType.Image,
                "article" => NicoRepoType.Article,
                "game" => NicoRepoType.Game,
                _ => throw new Exception("Cannot unmarshal type ObjectType")
            };            
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (NicoRepoType)untypedValue;
            switch (value)
            {
                case NicoRepoType.ComicStory:
                    serializer.Serialize(writer, "comicStory");
                    return;
                case NicoRepoType.Program:
                    serializer.Serialize(writer, "program");
                    return;
                case NicoRepoType.Video:
                    serializer.Serialize(writer, "video");
                    return;
                case NicoRepoType.Image:
                    serializer.Serialize(writer, "image");
                    return;
                case NicoRepoType.Article:
                    serializer.Serialize(writer, "article");
                    return;
                case NicoRepoType.Game:
                    serializer.Serialize(writer, "game");
                    return;
            }
            throw new Exception("Cannot marshal type ObjectType");
        }

        public static readonly ObjectTypeConverter Singleton = new ObjectTypeConverter();
    }
}
