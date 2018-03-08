using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Users.Follow
{
    [DataContract]
    public class ChannelFollowResult
    {

        [DataMember(Name = "channel_id")]
        public string ChannelId { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        private bool? _IsSucceed;


        /// <summary>
        /// フォローの追加や削除に成功した場合に true を示します。<br />
        /// 既に追加済み、解除済みだった場合も true を示します。
        /// </summary>
        public bool IsSucceed
        {
            get { return (_IsSucceed ?? (_IsSucceed = Status == "succeed")).Value; }
        }

    }
}
