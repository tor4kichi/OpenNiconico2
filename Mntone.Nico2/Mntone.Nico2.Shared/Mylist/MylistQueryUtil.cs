using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mntone.Nico2.Mylist
{
    public static class MylistQueryUtil
    {
		public static Tuple<string, string> MylistDataToQueryString(MylistData mylistData)
		{
			var key = NiconicoQueryHelper.Make_idlist_QueryKeyString(mylistData.ItemType);
			var val = NiconicoQueryHelper.RemoveIdPrefix(mylistData.ItemId);
			return new Tuple<string, string>(key, val);
		}

		public static string MylistDataToQueryString(IEnumerable<MylistData> list)
		{
			return String.Join("&", list.Select(MylistDataToQueryString).Select(x => x.Item1 + "=" + x.Item2));
		}
	}
}
