using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Videos.Search
{
    public enum SearchSortMethod
    {
		NewComment,    // n
		ViewCount,     // v
		MylistCount,   // m
		CommentCount,  // r
		FirstRetrieve, // f
		Length,		   // l
		Popurarity,    // h
    }


	public static class SearchSortMethodExtention
	{
		public static char ToChar(this SearchSortMethod method)
		{
			switch (method)
			{
				case SearchSortMethod.NewComment:
					return 'n';
				case SearchSortMethod.ViewCount:
					return 'v';
				case SearchSortMethod.MylistCount:
					return 'm';
				case SearchSortMethod.CommentCount:
					return 'r';
				case SearchSortMethod.FirstRetrieve:
					return 'f';
				case SearchSortMethod.Length:
					return 'l';
				case SearchSortMethod.Popurarity:
					return 'h';
				default:
					throw new NotSupportedException($"not support {nameof(SearchSortMethod)}.{method.ToString()}");
			}
		}

		public static string ToShortString(this SearchSortMethod method)
		{
			return method.ToChar().ToString();
		}
	}
}
