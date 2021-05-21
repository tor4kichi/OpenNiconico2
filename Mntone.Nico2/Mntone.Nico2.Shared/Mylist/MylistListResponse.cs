using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Mntone.Nico2.Mylist
{
	//[XmlRoot(ElementName = "options")]
	//public class Options
	//{
	//	[XmlAttribute(AttributeName = "mobile")]
	//	public string Mobile { get; set; }
	//	[XmlAttribute(AttributeName = "sun")]
	//	public string Sun { get; set; }
	//	[XmlAttribute(AttributeName = "large_thumbnail")]
	//	public string Large_thumbnail { get; set; }
	//}

	//[XmlRoot(ElementName = "video")]
	//public class Video
	//{
	//	[XmlElement(ElementName = "id")]
	//	public string Id { get; set; }
	//	[XmlElement(ElementName = "deleted")]
	//	public string Deleted { get; set; }
	//	[XmlElement(ElementName = "title")]
	//	public string Title { get; set; }
	//	[XmlElement(ElementName = "length_in_seconds")]
	//	public string Length_in_seconds { get; set; }
	//	[XmlElement(ElementName = "thumbnail_url")]
	//	public string Thumbnail_url { get; set; }
	//	[XmlElement(ElementName = "upload_time")]
	//	public string Upload_time { get; set; }
	//	[XmlElement(ElementName = "first_retrieve")]
	//	public string First_retrieve { get; set; }
	//	[XmlElement(ElementName = "view_counter")]
	//	public string View_counter { get; set; }
	//	[XmlElement(ElementName = "mylist_counter")]
	//	public string Mylist_counter { get; set; }
	//	[XmlElement(ElementName = "option_flag_community")]
	//	public string Option_flag_community { get; set; }
	//	[XmlElement(ElementName = "option_flag_nicowari")]
	//	public string Option_flag_nicowari { get; set; }
	//	[XmlElement(ElementName = "option_flag_middle_thumbnail")]
	//	public string Option_flag_middle_thumbnail { get; set; }
	//	[XmlElement(ElementName = "width")]
	//	public string Width { get; set; }
	//	[XmlElement(ElementName = "height")]
	//	public string Height { get; set; }
	//	[XmlElement(ElementName = "vita_playable")]
	//	public string Vita_playable { get; set; }
	//	[XmlElement(ElementName = "ppv_video")]
	//	public string Ppv_video { get; set; }
	//	[XmlElement(ElementName = "provider_type")]
	//	public string Provider_type { get; set; }
	//	[XmlElement(ElementName = "options")]
	//	public Options Options { get; set; }
	//}

	//[XmlRoot(ElementName = "thread")]
	//public class Thread
	//{
	//	[XmlElement(ElementName = "id")]
	//	public string Id { get; set; }
	//	[XmlElement(ElementName = "num_res")]
	//	public string Num_res { get; set; }
	//	[XmlElement(ElementName = "summary")]
	//	public string Summary { get; set; }
	//}

	[XmlRoot(ElementName = "mylist")]
	public class Mylist
	{
		[XmlElement(ElementName = "item_id")]
		public string Item_id { get; set; }
		[XmlElement(ElementName = "description")]
		public string Description { get; set; }
		[XmlElement(ElementName = "update_time")]
		public string Update_time { get; set; }
		[XmlElement(ElementName = "create_time")]
		public string Create_time { get; set; }
	}

	[XmlRoot(ElementName = "video_info")]
	public class Video_info
	{
		[XmlElement(ElementName = "video")]
		public Video Video { get; set; }
		[XmlElement(ElementName = "thread")]
		public Thread Thread { get; set; }
		[XmlElement(ElementName = "mylist")]
		public Mylist Mylist { get; set; }
	}

	[XmlRoot(ElementName = "niconico_response")]
	public class NicoVideoResponse
	{
		[XmlElement(ElementName = "count")]
		public string Count { get; set; }
		[XmlElement(ElementName = "video_info")]
		public List<Video_info> Video_info { get; set; }
		[XmlElement(ElementName = "total_count")]
		public string Total_count { get; set; }
		[XmlAttribute(AttributeName = "status")]
		public string Status { get; set; }
	}
}
