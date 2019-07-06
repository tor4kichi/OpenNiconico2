using System.Linq;
using System.Xml.Linq;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// 部屋情報を格納するクラス
	/// </summary>
	public sealed class Room
	{
		internal Room( XElement streamXml, XElement userXml )
		{
			Name = userXml.Element( "room_label" ).Value;
			SeatId = userXml.Element( "room_seetno" ).Value.ToUInt();

			SeatToken = userXml.Element( "seat_token" ).Value;
		}

		/// <summary>
		/// 部屋名
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// 席番号
		/// </summary>
		public uint SeatId { get; private set; }

		/// <summary>
		/// 座席のトークン
		/// </summary>
		/// <remarks>
		/// 非ログインで使われる
		/// </remarks>
		public string SeatToken { get; private set; }
	}
}