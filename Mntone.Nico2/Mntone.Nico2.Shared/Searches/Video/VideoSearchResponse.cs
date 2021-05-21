using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Searches.Video
{
	[DataContract]
	public class Options
	{

		[DataMember(Name = "@mobile")]
		public string @mobile { get; private set; }

		[DataMember(Name = "@sun")]
		public string @sun { get; private set; }

		[DataMember(Name = "@large_thumbnail")]
		public string @large_thumbnail { get; private set; }

        [DataMember(Name = "@adult")]
        public string Adult { get; set; }
    }

	[DataContract]
	public class Video
	{
		[DataMember(Name = "id")]
		public string __id { get; private set; }

		public string Id => __id;

        [DataMember(Name = "user_id")]
        public string UserId { get; set; }

        [DataMember(Name = "deleted")]
		public string __deleted { get; private set; }

		private bool? _IsDeleted;
		public bool IsDeleted => _IsDeleted.HasValue ? _IsDeleted.Value : (_IsDeleted = int.Parse(__deleted) > 0).Value;

		[DataMember(Name = "title")]
		public string __title { get; private set; }

		private string _Title;
		public string Title => _Title ?? (_Title = __title);


        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "length_in_seconds")]
		public string __length_in_seconds { get; private set; }

		private TimeSpan? _Length;
		public TimeSpan Length => _Length.HasValue ? _Length.Value : (_Length = TimeSpan.FromSeconds(int.Parse(__length_in_seconds))).Value;

		[DataMember(Name = "thumbnail_url")]
		public string __thumbnail_url { get; private set; }

		private Uri _ThumbnailUrl;
		public Uri ThumbnailUrl => _ThumbnailUrl ?? (_ThumbnailUrl = new Uri(__thumbnail_url));

		[DataMember(Name = "upload_time")]
		public DateTime __upload_time { get; private set; }

		public DateTime UploadTime => __upload_time;

		[DataMember(Name = "first_retrieve")]
		public DateTime __first_retrieve { get; private set; }

		public DateTime FirstRetrieve => __first_retrieve;

        [DataMember(Name = "default_thread")]
        public string DefaultThread { get; set; }


        [DataMember(Name = "view_counter")]
		public string __view_counter { get; private set; }

		private uint? _ViewCount;
		public uint ViewCount => _ViewCount.HasValue ? _ViewCount.Value : (_ViewCount = uint.Parse(__view_counter)).Value;


		[DataMember(Name = "mylist_counter")]
		public string __mylist_counter { get; private set; }

		private uint? _MylistCount;
		public uint MylistCount => _MylistCount.HasValue ? _MylistCount.Value : (_MylistCount = uint.Parse(__mylist_counter)).Value;


		[DataMember(Name = "option_flag_community")]
		public string __option_flag_community { get; private set; }

		private bool? _IsCommunity;
		public bool IsCommunity => _IsCommunity.HasValue ? _IsCommunity.Value : (_IsCommunity = __option_flag_community.ToBooleanFrom1()).Value;


//		[DataMember(Name = "option_flag_nicowari")]
//		public string __option_flag_nicowari { get; set; }

//		[DataMember(Name = "option_flag_middle_thumbnail")]
//		public string __option_flag_middle_thumbnail { get; set; }

		[DataMember(Name = "width")]
		public string __width { get; private set; }

		private uint? _Width;
		public uint Width => _Width.HasValue ? _Width.Value : (_Width = uint.Parse(__width)).Value;



		[DataMember(Name = "height")]
		public string __height { get; private set; }

		private uint? _Height;
		public uint Height => _Height.HasValue ? _Height.Value : (_Height = uint.Parse(__height)).Value;


		[DataMember(Name = "vita_playable")]
		public string __vita_playable { get; private set; }

		private bool? _IsVitaPlayable;
		public bool IsVitaPlayable => _IsVitaPlayable.HasValue ? _IsVitaPlayable.Value : (_IsVitaPlayable = __vita_playable.ToBooleanFrom1()).Value;


		[DataMember(Name = "ppv_video")]
		public string __ppv_video { get; private set; }



		[DataMember(Name = "provider_type")]
		public string __provider_type { get; private set; }

		public string ProviderType => __provider_type;

		[DataMember(Name = "options")]
		public Options Options { get; private set; }

        [DataMember(Name = "community_id")]
        public string CommunityId { get; set; }
    }

	[DataContract]
	public class Thread
	{

		[DataMember(Name = "id")]
		public string Id { get; private set; }

		[DataMember(Name = "num_res")]
		public string num_res { get; private set; }

		public uint GetCommentCount() => uint.Parse(num_res);


		[DataMember(Name = "summary")]
		public string summary { get; private set; }

		public string GetDecodedSummary() => summary.DecodeUTF8();

        [DataMember(Name = "community_id")]
        public string CommunityId { get; set; }

        [DataMember(Name = "group_type")]
        public string GroupType { get; set; }
    }

	[DataContract]
	public class VideoInfo
	{

		[DataMember(Name = "video")]
		public Video Video { get; private set; }

		[DataMember(Name = "thread")]
		public Thread Thread { get; private set; }
	}

	[DataContract]
	public class Tag
	{

		[DataMember(Name = "name")]
		public string Name { get; private set; }

		[DataMember(Name = "count")]
		public string __count { get; private set; }


		public uint GetCount() => uint.Parse(__count);
	}

	[DataContract]
	public class Tags
	{

		[DataMember(Name = "tag")]
		[JsonConverter(typeof(SingleOrArrayConverter<Tag>))]
		public List<Tag> TagItems { get; set; }
	}

	[DataContract]
	public class VideoListingResponse
	{

		[DataMember(Name = "count")]
		public string __count { get; private set; }

		public uint GetCount() => uint.Parse(__count);

		[DataMember(Name = "video_info")]
		[JsonConverter(typeof(SingleOrArrayConverter<VideoInfo>))]
		public List<VideoInfo> VideoInfoItems { get; private set; }

		[DataMember(Name = "total_count")]
		public string __total_count { get; private set; }


		public uint GetTotalCount() => uint.Parse(__total_count);

		[DataMember(Name = "tags")]
		public Tags Tags { get; private set; }

		[DataMember(Name = "@status")]
		public string @status { get; private set; }


		public bool IsOK => @status == "ok";
	}

	[DataContract]
	public class VideoListingResponseContainer
	{

		[DataMember(Name = "niconico_response")]
		public VideoListingResponse niconico_response { get; set; }
	}

}
