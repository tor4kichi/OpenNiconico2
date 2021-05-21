using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Mylist.MylistGroup
{
	[DataContract]
	public class MylistVideoInfo : Searches.Video.VideoInfo
	{
		[DataMember(Name = "mylist")]
		public Mylist Mylist { get; set; }
	}

	[DataContract]
	public class MylistGroupVideoResponse
	{

		[DataMember(Name = "count")]
		public string __count { get; set; }

		public uint GetCount() => uint.Parse(__count);


		
		[DataMember(Name = "total_count")]
		public string __total_count { get; private set; }

		public uint GetTotalCount() => uint.Parse(__total_count);


		[DataMember(Name = "video_info")]
		[JsonConverter(typeof(SingleOrArrayConverter<MylistVideoInfo>))]
		public IList<MylistVideoInfo> MylistVideoInfoItems { get; set; }


		[DataMember(Name = "@status")]
		public string status { get; set; }

		public bool IsOK => status == "ok";
	}

	[DataContract]
	public class MylistGroupVideoResponseContainer
	{

		[DataMember(Name = "niconico_response")]
		public MylistGroupVideoResponse niconico_response { get; set; }
	}
}
