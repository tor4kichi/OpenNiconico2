using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;

namespace Mntone.Nico2.Videos.Dmc
{
    public class DmcWatchData
    {
        public DmcWatchResponse DmcWatchResponse { get; set; }

        public DmcWatchEnvironment DmcWatchEnvironment { get; set; }
    }

    public class DmcApiWatchResponse
    {
        [DataMember(Name = "data")]
        public DmcWatchResponse Data { get; set; }

        [DataMember(Name = "meta")]
        public Meta Meta { get; set; }
    }
    


    #region Initial Watch Data
    public class DmcWatchResponse
    {
        [DataMember(Name = "ads")]
        public Ads Ads { get; set; }

        [DataMember(Name = "category")]
        public object Category { get; set; }

        [DataMember(Name = "channel")]
        public Channel Channel { get; set; }

        [DataMember(Name = "client")]
        public Client Client { get; set; }

        [DataMember(Name = "comment")]
        public Comment Comment { get; set; }

        [DataMember(Name = "community")]
        public object Community { get; set; }

        [DataMember(Name = "easyComment")]
        public EasyComment EasyComment { get; set; }

        [DataMember(Name = "external")]
        public External External { get; set; }

        [DataMember(Name = "genre")]
        public WelcomeGenre Genre { get; set; }

        [DataMember(Name = "marquee")]
        public Marquee Marquee { get; set; }

        [DataMember(Name = "media")]
        public Media Media { get; set; }

        [DataMember(Name = "okReason")]
        public string OkReason { get; set; }

        [DataMember(Name = "payment")]
        public Payment Payment { get; set; }

        [DataMember(Name = "owner")]
        public WelcomeOwner Owner { get; set; }

        [DataMember(Name = "pcWatchPage")]
        public PcWatchPage PcWatchPage { get; set; }

        [DataMember(Name = "player")]
        public Player Player { get; set; }

        [DataMember(Name = "ppv")]
        public Ppv Ppv { get; set; }

        [DataMember(Name = "ranking")]
        public Ranking Ranking { get; set; }

        [DataMember(Name = "series")]
        public Series Series { get; set; }

        [DataMember(Name = "smartphone")]
        public object Smartphone { get; set; }

        [DataMember(Name = "system")]
        public SystemClass System { get; set; }

        [DataMember(Name = "tag")]
        public Tag Tag { get; set; }

        [DataMember(Name = "video")]
        public WelcomeVideo Video { get; set; }

        [DataMember(Name = "videoAds")]
        public VideoAds VideoAds { get; set; }

        [DataMember(Name = "viewer")]
        public WelcomeViewer Viewer { get; set; }

        [DataMember(Name = "waku")]
        public Waku Waku { get; set; }
    }

    public class Ads
    {
        [DataMember(Name = "isAvailable")]
        public bool IsAvailable { get; set; }
    }

