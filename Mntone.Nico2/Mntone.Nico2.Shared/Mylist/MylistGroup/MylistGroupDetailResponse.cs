using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Mylist.MylistGroup
{
	[DataContract]
	public class Mylistgroup
	{

		[DataMember(Name = "id")]
		public string Id { get; private set; }

		[DataMember(Name = "user_id")]
		public string UserId { get; private set; }

		[DataMember(Name = "view_counter")]
		public string __view_counter { get; private set; }

		private uint? _ViewCount;
		public uint ViewCount => _ViewCount.HasValue ? _ViewCount.Value : (_ViewCount = uint.Parse(__view_counter)).Value;


		[DataMember(Name = "name")]
		public string Name { get; private set; }

		[DataMember(Name = "description")]
		public string __description { get; private set; }

		private string _Description;
		public string Description => _Description ?? (_Description = __description);


		[DataMember(Name = "public")]
		public string __isPublic { get; private set; }

		private bool? _IsPublic;
		public bool IsPublic => _IsPublic.HasValue ? _IsPublic.Value : (_IsPublic = __isPublic.ToBooleanFrom1()).Value;

		[DataMember(Name = "default_sort")]
		public string default_sort { get; private set; }

		public MylistDefaultSort GetMylistDefaultSort() => (MylistDefaultSort)int.Parse(default_sort);

		[DataMember(Name = "icon_id")]
		public string icon_id { get; private set; }

		public IconType GetIconType() => (IconType)int.Parse(icon_id);


		[DataMember(Name = "sort_order")]
		public string sort_order { get; private set; }

		public Order GetSortOrder() => (Order)int.Parse(sort_order);


		[DataMember(Name = "update_time")]
		public DateTime UpdateTime { get; private set; }

		[DataMember(Name = "create_time")]
		public DateTime CreateTime { get; private set; }

		[DataMember(Name = "count")]
		public string __count { get; private set; }

		private uint? _Count;
		public uint Count => _Count.HasValue ? _Count.Value : (_Count = uint.Parse(__count)).Value;


		[DataMember(Name = "default_sort_method")]
		public string default_sort_method { get; private set; }

		[DataMember(Name = "default_sort_order")]
		public string default_sort_order { get; private set; }

		[DataMember(Name = "video_info")]
		[JsonConverter(typeof(SingleOrArrayConverter<Searches.Video.VideoInfo>))]
		public IList<Searches.Video.VideoInfo> SampleVideoInfoItems { get; set; }
	}

	[DataContract]
	public class MylistGroupDetailResponse
	{

		[DataMember(Name = "mylistgroup")]
		public Mylistgroup MylistGroup { get; set; }

		[DataMember(Name = "@status")]
		public string status { get; set; }

		public bool IsOK => status == "ok";
	}

	[DataContract]
	public class MylistGroupDetailResponseContainer
	{

		[DataMember(Name = "nicovideo_mylistgroup_response")]
		public MylistGroupDetailResponse nicovideo_mylistgroup_response { get; set; }
	}

}
