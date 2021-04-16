using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Videos.Comment
{

    public interface ICommentSessionContext
    {

    }

    public abstract class CommentSessionContextBase : ICommentSessionContext
    {
       

    }


    public sealed class OldCommentSessionContext : CommentSessionContextBase
    {

    }

    public sealed class CommentSessionContext : CommentSessionContextBase
    {
        public NiconicoContext Context { get; private set; }

        public Dmc.Comment CommenctComposite { get; private set; }
        public string UserId { get; private set; }
        public string UserKey { get; private set; }
        public bool IsPremium { get; private set; }

        private Dmc.Thread DefaultPostTargetThread => CommenctComposite.Threads.FirstOrDefault(x => x.IsDefaultPostTarget);

        private string _DefaultPostTargetThreadId;
        private string DefaultPostTargetThreadId => _DefaultPostTargetThreadId ?? (_DefaultPostTargetThreadId = DefaultPostTargetThread.Id.ToString());

        private int _SubmitTimes = 0;
        private int _SeqNum = 0;

        private string _ThreadLeavesContentString;

        private int _LastRes = 0;

        private string _Ticket = null;

        public bool CanPostComment => _Ticket != null;



        internal CommentSessionContext(NiconicoContext context, Dmc.DmcWatchResponse dmc)
        {
            Context = context;

            CommenctComposite = dmc.Comment;
            var userId = dmc.Viewer?.Id.ToString();
            UserId = userId == null || userId == "0" ? "" : userId;
            UserKey = dmc.Comment.Keys.UserKey;
            IsPremium = dmc.Viewer?.IsPremium ?? false;

            //_LastRes = dmc.Thread.CommentCount;

            _ThreadLeavesContentString = ThreadLeaves.MakeContentString(TimeSpan.FromSeconds(dmc.Video.Duration));
        }


        /// <summary>
        /// コメントサーバーにコメントコマンドリストを
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private async Task<string> SendCommentCommandAsync(Uri server, object[] parameter)
        {
            var requestParamsJson = JsonConvert.SerializeObject(parameter, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
            });

            return await Context.PostAsync($"{server.OriginalString}/api.json", requestParamsJson);
        }


        private void IncrementSequenceNumber(int incrementCount)
        {
            _SeqNum += incrementCount;
            _SubmitTimes += 1;
        }



        #region Get Comments

        Dictionary<int, ThreadKeyResponse> _ThreadIdToThreadKey = new Dictionary<int, ThreadKeyResponse>();
        private async Task<ThreadKeyResponse> GetThreadKeyAsync(int threadId)
        {
            if (_ThreadIdToThreadKey.ContainsKey(threadId)) { return _ThreadIdToThreadKey[threadId]; }

            var threadKeyText = await CommentClient.GetThreadKeyDataAsync(Context, threadId);
            var threadKeyRes = CommentClient.ParseThreadKey(threadKeyText);

            _ThreadIdToThreadKey.Add(threadId, threadKeyRes);

            return threadKeyRes;
        }
        
        public async Task<NMSG_Response> GetCommentFirstAsync()
        {
            var hasCommunityThread = CommenctComposite.Threads.Any(x => x.Label == "community");

            List<object> commentCommandList = new List<object>();

            commentCommandList.Add(new PingItem($"rs:{_SubmitTimes}"));
            var seqNum = _SeqNum;
            foreach (var thread in CommenctComposite.Threads)
            {
                if (!thread.IsActive || thread.Label == "easy")
                {
                    continue;
                }

                commentCommandList.Add(new PingItem($"ps:{_SeqNum + seqNum}"));

                ThreadKeyResponse threadKey = null;
                if (thread.IsThreadkeyRequired)
                {
                    threadKey = await GetThreadKeyAsync(thread.Id);
                }

                if (thread.IsOwnerThread)
                {
                    commentCommandList.Add(new ThreadItem()
                    {
                        Thread = new Thread_CommentRequest()
                        {
                            Fork = thread.Fork,
                            UserId = UserId,
                            ThreadId = thread.Id.ToString(),
                            Version = "20061206",
                            Userkey = UserKey,

                            ResFrom = -1000,
                        }
                    });
                }
                else
                {
                    commentCommandList.Add(new ThreadItem()
                    {
                        Thread = new Thread_CommentRequest()
                        {
                            Fork = thread.Fork,
                            UserId = UserId,
                            ThreadId = thread.Id.ToString(),
                            Version = "20090904",
                            Userkey = !thread.IsThreadkeyRequired ? UserKey : null,

                            Threadkey = threadKey?.ThreadKey,
                            Force184 = threadKey?.Force184,
                        }
                    });
                }


                commentCommandList.Add(new PingItem($"pf:{_SeqNum + seqNum}"));

                ++seqNum;

                if (thread.IsLeafRequired)
                {
                    commentCommandList.Add(new PingItem($"ps:{_SeqNum + seqNum}"));

                    commentCommandList.Add(new ThreadLeavesItem()
                    {
                        ThreadLeaves = new ThreadLeaves()
                        {
                            UserId = UserId,
                            ThreadId = thread.Id.ToString(),
                            Userkey = !thread.IsThreadkeyRequired ? UserKey : null,

                            Threadkey = threadKey?.ThreadKey,
                            Force184 = threadKey?.Force184,

                            Content = _ThreadLeavesContentString,
                        }
                    });

                    commentCommandList.Add(new PingItem($"pf:{_SeqNum + seqNum}" ));

                    ++seqNum;
                }
            }

            commentCommandList.Add(new PingItem($"rf:{_SubmitTimes}"));


            // コメント取得リクエストを送信
            var responseString = await SendCommentCommandAsync(DefaultPostTargetThread.Server, commentCommandList.ToArray());

            // コメント取得結果をパース
            var res = ParseNMSGResponseJson(responseString);

            var defaultPostThreadInfo = res.Threads.FirstOrDefault(x => x.Thread == DefaultPostTargetThreadId);
            if (defaultPostThreadInfo != null)
            {
                _LastRes = defaultPostThreadInfo.LastRes;
                _Ticket = defaultPostThreadInfo.Ticket ?? _Ticket;

                IncrementSequenceNumber(commentCommandList.Count);
            }

            return res;
        }

        /// <summary>
        /// 動画のコメント取得。コメント投稿後に差分を取得する際に使用する。
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public async Task<NMSG_Response> GetDifferenceCommentAsync()
        {
            ThreadKeyResponse threadKey = null;
            if (DefaultPostTargetThread.IsThreadkeyRequired)
            {
                threadKey = await GetThreadKeyAsync(DefaultPostTargetThread.Id);
            }

            object[] commentCommandList = new object[]
            {
                new PingItem($"rs:{_SubmitTimes}"),
                new PingItem($"ps:{_SeqNum}"),
                new ThreadItem()
                {
                    Thread = new Thread_CommentRequest()
                    {
                        Fork = DefaultPostTargetThread.Fork,
                        UserId = UserId,
                        ThreadId = DefaultPostTargetThreadId,
                        Version = "20061206",
                        Userkey = UserKey,
                        ResFrom = _LastRes,

                        Threadkey = threadKey?.ThreadKey,
                        Force184 = threadKey?.Force184,
                    }
                },
                new PingItem($"pf:{_SeqNum}"),
                new PingItem($"rf:{_SubmitTimes}"),
            };

            // コメント取得リクエストを送信
            var responseString = await SendCommentCommandAsync(DefaultPostTargetThread.Server, commentCommandList);

            // コメント取得結果をパース
            var res = ParseNMSGResponseJson(responseString);


            var defaultPostThreadInfo = res.Threads.FirstOrDefault(x => x.Thread == DefaultPostTargetThreadId);
            if (defaultPostThreadInfo != null)
            {
                _LastRes = defaultPostThreadInfo.LastRes;
                _Ticket = defaultPostThreadInfo.Ticket ?? _Ticket;

                IncrementSequenceNumber(commentCommandList.Length);
            }

            return res;
        }



        private static NMSG_Response ParseNMSGResponseJson(string json)
        {
            var result = JsonConvert.DeserializeObject<List<object>>(json);

            var res = new NMSG_Response();

            foreach (var item in result.Cast<JObject>())
            {
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


        #region Post Comment

        private string _PostKey;
        private async Task<string> GetPostKeyAsync(string threadId, int commentCount, bool forceRefresh = false)
        {
            if (!forceRefresh && _PostKey != null) { return _PostKey; }

            _PostKey = await CommentClient.GetPostKeyAsync(Context, threadId, commentCount);

            return _PostKey;
        }

        private async Task<PostCommentResponse> PostCommentAsync_Internal(TimeSpan posision, string comment, string mail, string ticket, string postKey)
        {
            if (DefaultPostTargetThread == null)
            {
                throw new NotSupportedException("not found default comment post target.");
            }

            var vpos = (int)(posision.TotalMilliseconds * 0.1);
            object[] commentCommandList = new object[]
            {
                new PingItem($"rs:{_SubmitTimes}"),
                new PingItem($"ps:{_SeqNum}"),
                new PostChatData()
                {
                    Chat = new PostChat()
                    {
                        ThreadId = DefaultPostTargetThreadId,
                        Vpos = vpos,
                        Mail = mail,
                        Ticket = _Ticket,
                        UserId = UserId,
                        Content = comment,
                        PostKey = postKey,
                        Premium = IsPremium ? "1" : "0"
                    }
                },
                new PingItem($"pf:{_SeqNum}"),
                new PingItem($"rf:{_SubmitTimes}"),
            };

            var resString = await SendCommentCommandAsync(DefaultPostTargetThread.Server, commentCommandList);

            var res = NMSGParsePostCommentResult(resString);

            if (res?.Chat_result.__Status == (int)ChatResult.Success)
            {
                _LastRes = res.Chat_result.No;
            }

            IncrementSequenceNumber(commentCommandList.Length);

            return res;
        }


        /// <summary>
        /// コメント投稿。登校前に<see cref="GetCommentFirstAsync"/>、または<see cref="GetDifferenceCommentAsync"/>を呼び出して投稿のためのTicketを取得しておく必要があります。
        /// ログインしていない場合はコメント投稿は出来ません。
        /// </summary>
        /// <param name="position"></param>
        /// <param name="comment"></param>
        /// <param name="mail"></param>
        /// <returns></returns>
        public async Task<PostCommentResponse> PostCommentAsync(TimeSpan position, string comment, string mail)
        {
            if (_Ticket == null)
            {
                throw new Exception("not found posting ticket. once call GetCommentFirstAsync() then filling ticket in CommentSessionContext class inside.");
            }



            var postKey = await GetPostKeyAsync(DefaultPostTargetThreadId, _LastRes);
            var res = await PostCommentAsync_Internal(position, comment, mail, _Ticket, postKey);

            if (res.Chat_result.__Status == (int)ChatResult.InvalidPostkey || res.Chat_result.__Status == (int)ChatResult.InvalidTichet)
            {
                // 最新のコメント数とチケットを取得
                await GetDifferenceCommentAsync();

                // ポストキーを再取得
                postKey = await GetPostKeyAsync(DefaultPostTargetThreadId, _LastRes, forceRefresh: true);

                res = await PostCommentAsync_Internal(position, comment, mail, _Ticket, postKey);
            }

            return res;
        }



        private static PostCommentResponse NMSGParsePostCommentResult(string postCommentResult)
        {
            var jsonObject = (JArray)JsonConvert.DeserializeObject(postCommentResult);

            foreach (var token in jsonObject.Cast<JObject>())
            {
                if (token.TryGetValue("chat_result", out var chatResultToken))
                {
                    return new PostCommentResponse { Chat_result = chatResultToken.ToObject<Chat_result>() };
                }
            }

            throw new NotSupportedException(postCommentResult);
        }

        #endregion

    }
}
