using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Mntone.Nico2.Nicocas.Search
{
    public enum UserSearchSortOrder
    {
        [Description("followerCount")]
        FollowerCount,

        [Description("videoCount")]
        VideoCount,

        [Description("liveCount")]
        LiveCount,

    }
}
