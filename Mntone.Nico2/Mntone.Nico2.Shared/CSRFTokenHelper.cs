using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2
{
    public static class CSRFTokenHelper
    {
		public static async Task<MylistAdditionInfo> GetMylistToken(this NiconicoContext context, string group_id, string videoId)
		{
			var api = NiconicoUrls.MakeMylistAddVideoTokenApiUrl(videoId);

            var info = new MylistAdditionInfo() { GroupId = group_id };

            var documentText = await context.GetStringAsync(api);


            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(documentText);

            {
                const string nicoAPIString = "NicoAPI.token = '";
                var nicoAPIStartPosition = documentText.IndexOf(nicoAPIString);
                var token = new string(documentText
                    .Skip(nicoAPIStartPosition + nicoAPIString.Length)
                    .TakeWhile(x => '\'' != x)
                    .ToArray()
                    );
                info.Token = token;
            }

            {
                var inputNodes = htmlDocument.DocumentNode.Descendants("input");
                foreach (var inputNode in inputNodes)
                {
                    if (!inputNode.Attributes.Contains("type")) { continue; }
                    if (inputNode.Attributes["type"].Value != "hidden") { continue; }

                    var kind = inputNode.Attributes["name"].Value;
                    var value = inputNode.Attributes["value"].Value;

                    info.Values.Add(kind, value);
                }
            }

            return info;
        }

		public static async Task<string> GetToken(this NiconicoContext context)
		{
			var api = NiconicoUrls.MylistMyPageUrl;

			return await context.GetStringAsync(api)
				.ContinueWith(x => x.Result.Substring(x.Result.IndexOf("NicoAPI.token = \"") + 17, 60));

		}
	}

    public class MylistAdditionInfo
    {
        public string GroupId { get; set; }
        public Dictionary<string, string> Values { get; } = new Dictionary<string, string>();

        public string ItemType => Values["item_type"];
        public string ItemId => Values["item_id"];
        public string ItemAmc => Values["item_amc"];
        public string Token { get; set; }
    }
}
