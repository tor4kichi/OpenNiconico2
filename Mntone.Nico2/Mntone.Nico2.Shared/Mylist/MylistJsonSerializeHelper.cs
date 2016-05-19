using Mntone.Nico2.Mylist.MylistGroup;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Mylist
{
    public static class MylistJsonSerializeHelper
    {
		public static List<MylistGroupData> ParseMylistGroupListJson(string json)
		{
			dynamic parsedJson = JsonConvert.DeserializeObject(json);

			return MylistGroupData.ListCreateFromJson(parsedJson);
		}

		public static MylistGroupData ParseMylistGroupJson(string json)
		{
			dynamic parsedJson = JsonConvert.DeserializeObject(json);

			return MylistGroupData.CreateFromJson(parsedJson);
		}

		public static List<MylistData> ParseMylistListJson(string json)
		{
			dynamic parsedJson = JsonConvert.DeserializeObject(json);

			return MylistData.ParseMylistDataListFromJson(parsedJson);
		}

		public static MylistData ParseMylistJson(string json)
		{
			dynamic parsedJson = JsonConvert.DeserializeObject(json);

			return MylistData.ParseMylistDataEntry(parsedJson);
		}

		public static ContentManageResult ParseMylistApiResult(string json)
		{
			dynamic parsedJson = JsonConvert.DeserializeObject(json);

			if (parsedJson.error != null)
			{
				if (parsedJson.error.code == "EXIST")
				{
					return ContentManageResult.Exist;
				}
			}

			if (parsedJson.status != null)
			{
				if (parsedJson.status == "ok")
				{
					return ContentManageResult.Success;
				}
			}

			return ContentManageResult.Failed;
		}
	}
}
