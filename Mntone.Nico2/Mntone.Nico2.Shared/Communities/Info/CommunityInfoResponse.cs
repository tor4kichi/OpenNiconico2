using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Communities.Info
{

    [DataContract]
    public class Option
    {

        [DataMember(Name = "adult_flag")]
        public string AdultFlag { get; set; }

        [DataMember(Name = "allow_display_vast")]
        public string AllowDisplayVast { get; set; }
    }

    [DataContract]
	public class OptionFlagDetails
	{

		[DataMember(Name = "community_priv_user_auth")]
		public string CommunityPrivUserAuth { get; set; }

		[DataMember(Name = "community_icon_upload")]
		public string CommunityIconUpload { get; set; }
	}


	[DataContract]
	public class CommunityInfo
	{

		[DataMember(Name = "id")]
		public string Id { get; private set; }

		[DataMember(Name = "name")]
		public string Name { get; private set; }

		[DataMember(Name = "description")]
		public string Description { get; private set; }

		[DataMember(Name = "channel_id")]
		public string ChannelId { get; private set; }

		[DataMember(Name = "public")]
		public string __IsPublic { get; private set; }

		private bool? _IsPublic;
		public bool IsPublic => _IsPublic.HasValue ? _IsPublic.Value : (_IsPublic = __IsPublic.ToBooleanFrom1()).Value;


		[DataMember(Name = "type")]
		public string Type { get; private set; }

		[DataMember(Name = "official")]
		public string __IsOfficial { get; private set; }

		private bool? _IsOfficial;
		public bool IsOfficial => _IsOfficial.HasValue ? _IsOfficial.Value : (_IsOfficial = __IsOfficial.ToBooleanFrom1()).Value;


		[DataMember(Name = "option_flag")]
		public string OptionFlag { get; private set; }

		[DataMember(Name = "hidden")]
		public string __IsHidden { get; private set; }

		private bool? _IsHidden;
		public bool IsHidden => _IsHidden.HasValue ? _IsHidden.Value : (_IsHidden = __IsHidden.ToBooleanFrom1()).Value;


		[DataMember(Name = "user_id")]
		public string UserId { get; private set; }

		[DataMember(Name = "create_time")]
		public string __CreateTime { get; private set; }


		private DateTime? _CreateTime;
		public DateTime CreateTime => _CreateTime.HasValue ? _CreateTime.Value : (_CreateTime = DateTime.Parse(__CreateTime)).Value;


		[DataMember(Name = "global_id")]
		public string GlobalId { get; private set; }

		[DataMember(Name = "user_max")]
		public string __UserMax { get; private set; }

		private uint? _UserMax;
		public uint UserMax => _UserMax.HasValue ? _UserMax.Value : (_UserMax = uint.Parse(__UserMax)).Value;



		[DataMember(Name = "user_count")]
		public string __UserCount { get; private set; }

		private uint? _UserCount;
		public uint UserCount => _UserCount.HasValue ? _UserCount.Value : (_UserCount = uint.Parse(__UserCount)).Value;


		[DataMember(Name = "level")]
		public string __Level { get; private set; }

		private uint? _Level;
		public uint Level => _Level.HasValue ? _Level.Value : (_Level = uint.Parse(__Level)).Value;


		[DataMember(Name = "option")]
		public Option Option { get; private set; }

		[DataMember(Name = "thumbnail")]
		public string Thumbnail { get; private set; }

		[DataMember(Name = "thumbnail_small")]
		public string ThumbnailSmall { get; private set; }

		[DataMember(Name = "option_flag_details")]
		public OptionFlagDetails option_flag_details { get; private set; }

		[DataMember(Name = "top_url")]
		public string TopUrl { get; private set; }

		[DataMember(Name = "@key")]
		public string @key { get; private set; }
	}

	[DataContract]
	public class NicovideoCommunityResponse
	{
		[DataMember(Name = "community")]
		public CommunityInfo Community { get; set; }

		[DataMember(Name = "@status")]
		public string @status { get; set; }

		public bool IsStatusOK => @status == "ok";
	}

	[DataContract]
	public class CommunityInfoResponseContainer
	{
		[DataMember(Name = "nicovideo_community_response")]
		public NicovideoCommunityResponse NicovideoCommunityResponse { get; set; }
	}

}
