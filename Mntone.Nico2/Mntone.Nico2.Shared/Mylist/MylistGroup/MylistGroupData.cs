using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mntone.Nico2.Mylist.MylistGroup
{
    public class MylistGroupData
    {
		public DateTime CreateTime { get; set; }

		public string Description { get; set; }

		public string Id { get; set; }

		public string Name { get; set; }

		public bool IsPublic { get; set; }

		public MylistDefaultSort MylistSort { get; set; }

		public IconType IconType { get; set; }

		public Uri ThumnailUrl { get; set; }



		public static bool IsDeflist(string group_id)
		{
			return group_id == null || group_id == "0";
		}

		public bool IsDeflist()
		{
			return IsDeflist(Id);
		}

		public static List<MylistGroupData> ListCreateFromJson(dynamic json)
		{
			var dataList = (IEnumerable<dynamic>)json.mylistgroup;
			return dataList.Select(CreateFromJson).ToList();
		}

		public static MylistGroupData CreateFromJson(dynamic entry)
		{
			var data = new MylistGroupData();

			data.CreateTime = DateTime.Parse(entry.create_time);
			data.Description = Uri.UnescapeDataString(entry.description);

			data.Id = entry.id;
			data.Name = Uri.UnescapeDataString(entry.name);
			data.IsPublic = entry.@public == "0" ? false : true;
			data.MylistSort = (MylistDefaultSort)int.Parse(entry.sort_order);
			data.IconType = (IconType)int.Parse(entry.icon_id);

			return data;
		}
    }
}
