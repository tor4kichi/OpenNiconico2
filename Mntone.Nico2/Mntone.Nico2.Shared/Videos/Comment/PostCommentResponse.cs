using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

// convert tool using from http://xmltocsharp.azurewebsites.net/

/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */


namespace Mntone.Nico2.Videos.Comment
{
	
	[XmlRoot(ElementName = "chat_result")]
	public class Chat_result
	{
		[XmlAttribute(AttributeName = "thread")]
		public string Thread { get; set; }
		[XmlAttribute(AttributeName = "status")]
		public string __Status { get; set; }
		[XmlAttribute(AttributeName = "no")]
		public string __No { get; set; }

		private ChatResult? _Status;
		public ChatResult Status { get { return (_Status ?? (_Status = (ChatResult)Enum.Parse(typeof(ChatResult), __Status))).Value; } }

		private int? _No;
		public int No { get { return (_No ?? (_No = int.Parse(__No))).Value; } }
	}

	[XmlRoot(ElementName = "packet")]
	public class PostCommentResponse
	{
		[XmlElement(ElementName = "chat_result")]
		public Chat_result Chat_result { get; set; }
	}

}

	

