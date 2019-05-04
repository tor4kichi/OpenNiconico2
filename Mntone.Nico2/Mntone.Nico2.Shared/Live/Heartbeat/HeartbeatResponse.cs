using System;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.Heartbeat
{
	/// <summary>
	/// heartbeat の情報を格納するクラス
	/// </summary>
	public sealed class HeartbeatResponse
	{
#if WINDOWS_APP
		internal HeartbeatResponse( IXmlNode heartbeatXml )
#else
		internal HeartbeatResponse( XElement heartbeatXml )
#endif
		{
			LoadedAt = heartbeatXml.Attribute( "time" ).Value.ToDateTimeOffsetFromUnixTime();
			WatchCount = heartbeatXml.Element( "watchCount" ).Value.ToUInt();
			CommentCount = heartbeatXml.Element( "commentCount" ).Value.ToUInt();
			IsRestrict = heartbeatXml.Element( "is_restrict" ).Value.ToBooleanFrom1();
			Ticket = heartbeatXml.Element( "ticket" ).Value;
			WaitDuration = heartbeatXml.Element( "waitTime" ).Value.ToTimeSpanFromSecondsString();
		}

		/// <summary>
		/// 読み込み日時
		/// </summary>
		public DateTimeOffset LoadedAt { get; private set; }

		/// <summary>
		/// 合計視聴者数
		/// </summary>
		public uint WatchCount { get; private set; }

		/// <summary>
		/// 合計コメント数
		/// </summary>
		public uint CommentCount { get; private set; }

		/// <summary>
		/// ?
		/// </summary>
		public bool IsRestrict { get; private set; }

		/// <summary>
		/// チケット
		/// </summary>
		public string Ticket { get; private set; }

		/// <summary>
		/// 待機時間
		/// </summary>
		public TimeSpan WaitDuration { get; private set; }
	}
}