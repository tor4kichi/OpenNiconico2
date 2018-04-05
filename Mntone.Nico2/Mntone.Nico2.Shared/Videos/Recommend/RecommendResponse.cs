using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Videos.Recommend
{
    [DataContract]
    public class RecommendInfo
    {

        [DataMember(Name = "seed")]
        public int Seed { get; set; }

        [DataMember(Name = "page")]
        public int Page { get; set; }

        [DataMember(Name = "end_of_recommend")]
        public bool EndOfRecommend { get; set; }
    }

    [DataContract]
    public class Sherlock
    {

        [DataMember(Name = "tag")]
        public string Tag { get; set; }
    }

    [DataContract]
    public class AdditionalInfo
    {

        [DataMember(Name = "sherlock")]
        public Sherlock Sherlock { get; set; }
    }

    [DataContract]
    public class Item
    {

        [DataMember(Name = "item_type")]
        public string ItemType { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "thumbnail_url")]
        public string ThumbnailUrl { get; set; }

        [DataMember(Name = "title_short")]
        public string TitleShort { get; set; }

        [DataMember(Name = "view_counter")]
        public int ViewCounter { get; set; }

        [DataMember(Name = "num_res")]
        public int NumRes { get; set; }

        [DataMember(Name = "mylist_counter")]
        public int MylistCounter { get; set; }

        [DataMember(Name = "first_retrieve")]
        public long FirstRetrieve { get; set; }

        [DataMember(Name = "length")]
        public string Length { get; set; }

        [DataMember(Name = "is_original_language")]
        public bool IsOriginalLanguage { get; set; }

        [DataMember(Name = "is_translated")]
        public bool IsTranslated { get; set; }

        [DataMember(Name = "additional_info")]
        public AdditionalInfo AdditionalInfo { get; set; }



        public TimeSpan ParseLengthToTimeSpan()
        {
            return Length.ToTimeSpan();
        }

        public DateTimeOffset ParseForstRetroeveToDateTimeOffset()
        {
            return FirstRetrieve.ToDateTimeOffsetFromUnixTime();
        }

        public string ParseTitle()
        {
            return System.Net.WebUtility.HtmlDecode(TitleShort);
        }
    }

    [DataContract]
    public class RecommendContent
    {

        [DataMember(Name = "recommend_info")]
        public RecommendInfo RecommendInfo { get; set; }

        [DataMember(Name = "items")]
        public IList<Item> Items { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }
    }

    [DataContract]
    public class RecommendResponse
    {

        [DataMember(Name = "seed")]
        public int Seed { get; set; }

        [DataMember(Name = "page")]
        public int Page { get; set; }

        [DataMember(Name = "user_tag_param")]
        public string UserTagParam { get; set; }

        [DataMember(Name = "compiled_tpl")]
        public object CompiledTpl { get; set; }

        [DataMember(Name = "first_data")]
        public RecommendContent FirstData { get; set; }
    }


}
