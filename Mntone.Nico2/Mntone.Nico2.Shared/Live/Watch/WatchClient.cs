﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http.Headers;

namespace Mntone.Nico2.Live.Watch
{
    public sealed class WatchClient
    {
        // Liveのwatchページから再生準備情報を取得します
        public static Task<string> GetLiveWatchPageHtmlAsync(NiconicoContext context, string liveId, bool forceHtml5 = true)
        {
            var client = context.GetClient();
            var forceLepPlayerCookieValue = new HttpCookiePairHeaderValue("player_version", "leo");
            if (client.DefaultRequestHeaders.Cookie.Contains(forceLepPlayerCookieValue))
            {
                client.DefaultRequestHeaders.Cookie.Remove(forceLepPlayerCookieValue);
            }
            client.DefaultRequestHeaders.Cookie.Add(forceLepPlayerCookieValue);

            return client.GetStringAsync(NiconicoUrls.Live2WatchPageUrl + liveId);
        }

        private static Crescendo.CrescendoLeoProps ParseCrescendoLeoPlayerProps(string html)
        {
            const string propStartString = "id=\"embedded-data\" data-props=\"";
            const string propEndString = "\"";

            var startPos = html.IndexOf(propStartString);
            if (startPos == -1)
            {
                return null;
            }

            var endPos = html.IndexOf(propEndString, startPos + propStartString.Length);
            var headPos = startPos + propStartString.Length;
            var tailPos = endPos; //- propEndString.Length;
            var propsRawString = new string(html.Skip(headPos).Take(tailPos - headPos).ToArray());
            var unescaped = System.Net.WebUtility.HtmlDecode(propsRawString);;
            var parsed = Newtonsoft.Json.JsonConvert.DeserializeObject<Crescendo.CrescendoLeoProps>(unescaped,
                new Newtonsoft.Json.JsonSerializerSettings()
                {

                });


            return parsed;
        }


        private static LeoPlayerProps ParseLeoPlayerProps(string html)
        {
            const string propStartString = @"var leoPlayerProps = {";


            var startPos = html.IndexOf(propStartString);
            if (startPos == -1)
            {
                return null;
            }

            var endPos = html.IndexOf("};", startPos) ;
            var headPos = startPos + propStartString.Length;
            var tailPos = endPos - 2;
            var propsRawString = new string(html.Skip(headPos).Take(tailPos - headPos).ToArray());

            // unescapeHTML/JSON.parse/乗算演算子の含まれる数値を取り除く

            var filteredPropsString = propsRawString
                .Replace("unescapeHTML(", "")
                .Replace("JSON.parse(", "")
                .Replace("streamQuality: LeoPlayer.getStreamQuality(window.location.hash),", "")
                .Replace(")", "")
                .Replace(" * 1000", "");


            Func<string, string, string> removeTargetJsonParamter = (string text, string targetString) =>
            {
                var removeStartPos = text.IndexOf(targetString);
                if (removeStartPos == -1)
                {
                    return text;
                }
                var removeCount = (text.IndexOf("},", removeStartPos) + 2) - removeStartPos;
                return text.Remove(removeStartPos, removeCount);
            };

            filteredPropsString = removeTargetJsonParamter(filteredPropsString, @"externalLayout");
            filteredPropsString = removeTargetJsonParamter(filteredPropsString, @"externalClient");

            var propsJson = $"{{ leoPlayerProps : {{{filteredPropsString}}} }}";





            var parsed = Newtonsoft.Json.JsonConvert.DeserializeObject<LeoPlayerPropsContainer>(propsJson, 
                new Newtonsoft.Json.JsonSerializerSettings()
                {
                    
                });


            return parsed?.LeoPlayerProps;
        }
        

        public static Task<LeoPlayerProps> GetLeoPlayerPropsAsync(NiconicoContext context, string liveId)
        {
            return GetLiveWatchPageHtmlAsync(context, liveId)
                .ContinueWith(prevTask => ParseLeoPlayerProps(prevTask.Result));
        }

        public static Task<Crescendo.CrescendoLeoProps> GetCrescendoLeoPlayerPropsAsync(NiconicoContext context, string liveId)
        {
            return GetLiveWatchPageHtmlAsync(context, liveId)
                .ContinueWith(prevTask => ParseCrescendoLeoPlayerProps(prevTask.Result));
        }

    }
}
