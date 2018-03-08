using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Users.Follow
{
	public class FollowData
	{
		public NiconicoItemType ItemType { get; set; }
		public string ItemId { get; set; }
		public string Title { get; set; }
	}

    public class ChannelFollowData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int VideoCount { get; set; }
        public string ThumbnailUrl { get; set; }
    }

    #region WatchItem Response

    [DataContract]
	public class ItemData
	{
		[DataMember(Name = "id")]
		public string id { get; set; }
		[DataMember(Name = "nickname")]
		public string nickname { get; set; }
		[DataMember(Name = "thumbnail_url")]
		public string thumbnail_url { get; set; }
	}

	[DataContract]
	public class Watchitem
	{
		[DataMember(Name = "item_type")]
		public string item_type { get; set; }
		[DataMember(Name = "item_id")]
		public string item_id { get; set; }
		[DataMember(Name = "item_data")]
		public ItemData item_data { get; set; }
		[DataMember(Name = "create_time")]
		public int create_time { get; set; }
		[DataMember(Name = "update_time")]
		public int update_time { get; set; }
	}

	[DataContract]
	public class WatchItemResponse
	{
		[DataMember(Name = "watchitem")]
		public IList<Watchitem> watchitem { get; set; }
		[DataMember(Name = "total_count")]
		public int total_count { get; set; }
		[DataMember(Name = "status")]
		public string status { get; set; }
	}

	#endregion



	#region Fav Tag Response

	[DataContract]
	public class FollowTagItem
	{
		[DataMember(Name = "tag")]
		public string tag { get; set; }
	}

	[DataContract]
	public class FollowTagResponse
	{
		[DataMember(Name = "favtag_items")]
		public IList<FollowTagItem> favtag_items { get; set; }
		[DataMember(Name = "user_id")]
		public int user_id { get; set; }
		[DataMember(Name = "status")]
		public string status { get; set; }
	}

	#endregion

}
