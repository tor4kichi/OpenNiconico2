using HtmlAgilityPack;
using System;
using System.Threading.Tasks;

#if WINDOWS_UWP
using Windows.Storage.Streams;
#else
using System.Net.Http;
#endif

namespace Mntone.Nico2.Users.Icon
{
	internal sealed class IconClient
	{
#if WINDOWS_UWP
        public static async Task<IBuffer> GetIconAsync(NiconicoContext context, uint userId)
        {
            try
            {
                return await context.GetClient().GetBufferAsync(new Uri(string.Format(NiconicoUrls.UserIconUrl, userId / 10000, userId)));
            }
            catch (AggregateException ex)
            {
                if (ex.HResult != -2146233088)
                {
                    throw;
                }
            }

            return await context.GetClient().GetBufferAsync(new Uri(NiconicoUrls.UserBlankIconUrl));
        }
#else
		public static async Task<byte[]> GetIconAsync( NiconicoContext context, uint userId )
		{
            try
            {
                return await context.GetClient().GetByteArrayAsync(string.Format(NiconicoUrls.UserIconUrl, userId / 10000, userId));
            }
            catch (AggregateException ex)
            {
                if (ex.HResult != -2146233088)
                {
                    throw;
                }
            }
                
			return await context.GetClient().GetByteArrayAsync( NiconicoUrls.UserBlankIconUrl );
        }
#endif
    }
}