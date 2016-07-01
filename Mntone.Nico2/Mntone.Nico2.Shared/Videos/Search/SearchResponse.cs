using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Videos.Search
{
	// this code generated with 
	// http://jsonutils.com/


	[DataContract]
	public class ThumbnailStyle
	{
		[DataMember(Name = "offset_x")]
		public int offset_x { get; set; }
		[DataMember(Name = "offset_y")]
		public int offset_y { get; set; }
		[DataMember(Name = "width")]
		public int width { get; set; }
	}

	[DataContract]
	public class ListItem
	{
		[DataMember(Name = "id")]
		public string id { get; set; }
		[DataMember(Name = "title")]
		public string title { get; set; }
		[DataMember(Name = "first_retrieve")]
		public string _first_retrieve { get; set; }
		[DataMember(Name = "view_counter")]
		public int view_counter { get; set; }
		[DataMember(Name = "mylist_counter")]
		public int mylist_counter { get; set; }
		[DataMember(Name = "thumbnail_url")]
		public string thumbnail_url { get; set; }
		[DataMember(Name = "num_res")]
		public int num_res { get; set; }
		[DataMember(Name = "last_res_body")]
		public string last_res_body { get; set; }
		[DataMember(Name = "length")]
		public string _length { get; set; }
		[DataMember(Name = "title_short")]
		public string title_short { get; set; }
		[DataMember(Name = "description_short")]
		public string description_short { get; set; }
		[DataMember(Name = "thumbnail_style")]
		public ThumbnailStyle thumbnail_style { get; set; }
		[DataMember(Name = "is_middle_thumbnail")]
		public bool is_middle_thumbnail { get; set; }

		/// <summary>
		/// 投稿日時を取得。
		/// </summary>
		/// <returns></returns>
		public DateTime FirstRetrieve { get; private set; }

		public TimeSpan Length { get; private set; }

		[OnDeserialized]
		private void SetValuesOnDeserialized(StreamingContext context)
		{
			FirstRetrieve = DateTime.Parse(_first_retrieve);

			var values = _length.Split(':').Reverse();
			var totalTime_Sec = 0;
			var q = 0;
			foreach (var t in values)
			{
				totalTime_Sec += int.Parse(t) * (q == 0 ? 1 : (q * 60));
				q++;
			}

			Length = TimeSpan.FromSeconds(totalTime_Sec);
		}

	}

	[DataContract]
	public class SearchResponse
	{
		[DataMember(Name = "list")]
		public IList<ListItem> list { get; set; }
		[DataMember(Name = "count")]
		public int count { get; set; }
		[DataMember(Name = "has_ng_video_for_adsense_on_listing")]
		public bool has_ng_video_for_adsense_on_listing { get; set; }
		[DataMember(Name = "related_tags")]
		public IList<string> related_tags { get; set; }
		[DataMember(Name = "page")]
		public int page { get; set; }
		[DataMember(Name = "status")]
		public string status { get; set; }

		public bool IsStatusOK
		{
			get { return status == "ok"; }
		}
	}

}
