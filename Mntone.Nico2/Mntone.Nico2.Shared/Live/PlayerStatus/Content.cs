using System;
using System.Xml.Linq;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// コンテンツを格納するクラス
	/// </summary>
	public sealed class Content
	{
		internal Content( XElement contentsXml )
		{
			Id = contentsXml.Attribute( "id" ).Value;
			IsAudioDisabled = contentsXml.Attribute( "disableAudio" ).Value.ToBooleanFrom1();
			IsVideoDisabled = contentsXml.Attribute( "disableVideo" ).Value.ToBooleanFrom1();
			StartedAt = contentsXml.Attribute( "start_time" ).Value.ToDateTimeOffsetFromUnixTime();
			Duration = contentsXml.Attribute( "duration" ).Value.ToTimeSpanFromSecondsString();
			Title = contentsXml.Attribute( "title" ).Value;
			Value = contentsXml.Value;
		}

		/// <summary>
		/// ID
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// 音声が無効か
		/// </summary>
		public bool IsAudioDisabled { get; private set; }

		/// <summary>
		/// 映像が無効か
		/// </summary>
		public bool IsVideoDisabled { get; private set; }

		/// <summary>
		/// 開始日時
		/// </summary>
		public DateTimeOffset StartedAt { get; private set; }

		/// <summary>
		/// 再生時間
		/// </summary>
		public TimeSpan Duration { get; private set; }

		/// <summary>
		/// 題名
		/// </summary>
		public string Title { get; private set; }

		/// <summary>
		/// コンテンツ
		/// </summary>
		public string Value { get; private set; }
	}
}