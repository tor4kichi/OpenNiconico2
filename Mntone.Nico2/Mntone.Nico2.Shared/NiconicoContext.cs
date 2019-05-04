using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http;


namespace Mntone.Nico2
{
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
		public NiconicoContext()
		{ }

		/// <summary>
		/// コンストラクター
		/// </summary>
		/// <param name="token">認証トークン</param>
		public NiconicoContext( NiconicoAuthenticationToken token )
		{
			this.AuthenticationToken = token;
		}

		/// <summary>
		/// コンストラクター
		/// </summary>
		/// <param name="token">認証トークン</param>
		/// <param name="session">ログオン セッション</param>
		public NiconicoContext( NiconicoAuthenticationToken token, NiconicoSession session )
			: this( token )
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
                Content = new FormUrlEncodedContent(new Dictionary<string, string>()
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
                HttpClient = null;
                var httpClient = this.GetClient(h => h.AllowAutoRedirect = false);

                var res = await httpClient
                    .SendAsync(req, HttpCompletionOption.ResponseHeadersRead);

                const string TwoFactorAuthSite = @"https://account.nicovideo.jp/mfa";

                if (res.RequestMessage.RequestUri.OriginalString.StartsWith(TwoFactorAuthSite))
                {
                    LastRedirectHttpRequestMessage = res.RequestMessage;
                    LastRedirectHttpContent = res.Content;
                    return NiconicoSignInStatus.TwoFactorAuthRequired;
                }

                return await this.GetIsSignedInOnInternalAsync();
            }
            finally
            {
                // HttpClient = null; としているのはHttpClientHanlderの再設定させたい
                // 自動リダイレクトをログイン処理が終わったら再度有効にしたい
                HttpClient = null;
            }

        }

        public HttpContent LastRedirectHttpContent { get; private set; }
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
            var response = await this.GetClient()
                .GetAsync(new Uri(NiconicoUrls.TopPageUrl));

            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Headers.TryGetValues(XNiconicoAuthflag, out var flags))
                {
                    var authFlag = flags.First().ToUInt();
                    var auth = (NiconicoAccountAuthority)authFlag;
                    return auth != NiconicoAccountAuthority.NotSignedIn ? NiconicoSignInStatus.Success : NiconicoSignInStatus.Failed;
                }
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
            await this.GetClient()
                .GetAsync(new Uri(NiconicoUrls.LogOffUrl));
	        return await this.GetIsSignedInOnInternalAsync();
        }

        CookieContainer CookieContainer = new CookieContainer();

		internal HttpClient GetClient(Action<HttpClientHandler> modifier = null)
		{
            if( this.HttpClient == null )
			{
                var handler = new HttpClientHandler();
                handler.CookieContainer = CookieContainer;
                modifier?.Invoke(handler);

                this.HttpClient = new HttpClient(handler);

                this.HttpClient.DefaultRequestHeaders.Add( "User-Agent", this._AdditionalUserAgent != null
					? NiconicoContext.DefaultUserAgent + " (" + this._AdditionalUserAgent + ')'
					: NiconicoContext.DefaultUserAgent );
            }
			return this.HttpClient;
		}


        public string GetCurrentNicoVideoCookieHeader()
        {
            return CookieContainer.GetCookieHeader(new Uri(NiconicoUrls.TopPageUrl));
        }

		internal Task<HttpResponseMessage> GetAsync(string url)
		{
			return GetClient().GetAsync(url);
		}

		internal Task<string> GetStringAsync(string url, Dictionary<string, string> query)
		{
			var queryText = String.Join("&", query.Select(x => x.Key + "=" + Uri.EscapeDataString(x.Value)));
			var realUri = $"{url}?{queryText}";

			return GetClient().GetStringAsync(realUri);
		}

		internal Task<string> GetStringAsync(string url)
		{
			return GetClient().GetStringAsync(url);
		}

		internal Task<string> PostAsync(string url, bool withToken = true)
		{
			Dictionary<string, string> keyvalues = new Dictionary<string, string>();

			return this.PostAsync(url, keyvalues, withToken);
		}

		internal async Task<string> PostAsync(string url, Dictionary<string, string> keyvalues, bool withToken = true)
		{
			if (withToken && !keyvalues.ContainsKey("token"))
			{
				var token = await this.GetToken();
				keyvalues.Add("token", token);
			}

			var content = new FormUrlEncodedContent(keyvalues);

			return await this.PostAsync(url, content);
		}

		internal async Task<string> PostAsync(string url, HttpContent content)
		{
			using (var res = await GetClient().PostAsync(new Uri(url), content))
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
		/// ニコニコ静画の API 群
		/// </summary>
		public Images.ImageApi Image
		{
			get { return this._Image ?? ( this._Image = new Images.ImageApi( this ) ); }
		}
		private Images.ImageApi _Image = null;

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
		public string AdditionalUserAgent
		{
			get { return this._AdditionalUserAgent; }
			set { this._AdditionalUserAgent = value; }
		}
		private string _AdditionalUserAgent = null;

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