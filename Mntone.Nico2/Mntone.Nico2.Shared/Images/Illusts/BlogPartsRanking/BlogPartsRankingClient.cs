using System;
using System.Threading.Tasks;

using System.Xml.Linq;

namespace Mntone.Nico2.Images.Illusts.BlogPartsRanking
{
	internal sealed class BlogPartsRankingClient
	{
		public static Task<string> GetRankingDataAsync(
			NiconicoContext context, DurationType targetDuration, GenreOrCategory targetGenreOrCategory )
		{
			return context.GetStringAsync(
					$"{NiconicoUrls.ImageBlogPartsUrl}ranking&key={targetDuration.ToDurationTypeString()}%2c{targetGenreOrCategory.ToGenreAndCategoryString()}"
				);
		}

		public static BlogPartsRankingResponse ParseRankingData( string rankingData )
		{
			var xml = XDocument.Parse( rankingData );

            var responseXml = xml.Root;
			if( responseXml.Name.LocalName != "response" )
			{
				throw new Exception( "Parse Error: Node name is invalid." );
			}

			return new BlogPartsRankingResponse( responseXml );
		}

		public static Task<BlogPartsRankingResponse> GetRankingAsync(
			NiconicoContext context, DurationType targetDuration, GenreOrCategory targetGenreOrCategory )
		{
			return GetRankingDataAsync( context, targetDuration, targetGenreOrCategory )
				.ContinueWith( prevTask => ParseRankingData( prevTask.Result ) );
		}
	}
}