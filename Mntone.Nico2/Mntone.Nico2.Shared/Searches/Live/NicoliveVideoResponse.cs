using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Searches.Live
{
	
	

	[DataContract]
	public class VideoInfo
	{

		[DataMember(Name = "video")]
		public Communities.Live.LiveVideo Video { get; set; }

		[DataMember(Name = "community")]
		public Communities.Live.Community Community { get; set; }
	}

	[DataContract]
	public class TotalCount
	{

		[DataMember(Name = "onair")]
		public string __OnairCount { get; set; }

		private int? _OnairCount;
		public int OnairCount => _OnairCount.HasValue ?
			_OnairCount.Value :
			(_OnairCount = int.Parse(__OnairCount)).Value;


		[DataMember(Name = "closed")]
		public string __ClosedCount { get; set; }

		private int? _ClosedCount;
		public int ClosedCount => _ClosedCount.HasValue ?
			_ClosedCount.Value :
			(_ClosedCount = int.Parse(__ClosedCount)).Value;


		[DataMember(Name = "reserved")]
		public string __ReservedCount { get; set; }


		private int? _ReservedCount;
		public int ReservedCount => _ReservedCount.HasValue ?
			_ReservedCount.Value :
			(_ReservedCount = int.Parse(__ReservedCount)).Value;


		[DataMember(Name = "filtered")]
		public string __FilteredCount { get; set; }


		private int? _FilteredCount;
		public int FilteredCount => _FilteredCount.HasValue ?
			_FilteredCount.Value :
			(_FilteredCount = int.Parse(__FilteredCount)).Value;

	}

	[DataContract]
	public class Tag
	{

		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "count")]
		public string __Count { get; set; }

		private int? _Count;
		public int Count => _Count.HasValue ?
			_Count.Value :
			(_Count = int.Parse(__Count)).Value;

	}

	[DataContract]
	public class Tags
	{
		[DataMember(Name = "tag")]
		[JsonConverter(typeof(SingleOrArrayConverter<Tag>))]
		public IList<Tag> Tag { get; set; }
	}

	[DataContract]
	public class NicoliveVideoResponse
	{

		[DataMember(Name = "video_info")]
		[JsonConverter(typeof(SingleOrArrayConverter<VideoInfo>))]
		public IList<VideoInfo> VideoInfo { get; set; }

		[DataMember(Name = "count")]
		public string Count { get; set; }

		[DataMember(Name = "total_count")]
		public TotalCount TotalCount { get; set; }

		[DataMember(Name = "is_terminate")]
		public string __IsTerminate { get; set; }

		public bool IsTerminate => __IsTerminate.ToBooleanFrom1();

		[DataMember(Name = "tags")]
		public Tags Tags { get; set; }

		[DataMember(Name = "@status")]
		public string Status { get; set; }


		public bool IsStatusOK => Status == "ok";
	}

	[DataContract]
	public class NicoliveVideoResponseContainer
	{

		[DataMember(Name = "nicolive_video_response")]
		public NicoliveVideoResponse NicoliveVideoResponse { get; set; }
	}
}
