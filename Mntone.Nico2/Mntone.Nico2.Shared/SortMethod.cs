using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2
{
    public enum SortMethod
    {
		NewComment,    // n
		ViewCount,     // v
		MylistCount,   // m
		CommentCount,  // r
		FirstRetrieve, // f
		Length,		   // l
		Popurarity,    // h
    }


	public static class SortMethodExtention
	{
		public static char ToChar(this SortMethod method)
		{
			switch (method)
			{
				case SortMethod.NewComment:
					return 'n';
				case SortMethod.ViewCount:
					return 'v';
				case SortMethod.MylistCount:
					return 'm';
				case SortMethod.CommentCount:
					return 'r';
				case SortMethod.FirstRetrieve:
					return 'f';
				case SortMethod.Length:
					return 'l';
				case SortMethod.Popurarity:
					return 'h';
				default:
					throw new NotSupportedException($"not support {nameof(SortMethod)}.{method.ToString()}");
			}
		}

		public static string ToShortString(this SortMethod method)
		{
			return method.ToChar().ToString();
		}
	}
}
