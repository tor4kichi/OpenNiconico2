using System;
using System.Collections.Generic;
using System.IO;
using Windows.Web.Http;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;

#if WINDOWS_APP
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;
#endif

namespace Mntone.Nico2
{
	internal static class HttpClientExtensions
	{
		public static Task<HttpResponseMessage> GetAsync(this HttpClient client, string uri)
		{
			return client.GetAsync(new Uri(uri))
				.AsTask();
		}

		public static Task<string> GetStringAsync(this HttpClient client, string uri)
		{
			return client.GetStringAsync(new Uri(uri))
				.AsTask();
		}
		


		public static Task<string> GetConvertedStringAsync(this HttpClient client, string uri)
		{
			return client.GetConvertedStringAsync(uri, Encoding.UTF8);
		}

		public static Task<string> GetConvertedStringAsync(this HttpClient client, string uri, Encoding encoding)
		{
			return client
				.GetBufferAsync(new Uri(uri))
				.AsTask()
				.ContinueWith(stream =>
				{
					var byteArray = stream.Result.ToArray();
					return Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
				});
		}

		public static Task<byte[]> GetByteArrayAsync(this HttpClient client, string uri)
		{
			return client
				.GetBufferAsync(new Uri(uri))
				.AsTask()
				.ContinueWith(stream =>
				{
					return stream.Result.ToArray();
				});
		}
		
	}
}