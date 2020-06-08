using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;


// sample: {"meta":{"status":200,"errorCode":"OK"},"data":{"title":"2018年秋アニメ個人的評価枠(1話～４話)","description":"事前期待度<br /> <br /> \n \nS<br /> \n風が強く吹いている<br /> \nRErideD-刻越えのデリダ-<br /> \nゾンビランドサガ<br /> <br /> \n \nA<br /> \nとある魔術の禁書目録Ⅲ<br /> \nDOUBLE DECKER！ダグ＆キリル<br /> \nあかねさす少女<br /> \n青春豚野郎はバニーガール先輩の夢を見ない<br /> \nアニマエール！<br /> <br /> \nB<br /> \nジョジョの奇妙な冒険 黄金の風<br /> \n色づく世界の明日から<br /> \nSSSS.GRIDMAN<br /> \nRELEASE THE SPYCE<br /> \n宇宙戦艦ティラミスⅡ<br /> \nからくりサーカス<br /> \n火ノ丸相撲<br /> <br /> \nC<br /> \nラディアン<br /> \nソードアート・オンライン アリシゼーション<br /> \nやがて君になる<br /> \nゴブリンスレイヤー<br /> \n転生したらスライムだった件<br /> \nツルネー風舞高校弓道部ー<br /> \n抱かれたい男1位に脅かされています。<br /> \nベルゼブブ嬢のお気に召すまま。<br /> \n骸骨書店員 本田さん<br /> \nうちのメイドがウザすぎる！<br /> <br /> \n \nD<br /> \nひもてはうす<br /> \nソラとウミのアイダ<br /> \nイングレス<br /> \n寄宿学校のジュリエット<br /> \nとなりの吸血鬼さん<br /> \nメルクストーリア<br /> \nCONCEPTION<br /> <br /> \n \nE<br /> \n叛逆性ミリオンアーサー<br /> \n狐狸之声<br /> \n俺が好きなのは妹だけど妹じゃない<br /> <br /> \n追加作品<br /> \nおこしやす、ちとせちゃん<br /> \n終電後、カプセルホテルで、上司に微熱伝わる夜。<br /> \n人外さんの嫁<br /> \nおとなの防具屋さん<br /><br /> \n \n継続<br /> \nBANANA FISH<br /> \n中間管理録トネガワ<br /> \nBanG Dream ガルパ☆ピコ<br /> \n東京喰種:re<br />","isMemberOnly":false,"vposBaseAt":1540719000,"beginAt":1540719000,"endAt":1540724927,"status":"end","categories":["一般(その他)"],"rooms":[],"isUserNiconicoAdsEnabled":true,"socialGroup":{"type":"community","id":"co3663015","name":"アニメとかゲームとか","communityLevel":18},"broadcaster":{"name":"ezo","id":"18090344"}}}
// sample: {"meta":{"status":200,"errorCode":"OK"},"data":{"title":"日経チャンネルマーケッツ","description":"「日経チャンネルマーケッツ」は、マーケット・経済専門チャンネル「日経CNBC」の番組を生放送でお届けするチャンネルです。東京市場やニューヨーク市場を含む、世界の市場の動きをリアルタイムで解説します。<br />\n\nニューヨーク市場の大引けから始まり、東京市場の寄り付き、前場 、後場の市場動向を速報。東京市場の大引け後も、アジア・ヨーロッパの株式市場の各拠点から最新ニュースをライブ中継。ニューヨーク市場の寄り付きは、米CNBCの映像に日本語同時通訳を交えて速報します。<br /><br />\n\n<b>【配信時間】<br />\n月曜 8:00～23:50／火曜～金曜 4:00～23:50／土曜 4:00～7:00<br />\n※配信時間は変更になる可能性があります。詳細はチャンネルページのお知らせ欄をご覧ください。<br /><br />\n\nhttp://ch.nicovideo.jp/channel/nikkei-channel-markets\n\n<br /><br /><br />\n<font color=\"#ff0000\">本放送をミラー配信した際は視聴を停止させていただく場合がございます。</font><br />ミラー放送がございましたら、宜しければ下記よりご連絡ください。<br />・ユーザー生放送：視聴プレイヤー下の<font color=\"#ff0000\">【！】この番組を違反通報</font><br />\n・外部サイト：外部ミラー通報窓口<br />件名：ミラー配信通報<br />\n本文：元番組とミラー先のURLをご記入ください<br /><br />◆ご不明点について<br />有料生放送ヘルプをご確認ください。<br /></b>","isMemberOnly":false,"vposBaseAt":1527534000,"beginAt":1527534000,"endAt":1527605393,"status":"end","categories":["一般(その他)"],"rooms":[],"isUserNiconicoAdsEnabled":false,"socialGroup":{"type":"channel","id":"ch1216","name":"NIKKEI Channel ＜Markets＞","ownerName":"株式会社日経CNBC"},"broadcaster":{"name":"株式会社日経CNBC","id":"ch1216"}}}
namespace Mntone.Nico2.Live.Watch
{
    [DataContract]
    public class Meta
    {

