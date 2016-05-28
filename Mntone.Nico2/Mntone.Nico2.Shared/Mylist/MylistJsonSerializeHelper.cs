using Mntone.Nico2.Mylist.MylistGroup;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mntone.Nico2.Mylist
{
    public static class MylistJsonSerializeHelper
    {
		public static List<MylistGroupData> ParseMylistGroupListJson(string json)
		{
			var parsedJson = JsonConvert.DeserializeObject< LoginUserMylistGroupData>(json);

			return parsedJson.mylistgroup
				.Select(x => new MylistGroupData(x))
				.ToList();
		}

		
		public static List<MylistData> ParseMylistItemResponse(string json)
		{
			var res = JsonConvert.DeserializeObject<MylistItem.MylistItemResponse>(json);

			if (res.status == "ok")
			{
				return res.mylistitem.Select(x =>
				{
					return new MylistData()
					{
						Title = x.item_data.title,
						Description = x.description,
						ItemId = x.item_data.video_id,
						ItemType = (NiconicoItemType)int.Parse(x.item_type),
						FirstRetrieve = new DateTime(x.item_data.first_retrieve),
						ViewCount = uint.Parse(x.item_data.view_counter),
						CommentCount = uint.Parse(x.item_data.num_res),
						MylistCount = uint.Parse(x.item_data.mylist_counter),
						CreateTime = new DateTime(x.create_time),
						UpdateTime = new DateTime(x.update_time),
						IsDeleted = x.item_data.deleted.ToBooleanFrom1(),
						Length = TimeSpan.FromSeconds(int.Parse(x.item_data.length_seconds)),
						ThumbnailUrl = new Uri(x.item_data.thumbnail_url),
					};
				})
				.ToList();
			}
			else
			{
				return null;
			}
		}

		

		
	}
}
