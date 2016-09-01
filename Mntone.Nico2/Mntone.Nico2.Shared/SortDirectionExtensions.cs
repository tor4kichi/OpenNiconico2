using System;

namespace Mntone.Nico2
{
	internal static class SortDirectionExtensions
	{
		public static char ToChar( this Order direction )
		{
			switch( direction )
			{
			case Order.Ascending:
				return 'a';
			case Order.Descending:
				return 'd';
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		public static string ToShortString( this Order direction )
		{
			switch( direction )
			{
			case Order.Ascending:
				return "asc";
			case Order.Descending:
				return "desc";
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}