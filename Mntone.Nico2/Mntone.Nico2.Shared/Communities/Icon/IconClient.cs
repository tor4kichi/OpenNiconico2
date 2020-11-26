using HtmlAgilityPack;
using System;
using System.Threading.Tasks;

#if WINDOWS_UWP
using Windows.Storage.Streams;
#else
using System.Net.Http;
#endif

namespace Mntone.Nico2.Communities.Icon
{
	internal sealed class IconClient
	{
#if WINDOWS_UWP
		public static async Task<IBuffer> GetIconAsync( NiconicoContext context, string requestId )
		{
			if( !NiconicoRegex.IsCommunityId( requestId ) )
			{
				throw new ArgumentException();
			}

            var communityNumber = requestId.Substring(2).ToUInt();
            try
            {
                return await context.HttpClient
                    .GetBufferAsync(new Uri(string.Format(NiconicoUrls.CommunityIconUrl, communityNumber / 10000, communityNumber)));
            }
            catch (AggregateException ex)
            {
                if (ex.HResult != -2146233088)
                {
                    throw;
                }
            }

            return await context.HttpClient.GetBufferAsync(new Uri(NiconicoUrls.CommunityBlankIconUrl));
		}
#else
		public static async Task<byte[]> GetIconAsync( NiconicoContext context, string requestId )
        {
            if (!NiconicoRegex.IsCommunityId(requestId))
            {
                throw new ArgumentException();
            }

            var communityNumber = requestId.Substring(2).ToUInt();
            try
            {
                return await context.HttpClient
				    .GetByteArrayAsync( string.Format( NiconicoUrls.CommunityIconUrl, communityNumber / 10000, communityNumber ) );
            }
            catch (AggregateException ex)
            {
                if (ex.HResult != -2146233088)
                {
                    throw;
                }
            }

            return await context.HttpClient.GetByteArrayAsync( NiconicoUrls.CommunityBlankIconUrl );
        }
#endif
    }
}