using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Mntone.Nico2;
using System.Linq;
using System.Diagnostics;

namespace Mntone.Nico2.Searches.Community
{
    public sealed class CommunityClient
    {
		public static Task<string> GetCommunitySearchPageHtmlAsync(
			NiconicoContext context
			, string str
			, uint page
			, CommunitySearchSort sort
			, Order order
			, CommunitySearchMode mode
			)
		{
			var dict = new Dictionary<string, string>();

			dict.Add("__format", "json");

			dict.Add(nameof(page), page.ToString());
			dict.Add(nameof(order), order.ToChar().ToString());
			dict.Add(nameof(sort), sort.ToShortString());
			dict.Add(nameof(mode), mode.ToShortString());

			return context.GetStringAsync(NiconicoUrls.CommynitySearchPageUrl + str, dict);
		}

		private static CommunitySearchResponse ParseCommunitySearchPageHtml(string html)
		{
			CommunitySearchResponse res = new CommunitySearchResponse();

			if (string.IsNullOrEmpty(html))
			{
				Debug.WriteLine("community search failed, invalid page response.");
				return res;
			}


			var doc = new HtmlAgilityPack.HtmlDocument();
			try
			{
				doc.LoadHtml(html);
			} 
			catch
			{
				Debug.WriteLine("community search failed, html parse error.");
				return res;
			}

			var rootNode = doc.DocumentNode.Element("html");
			var body = rootNode.GetElementByTagName("body");
			var siteBody = body.GetElementById("site-body");
			var contents = siteBody.GetElementById("contents0727");
			var main = contents.GetElementById("main0727");

			// トータルカウント
			try
			{
				var pageNavi = main.GetElementsByClassName("pagenavi").FirstOrDefault();
				var pagelink = pageNavi.GetElementByClassName("pagelink");
				var totalCountElem = pagelink.Element("strong");
				var totalCountText = string.Join("", totalCountElem.InnerText.Split(','));
				var totalCount = uint.Parse(totalCountText);

				res.TotalCount = totalCount;
			}
			catch
			{
				Debug.WriteLine("community search failed, TotalCount parse error.");
				throw;
			}

			// コミュニティ情報
			try
			{
				var contentOuterContainer = main.Elements("div").ElementAt(1);
				var contentInnerContainer = contentOuterContainer.Element("div");

				foreach (var contentContainer in contentInnerContainer.Elements("div"))
				{
					var community = new Community.NicoCommynity();

					var table = contentContainer.Element("table");
					//var tbody = table.Element("tbody");
					var rootTr = table.Element("tr");

					var thumb = rootTr.Elements("td").ElementAt(0);
					var thumbAnchor = thumb.Element("a");

					// Community Id
					{
						var href = thumbAnchor.Attributes["href"].Value;
						var id = href.Split('/').LastOrDefault();

						if (!string.IsNullOrEmpty(id))
						{
							community.Id = id;
						}
						else
						{
							throw new Exception("Community Idの取得に失敗");
						}
					}


					// IconUrl
					{
						var iconImage = thumbAnchor.Element("img");
						var iconUrl = iconImage.Attributes["src"].Value;

						community.IconUrl = new Uri(iconUrl);
					}

					// レベル・メンバー数・動画数
					{
						var fs = thumb.Element("p");
						var fsElemets = fs.Elements("strong");
						var level = uint.Parse(fsElemets.ElementAt(0).InnerText);
						var memberCount = uint.Parse(fsElemets.ElementAt(1).InnerText.Trim(','));
						var videoCount = uint.Parse(fsElemets.ElementAt(2).InnerText.Trim(','));

						community.Level = level;
						community.MemberCount = memberCount;
						community.VideoCount = videoCount;
					}
					


					var details = rootTr.Elements("td").ElementAt(1);

					// DateTime
					{
						var dateElem = details.GetElementByClassName("date");
						var dateText = dateElem.InnerText;

						community.DateTime = dateText;
					}


					// Title 
					{
						var titleElem = details.GetElementByClassName("title");
						var titleAnchor = titleElem.Element("a");
						var title = titleAnchor.InnerText;

						community.Name = title;
					}

					// ShortDescription
					{
						var desc = details.GetElementByClassName("desc");
						var text = desc.InnerText;

						community.ShortDescription = text;
					}

					res.Communities.Add(community);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				return res;
			}

			res.IsStatusOK = true;
			return res;
		}


		public static Task<CommunitySearchResponse> CommunitySearchAsync(
			NiconicoContext context
			, string keyword
			, uint page
			, CommunitySearchSort sort
			, Order order
			, CommunitySearchMode mode
			)
		{
			return GetCommunitySearchPageHtmlAsync(context, keyword, page, sort, order, mode)
				.ContinueWith(prevTask => ParseCommunitySearchPageHtml(prevTask.Result));
		}
	}
}
