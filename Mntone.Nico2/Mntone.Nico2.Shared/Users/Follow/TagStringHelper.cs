using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mntone.Nico2.Users.Follow
{
    public static class TagStringHelper
    {
		static Dictionary<char, char> NumberZenkakuToHankaku = new Dictionary<char, char>();

		static TagStringHelper()
		{
			NumberZenkakuToHankaku.Add('０', '0');
			NumberZenkakuToHankaku.Add('１', '1');
			NumberZenkakuToHankaku.Add('２', '2');
			NumberZenkakuToHankaku.Add('３', '3');
			NumberZenkakuToHankaku.Add('４', '4');
			NumberZenkakuToHankaku.Add('５', '5');
			NumberZenkakuToHankaku.Add('６', '6');
			NumberZenkakuToHankaku.Add('７', '7');
			NumberZenkakuToHankaku.Add('８', '8');
			NumberZenkakuToHankaku.Add('９', '9');
		}

		public static string ToEnsureHankakuNumberTagString(this string tag)
		{
			bool hasZenkakuNumber = false;
			foreach (var c in tag)
			{
				if (NumberZenkakuToHankaku.ContainsKey(c))
				{
					hasZenkakuNumber = true;
					break;
				}
			}

			if (hasZenkakuNumber)
			{
				return new string(tag.Select(x =>
				{
					return NumberZenkakuToHankaku.ContainsKey(x) ? NumberZenkakuToHankaku[x] : x;
				}).ToArray());
			}
			else
			{
				return tag;
			}
		}
    }
}
