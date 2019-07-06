using System.Linq;
using System.Xml.Linq;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// テロップの情報を格納するクラス
	/// </summary>
	public sealed class Telop
	{
		internal Telop( XElement telopNode )
		{
			IsEnabled = telopNode.Element( "enable" ).Value.ToBooleanFrom1();

			var mailXml = telopNode.Element( "mail" ).Value;
			if( !string.IsNullOrEmpty( mailXml ) )
			{
				Mail = mailXml;
				Value = telopNode.Element( "caption" ).Value;
			}
			else
			{
				Mail = string.Empty;
				Value = string.Empty;
			}
		}

		/// <summary>
		/// テロップが有効か
		/// </summary>
		public bool IsEnabled { get; private set; }

		/// <summary>
		/// テロップのコマンド
		/// </summary>
		public string Mail { get; private set; }

		/// <summary>
		/// テロップの内容
		/// </summary>
		public string Value { get; private set; }
	}
}
