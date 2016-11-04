using Mntone.Nico2.Communities.Detail;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Communities.Video
{
	public class CommunityVideoResponse
    {
		public uint VideoCount { get; set; }
		public List<CommunityVideoDetail> Videos { get; set; } = new List<CommunityVideoDetail>();
    }


	public class CommunityVideoDetail : CommunityVideo
	{
		public DateTime PostedAt { get; set; }

		public uint ViewCount { get; set; }
		public uint CommentCount { get; set; }
		public uint MylistCount { get; set; }

		public Uri ThumbnailUri { get; set; }

		public bool IsCommunityOnly { get; set; }
	}
}
