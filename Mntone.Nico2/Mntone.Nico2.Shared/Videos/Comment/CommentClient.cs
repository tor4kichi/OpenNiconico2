using Mntone.Nico2.Videos.Flv;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Mntone.Nico2.Videos.Comment
{
    internal sealed class CommentClient
    {

        public static async Task<string> GetThreadKeyDataAsync(NiconicoContext context, long threadId)
        {
            return await context
                .GetStringAsync(NiconicoUrls.VideoThreadKeyApiUrl + threadId.ToString());
        }


        public static ThreadKeyResponse ParseThreadKey(string threadKeyData)
        {
            // threadkey response
            // see@ http://d.hatena.ne.jp/s01149ht/20091216/1260983794

            if (string.IsNullOrEmpty(threadKeyData)) { return null; }

            var dict = HttpQueryExtention.QueryToDictionary(threadKeyData);

            return new ThreadKeyResponse(dict["threadkey"], dict["force_184"]);
        }

        public static async Task<string> GetCommentDataAsync(NiconicoContext context, int userId, string commentServerUrl, int threadId, bool isKeyRequired)
        {
            var paramDict = new Dictionary<string, string>();
            paramDict.Add("user_id", userId is 0 ? "" : userId.ToString());
            paramDict.Add("version", "20090904");
            paramDict.Add("thread", threadId.ToString());
            paramDict.Add("res_from", "-1000");

            // 公式動画の場合はThreadKeyとforce_184を取得する
            if (isKeyRequired)
            {
                var threadKeyResponse = await GetThreadKeyDataAsync(context, threadId)
                    .ContinueWith(prevTask => ParseThreadKey(prevTask.Result));

                if (threadKeyResponse != null)
                {
                    paramDict.Add("threadkey", threadKeyResponse.ThreadKey);
                    paramDict.Add("force_184", threadKeyResponse.Force184);
                }
            }

            var param = HttpQueryExtention.DictionaryToQuery(paramDict);
            var commentUrl = $"{commentServerUrl}thread?{Uri.EscapeUriString(param)}";

            return await context.GetStringAsync(commentUrl);
        }


        public static CommentResponse ParseComment(string commentData)
        {
            return CommentResponse.CreateFromXml(commentData);
        }


        public static Task<CommentResponse> GetCommentAsync(NiconicoContext context, int userId, string commentServerUrl, int threadId, bool isKeyRequired)
        {
            return GetCommentDataAsync(context, userId, commentServerUrl, threadId, isKeyRequired)
                    .ContinueWith(prevTask => ParseComment(prevTask.Result));
        }




        // コメントの送信

        public static async Task<string> GetPostKeyAsync(NiconicoContext context, string threadId, int commentCount)
        {
            var paramDict = new Dictionary<string, string>();
            paramDict.Add("thread", threadId);
            paramDict.Add("block_no", ((commentCount) / 100).ToString());
            paramDict.Add("device", "1");
            paramDict.Add("version", "1");
            paramDict.Add("version_sub", "6");

            return await context.GetStringAsync(NiconicoUrls.VideoPostKeyUrl, paramDict);
        }


        static readonly int PostKeyCharCount = "postkey=".Count();

        public static string ParsePostKey(string getPostKeyResult)
        {
            Debug.WriteLine(getPostKeyResult);
            return new String(getPostKeyResult.Skip(PostKeyCharCount).ToArray());
        }


        public static async Task<string> PostCommentDataAsync(
            NiconicoContext context,
            string commentServerUrl,
            string threadId,
            string ticket,
            int commentCount,
            string comment,
            TimeSpan position,
            string command
            )
        {
            var info = await context.User.GetInfoAsync();
            var userid = info.Id;
            var isPremium = info.IsPremium;

            // postkeyの取得
            var postKey = await GetPostKeyAsync(context, threadId, commentCount)
                .ContinueWith(prevResult => ParsePostKey(prevResult.Result));

            Debug.WriteLine(postKey);


            var postComment = new PostComment()
            {
                user_id = userid.ToString(),
                mail = command,
                thread = threadId,
                vpos = ((uint)position.TotalMilliseconds / 10).ToString(),
                ticket = ticket,
                premium = isPremium.ToString1Or0(),
                postkey = postKey,
                comment = comment,
            };


            string postCommentXml = "";
            var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
            var serializer = new XmlSerializer(typeof(PostComment));

            var xmlwriterSettings = new XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
            };

            using (var memoryStream = new MemoryStream())
            {
                serializer.Serialize(XmlWriter.Create(memoryStream, xmlwriterSettings), postComment, emptyNamepsaces);
                memoryStream.Flush();
                memoryStream.Seek(0, SeekOrigin.Begin);

                using (var reader = new StreamReader(memoryStream))
                {
                    postCommentXml = reader.ReadToEnd();
                }
            }

            Debug.WriteLine(postCommentXml);

            return await context.PostAsync(commentServerUrl, postCommentXml);
        }


        public static PostCommentResponse ParsePostCommentResult(string postCommentResult)
        {
            // 成功していればchatクラスのXMLがかえってくる
            // TODO: 
            PostCommentResponse res = null;
            var serializer = new XmlSerializer(typeof(PostCommentResponse));
            using (var reader = new StringReader(postCommentResult))
            {
                res = (PostCommentResponse)serializer.Deserialize(reader);
            }
            Debug.WriteLine(postCommentResult);
            return res;
        }

        public static Task<PostCommentResponse> PostCommentAsync(
            NiconicoContext context,
            string commentServerUrl,
            string threadId,
            string ticket,
            int commentCount,
            string comment,
            TimeSpan position,
            string commands
            )
        {
            return PostCommentDataAsync(context, commentServerUrl, threadId, ticket, commentCount, comment, position, commands)
                .ContinueWith(prevResult => ParsePostCommentResult(prevResult.Result));
        }

    }


    public enum ThreadType
    {
        UserVideo = 1,
        UserVideoWithOwnerComment,
        ChannelVideo,
    }


    [XmlRoot(ElementName = "chat")]
	public class PostComment
	{
		[XmlAttribute]
		public string thread { get; set; }
		[XmlAttribute]
		public string vpos { get; set; }
		[XmlAttribute]
		public string mail { get; set; }
		[XmlAttribute]
		public string ticket { get; set; }
		[XmlAttribute]
		public string user_id { get; set; }
		[XmlAttribute]
		public string postkey { get; set; }
		[XmlAttribute]
		public string premium { get; set; }

		[XmlText]
		public string comment { get; set; }
	}
}
