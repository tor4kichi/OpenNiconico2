using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Embed.Ichiba
{
    public sealed class IchibaClient
    {
        public static Task<string> GetIchibaJsonAsync(NiconicoContext context, string requestId)
        {
            var url = $"http://ichiba.nicovideo.jp/embed/zero/show_ichiba?v={requestId}&country=ja-jp&ch=&rev=20120220";
            return context.GetStringAsync(url);
        }

        private static IchibaResponse ParseIchibaJson(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<IchibaResponse>(json);
        }

        /// <summary>
        /// 動画や生放送のニコニコ市場情報を取得します。<br />
        /// ニコニコ動画 Zero 世代のニコニコ市場APIを利用しています。
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        public static Task<IchibaResponse> GetIchibaAsync(NiconicoContext context, string requestId)
        {
            return GetIchibaJsonAsync(context, requestId)
                .ContinueWith(prevTask => ParseIchibaJson(prevTask.Result));
        }
    }
}
