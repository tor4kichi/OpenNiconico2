using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// コメントの情報を格納するクラス
	/// </summary>
	public sealed class Comment
	{
		internal Comment( XElement streamXml, CommentServer commentServer )
		{
			IsLocked = streamXml.Element( "comment_lock" ).Value.ToBooleanFrom1();

			var scale = streamXml.Element( "font_scale" ).Value;
			Scale = !string.IsNullOrEmpty( scale ) ? scale.ToSingle() : 1.0f;

			Perm = streamXml.Element( "perm" ).Value;

			var splitTop = streamXml.Element( "split_top" ).Value.ToBooleanFrom1();
			if( splitTop )
			{
				Position = CommentPosition.Bottom;
			}
			else
			{
				var splitBottom = streamXml.Element( "split_bottom" ).Value.ToBooleanFrom1();
				if( splitBottom )
				{
					Position = CommentPosition.Top;
				}
				else
				{
					var isTop = streamXml.Element( "header_comment" ).Value.ToBooleanFrom1();
					var isBottom = streamXml.Element( "footer_comment" ).Value.ToBooleanFrom1();
					if( isTop )
					{
						Position = isBottom ? CommentPosition.Both : CommentPosition.Top;
					}
					else
					{
						Position = isBottom ? CommentPosition.Bottom : CommentPosition.Default;
					}
				}
			}

			FilteringLevel = ( CommentFilteringLevel )streamXml.Element( "ng_scoring" ).Value.ToUShort();
			SexMode = ( CommentSexMode )streamXml.Element( "danjo_comment_mode" ).Value.ToInt();

			var quesheetXml = streamXml.Element( "quesheet" );
			if( quesheetXml != null )
			{
				Commands = quesheetXml.Elements().Select( queXml => new Command( queXml ) ).ToList();
			}

			IsRestrict = streamXml.Element( "is_restrict" ).Value.ToBooleanFrom1();

			var productCommentXml = streamXml.Element( "product_comment" ).Value;
			if( !string.IsNullOrEmpty( productCommentXml ) )
			{
				LimitMode = ( CommentLimitMode )productCommentXml.ToInt();
			}

			Server = commentServer;
		}

		/// <summary>
		/// コメント ロックされているか
		/// </summary>
		public bool IsLocked { get; private set; }

		/// <summary>
		/// コメント スケール率
		/// </summary>
		public float Scale { get; private set; }

		/// <summary>
		/// perm
		/// </summary>
		public string Perm { get; private set; }

		/// <summary>
		/// コメント表示位置
		/// </summary>
		public CommentPosition Position { get; private set; }

		/// <summary>
		/// フィルタリング レベル
		/// </summary>
		public CommentFilteringLevel FilteringLevel { get; private set; }

		/// <summary>
		/// 性別による装飾方法
		/// </summary>
		public CommentSexMode SexMode { get; private set; }

		/// <summary>
		/// コマンド
		/// </summary>
		public IReadOnlyList<Command> Commands { get; private set; }

		/// <summary>
		/// 厳密に評価するか
		/// </summary>
		/// <remarks>
		/// 厳密に評価する場合、試用版では LimitType に応じて機能が制限されます。
		/// LimitType = Allow, Restrict: コメントは制限されません。
		/// LimitType = Deny: コメントは制限されます (購入者のみ投稿できます)
		/// </remarks>
		public bool IsRestrict { get; private set; }

		/// <summary>
		/// 製品のコメント モード
		/// </summary>
		public CommentLimitMode LimitMode { get; private set; }

		/// <summary>
		/// コメント サーバー情報
		/// </summary>
		public CommentServer Server { get; private set; }
	}
}