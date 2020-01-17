using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Mylist
{
    [DataContract]
    public class MylistGroupData
    {
        public const string DeflistGroupId = "0";

        public bool IsDeflist()
        {
            return IsDeflist(Id);
        }


        public static bool IsDeflist(string group_id)
        {
            return group_id == null || group_id == DeflistGroupId;
        }

        public MylistGroupData() { }

        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "user_id")]
        public string UserId { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "public")]
        public string IsPublic { get; set; }
        [DataMember(Name = "icon_id")]
        public string IconId { get; set; }

        public List<Uri> ThumbnailUrls { get; set; }

        public int Count { get; set; }


        public bool GetIsPublic()
        {
            return IsPublic.ToBooleanFrom1();
        }

        public IconType GetIconType()
        {
            return (IconType)int.Parse(IconId);
        }
    }

    [DataContract]
	public class LoginUserMylistGroupData
	{
		public const string DeflistGroupId = "0";


		public LoginUserMylistGroupData() { }

		


		public bool IsDeflist()
		{
			return IsDeflist(Id);
		}


		public static bool IsDeflist(string group_id)
		{
			return group_id == null || group_id == DeflistGroupId;
		}

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "user_id")]
        public string UserId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "public")]
        public string Public { get; set; }

        [DataMember(Name = "default_sort")]
        public string DefaultSort { get; set; }

        [DataMember(Name = "create_time")]
        public int CreateTime { get; set; }

        [DataMember(Name = "update_time")]
        public int UpdateTime { get; set; }

        [DataMember(Name = "sort_order")]
        public string SortOrder { get; set; }

        [DataMember(Name = "icon_id")]
        public string IconId { get; set; }

        [DataMember(Name = "watch_playlist")]
        public string WatchPlaylist { get; set; }

        public bool GetIsPublic()
		{
			return Public.ToBooleanFrom1();
		}

		public IconType GetIconType()
		{
			return (IconType)int.Parse(IconId);
		}

        public MylistDefaultSort GetDefaultSort()
        {
            return (MylistDefaultSort)int.Parse(DefaultSort);
        }
    }

	[DataContract]
	public class LoginUserMylistGroup : MylistGroupData
	{
		
		[DataMember(Name = "default_sort")]
		public string DefaultSort { get; set; }
		[DataMember(Name = "create_time")]
		public long CreateTime { get; set; }
		[DataMember(Name = "update_time")]
		public long UpdateTime { get; set; }
		[DataMember(Name = "sort_order")]
		public string SortOrder { get; set; }
		

		public int ItemCount { get; set; } = 0;


		public MylistDefaultSort GetDefaultSort()
		{
			var index = int.Parse(DefaultSort);

			return (MylistDefaultSort)index;
		}
	}

	[DataContract]
	public class LoginUserMylistGroupResponse
	{
		[DataMember(Name = "mylistgroup")]
		public List<LoginUserMylistGroup> mylistgroup { get; set; }
		[DataMember(Name = "status")]
		public string status { get; set; }
	}




	
	
}
