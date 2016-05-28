using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Mntone.Nico2.Users.NG
{
	[XmlRoot(ElementName = "ngclient")]
	public class NGCommentItem
	{
		[XmlElement(ElementName = "type")]
		public string TypeRaw { get; set; }
		[XmlElement(ElementName = "source")]
		public string Source { get; set; }
		[XmlElement(ElementName = "register_time")]
		public string Register_time { get; set; }

		private NGCommentType? _NGType;
		public NGCommentType NGType
		{
			get
			{
				return (_NGType ?? (_NGType = (NGCommentType)Enum.Parse(typeof(NGCommentType), TypeRaw))).Value;
			}
		}

		private DateTime? _RegisterTime;
		public DateTime RegisterTime
		{
			get
			{
				return (_RegisterTime ?? (_RegisterTime = new DateTime(uint.Parse(Register_time)))).Value;
			}
		}
	}

	[XmlRoot(ElementName = "response_ngclient")]
	public class NGCommentResponseCore
	{
		[XmlAttribute(AttributeName = "status")]
		public string StatusRaw { get; set; }

		[XmlElement(ElementName = "revision")]
		public string RevisionRaw { get; set; }

		private bool? _IsStatusOK;
		public bool IsStatusOK { get { return (_IsStatusOK ?? (_IsStatusOK = StatusRaw == "ok")).Value; } }

		private uint? _Revision;
		public uint Revision
		{
			get
			{
				return (_Revision ?? (_Revision = uint.Parse(RevisionRaw))).Value;
			}
		}
	}

	[XmlRoot(ElementName = "response_ngclient")]
	public class NGCommentResponse : NGCommentResponseCore
	{
		[XmlElement(ElementName = "count")]
		public string CountRaw { get; set; }
		[XmlElement(ElementName = "ngclient")]
		public List<NGCommentItem> NGCommentItems { get; set; }
		
		

		private uint? _Count;
		public uint Count
		{
			get
			{
				return (_Count ?? (_Count = uint.Parse(CountRaw))).Value;
			}
		}
	}


	public enum NGCommentType
	{
		id,
		word,
		command,
	}
}
