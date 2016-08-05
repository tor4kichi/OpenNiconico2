using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Mntone.Nico2
{
	internal static class JsonSerializerExtensions
	{
		public static T Load<T>( string data )
		{
			return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
		}
	}
}
