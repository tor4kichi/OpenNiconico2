
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;

namespace Mntone.Nico2.Mylist
{

    public class MylistData
    {
		public NiconicoItemType ItemType { get; set; }
		public string ItemId { get; set; }
		public DateTime CreateTime { get; set; }
		public DateTime UpdateTime { get; set; }
		public string Description { get; set; }
		public DateTime FirstRetrieve { get; set; }

		public TimeSpan Length { get; set; }
		public string WatchId { get; set; }
		public string GroupId { get; set; }
		public string Title { get; set; }
		public uint ViewCount { get; set; }
		public uint CommentCount { get; set; }
		public uint MylistCount { get; set; }
		public Uri ThumbnailUrl { get; set; }
		public bool IsDeleted { get; set; }


		public static List<MylistData> ParseMylistDataListFromJson(dynamic json)
		{
			var items = (IEnumerable<dynamic>)json.mylistitem;
			return items.Select(x => (MylistData)ParseMylistDataEntry(x)).ToList();
		}

		public static MylistData ParseMylistDataEntry(dynamic json)
		{
			var data = new MylistData();

			data.CreateTime = DateTime.Parse(json.create_time as string);
			data.Description = json.description;
			data.ItemId = json.item_id;

			var item = json.item_data;
			data.Title = Uri.UnescapeDataString(item.title);

			if (json.item_type is string)
			{
				data.ItemType = (NiconicoItemType)int.Parse(json.item_type);
			}
			else if (json.item_type is double)
			{
				data.ItemType = (NiconicoItemType)(int)json.item_type;
			}
			else
			{
				throw new NotSupportedException("not support MylistItemType:" + json.item_type);
			}


			switch (data.ItemType)
			{
				case NiconicoItemType.Video:
					data.WatchId = json.watch_id;
					data.UpdateTime = DateTime.Parse(json.update_time);
					data.FirstRetrieve = DateTime.Parse(json.first_retrieve);
					data.Length = TimeSpan.FromSeconds(long.Parse(json.length_seconds));
					data.GroupId = item.video_id;
					data.ViewCount = int.Parse(item.view_counter);
					data.CommentCount = int.Parse(item.num_res);
					data.MylistCount = int.Parse(item.mylist_counter);
					data.ThumbnailUrl = new Uri(item.thumbnail_url);
					data.IsDeleted = ((string)json.deleted).ToBooleanFrom1();

					break;
				case NiconicoItemType.Seiga:
					data.UpdateTime = DateTime.Parse(json.update_time);
					data.FirstRetrieve = DateTime.Parse(json.first_retrieve);
					data.GroupId = item.id.ToString();
					data.ViewCount = int.Parse(item.view_counter);
					data.CommentCount = int.Parse(item.num_res);
					data.MylistCount = int.Parse(item.mylist_counter);
					data.ThumbnailUrl = new Uri(item.thumbnail_url);

					break;
				case NiconicoItemType.Book:
					data.UpdateTime = DateTime.Parse(json.update_time);
					data.FirstRetrieve = DateTime.Parse(json.first_retrieve);
					data.GroupId = "bk" + item.id;
					data.CommentCount = int.Parse(item.num_res);
					data.MylistCount = int.Parse(item.mylist_counter);
					data.ThumbnailUrl = new Uri(item.thumbnail_url);

					break;
				case NiconicoItemType.Blomaga:
					data.UpdateTime = DateTime.Parse(json.update_time);
					data.FirstRetrieve = DateTime.Parse(json.first_retrieve);
					data.GroupId = "ar" + item.id;
					data.CommentCount = int.Parse(item.num_res);
					data.MylistCount = int.Parse(item.mylist_counter);
					data.ThumbnailUrl = new Uri(item.thumbnail_url);

					break;
				default:
					throw new NotSupportedException();
			}

			return data;
		}
	}
}
