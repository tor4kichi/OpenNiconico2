using System;
using System.Runtime.Serialization;

namespace Mntone.Nico2.Videos.Histories
{
	/// <summary>
	/// 視聴した動画を格納するクラス
	/// </summary>
	[DataContract]
	public sealed class History
	{
		internal History()
		{ }

		/// <summary>
		/// 削除された (非公開含む) か
		/// </summary>
		public PrivateReasonType DeleteStatus { get { return this._DeleteStatus; } }
		private PrivateReasonType _DeleteStatus = PrivateReasonType.None;

		[DataMember(Name = "deleted")]
		private uint IsDeletedImpl
		{
			get
			{
				return (uint)_DeleteStatus;
			}
			set
			{
				_DeleteStatus = (PrivateReasonType)value;
			}
		}

		/// <summary>
		/// デバイス
		/// </summary>
		[DataMember( Name = "device" )]
		public ushort Device { get; set; }

		/// <summary>
		/// 要素の ID
		/// </summary>
		[DataMember( Name = "item_id" )]
		public string ItemId { get; set; }

		/// <summary>
		/// 長さ
		/// </summary>
		public TimeSpan Length { get { return this._Length; } }
        internal TimeSpan _Length = TimeSpan.Zero;

		[DataMember( Name = "length" )]
		private string LengthImpl
		{
			get
			{
				if( this._Length == null )
				{
					return "0:00";
				}

				return ( 60 * this._Length.Hours + this._Length.Minutes ).ToString() + ':' + this._Length.Seconds;
			}
			set { this._Length = value.ToTimeSpan(); }
		}

		/// <summary>
		/// サムネール URL
		/// </summary>
		[DataMember( Name = "thumbnail_url" )]
		public Uri ThumbnailUrl { get; set; }

		/// <summary>
		/// 題名
		/// </summary>
		[DataMember( Name = "title" )]
		public string Title { get; set; }

		/// <summary>
		/// 動画 ID
		/// </summary>
		[DataMember( Name = "video_id" )]
		public string Id { get; set; }

		/// <summary>
		/// 閲覧数
		/// </summary>
		[DataMember( Name = "watch_count" )]
		public uint WatchCount { get; set; }

		/// <summary>
		/// 開場時間
		/// </summary>
		public DateTimeOffset WatchedAt { get { return this._WatchedAt; } }
		internal DateTimeOffset _WatchedAt = DateTimeOffset.MinValue;

		[DataMember( Name = "watch_date" )]
		private long WatchedAtImpl
		{
			get { return this._WatchedAt.ToLongFromDateTimeOffset(); }
			set { this._WatchedAt = value.ToDateTimeOffsetFromUnixTime(); }
		}
	}
}