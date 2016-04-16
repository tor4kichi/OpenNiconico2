using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Videos.Comment
{
	internal sealed class CommentClient
    {

		public static async Task<string> GetThreadKeyDataAsync(NiconicoContext context, uint threadId)
		{
			return await context.GetClient()
				.GetStringAsync(NiconicoUrls.VideoThreadKeyApiUrl + threadId.ToString());
		}


		public static ThreadKeyResponse ParseThreadKey(string threadKeyData)
		{
			// threadkey response
			// see@ http://d.hatena.ne.jp/s01149ht/20091216/1260983794

			var dict = HttpQueryExtention.QueryToDictionary(threadKeyData);
			
			return new ThreadKeyResponse(dict["threadkey"], dict["force_184"]);
		}

		public static async Task<string> GetCommentDataAsync(NiconicoContext context, Flv.FlvResponse response)
		{
			var paramDict = new Dictionary<string, string>();
			paramDict.Add("user_id", response.UserId.ToString());
			paramDict.Add("version", "20090904");
			paramDict.Add("thread", response.ThreadId.ToString());
			paramDict.Add("res_from", "-1000");

			// 公式動画の場合はThreadKeyとforce_184を取得する
			if (response.IsKeyRequired)
			{
				var threadKeyResponse = await GetThreadKeyDataAsync(context, response.ThreadId)
					.ContinueWith(prevTask => ParseThreadKey(prevTask.Result));

				paramDict.Add("threadkey", threadKeyResponse.ThreadKey);
				paramDict.Add("force_184", threadKeyResponse.Force184);
			}
			
			/*
			var commentPostData = new CommentPostData(
				response.ThreadId.ToString()
				, threadKey
				, null // response.UserId.ToString()
				, (uint)response.Length.TotalMinutes
				, force184
				);

			var xml = commentPostData.ToXml();

			System.Diagnostics.Debug.Write(xml);


			var content = new Windows.Web.Http.HttpStringContent(xml);
*/

			try
			{
				var param = HttpQueryExtention.DictionaryToQuery(paramDict);
				var commentUrl = $"{response.CommentServerUrl}thread?{Uri.EscapeUriString(param)}";

				return  await context.GetClient()
					.GetStringAsync(commentUrl);
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.Write(e.Message);
				throw;
			}



			
		}


		public static CommentResponse ParseComment(string commentData)
		{
			System.Diagnostics.Debug.Write(commentData);

			return CommentResponse.CreateFromXml(commentData);
		}


		public static Task<CommentResponse> GetCommentAsync(NiconicoContext context, Flv.FlvResponse response)
		{
			return GetCommentDataAsync(context, response)
					.ContinueWith(prevTask => ParseComment(prevTask.Result));
		}
	}
}
