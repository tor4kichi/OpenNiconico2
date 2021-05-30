using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

#if WINDOWS_UWP
using Windows.Web;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using Windows.Web.Http.Headers;
#else
using System.Net;
using System.Net.Http;
#endif

namespace Mntone.Nico2
{
#if DEBUG_NICO_URL
	internal static class UrlDebugHelper
    {
		internal static void DebugLog(string url)
        {
			Debug.WriteLine($"[DEBUG_URL] " + url);
        }
    }
#endif


	/// <summary>
	/// ニコニコの API コンテクスト
	/// </summary>
	public sealed class NiconicoContext
		: IDisposable
	{
		/// <summary>
		/// コンストラクター
		/// </summary>
		/// <remarks>
		/// 非ログイン API 用に使用できます
		/// </remarks>
		public NiconicoContext(string additionalUserAgent)
		{
			AdditionalUserAgent = additionalUserAgent;
#if WINDOWS_UWP
			var filter = new HttpBaseProtocolFilter()
			{

			};

			HttpClient = new HttpClient(filter);
			CookieContainer = filter.CookieManager;
#else
                HttpClient = new HttpClient(
                    new HttpClientHandler() 
                    {
                        CookieContainer = CookieContainer
                    });
#endif
			HttpClient.DefaultRequestHeaders.Add("User-Agent", this.AdditionalUserAgent != null
				? NiconicoContext.DefaultUserAgent + " (" + this.AdditionalUserAgent + ')'
				: NiconicoContext.DefaultUserAgent);

			HttpClient.DefaultRequestHeaders.Add("Referer", "https://www.nicovideo.jp/");
			HttpClient.DefaultRequestHeaders.Add("X-Frontend-Id", "6");
			HttpClient.DefaultRequestHeaders.Add("X-Frontend-Version", "0");
			HttpClient.DefaultRequestHeaders.Add("X-Niconico-Language", "ja-jp");

			HttpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
			HttpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
			HttpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-site");
			HttpClient.DefaultRequestHeaders.Add("X-Request-With", "https://www.nicovideo.jp");

			HttpClient.DefaultRequestHeaders.Add("Origin", "https://www.nicovideo.jp");
		}

			/// <summary>
			/// コンストラクター
			/// </summary>
			/// <param name="token">認証トークン</param>
		public NiconicoContext(string additionalUserAgent, NiconicoAuthenticationToken token )
			: this(additionalUserAgent)
		{
			this.AuthenticationToken = token;
		}

		/// <summary>
		/// コンストラクター
		/// </summary>
		/// <param name="token">認証トークン</param>
		/// <param name="session">ログオン セッション</param>
		public NiconicoContext(string additionalUserAgent, NiconicoAuthenticationToken token, NiconicoSession session )
			: this(additionalUserAgent, token)
		{
			this.CurrentSession = session;
		}

		/// <summary>
		/// デストラクター
		/// </summary>
		public void Dispose()
		{
			this.DisposeImpl();
		}

		private void DisposeImpl()
		{
			if( this.HttpClient != null )
			{
				this.HttpClient.Dispose();
				this.HttpClient = null;
			}
		}

        /// <summary>
        /// 非同期操作としてログイン要求を送信します。
        /// ログイン完了後、ログインが正常にできているかをチェックし、その状態をセッションに記録します。
        /// </summary>
        /// <returns>非同期操作を表すオブジェクト</returns>
        public async Task<NiconicoSignInStatus> SignInAsync()
		{
			var req = new HttpRequestMessage(HttpMethod.Post, new Uri(NiconicoUrls.LogOnApiUrl))
            {
#if WINDOWS_UWP
                Content = new HttpFormUrlEncodedContent(new Dictionary<string, string>()
#else

                Content = new FormUrlEncodedContent(new Dictionary<string, string>() 
#endif
                {
                    { MailTelName, this.AuthenticationToken.MailOrTelephone },
                    { PasswordName, this.AuthenticationToken.Password },
                })
            };

            try
            {
                // 自動リダイレクトが有効だと、クッキー認証の情報がCookieContainerに保存されず
                // ログインセッションを張るのに失敗してしまう

                // HttpClient = null; としているのはHttpClientHanlderの再設定させたい
                // 自動リダイレクトをログイン時のみOFFに設定させたいため

                /*
                var httpClient = new HttpClient(new HttpClientHandler()
                {
                    AllowAutoRedirect = false,
                    CookieContainer = CookieContainer
                }
                );

                httpClient.DefaultRequestHeaders.Add("User-Agent", this._AdditionalUserAgent != null
                    ? NiconicoContext.DefaultUserAgent + " (" + this._AdditionalUserAgent + ')'
                    : NiconicoContext.DefaultUserAgent);


    */
                await GetAsync(NiconicoUrls.VideoLoginUrl);

                var res = await SendAsync(req, HttpCompletionOption.ResponseHeadersRead);

                const string TwoFactorAuthSite = @"https://account.nicovideo.jp/mfa";

                if (res.RequestMessage.RequestUri.OriginalString.StartsWith(TwoFactorAuthSite))
                {
                    LastRedirectHttpRequestMessage = res.RequestMessage;
                    return NiconicoSignInStatus.TwoFactorAuthRequired;
                }

                return await this.GetIsSignedInOnInternalAsync();
            }
            finally
            {
                // HttpClient = null; としているのはHttpClientHanlderの再設定させたい
                // 自動リダイレクトをログイン処理が終わったら再度有効にしたい
            }
        }

        
        
        


        public async Task<NiconicoSignInStatus> MfaAsync(Uri location, string code, bool isTrustedDevice, string deviceName)
        {
			var requestMessage = new HttpRequestMessage(HttpMethod.Post, location)
            {
#if WINDOWS_UWP
                Content = new HttpFormUrlEncodedContent(new Dictionary<string, string>()
#else
                Content = new FormUrlEncodedContent(new Dictionary<string, string>()
#endif
                {
                    { "otp", code },
                    { "loginBtn", "ログイン" },
                    { "is_mfa_trusted_device", isTrustedDevice ? "true" : "false" },
                    { "device_name", "Edge (Windows)" }
                })
            };

            requestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; ServiceUI 11) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36 Edge/16.16299");
            requestMessage.Headers.Add("Referer", location.OriginalString);
            requestMessage.Headers.Add("Origin", "https://account.nicovideo.jp");
            requestMessage.Headers.Add("Upgrade-Insecure-Requests", "1");
            requestMessage.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");

            var result = await SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
//            var result = await HttpClient.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);

            return result.IsSuccessStatusCode ? NiconicoSignInStatus.Success : NiconicoSignInStatus.Failed;
        }

        public HttpRequestMessage LastRedirectHttpRequestMessage { get; private set; }


        /// <summary>
        /// 非同期操作としてログイン確認のための要求を送信します。
        /// ログインが正常にできている場合、その状態をセッションに記録します。
        /// </summary>
        /// <returns>非同期操作を表すオブジェクト</returns>
        public Task<NiconicoSignInStatus> GetIsSignedInAsync()
		{
			return this.GetIsSignedInOnInternalAsync();
		}


		internal async Task<NiconicoSignInStatus> GetIsSignedInOnInternalAsync()
		{
            var response = await GetAsync(NiconicoUrls.TopPageUrl);

            if (response.IsSuccessStatusCode)
            {
#if WINDOWS_UWP
                if (response.Headers.TryGetValue(XNiconicoAuthflag, out var flags))
                {
                    var authFlag = flags.ToUInt();
                    var auth = (NiconicoAccountAuthority)authFlag;
                    return auth != NiconicoAccountAuthority.NotSignedIn ? NiconicoSignInStatus.Success : NiconicoSignInStatus.Failed;
                }
#else
                if (response.Headers.TryGetValues(XNiconicoAuthflag, out var flags))
                {
                    var authFlag = flags.First().ToUInt();
                    var auth = (NiconicoAccountAuthority)authFlag;
                    return auth != NiconicoAccountAuthority.NotSignedIn ? NiconicoSignInStatus.Success : NiconicoSignInStatus.Failed;
                }
#endif
            }
            else if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
            {
                return NiconicoSignInStatus.ServiceUnavailable;
            }

            return NiconicoSignInStatus.Failed;
        }

		/// <summary>
		/// 非同期操作としてログオフ要求を送信します
		/// </summary>
		/// <returns>非同期操作を表すオブジェクト</returns>
		public async Task<NiconicoSignInStatus> SignOutOffAsync()
		{
            await this.GetAsync(NiconicoUrls.LogOffUrl);
	        return await this.GetIsSignedInOnInternalAsync();
        }

#if WINDOWS_UWP
        HttpCookieManager CookieContainer;
#else
        CookieContainer CookieContainer = new CookieContainer();
#endif

        public string GetCurrentNicoVideoCookieHeader()
        {
#if WINDOWS_UWP
            return string.Join(" ", CookieContainer.GetCookies(new Uri(NiconicoUrls.TopPageUrl)));
#else
            return CookieContainer.GetCookieHeader(new Uri(NiconicoUrls.TopPageUrl));
#endif

        }

        internal async Task<HttpResponseMessage> GetAsync(string url)
		{
#if DEBUG_NICO_URL
			UrlDebugHelper.DebugLog(url);
#endif

#if WINDOWS_UWP
			return await HttpClient.GetAsync(new Uri(url));
#else
            return await HttpClient.GetAsync(url);
#endif

        }

        internal async Task<string> GetStringAsync(string url, Dictionary<string, string> query)
		{
			var queryText = String.Join("&", query.Select(x => x.Key + "=" + Uri.EscapeDataString(x.Value)));
			var realUri = $"{url}?{queryText}";

            return await GetStringAsync(realUri);
        }

#if WINDOWS_UWP
		internal async Task<T> GetJsonAsAsync<T>(string url, JsonSerializerSettings settings = null, Action<HttpRequestHeaderCollection> headerModifier = null)
#else
		internal async Task<T> GetJsonAsAsync<T>(string url, JsonSerializerSettings settings = null, Action<System.Net.Http.Headers.HttpRequestHeaders> headerModifier = null)
#endif
		{
			var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
			headerModifier?.Invoke(request.Headers);
			var message = await SendAsync(request);
			var json = await message.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(json, settings);
        }


#if WINDOWS_UWP
		internal async Task<T> GetJsonAsAsync<T>(HttpMethod httpMethod, string url, JsonSerializerSettings settings = null, Action<HttpRequestHeaderCollection> headerModifier = null)
#else
		internal async Task<T> GetJsonAsAsync<T>(HttpMethod httpMethod, string url, JsonSerializerSettings settings = null, Action<System.Net.Http.Headers.HttpRequestHeaders> headerModifier = null)
#endif
		{
			var request = new HttpRequestMessage(httpMethod, new Uri(url));
			headerModifier?.Invoke(request.Headers);
			var message = await SendAsync(request);
			var json = await message.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<T>(json, settings);
		}


		internal async Task<string> GetStringAsync(string url)
		{
#if DEBUG_NICO_URL
			UrlDebugHelper.DebugLog(url);
#endif

#if WINDOWS_UWP
			return await HttpClient.GetStringAsync(new Uri(url));
#else
            return await HttpClient.GetConvertedStringAsync(url);
#endif
		}

        internal async Task<string> GetConvertedStringAsync(string url)
        {
#if DEBUG_NICO_URL
			UrlDebugHelper.DebugLog(url);
#endif
			return await HttpClient.GetConvertedStringAsync(url);
        }


        internal Task<string> PostAsync(string url, bool withToken = true)
		{
			Dictionary<string, string> keyvalues = new Dictionary<string, string>();

			return this.PostAsync(url, keyvalues, withToken);
		}

        internal Task<string> PostAsync(string url, string stringContent)
        {
#if WINDOWS_UWP
			var content = new HttpStringContent(stringContent);
#else
            var content = new StringContent(stringContent);
#endif
            return this.PostAsync(url, content);
        }

        internal async Task<string> PostAsync(string url, Dictionary<string, string> keyvalues, bool withToken = true)
		{
#if DEBUG_NICO_URL
			UrlDebugHelper.DebugLog(url);
#endif

			if (withToken && !keyvalues.ContainsKey("token"))
			{
				var token = await this.GetToken();
				keyvalues.Add("token", token);
			}

#if WINDOWS_UWP
            var content = new HttpFormUrlEncodedContent(keyvalues);
#else
            var content = new FormUrlEncodedContent(keyvalues);
#endif


            return await this.PostAsync(url, content);
		}


        internal async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, HttpCompletionOption completionOption = HttpCompletionOption.ResponseContentRead)
        {
#if DEBUG_NICO_URL
			UrlDebugHelper.DebugLog($"[{request.Method}] {request.RequestUri.OriginalString}");
#endif

#if WINDOWS_UWP
			return await HttpClient.SendRequestAsync(request, completionOption);
#else
            return await HttpClient.SendAsync(request, completionOption);
#endif
        }


#if WINDOWS_UWP

#else
            
#endif

#if WINDOWS_UWP
        internal async Task<string> PostAsync(string url, IHttpContent content)
#else
            internal async Task<string> PostAsync(string url, HttpContent content)
#endif
		{
#if DEBUG_NICO_URL
			UrlDebugHelper.DebugLog(url);
#endif

			using (var res = await HttpClient.PostAsync(new Uri(url), content))
			{
				if (res.IsSuccessStatusCode)
				{
					return await res.Content.ReadAsStringAsync();
				}
				else
				{
                    System.Diagnostics.Debug.WriteLine(res.ToString());
					return "";
				}
			}
		}



		internal async Task PrepareCorsAsscessAsync(HttpMethod httpMethod, string uri)
		{
			var optionReq = new HttpRequestMessage(HttpMethod.Options, new Uri(uri));
			optionReq.Headers.Add("Access-Control-Request-Headers", "x-frontend-id,x-frontend-version,x-niconico-language,x-request-with");
			optionReq.Headers.Add("Access-Control-Request-Method", httpMethod.Method);
			var res = await SendAsync(optionReq, HttpCompletionOption.ResponseHeadersRead);
		}


		#region APIs

		/// <summary>
		/// ニコニコ動画の API 群
		/// </summary>
		public Videos.VideoApi Video
		{
			get { return this._Video ?? ( this._Video = new Videos.VideoApi( this ) ); }
		}
		private Videos.VideoApi _Video = null;

		/// <summary>
		/// ニコニコ生放送の API 群
		/// </summary>
		public Live.LiveApi Live
		{
			get { return this._Live ?? ( this._Live = new Live.LiveApi( this ) ); }
		}
		private Live.LiveApi _Live = null;

		/// <summary>
		/// ニコニコ検索の API 群
		/// </summary>
		public Searches.SearchApi Search
		{
			get { return this._Search ?? ( this._Search = new Searches.SearchApi( this ) ); }
		}
		private Searches.SearchApi _Search = null;

		/// <summary>
		/// ニコニコ大百科の API 群
		/// </summary>
		public Dictionaries.DictionaryApi Dictionary
		{
			get { return this._Dictionary ?? ( this._Dictionary = new Dictionaries.DictionaryApi( this ) ); }
		}
		private Dictionaries.DictionaryApi _Dictionary = null;

		/// <summary>
		/// ニコニコ コミュニティー API 群
		/// </summary>
		public Communities.CommunityApi Community
		{
			get { return this._Community ?? ( this._Community = new Communities.CommunityApi( this ) ); }
		}
		private Communities.CommunityApi _Community = null;

		/// <summary>
		/// ニコニコ チャンネル API 群
		/// </summary>
		public Channels.ChannelApi Channel
		{
			get { return this._Channel ?? ( this._Channel = new Channels.ChannelApi( this ) ); }
		}
		private Channels.ChannelApi _Channel = null;

		/// <summary>
		/// ニコニコ ユーザー API 群
		/// </summary>
		public Users.UserApi User
		{
			get { return this._User ?? ( this._User = new Users.UserApi( this ) ); }
		}
		private Users.UserApi _User = null;


		/// <summary>
		/// ニコニコ マイリスト API 郡
		/// </summary>
		public Mylist.MylistApi Mylist
		{
			get { return this._Mylist ?? (this._Mylist = new Nico2.Mylist.MylistApi(this)); }
		}
		private Mylist.MylistApi _Mylist = null;


        /// <summary>
		/// ニコニコ マイリスト API 郡
		/// </summary>
		public NicoRepo.NicoRepoApi NicoRepo
        {
            get { return this._NicoRepo ?? (this._NicoRepo = new Nico2.NicoRepo.NicoRepoApi(this)); }
        }
        private NicoRepo.NicoRepoApi _NicoRepo = null;


        /// <summary>
		/// ニコニコ 組み込み系 API 郡
		/// </summary>
		public Embed.EmbedApi Embed
        {
            get { return this._Embed ?? (this._Embed = new Nico2.Embed.EmbedApi(this)); }
        }
        private Embed.EmbedApi _Embed = null;


        /// <summary>
		/// ニコニコ 組み込み系 API 郡
		/// </summary>
		public Nicocas.NicocasApi Nicocas
        {
            get { return this._Nicocas ?? (this._Nicocas = new Nico2.Nicocas.NicocasApi(this)); }
        }
        private Nicocas.NicocasApi _Nicocas = null;


#endregion


#region property (and related field)

        /// <summary>
        /// ニコニコ　トークン
        /// </summary>
        public NiconicoAuthenticationToken AuthenticationToken { get; set; }

		/// <summary>
		/// ニコニコ セッション
		/// </summary>
		public NiconicoSession CurrentSession
		{
			get { return this._CurrentSession; }
			set
			{
				this._CurrentSession = value;
				this.DisposeImpl();
			}
		}
		private NiconicoSession _CurrentSession = null;

		/// <summary>
		/// 追加のユーザー エージェント
		/// </summary>
		/// <remarks>
		/// 特に事情がない限り、各アプリ名を指定するなどしてください
		/// </remarks>
		public string AdditionalUserAgent { get; }

#endregion


#region field

		private const string XNiconicoId = "x-niconico-id";
		private const string XNiconicoAuthflag = "x-niconico-authflag";
		private const string MailTelName = "mail_tel";
		private const string PasswordName = "password";
		private const string UserSessionName = "user_session";
		internal const string DefaultUserAgent = "OpenNiconico/2.0";
		private readonly Uri NiconicoCookieUrl = new Uri( "http://nicovideo.jp/" );

        public HttpClient HttpClient { get; private set; }

#endregion
    }
}