    public class Channel
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "isOfficialAnime")]
        public bool IsOfficialAnime { get; set; }

        [DataMember(Name = "isDisplayAdBanner")]
        public bool IsDisplayAdBanner { get; set; }

        [DataMember(Name = "thumbnail")]
        public ChannelThumbnail Thumbnail { get; set; }

        [DataMember(Name = "viewer")]
        public ChannelViewer Viewer { get; set; }
    }

    public class ChannelThumbnail
    {
        [DataMember(Name = "url")]
        public Uri Url { get; set; }

        [DataMember(Name = "smallUrl")]
        public Uri SmallUrl { get; set; }
    }

    public class ChannelViewer
    {
        [DataMember(Name = "follow")]
        public Follow Follow { get; set; }
    }

    public class Follow
    {
        [DataMember(Name = "isFollowed")]
        public bool IsFollowed { get; set; }

        [DataMember(Name = "isBookmarked")]
        public bool IsBookmarked { get; set; }

        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "tokenTimestamp")]
        public long TokenTimestamp { get; set; }
    }

    public class Client
    {
        [DataMember(Name = "nicosid")]
        public string Nicosid { get; set; }

        [DataMember(Name = "watchId")]
        public string WatchId { get; set; }

        [DataMember(Name = "watchTrackId")]
        public string WatchTrackId { get; set; }
    }

    public class Comment
    {
        [DataMember(Name = "server")]
        public Server Server { get; set; }

        [DataMember(Name = "keys")]
        public Keys Keys { get; set; }

        [DataMember(Name = "layers")]
        public Layer[] Layers { get; set; }

        [DataMember(Name = "threads")]
        public Thread[] Threads { get; set; }

        [DataMember(Name = "ng")]
        public Ng Ng { get; set; }

        [DataMember(Name = "isAttentionRequired")]
        public bool IsAttentionRequired { get; set; }
    }

    public class Keys
    {
        [DataMember(Name = "userKey")]
        public string UserKey { get; set; }
    }

    public class Layer
    {
        [DataMember(Name = "index")]
        public int Index { get; set; }

        [DataMember(Name = "isTranslucent")]
        public bool IsTranslucent { get; set; }

        [DataMember(Name = "threadIds")]
        public ThreadId[] ThreadIds { get; set; }
    }

    public class ThreadId
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "fork")]
        public int Fork { get; set; }
    }

    public class Ng
    {
        [DataMember(Name = "ngScore")]
        public NgScore NgScore { get; set; }

        [DataMember(Name = "channel")]
        public object[] Channel { get; set; }

        [DataMember(Name = "owner")]
        public object[] Owner { get; set; }

        [DataMember(Name = "viewer")]
        public NgViewer Viewer { get; set; }
    }

    public class NgScore
    {
        [DataMember(Name = "isDisabled")]
        public bool IsDisabled { get; set; }
    }

    public class NgViewer
    {
        [DataMember(Name = "revision")]
        public int Revision { get; set; }

        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "items")]
        public object[] Items { get; set; }
    }

    public class Server
    {
        [DataMember(Name = "url")]
        public Uri Url { get; set; }
    }

    public class Thread
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "fork")]
        public int Fork { get; set; }

        [DataMember(Name = "isActive")]
        public bool IsActive { get; set; }

        [DataMember(Name = "isDefaultPostTarget")]
        public bool IsDefaultPostTarget { get; set; }

        [DataMember(Name = "isEasyCommentPostTarget")]
        public bool IsEasyCommentPostTarget { get; set; }

        [DataMember(Name = "isLeafRequired")]
        public bool IsLeafRequired { get; set; }

        [DataMember(Name = "isOwnerThread")]
        public bool IsOwnerThread { get; set; }

        [DataMember(Name = "isThreadkeyRequired")]
        public bool IsThreadkeyRequired { get; set; }

        [DataMember(Name = "threadkey")]
        public object Threadkey { get; set; }

        [DataMember(Name = "is184Forced")]
        public bool Is184Forced { get; set; }

        [DataMember(Name = "hasNicoscript")]
        public bool HasNicoscript { get; set; }

        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "postkeyStatus")]
        public int PostkeyStatus { get; set; }

        [DataMember(Name = "server")]
        public Uri Server { get; set; }
    }

    public class EasyComment
    {
        [DataMember(Name = "phrases")]
        public Phrase[] Phrases { get; set; }
    }

    public class Phrase
    {
        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "nicodic")]
        public Nicodic Nicodic { get; set; }
    }

    public class Nicodic
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "viewTitle")]
        public string ViewTitle { get; set; }

        [DataMember(Name = "summary")]
        public string Summary { get; set; }

        [DataMember(Name = "link")]
        public Uri Link { get; set; }
    }

    public class External
    {
        [DataMember(Name = "commons")]
        public Commons Commons { get; set; }

        [DataMember(Name = "ichiba")]
        public Ichiba Ichiba { get; set; }
    }

    public class Commons
    {
        [DataMember(Name = "hasContentTree")]
        public bool HasContentTree { get; set; }
    }

    public class Ichiba
    {
        [DataMember(Name = "isEnabled")]
        public bool IsEnabled { get; set; }
    }

    public class WelcomeGenre
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "isImmoral")]
        public bool IsImmoral { get; set; }

        [DataMember(Name = "isDisabled")]
        public bool IsDisabled { get; set; }

        [DataMember(Name = "isNotSet")]
        public bool IsNotSet { get; set; }
    }

    public class Marquee
    {
        [DataMember(Name = "isDisabled")]
        public bool IsDisabled { get; set; }

        [DataMember(Name = "tagRelatedLead")]
        public object TagRelatedLead { get; set; }
    }

    public class Media
    {
        [DataMember(Name = "delivery")]
        public Delivery Delivery { get; set; }

        [DataMember(Name = "deliveryLegacy")]
        public object DeliveryLegacy { get; set; }
    }

    public class Delivery
    {
        [DataMember(Name = "recipeId")]
        public string RecipeId { get; set; }

        [DataMember(Name = "encryption")]
        public Encryption Encryption { get; set; }

        [DataMember(Name = "movie")]
        public Movie Movie { get; set; }

        [DataMember(Name = "storyboard")]
        public object Storyboard { get; set; }

        [DataMember(Name = "trackingId")]
        public string TrackingId { get; set; }
    }

    public class Encryption
    {
        [DataMember(Name = "encryptedKey")]
        public string EncryptedKey { get; set; }

        [DataMember(Name = "keyUri")]
        public string KeyUri { get; set; }
    }


    public class Movie
    {
        [DataMember(Name = "contentId")]
        public string ContentId { get; set; }

        [DataMember(Name = "audios")]
        public AudioContent[] Audios { get; set; }

        [DataMember(Name = "videos")]
        public VideoContent[] Videos { get; set; }

        [DataMember(Name = "session")]
        public Session Session { get; set; }
    }

    public class AudioContent
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "isAvailable")]
        public bool IsAvailable { get; set; }

        [DataMember(Name = "metadata")]
        public AudioMetadata Metadata { get; set; }
    }

    public class AudioMetadata
    {
        [DataMember(Name = "bitrate")]
        public int Bitrate { get; set; }

        [DataMember(Name = "samplingRate")]
        public int SamplingRate { get; set; }

        [DataMember(Name = "loudness")]
        public Loudness Loudness { get; set; }

        [DataMember(Name = "levelIndex")]
        public int LevelIndex { get; set; }

        [DataMember(Name = "loudnessCollection")]
        public LoudnessCollection[] LoudnessCollection { get; set; }


        [IgnoreDataMember]
        public double VideoLoudnessCollection => LoudnessCollection.First(x => x.Type == "video").Value;
    }

    public class Loudness
    {
        [DataMember(Name = "integratedLoudness")]
        public double IntegratedLoudness { get; set; }

        [DataMember(Name = "truePeak")]
        public double TruePeak { get; set; }
    }

    public class LoudnessCollection
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "value")]
        public double Value { get; set; }
    }

    public class Session
    {
        [DataMember(Name = "recipeId")]
        public string RecipeId { get; set; }

        [DataMember(Name = "playerId")]
        public string PlayerId { get; set; }

        [DataMember(Name = "videos")]
        public string[] Videos { get; set; }

        [DataMember(Name = "audios")]
        public string[] Audios { get; set; }

        [DataMember(Name = "movies")]
        public object[] Movies { get; set; }

        [DataMember(Name = "protocols")]
        public string[] Protocols { get; set; }

        [DataMember(Name = "authTypes")]
        public AuthTypes AuthTypes { get; set; }

        [DataMember(Name = "serviceUserId")]
        public string ServiceUserId { get; set; }

        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "signature")]
        public string Signature { get; set; }

        [DataMember(Name = "contentId")]
        public string ContentId { get; set; }

        [DataMember(Name = "heartbeatLifetime")]
        public int HeartbeatLifetime { get; set; }

        [DataMember(Name = "contentKeyTimeout")]
        public int ContentKeyTimeout { get; set; }

        [DataMember(Name = "priority")]
        public double Priority { get; set; }

        [DataMember(Name = "transferPresets")]
        public object[] TransferPresets { get; set; }

        [DataMember(Name = "urls")]
        public UrlData[] Urls { get; set; }
    }

    public class AuthTypes
    {
        [DataMember(Name = "http")]
        public string Http { get; set; }

        [DataMember(Name = "hls")]
        public string Hls { get; set; }
    }

    public class UrlData
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }

        public string UrlUnsafe => "https://api.dmc.nico/api/sessions";

        [DataMember(Name = "isWellKnownPort")]
        public bool IsWellKnownPort { get; set; }

        [DataMember(Name = "isSsl")]
        public bool IsSsl { get; set; }
    }

    public class VideoContent
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "isAvailable")]
        public bool IsAvailable { get; set; }

        [DataMember(Name = "metadata")]
        public VideoMetadata Metadata { get; set; }
    }

    public class VideoMetadata
    {
        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "bitrate")]
        public int Bitrate { get; set; }

        [DataMember(Name = "resolution")]
        public Resolution Resolution { get; set; }

        [DataMember(Name = "levelIndex")]
        public int LevelIndex { get; set; }

        [DataMember(Name = "recommendedHighestAudioLevelIndex")]
        public int RecommendedHighestAudioLevelIndex { get; set; }
    }

    public class Resolution
    {
        [DataMember(Name = "width")]
        public int Width { get; set; }

        [DataMember(Name = "height")]
        public int Height { get; set; }
    }

    public class WelcomeOwner
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }

        [DataMember(Name = "iconUrl")]
        public Uri IconUrl { get; set; }

        [DataMember(Name = "channel")]
        public object Channel { get; set; }

        [DataMember(Name = "live")]
        public object Live { get; set; }

        [DataMember(Name = "isVideosPublic")]
        public bool IsVideosPublic { get; set; }

        [DataMember(Name = "isMylistsPublic")]
        public bool IsMylistsPublic { get; set; }

        [DataMember(Name = "viewer")]
        public OwnerViewer Viewer { get; set; }
    }

    public class OwnerViewer
    {
        [DataMember(Name = "isFollowing")]
        public bool IsFollowing { get; set; }
    }

    public class PreviewPermission
    {
        [DataMember(Name = "isEnabled")]
        public bool IsEnabled { get; set; }
    }


    public partial class Payment
    {
        [DataMember(Name = "video")]
        public PaymentVideo Video { get; set; }

        [DataMember(Name = "preview")]
        public Preview Preview { get; set; }
    }

    public partial class Preview
    {
        [DataMember(Name = "ppv")]
        public PreviewPermission Ppv { get; set; }

        [DataMember(Name = "admission")]
        public PreviewPermission Admission { get; set; }

        [DataMember(Name = "premium")]
        public PreviewPermission Premium { get; set; }
    }

    public partial class PaymentVideo
    {
        [DataMember(Name = "isPpv")]
        public bool IsPpv { get; set; }

        [DataMember(Name = "isAdmission")]
        public bool IsAdmission { get; set; }

        [DataMember(Name = "isPremium")]
        public bool IsPremium { get; set; }

        [DataMember(Name = "watchableUserType")]
        public string WatchableUserType { get; set; }

        [DataMember(Name = "commentableUserType")]
        public string CommentableUserType { get; set; }
    }




    public class PcWatchPage
    {
        [DataMember(Name = "tagRelatedBanner")]
        public object TagRelatedBanner { get; set; }

        [DataMember(Name = "videoEnd")]
        public VideoEnd VideoEnd { get; set; }

        [DataMember(Name = "showOwnerMenu")]
        public bool ShowOwnerMenu { get; set; }

        [DataMember(Name = "showOwnerThreadCoEditingLink")]
        public bool ShowOwnerThreadCoEditingLink { get; set; }

        [DataMember(Name = "showMymemoryEditingLink")]
        public bool ShowMymemoryEditingLink { get; set; }
    }

    public class VideoEnd
    {
        [DataMember(Name = "bannerIn")]
        public object BannerIn { get; set; }

        [DataMember(Name = "overlay")]
        public object Overlay { get; set; }
    }

    public class Player
    {
        [DataMember(Name = "initialPlayback")]
        public object InitialPlayback { get; set; }

        [DataMember(Name = "comment")]
        public PlayerComment Comment { get; set; }

        [DataMember(Name = "layerMode")]
        public int LayerMode { get; set; }
    }

    public class PlayerComment
    {
        [DataMember(Name = "isDefaultInvisible")]
        public bool IsDefaultInvisible { get; set; }
    }

    public class Ppv
    {
        [DataMember(Name = "accessFrom")]
        public object AccessFrom { get; set; }
    }

    public class Ranking
    {
        [DataMember(Name = "genre")]
        public RankingGenre Genre { get; set; }

        [DataMember(Name = "popularTag")]
        public PopularTag[] PopularTag { get; set; }
    }

    public class RankingGenre
    {
        [DataMember(Name = "rank")]
        public int Rank { get; set; }

        [DataMember(Name = "genre")]
        public string Genre { get; set; }

        [DataMember(Name = "dateTime")]
        public DateTimeOffset DateTime { get; set; }
    }

    public class PopularTag
    {
        [DataMember(Name = "tag")]
        public string Tag { get; set; }

        [DataMember(Name = "regularizedTag")]
        public string RegularizedTag { get; set; }

        [DataMember(Name = "rank")]
        public int Rank { get; set; }

        [DataMember(Name = "genre")]
        public string Genre { get; set; }

        [DataMember(Name = "dateTime")]
        public DateTimeOffset DateTime { get; set; }
    }

    public class Series
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "thumbnailUrl")]
        public Uri ThumbnailUrl { get; set; }

        [DataMember(Name = "video")]
        public SeriesVideo Video { get; set; }
    }

    public class SeriesVideo
    {
        [DataMember(Name = "prev")]
        public SeriesVideoContent Prev { get; set; }

        [DataMember(Name = "next")]
        public SeriesVideoContent Next { get; set; }

        [DataMember(Name = "first")]
        public SeriesVideoContent First { get; set; }
    }

    public class SeriesVideoContent
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "registeredAt")]
        public DateTimeOffset RegisteredAt { get; set; }

        [DataMember(Name = "count")]
        public Count Count { get; set; }

        [DataMember(Name = "thumbnail")]
        public FirstThumbnail Thumbnail { get; set; }

        [DataMember(Name = "duration")]
        public int Duration { get; set; }

        [DataMember(Name = "shortDescription")]
        public string ShortDescription { get; set; }

        [DataMember(Name = "latestCommentSummary")]
        public string LatestCommentSummary { get; set; }

        [DataMember(Name = "isChannelVideo")]
        public bool IsChannelVideo { get; set; }

        [DataMember(Name = "isPaymentRequired")]
        public bool IsPaymentRequired { get; set; }

        [DataMember(Name = "playbackPosition")]
        public object PlaybackPosition { get; set; }

        [DataMember(Name = "owner")]
        public FirstOwner Owner { get; set; }

        [DataMember(Name = "requireSensitiveMasking")]
        public bool RequireSensitiveMasking { get; set; }

        [DataMember(Name = "9d091f87")]
        public bool The9D091F87 { get; set; }

        [DataMember(Name = "acf68865")]
        public bool Acf68865 { get; set; }
    }

    public class Count
    {
        [DataMember(Name = "view")]
        public int View { get; set; }

        [DataMember(Name = "comment")]
        public int Comment { get; set; }

        [DataMember(Name = "mylist")]
        public int Mylist { get; set; }

        [DataMember(Name = "like")]
        public int Like { get; set; }
    }

    public class FirstOwner
    {
        [DataMember(Name = "ownerType")]
        public string OwnerType { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "iconUrl")]
        public Uri IconUrl { get; set; }
    }

    public class FirstThumbnail
    {
        [DataMember(Name = "url")]
        public Uri Url { get; set; }

        [DataMember(Name = "middleUrl")]
        public Uri MiddleUrl { get; set; }

        [DataMember(Name = "largeUrl")]
        public Uri LargeUrl { get; set; }

        [DataMember(Name = "listingUrl")]
        public Uri ListingUrl { get; set; }

        [DataMember(Name = "nHdUrl")]
        public Uri NHdUrl { get; set; }
    }

    public class SystemClass
    {
        [DataMember(Name = "serverTime")]
        public DateTimeOffset ServerTime { get; set; }

        [DataMember(Name = "isPeakTime")]
        public bool IsPeakTime { get; set; }
    }

    public class Tag
    {
        [DataMember(Name = "items")]
        public TagItem[] Items { get; set; }

        [DataMember(Name = "hasR18Tag")]
        public bool HasR18Tag { get; set; }

        [DataMember(Name = "isPublishedNicoscript")]
        public bool IsPublishedNicoscript { get; set; }

        [DataMember(Name = "edit")]
        public Edit Edit { get; set; }

        [DataMember(Name = "viewer")]
        public Edit Viewer { get; set; }
    }

    public class Edit
    {
        [DataMember(Name = "isEditable")]
        public bool IsEditable { get; set; }

        [DataMember(Name = "uneditableReason")]
        public object UneditableReason { get; set; }

        [DataMember(Name = "editKey")]
        public string EditKey { get; set; }
    }

    public class TagItem
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "isCategory")]
        public bool IsCategory { get; set; }

        [DataMember(Name = "isCategoryCandidate")]
        public bool IsCategoryCandidate { get; set; }

        [DataMember(Name = "isNicodicArticleExists")]
        public bool IsNicodicArticleExists { get; set; }

        [DataMember(Name = "isLocked")]
        public bool IsLocked { get; set; }
    }

    public class WelcomeVideo
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "count")]
        public Count Count { get; set; }

        [DataMember(Name = "duration")]
        public int Duration { get; set; }

        [DataMember(Name = "thumbnail")]
        public VideoThumbnail Thumbnail { get; set; }

        [DataMember(Name = "rating")]
        public Rating Rating { get; set; }

        [DataMember(Name = "registeredAt")]
        public DateTimeOffset RegisteredAt { get; set; }

        [DataMember(Name = "isPrivate")]
        public bool IsPrivate { get; set; }

        [DataMember(Name = "isDeleted")]
        public bool IsDeleted { get; set; }

        [DataMember(Name = "isNoBanner")]
        public bool IsNoBanner { get; set; }

        [DataMember(Name = "isAuthenticationRequired")]
        public bool IsAuthenticationRequired { get; set; }

        [DataMember(Name = "isEmbedPlayerAllowed")]
        public bool IsEmbedPlayerAllowed { get; set; }

        [DataMember(Name = "viewer")]
        public VideoViewer Viewer { get; set; }

        [DataMember(Name = "watchableUserTypeForPayment")]
        public string WatchableUserTypeForPayment { get; set; }

        [DataMember(Name = "commentableUserTypeForPayment")]
        public string CommentableUserTypeForPayment { get; set; }

        [DataMember(Name = "9d091f87")]
        public bool The9D091F87 { get; set; }
    }

    public class Rating
    {
        [DataMember(Name = "isAdult")]
        public bool IsAdult { get; set; }
    }

    public class VideoThumbnail
    {
        [DataMember(Name = "url")]
        public Uri Url { get; set; }

        [DataMember(Name = "middleUrl")]
        public Uri MiddleUrl { get; set; }

        [DataMember(Name = "largeUrl")]
        public Uri LargeUrl { get; set; }

        [DataMember(Name = "player")]
        public Uri Player { get; set; }

        [DataMember(Name = "ogp")]
        public Uri Ogp { get; set; }
    }

    public class VideoViewer
    {
        [DataMember(Name = "isOwner")]
        public bool IsOwner { get; set; }

        [DataMember(Name = "like")]
        public Like Like { get; set; }
    }

    public class Like
    {
        [DataMember(Name = "isLiked")]
        public bool IsLiked { get; set; }

        [DataMember(Name = "count")]
        public object Count { get; set; }
    }

    public class VideoAds
    {
        [DataMember(Name = "additionalParams")]
        public VideoAdsAdditionalParams AdditionalParams { get; set; }

        [DataMember(Name = "items")]
        public VideoAdsItem[] Items { get; set; }

        [DataMember(Name = "reason")]
        public string Reason { get; set; }
    }

    public class VideoAdsAdditionalParams
    {
        [DataMember(Name = "videoId")]
        public string VideoId { get; set; }

        [DataMember(Name = "videoDuration")]
        public int VideoDuration { get; set; }

        [DataMember(Name = "isAdultRatingNG")]
        public bool IsAdultRatingNg { get; set; }

        [DataMember(Name = "isAuthenticationRequired")]
        public bool IsAuthenticationRequired { get; set; }

        [DataMember(Name = "isR18")]
        public bool IsR18 { get; set; }

        [DataMember(Name = "nicosid")]
        public string Nicosid { get; set; }

        [DataMember(Name = "lang")]
        public string Lang { get; set; }

        [DataMember(Name = "watchTrackId")]
        public string WatchTrackId { get; set; }

        [DataMember(Name = "genre")]
        public string Genre { get; set; }

        [DataMember(Name = "gender")]
        public int Gender { get; set; }

        [DataMember(Name = "age")]
        public int Age { get; set; }
    }

    public class VideoAdsItem
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "timingMs")]
        public object TimingMs { get; set; }

        [DataMember(Name = "additionalParams")]
        public ItemAdditionalParams AdditionalParams { get; set; }
    }

    public class ItemAdditionalParams
    {
        [DataMember(Name = "linearType")]
        public string LinearType { get; set; }

        [DataMember(Name = "adIdx")]
        public int AdIdx { get; set; }

        [DataMember(Name = "skipType")]
        public int SkipType { get; set; }

        [DataMember(Name = "skippableType")]
        public int SkippableType { get; set; }

        [DataMember(Name = "pod")]
        public int Pod { get; set; }
    }

    public class WelcomeViewer
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }

        [DataMember(Name = "isPremium")]
        public bool IsPremium { get; set; }

        [DataMember(Name = "existence")]
        public Existence Existence { get; set; }
    }

    public class Existence
    {
        [DataMember(Name = "age")]
        public int Age { get; set; }

        [DataMember(Name = "prefecture")]
        public string Prefecture { get; set; }

        [DataMember(Name = "sex")]
        public string Sex { get; set; }
    }

    public class Waku
    {
        [DataMember(Name = "information")]
        public object Information { get; set; }

        [DataMember(Name = "bgImages")]
        public object[] BgImages { get; set; }

        [DataMember(Name = "addContents")]
        public object AddContents { get; set; }

        [DataMember(Name = "addVideo")]
        public object AddVideo { get; set; }

        [DataMember(Name = "tagRelatedBanner")]
        public TagRelatedBanner TagRelatedBanner { get; set; }

        [DataMember(Name = "tagRelatedMarquee")]
        public TagRelatedMarquee TagRelatedMarquee { get; set; }
    }

    public class TagRelatedBanner
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "imageUrl")]
        public Uri ImageUrl { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "isEvent")]
        public bool IsEvent { get; set; }

        [DataMember(Name = "linkUrl")]
        public Uri LinkUrl { get; set; }

        [DataMember(Name = "isNewWindow")]
        public bool IsNewWindow { get; set; }
    }

    public class TagRelatedMarquee
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "linkUrl")]
        public Uri LinkUrl { get; set; }

        [DataMember(Name = "isNewWindow")]
        public bool IsNewWindow { get; set; }
    }

    #endregion


    #region Data Environment

    [DataContract]
    public class Updated
    {

        [DataMember(Name = "timestampS")]
        public int? TimestampS { get; set; }

        [DataMember(Name = "isNew")]
        public bool? IsNew { get; set; }
    }

    [DataContract]
    public class BaseURL
    {

        [DataMember(Name = "web")]
        public string Web { get; set; }

        [DataMember(Name = "res")]
        public string Res { get; set; }

        [DataMember(Name = "dic")]
        public string Dic { get; set; }

        [DataMember(Name = "flapi")]
        public string Flapi { get; set; }

        [DataMember(Name = "riapi")]
        public string Riapi { get; set; }

        [DataMember(Name = "live")]
        public string Live { get; set; }

        [DataMember(Name = "com")]
        public string Com { get; set; }

        [DataMember(Name = "ch")]
        public string Ch { get; set; }

        [DataMember(Name = "secureCh")]
        public string SecureCh { get; set; }

        [DataMember(Name = "commons")]
        public string Commons { get; set; }

        [DataMember(Name = "commonsAPI")]
        public string CommonsAPI { get; set; }

        [DataMember(Name = "embed")]
        public string Embed { get; set; }

        [DataMember(Name = "ext")]
        public string Ext { get; set; }

        [DataMember(Name = "nicoMs")]
        public string NicoMs { get; set; }

        [DataMember(Name = "ichiba")]
        public string Ichiba { get; set; }

        [DataMember(Name = "uadAPI")]
        public string UadAPI { get; set; }

        [DataMember(Name = "ads")]
        public string Ads { get; set; }

        [DataMember(Name = "account")]
        public string Account { get; set; }

        [DataMember(Name = "secure")]
        public string Secure { get; set; }

        [DataMember(Name = "ex")]
        public string Ex { get; set; }

        [DataMember(Name = "qa")]
        public string Qa { get; set; }

        [DataMember(Name = "publicAPI")]
        public string PublicAPI { get; set; }

        [DataMember(Name = "uad")]
        public string Uad { get; set; }

        [DataMember(Name = "app")]
        public string App { get; set; }

        [DataMember(Name = "appClientAPI")]
        public string AppClientAPI { get; set; }

        [DataMember(Name = "point")]
        public string Point { get; set; }

        [DataMember(Name = "enquete")]
        public string Enquete { get; set; }

        [DataMember(Name = "notification")]
        public string Notification { get; set; }

        [DataMember(Name = "upload")]
        public string Upload { get; set; }

        [DataMember(Name = "sugoiSearchSystem")]
        public string SugoiSearchSystem { get; set; }
    }

    [DataContract]
    public class I18n
    {

        [DataMember(Name = "language")]
        public string Language { get; set; }

        [DataMember(Name = "locale")]
        public string Locale { get; set; }

        [DataMember(Name = "area")]
        public string Area { get; set; }

        [DataMember(Name = "footer")]
        public object Footer { get; set; }
    }

    [DataContract]
    public class Urls
    {

        [DataMember(Name = "playerHelp")]
        public string PlayerHelp { get; set; }
    }

    [DataContract]
    public class DmcWatchEnvironment
    {

        [DataMember(Name = "updated")]
        public Updated Updated { get; set; }

        [DataMember(Name = "baseURL")]
        public BaseURL BaseURL { get; set; }

        [DataMember(Name = "playlistToken")]
        public string PlaylistToken { get; set; }

        [DataMember(Name = "i18n")]
        public I18n I18n { get; set; }

        [DataMember(Name = "urls")]
        public Urls Urls { get; set; }

        [DataMember(Name = "isMonitoringLogUser")]
        public bool? IsMonitoringLogUser { get; set; }
    }

    #endregion

}
