using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mntone.Nico2.Live.PlayerStatus
{
	internal sealed class PlayerStatusClient
	{
		public static Task<string> GetPlayerStatusDataAsync( NiconicoContext context, string requestId )
		{
			if( !NiconicoRegex.IsLiveId( requestId ) )
			{
				throw new ArgumentException();
			}

			return context.GetConvertedStringAsync( NiconicoUrls.LivePlayerStatusUrl + requestId );
		}

		public static PlayerStatusResponse ParsePlayerStatusData( string playerStatusData )
		{
			var xml = XDocument.Parse( playerStatusData );

			var getPlayerStatusXml = xml.Root;
			if( getPlayerStatusXml.Name != "getplayerstatus" )
			{
				throw new ParseException( "Parse Error: Node name is invalid." );
			}

			if( getPlayerStatusXml.Attribute( "status" ).Value != "ok" )
			{
				var error = getPlayerStatusXml.Elements().First();
				var code = error.Element( "code" ).Value;
				switch( code )
				{
				case "not_found":
					throw CustomExceptionFactory.Create( NiconicoHResult.E_LIVE_NOT_FOUND );

				case "closed":
					throw CustomExceptionFactory.Create( NiconicoHResult.E_LIVE_CLOSED );

				case "comingsoon":
					throw CustomExceptionFactory.Create( NiconicoHResult.E_LIVE_COMING_SOON );

				case "maintenance":
					throw CustomExceptionFactory.Create( NiconicoHResult.E_MAINTENANCE );

				case "require_community_member":
					throw CustomExceptionFactory.Create( NiconicoHResult.E_LIVE_COMMUNITY_MEMBER_ONLY );

				case "full":
					throw CustomExceptionFactory.Create( NiconicoHResult.E_LIVE_FULL );

				case "premium_only":
					throw CustomExceptionFactory.Create( NiconicoHResult.E_LIVE_PREMIUM_ONLY );

				case "notlogin":
					throw CustomExceptionFactory.Create( NiconicoHResult.E_NOT_SIGNING_IN );

				default:
					throw CustomExceptionFactory.Create( NiconicoHResult.E_LIVE_UNKNOWN );
				}
			}

			return new PlayerStatusResponse( getPlayerStatusXml );
		}

		public static Task<PlayerStatusResponse> GetPlayerStatusAsync( NiconicoContext context, string requestId )
		{
			return GetPlayerStatusDataAsync( context, requestId )
				.ContinueWith( prevTask => ParsePlayerStatusData( prevTask.Result ) );
		}
	}
}