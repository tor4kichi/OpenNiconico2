using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Mylist.MylistItem
{
  
	[DataContract]
	public class ItemData
	{
		[DataMember(Name = "video_id")]
		public string video_id { get; set; }
		[DataMember(Name = "title")]
		public string title { get; set; }
		[DataMember(Name = "thumbnail_url")]
		public string thumbnail_url { get; set; }
		[DataMember(Name = "first_retrieve")]
		public int first_retrieve { get; set; }
		[DataMember(Name = "update_time")]
		public int update_time { get; set; }
		[DataMember(Name = "view_counter")]
		public string view_counter { get; set; }
		[DataMember(Name = "mylist_counter")]
		public string mylist_counter { get; set; }
		[DataMember(Name = "num_res")]
		public string num_res { get; set; }
		[DataMember(Name = "group_type")]
		public string group_type { get; set; }
		[DataMember(Name = "length_seconds")]
		public string length_seconds { get; set; }
		[DataMember(Name = "deleted")]
		public string deleted { get; set; }
		[DataMember(Name = "last_res_body")]
		public string last_res_body { get; set; }
		[DataMember(Name = "watch_id")]
		public string watch_id { get; set; }
	}

	[DataContract]
	public class MylistItem
	{
		[DataMember(Name = "item_type")]
		public string item_type { get; set; }
		[DataMember(Name = "item_id")]
		public string item_id { get; set; }
		[DataMember(Name = "description")]
		public string description { get; set; }
		[DataMember(Name = "item_data")]
		public ItemData item_data { get; set; }
		[DataMember(Name = "watch")]
		public int watch { get; set; }
		[DataMember(Name = "create_time")]
		public int create_time { get; set; }
		[DataMember(Name = "update_time")]
		public int update_time { get; set; }
	}

	[DataContract]
	public class MylistItemResponse
	{
		[DataMember(Name = "mylistitem")]
		public List<MylistItem> mylistitem { get; set; }
		[DataMember(Name = "status")]
		public string status { get; set; }
	}
}
