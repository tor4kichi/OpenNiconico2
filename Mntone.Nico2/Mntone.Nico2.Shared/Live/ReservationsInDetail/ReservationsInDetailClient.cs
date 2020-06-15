using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mntone.Nico2.Live.ReservationsInDetail
{
	internal sealed class ReservationsInDetailClient
	{
		public static Task<string> GetReservationsInDetailDataAsync( NiconicoContext context )
		{
			return context.GetConvertedStringAsync( NiconicoUrls.LiveWatchingReservationDetailListUrl );
		}

		public static ReservationsInDetailResponse ParseReservationsInDetailData( string reservationsInDatailData )
		{
			var xml = XDocument.Parse( reservationsInDatailData );
			var responseXml = xml.Root;
			if( responseXml.Name != "nicolive_video_response" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			var listXml = responseXml.Element("timeshift_reserved_detail_list");
			if( listXml == null )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			return new ReservationsInDetailResponse( listXml );
		}

		public static Task<ReservationsInDetailResponse> GetReservationsInDetailAsync( NiconicoContext context )
		{
			return GetReservationsInDetailDataAsync( context )
				.ContinueWith( prevTask => ParseReservationsInDetailData( prevTask.Result ) );
		}
	}
}