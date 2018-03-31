using Mntone.Nico2.Videos.Flv;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            return await context.GetClient()
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
            paramDict.Add("user_id", userId.ToString());
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

            return await context.GetClient()
                .GetStringAsync(commentUrl);
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


            var content = new Windows.Web.Http.HttpStringContent(postCommentXml);

            return await context.PostAsync(commentServerUrl, content);
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




        #region NMSG コメント取得





        public static Task<string> GetNMSGCommentDataAsync(NiconicoContext context, long threadId, int userId, string userKey, TimeSpan videoLength, bool hasOwnerThread)
        {
            var parameterJson = NMSG_RequestParamaterBuilder
                .MakeVideoCommmentRequest(threadId.ToString(), userId, userKey, videoLength, hasOwnerThread);

            var stringContent = new Windows.Web.Http.HttpStringContent(parameterJson);

            return context.PostAsync(NiconicoUrls.NmsgCommentApiUrl, stringContent);
        }

        public static Task<NMSG_Response> GetNMSGCommentAsync(NiconicoContext context, long threadId, int userId, string userKey, TimeSpan videoLength, bool hasOwnerThread)
        {
            return GetNMSGCommentDataAsync(context, threadId, userId, userKey, videoLength, hasOwnerThread)
                .ContinueWith(prevTask => ParseNMSGResponseJson(prevTask.Result));
        }





        public static async Task<string> GetOfficialVideoNMSGCommentDataAsync(
            NiconicoContext context,
            long threadId,
            long sub_threadId,
            int userId,
            string userKey,
            TimeSpan videoLength
            )
        {
            var threadKeyResponse = await GetThreadKeyDataAsync(context, threadId)
                    .ContinueWith(prevTask => ParseThreadKey(prevTask.Result));

            if (threadKeyResponse == null)
            {
                throw new Exception("can not get ThreadKey, threadId is " + threadId);
            }

            var threadKey = threadKeyResponse.ThreadKey;
            var force184 = threadKeyResponse.Force184.ToBooleanFrom1();

            var parameterJson = NMSG_RequestParamaterBuilder
                .MakeOfficialVideoCommmentRequest(threadId.ToString(), sub_threadId.ToString(), threadKey, userId, userKey, videoLength, force184);

            var stringContent = new Windows.Web.Http.HttpStringContent(parameterJson);

            return await context.PostAsync(NiconicoUrls.NmsgCommentApiUrl, stringContent);
        }


        public static Task<NMSG_Response> GetOfficialVideoNMSGCommentAsync(
            NiconicoContext context,
            long threadId,
            long sub_threadId,
            int userId,
            string userKey,
            TimeSpan videoLength
            )
        {
            return GetOfficialVideoNMSGCommentDataAsync(context, threadId, sub_threadId, userId, userKey, videoLength)
                .ContinueWith(prevTask => ParseNMSGResponseJson(prevTask.Result));
        }





        private static NMSG_Response ParseNMSGResponseJson(string json)
        {
            var result = JsonConvert.DeserializeObject<List<object>>(json);

            var res = new NMSG_Response();

            foreach (var item in result.Cast<JObject>())
            {
                Debug.WriteLine(item.Path);
                if (item.TryGetValue("thread", out var thread_token))
                {
                     res.Threads.Add(thread_token.ToObject<NGMS_Thread_Response>());
                }
                else if (item.TryGetValue("global_num_res", out var globalNumRes_token))
                {
                    if (res.GlobalNumRes == null)
                    {
                        res.GlobalNumRes = globalNumRes_token.ToObject<NGMS_GlobalNumRes>();
                    }
                }
                else if (item.TryGetValue("chat", out var chat_token))
                {
//                    var chat = chat_token.ToObject<NMSG_Chat>();

                    res._CommentsSource.Add(chat_token);
                }

                // pingとleafは不要？
            }


            return res;
        }



        #endregion



        #region NMSG コメント投稿

        // コメ取得
        // コメ投稿１回目
        // コメ投稿２回目
        // 公式プレイヤーだと差分コメの問い合わせによって変動している


        //投コメナシの場合
        //rs 0 ps 0
        //pf 0 ps 1
        //pf 1 rf 0
        //
        //rs 1 ps 8
        //pf 8 rf 1
        //
        //rs 2 ps 13
        //pf 13 rf 2


        //投コメアリの場合のpsとかの動き
        //rs 0 ps 0
        //pf 0 ps 1
        //pf 1 ps 2
        //pf 2 rf 0
        //
        //rs 1 ps11
        //pf 11 rf 1
        //
        //rs 2 ps 16
        //pf 16 rf 2


        //公式アニメなどサブスレッドまで問い合わせる場合
        //rs 0 ps 0
        //pf 0 ps 1
        //pf 1 ps 2
        //pf 2 ps 3
        //pf 3 rf 0
        //
        //rs 1 ps 14
        //pf 14 rf 1
        //
        //rs 2 ps 19
        //pf 19 rf 2


        public static async Task<string> NMSGPostCommentDataAsync(
            NiconicoContext context,
            string threadId,
            string ticket,
            int commentCount,
            int userid,
            string comment,
            TimeSpan position,
            string command,
            string postKey,
            ThreadType threadInitialType,
            int commentSequenceNumber
            )
        {

            Debug.WriteLine(postKey);

            var postCommentMessage = NMSG_RequestParamaterBuilder.MakeVideoPostCommmentRequest(
                comment,
                ((int)position.TotalMilliseconds / 10),
                command,
                threadId,
                ticket,
                userid,
                postKey,
                commentSequenceNumber,
                threadInitialType
                );
                        
            Debug.WriteLine(postCommentMessage);

            var content = new Windows.Web.Http.HttpStringContent(postCommentMessage);

            return await context.PostAsync(NiconicoUrls.NmsgCommentApiUrl, content);
        }


        public static PostCommentResponse NMSGParsePostCommentResult(string postCommentResult)
        {
            var startStr = "{\"chat_result";
            var endStr = "}}";

            var startPos = postCommentResult.IndexOf(startStr);
            if (startPos == -1) { return null; }

            var endPos = postCommentResult.IndexOf(endStr, startPos) + endStr.Length;
            if (endPos == -1) { return null; }

            var chatResultText = postCommentResult.Substring(startPos, endPos - startPos);

            return JsonConvert.DeserializeObject<PostCommentResponse>(chatResultText);
        }

        // 単一アカウントから利用されることを前提とした危険な実装です
        private static string _LastPostKey;

        private static string _LastThreadId;
        private static int _CommentRequestCount;

        public static async Task<PostCommentResponse> NMSGPostCommentAsync(
            NiconicoContext context,
            string threadId,
            string ticket,
            int commentCount,
            int userId,
            string comment,
            TimeSpan position,
            string commands,
            ThreadType threadInitialType
            )
        {
            foreach (var _ in Enumerable.Range(0, 2))
            {
                // postkeyの取得
                if (_LastPostKey == null)
                {
                    _LastPostKey = await GetPostKeyAsync(context, threadId, commentCount)
                        .ContinueWith(prevResult => ParsePostKey(prevResult.Result));
                }

                if (_LastThreadId != threadId)
                {
                    _LastThreadId = threadId;
                    _CommentRequestCount = 1;
                }

                var res = await NMSGPostCommentDataAsync(context, threadId, ticket, commentCount, userId, comment, position, commands, _LastPostKey, threadInitialType, _CommentRequestCount);
                var result = NMSGParsePostCommentResult(res);

                _CommentRequestCount++;

                if (result.Chat_result.Status == ChatResult.InvalidPostkey)
                {
                    _LastPostKey = null;
                }
                else
                {
                    return result;
                }
            }

            // 2回 postkey が無効となった場合は諦める
            return null;
        }


        #endregion










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
