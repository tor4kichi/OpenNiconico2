using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Searches.Mylist
{
	[DataContract]
	public class MylistGroup
	{

		[DataMember(Name = "id")]
		public string Id { get; private set; }

		[DataMember(Name = "name")]
		public string __name { get; private set; }

		private string _Name;
		public string Name => _Name ?? (_Name = __name);


		[DataMember(Name = "description")]
		public string __description { get; private set; }

		private string _Description;
		public string Description => _Description ?? (_Description = __description);

	
		[DataMember(Name = "thread_ids")]
		public string __thread_ids { get; private set; }

		[DataMember(Name = "item")]
		public string __itemCount { get; private set; }

		private uint? _ItemCount;
		public uint ItemCount => _ItemCount.HasValue ? _ItemCount.Value : (_ItemCount = uint.Parse(__itemCount)).Value;



		[DataMember(Name = "update_time")]
		public string __update_time { get; private set; }

		private DateTime? _UpdateTime;
		public DateTime UpdateTime => _UpdateTime.HasValue ? _UpdateTime.Value : (_UpdateTime = DateTime.Parse(__update_time)).Value;


		[DataMember(Name = "video_info")]
		[JsonConverter(typeof(SingleOrArrayConverter<Video.VideoInfo>))]
		public IList<Video.VideoInfo> VideoInfoItems { get; private set; }
	}

	[DataContract]
	public class MylistSearchResponse
	{

		[DataMember(Name = "total_count")]
		public string __total_count { get; private set; }

		public uint GetTotalCount() => uint.Parse(__total_count);

		[DataMember(Name = "data_count")]
		public string __data_count { get; private set; }

		public uint GetDataCount() => uint.Parse(__data_count);


		[DataMember(Name = "mylistgroup")]
		[JsonConverter(typeof(SingleOrArrayConverter<MylistGroup>))]
		public IList<MylistGroup> MylistGroupItems { get; private set; }

		[DataMember(Name = "@status")]
		public string status { get; private set; }


		public bool IsOK => status == "ok";
	}

	[DataContract]
	public class MylistSearchResponseContainer
	{
		[DataMember(Name = "nicovideo_mylist_response")]
		public MylistSearchResponse nicovideo_mylist_response { get; set; }
	}
}