        [DataMember(Name = "status")]
        public int Status { get; set; }

        [DataMember(Name = "errorCode")]
        public string ErrorCode { get; set; }
    }

    [DataContract]
    public class Room
    {

        [DataMember(Name = "webSocketUri")]
        public string WebSocketUri { get; set; }

        [DataMember(Name = "xmlSocketUri")]
        public string XmlSocketUri { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "threadId")]
        public string ThreadId { get; set; }
    }

    [DataContract]
    public class ProgramInfoSocialGroup
    {
        public CommunityType Type => CommunityTypeExtensions.ToCommunityType(__Type);


        [DataMember(Name = "type")]
        public string __Type { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "communityLevel")]
        public int? CommunityLevel { get; set; }
    }

    [DataContract]
    public class Broadcaster
    {

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }
    }

    [DataContract]
    public class Data
    {

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "isMemberOnly")]
        public bool IsMemberOnly { get; set; }

        [DataMember(Name = "vposBaseAt")]
        public int __VposBaseAt { get; set; }

        public DateTime VposBaseAt => DateTimeOffset.FromUnixTimeSeconds(__VposBaseAt).LocalDateTime;

        [DataMember(Name = "beginAt")]
        public int __BeginAt { get; set; }

        public DateTime BeginAt => DateTimeOffset.FromUnixTimeSeconds(__BeginAt).LocalDateTime;

        [DataMember(Name = "endAt")]
        public int __EndAt { get; set; }

        public DateTime EndAt => DateTimeOffset.FromUnixTimeSeconds(__EndAt).LocalDateTime;

        [DataMember(Name = "status")]
        public string __Status { get; set; }

        public StatusType Status => __Status switch
        {
            "onAir" => StatusType.OnAir,
            "reserved" => StatusType.ComingSoon,
            "closed" => StatusType.Closed,
            _ => throw new NotSupportedException()
        };

        [DataMember(Name = "categories")]
        public IList<string> Categories { get; set; }

        [DataMember(Name = "rooms")]
        public IList<Room> Rooms { get; set; }

        [DataMember(Name = "isUserNiconicoAdsEnabled")]
        public bool? IsUserNiconicoAdsEnabled { get; set; }

        [DataMember(Name = "socialGroup")]
        public ProgramInfoSocialGroup SocialGroup { get; set; }

        [DataMember(Name = "broadcaster")]
        public Broadcaster Broadcaster { get; set; }
    }

    [DataContract]
    public class ProgramInfo
    {

        [DataMember(Name = "meta")]
        public Meta Meta { get; set; }

        [DataMember(Name = "data")]
        public Data Data { get; set; }


        public bool IsOK => Meta.Status == 200;
    }
}

