using System;
using System.Collections.Generic;
using System.Linq;

using System.Xml.Linq;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// getplayerstatus の情報を格納するクラス
	/// </summary>
	/// <!--
	/// よくわからないもの
	/// - stream/is_kirei
	/// - stream/press
	/// -->
	public sealed class PlayerStatusResponse
	{
		internal PlayerStatusResponse( XElement playerStatusXml )
		{
			var streamXml = playerStatusXml.Element( "stream" );
			var userXml = playerStatusXml.Element( "user" );
			var playerXml = playerStatusXml.Element( "player" );
			var farreXml = playerStatusXml.Element( "farre" );

			LoadedAt = playerStatusXml.Attribute( "time" ).Value.ToDateTimeOffsetFromUnixTime();
			Program = new Program(
				streamXml,
				playerXml,
				playerStatusXml.Element( "nsen" ),
				new ProgramTwitter( streamXml, playerStatusXml.Element( "twitter" ) ) );
			Room = new Room( streamXml, userXml );
			Stream = new Stream(
				streamXml,
				playerStatusXml.Element( "rtmp" ),
				playerStatusXml.Element( "tickets" ),
				playerXml );
			Comment = new Comment(
				streamXml,
				new CommentServer(
					playerStatusXml.Element( "ms" ),
					playerStatusXml.Element( "tid_list" ) ) );
			Telop = new Telop( streamXml.Element( "telop" ) );
			NetDuetto = new NetDuetto( streamXml );
			Farre = farreXml != null ? new Farre( farreXml ) : null;
			Marquee = new Marquee( playerStatusXml.Element( "marquee" ) );
			User = new User( streamXml, userXml );
		}

		/// <summary>
		/// 読み込み日時
		/// </summary>
		public DateTimeOffset LoadedAt { get; private set; }

		/// <summary>
		/// 番組情報
		/// </summary>
		public Program Program { get; private set; }

		/// <summary>
		/// 部屋情報
		/// </summary>
		public Room Room { get; private set; }

		/// <summary>
		/// ストリーム情報
		/// </summary>
		public Stream Stream { get; private set; }

		/// <summary>
		/// コメント情報
		/// </summary>
		public Comment Comment { get; private set; }

		/// <summary>
		/// テロップ情報
		/// </summary>
		public Telop Telop { get; private set; }

		/// <summary>
		/// ネット デュエット情報
		/// </summary>
		public NetDuetto NetDuetto { get; private set; }

		/// <summary>
		/// ニコファーレ情報
		/// </summary>
		public Farre Farre { get; private set; }

		/// <summary>
		/// Marquee 情報
		/// </summary>
		public Marquee Marquee { get; private set; }

		/// <summary>
		/// ユーザー情報
		/// </summary>
		/// <remarks>
		/// 視聴者の情報が格納されています
		/// </remarks>
		public User User { get; private set; }
	}
}