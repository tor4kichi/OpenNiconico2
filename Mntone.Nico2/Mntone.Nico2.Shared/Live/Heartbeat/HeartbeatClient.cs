using System;
using System.Linq;
using System.Threading.Tasks;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Live.Heartbeat
{
	internal sealed class HeartbeatClient
	{
		public static Task<string> HeartbeatDataAsync( NiconicoContext context, string requestId )
		{
			if( !NiconicoRegex.IsLiveId( requestId ) )
			{
				throw new ArgumentException();
			}

            return context.GetClient()
                .GetStringAsync(new Uri($"{NiconicoUrls.LiveHeartbeatUrl}?v={requestId}"));
		}

		public static HeartbeatResponse ParseHeartbeatData( string heartbeatData )
		{
#if WINDOWS_APP
			var xml = new XmlDocument();
			xml.LoadXml( heartbeatData, new XmlLoadSettings { ElementContentWhiteSpace = false, MaxElementDepth = 3 } );
#else
			var xml = XDocument.Parse( heartbeatData );
#endif

			var heartbeatXml = xml.Root;
			if( heartbeatXml.Name != "heartbeat" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			if( heartbeatXml.Attribute( "status" ).Value != "ok" )
			{
				var error = heartbeatXml.Elements().First();
				var code = error.Element( "code" ).Value;
				var description = error.Element( "description" ).Value;
				var reject = error.Element( "reject" ).Value.ToBooleanFromString();

				throw new Exception( "Parse Error: " + description + " (" + code + ')' );
			}

			return new HeartbeatResponse( heartbeatXml );
		}

		public static Task<HeartbeatResponse> HeartbeatAsync( NiconicoContext context, string requestId )
		{
			return HeartbeatDataAsync( context, requestId )
				.ContinueWith( prevTask => ParseHeartbeatData( prevTask.Result ) );
		}
	}
}