using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Channels.Info
{
    internal static class ChannelInfoClient
    {
        public static Task<string> GetChannelInfoJsonAsync(NiconicoContext context, string channelId)
        {
            string channelIdNumberOnly = channelId;
            
            if (channelId.StartsWith("ch") && channelId.Skip(2).All(c => c >= '0' && c <= '9'))
            {
                channelIdNumberOnly = channelId.Remove(0, 2);
            }

            if (channelIdNumberOnly.All(c => c >= '0' && c <= '9'))
            {
                return context.GetStringAsync($"{NiconicoUrls.ChannelInfoApiUrl}{channelIdNumberOnly}");
            }
            else
            {
                throw new NotSupportedException();
            }
        }


        public static Task<ChannelInfo> GetChannelInfoAsync(NiconicoContext context, string channelId)
        {
            return GetChannelInfoJsonAsync(context, channelId)
                .ContinueWith(prevTask => JsonSerializerExtensions.Load<ChannelInfo>(prevTask.Result));
        }
    }
}
