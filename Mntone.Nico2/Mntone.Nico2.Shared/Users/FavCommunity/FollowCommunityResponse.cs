using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Users.FollowCommunity
{
	public class FollowCommunityInfo
	{
		public string CommunityName { get; set; }
		public string CommunityId { get; set; }
		public string IconUrl { get; set; }
		public int VideoCount { get; set; }
		public int MemberCount { get; set; }
		public string ShortDescription { get; set; }
	}

    public class FollowCommunityResponse
    {
		public List<FollowCommunityInfo> Items { get; private set; } = new List<FollowCommunityInfo>();
    }
}
