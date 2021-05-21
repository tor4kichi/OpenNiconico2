using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Videos.Series
{
    internal sealed class SeriesVideosClient
    {
        
        private static Task<string> GetUserSeriesDetailsHtmlAsync(NiconicoContext context, string seriesId)
        {
            return context.GetStringAsync(NiconicoUrls.MakeSeriesPageUrl(seriesId));
        }

        private static SeriesDetails ParseSeriesDetailsHtml(string html)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);

            SeriesDetails seriesDetails = new SeriesDetails();

            var root = doc.DocumentNode;
            {
                var ownerAreaNode = root.SelectSingleNode("/html/body/div[3]/div[1]/div[1]/div[1]");
                var imageNode = ownerAreaNode.Descendants("img").FirstOrDefault();
                var anchorNode = ownerAreaNode.Descendants("a").FirstOrDefault();
                seriesDetails.Owner = new SeriesOwner()
                {
                    IconUrl = imageNode?.GetAttributeValue("src", string.Empty),
                    Id = anchorNode?.GetAttributeValue("href", string.Empty)?.Split('/').LastOrDefault(),
                    Nickname = anchorNode.InnerText
                };
            }

            {
                var ownerSeriesListNodes = root.SelectNodes("/html/body/div[3]/div[1]/div[2]/div[1]/div/ul/li");
                seriesDetails.OwnerSeries = ownerSeriesListNodes.Select(node =>
                {
                    var anchorNode = node.SelectSingleNode("a");
                    return new SeriesSimple()
                    {
                        Id = anchorNode?.GetAttributeValue("href", string.Empty),
                        Title = anchorNode.InnerText
                    };
                }).ToList();
            }
            {
                Func<string, int> _AsExtractNumber = (str) =>
                {
                    return int.Parse(new string(str.Where(c => char.IsDigit(c)).ToArray()));
                };

                var seriesetailContainerNode = root.SelectSingleNode("/html/body/div[3]/div[1]/div[2]/div[2]/div");
                var seriesDetailsChildren = seriesetailContainerNode.Elements("div");
                var detailMedia = seriesDetailsChildren.ElementAt(0);

                var detailFigure = detailMedia.Descendants("div").ElementAt(0);
                var seriesThumbnailImageDiv = detailFigure.SelectSingleNode("div/div");
                var detailBody = detailMedia.Elements("div").ElementAt(1);
                var detailDescription = seriesDetailsChildren.ElementAt(1);
                var expandedDescTextNode = detailDescription.SelectSingleNode("div/div/div/div[1]/div");
                seriesDetails.DescriptionHTML = expandedDescTextNode.InnerHtml;

                var bgUrl = seriesThumbnailImageDiv.GetAttributeValue("data-background-image", null);
                seriesDetails.Series = new Series()
                {
                    ThumbnailUrl = !string.IsNullOrEmpty(bgUrl) ? new Uri(bgUrl) : null,
                    Count = _AsExtractNumber(detailBody.Elements("div").ElementAt(1).InnerText),
                    Title = seriesThumbnailImageDiv.GetAttributeValue("alt", string.Empty),
                };

                var seriesVideoListContainer = seriesDetailsChildren.ElementAt(2);
                var videoNodes = seriesVideoListContainer.Elements("div");

                SeriresVideo _NodeToSeries(HtmlNode node)
                {
                    var seriesVideo = new SeriresVideo()
                    {
                        Id = node.GetAttributeValue("data-video-itemdata-video-id", string.Empty),
                    };
                    var mediaObjectContentNodeChildren = node.Element("div").Elements("div");
                    {
                        var mediaObjectImageNode = mediaObjectContentNodeChildren.ElementAt(0);
                        var anchorNode = mediaObjectImageNode.Element("a");
                        var VideoThumbnailNodeChildren = anchorNode.Element("div").Elements("div");
                        var thumbnailImageNode = VideoThumbnailNodeChildren.ElementAt(0);
                        seriesVideo.ThumbnailUrl = new Uri(thumbnailImageNode.GetAttributeValue("data-background-image", string.Empty));
                        seriesVideo.Title = thumbnailImageNode.GetAttributeValue("alt", string.Empty);

                        var videoLengthNode = VideoThumbnailNodeChildren.FirstOrDefault(x => x.GetAttributeValue("class", default(string)) == "VideoLength");
                        seriesVideo.Duration = videoLengthNode.InnerText.ToTimeSpan();
                    }
                    {
                        var mediaObjectBodyNodeChildren = mediaObjectContentNodeChildren.ElementAt(1).Elements("div");
                        var videoRegisteredAtNode = mediaObjectBodyNodeChildren.ElementAt(0);
                        var videoRegisteredAtText = videoRegisteredAtNode.InnerText;
                        if (videoRegisteredAtText.Contains("/"))
                        {
                            // ex) 2020/06/24 18:00 投稿
                            var videoRegisteredAtDateTimeText = string.Join(" ", videoRegisteredAtText.Trim().Split(' ').Take(2));
                            seriesVideo.PostAt = videoRegisteredAtDateTimeText.ToDateTimeOffsetFromIso8601().DateTime;
                        }
                        else 
                        {
                            if (videoRegisteredAtText.Contains("時間"))
                            {
                                // ex) 19時間前 投稿
                                var time = int.Parse(new string(videoRegisteredAtText.TrimStart(' ', '\t', '\n').TakeWhile(c => char.IsDigit(c)).ToArray()));
                                seriesVideo.PostAt = DateTime.Now - TimeSpan.FromHours(time);
                            }
                            else if (videoRegisteredAtText.Contains("分"))
                            {
                                // ex) 19分前 投稿 
                                // があるか知らないけど念の為
                                var time = int.Parse(new string(videoRegisteredAtText.TakeWhile(c => char.IsDigit(c)).ToArray()));
                                seriesVideo.PostAt = DateTime.Now - TimeSpan.FromMinutes(time);
                            }
                            else
                            {
                                seriesVideo.PostAt = DateTime.Now;
                            }
                        }
                        var titleNode = mediaObjectBodyNodeChildren.ElementAt(1);
                        //var descNode = mediaObjectBodyNodeChildren.ElementAt(2);
                        var videoMetaCountChildren = mediaObjectBodyNodeChildren.ElementAt(3).Elements("span");
                        var viewCountNode = videoMetaCountChildren.ElementAt(0);
                        seriesVideo.WatchCount = viewCountNode.InnerText.Trim('"').ToInt();
                        var commentCountNode = videoMetaCountChildren.ElementAt(1);
                        seriesVideo.CommentCount = commentCountNode.InnerText.Trim('"').ToInt();
                        var mylistCountNode = videoMetaCountChildren.ElementAt(2);
                        seriesVideo.MylistCount = mylistCountNode.InnerText.Trim('"').ToInt();
                    }

                    return seriesVideo;
                }

                seriesDetails.Videos = videoNodes.Select(_NodeToSeries).ToList();
            }

            return seriesDetails;
        }


        public static async Task<SeriesDetails> GetSeriesDetailsAsync(NiconicoContext context, string seriesId)
        {
            var html = await GetUserSeriesDetailsHtmlAsync(context, seriesId);
            var details = ParseSeriesDetailsHtml(html);
            details.Series.Id = seriesId;
            return details;
        }
    }
}
