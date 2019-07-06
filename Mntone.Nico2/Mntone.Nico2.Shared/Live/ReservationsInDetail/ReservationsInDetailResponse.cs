using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Mntone.Nico2.Live.ReservationsInDetail
{
	/// <summary>
	/// watchingreservation?mode=detaillist の情報を格納するクラス
	/// </summary>
	public sealed class ReservationsInDetailResponse
	{
		internal ReservationsInDetailResponse( XElement reservedItemsXml )
		{
			if( reservedItemsXml != null )
			{
				ReservedProgram = reservedItemsXml.Elements().Select( reservedItemXml => new Program( reservedItemXml ) ).ToList();
			}
			else
			{
				ReservedProgram = new List<Program>();
			}
		}

		/// <summary>
		/// 画像の一覧
		/// </summary>
		public IReadOnlyList<Program> ReservedProgram { get; private set; }
	}
}