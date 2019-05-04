using System;
using System.Linq;

using System.Xml.Linq;

namespace Mntone.Nico2.Images
{
	/// <summary>
	/// コメントの情報を格納するクラス
	/// </summary>
	public sealed class Comment
	{
		internal Comment( XElement commentXml )
		{
			Id = commentXml.Element( "comment_id" ).Value.ToULong();
			ImageId = "im" + commentXml.Element( "image_id" ).Value;
			ResId = commentXml.Element( "res_id" ).Value.ToULong();
			Value = commentXml.Element( "content" ).Value;
			Command = commentXml.Element( "command" ).Value;
			PostedAt = ( commentXml.Element( "created" ).Value + "+09:00" ).ToDateTimeOffsetFromIso8601();
			Frame = commentXml.Element( "frame" ).Value.ToInt();
			UserHash = commentXml.Element( "user_hash" ).Value;
			IsAnonymous = commentXml.Element( "anonymous_flag" ).Value.ToBooleanFrom1();
		}

		/// <summary>
		/// ID
		/// </summary>
		public ulong Id { get; private set; }

		/// <summary>
		/// 画像 ID
		/// </summary>
		public string ImageId { get; private set; }

		/// <summary>
		/// レス先 ID
		/// </summary>
		public ulong ResId { get; private set; }

		/// <summary>
		/// 内容
		/// </summary>
		public string Value { get; private set; }

		/// <summary>
		/// コマンド
		/// </summary>
		public string Command { get; private set; }

		/// <summary>
		/// 投稿日時
		/// </summary>
		public DateTimeOffset PostedAt { get; private set; }

		/// <summary>
		/// フレーム (?)
		/// </summary>
		public int Frame { get; private set; }

		/// <summary>
		/// ユーザー ハッシュ
		/// </summary>
		public string UserHash { get; private set; }

		/// <summary>
		/// 匿名か
		/// </summary>
		public bool IsAnonymous { get; private set; }
	}
}