using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2
{
    public enum Sort
    {
		NewComment,    // n
		ViewCount,     // v
		MylistCount,   // m
		CommentCount,  // r
		FirstRetrieve, // f
		Length,		   // l
		Popurarity,    // h


		MylistPopurarity, // c
		UpdateTime,    // n
		Relation,      // s
		VideoCount,    // i
	}


	public static class SortMethodExtention
	{
		public static char ToChar(this Sort method)
		{
			switch (method)
			{
				case Sort.NewComment:
					return 'n';
				case Sort.ViewCount:
					return 'v';
				case Sort.MylistCount:
					return 'm';
				case Sort.CommentCount:
					return 'r';
				case Sort.FirstRetrieve:
					return 'f';
				case Sort.Length:
					return 'l';
				case Sort.Popurarity:
					return 'h';

				case Sort.MylistPopurarity:
					return 'c';
				case Sort.UpdateTime:
					return 'n';
				case Sort.Relation:
					return 's';
				case Sort.VideoCount:
					return 'i';
				default:
					throw new NotSupportedException($"not support {nameof(Sort)}.{method.ToString()}");
			}
		}

		public static string ToShortString(this Sort method)
		{
			return method.ToChar().ToString();
		}
	}
}
