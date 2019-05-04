#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Runtime.Serialization;
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Videos.Thumbnail
{
	/// <summary>
	/// タグ情報
	/// </summary>
	[DataContract]
	public sealed class ThumbnailTag
	{
#if WINDOWS_APP
		internal Tag( IXmlNode tagXml )
#else
		internal ThumbnailTag( XElement tagXml )
#endif
		{
			Category = tagXml.Attribute( "category" ).Value.ToBooleanFrom1();
			Lock = tagXml.Attribute( "lock" ).Value.ToBooleanFrom1();
			Value = tagXml.Value;
		}

		// シリアライズのためにデフォルトコンストラクタを用意しておく
		public ThumbnailTag() { }

		/// <summary>
		/// カテゴリー タグか
		/// </summary>
		[DataMember]
		public bool Category { get; private set; }

		/// <summary>
		/// ロックされているか
		/// </summary>
		[DataMember]
		public bool Lock { get; private set; }

		/// <summary>
		/// タグの内容
		/// </summary>
		[DataMember]
		public string Value { get; private set; }
	}
}