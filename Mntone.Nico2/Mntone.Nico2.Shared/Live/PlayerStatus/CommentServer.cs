using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// コメント サーバーの情報を格納するクラス
	/// </summary>
	public sealed class CommentServer
	{
		internal CommentServer( XElement commentServerXml, XElement threadIdsXml )
		{
			Host = commentServerXml.Element( "addr" ).Value;
			Port = commentServerXml.Element( "port" ).Value.ToUShort();
			if( threadIdsXml.HasElements )
			{
				ThreadIds = threadIdsXml.Elements().Select( threadIdXml => threadIdXml.Value.ToUInt() ).ToList();
			}
			else
			{
				ThreadIds = new List<uint>()
				{
					commentServerXml.Element( "thread" ).Value.ToUInt()
				};
			}
		}

		/// <summary>
		/// ホスト名
		/// </summary>
		public string Host { get; private set; }

		/// <summary>
		/// ポート番号
		/// </summary>
		public ushort Port { get; private set; }

		/// <summary>
		/// スレッド ID
		/// </summary>
		/// <remarks>
		/// 配信者がこの API を叩くと、複数のスレッド ID を得ることができる。
		/// それにより、メインと立ち見の部屋を移動することができる。
		/// </remarks>
		public IReadOnlyList<uint> ThreadIds { get; private set; }
	}
}