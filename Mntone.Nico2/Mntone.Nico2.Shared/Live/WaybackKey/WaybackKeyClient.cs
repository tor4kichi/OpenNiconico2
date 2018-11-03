using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Live.WaybackKey
{
    internal static class WaybackKeyClient
    {
        public static async Task<string> GetWaybackKeyAsync(NiconicoContext context, string threadId)
        {
            var res = await context.GetStringAsync($"http://watch.live.nicovideo.jp/api/getwaybackkey?thread={threadId}");

            return res.Remove(0, "waybackkey=".Length);
        }
    }
}
