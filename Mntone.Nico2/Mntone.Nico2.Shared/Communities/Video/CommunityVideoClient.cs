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
		public static Task<string> GetCommunityVideoRssAsync(NiconicoContext context, string communityId, uint page)
		{
			var dict = new Dictionary<string, string>();
			dict.Add("rss", "2.0");
			dict.Add(nameof(page), page.ToString());

			return context.GetStringAsync($"{NiconicoUrls.CommynityVideoPageUrl}/{communityId}", dict);
		}


		private static NiconicoVideoRss ParseRssString(string rssText)
		{
			using (var contentStream = new StringReader(rssText))
			{
				var serializer = new XmlSerializer(typeof(NiconicoVideoRss));

				return (NiconicoVideoRss)serializer.Deserialize(contentStream);
			}
		}


		public static Task<NiconicoVideoRss> GetCommunityVideosAsync(NiconicoContext context, string communityId, uint page)
		{
			return GetCommunityVideoRssAsync(context, communityId, page)
				.ContinueWith(prevTask => ParseRssString(prevTask.Result));
		}
	}
}
