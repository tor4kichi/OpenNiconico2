/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */

 using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Mntone.Nico2.Videos.Comment
{

	[XmlRoot(ElementName = "thread")]
	public class CommentPostThread
	{
		public CommentPostThread() { }


		public CommentPostThread(string threadId, string threadKey, string userId, string force184, bool requestOwnerComment = false)
		{
			_thread = threadId;
			ThreadKey = threadKey;
//			User_id = userId;
//			Force184 = force184;
//			RequestOwnerComment = requestOwnerComment.ToString1Or0();
		}

		[XmlAttribute(AttributeName = "thread")]
		public string _thread { get; set; }
		[XmlAttribute(AttributeName = "threadkey")]
		public string ThreadKey { get; set; }
		[XmlAttribute(AttributeName = "version")]
		public string Version { get; set; }
		[XmlAttribute(AttributeName = "scores")]
		public string Scores { get; set; }
		[XmlAttribute(AttributeName = "nicoru")]
		public string Nicoru { get; set; }
		[XmlAttribute(AttributeName = "language")]
		public string Language { get; set; }
		[XmlAttribute(AttributeName = "with_global")]
		public string With_global { get; set; }
		[XmlAttribute(AttributeName = "res_from")]
		public string Res_from { get; set; }
		[XmlAttribute(AttributeName = "fork")]
		public string Fork { get; set; }

	}

	[XmlRoot(ElementName = "thread_leaves")]
	public class Thread_leaves
	{
		public Thread_leaves() { }


		public Thread_leaves(string threadId, string threadKey, string userId, uint videoLengthMinute, string force184, bool requestScore = true)
		{
			Thread = threadId;
			ThreadKey = threadKey;
//			User_id = userId;
//			RequestScore = requestScore.ToString1Or0();
//			Force184 = force184;
			Text = MakeText(videoLengthMinute);
		}

		[XmlAttribute(AttributeName = "thread")]
		public string Thread { get; set; }
		[XmlAttribute(AttributeName = "threadkey")]
		public string ThreadKey { get; set; }
		[XmlAttribute(AttributeName = "scores")]
		public string Scores { get; set; }
		[XmlAttribute(AttributeName = "nicoru")]
		public string Nicoru { get; set; }
		[XmlAttribute(AttributeName = "language")]
		public string Language { get; set; }
		[XmlText]
		public string Text { get; set; }

		public static string MakeText(uint videoLengthMinute)
		{
			var len = videoLengthMinute + 1;
			var maxCommentCount = len * 100;
			return $"0-{len};100,{maxCommentCount}";
        }
	}

	[XmlRoot(ElementName = "packet")]
	public class CommentPostData
	{
		public CommentPostData()
		{

		}

		public CommentPostData(string threadId, string threadKey, string userId, uint videoLength, string force184, bool requestOwnerComment = false, bool requestScore = true)
		{
			FirstThread = new CommentPostThread()
			{
				_thread = threadId,
				ThreadKey = threadKey,
				Version = "20061206",
				Scores = "1",
				Nicoru = "0",

			};

			ThreadLeaves = new Thread_leaves()
			{
				Thread = threadId,
				Scores = "1",
				Nicoru = "0",
				Text = Thread_leaves.MakeText(videoLength)
			};

			SecondThread = new CommentPostThread()
			{
				_thread = threadId,
				ThreadKey = threadKey,
				Version = "20061206",
				Res_from = "-1000",
				Fork = "1"
			};


		}

		[XmlElement(ElementName = "thread")]
		public CommentPostThread FirstThread { get; set; }
		[XmlElement(ElementName = "thread_leaves")]
		public Thread_leaves ThreadLeaves { get; set; }
		[XmlElement(ElementName = "thread")]
		public CommentPostThread SecondThread { get; set; }



		public string ToXml()
		{
			//メモリストリームに一旦書き出す
			using (var stream = new System.IO.MemoryStream())
			using (var writer = new System.IO.StreamWriter(stream, System.Text.Encoding.UTF8))
			{
				System.Xml.Serialization.XmlSerializerNamespaces ns = new System.Xml.Serialization.XmlSerializerNamespaces();
				ns.Add(String.Empty, String.Empty);

				//シリアライズ
				System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(CommentPostData));
				serializer.Serialize(writer, this, ns);
				writer.Flush();
				return Encoding.UTF8.GetString(stream.ToArray());
			}
		}
	}

}