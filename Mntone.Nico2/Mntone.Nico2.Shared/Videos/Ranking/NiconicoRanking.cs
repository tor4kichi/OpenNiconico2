using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mntone.Nico2.Videos.Ranking
{
	public sealed class NiconicoRanking
	{
		public const string NiconicoRankingDomain = "http://www.nicovideo.jp/ranking/";

		internal static string MakeRankingUrlParameters(RankingTarget target, RankingTimeSpan timeSpan, RankingCategory category)
		{
			var _target = target.ToString();
			var _timeSpan = timeSpan.ToString();
			var _category = category.ToString();

			return $"{_target}/{_timeSpan}/{_category}?rss=2.0";
        }

		public static async Task<NiconicoRankingRss> GetRankingData(RankingTarget target, RankingTimeSpan timeSpan, RankingCategory category)
		{
			var rssUrl = NiconicoRankingDomain + MakeRankingUrlParameters(target, timeSpan, category);

			//			var rssParameters = Uri.EscapeUriString();

			try
			{
				using (HttpClient client = new HttpClient())
				{
					HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, rssUrl);

					request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue("ja", 0.5));
					request.Headers.UserAgent.Add(new ProductInfoHeaderValue("NicoPlayerHohoema_UWP", "1.0"));

					var result = await client.SendAsync(request);
					using (var contentStream = await result.Content.ReadAsStreamAsync())
					{
						var serializer = new XmlSerializer(typeof(NiconicoRankingRss));

						return (NiconicoRankingRss)serializer.Deserialize(contentStream);
					}
				}
			}
			catch (HttpRequestException reqException)
			{

			}
			catch (Exception e)
			{

			}

			return null;
		}
	}

	// see@ http://nicowiki.com/?RSS%E3%83%95%E3%82%A3%E3%83%BC%E3%83%89%E4%B8%80%E8%A6%A7

	public enum RankingTarget
	{
		view,
		res,
		mylist,
		
	}

	public enum RankingTimeSpan
	{
		hourly,
		daily,
		weekly,
		monthly,
		total,
	}

	public enum RankingCategory
	{
		/// <summary>カテゴリ合算</summary>
		all,

		/// <summary>エンタメ・音楽</summary>
		g_ent2,

		/// <summary>エンターテイメント</summary>
		ent,　　	
		/// <summary>音楽</summary>
		music,      
		/// <summary>謳ってみた</summary>
		sing,		
		/// <summary>踊ってみた</summary>
		dance,		
		/// <summary>演奏してみた</summary>
		play,		
		/// <summary>VOCALOID</summary>
		vocaloid,	
		/// <summary>ニコニコインディーズ</summary>
		nicoindies,	

		/// <summary>生活・一般・スポ</summary>
		g_life2,
		/// <summary>動物</summary>
		animal,		 
		/// <summary>料理</summary>
		cooking,	 
		/// <summary>自然</summary>
		nature,		 
		/// <summary>旅行</summary>
		travel,		 
		/// <summary>スポーツ</summary>
		sport,		 
		/// <summary>ニコニコ動画講座</summary>
		lecture,	 
		/// <summary>車載動画</summary>
		drive,		 
		/// <summary>歴史</summary>
		history,	 

		/// <summary>政治</summary>
		g_politics,	 

		/// <summary>科学・技術</summary>
		g_tech,      
		/// <summary>科学</summary>
		science,	 
		/// <summary>ニコニコ技術部</summary>
		tech,		 
		/// <summary>ニコニコ手芸部</summary>
		handcraft,   
		/// <summary>作ってみた</summary>
		make,        

		/// <summary>アニメ・ゲーム・絵</summary>
		g_culture2,  

		/// <summary>アニメ</summary>
		anime,		
		/// <summary>ゲーム</summary>
		game,		
		/// <remarks>実況プレイ動画</remarks>
		jikkyo,
		/// <summary>東方</summary>
		toho,		
		/// <summary>アイドルマスター</summary>
		imas,		 
		/// <summary>ラジオ</summary>
		radio,		 
		/// <summary>描いてみた</summary>
		draw,		 
		/// <summary></summary>

		/// <summary>その他（合算）</summary>
		g_other,	 

		/// <summary>例のアレ</summary>
		are,		
		/// <summary>日記</summary>
		diary,		 
		/// <summary>その他</summary>
		other,		 
	}
}
