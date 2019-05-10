using Mntone.Nico2.Videos.WatchAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Videos.Dmc
{
    internal sealed class DmcClient
    {
        #region DmcWatchResponse

        public static async Task<string> GetDmcWatchJsonDataAsync(NiconicoContext context, string requestId, string playlistToken)
        {
            if (!NiconicoRegex.IsVideoId(requestId))
            {
                //				throw new ArgumentException();
            }

            var dict = new Dictionary<string, string>();
            var url = $"{NiconicoUrls.VideoWatchPageUrl}{requestId}";

            dict.Add("mode", "pc_html5");
            dict.Add("eco", "0");
            dict.Add("playlist_token", playlistToken);
            dict.Add("watch_harmful", ((uint)HarmfulContentReactionType.ContinueWithNotMoreConfirm).ToString());
            dict.Add("continue_watching", "1");

            url += "?" + HttpQueryExtention.DictionaryToQuery(dict);


            var requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(url));

            requestMessage.Headers.Add("Accept", "*/*");

            try
            {
                var res = await context.GetClient().SendAsync(requestMessage);

                if (res.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new WebException("require payment.");
                }

                var text = await res.Content.ReadAsStringAsync();
                return text;

            }
            catch (ContentZoningException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new WebException("access failed watch/" + requestId, e);
            }

        }


        public static DmcWatchResponse ParseDmcWatchJsonData(string htmlString)
        {
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.NullValueHandling = NullValueHandling.Include;
            jsonSerializer.DefaultValueHandling = DefaultValueHandling.Include;

            var dmcWatchResponse = jsonSerializer.Deserialize<DmcWatchResponse>(new JsonTextReader(new StringReader(htmlString)));
            return dmcWatchResponse;
        }


        public static Task<DmcWatchResponse> GetDmcWatchJsonAsync(NiconicoContext context, string requestId, string playlistToken)
        {
            return GetDmcWatchJsonDataAsync(context, requestId, playlistToken)
                .ContinueWith(prevTask => ParseDmcWatchJsonData(prevTask.Result));
        }


        public static async Task<string> GetDmcWatchResponseDataAsync(NiconicoContext context, string requestId, HarmfulContentReactionType harmfulReactType)
        {
            if (!NiconicoRegex.IsVideoId(requestId))
            {
                //				throw new ArgumentException();
            }

            var dict = new Dictionary<string, string>();
            var url = $"{NiconicoUrls.VideoWatchPageUrl}{requestId}";


            if (harmfulReactType != HarmfulContentReactionType.None)
            {
                dict.Add("watch_harmful", ((uint)harmfulReactType).ToString());
            }

            url += "?" + HttpQueryExtention.DictionaryToQuery(dict);

            try
            {
                var client = context.GetClient();

                var message = new HttpRequestMessage(HttpMethod.Get, new Uri(url));

                message.Headers.Add(@"Cookie", "watch_html5=1, watch_flash=0");

                var res = await context.GetClient()
                    .SendAsync(message);

                if (res.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new WebException("require payment.");
                }

                var text = await res.Content.ReadAsStringAsync();
                return text;
                
            }
            catch (ContentZoningException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new WebException("access failed watch/" + requestId, e);
            }
            
        }


        public static DmcWatchData ParseDmcWatchResponseData(string htmlString)
        {
            var htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(htmlString);

            // 推定有害動画の視聴ブロックページかをチェック
            if (htmlDocument.GetElementbyId("PAGECONTAINER") != null)
            {
                throw new ContentZoningException("access once blocked, maybe harmful video.");
            }
            else
            {
                try
                {
                    var videoInfoNode = htmlDocument.GetElementbyId("js-initial-watch-data");
                    var watchDataRawString = videoInfoNode.GetAttributeValue("data-api-data", "");
                    var htmlDecoded = WebUtility.HtmlDecode(watchDataRawString);

                    var jsonSerializer = new JsonSerializer();
                    jsonSerializer.NullValueHandling = NullValueHandling.Include;
                    jsonSerializer.DefaultValueHandling = DefaultValueHandling.Include;

                    DmcWatchResponse dmcWatchResponse = jsonSerializer.Deserialize<DmcWatchResponse>(new JsonTextReader(new StringReader(htmlDecoded)));

                    var environmentRawString = videoInfoNode.GetAttributeValue("data-environment", "");
                    var environmentHtmlDecoded = WebUtility.HtmlDecode(environmentRawString);

                    DmcWatchEnvironment dmcWatchEnvironment = null;

                    try
                    {
                        dmcWatchEnvironment = jsonSerializer.Deserialize<DmcWatchEnvironment>(new JsonTextReader(new StringReader(environmentHtmlDecoded)));
                    }
                    catch { }

                    return new DmcWatchData()
                    {
                         DmcWatchResponse = dmcWatchResponse,
                         DmcWatchEnvironment = dmcWatchEnvironment
                    };
                }
                catch
                {
                    var videoInfoNode = htmlDocument.GetElementbyId("watchAPIDataContainer");
                    if (videoInfoNode == null)
                    {
                        return null;
                    }

                    var rawStr = videoInfoNode.InnerText;
                    var htmlDecoded = WebUtility.HtmlDecode(rawStr);

                    var jsonSerializer = new JsonSerializer();
                    jsonSerializer.NullValueHandling = NullValueHandling.Include;
                    jsonSerializer.DefaultValueHandling = DefaultValueHandling.Include;

                    var watchApi = jsonSerializer.Deserialize<WatchApiResponse>(new JsonTextReader(new StringReader(htmlDecoded)));
                    var dmcWatchResponse = new DmcWatchResponse()
                    {
                        Video = new Video()
                        {
                            Id = watchApi.videoDetail.id,
                            Description = watchApi.videoDetail.description,
                            OriginalDescription = watchApi.videoDetail.description_original,
                            IsDeleted = watchApi.videoDetail.isDeleted,
                            IsOfficial = watchApi.videoDetail.is_official,
                            IsR18 = watchApi.videoDetail.isR18,
                            //IsAdult = watchApi.videoDetail.is_adult,
                            IsNicowari = watchApi.videoDetail.is_nicowari,
                            Duration = watchApi.videoDetail.length.Value,
                            IsPublic = watchApi.videoDetail.is_public,
                            IsMonetized = watchApi.videoDetail.isMonetized,
                            Width = watchApi.videoDetail.width,
                            Height = watchApi.videoDetail.height,
                            ViewCount = watchApi.videoDetail.viewCount.Value,
                            MylistCount = watchApi.videoDetail.mylistCount.Value,
                            MovieType = watchApi.flashvars.movie_type,
                            OriginalTitle = watchApi.videoDetail.title_original,
                            Title = watchApi.videoDetail.title,
                            SmileInfo = new SmileInfo()
                            {
                                Url = watchApi.VideoUrl.OriginalString,
                            },

                        },
                        Thread = new Thread()
                        {
                            ServerUrl = watchApi.CommentServerUrl.OriginalString,
                            SubServerUrl = watchApi.SubCommentServerUrl.OriginalString,
                            CommentCount = watchApi.videoDetail.commentCount.Value,
                            Ids = new Ids()
                            {
                                Default = watchApi.ThreadId.ToString()
                            }
                        },
                        Viewer = new Viewer()
                        {
                            Id = watchApi.viewerInfo.id,
                            Nickname = watchApi.viewerInfo.nickname,
                            IsPremium = watchApi.viewerInfo.isPremium,
                            IsPrivileged = watchApi.viewerInfo.isPrivileged,
                        },
                        Tags = watchApi.videoDetail.tagList.Select(x => new Tag()
                        {
                            Name = x.tag,
                            Id = x.id,
                            IsCategory = x.cat.HasValue ? x.cat.Value : false,
                            IsLocked = x.lck.ToBooleanFrom1(),
                            IsDictionaryExists = x.dic.HasValue ? x.dic.Value : false,
                        }).ToList(),
                    };

                    if (watchApi.UploaderInfo != null)
                    {
                        dmcWatchResponse.Owner = new Owner()
                        {
                            Id = watchApi.UserId.ToString(),
                            Nickname = watchApi.UserName,
                            IconURL = watchApi.UploaderInfo.icon_url,
                            IsUserMyVideoPublic = watchApi.UploaderInfo.is_user_myvideo_public,
                            IsUserOpenListPublic = watchApi.UploaderInfo.is_user_openlist_public,
                            IsUserVideoPublic = watchApi.UploaderInfo.is_uservideo_public,
                        };
                    }

                    if (watchApi.channelInfo != null)
                    {
                        dmcWatchResponse.Channel = new Channel()
                        {
                            Id = watchApi.channelInfo.id,
                            IconURL = watchApi.channelInfo.icon_url,
                            IsFavorited = watchApi.channelInfo.is_favorited == 1,
                            FavoriteToken = watchApi.channelInfo.favorite_token,
                            FavoriteTokenTime = watchApi.channelInfo.favorite_token_time.Value,
                            Name = watchApi.channelInfo.name,
                        };
                    }

                    return new DmcWatchData()
                    {
                        DmcWatchResponse = dmcWatchResponse
                    };
                }
            }
        }


        public static Task<DmcWatchData> GetDmcWatchResponseAsync(NiconicoContext context, string requestId, HarmfulContentReactionType harmfulReactType)
        {
            return GetDmcWatchResponseDataAsync(context, requestId, harmfulReactType)
                .ContinueWith(prevTask => ParseDmcWatchResponseData(prevTask.Result));
        }

        #endregion


        #region DmcSession

        public static async Task<string> GetDmcSessionResponseDataAsync(
            NiconicoContext context, 
            DmcWatchResponse watchData, 
            VideoContent videoQuality = null, 
            AudioContent audioQuality = null,
            bool hlsMode = false
            )
        {
            var req = new DmcSessionRequest();

            var session = watchData.Video.DmcInfo.SessionApi;
            var qualities = watchData.Video.DmcInfo.Quality;
            var encryption = watchData.Video.DmcInfo.Encryption;

            // リクエストする動画品質を決定します
            // モバイルの時は最後の動画品質をモバイル画質として断定して指定
            // それ以外の場合、対象画質とそれ以下の有効な画質をすべて指定
            var requestVideoQuality = new List<string>();
            if (videoQuality?.Available ?? false)
            {
                requestVideoQuality.Add(videoQuality.Id);
            }
            else
            {
                var fallbackVideoQuality = qualities.Videos.Last();
                requestVideoQuality.Add(fallbackVideoQuality.Id);
            }

            var requestAudioQuality = new List<string>();
            if (audioQuality?.Available ?? false)
            {
                requestAudioQuality.Add(audioQuality.Id);
            }
            else
            {
                var fallbackAudioQuality = qualities.Audios.Last();
                requestAudioQuality.Add(fallbackAudioQuality.Id);
            }

            var sessionUrl = $"{session.Urls[0].Url}?_format=json";
            var useSsl = true; // session.Urls[0].IsSsl;
            var wellKnownPort = session.Urls[0].IsWellKnownPort;
            var protocolName = session.Protocols[0]; // http,hls
            var protocolAuthType = session.Protocols.ElementAtOrDefault(1) ?? "ht2"; // ht2
            req.Session = new RequestSession()
            {
                RecipeId = session.RecipeId,
                ContentId = session.ContentId,
                ContentType = "movie",
                ContentSrcIdSets = new List<ContentSrcIdSet>()
                {
                    new ContentSrcIdSet()
                    {
                        ContentSrcIds = new List<ContentSrcId>()
                        {
                            new ContentSrcId()
                            {
                                SrcIdToMux = new SrcIdToMux()
                                {
                                    VideoSrcIds = requestVideoQuality,
                                    AudioSrcIds = requestAudioQuality
                                },
                            }
                        }
                    }
                },
                TimingConstraint = "unlimited",
                KeepMethod = new KeepMethod()
                {
                    Heartbeat = new Heartbeat() { Lifetime = 120000 }
                },
                Protocol = new Protocol()
                {
                    Name = "http",
                    Parameters = new Protocol.ProtocolParameters()
                    {
                        HttpParameters = new Protocol.HttpParameters()
                        {
                            Parameters = new Protocol.ParametersInfo()
                            {
                                HlsParameters = protocolName == "hls" ? new Protocol.HlsParameters()
                                {
                                    UseSsl = useSsl ? "yes" : "no",
                                    UseWellKnownPort = wellKnownPort ? "yes" : "no",
                                    SegmentDuration = 5000,
                                    TransferPreset = "",
                                    Encryption = encryption != null
                                    ? new Protocol.Encryption()
                                    {
                                        HlsEncryptionV1 = new Protocol.HlsEncryptionV1()
                                        {
                                            EncryptedKey = encryption.HlsEncryptionV1.EncryptedKey,
                                            KeyUri = encryption.HlsEncryptionV1.KeyUri
                                        }
                                    }
                                    : null
                                }
                                : null
                                ,
                                HttpOutputDownloadParameters = protocolName == "http" ? new Protocol.HttpOutputDownloadParameters() : null
                            }
                        }
                    }
                },
                ContentUri = "",
                SessionOperationAuth = new SessionOperationAuth_Request()
                {
                    SessionOperationAuthBySignature = new SessionOperationAuthBySignature_Request()
                    {
                        Token = session.Token,
                        Signature = session.Signature
                    }
                },
                ContentAuth = new ContentAuth_Request()
                {
                    AuthType = "ht2",
                    ContentKeyTimeout = session.ContentKeyTimeout,
                    ServiceId = "nicovideo",
                    ServiceUserId = session.ServiceUserId
                },
                ClientInfo = new ClientInfo()
                {
                    PlayerId = session.PlayerId
                },
                Priority = session.Priority
            };
           
            var requestJson = JsonConvert.SerializeObject(req, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });

//            var decodedJson = WebUtility.HtmlEncode(requestJson);
            var decodedJson = requestJson;

            return await context.PostAsync(sessionUrl, new StringContent(decodedJson, UnicodeEncoding.UTF8, "application/json"));
        }


        public static DmcSessionResponse ParseDmcSessionResponse(
            string json
            )
        {
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.NullValueHandling = NullValueHandling.Include;
            jsonSerializer.DefaultValueHandling = DefaultValueHandling.Include;

            var watchApi = jsonSerializer.Deserialize<DmcSessionResponse>(new JsonTextReader(new StringReader(json)));

            return watchApi;
        }


        public static Task<DmcSessionResponse> GetDmcSessionResponseAsync(
            NiconicoContext context, 
            DmcWatchResponse watchData,
            VideoContent videoQuality = null,
            AudioContent audioQuality = null,
            bool hlsMode = false
            )
        {
            return GetDmcSessionResponseDataAsync(context, watchData, videoQuality, audioQuality, hlsMode)
                .ContinueWith(prevTask => ParseDmcSessionResponse(prevTask.Result));
        }

        #endregion


        #region DmcSessionHeartbeat

        public static async Task DmcSessionFirstHeartbeatAsync(
            NiconicoContext context,
            DmcWatchResponse watch,
            DmcSessionResponse sessionRes
            )
        {
            var session = watch.Video.DmcInfo.SessionApi;
            var sessionUrl = $"{session.Urls[0].Url}/{sessionRes.Data.Session.Id}?_format=json&_method=PUT";

            var message = new HttpRequestMessage(HttpMethod.Options, new Uri(sessionUrl));
            message.Headers.Add("Access-Control-Request-Method", "POST");
            message.Headers.Add("Access-Control-Request-Headers", "content-type");
            message.Headers.UserAgent.Add(context.HttpClient.DefaultRequestHeaders.UserAgent.First());
            var result = await context.GetClient().SendAsync(message, HttpCompletionOption.ResponseHeadersRead);
            if (!result.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine(result.ToString());
            }
        }

        public static async Task DmcSessionHeartbeatAsync(
            NiconicoContext context,
            DmcWatchResponse watch,
            DmcSessionResponse sessionRes
            )
        {
            var session = watch.Video.DmcInfo.SessionApi;
            var sessionUrl = $"{session.Urls[0].Url}/{sessionRes.Data.Session.Id}?_format=json&_method=PUT";

            var message = new HttpRequestMessage(HttpMethod.Post, new Uri(sessionUrl));

            message.Headers.UserAgent.Add(context.HttpClient.DefaultRequestHeaders.UserAgent.First());
            message.Headers.Add("Origin", "http://www.nicovideo.jp");
            message.Headers.Add("Referer", "http://www.nicovideo.jp/watch/" + watch.Video.Id);
            message.Headers.Add("Accept", "application/json");

            var requestJson = JsonConvert.SerializeObject(sessionRes.Data, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            message.Content = new StringContent(requestJson, UnicodeEncoding.UTF8, "application/json");

            var result = await context.GetClient().SendAsync(message, HttpCompletionOption.ResponseHeadersRead);
            if (!result.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine(result.ToString());
            }
        }

        public static async Task DmcSessionLeaveAsync(
            NiconicoContext context,
            DmcWatchResponse watch,
            DmcSessionResponse sessionRes
            )
        {
            var session = watch.Video.DmcInfo.SessionApi;
            var sessionUrl = $"{session.Urls[0].Url}/{sessionRes.Data.Session.Id}?_format=json&_method=DELETE";

            var message = new HttpRequestMessage(HttpMethod.Options, new Uri(sessionUrl));
            message.Headers.Add("Access-Control-Request-Method", "POST");
            message.Headers.Add("Access-Control-Request-Headers", "content-type");
            message.Headers.UserAgent.Add(context.HttpClient.DefaultRequestHeaders.UserAgent.First());
            var result = await context.GetClient().SendAsync(message, HttpCompletionOption.ResponseHeadersRead);
            if (!result.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine(result.ToString());
            }
        }

        public static async Task DmcSessionExitHeartbeatAsync(
            NiconicoContext context,
            DmcWatchResponse watch,
            DmcSessionResponse sessionRes
            )
        {
            var session = watch.Video.DmcInfo.SessionApi;
            var sessionUrl = $"{session.Urls[0].Url}/{sessionRes.Data.Session.Id}?_format=json&_method=DELETE";

            var message = new HttpRequestMessage(HttpMethod.Post, new Uri(sessionUrl));
            message.Headers.Add("Access-Control-Request-Method", "POST");
            message.Headers.Add("Access-Control-Request-Headers", "content-type");
            message.Headers.UserAgent.Add(context.HttpClient.DefaultRequestHeaders.UserAgent.First());
            message.Headers.Add("Accept", "application/json");

            var requestJson = JsonConvert.SerializeObject(sessionRes.Data, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            message.Content = new StringContent(requestJson, UnicodeEncoding.UTF8, "application/json");

            var result = await context.GetClient().SendAsync(message, HttpCompletionOption.ResponseHeadersRead);
            if (!result.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine(result.ToString());
            }
        }

        #endregion


        #region nvapi Watch

        public static async Task<bool> SendOfficialHlsWatchAsync(
            NiconicoContext context,
            string contentId,
            string trackId
            )
        {
            var uri = new Uri($"https://nvapi.nicovideo.jp/v1/2ab0cbaa/watch?t={Uri.EscapeDataString(trackId)}");
            var refererUri = new Uri($"https://www.nicovideo.jp/watch/{contentId}");

            var optionReq = new HttpRequestMessage(HttpMethod.Options, uri);
            optionReq.Headers.Add("Access-Control-Request-Headers", "x-frontend-id,x-frontend-version");
            optionReq.Headers.Add("Access-Control-Request-Method", "GET");
            optionReq.Headers.Referrer = refererUri;
            optionReq.Headers.Add("Origin", "https://www.nicovideo.jp");

            var optionRes = await context.HttpClient.SendAsync(optionReq);

            if (!optionRes.IsSuccessStatusCode) { return false; }

            //var allowHeaders = res.Headers["Access-Control-Allow-Headers"];

            var watchReq = new HttpRequestMessage(HttpMethod.Get, uri);
            
            watchReq.Headers.Add("X-Frontend-Id", "6");
            watchReq.Headers.Add("X-Frontend-Version", "0");
            watchReq.Headers.Referrer = refererUri;
            optionReq.Headers.Add("Origin", "https://www.nicovideo.jp");

            var watchRes = await context.HttpClient.SendAsync(watchReq);

            return watchRes.IsSuccessStatusCode;
        }

        #endregion

    }
}
