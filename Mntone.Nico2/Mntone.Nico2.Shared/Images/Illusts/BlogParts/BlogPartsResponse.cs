using System;
using System.Collections.Generic;
using System.Linq;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Images.Illusts.BlogParts
{
	/// <summary>
	/// blogparts の情報を格納するクラス
	/// </summary>
	public sealed class BlogPartsResponse
	{
#if WINDOWS_APP
		internal BlogPartsResponse( IXmlNode responseXml )
#else
		internal BlogPartsResponse( XElement responseXml )
#endif
		{
#if DEBUG
			BaseUrl = responseXml.Element( "base_url" ).Value.ToUri();
#endif
			PageUrl = responseXml.Element( "icon_url" ).Value.ToUri();
#if DEBUG
			ImageBaseUrl = responseXml.Element( "image_url" ).Value.ToUri();
#endif

			var imageListXml = responseXml.Element( "image_list" );
			if( imageListXml.Elements().FirstOrDefault()?.Elements().FirstOrDefault() != null )
			{
				Images = imageListXml.Elements().Select( imageXml => new Image( imageXml ) ).ToList();
			}
			else
			{
				Images = new List<Image>();
			}
		}

#if DEBUG
		/// <summary>
		/// ベース URL
		/// </summary>
		public Uri BaseUrl { get; private set; }
#endif

		/// <summary>
		/// 視聴ページ
		/// </summary>
		public Uri PageUrl { get; private set; }

#if DEBUG
		/// <summary>
		/// 画像のベース URL
		/// </summary>
		public Uri ImageBaseUrl { get; private set; }
#endif
		/// <summary>
		/// 画像の一覧
		/// </summary>
		public IReadOnlyList<Image> Images { get; private set; }
	}
}