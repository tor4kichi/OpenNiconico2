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
			return withPrefixId.Substring(0, 2);
		}

		public static IEnumerable<string> RemoveIdPrefix(IEnumerable<string> items)
		{
			return items.Select(x => RemoveIdPrefix(x));
		}


		public static string Make_idlist_QueryString(NiconicoItemType itemType, string withPrefixId)
		{
			// id_list[0][]=0123345&id_list[0][]=9876543
			// [0]は動画の場合、ItemTypeによって変化
			// "="の後の数字は接頭辞(sm等)を含まないID
			return $"id_list[{(uint)itemType}][]={NiconicoQueryHelper.RemoveIdPrefix(withPrefixId)}";
		}
	}
}
