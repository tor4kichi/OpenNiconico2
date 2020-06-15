using HtmlAgilityPack;
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
			return await context
				.GetConvertedStringAsync(NiconicoUrls.MakeUserPageUrl(user_id) + "/mylist");
		}



		private static List<MylistGroupData> ParseMylistPageHtml(string rawHtml)
		{
			List<MylistGroupData> list = new List<MylistGroupData>();

			var html = new HtmlDocument();
			html.LoadHtml(rawHtml);

			var body = html.DocumentNode
				.GetElementByTagName("html")
				.GetElementByTagName("body");


			var articleBody = body
                .GetElementByClassName("BaseLayout")
				.GetElementByClassName("wrapper")
				.GetElementById("mylist")
				.GetElementByClassName("articleBody");

			return articleBody.GetElementsByClassName("outer")
				.Select(x => 
				{
					var data = new MylistGroupData();

					var section = x
						.GetElementByClassName("section");
					var h4 = section
						.GetElementByTagName("h4");
					var anchor = h4
						.GetElementByTagName("a");

					data.Id = new String(anchor.GetAttributeValue("href", "").Skip("mylist/".Count()).ToArray());
					data.Name = anchor.InnerText;
					data.Count = int.Parse(new String(
						h4
						.GetElementByTagName("span")
						.InnerText.Skip(2).TakeWhile(c => c >= '0' && c <= '9')
						.ToArray()
						)
						);

					data.IconId = anchor
						.GetElementByClassName("folderIcon")
						.GetAttributeValue("class", "")
						.Last()
						.ToString();
						


					var fullDescription = section.GetElementsByClassName("mylistDescription")?
						.SingleOrDefault(y => y.GetAttributeValue("data-nico-mylist-desc-full", "") == "true");

					data.Description = fullDescription != null ? fullDescription.InnerText : "";



					data.ThumbnailUrls  = x.GetElementByClassName("thumbContainer")
						.GetElementByTagName("ul")
						.GetElementsByTagName("li")
						.Select(thumb => 
						{
							return new Uri(
								thumb.GetElementByTagName("img")
									.GetAttributeValue("src", "")
									);
						})
						.ToList();


					return data;
				})
				.ToList();
		}

		public static List<MylistGroupData> ParseRss(string rss)
		{
			var serializer = new XmlSerializer(typeof(UserMylistRss));

			UserMylistRss parsedRss = null;
			using (var stream = new StringReader(rss))
			{
				parsedRss = (UserMylistRss)serializer.Deserialize(stream);
			}

			return parsedRss.Channel.Item.Select(x =>
			{
				return new MylistGroupData()
				{
					Id = x.Link.Split('/').Last(),
					IsPublic = "1",
					Description = x.Description,
					Name = x.Title,
					ThumbnailUrls = new List<Uri>() { new Uri(x.Thumbnail.Url) },
				};
			})
			.ToList();
		}

		public static Task<List<MylistGroupData>> GetUserMylistAsync(NiconicoContext context, string user_id)
		{
			return GetUserMylistDataAsync(context, user_id)
				.ContinueWith(prevTask => ParseMylistPageHtml(prevTask.Result));
		}
	}
}
