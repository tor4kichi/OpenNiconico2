﻿using System;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// コマンドを格納するクラス
	/// </summary>
	/// <remarks>
	/// タイムシフトでは stream/quesheet による放送再現を行う
	/// </remarks>
	public sealed class Command
	{
#if WINDOWS_APP
		internal Command( IXmlNode queXml )
#else
		internal Command( XElement queXml )
#endif
		{
			Position = TimeSpan.FromTicks( queXml.Attribute( "vpos" ).Value.ToLong() * 10000 );
			Mail = queXml.Attribute( "mail" ).Value;
			Name = queXml.Attribute( "name" ).Value;
			Value = queXml.Value;
		}

		/// <summary>
		/// 実行時間
		/// </summary>
		public TimeSpan Position { get; private set; }

		/// <summary>
		/// 文字コマンド
		/// </summary>
		public string Mail { get; private set; }

		/// <summary>
		/// 投稿者名
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// コマンドの内容
		/// </summary>
		public string Value { get; private set; }
	}
}