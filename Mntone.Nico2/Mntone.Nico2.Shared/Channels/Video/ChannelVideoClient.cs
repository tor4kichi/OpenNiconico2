using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Channels.Video
{
    public sealed class ChannelVideoInfo
    {
        public string ItemId { get; set; }

        public string Title { get; set; }
        public string ThumbnailUrl { get; set; }
        public TimeSpan Length { get; set; }

        public int ViewCount { get; set; }
        public int CommentCount { get; set; }
        public int MylistCount { get; set; }

        public DateTime PostedAt { get; set; }

        public string Description { get; set; }

        public string CommentSummary { get; set; }

        public bool IsRequirePayment { get; set; }
        public string PurchasePreviewUrl { get; set; }

        public bool IsMemberUnlimitedAccess { get; set; }
        public bool IsFreeForMember { get; set; }
    }


    public sealed class ChannelVideoResponse
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public List<ChannelVideoInfo> Videos { get; set; }
    }

    internal static class ChannelVideoClient
    {
        // Note: ChannelId の種類とアクセス方法
        // 
        // 英字のみのチャンネルIDは
        // http://ch.nicovideo.jp/yurucamp といった形でアクセスする
        // ch+数字の場合は
        // http://ch.nicovideo.jp/channel/ch2634868/ といった形でアクセスすると
        // 英字のみのチャンネルになるようリダイレクトされる

        private static string ChannelIdToURLDirectoryName(string channelId)
        {
            if (channelId.StartsWith("ch"))
            {
                if (channelId.Skip(2).All(c => c >= '0' && c <= '9'))
                {
                    return $"channel/{channelId}";
                }
            }
            else if (channelId.All(c => c >= '0' && c <= '9'))
            {
                return $"channel/ch{channelId}";
            }

            return channelId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="channelId"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static Task<string> GetChannelVideoPageHtmlAsync(NiconicoContext context, string channelId, int page)
        {            
            var directoryName = ChannelIdToURLDirectoryName(channelId);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            if (page != 0)
            {
                dict.Add("page", (page + 1).ToString());
            }
            return context.GetStringAsync($"{NiconicoUrls.ChannelUrlBase}{directoryName}/video", dict);
        }

        public static ChannelVideoResponse ParseChannelVideosFromChannelVideoPageHtml(string html, int page)
        {
            var doc = new HtmlDocument();

            var liReplaced = html.Replace("<!--/li-->", "</li>");
            doc.LoadHtml(liReplaced);

            var videoHeaderNode = doc.DocumentNode.Descendants("section").Single(x =>
            {
                return x.Attributes.Contains("class") && x.Attributes["class"].Value.Split(' ').Any(c => c == "video");
            });

            var res = new ChannelVideoResponse();
            res.Page = page;
            res.TotalCount = (int)Decimal.Parse(videoHeaderNode.Element("header").Element("span").Element("var").InnerText);

            try
            {
                var itemsNode = doc.DocumentNode.SelectNodes("//*[@id=\"video_page\"]/section[1]/article/section/section/ul/li");

                res.Videos = itemsNode
                    ?.Select(li =>
                    {
                        ChannelVideoInfo info = new ChannelVideoInfo();

                        // div.item_left
                        var left = li.GetElementByClassName("item_left");
                        {
                            var anchor = left.Element("a");
                            var thumbnailImage = anchor.Element("img");
                            info.ThumbnailUrl = thumbnailImage.Attributes["src"].Value;

                            var ppv = anchor.GetElementByClassName("purchase_type");
                            if (ppv != null)
                            {
                                foreach (var ppvType in ppv.Element("span").GetClasses())
                                {
                                    if (ppvType== "all_pay")
                                    {
                                        info.IsRequirePayment = true;
                                    }
                                    else if (ppvType == "free_for_member")
                                    {
                                        info.IsFreeForMember = true;
                                    }
                                    else if (ppvType == "member_unlimited_access")
                                    {
                                        info.IsMemberUnlimitedAccess = true;
                                    }
                                }
                            }

                            var commentDescNode = anchor.GetElementByClassName("last_res");
                            info.CommentSummary = commentDescNode?.InnerText;

                            var lengthNode = anchor.GetElementByClassName("length");
                            info.Length = lengthNode.InnerText.ToTimeSpan();
                        }

                        var right = li.GetElementByClassName("item_right");
                        {
                            var titleNode = right.GetElementByClassName("title");
                            {
                                var titleAnchor = titleNode.Element("a");
                                info.Title = titleAnchor.InnerText;
                                var videoUrl = titleAnchor.Attributes["href"].Value;
                                info.ItemId = videoUrl.Split('/').Last();
                            }

                            var actionsNode = right.GetElementByClassName("actions");
                            {
                                var liList = actionsNode.Element("ul").Elements("li");
                                var previewAnchorNode = liList.ElementAtOrDefault(1)?.GetElementByClassName("purchase_method_preview");
                                if (previewAnchorNode != null)
                                {
                                    info.PurchasePreviewUrl = previewAnchorNode.Attributes["href"].Value;
                                }
                            }

                            var countsNode = right.GetElementByClassName("counts");
                            {
                                var children = countsNode.Elements("li");
                                foreach (var countLi in children)
                                {
                                    var className = countLi.Attributes["class"].Value.Trim();
                                    if (className == "view")
                                    {
                                        info.ViewCount = (int)Decimal.Parse(countLi.Element("var").InnerText); // 数字に , が付いてるのでDecimalを経由
                                    }
                                    else if (className == "comment")
                                    {
                                        info.CommentCount = (int)Decimal.Parse(countLi.Element("var").InnerText); // 数字に , が付いてるのでDecimalを経由
                                    }
                                    else if (className == "mylist")
                                    {
                                        info.MylistCount = (int)Decimal.Parse(countLi.Element("a").Element("var").InnerText); // 数字に , が付いてるのでDecimalを経由
                                    }
                                }
                            }

                            var descNode = right.GetElementByClassName("description");
                            {
                                info.Description = descNode.InnerText;
                            }

                            var timeNode = right.GetElementByClassName("time");
                            {
                                var timeText = timeNode.Element("time").Element("var").Attributes["title"].Value;
                                info.PostedAt = DateTime.Parse(timeText);
                            }
                        }

                        return info;
                    })
                    ?.ToList() 
                    ?? new List<ChannelVideoInfo>();

            }
            catch
            {
                res.Videos = new List<ChannelVideoInfo>();
            }

            return res;
        }
            
        




        public static Task<ChannelVideoResponse> GetChannelVideosAsync(NiconicoContext context, string channelId, int page)
        {
            return GetChannelVideoPageHtmlAsync(context, channelId, page)
                .ContinueWith(prevTask => ParseChannelVideosFromChannelVideoPageHtml(prevTask.Result, page));
        }
    }
}
