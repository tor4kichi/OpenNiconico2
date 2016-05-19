using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mntone.Nico2.Mylist.UserMylist
{
    public class UserMylistClient
    {
		public static async Task<string> GetUserMylistDataAsync(NiconicoContext context, string user_id)
		{
			return await context.GetClient()
				.GetStringAsync(NiconicoUrls.MakeUserMylistGroupListRssUrl(user_id));
		}



		public static List<MylistGroup.MylistGroupData> ParseRss(string rss)
		{
			var serializer = new XmlSerializer(typeof(UserMylistRss));

			UserMylistRss parsedRss = null;
			using (var stream = new StringReader(rss))
			{
				parsedRss = (UserMylistRss)serializer.Deserialize(stream);
			}

			return parsedRss.Channel.Item.Select(x =>
			{
				return new MylistGroup.MylistGroupData()
				{
					Id = x.Link.Split('/').Last(),
					IsPublic = true,
					Description = x.Description,
					Name = x.Title,
					ThumnailUrl = new Uri(x.Thumbnail.Url),
				};
			})
			.ToList();
		}

		public static Task<List<MylistGroup.MylistGroupData>> GetUserMylistAsync(NiconicoContext context, string user_id)
		{
			return GetUserMylistDataAsync(context, user_id)
				.ContinueWith(prevTask => ParseRss(prevTask.Result));
		}
	}
}
