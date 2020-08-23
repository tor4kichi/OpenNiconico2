
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mntone.Nico2.Mylist
{
    public static class MylistJsonSerializeHelper
    {
		public static List<LoginUserMylistGroup> ParseMylistGroupListJson(string json)
		{
			var parsedJson = JsonConvert.DeserializeObject<LoginUserMylistGroupResponse>(json);

			return parsedJson.mylistgroup
				.Cast<LoginUserMylistGroup>()
				.ToList();
		}
	}
}
