using HtmlAgilityPack;
using System;
using System.Linq;

namespace Mntone.Nico2.Users.Info
{
	/// <summary>
	/// ユーザー情報を格納するクラス
	/// </summary>
	public sealed class InfoResponse
	{
		internal InfoResponse(HtmlNode bodyHtml, string language, UserMyPageJSInfo info)
		{
			try
			{
				this.Id = uint.Parse(info.Id);
			}
			catch { }

			try
			{
				this.IsPremium = info.IsPremium;
			}
			catch { }

			try
			{
				this.IsOver18 = info.Age >= 18;
			}
			catch { }

			/*
			var profileHtml = bodyHtml
                .GetElementByClassName("BaseLayout")
                .GetElementByClassName("userDetail")
                .GetElementByClassName("profile");
			try
			{
				var h2Html = profileHtml.Element("h2");
				this.Name = h2Html.FirstChild.InnerText;
			}
			catch //(Exception ex)
			{
				//throw new Exception("ユーザー名の取得に失敗しました", ex);
			}
			*/
			/*
			{
				var accountIdHtml = profileHtml.GetElementByClassName( "account" ).GetElementByClassName( "accountNumber" ).Element( "span" );
				var keywords = accountIdHtml.InnerText.Split( new char[] { ' ', '(', ')' } );
				if( keywords.Count() >= 4 )
				{
					switch( language )
					{
						case "ja-jp":
							this.Id = keywords[0].ToUInt();
							this.JoinedVersion = keywords[1];
							this.IsPremium = keywords[3] == "プレミアム会員";
							break;
						case "en-us":
							this.Id = keywords[0].ToUInt();
							this.JoinedVersion = keywords[1];
							this.IsPremium = keywords[3] == "Premium";
							break;
						case "zh-tw":
							this.Id = keywords[0].ToUInt();
							this.JoinedVersion = keywords[1];
							this.IsPremium = keywords[3] == "白金會員";
							break;
					}
				}
			}
			*/

            /*
			try
			{
				var stats = profileHtml.GetElementByClassName("stats");
				var statsItems = stats.SelectNodes("./li/a/span");

				var statsItemNumbers = statsItems.Select(x =>
				{
					var numberWithUnit = x.InnerText.Where(y => y != ',');
					var numberText = string.Join("", numberWithUnit.TakeWhile(y => y >= '0' && y <= '9'));
					return uint.Parse(numberText);
				})
				.ToArray();

				this.FavoriteCount = (ushort)statsItemNumbers[0];
				this.StampCount = (ushort)statsItemNumbers[1];
				this.Points = statsItemNumbers[2];
				this.CreatorScore = statsItemNumbers[3];
			}
			catch (Exception) { }
            */
		}

		/// <summary>
		/// 名前
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// ユーザー ID
		/// </summary>
		public uint Id { get; private set; }

		/// <summary>
		/// アカウントが加入したときのバージョン
		/// </summary>
		public string JoinedVersion { get; private set; }

		/// <summary>
		/// プレミアムか
		/// </summary>
		public bool IsPremium { get; private set; }

		/// <summary>
		/// プレミアムか
		/// </summary>
		public bool IsOver18 { get; private set; }


		/// <summary>
		/// お気に入り登録された数
		/// </summary>
		public ushort FavoriteCount { get; private set; }

		/// <summary>
		/// スタンプ数
		/// </summary>
		public ushort StampCount { get; private set; }

		/// <summary>
		/// ニコニコポイント数
		/// </summary>
		public uint Points { get; private set; }

		/// <summary>
		/// クリエイター推奨スコア
		/// </summary>
		public uint CreatorScore { get; private set; }
	}
}
