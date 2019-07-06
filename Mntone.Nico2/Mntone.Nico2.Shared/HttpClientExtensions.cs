using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

#if WINDOWS_UWP
using Windows.Web.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;
#else 
using System.Net.Http;
#endif

namespace Mntone.Nico2
{
	internal static class HttpClientExtensions
	{
		public static Task<HttpResponseMessage> GetAsync(this HttpClient client, string uri)
		{
			return client.GetAsync(new Uri(uri))
#if WINDOWS_UWP
                .AsTask()
#endif
                ;
		}

		public static Task<string> GetStringAsync(this HttpClient client, string uri)
		{
			return client.GetStringAsync(new Uri(uri))
#if WINDOWS_UWP
                .AsTask()
#endif
                ;
        }
		


		public static Task<string> GetConvertedStringAsync(this HttpClient client, string uri)
		{
			return client.GetConvertedStringAsync(uri, Encoding.UTF8);
		}

		public static async Task<string> GetConvertedStringAsync(this HttpClient client, string uri, Encoding encoding)
		{
#if WINDOWS_UWP
            var byteArray = await client.GetByteArrayAsync(uri);
            return Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
#else
            var stream = await client.GetStreamAsync(uri);
            using (var sr = new StreamReader(stream, encoding))
            {
                return await sr.ReadToEndAsync();
            }
#endif
        }

		public static Task<byte[]> GetByteArrayAsync(this HttpClient client, string uri)
		{
#if WINDOWS_UWP
            return client
				.GetBufferAsync(new Uri(uri))
				.AsTask()
				.ContinueWith(stream =>
				{
					return stream.Result.ToArray();
				});
#else
            return client
                .GetByteArrayAsync(new Uri(uri));
#endif
        }

    }
}