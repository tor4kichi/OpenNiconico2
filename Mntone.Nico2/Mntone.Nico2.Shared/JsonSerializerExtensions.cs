using Mntone.Nico2.JsonHelpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Mntone.Nico2
{
	internal static class JsonSerializerExtensions
	{
		public static T Load<T>(string data, JsonSerializerSettings settings = null)
		{
			if (settings == null)
            {
				settings = new JsonSerializerSettings();
			}

			//settings.Converters.Add(new HtmlEncodingConverter());
			return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data, settings);
		}
	}

	public class SingleOrArrayConverter<T> : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return (objectType == typeof(List<T>));
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JToken token = JToken.Load(reader);
			if (token.Type == JTokenType.Array)
			{
				return token.ToObject<List<T>>();
			}
			return new List<T> { token.ToObject<T>() };
		}

		public override bool CanWrite
		{
			get { return false; }
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}
