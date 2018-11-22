using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Videos.Comment
{
    [DataContract]
    public class PingItem
    {
        public PingItem()
        {

        }

        public PingItem(string content)
        {
            Ping.Content = content;
        }


        [DataMember(Name = "ping")]
        public Ping Ping { get; set; } = new Ping();
    }


    [DataContract]
    public class Ping
    {
        [DataMember(Name = "content")]
        public string Content { get; set; }
    }

    [DataContract]
    public class ThreadItem
    {
        [DataMember(Name = "thread")]
        public Thread_CommentRequest Thread { get; set; }
    }

    [DataContract]
    public class Thread_CommentRequest
    {
        [DataMember(Name = "fork")]
        public int? Fork { get; set; } = null;

        [DataMember(Name = "thread")]
        public string ThreadId { get; set; }

        [DataMember(Name = "version")]
        public string Version { get; set; } = "20090904"; // 投コメ取得の場合だけ "20061206"

        [DataMember(Name = "language")]
        public int? Language { get; set; } = 0;

        [DataMember(Name = "user_id")]
        public string UserId { get; set; }

        [DataMember(Name = "with_global")]
        public int? WithGlobal { get; set; } = 1;

        [DataMember(Name = "scores")]
        public int? Scores { get; set; } = 1;

        [DataMember(Name = "nicoru")]
        public int? Nicoru { get; set; } = 0;

        [DataMember(Name = "userkey")]
        public string Userkey { get; set; }

        [DataMember(Name = "force_184")]
        public string Force184 { get; set; } = null; // 公式動画のみ"1"

        [DataMember(Name = "threadkey")]
        public string Threadkey { get; set; } = null; // 公式動画のみ必要

        [DataMember(Name = "res_from")]
        public int? ResFrom { get; set; } = null;

    }

    [DataContract]
    public class ThreadLeavesItem
    {
        [DataMember(Name = "thread_leaves")]
        public ThreadLeaves ThreadLeaves { get; set; }
    }

    [DataContract]
    public class ThreadLeaves
    {

        [DataMember(Name = "thread")]
        public string ThreadId { get; set; }

        [DataMember(Name = "language")]
        public int Language { get; set; } = 0;

        [DataMember(Name = "user_id")]
        public string UserId { get; set; }

        [DataMember(Name = "content")]
        public string Content { get; set; }

        [DataMember(Name = "scores")]
        public int Scores { get; set; } = 1;

        [DataMember(Name = "nicoru")]
        public int Nicoru { get; set; } = 0;

        [DataMember(Name = "userkey")]
        public string Userkey { get; set; }

        [DataMember(Name = "force_184")]
        public string Force184 { get; set; } = null; // 公式動画のみ"1"

        [DataMember(Name = "threadkey")]
        public string Threadkey { get; set; } = null; // 公式動画のみ必要



        public static string MakeContentString(TimeSpan videoLength)
        {
            return $"0-{(int)(Math.Ceiling(videoLength.TotalMinutes))}:100,1000";
        }
    }

    [DataContract]
    public sealed class PostChatData
    {
        [DataMember(Name = "chat")]
        public PostChat Chat { get; set; }
    }

    [DataContract]
    public sealed class PostChat
    {
        [DataMember(Name = "thread")]
        public string ThreadId { get; set; }

        [DataMember(Name = "vpos")]
        public int Vpos { get; set; }

        [DataMember(Name = "mail")]
        public string Mail { get; set; }

        [DataMember(Name = "ticket")]
        public string Ticket { get; set; }

        [DataMember(Name = "user_id")]
        public string UserId { get; set; }

        [DataMember(Name = "content")]
        public string Content { get; set; }

        [DataMember(Name = "postkey")]
        public string PostKey { get; set; }

        [DataMember(Name = "premium")]
        public string Premium { get; set; } = "0"; // 一般ユーザー:0 プレミアム会員:1

    }


}
