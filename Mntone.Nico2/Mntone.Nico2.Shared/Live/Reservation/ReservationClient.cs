using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Mntone.Nico2.Live.Reservation
{
    public sealed class ReservationDeleteToken
    {
        public string Token { get; internal set; }
    }


    internal sealed class ReservationClient
    {
        public static async Task<string> PostReservationAsync(NiconicoContext context, string vid, bool overwrite)
        {
            var dict = new Dictionary<string, string>();

            if (vid.StartsWith("lv"))
            {
                vid = vid.Remove(0, 2);
            }

            if (!int.TryParse(vid, out var vidNumber))
            {
                throw new ArgumentException("vid can not accept NiconicoLiveContentId.");
            }

            dict.Add(nameof(vid), vid);
            dict.Add(nameof(overwrite), overwrite.ToString1Or0());

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, new Uri("http://live.nicovideo.jp/api/timeshift.reservations"))
            {
                Content = new HttpFormUrlEncodedContent(dict)
            };

            var response = await context.GetClient().SendRequestAsync(requestMessage, HttpCompletionOption.ResponseContentRead);

            return await response.Content.ReadAsStringAsync();
        }


        private static ReservationResponse ParseReservationJson(string json)
        {
            return JsonSerializerExtensions.Load<ReservationResponse>(json);
        }

        public static Task<ReservationResponse> ReservationAsync(NiconicoContext context, string vid, bool overwrite)
        {
            return PostReservationAsync(context, vid, overwrite)
                .ContinueWith(prevTask => ParseReservationJson(prevTask.Result));
        }


        

        /// <summary>
        /// タイムシフト予約の削除用トークンを取得します。（要ログインセッション）
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task<ReservationDeleteToken> GetReservationDeleteToken(NiconicoContext context)
        {
            var timeshiftPageHtmlText = await context.GetStringAsync("http://live.nicovideo.jp/my_timeshift_list");

            HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(timeshiftPageHtmlText);

            var confirmNode = htmlDocument.DocumentNode
                .Descendants("input")
                .FirstOrDefault(x => x.Id == "confirm")
                ;
            
            // will return string like "ulck_0123456789"
            if (confirmNode != null)
            {
                return new ReservationDeleteToken()
                {
                    Token = confirmNode.GetAttributeValue("value", "")
                };
            }
            else
            {
                return null;
            }
        }



        public static Task DeleteReservationAsync(NiconicoContext context, string vid, ReservationDeleteToken reservationDeleteToken)
        {
            var dict = new Dictionary<string, string>();
            
            if (vid.StartsWith("lv"))
            {
                vid = vid.Remove(0, 2);
            }

            if (!int.TryParse(vid, out var vidNumber))
            {
                throw new ArgumentException("vid can not accept NiconicoLiveContentId.");
            }

            dict.Add("delete", "timeshift");
            dict.Add(nameof(vid), vid);
            dict.Add("confirm", reservationDeleteToken.Token);

            return context.GetStringAsync("http://live.nicovideo.jp/my", dict);
        }



    }
}
