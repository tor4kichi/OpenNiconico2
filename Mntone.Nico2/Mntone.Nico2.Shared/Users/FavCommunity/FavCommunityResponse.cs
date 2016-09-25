using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Users.FavCommunity
{
	public class FavCommunityInfo
	{
		public string CommunityName { get; set; }
		public string CommunityId { get; set; }
		public string IconUrl { get; set; }
		public int VideoCount { get; set; }
		public int MemberCount { get; set; }
		public string ShortDescription { get; set; }
	}

    public class FavCommunityResponse
    {
		public List<FavCommunityInfo> Items { get; private set; } = new List<FavCommunityInfo>();
    }
}
