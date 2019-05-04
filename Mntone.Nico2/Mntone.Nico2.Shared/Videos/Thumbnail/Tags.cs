using System.Collections.Generic;
using System.Linq;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Videos.Thumbnail
{
	/// <summary>
	/// タグ データ
	/// </summary>
	public sealed class ThumbnailTags
	{
#if WINDOWS_APP
		internal Tags( IXmlNode tagsXml )
#else
		internal ThumbnailTags( XElement tagsXml )
#endif
		{
			Domain = tagsXml.Element( "domain" ).Value;
			Value = tagsXml.Elements().Select( tagXml => new ThumbnailTag( tagXml ) ).ToList();
		}

		/// <summary>
		/// タグのドメイン
		/// </summary>
		public string Domain { get; private set; }

		/// <summary>
		/// タグの一覧
		/// </summary>
		public IReadOnlyList<ThumbnailTag> Value { get; private set; }
	}
}