using Mntone.Nico2.Videos.Ranking;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mntone.Nico2.Communities.Video
{
    public sealed class CommunityVideoClient
    {
		public static async Task<RssVideoResponse> GetCommunityVideosAsync(NiconicoContext context, string communityId, uint page)
		{
            var dict = new Dictionary<string, string>();
            dict.Add("rss", "2.0");
            dict.Add(nameof(page), page.ToString());

            return await VideoRssContentHelper.GetRssVideoResponseAsync(
                $"{NiconicoUrls.CommynityVideoPageUrl}/{communityId}?{HttpQueryExtention.DictionaryToQuery(dict)}"
                );

        }
	}
}
