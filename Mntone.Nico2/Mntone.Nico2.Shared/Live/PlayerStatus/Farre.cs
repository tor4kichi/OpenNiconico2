using System.Xml.Linq;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// ニコファーレの情報を格納するクラス
	/// </summary>
	public sealed class Farre
	{
		internal Farre( XElement farreXml )
		{
			IsEnabled = farreXml.Element( "farremode" ).Value.ToBooleanFrom1();
			IsAvatarmakerEnabled = farreXml.Element( "avatarmaker_enabled" ).Value.ToBooleanFrom1();
			IsInvokeAvatarmakerEnabled = farreXml.Element( "is_invoke_avatarmaker" ).Value.ToBooleanFrom1();
			IsMultiAngleEnabled = farreXml.Element( "multi_angle" ).Value.ToBooleanFrom1();
			MultiAngleCount = farreXml.Element( "multi_angle_num" ).Value.ToUShort();
		}

		/// <summary>
		/// 有効か
		/// </summary>
		public bool IsEnabled { get; private set; }

		/// <summary>
		/// Avatarmaker が有効か
		/// </summary>
		public bool IsAvatarmakerEnabled { get; private set; }

		/// <summary>
		/// InvokeAvatarmaker が有効か
		/// </summary>
		public bool IsInvokeAvatarmakerEnabled { get; private set; }

		/// <summary>
		/// マルチアングルが有効か
		/// </summary>
		public bool IsMultiAngleEnabled { get; private set; }

		/// <summary>
		/// マルチアングル数
		/// </summary>
		public ushort MultiAngleCount { get; private set; }
	}
}