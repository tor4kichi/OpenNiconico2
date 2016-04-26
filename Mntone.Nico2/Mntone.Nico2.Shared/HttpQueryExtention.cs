using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mntone.Nico2
{
	public static class HttpQueryExtention
	{
		public static string DictionaryToQuery(IDictionary<string, string> dict)
		{
			return String.Join("&",
				dict.Select(pair => $"{pair.Key}={pair.Value}")
				);
		}

		public static IDictionary<string, string> QueryToDictionary(string query)
		{
			return query.Split(new char[] { '&' }).ToDictionary(
						source => source.Substring(0, source.IndexOf('=')),
						source => Uri.UnescapeDataString(source.Substring(source.IndexOf('=') + 1)));
		}
	}
}
