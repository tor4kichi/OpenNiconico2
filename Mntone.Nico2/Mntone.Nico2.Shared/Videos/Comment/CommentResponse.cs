using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

/* 
   Licensed under the Apache License, Version 2.0

   http://www.apache.org/licenses/LICENSE-2.0
   */

namespace Mntone.Nico2.Videos.Comment
{
	[XmlRoot(ElementName = "thread")]
	public class CommentThread
	{
		/// <summary>
		/// 最後に投稿されたコメント番号（＝コメント数）
		/// </summary>
		[XmlAttribute(AttributeName = "last_res")]
		public string CommentCount { get; set; }


		[XmlAttribute(AttributeName = "resultcode")]
		public string Resultcode { get; set; }
		[XmlAttribute(AttributeName = "revision")]
		public string Revision { get; set; }
		[XmlAttribute(AttributeName = "server_time")]
		public string Server_time { get; set; }
		[XmlAttribute(AttributeName = "thread")]
		public string _thread { get; set; }
		[XmlAttribute(AttributeName = "ticket")]
		public string Ticket { get; set; }


		
	}

	[XmlRoot(ElementName = "view_counter")]
	public class ViewCounter
	{
		[XmlAttribute(AttributeName = "id")]
		public string Id { get; set; }
		[XmlAttribute(AttributeName = "mylist")]
		public string Mylist { get; set; }
		[XmlAttribute(AttributeName = "video")]
		public string Video { get; set; }


		
	}

	[XmlRoot(ElementName = "chat")]
	public class Chat
	{
		/// <summary>
		/// 匿名コメント
		/// </summary>
		[XmlAttribute(AttributeName = "anonymity")]
		public string Anonymity { get; set; }
		[XmlAttribute(AttributeName = "date")]
		public string Date { get; set; }

		/// <summary>
		/// コメントのコマンド（184など）
		/// </summary>
		[XmlAttribute(AttributeName = "mail")]
		public string Mail { get; set; }

		/// <summary>
		/// コメント番号
		/// </summary>
		[XmlAttribute(AttributeName = "no")]
		public string No { get; set; }
		[XmlAttribute(AttributeName = "thread")]
		public string Thread { get; set; }
		[XmlAttribute(AttributeName = "user_id")]
		public string UserId { get; set; }


		/// <summary>
		/// コメント書き込み再生位置（1/100秒）
		/// </summary>
		[XmlAttribute(AttributeName = "vpos")]
		public string Vpos { get; set; }
		[XmlText]
		public string Text { get; set; }


		public string GetDecodedText()
		{
			var bytes = Text.Select(x => Convert.ToByte(x)).ToArray();
			return Encoding.UTF8.GetString(bytes);
		}

		public bool GetAnonymity()
		{
			return Anonymity == "1";
		}

		public uint GetCommentNo()
		{
			return uint.Parse(No);
		}

		public int GetVpos()
		{
			return int.Parse(Vpos);
		}


        public List<CommandType> ParseCommandTypes()
        {
            return CommandTypesHelper.ParseCommentCommandTypes(this.Mail);
        }

        
	}

	[XmlRoot(ElementName = "packet")]
	public class CommentResponse
	{
		public static CommentResponse CreateFromXml(string xmlText)
		{
			var serializer = new XmlSerializer(typeof(CommentResponse));
			
			return (CommentResponse) serializer.Deserialize(new StringReader(xmlText));
		}



		[XmlElement(ElementName = "thread")]
		public CommentThread Thread { get; set; }
		[XmlElement(ElementName = "view_counter")]
		public ViewCounter View_counter { get; set; }
		[XmlElement(ElementName = "chat")]
		public List<Chat> Chat { get; set; }



		public uint GetCommentCount()
		{
			return Thread.CommentCount != null ? uint.Parse(Thread.CommentCount) : 0;
		}

		public uint GetMylistCount()
		{
			return uint.Parse(View_counter.Mylist);
		}

		public uint GetViewCount()
		{
			return uint.Parse(View_counter.Video);
		}
	}

}

