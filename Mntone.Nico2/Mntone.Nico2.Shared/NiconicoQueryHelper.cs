using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mntone.Nico2
{
    public static class NiconicoQueryHelper
    {
		public static string RemoveIdPrefix(string withPrefixId)
		{
			var c1 = withPrefixId.ElementAt(0);
			var c2 = withPrefixId.ElementAt(1);
			
			if (c1 >= '0' && c1 <= '9')
			{
				return withPrefixId;
			}
			else
			{
				return withPrefixId.Substring(2);
			}
		}

		public static IEnumerable<string> RemoveIdPrefix(IEnumerable<string> items)
		{
			return items.Select(x => RemoveIdPrefix(x));
		}


		public static string Make_idlist_QueryKeyString(NiconicoItemType itemType)
		{
			// id_list[0][]=0123345&id_list[0][]=9876543
			// [0]は動画の場合、ItemTypeによって変化
			// "="の後の数字は接頭辞(sm等)を含まないID
//			return $"id_list%5B{(uint)itemType}%5D%5B%5D";
			return $"id_list[{(uint)itemType}][]";
		}
	}
}
