using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Mntone.Nico2.Users.User
{
	/* 
		Licensed under the Apache License, Version 2.0

		http://www.apache.org/licenses/LICENSE-2.0
	*/
	[XmlRoot(ElementName = "user")]
	public class User
	{
		[XmlElement(ElementName = "id")]
		public string Id { get; set; }
		[XmlElement(ElementName = "nickname")]
		public string Nickname { get; set; }
		[XmlElement(ElementName = "thumbnail_url")]
		public string ThumbnailUrl { get; set; }
	}

	[XmlRoot(ElementName = "vita_option")]
	public class Vita_option
	{
		[XmlElement(ElementName = "user_secret")]
		public string User_secret { get; set; }
	}

	[XmlRoot(ElementName = "niconico_response")]
	public class UserResponse
	{
		[XmlElement(ElementName = "user")]
		public User User { get; set; }
		[XmlElement(ElementName = "vita_option")]
		public Vita_option Vita_option { get; set; }
		[XmlElement(ElementName = "additionals")]
		public string Additionals { get; set; }
		[XmlAttribute(AttributeName = "status")]
		public string Status { get; set; }
	}

	

}
