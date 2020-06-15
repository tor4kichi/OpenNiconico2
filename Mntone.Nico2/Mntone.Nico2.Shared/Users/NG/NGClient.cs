using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mntone.Nico2.Users.NG
{
	internal sealed class NGClient
    {
		public static Task<string> GetNGCommentDataAsync(NiconicoContext context)
		{
			var dict = new Dictionary<string, string>();

			dict.Add("mode", "get");

			return context
				.GetStringAsync(NiconicoUrls.UserNGCommentUrl + "?mode=get");
		}

		public static Task<string> AddNGCommentDataAsync(NiconicoContext context, NGCommentType type, string source)
		{
			var dict = new Dictionary<string, string>();

			dict.Add("mode", "add");
			dict.Add("language", "0");
			dict.Add("type", type.ToString());
			dict.Add("source", source);

			return context.PostAsync(NiconicoUrls.UserNGCommentUrl, dict);
		}

		public static Task<string> DeleteNGCommentDataAsync(NiconicoContext context, NGCommentType type, string source)
		{
			var dict = new Dictionary<string, string>();

			dict.Add("mode", "delete");
			dict.Add("language", "0");
			dict.Add("type", type.ToString());
			dict.Add("source", source);

			return context.PostAsync(NiconicoUrls.UserNGCommentUrl, dict);
		}


		private static T ParseNGCommentListXml<T>(string xml)
			where T : NGCommentResponseCore
		{
			var serializer = new XmlSerializer(typeof(T));

			using (var stream = new StringReader(xml))
			{
				return (T)serializer.Deserialize(stream);
			}
		}




		public static Task<NGCommentResponse> GetNGCommentAsync(NiconicoContext context)
		{
			return GetNGCommentDataAsync(context)
				.ContinueWith(prevTask => ParseNGCommentListXml<NGCommentResponse>(prevTask.Result));
		}

		public static Task<NGCommentResponseCore> AddNGCommentAsync(NiconicoContext context, NGCommentType type, string source)
		{
			return AddNGCommentDataAsync(context, type, source)
				.ContinueWith(prevTask => ParseNGCommentListXml<NGCommentResponseCore>(prevTask.Result));
		}

		public static Task<NGCommentResponseCore> DeleteNGCommentAsync(NiconicoContext context, NGCommentType type, string source)
		{
			return DeleteNGCommentDataAsync(context, type, source)
				.ContinueWith(prevTask => ParseNGCommentListXml<NGCommentResponseCore>(prevTask.Result));
		}
	}
}
