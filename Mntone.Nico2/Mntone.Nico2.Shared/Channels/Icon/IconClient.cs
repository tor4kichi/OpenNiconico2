using HtmlAgilityPack;
using System;
using System.Threading.Tasks;

#if WINDOWS_UWP
using Windows.Storage.Streams;
#else
using System.Net.Http;
#endif

namespace Mntone.Nico2.Channels.Icon
{
	internal sealed class IconClient
	{
#if WINDOWS_UWP
		public static async Task<IBuffer> GetIconAsync( NiconicoContext context, string requestId )
#else
		public static async Task<byte[]> GetIconAsync( NiconicoContext context, string requestId )
#endif
		{
			if( !NiconicoRegex.IsChannelId( requestId ) )
			{
				throw new ArgumentException();
			}

			var channelNumber = requestId.Substring( 2 ).ToUInt();
			return await context.HttpClient
#if WINDOWS_UWP
				.GetBufferAsync( new Uri(string.Format( NiconicoUrls.ChannelIconUrl, channelNumber )) );
#else
				.GetByteArrayAsync( string.Format( NiconicoUrls.ChannelIconUrl, channelNumber ) );
#endif
		}
	}
}