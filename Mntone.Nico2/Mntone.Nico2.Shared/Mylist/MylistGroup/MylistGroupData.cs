using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Mylist.MylistGroup
{


	[DataContract]
	public class LoginUserMylistGroup
	{
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
		[DataMember(Name = "default_sort")]
		public string DefaultSort { get; set; }
		[DataMember(Name = "create_time")]
		public long CreateTime { get; set; }
		[DataMember(Name = "update_time")]
		public long UpdateTime { get; set; }
		[DataMember(Name = "sort_order")]
		public string SortOrder { get; set; }
		[DataMember(Name = "icon_id")]
		public string IconId { get; set; }
	}

	[DataContract]
	public class LoginUserMylistGroupData
	{
		[DataMember(Name = "mylistgroup")]
		public List<LoginUserMylistGroup> mylistgroup { get; set; }
		[DataMember(Name = "status")]
		public string status { get; set; }
	}




	public class MylistGroupData
	{
		public MylistGroupData() { }

		public MylistGroupData(LoginUserMylistGroup raw)
		{
			Id = raw.Id;
			UserId = uint.Parse(raw.UserId);
			Name = raw.Name;
			Description = raw.Description;
			IsPublic = raw.IsPublic == "0" ? false : true;
			DefaultSort = (MylistDefaultSort)int.Parse(raw.DefaultSort);
			CreateTime = new DateTime(raw.CreateTime);
			UpdateTime = new DateTime(raw.UpdateTime);
//			SortOrder = (SortMethod)int.Parse(raw.SortOrder);
			IconId = (IconType)int.Parse(raw.IconId);
		}


		public bool IsDeflist()
		{
			return IsDeflist(Id);
		}


		public static bool IsDeflist(string group_id)
		{
			return group_id == null || group_id == "0";
		}


		public string Id { get; set; }
		public uint UserId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsPublic { get; set; }
		public MylistDefaultSort DefaultSort { get; set; }
		public DateTime CreateTime { get; set; }
		public DateTime UpdateTime { get; set; }
//		public SortMethod SortOrder { get; set; }
		public IconType IconId { get; set; }
		public Uri ThumbnailUrl { get; set; }
	}
	
}
