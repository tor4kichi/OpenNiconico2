using System.Linq;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// 部屋情報を格納するクラス
	/// </summary>
	public sealed class Room
	{
#if WINDOWS_APP
		internal Room( IXmlNode streamXml, IXmlNode userXml )
#else
		internal Room( XElement streamXml, XElement userXml )
#endif
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