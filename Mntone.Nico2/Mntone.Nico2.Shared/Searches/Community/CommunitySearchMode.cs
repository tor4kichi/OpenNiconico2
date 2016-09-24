using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Searches.Community
{
    public enum CommunitySearchMode
    {
		Keyword,
		Tag
    }

	public static class CommunitySearchModeExtention
	{
		public static string ToShortString(this CommunitySearchMode mode)
		{
			switch (mode)
			{
				case CommunitySearchMode.Keyword:
					return "s";
				case CommunitySearchMode.Tag:
					return "t";
				default:
					throw new NotSupportedException();
			}
		}
	}
}
