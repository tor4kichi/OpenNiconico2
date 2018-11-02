using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;


namespace Mntone.Nico2.Embed.Ichiba
{
    [DataContract]
    public sealed class Polling
    {

        [DataMember(Name = "shortIntarval")]
        public int ShortIntarval { get; set; }

        [DataMember(Name = "longIntarval")]
        public int LongIntarval { get; set; }

        [DataMember(Name = "defaultIntarval")]
        public int DefaultIntarval { get; set; }

        [DataMember(Name = "maxNoChangeCount")]
        public int MaxNoChangeCount { get; set; }
    }

    [DataContract]
    public sealed class IchibaResponse
    {
        [DataMember(Name = "pickup")]
        public string Pickup { get; set; }

        [DataMember(Name = "main")]
        public string Main { get; set; }

        [DataMember(Name = "polling")]
        public Polling Polling { get; set; }



        List<IchibaItem> _MainItems;
        List<IchibaItem> _PickupItems;
        public List<IchibaItem> GetMainIchibaItems()
        {
            return _MainItems
                ?? (_MainItems = ParseIchibaHtml(Main));
        }

        public List<IchibaItem> GetPickupIchibaItems()
        {
            return _PickupItems
                ?? (_PickupItems = ParseIchibaHtml(Pickup));
        }


        private static List<IchibaItem> ParseIchibaHtml(string html)
        {
            var htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(html);

            var root = htmlDocument.DocumentNode;
            List<IchibaItem> items = new List<IchibaItem>();

            var ichibaMainNode = root.FirstChild;
            var ichiba_mainitemList = ichibaMainNode.GetElementsByClassName("ichiba_mainitem");

            foreach (var itemNode in ichiba_mainitemList)
            {
                try
                {
                    var item = IchibaItemFromXmlNode(itemNode);
                    items.Add(item);
                }
                catch { }
            }

            return items;
        }


        private static IchibaItem IchibaItemFromXmlNode(HtmlAgilityPack.HtmlNode node)
        {
            IchibaItem resultItem = new IchibaItem();
            var ichibaItemNode = node.GetElementByTagName("div");

            resultItem.Id = ichibaItemNode.Id.Split('_').Last();

            // Thumbnail
            {
                var img = ichibaItemNode.GetElementByClassName("thumbnail")
                    .GetElementByTagName("div")
                    .GetElementByTagName("a")
                    .GetElementByTagName("img");

                var url = img?.GetAttributeValue("src", "");
                if (url != null)
                {
                    resultItem.ThumbnailUrl = new Uri(url);
                }
            }

            // amazonLink + Title
            {
                var amazonNode = ichibaItemNode.GetElementByClassName("itemname")
                    .GetElementByTagName("a");

                var title = amazonNode.InnerText;
                var amazonLink = amazonNode.GetAttributeValue("href", "");
                resultItem.AmazonItemLink = new Uri(amazonLink);
                resultItem.Title = System.Net.WebUtility.HtmlDecode(title);
            }

            // maker
            {
                var makerNode = ichibaItemNode.GetElementByClassName("maker");
                var makerName = makerNode?.InnerText;
                resultItem.Maker = System.Net.WebUtility.HtmlDecode(makerName);
            }

            // price + discountText
            {
                var priceNode = ichibaItemNode.GetElementByClassName("price");
                if (priceNode != null)
                {
                    resultItem.Price = priceNode.InnerText;

                    var discountTextNode = priceNode.GetElementByTagName("span");
                    if (discountTextNode != null)
                    {
                        resultItem.DiscountText = discountTextNode.InnerText;
                    }
                }

            }

            {
                var releaseNode = ichibaItemNode.GetElementByClassName("release")
                    ?.GetElementByTagName("span");
                if (releaseNode  != null)
                {
                    resultItem.Reservation = new IchibaItemReservation()
                    {
                        ReleaseDate = releaseNode.InnerText
                    };
                }
            }


            // action buy click
            var actionNode = ichibaItemNode.GetElementByClassName("action");
            if (actionNode != null)
            {

                IchibaItemSellBase sellInfo = null;
                if (resultItem.Reservation != null)
                {
                    sellInfo = resultItem.Reservation;
                    var reservationNode = actionNode.GetElementByClassName("reservation");
                    if (reservationNode != null)
                    {
                        resultItem.Reservation.ReservationActionText = reservationNode.InnerText;
                    }

                    var buyYesterdayNode = actionNode.GetElementByClassName("reservationYesterday");
                    if (buyYesterdayNode != null)
                    {
                        resultItem.Reservation.YesterdayReservationActionText = buyYesterdayNode.InnerText;
                    }
                }
                else
                {
                    sellInfo = resultItem.Sell = new IchibaItemSell();
                    var buyNode = actionNode.GetElementByClassName("buy");
                    if (buyNode != null)
                    {
                        resultItem.Sell.BuyActionText = buyNode.InnerText;
                    }

                    var buyYesterdayNode = actionNode.GetElementByClassName("buyYesterday");
                    if (buyYesterdayNode != null)
                    {
                        resultItem.Sell.YesterdayBuyActionText = buyYesterdayNode.InnerText;
                    }
                }

                var clickNode = actionNode.GetElementByClassName("click");
                if (clickNode != null)
                {
                    sellInfo.ClickActionText = clickNode.InnerText;
                }

                var lastNode = actionNode.GetElementsByTagName("span").LastOrDefault();
                if (lastNode != null && !lastNode.Attributes.Contains("class"))
                {
                    sellInfo.ClickInThisContentText = lastNode.InnerText;
                }
            }

            {
                var goIchibaNode = ichibaItemNode.GetElementByClassName("goIchiba")
                    ?.GetElementByTagName("a");

                if (goIchibaNode != null)
                {
                    var ichibaLink = goIchibaNode.GetAttributeValue("href", "");
                    resultItem.IchibaUrl = new Uri(ichibaLink);
                }
            }


            return resultItem;
        }
    }

    public class IchibaItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public Uri ThumbnailUrl { get; set; }
        public Uri AmazonItemLink { get; set; }
        public string Maker { get; set; }
        public string Price { get; set; }
        public string DiscountText { get; set; }

        public Uri IchibaUrl { get; set; }

        public IchibaItemReservation Reservation { get; set; }
        public IchibaItemSell Sell { get; set; }
    }


    public class IchibaItemReservation : IchibaItemSellBase
    {
        public string ReleaseDate { get; set; }
        public string ReservationActionText { get; set; }
        public string YesterdayReservationActionText { get; set; }

    }

    public class IchibaItemSell : IchibaItemSellBase
    {
        public string BuyActionText { get; set; }
        public string YesterdayBuyActionText { get; set; }

    }

    public class IchibaItemSellBase
    {
        public string ClickActionText { get; set; }
        public string ClickInThisContentText { get; set; }

    }


}
