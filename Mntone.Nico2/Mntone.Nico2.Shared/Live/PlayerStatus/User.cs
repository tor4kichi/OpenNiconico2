using System.Linq;
using System.Xml.Linq;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// ユーザー情報を格納するクラス
	/// </summary>
	public sealed class User
	{
        internal User(XElement streamXml, XElement userXml)
        {
            Id = userXml.Element( "user_id" ).Value.ToUInt();
			Name = userXml.Element( "nickname" ).Value;
			IsPremium = userXml.Element( "is_premium" ).Value.ToBooleanFrom1();
			Age = userXml.Element( "userAge" ).Value.ToUShort();
			Sex = userXml.Element( "userSex" ).Value.ToBooleanFrom1() ? Sex.Male : Sex.Female;
			Domain = userXml.Element( "userDomain" ).Value;
			Prefecture = ( Prefecture )userXml.Element( "userPrefecture" ).Value.ToInt();
			Language = userXml.Element( "userLanguage" ).Value;
			HKey = streamXml.Element( "hkey" ).Value;
			IsOwner = streamXml.Element( "is_owner" ).Value.ToBooleanFrom1();
			IsJoin = userXml.Element( "is_join" ).Value.ToBooleanFrom1();
			IsReserved = streamXml.Element( "is_timeshift_reserved" ).Value.ToBooleanFrom1();
			IsPrefecturePreferential = streamXml.Element( "is_priority_prefecture" ).Value.ToBooleanFrom1();

			var productPurchasedXml = streamXml.Element( "product_purchased" ).Value;
			if( !string.IsNullOrEmpty( productPurchasedXml ) )
			{
				IsPurchased = productPurchasedXml.ToBooleanFrom1();
				IsSerialUsing = streamXml.Attribute( "is_serial_stream" ).Value.ToBooleanFrom1();
			}

			Twitter = new UserTwitter( userXml.Element( "twitter_info" ) );
		}

		/// <summary>
		/// ID
		/// </summary>
		public uint Id { get; private set; }

		/// <summary>
		/// 名前
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// プレミアム会員か
		/// </summary>
		public bool IsPremium { get; private set; }

		/// <summary>
		/// 年齢
		/// </summary>
		public ushort Age { get; private set; }

		/// <summary>
		/// 男性か
		/// </summary>
		public bool IsMale { get { return Sex == Sex.Male; } }

		/// <summary>
		/// 女性か
		/// </summary>
		public bool IsFemale { get { return Sex == Sex.Female; } }

		/// <summary>
		/// 性別
		/// </summary>
		public Sex Sex { get; private set; }

		/// <summary>
		/// ドメイン
		/// </summary>
		public string Domain { get; private set; }

		/// <summary>
		/// 都道府県
		/// </summary>
		public Prefecture Prefecture { get; private set; }

		/// <summary>
		/// 言語
		/// </summary>
		public string Language { get; private set; }

		/// <summary>
		/// (?)
		/// </summary>
		public string HKey { get; private set; }


		/// <summary>
		/// コミュニティーのオーナーか
		/// </summary>
		public bool IsOwner { get; private set; }

		/// <summary>
		/// コミュニティーに参加しているか
		/// </summary>
		public bool IsJoin { get; private set; }

		/// <summary>
		/// タイムシフト予約したか
		/// </summary>
		public bool IsReserved { get; private set; }

		/// <summary>
		/// 都道府県が優先的か
		/// </summary>
		public bool IsPrefecturePreferential { get; private set; }

		/// <summary>
		/// 購入または利用したか
		/// </summary>
		public bool IsPurchased { get; private set; }

		/// <summary>
		/// シリアル コードを使ったか
		/// </summary>
		/// <remarks>
		/// メッセージを変更するのに用いられる
		/// </remarks>
		public bool IsSerialUsing { get; private set; }


		/// <summary>
		/// Twitter 情報
		/// </summary>
		public UserTwitter Twitter { get; private set; }
	}
}