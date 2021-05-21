using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

#if WINDOWS_UWP
using Windows.Web.Http;
#else 
using System.Net.Http;
#endif

namespace Mntone.Nico2.Live.Reservation
{
    public sealed class ReservationToken
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
#if WINDOWS_UWP
                Content = new HttpFormUrlEncodedContent(dict)
#else
                Content = new FormUrlEncodedContent(dict)
#endif
            };

            requestMessage.Headers.Add("origin", "https://live.nicovideo.jp");
            var response = await context.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead);

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
        public static async Task<ReservationToken> GetReservationToken(NiconicoContext context)
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
                return new ReservationToken()
                {
                    Token = confirmNode.GetAttributeValue("value", "")
                };
            }
            else
            {
                return null;
            }
        }



        public static Task DeleteReservationAsync(NiconicoContext context, string vid, ReservationToken reservationDeleteToken)
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





        public static Task UseReservationAsync(NiconicoContext context, string vid, ReservationToken token)
        {
            if (vid.StartsWith("lv"))
            {
                vid = vid.Remove(0, 2);
            }

            if (!int.TryParse(vid, out var vidNumber))
            {
                throw new ArgumentException("vid can not accept NiconicoLiveContentId.");
            }

            var dict = new Dictionary<string, string>();
            dict.Add("accept", "true");
            dict.Add("mode", "use");
            dict.Add("vid", vid);
            dict.Add("token", token.Token);

            return context.PostAsync($"http://live.nicovideo.jp/api/watchingreservation", dict, withToken:false);
        }





        public static async Task<MyTimeshiftListData> GetMyTimeshiftListAsync(NiconicoContext context)
        {
            MyTimeshiftListData data = new MyTimeshiftListData();
            var timeshiftPageHtmlText = await context.GetStringAsync("http://live.nicovideo.jp/my_timeshift_list");

            // input要素のクローズを行う

            HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();

            // Note: fix <input> node not closing issue.
            htmlDocument.OptionAutoCloseOnEnd = true;

            htmlDocument.LoadHtml(timeshiftPageHtmlText);

            var errors = htmlDocument.ParseErrors.ToArray();
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    System.Diagnostics.Debug.WriteLine(error.ToString());
                }
//                throw new Exception();
            }



            try
            {
                var confirmNode = htmlDocument.DocumentNode
                    .Descendants("input")
                    .FirstOrDefault(x => x.Id == "confirm")
                    ;

                if (confirmNode == null) { return null; }

                data.Token = confirmNode.GetAttributeValue("value", "");
            }
            catch (Exception e)
            {
                throw new Exception("not Loggedin, or HTML layout changed (in http://live.nicovideo.jp/my_timeshift_list). can not parsing MyTimeshiftList HTML.", e);
            }

            var formNode = htmlDocument.DocumentNode.Descendants("div").First(x => x.Attributes["class"]?.Value == "liveItems clearfix");
            var liveItemNodes = formNode.GetElementsByClassName("column");

            Regex dateTimeRegex = new Regex(@"(\d\d\d\d)\/(\d?\d)\/(\d?\d)\(\S\)(\d?\d):(\d?\d)");
            foreach (var liveItemNode in liveItemNodes)
            {
                MyTimeshiftListItem item = new MyTimeshiftListItem();

                var nameAnchorNode = liveItemNode.GetElementByClassName("name").Element("a");
                var title = nameAnchorNode.Attributes["title"];
                var gateUrl = nameAnchorNode.Attributes["href"];

                item.Id = gateUrl.Value.Split('/').Last();

                var statusNode = liveItemNode.GetElementByClassName("status");
                var statusSpanNode = statusNode.Element("span");
                var statusSpanNodeClassName = statusSpanNode.Attributes["class"].Value;
                var statusTextItems = statusSpanNode.InnerText.Split(new[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                //【 statusSpanNode.InnerText の具体例 】
                // 
                // 視聴する	期限中、何度でも視聴できます	[2018/11/11(日)23:59まで]
                // 視聴する	[2018/11/11(日)12:37まで]
                // 視聴する	[視聴期限未定]
                // 予約中	[2018/11/23(金)11:00放送開始]
                // 視聴権利用期限が切れています	[2018/11/11(日)12:37まで]
                // 
                // TODO: このほか期限切れの表記も必要

                Func<string, DateTimeOffset?> ExpiredTextToDateTimeOffset = (t) => 
                {
                    if (dateTimeRegex.IsMatch(t))
                    {
                        var match = dateTimeRegex.Match(t);
                        var times = match.Groups.Cast<Group>().Skip(1).Select(x => int.Parse(x.Value)).ToArray();
                        return new DateTimeOffset(times[0], times[1], times[2], times[3], times[4], 0, DateTimeOffset.Now.Offset);
                    }
                    else { return null; }
                };

                var timeLimitText = statusTextItems.Last();

                switch (statusSpanNodeClassName)
                {
                    case "timeshift_watch":

                        if (timeLimitText == "[視聴期限未定]")
                        {
                            item.WatchTimeLimit = DateTimeOffset.MaxValue;
                        }
                        else 
                        {
                            item.WatchTimeLimit = ExpiredTextToDateTimeOffset(timeLimitText);
                        }

                        item.IsCanWatch = true;
                        break;
                    case "timeshift_reservation":
                        //if (DateTimeOffset.TryParseExact(timeLimitText, "[yyyy/mm/dd(ddd)HH:MM放送開始]", null, System.Globalization.DateTimeStyles.AssumeLocal, out var timeLimit))
                        {
                            //                                item.WatchTimeLimit = timeLimit;
                        }
                        break;
                    case "timeshift_disable":
                        item.WatchTimeLimit = ExpiredTextToDateTimeOffset(timeLimitText);
                        break;
                }

                data._Items.Add(item);
            }
            

            return data;
        }
    }

    public sealed class MyTimeshiftListData
    {
        public string Token { get; internal set; }

        internal List<MyTimeshiftListItem> _Items = new List<MyTimeshiftListItem>();

        public IReadOnlyList<MyTimeshiftListItem> Items => _Items;
    }

    public sealed class MyTimeshiftListItem
    {
        public DateTimeOffset? WatchTimeLimit { get; internal set; }
        public bool IsCanWatch { get; internal set; }
        public string Id { get; internal set; }
    }
}
