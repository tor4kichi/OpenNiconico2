using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mntone.Nico2.Live.Reservations
{
	internal sealed class ReservationsClient
	{
		public static Task<string> GetReservationsDataAsync( NiconicoContext context )
		{
			return context.HttpClient
				.GetStringAsync(NiconicoUrls.LiveWatchingReservationListUrl);
		}

		public static IReadOnlyList<string> ParseReservationsData( string reservationsInDatailData )
		{
			var xml = XDocument.Parse( reservationsInDatailData );
			var responseXml = xml.Root;
			if( responseXml.Name.LocalName != "nicolive_video_response" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			var listXml = responseXml.Element("timeshift_reserved_list");
			if( listXml == null)
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			if( listXml != null )
			{
				return listXml.Elements().Select( vidXml => "lv" + vidXml.Value ).ToList();
			}
			return new List<string>();
		}

		public static Task<IReadOnlyList<string>> GetReservationsAsync( NiconicoContext context )
		{
			return GetReservationsDataAsync( context )
				.ContinueWith( prevTask => ParseReservationsData( prevTask.Result ) );
		}
	}
}