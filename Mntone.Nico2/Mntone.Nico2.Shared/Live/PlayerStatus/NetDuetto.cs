﻿#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// ネット デュエットの情報を格納するクラス
	/// </summary>
	public sealed class NetDuetto
	{
#if WINDOWS_APP
		internal NetDuetto( IXmlNode streamXml )
#else
		internal NetDuetto( XElement streamXml )
#endif
		{
			IsEnabled = streamXml.Element( "allow_netduetto" ).Value.ToBooleanFrom1();
			Token = streamXml.Element( "nd_token" ).Value;
		}

		/// <summary>
		/// 有効か
		/// </summary>
		public bool IsEnabled { get; private set; }

		/// <summary>
		/// トークン
		/// </summary>
		public string Token { get; private set; }
	}
}