using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http.Headers;

namespace Mntone.Nico2.Videos.WatchAPI
{
	internal sealed class WatchAPIClient
	{
       


        #region WatchApi

        public static async Task<string> GetWatchApiDataAsync(NiconicoContext context, string requestId, bool forceQuality, HarmfulContentReactionType harmfulReactType)
        {
            if (!NiconicoRegex.IsVideoId(requestId))
            {
                //				throw new ArgumentException();
            }

            var dict = new Dictionary<string, string>();
            var url = $"{NiconicoUrls.VideoWatchPageUrl}{requestId}";

            if (forceQuality)
            {
                dict.Add("eco", "1");
            }
            if (harmfulReactType != HarmfulContentReactionType.None)
            {
                dict.Add("watch_harmful", ((uint)harmfulReactType).ToString());
            }

            url += "?" + HttpQueryExtention.DictionaryToQuery(dict);

            try
            {
                var client = context.GetClient();

                var watchHtml5Player = new HttpCookiePairHeaderValue("watch_html5", "1");
                if (client.DefaultRequestHeaders.Cookie.Contains(watchHtml5Player))
                {
                    client.DefaultRequestHeaders.Cookie.Remove(watchHtml5Player);
                }
                client.DefaultRequestHeaders.Cookie.Add(watchHtml5Player);

                var watchFlashPlayer = new HttpCookiePairHeaderValue("watch_flash", "1");
                var old = client.DefaultRequestHeaders.Cookie.SingleOrDefault(x => x.Name == "watch_flash");
                if (old != null)
                {
                    client.DefaultRequestHeaders.Cookie.Remove(old);
                }
                client.DefaultRequestHeaders.Cookie.Add(watchFlashPlayer);

                var res = await context.GetClient()
                    .GetAsync(url);

                if (res.StatusCode == Windows.Web.Http.HttpStatusCode.Forbidden)
                {
                    throw new WebException("require payment.");
                }

                var text = await res.Content.ReadAsStringAsync().AsTask();

                var htmlDocument = new HtmlAgilityPack.HtmlDocument();
                htmlDocument.LoadHtml(text);

                // 推定有害動画の視聴ブロックページかをチェック
                if (htmlDocument.GetElementbyId("PAGECONTAINER") != null)
                {
                    throw new ContentZoningException("access once blocked, maybe harmful video.");
                }
                else
                {
                    var videoInfoNode = htmlDocument.GetElementbyId("watchAPIDataContainer");
                    var rawStr = videoInfoNode.InnerText;
                    var htmlDecoded = WebUtility.HtmlDecode(rawStr);
                    //var str = WebUtility.UrlDecode(htmlDecoded);

                    System.Diagnostics.Debug.WriteLine(htmlDecoded);

                    return htmlDecoded;

                }
            }
            catch (ContentZoningException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new WebException("access failed watch/" + requestId, e);
            }
        }


        public static WatchApiResponse ParseWatchApi(string flvData)
        {
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.NullValueHandling = NullValueHandling.Include;
            jsonSerializer.Error += JsonSerializer_Error;
            jsonSerializer.DefaultValueHandling = DefaultValueHandling.Include;

            var watchApi = jsonSerializer.Deserialize<WatchApiResponse>(new JsonTextReader(new StringReader(flvData)));

            return watchApi;
        }


        public static Task<WatchApiResponse> GetWatchApiAsync(NiconicoContext context, string requestId, bool forceLowQuality, HarmfulContentReactionType harmfulReactType)
        {
            return GetWatchApiDataAsync(context, requestId, forceLowQuality, harmfulReactType)
                .ContinueWith(prevTask => ParseWatchApi(prevTask.Result));
        }

        #endregion


        private static void JsonSerializer_Error(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.ErrorContext.Path);

        }

    }
}
