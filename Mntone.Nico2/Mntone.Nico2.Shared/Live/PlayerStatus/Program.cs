using System;
using System.Linq;
using System.Xml.Linq;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// 番組情報を格納するクラス
	/// </summary>
	public sealed class Program
	{
		internal Program( XElement streamXml, XElement playerXml, XElement nsenXml, ProgramTwitter programTwitter )
		{
			Id = streamXml.Element( "id" ).Value;
			Title = streamXml.Element( "title" ).Value;
			Description = streamXml.Element( "description" ).Value;

			WatchCount = streamXml.Element( "watch_count" ).Value.ToUInt();
			CommentCount = streamXml.Element( "comment_count" ).Value.ToUInt();

			CommunityType = streamXml.Element( "provider_type" ).Value.ToCommunityType();
			CommunityId = streamXml.Element( "default_community" ).Value;
			BroadcasterId = streamXml.Element( "owner_id" ).Value.ToUInt();
			BroadcasterName = streamXml.Element( "owner_name" ).Value;

			International = streamXml.Element( "international" ).Value.ToUShort();

			BaseAt = streamXml.Element( "base_time" ).Value.ToDateTimeOffsetFromUnixTime();
			OpenedAt = streamXml.Element( "open_time" ).Value.ToDateTimeOffsetFromUnixTime();
			StartedAt = streamXml.Element( "start_time" ).Value.ToDateTimeOffsetFromUnixTime();
			EndedAt = streamXml.Element( "end_time" ).Value.ToDateTimeOffsetFromUnixTime();

			var timeshiftTimeXml = streamXml.Element( "timeshift_time" ).Value;
			if( !string.IsNullOrEmpty( timeshiftTimeXml ) )
			{
				TimeshiftAt = timeshiftTimeXml.ToDateTimeOffsetFromUnixTime();
			}

#if DEBUG
			BourbonUrl = streamXml.Element( "bourbon_url" ).Value.ToUri();
#endif
			CrowdedUrl = streamXml.Element( "full_video" ).Value.ToUri();
#if DEBUG
			AfterUrl = streamXml.Element( "after_video" ).Value.ToUri();
			BeforeUrl = streamXml.Element( "before_video" ).Value.ToUri();
#endif
			KickOutUrl = streamXml.Element( "kickout_video" ).Value.ToUri();
			KickOutImageUrl = playerXml.Element( "dialog_image" ).Element( "oidashi" ).Value.ToUri();

			var pictureUrl = streamXml.Element( "picture_url" ).Value.ToUri();
			if( pictureUrl != null )
			{
				CommunityImageUrl = pictureUrl;
				CommunitySmallImageUrl = streamXml.Element( "thumb_url" ).Value.ToUri();
			}

			TicketUrl = streamXml.Element( "product_ticket_url" ).Value.ToUri();
			BannerUrl = streamXml.Element( "product_banner_url" ).Value.ToUri();
			ShutterUrl = streamXml.Element( "shutter_url" ).Value.ToUri();
			IsRerun = streamXml.Element( "is_rerun_stream" ).Value.ToBooleanFrom1();
			IsArchive = streamXml.Element( "archive" ).Value.ToBooleanFrom1();

			var isDJStream = streamXml.Element( "is_dj_stream" ).Value;
			if( isDJStream.ToBooleanFrom1() )
			{
				ExtendedType = ProgramExtendedType.NewComer;
				NsenType = string.Empty;
				NsenCommand = string.Empty;
			}
			else
			{
				var isCruiseStream = streamXml.Element( "is_cruise_stream" ).Value.ToBooleanFrom1();
				if( isCruiseStream )
				{
					ExtendedType = ProgramExtendedType.Cruise;
					NsenType = string.Empty;
					NsenCommand = string.Empty;
				}
				else if( nsenXml != null && nsenXml.Element( "is_ns_stream" ).Value.ToBooleanFrom1() )
				{
					ExtendedType = ProgramExtendedType.Nsen;
					NsenType = nsenXml.Element( "nstype" ).Value;
					NsenCommand = nsenXml.Element( "nspanel" ).Value;
				}
				else
				{
					ExtendedType = ProgramExtendedType.None;
					NsenType = string.Empty;
					NsenCommand = string.Empty;
				}
			}

			IsHighQuality = streamXml.Element( "hqstream" ).Value.ToBooleanFrom1();
			IsInfinity = streamXml.Element( "infinity_mode" ).Value.ToBooleanFrom1();
			IsReserved = streamXml.Element( "is_reserved" ).Value.ToBooleanFrom1();
			IsArchivePlayServer = streamXml.Element( "is_archiveplayserver" ).Value.ToBooleanFrom1();
			IsTimeshiftEnabled = streamXml.Element( "is_nonarchive_timeshift_enabled" ).Value.ToBooleanFrom1();

			var isProductStreamText = streamXml.Element( "is_product_stream" );
			if( isProductStreamText != null )
			{
				IsProductEnabled = isProductStreamText.Value.ToBooleanFrom1();
				IsTrialEnabled = streamXml.Element( "is_trial" ).Value.ToBooleanFrom1();
				IsBannerForced = streamXml.Element( "product_fixed_banner" ).Value.ToBooleanFrom1();
			}

			IsNoticeBalloonEnabled = playerXml.Element( "is_notice_viewer_balloon_enabled" ).Value.ToBooleanFrom1();
			IsErrorReportEnabled = playerXml.Element( "error_report" ).Value.ToBooleanFrom1();

			Twitter = programTwitter;
		}

		#region Description

		/// <summary>
		/// ID
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// 題名
		/// </summary>
		public string Title { get; private set; }

		/// <summary>
		/// 説明文
		/// </summary>
		public string Description { get; private set; }


		/// <summary>
		/// 来場者数
		/// </summary>
		public uint WatchCount { get; private set; }

		/// <summary>
		/// コメント数
		/// </summary>
		public uint CommentCount { get; private set; }


		/// <summary>
		/// 公式配信か
		/// </summary>
		public bool IsOfficial { get { return CommunityType == CommunityType.Official; } }

		/// <summary>
		/// チャンネル配信か
		/// </summary>
		public bool IsChannel { get { return CommunityType == CommunityType.Channel; } }

		/// <summary>
		/// コミュニティー配信か
		/// </summary>
		public bool IsCommunity { get { return CommunityType == CommunityType.Community; } }

		/// <summary>
		/// コミュニティーの種類
		/// </summary>
		public CommunityType CommunityType { get; private set; }

		/// <summary>
		/// コミュニティーの ID
		/// </summary>
		public string CommunityId { get; private set; }

		/// <summary>
		/// 配信者 ID
		/// </summary>
		public uint BroadcasterId { get; private set; }

		/// <summary>
		/// 配信者名
		/// </summary>
		public string BroadcasterName { get; private set; }


		/// <summary>
		/// 国際化定義
		/// </summary>
		public ushort International { get; private set; }

		#endregion

		#region Time

		/// <summary>
		/// 基準日時
		/// </summary>
		public DateTimeOffset BaseAt { get; private set; }

		/// <summary>
		/// 開場日時
		/// </summary>
		public DateTimeOffset OpenedAt { get; private set; }

		/// <summary>
		/// 開始日時
		/// </summary>
		public DateTimeOffset StartedAt { get; private set; }

		/// <summary>
		/// 終了日時
		/// </summary>
		public DateTimeOffset EndedAt { get; private set; }

		/// <summary>
		/// タイムシフト日時
		/// </summary>
		public DateTimeOffset TimeshiftAt { get; private set; }

		#endregion

		#region Url

#if DEBUG
		/// <summary>
		/// (?)
		/// </summary>
		public Uri BourbonUrl { get; private set; }
#endif

		/// <summary>
		/// 満員のときに飛ぶ URL
		/// </summary>
		public Uri CrowdedUrl { get; private set; }

#if DEBUG
		/// <summary>
		/// (?)
		/// </summary>
		/// <remarks>
		/// もはや使われていない模様
		/// </remarks>
		public Uri AfterUrl { get; private set; }

		/// <summary>
		/// (?)
		/// </summary>
		/// <remarks>
		/// もはや使われていない模様
		/// </remarks>
		public Uri BeforeUrl { get; private set; }
#endif

		/// <summary>
		/// 追い出されたときに飛ぶ URL
		/// </summary>
		public Uri KickOutUrl { get; private set; }

		/// <summary>
		/// 追い出し用画像 URL
		/// </summary>
		public Uri KickOutImageUrl { get; private set; }

		/// <summary>
		/// コミュニティー画像 URL
		/// </summary>
		public Uri CommunityImageUrl { get; private set; }

		/// <summary>
		/// コミュニティーの小さい画像 URL
		/// </summary>
		public Uri CommunitySmallImageUrl { get; private set; }

		/// <summary>
		/// チケット URL
		/// </summary>
		public Uri TicketUrl { get; private set; }

		/// <summary>
		/// バナー URL
		/// </summary>
		public Uri BannerUrl { get; private set; }

		/// <summary>
		/// シャッター URL
		/// </summary>
		public Uri ShutterUrl { get; private set; }

		#endregion

		/// <summary>
		/// 再放送か
		/// </summary>
		public bool IsRerun { get; private set; }

		/// <summary>
		/// タイムシフトか
		/// </summary>
		public bool IsArchive { get; private set; }

		/// <summary>
		/// 生放送か
		/// </summary>
		/// <remarks>
		/// 再放送も生放送として扱う
		/// </remarks>
		public bool IsLive { get { return !IsArchive; } }

		/// <summary>
		/// 新着動画か
		/// </summary>
		public bool IsNewComer { get { return ExtendedType == ProgramExtendedType.NewComer; } }

		/// <summary>
		/// クルーズか
		/// </summary>
		public bool IsCruise { get { return ExtendedType == ProgramExtendedType.Cruise; } }

		/// <summary>
		/// Nsen か
		/// </summary>
		public bool IsNsen { get { return ExtendedType == ProgramExtendedType.Nsen; } }

		/// <summary>
		/// 番組の拡張タイプ
		/// </summary>
		public ProgramExtendedType ExtendedType { get; private set; }

		/// <summary>
		/// 高画質配信か
		/// </summary>
		public bool IsHighQuality { get; private set; }

		/// <summary>
		/// 時間制限なしの放送か
		/// </summary>
		public bool IsInfinity { get; private set; }

		/// <summary>
		/// 予約番組か
		/// </summary>
		public bool IsReserved { get; private set; }

		/// <summary>
		/// (?)
		/// </summary>
		public bool IsArchivePlayServer { get; private set; }

		/// <summary>
		/// タイムシフトが有効か
		/// </summary>
		public bool IsTimeshiftEnabled { get; private set; }

		/// <summary>
		/// 製品番組か
		/// </summary>
		public bool IsProductEnabled { get; private set; }

		/// <summary>
		/// 仕様が有効か
		/// </summary>
		public bool IsTrialEnabled { get; private set; }

		/// <summary>
		/// バナー強制 (?)。
		/// stream/product_fixed_banner
		/// </summary>
		public bool IsBannerForced { get; private set; }

		/// <summary>
		/// 注意バルーンが有効か
		/// </summary>
		public bool IsNoticeBalloonEnabled { get; private set; }

		/// <summary>
		/// エラー報告が有効か
		/// </summary>
		public bool IsErrorReportEnabled { get; private set; }

		#region Nsen

		/// <summary>
		/// Nsen のカテゴリー
		/// </summary>
		public string NsenType { get; private set; }

		/// <summary>
		/// Nsen の現在のコマンド
		/// </summary>
		public string NsenCommand { get; private set; }

		#endregion

		/// <summary>
		/// Twitter 情報
		/// </summary>
		public ProgramTwitter Twitter { get; private set; }
	}
}