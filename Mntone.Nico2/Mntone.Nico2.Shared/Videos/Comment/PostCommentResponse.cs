using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

// convert tool using from http://xmltocsharp.azurewebsites.net/

/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */


namespace Mntone.Nico2.Videos.Comment
{
	
	[XmlRoot(ElementName = "chat_result")]
    [DataContract(Name = "chat_result")]
	public class Chat_result
	{
		[XmlAttribute(AttributeName = "thread")]
        [DataMember(Name = "thread")]
        public string Thread { get; set; }

		[XmlAttribute(AttributeName = "status")]
        [DataMember(Name = "status")]
        public int __Status { get; set; }

        [XmlAttribute(AttributeName = "no")]
        [DataMember(Name = "no")]
        public string __No { get; set; }

		private ChatResult? _Status;
		public ChatResult Status { get { return (_Status ?? (_Status = (ChatResult)__Status)).Value; } }

        private int? _No;
		public int No { get { return (_No ?? (_No = int.Parse(__No))).Value; } }
	}

	[XmlRoot(ElementName = "packet")]
    [DataContract]
    public class PostCommentResponse
	{
		[XmlElement(ElementName = "chat_result")]
        [DataMember(Name = "chat_result")]
        public Chat_result Chat_result { get; set; }
	}

}

	

