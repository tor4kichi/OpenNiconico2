using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mntone.Nico2.Mylist
{
    public static class MylistQueryUtil
    {
		public static string MylistDataToQueryString(MylistData mylistData)
		{
			return NiconicoQueryHelper.Make_idlist_QueryString(mylistData.ItemType, mylistData.ItemId);
		}

		public static string MylistDataToQueryString(IEnumerable<MylistData> list)
		{
			return String.Join("&", list.Select(MylistDataToQueryString));
		}
	}
}
