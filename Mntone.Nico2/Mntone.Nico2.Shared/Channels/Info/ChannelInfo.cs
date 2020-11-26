using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Channels.Info
{
    [DataContract]
    public class ChannelInfo
    {

        [DataMember(Name = "channel_id")]
        public int ChannelId { get; set; }

        [DataMember(Name = "category_id")]
        public int CategoryId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "company_viewname")]
        public string CompanyViewname { get; set; }

        [DataMember(Name = "open_time")]
        public string OpenTime { get; set; }

        [DataMember(Name = "update_time")]
        public string UpdateTime { get; set; }

        //[DataMember(Name = "dfp_setting")]
        //public string DfpSetting { get; set; }

        [DataMember(Name = "screen_name")]
        public string ScreenName { get; set; }


        public DateTime ParseOpenTime()
        {
            return DateTime.Parse(OpenTime);
        }

        public DateTime ParseUpdateTime()
        {
            return DateTime.Parse(UpdateTime);
        }
    }


}
