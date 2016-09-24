using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Searches.Community
{
    public enum CommunitySearchSort
    {
		CreatedAt,
		UpdateAt,
		CommunityLevel,
		VideoCount,
		MemberCount
    }


	public static class CommunitySearchSortExtention
	{
		public static string ToShortString(this CommunitySearchSort sort)
		{
			switch (sort)
			{
				case CommunitySearchSort.CreatedAt:
					return "c";
				case CommunitySearchSort.UpdateAt:
					return "u";
				case CommunitySearchSort.CommunityLevel:
					return "l";
				case CommunitySearchSort.VideoCount:
					return "t";
				case CommunitySearchSort.MemberCount:
					return "m";
				default:
					throw new NotSupportedException();
			}
		}
	}
}
