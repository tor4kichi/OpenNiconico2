using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mntone.Nico2
{
    public static class StringExtention
    {
		public static string DecodeUTF8(this string encoded)
		{
			if (encoded != null)
			{
				var bytes = encoded.Select(x => Convert.ToByte(x)).ToArray();
				return Encoding.UTF8.GetString(bytes);
			}
			else
			{
				return "";
			}
		}

		public static string ToCamelCase(this string str)
		{
			if (!string.IsNullOrEmpty(str) && str.Length > 1)
			{
				return Char.ToLowerInvariant(str[0]) + str.Substring(1);
			}
			return str;
		}
	}
}
