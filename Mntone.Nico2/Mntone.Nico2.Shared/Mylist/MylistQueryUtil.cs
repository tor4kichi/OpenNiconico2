using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mntone.Nico2.Mylist
{
    public static class MylistQueryUtil
    {
		public static string RemoveIdPrefix(string withPrefixId)
		{
			return withPrefixId.Substring(0, 2);
		}

		public static IEnumerable<string> RemoveIdPrefix(IEnumerable<string> items)
		{
			return items.Select(x => RemoveIdPrefix(x));
		}

		public static string IdListToQueryString(IEnumerable<string> id_list)
		{
			// 全てのIDからsm000000の"sm"などの接頭辞を取り除いて
			var no_prefix_id_list = MylistQueryUtil.RemoveIdPrefix(id_list);

			// "[000, 111]"という形の文字列に変換
			return $"[{String.Join(",", no_prefix_id_list)}]";
		}


		public static string MylistDataToQueryString(NiconicoItemType itemType, string withPrefixId)
		{
			// id_list[0][]=0123345&id_list[0][]=9876543
			// [0]は動画の場合、ItemTypeによって変化
			// "="の後の数字は接頭辞(sm等)を含まないID
			return $"id_list[{(uint)itemType}][]={MylistQueryUtil.RemoveIdPrefix(withPrefixId)}";
		}

		public static string MylistDataToQueryString(MylistData mylistData)
		{
			return MylistDataToQueryString(mylistData.ItemType, mylistData.ItemId);
		}

		public static string MylistDataToQueryString(IEnumerable<MylistData> list)
		{
			return String.Join("&", list.Select(MylistDataToQueryString));
		}
	}
}
