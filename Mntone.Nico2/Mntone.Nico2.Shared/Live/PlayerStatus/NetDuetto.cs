using System.Xml.Linq;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// ネット デュエットの情報を格納するクラス
	/// </summary>
	public sealed class NetDuetto
	{
		internal NetDuetto( XElement streamXml )
		{
			IsEnabled = streamXml.Element( "allow_netduetto" ).Value.ToBooleanFrom1();
			Token = streamXml.Element( "nd_token" ).Value;
		}

		/// <summary>
		/// 有効か
		/// </summary>
		public bool IsEnabled { get; private set; }

		/// <summary>
		/// トークン
		/// </summary>
		public string Token { get; private set; }
	}
}