using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Mntone.Nico2.JsonHelpers
{
    public class HtmlEncodingConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(String);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return WebUtility.HtmlDecode(reader.Value?.ToString() ?? string.Empty);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(WebUtility.HtmlEncode((string)value));
        }
    }

}
