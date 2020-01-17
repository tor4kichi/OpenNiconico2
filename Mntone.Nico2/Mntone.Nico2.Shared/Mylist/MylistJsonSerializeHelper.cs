
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

		
		public static List<MylistData> ParseMylistItemResponse(string json)
		{
			var res = JsonConvert.DeserializeObject<Users.MylistItem.MylistItemResponse>(json);

			if (res.status == "ok")
			{
				return res.mylistitem.Select(x =>
				{
					return new MylistData()
					{
						Title = x.item_data.title,
						Description = x.description,
						ItemId = x.item_id,
						WatchId = x.item_data.watch_id,
						ItemType = (NiconicoItemType)int.Parse(x.item_type),
						FirstRetrieve = DateTimeOffset.FromUnixTimeSeconds(x.item_data.first_retrieve).DateTime,
						ViewCount = x.item_data.view_counter != null ? uint.Parse(x.item_data.view_counter) : 0,
						CommentCount = x.item_data.num_res != null ? uint.Parse(x.item_data.num_res) : 0,
						MylistCount = x.item_data.mylist_counter != null ? uint.Parse(x.item_data.mylist_counter) : 0,
						CreateTime = DateTimeOffset.FromUnixTimeSeconds(x.create_time).DateTime,
						UpdateTime = DateTimeOffset.FromUnixTimeSeconds(x.update_time).DateTime,
						IsDeleted = x.item_data.deleted.ToBooleanFrom1(),
						Length = x.item_data.length_seconds != null ? TimeSpan.FromSeconds(int.Parse(x.item_data.length_seconds)) : TimeSpan.Zero,
						ThumbnailUrl = x.item_data.thumbnail_url != null ? new Uri(x.item_data.thumbnail_url) : null,
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
