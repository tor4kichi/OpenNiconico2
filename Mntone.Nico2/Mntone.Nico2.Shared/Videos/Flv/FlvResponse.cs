using System;
using System.Collections.Generic;

#if WINDOWS_UWP
using Windows.Networking;
#endif

namespace Mntone.Nico2.Videos.Flv
{
	/// <summary>
	/// getflv の情報を格納するクラス
	/// </summary>
	public class FlvResponse
	{
		internal FlvResponse()
		{

		}

		internal FlvResponse( Dictionary<string, string> wwwFormData )
		{
			SetupFlvData(wwwFormData);
		}


		internal void SetupFlvData( Dictionary<string, string> wwwFormData )
		{
			ThreadId = SafeGetValue(wwwFormData, "thread_id")?.ToUInt() ?? uint.MaxValue;
			Length = SafeGetValue(wwwFormData, "l")?.ToTimeSpanFromSecondsString() ?? TimeSpan.FromSeconds(0);
			VideoUrl = SafeGetValue(wwwFormData, "url")?.ToUri() ?? null;
			ReportUrl = SafeGetValue(wwwFormData, "link")?.ToUri() ?? null;
			CommentServerUrl = SafeGetValue(wwwFormData, "ms")?.ToUri() ?? null;
			SubCommentServerUrl = SafeGetValue(wwwFormData, "ms_sub")?.ToUri() ?? null;

			var deleted = SafeGetValue(wwwFormData, "deleted");
			PrivateReason = deleted != null ? (PrivateReasonType)deleted.ToUShort() : PrivateReasonType.None;
			UserId = SafeGetValue(wwwFormData, "user_id")?.ToUInt() ?? uint.MaxValue;
			IsPremium = SafeGetValue(wwwFormData, "is_premium")?.ToBooleanFrom1() ?? false;
			UserName = SafeGetValue(wwwFormData, "nickname");
			var loadedAtTime = SafeGetValue(wwwFormData, "time");
			LoadedAt = DateTimeOffset.FromFileTime(10000 * long.Parse(loadedAtTime) + 116444736000000000);

			IsKeyRequired = SafeGetValue(wwwFormData, "needs_key")?.ToBooleanFrom1() ?? false;
			OptionalThreadId = SafeGetValue(wwwFormData, "optional_thread_id")?.ToUInt() ?? uint.MaxValue;

			ChannelFilter = SafeGetValue(wwwFormData, "ng_ch") ?? string.Empty;
			FlashMediaServerToken = SafeGetValue(wwwFormData, "fmst") ?? string.Empty;

#if WINDOWS_UWP
			AppsHost = wwwFormData["hms"].ToHostName();
#else
			AppsHost = SafeGetValue(wwwFormData, "hms");
#endif
			AppsPort = SafeGetValue(wwwFormData, "hmsp")?.ToUShort() ?? 0;
			AppsThreadId = SafeGetValue(wwwFormData, "hmst")?.ToUShort() ?? 0;
			AppsTicket = SafeGetValue(wwwFormData, "hmstk");

#if DEBUG
			Done = SafeGetValue(wwwFormData, "done")?.ToBooleanFromString() ?? false;
			NgRv = SafeGetValue(wwwFormData, "ng_rv")?.ToUShort() ?? ushort.MaxValue;
#endif
		}

		private string SafeGetValue(Dictionary<string, string> dict, string key)
		{
			if (dict.ContainsKey(key))
			{
				return dict[key];
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// スレッド ID
		/// </summary>
		public uint ThreadId { get; private set; }

		/// <summary>
		/// 長さ
		/// </summary>
		public TimeSpan Length { get; private set; }

		/// <summary>
		/// 動画 URL
		/// </summary>
		public Uri VideoUrl { get; private set; }

		/// <summary>
		/// 連絡ページ URL
		/// </summary>
		public Uri ReportUrl { get; private set; }

		/// <summary>
		/// コメント サーバー URL
		/// </summary>
		public Uri CommentServerUrl { get; private set; }

		/// <summary>
		/// サブ コメント サーバー URL
		/// </summary>
		public Uri SubCommentServerUrl { get; private set; }

		/// <summary>
		/// 非公開理由
		/// </summary>
		public PrivateReasonType PrivateReason { get; private set; }

		/// <summary>
		/// 削除されたか
		/// </summary>
		/// <remarks>
		/// 非公開動画は削除されたうちに入らない
		/// </remarks>
		public bool IsDeleted { get { return PrivateReason != PrivateReasonType.None && PrivateReason != PrivateReasonType.Private; } }

		/// <summary>
		/// ユーザー ID
		/// </summary>
		public uint UserId { get; private set; }

		/// <summary>
		/// プレミアム会員か
		/// </summary>
		public bool IsPremium { get; private set; }

		/// <summary>
		/// ユーザー名
		/// </summary>
		public string UserName { get; private set; }

		/// <summary>
		/// 読み込み日時
		/// </summary>
		public DateTimeOffset LoadedAt { get; private set; }

		/// <summary>
		/// キーが必要か
		/// </summary>
		public bool IsKeyRequired { get; private set; }

		/// <summary>
		/// 追加のスレッド ID
		/// </summary>
		public uint OptionalThreadId { get; private set; }

		/// <summary>
		/// チャンネル動画のフィルター
		/// </summary>
		public string ChannelFilter { get; private set; }

		/// <summary>
		/// Flash Media サーバーのトークン
		/// </summary>
		public string FlashMediaServerToken { get; private set; }

#if DEBUG
		/// <summary>
		/// ? done
		/// </summary>
		public bool Done { get; private set; }

		/// <summary>
		/// ? ng_rv
		/// </summary>
		public ushort NgRv { get; private set; }
#endif

		/// <summary>
		/// ニコニコアプリのホスト名
		/// </summary>
#if WINDOWS_UWP
		public HostName AppsHost { get; private set; }
#else
		public string AppsHost { get; private set; }
#endif

		/// <summary>
		/// ニコニコアプリのポート番号
		/// </summary>
		public ushort AppsPort { get; private set; }

		/// <summary>
		/// ニコニコアプリのスレッド ID
		/// </summary>
		public uint AppsThreadId { get; set; }

		/// <summary>
		/// ニコニコアプリのチケット
		/// </summary>
		public string AppsTicket { get; private set; }
	}
}