using System;
using System.Xml.Linq;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// ユーザーの Twitter 情報を格納するクラス
	/// </summary>
	public sealed class UserTwitter
	{
		internal UserTwitter( XElement twitterInfoXml )
		{
			IsEnabled = twitterInfoXml.Element( "status" ).Value == "enabled";
			ScreenName = twitterInfoXml.Element( "screen_name" ).Value;
			FollowersCount = twitterInfoXml.Element( "followers_count" ).Value.ToUInt();
			IsVip = twitterInfoXml.Element( "is_vip" ).Value.ToBooleanFrom1();
			ProfileImageUrl = twitterInfoXml.Element( "profile_image_url" ).Value.ToUri();
			IsAuthenticationRequired = twitterInfoXml.Element( "after_auth" ).Value.ToBooleanFrom1();
			Token = twitterInfoXml.Element( "tweet_token" ).Value;
		}

		/// <summary>
		/// Twitter 情報が有効か
		/// </summary>
		public bool IsEnabled { get; private set; }

		/// <summary>
		/// スクリーン ネーム
		/// </summary>
		public string ScreenName { get; private set; }

		/// <summary>
		/// フォロワー数
		/// </summary>
		public uint FollowersCount { get; private set; }

		/// <summary>
		/// VIP アカウントか
		/// </summary>
		public bool IsVip { get; private set; }

		/// <summary>
		/// 画像 URL
		/// </summary>
		public Uri ProfileImageUrl { get; private set; }

		/// <summary>
		/// 認証が必要か
		/// </summary>
		public bool IsAuthenticationRequired { get; private set; }

		/// <summary>
		/// トークン
		/// </summary>
		public string Token { get; private set; }
	}
}