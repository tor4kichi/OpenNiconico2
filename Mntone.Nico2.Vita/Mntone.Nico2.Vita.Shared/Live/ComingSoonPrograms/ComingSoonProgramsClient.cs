﻿using Mntone.Nico2.Live;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Vita.Live.ComingSoonPrograms
{
	internal sealed class ComingSoonProgramsClient
	{
		public static Task<string> GetOnAirProgramsDataAsync(
			NiconicoVitaContext context, CommunityType type, Range range )
		{
			range.CheckMaximumLength( 149, "range" );

			var sb = new StringBuilder( NiconicoUrls.LiveVideoComingSoonListUrl );
			sb.Append( '&' );
			sb.Append( range.ToFromLimitString() );
			sb.Append( "&pt=" );
			sb.Append( type.ToCommunityTypeString() );
			return context.GetClient().GetStringWithoutHttpRequestExceptionAsync( sb.ToString() );
		}

		public static ProgramsResponse ParseOnAirProgramsData( string comingSoonProgramsData )
		{
			return JsonSerializerExtensions.Load<ProgramsResponseWrapper>( ProgramsResponseWrapper.PatchJson2( comingSoonProgramsData ) ).Response;
		}

		public static Task<ProgramsResponse> GetOnAirProgramsAsync(
			NiconicoVitaContext context, CommunityType type, Range range )
		{
			return GetOnAirProgramsDataAsync( context, type, range )
				.ContinueWith( prevTask => ParseOnAirProgramsData( prevTask.Result ) );
		}
	}
}