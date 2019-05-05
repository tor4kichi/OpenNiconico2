﻿using System;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// 番組の Twitter 情報を格納するクラス
	/// </summary>
	public sealed class ProgramTwitter
	{
#if WINDOWS_APP
		internal ProgramTwitter( IXmlNode streamXml, IXmlNode twitterXml )
#else
		internal ProgramTwitter( XElement streamXml, XElement twitterXml )
#endif
		{
			IsEnabled = twitterXml.Element( "live_enabled" ).Value.ToBooleanFrom1();
			Hashtag = streamXml.Element( "twitter_tag" ).Value;
			VipModeCount = twitterXml.Element( "vip_mode_count" ).Value.ToUInt();
		}

		/// <summary>
		/// Twitter が有効か
		/// </summary>
		public bool IsEnabled { get; private set; }

		/// <summary>
		/// ハッシュタグ
		/// </summary>
		public string Hashtag { get; private set; }

		/// <summary>
		/// Vip アカウントとして表示するフォロワーの最低件数
		/// </summary>
		public uint VipModeCount { get; private set; }
	}
}