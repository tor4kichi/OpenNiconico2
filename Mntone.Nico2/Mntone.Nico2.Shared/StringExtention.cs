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
    }
}
