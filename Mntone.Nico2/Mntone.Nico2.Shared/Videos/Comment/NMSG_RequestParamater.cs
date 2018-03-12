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
        public Thread Thread { get; set; }
    }

    [DataContract]
    public class Thread
    {

        [DataMember(Name = "thread")]
        public string ThreadId { get; set; }

        [DataMember(Name = "version")]
        public string Version { get; set; } = "20090904";

        [DataMember(Name = "language")]
        public int Language { get; set; } = 0;

        [DataMember(Name = "user_id")]
        public int UserId { get; set; }

        [DataMember(Name = "with_global")]
        public int WithGlobal { get; set; } = 1;

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
    }

    [DataContract]
    public class OwnerThreadItem
    {
        [DataMember(Name = "thread")]
        public OwnerThread Thread { get; set; }
    }

    [DataContract]
    public class OwnerThread
    {

        [DataMember(Name = "thread")]
        public string ThreadId { get; set; }

        [DataMember(Name = "version")]
        public string Version { get; set; } = "20061206";

        [DataMember(Name = "user_id")]
        public int UserId { get; set; }

        [DataMember(Name = "with_global")]
        public int WithGlobal { get; set; } = 1;

        [DataMember(Name = "scores")]
        public int Scores { get; set; } = 1;

        [DataMember(Name = "nicoru")]
        public int Nicoru { get; set; } = 0;

        [DataMember(Name = "userkey")]
        public string Userkey { get; set; }

        [DataMember(Name = "fork")]
        public int Fork { get; set; } = 1;

        [DataMember(Name = "res_from")]
        public int ResFrom { get; set; } = -1000;
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
        public int UserId { get; set; }

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
    }





    public static class NMSG_RequestParamaterBuilder
    {
        // normal video comment request parameter sample 
        /* 
[{
    "ping": {
            "content": "rs:0"
        }
    }, {
        "ping": {
            "content": "ps:0"
        }
    }, {
        "thread": {
            "thread": "1463483922",
            "version": "20090904",
            "language": 0,
            "user_id": 53842185,
            "with_global": 1,
            "scores": 1,
            "nicoru": 0,
            "userkey": "1502173042.~1~MzCxfaTZL7rDZztXT4fhmR3fXdyv-_24iGol36KOkRA"
        }
    }, {
        "ping": {
            "content": "pf:0"
        }
    }, {
        "ping": {
            "content": "ps:1"
        }
    }, {
        "thread_leaves": {
            "thread": "1463483922",
            "language": 0,
            "user_id": 53842185,
            "content": "0-22:100,1000", // 0-22 の22は動画時間（秒は切り上げ）
            "scores": 1,
            "nicoru": 0,
            "userkey": "1502173042.~1~MzCxfaTZL7rDZztXT4fhmR3fXdyv-_24iGol36KOkRA"
        }
    }, {
        "ping": {
            "content": "pf:1"
        }
    }, {
        "ping": {
            "content": "rf:0"
        }
}]
    */
        public static string MakeVideoCommmentRequest(string threadId, int userId, string userKey, TimeSpan video_length, bool hasOwnerThread)
        {
            object[] parameters = null;
            if (!hasOwnerThread)
            {
                parameters = new object[]
                {
                    new PingItem("rs:0"),
                    new PingItem("ps:0"),
                    new ThreadItem()
                    {
                        Thread = new Thread()
                        {
                            ThreadId = threadId,
                            UserId = userId,
                            Userkey = userKey
                        }
                    },
                    new PingItem("pf:0"),
                    new PingItem("ps:1"),
                    new ThreadLeavesItem()
                    {
                        ThreadLeaves = new ThreadLeaves()
                        {
                            ThreadId = threadId,
                            UserId = userId,
                            Content = ThreadLeaves.MakeContentString(video_length),
                            Userkey = userKey,
                            Scores = 1,
                            Nicoru = 0,
                        }
                    },
                    new PingItem("pf:1"),
                    new PingItem("rf:0"),
                };
            }
            else
            {
                parameters = new object[]
                {
                    new PingItem("rs:0"),
                    new PingItem("ps:0"),
                    new ThreadItem()
                    {
                        Thread = new Thread()
                        {
                            ThreadId = threadId,
                            UserId = userId,
                            Userkey = userKey
                        }
                    },
                    new PingItem("pf:0"),
                    new PingItem("ps:1"),
                    new ThreadLeavesItem()
                    {
                        ThreadLeaves = new ThreadLeaves()
                        {
                            ThreadId = threadId,
                            UserId = userId,
                            Content = ThreadLeaves.MakeContentString(video_length),
                            Userkey = userKey,
                            Scores = 1,
                            Nicoru = 0,
                        }
                    },
                    new PingItem("pf:1"),
                    new PingItem("ps:2"),
                    new OwnerThreadItem()
                    {
                        Thread = new OwnerThread()
                        {
                            ThreadId = threadId,
                            UserId = userId,
                            Userkey = userKey
                        }
                    },
                    new PingItem("pf:2"),
                    new PingItem("rf:0"),
                };
            }
            
        

            var requestParamsJson = JsonConvert.SerializeObject(parameters, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,

            });

            return requestParamsJson;
        }

        // Official Video comment reqeust paramter sample

        /*
[{
	"ping": {
		"content": "rs:0"
	}
	}, {
		"ping": {
			"content": "ps:0"
		}
	}, {
		"thread": {
			"thread": "1501742473",
			"version": "20090904",
			"language": 0,
			"user_id": 53842185,
			"with_global": 1,
			"scores": 1,
			"nicoru": 0,
			"userkey": "1502173804.~1~fwFqcTlwtEbO4ggddXkZLdbowXV9TrcE_NTbhDTmFlo"
		}
	}, {
		"ping": {
			"content": "pf:0"
		}
	}, {
		"ping": {
			"content": "ps:1"
		}
	}, {
		"thread_leaves": {
			"thread": "1501742473",
			"language": 0,
			"user_id": 53842185,
			"content": "0-13:100,1000",
			"scores": 1,
			"nicoru": 0,
			"userkey": "1502173804.~1~fwFqcTlwtEbO4ggddXkZLdbowXV9TrcE_NTbhDTmFlo"
		}
	}, {
		"ping": {
			"content": "pf:1"
		}
	}, {
		"ping": {
			"content": "ps:2"
		}
	}, {
		"thread": {
			"thread": "1501742474",
			"version": "20090904",
			"language": 0,
			"user_id": 53842185,
			"force_184": "1",
			"with_global": 1,
			"scores": 1,
			"nicoru": 0,
			"threadkey": "1502173806.e_qPpM9yX3kUgW80nVYo32EdDCU"
		}
	}, {
		"ping": {
			"content": "pf:2"
		}
	}, {
		"ping": {
			"content": "ps:3"
		}
	},
	{
		"thread_leaves": {
			"thread": "1501742474",
			"language": 0,
			"user_id": 53842185,
			"content": "0-13:100,1000",
			"scores": 1,
			"nicoru": 0,
			"force_184": "1",
			"threadkey": "1502173806.e_qPpM9yX3kUgW80nVYo32EdDCU"
		}
	}, {
		"ping": {
			"content": "pf:3"
		}
	}, {
		"ping": {
			"content": "rf:0"
		}
	}
]
        */

        public static string MakeOfficialVideoCommmentRequest(
            string threadId, 
            string sub_threadId, 
            string threadKey, 
            int userId,
            string userKey, 
            TimeSpan videoLength,
            bool force184
            )
        {

            var threadLeavesContent = ThreadLeaves.MakeContentString(videoLength);
            object[] paramter = new object[]
            {
                new PingItem("rs:0"),
                new PingItem("ps:0"),
                new ThreadItem()
                {
                    Thread = new Thread()
                    {
                        ThreadId = sub_threadId,
                        UserId = userId,
                        Userkey = userKey
                    }
                },
                new PingItem("pf:0"),
                new PingItem("ps:1"),
                new ThreadLeavesItem()
                {
                    ThreadLeaves = new ThreadLeaves()
                    {
                        ThreadId = sub_threadId,
                        UserId = userId,
                        Content = threadLeavesContent,
                        Userkey = userKey,
                        Scores = 1,
                        Nicoru = 0,
                    }
                },
                new PingItem("pf:1"),
                new PingItem("ps:2"),

                new ThreadItem()
                {
                    Thread = new Thread()
                    {
                        ThreadId = threadId,
                        UserId = userId,
                        Threadkey = threadKey,
                        Force184 = force184 ? "1" : null,
                    }
                },
                new PingItem("pf:2"),
                new PingItem("ps:3"),
                new ThreadLeavesItem()
                {
                    ThreadLeaves = new ThreadLeaves()
                    {
                        ThreadId = threadId,
                        UserId = userId,
                        Content = threadLeavesContent,
                        Threadkey = threadKey,
                        Force184 = force184 ? "1" : null,
                        Scores = 1,
                        Nicoru = 0,
                    }
                },
                new PingItem("pf:3"),
                new PingItem("rf:0"),
            };

            var requestParamsJson = JsonConvert.SerializeObject(paramter, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
            });

            return requestParamsJson;
        }


        


        public static string MakeOfficialVideoPostCommmentRequest(
            string content,
            int vpos,
            string mail,
            string threadId,
            string ticket,
            int userId,
            string postkey
            )
        {
            object[] paramter = new object[]
            {
                new PingItem("rs:1"),
                new PingItem("ps:14"),
                new PostChatData()
                {
                    Chat = new PostChat()
                    {
                        ThreadId = threadId,
                        Vpos = vpos,
                        Mail = mail,
                        Ticket = ticket,
                        UserId = userId.ToString(),
                        Content = content,
                        PostKey = postkey,
                    }
                },
                new PingItem("pf:14"),
                new PingItem("rf:1"),
            };

            var requestParamsJson = JsonConvert.SerializeObject(paramter, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
            });

            return requestParamsJson;
        }
    }
}
