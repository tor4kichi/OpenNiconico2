using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mntone.Nico2.Users.Video
{
    internal sealed class UserVideoClient
    {
		// ユーザーの投稿動画関連

		public static Task<string> GetUserDataAsync(NiconicoContext context, uint user_id, uint page, Sort sortMethod, Order sortDir)
		{
			var url = NiconicoUrls.MakeUserVideoRssUrl(user_id.ToString(), page, sortMethod.ToShortString(), sortDir.ToShortString());
			return context.GetClient()
				.GetStringAsync(url);
		}



		public static UserVideoResponse ParseUserData(string xml)
		{
			var serializer = new XmlSerializer(typeof(UserVideoRss));

			UserVideoRss rss;
			using (var stream = new StringReader(xml))
			{
				rss = (UserVideoRss)serializer.Deserialize(stream);
			}

			return new UserVideoResponse()
			{
				UserId = uint.Parse(rss.Channel.Link.Split('/')[2]),
				UserName = rss.Channel.Creator,
				Items = rss.Channel.Item.Select(x =>
				{
					return new VideoData()
					{
						VideoId = x.Link2.Split('/').Last(),
						Title = x.Title,
						ThumbnailUrl = new Uri(x.Thumbnail.Url),
						Description = x.Description
					};
				})
				.ToList()
			};


		}

		public static Task<UserVideoResponse> GetUserAsync(NiconicoContext context, uint user_id, uint page, Sort sortMethod, Order sortDir)
		{
			return GetUserDataAsync(context, user_id, page, sortMethod, sortDir)
				.ContinueWith(prevTask => ParseUserData(prevTask.Result));
		}
	}
}
