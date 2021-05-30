using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Videos.Comment
{

    public class NMSG_ResponseJsonConverter : JsonConverter
    {
        public override bool CanWrite => false;
        public override bool CanRead => true;

        private Type[] CanConvertTypes = new[] 
        {
            typeof(NGMS_Thread_ResponseItem),
            typeof(NGMS_Leaf),
            typeof(NGMS_GlobalNumRes),
            typeof(NMSG_Chat),
            typeof(PingItem),
        };

        public override bool CanConvert(Type objectType)
        {
            return CanConvertTypes.Any(x => x == objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            if (jObject["ping"] != null)
            {
                var ping = new PingItem();
                serializer.Populate(reader, ping);
                return ping;
            }
            else if (jObject["thread"] != null)
            {
                var threadItem = new NGMS_Thread_ResponseItem();
                serializer.Populate(reader, threadItem);
                return threadItem;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }


    public class NMSG_Response
    {
        public List<NGMS_Thread_Response> Threads { get; } = new List<NGMS_Thread_Response>();

        internal List<JToken> _CommentsSource { get; } = new List<JToken>();

        //public ThreadType ThreadType { get; internal set; }

        public List<NMSG_Chat> ParseComments()
        {
            return _CommentsSource.Select(x => x.ToObject<NMSG_Chat>()).ToList();
        }


        public NGMS_GlobalNumRes GlobalNumRes { get; set; }
    }

    [DataContract]
    public class NGMS_Thread_ResponseItem
    {
        [DataMember(Name = "thread")]
        NGMS_Thread_Response Thread { get; set; }
    }

    [DataContract]
    public class NGMS_Thread_Response
    {
        [DataMember(Name = "resultcode")]
        public int Resultcode { get; set; }

        [DataMember(Name = "thread")]
        public string Thread { get; set; }

        [DataMember(Name = "server_time")]
        public int ServerTime { get; set; }

        [DataMember(Name = "last_res")]
        public int LastRes { get; set; }

        [DataMember(Name = "ticket")]
        public string Ticket { get; set; }

        [DataMember(Name = "revision")]
        public int Revision { get; set; }
    }

    [DataContract]
    public class NGMS_Leaf
    {

        [DataMember(Name = "thread")]
        public string Thread { get; set; }

        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "leaf")]
        public int? Leaf { get; set; }
    }

    [DataContract]
    public class NGMS_GlobalNumRes
    {

        [DataMember(Name = "thread")]
        public string Thread { get; set; }

        [DataMember(Name = "num_res")]
        public int NumRes { get; set; }
    }

    [DataContract]
    public class NMSG_Chat
    {

        [DataMember(Name = "thread")]
        public string Thread { get; set; }

        [DataMember(Name = "no")]
        public int No { get; set; }

        [DataMember(Name = "vpos")]
        public int Vpos { get; set; }

        [DataMember(Name = "leaf")]
        public int? Leaf { get; set; }

        [DataMember(Name = "date")]
        public int Date { get; set; }

        [DataMember(Name = "date_usec")]
        public int DateUsec { get; set; }

        [DataMember(Name = "premium")]
        public int? Premium { get; set; }

        [DataMember(Name = "anonymity")]
        public int? Anonymity { get; set; }

        [DataMember(Name = "user_id")]
        public string UserId { get; set; }

        [DataMember(Name = "mail")]
        public string Mail { get; set; }

        [DataMember(Name = "content")]
        public string Content { get; set; }

        [DataMember(Name = "score")]
        public int? Score { get; set; }

        [DataMember(Name = "deleted")]
        public int? Deleted { get; set; }

        [DataMember(Name = "yourpost")]
        public int? Yourpost { get; set; }

        public IEnumerable<CommandType> ParseCommandTypes()
        {
            return CommandTypesHelper.ParseCommentCommandTypes(this.Mail);
        }
    }
}
