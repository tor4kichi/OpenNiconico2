using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Mntone.Nico2.Live.PlayerStatus
{
	/// <summary>
	/// ストリーム情報を格納するクラス
	/// </summary>
	public sealed class Stream
	{
		internal Stream( XElement streamXml, XElement rtmpXml, XElement ticketsXml, XElement playerXml )
		{
			IsFlashMediaServer = rtmpXml.Element( "is_fms" ).Value.ToBooleanFrom1();

			var rtmptPortXml = rtmpXml.Attribute( "rtmpt_port" ).Value;
			RtmptPort = !string.IsNullOrEmpty( rtmptPortXml ) ? rtmptPortXml.ToUShort() : ( ushort )0u;
			
			RtmpUrl = rtmpXml.Element( "url" ).Value.ToUri();
			Ticket = rtmpXml.Element( "ticket" ).Value;

			if( ticketsXml != null )
			{
				Tickets = ticketsXml.Elements().ToDictionary(
					ticketXml => ticketXml.Attribute( "name" ).Value,
					ticketXml => ticketXml.Value );
			}

			Contents = streamXml.Element( "contents_list" ).Elements().Select( contentsXml => new Content( contentsXml ) ).ToList();

			var splitTop = streamXml.Element( "split_top" ).Value.ToBooleanFrom1();
			if( splitTop )
			{
				Position = VideoPosition.Top;
			}
			else
			{
				var splitBottom = streamXml.Element( "split_bottom" ).Value.ToBooleanFrom1();
				if( splitBottom )
				{
					Position = VideoPosition.Bottom;
				}
				else
				{
					var background = streamXml.Element( "background_comment" ).Value.ToBooleanFrom1();
					Position = background ? VideoPosition.Small : VideoPosition.Default;
				}
			}

			var aspectXml = streamXml.Element( "aspect" ).Value;
			Aspect = !string.IsNullOrEmpty( aspectXml ) ? aspectXml.ToVideoAspect() : VideoAspect.Auto;

			BroadcastToken = streamXml.Element( "broadcast_token" ).Value;
			IsQualityOfServiceAnalyticsEnabled = playerXml.Element( "qos_analytics" ).Value.ToBooleanFrom1();
		}

		/// <summary>
		/// Flash Media サーバーか
		/// </summary>
		public bool IsFlashMediaServer { get; private set; }

		/// <summary>
		/// rtmpt の場合のポート番号
		/// </summary>
		public ushort RtmptPort { get; private set; }

		/// <summary>
		/// rtmp の URL
		/// </summary>
		public Uri RtmpUrl { get; private set; }

		/// <summary>
		/// チケット
		/// </summary>
		public string Ticket { get; private set; }

		/// <summary>
		/// チケット
		/// </summary>
		public IReadOnlyDictionary<string, string> Tickets { get; private set; }

		/// <summary>
		/// コンテンツ
		/// </summary>
		public IReadOnlyList<Content> Contents { get; private set; }

		/// <summary>
		/// 映像の表示位置
		/// </summary>
		public VideoPosition Position { get; private set; }

		/// <summary>
		/// 映像のアスペクト比
		/// </summary>
		public VideoAspect Aspect { get; private set; }

		/// <summary>
		/// 配信のトークン
		/// </summary>
		/// <remarks>
		/// 配信で使われる
		/// </remarks>
		public string BroadcastToken { get; private set; }

		/// <summary>
		/// QoS 分析が有効か
		/// </summary>
		public bool IsQualityOfServiceAnalyticsEnabled { get; private set; }
	}
}