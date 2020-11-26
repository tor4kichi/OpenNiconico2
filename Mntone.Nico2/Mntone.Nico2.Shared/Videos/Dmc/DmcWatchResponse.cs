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



    #region Initial Watch Data

    [DataContract]
    public class DmcVideo
    {

        [DataMember(Name = "video_id")]
        public string VideoId { get; set; }

        [DataMember(Name = "length_seconds")]
        public int LengthSeconds { get; set; }

        [DataMember(Name = "deleted")]
        public int Deleted { get; set; }
    }

    [DataContract]
    public class DmcThread
    {

        [DataMember(Name = "server_url")]
        public string ServerUrl { get; set; }

        [DataMember(Name = "sub_server_url")]
        public string SubServerUrl { get; set; }

        [DataMember(Name = "thread_id")]
        public int ThreadId { get; set; }

        [DataMember(Name = "nicos_thread_id")]
        public object NicosThreadId { get; set; }

        [DataMember(Name = "optional_thread_id")]
        public object OptionalThreadId { get; set; }

        [DataMember(Name = "thread_key_required")]
        public bool ThreadKeyRequired { get; set; }

        [DataMember(Name = "channel_ng_words")]
        public IList<object> ChannelNgWords { get; set; }

        [DataMember(Name = "owner_ng_words")]
        public IList<object> OwnerNgWords { get; set; }

        [DataMember(Name = "maintenances_ng")]
        public bool MaintenancesNg { get; set; }

        [DataMember(Name = "postkey_available")]
        public bool PostkeyAvailable { get; set; }

        [DataMember(Name = "ng_revision")]
        public int? NgRevision { get; set; }
    }

    [DataContract]
    public class User
    {

        [DataMember(Name = "user_id")]
        public int UserId { get; set; }

        [DataMember(Name = "is_premium")]
        public bool IsPremium { get; set; }

        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }
    }

    [DataContract]
    public class Hiroba
    {

        [DataMember(Name = "fms_token")]
        public object FmsToken { get; set; }

        [DataMember(Name = "server_url")]
        public string ServerUrl { get; set; }

        [DataMember(Name = "server_port")]
        public int? ServerPort { get; set; }

        [DataMember(Name = "thread_id")]
        public int? ThreadId { get; set; }

        [DataMember(Name = "thread_key")]
        public string ThreadKey { get; set; }
    }


    [DataContract]
    public class HlsEncryptionV1
    {

        [DataMember(Name = "encrypted_key")]
        public string EncryptedKey { get; set; }

        [DataMember(Name = "key_uri")]
        public string KeyUri { get; set; }
    }

    [DataContract]
    public class Encryption
    {

        [DataMember(Name = "hls_encryption_v1")]
        public HlsEncryptionV1 HlsEncryptionV1 { get; set; }
    }

    [DataContract]
    public class AuthTypes
    {

        [DataMember(Name = "hls")]
        public string Hls { get; set; }
    }


    [DataContract]
    public class UrlInfo
    {

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "is_well_known_port")]
        public bool IsWellKnownPort { get; set; }

        [DataMember(Name = "is_ssl")]
        public bool IsSsl { get; set; }
    }

    [DataContract]
    public class SessionApi
    {

        [DataMember(Name = "recipe_id")]
        public string RecipeId { get; set; }

        [DataMember(Name = "player_id")]
        public string PlayerId { get; set; }

        [DataMember(Name = "videos")]
        public IList<string> Videos { get; set; }

        [DataMember(Name = "audios")]
        public IList<string> Audios { get; set; }

        [DataMember(Name = "movies")]
        public IList<object> Movies { get; set; }

        [DataMember(Name = "protocols")]
        public IList<string> Protocols { get; set; }

        [DataMember(Name = "auth_types")]
        public AuthTypes AuthTypes { get; set; }

        [DataMember(Name = "service_user_id")]
        public string ServiceUserId { get; set; }

        [DataMember(Name = "token")]
        public string Token { get; set; }

        [DataMember(Name = "signature")]
        public string Signature { get; set; }

        [DataMember(Name = "content_id")]
        public string ContentId { get; set; }

        [DataMember(Name = "heartbeat_lifetime")]
        public int HeartbeatLifetime { get; set; }

        [DataMember(Name = "content_key_timeout")]
        public int ContentKeyTimeout { get; set; }

        [DataMember(Name = "priority")]
        public double Priority { get; set; }

        [DataMember(Name = "urls")]
        public IList<UrlInfo> Urls { get; set; }
    }

    [DataContract]
    public class Resolution
    {

        [DataMember(Name = "width")]
        public int Width { get; set; }

        [DataMember(Name = "height")]
        public int Height { get; set; }
    }

    [DataContract]
    public class VideoContent
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "available")]
        public bool Available { get; set; }

        [DataMember(Name = "bitrate")]
        public int Bitrate { get; set; }

        [DataMember(Name = "resolution")]
        public Resolution Resolution { get; set; }
    }

    [DataContract]
    public class Loudness
    {
        [DataMember(Name = "integratedLoudness")]
        public double IntegratedLoudness { get; set; }

        [DataMember(Name = "truePeak")]
        public double TruePeak { get; set; }
    }

    [DataContract]
    public class AudioContent
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "available")]
        public bool Available { get; set; }

        [DataMember(Name = "bitrate")]
        public int Bitrate { get; set; }

        [DataMember(Name = "sampling_rate")]
        public int SamplingRate { get; set; }

        [DataMember(Name = "loudness")]
        public Loudness Loudness { get; set; }

        [DataMember(Name = "loudness_correction_value")]
        public IList<LoudnessCorrectionValue> LoudnessCorrectionValue { get; set; }

        public double VideoLoudnessCorrectionValue => LoudnessCorrectionValue.First(x => x.Type == "video").Value;
    }

    [DataContract]
    public class Quality
    {

        [DataMember(Name = "videos")]
        public IList<VideoContent> Videos { get; set; }

        [DataMember(Name = "audios")]
        public IList<AudioContent> Audios { get; set; }
    }

    [DataContract]
    public class DmcInfo
    {

        [DataMember(Name = "time")]
        public int Time { get; set; }

        [DataMember(Name = "time_ms")]
        public long TimeMs { get; set; }

        [DataMember(Name = "video")]
        public DmcVideo Video { get; set; }

        [DataMember(Name = "thread")]
        public DmcThread Thread { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "hiroba")]
        public Hiroba Hiroba { get; set; }

        [DataMember(Name = "error")]
        public object Error { get; set; }

        [DataMember(Name = "session_api")]
        public SessionApi SessionApi { get; set; }

        [DataMember(Name = "storyboard_session_api")]
        public object StoryboardSessionApi { get; set; }

        [DataMember(Name = "quality")]
        public Quality Quality { get; set; }

        [DataMember(Name = "encryption")]
        public Encryption Encryption { get; set; }

        [DataMember(Name = "tracking_id")]
        public string TrackingId { get; set; }
    }

    [DataContract]
    public class LoudnessCorrectionValue
    {

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "value")]
        public double Value { get; set; }
    }

    [DataContract]
    public class SmileInfo
    {

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "isSlowLine")]
        public bool IsSlowLine { get; set; }

        [DataMember(Name = "currentQualityId")]
        public string CurrentQualityId { get; set; }

        [DataMember(Name = "qualityIds")]
        public IList<string> QualityIds { get; set; }

        [DataMember(Name = "loudnessCorrectionValue")]
        public IList<LoudnessCorrectionValue> LoudnessCorrectionValue { get; set; }

        public double VideoLoudnessCorrectionValue => LoudnessCorrectionValue.First(x => x.Type == "video").Value;
    }

    [DataContract]
    public class Video
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "originalTitle")]
        public string OriginalTitle { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "originalDescription")]
        public string OriginalDescription { get; set; }

        [DataMember(Name = "thumbnailURL")]
        public string ThumbnailURL { get; set; }

        [DataMember(Name = "postedDateTime")]
        public string PostedDateTime { get; set; }

        [DataMember(Name = "originalPostedDateTime")]
        public object OriginalPostedDateTime { get; set; }

        [DataMember(Name = "width")]
        public int? Width { get; set; }

        [DataMember(Name = "height")]
        public int? Height { get; set; }

        [DataMember(Name = "duration")]
        public int Duration { get; set; }

        [DataMember(Name = "viewCount")]
        public int ViewCount { get; set; }

        [DataMember(Name = "mylistCount")]
        public int MylistCount { get; set; }

        [DataMember(Name = "translation")]
        public object Translation { get; set; }

        [DataMember(Name = "translator")]
        public object Translator { get; set; }

        [DataMember(Name = "movieType")]
        public string MovieType { get; set; }

        [DataMember(Name = "badges")]
        public object Badges { get; set; }

        [DataMember(Name = "introducedNicoliveDJInfo")]
        public object IntroducedNicoliveDJInfo { get; set; }

        [DataMember(Name = "dmcInfo")]
        public DmcInfo DmcInfo { get; set; }

        [DataMember(Name = "backCommentType")]
        public object BackCommentType { get; set; }

        [DataMember(Name = "isCommentExpired")]
        public bool IsCommentExpired { get; set; }

        [DataMember(Name = "isWide")]
        public string IsWide { get; set; }

        [DataMember(Name = "isOfficialAnime")]
        public object IsOfficialAnime { get; set; }

        [DataMember(Name = "isNoBanner")]
        public object IsNoBanner { get; set; }

        [DataMember(Name = "isDeleted")]
        public bool IsDeleted { get; set; }

        [DataMember(Name = "isTranslated")]
        public bool IsTranslated { get; set; }

        [DataMember(Name = "isR18")]
        public bool IsR18 { get; set; }

        [DataMember(Name = "isAdult")]
        public bool IsAdult { get; set; }

        [DataMember(Name = "isNicowari")]
        public object IsNicowari { get; set; }

        [DataMember(Name = "isPublic")]
        public bool IsPublic { get; set; }

        [DataMember(Name = "isPublishedNicoscript")]
        public object IsPublishedNicoscript { get; set; }

        [DataMember(Name = "isNoNGS")]
        public object IsNoNGS { get; set; }

        [DataMember(Name = "isCommunityMemberOnly")]
        public string IsCommunityMemberOnly { get; set; }

        [DataMember(Name = "isCommonsTreeExists")]
        public bool? IsCommonsTreeExists { get; set; }

        [DataMember(Name = "isNoIchiba")]
        public bool IsNoIchiba { get; set; }

        [DataMember(Name = "isOfficial")]
        public bool IsOfficial { get; set; }

        [DataMember(Name = "isMonetized")]
        public bool IsMonetized { get; set; }

        [DataMember(Name = "smileInfo")]
        public SmileInfo SmileInfo { get; set; }
    }

    [DataContract]
    public class Player
    {

        [DataMember(Name = "playerInfoXMLUpdateTIme")]
        public int PlayerInfoXMLUpdateTIme { get; set; }

        [DataMember(Name = "isContinuous")]
        public bool IsContinuous { get; set; }
    }


    [DataContract]
    public class ThreadFragment
    {

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "fork")]
        public int Fork { get; set; }

        [DataMember(Name = "isActive")]
        public bool IsActive { get; set; }

        [DataMember(Name = "postkeyStatus")]
        public int PostkeyStatus { get; set; }

        [DataMember(Name = "isDefaultPostTarget")]
        public bool IsDefaultPostTarget { get; set; }

        [DataMember(Name = "isThreadkeyRequired")]
        public bool IsThreadkeyRequired { get; set; }

        [DataMember(Name = "isLeafRequired")]
        public bool IsLeafRequired { get; set; }

        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "isOwnerThread")]
        public bool IsOwnerThread { get; set; }

        [DataMember(Name = "hasNicoscript")]
        public bool HasNicoscript { get; set; }
    }

    [DataContract]
    public class ThreadId
    {

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "fork")]
        public int Fork { get; set; }
    }

    [DataContract]
    public class Layer
    {

        [DataMember(Name = "index")]
        public int Index { get; set; }

        [DataMember(Name = "isTranslucent")]
        public bool IsTranslucent { get; set; }

        [DataMember(Name = "threadIds")]
        public IList<ThreadId> ThreadIds { get; set; }
    }

    [DataContract]
    public class CommentComposite
    {

        [DataMember(Name = "threads")]
        public IList<ThreadFragment> Threads { get; set; }

        [DataMember(Name = "layers")]
        public IList<Layer> Layers { get; set; }
    }


    [DataContract]
    public class Tag
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "isCategory")]
        public bool IsCategory { get; set; }

        [DataMember(Name = "isCategoryCandidate")]
        public object IsCategoryCandidate { get; set; }

        [DataMember(Name = "isDictionaryExists")]
        public bool IsDictionaryExists { get; set; }

        [DataMember(Name = "isLocked")]
        public bool IsLocked { get; set; }
    }

    [DataContract]
    public class Item
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "requestId")]
        public string RequestId { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "thumbnailURL")]
        public string ThumbnailURL { get; set; }

        [DataMember(Name = "viewCounter")]
        public string __ViewCounter { get; set; }

        private int? _ViewCounter;
        public int ViewCounter => (_ViewCounter ?? (_ViewCounter = int.Parse(__ViewCounter))).Value;

        // Note: numResは文字列と数値のどちらもあるようなので高度な柔軟性を維持しつつ臨機応変に対応する
        // というか数値に統一されていないのは大変遺憾である。ぷち怒
        [DataMember(Name = "numRes")]
        public object __NumRes { get; set; }

        private int? _NumRes;
        public int NumRes
        {
            get
            {
                if (!_NumRes.HasValue)
                {
                    if (__NumRes is long)
                    {
                        _NumRes = (int)(long)__NumRes;
                    }
                    if (__NumRes is string)
                    {
                        _NumRes = int.Parse((string)__NumRes);
                    }
                }

                return _NumRes ?? 0;
            }
        }


        [DataMember(Name = "mylistCounter")]
        public string __MylistCounter { get; set; }

        private int? _MylistCounter;
        public int MylistCounter => (_MylistCounter ?? (_MylistCounter = int.Parse(__MylistCounter))).Value;


        [DataMember(Name = "firstRetrieve")]
        public string __FirstRetrieve { get; set; }

        private DateTime? _FirstRetrieve;
        public DateTime FirstRetrieve => (_FirstRetrieve ?? (_FirstRetrieve = DateTime.Parse(__FirstRetrieve))).Value;

        [DataMember(Name = "lengthSeconds")]
        public string __LengthSeconds { get; set; }

        private TimeSpan? _LengthSeconds;
        public TimeSpan LengthSeconds => (_LengthSeconds ?? (_LengthSeconds = TimeSpan.FromSeconds(int.Parse(__LengthSeconds)))).Value;


        [DataMember(Name = "threadUpdateTime")]
        public string __ThreadUpdateTime { get; set; }

        private DateTime? _ThreadUpdateTime;
        public DateTime ThreadUpdateTime => (_ThreadUpdateTime ?? (_ThreadUpdateTime = DateTime.Parse(__ThreadUpdateTime))).Value;

        [DataMember(Name = "createTime")]
        public string __CreateTime { get; set; }

        private DateTime? _CreateTime;
        public DateTime CreateTime => (_CreateTime ?? (_CreateTime = DateTime.Parse(__CreateTime))).Value;


        [DataMember(Name = "width")]
        public int? Width { get; set; }

        [DataMember(Name = "height")]
        public int? Height { get; set; }

        [DataMember(Name = "isTranslated")]
        public bool IsTranslated { get; set; }

        [DataMember(Name = "mylistComment")]
        public int? MylistComment { get; set; }

        [DataMember(Name = "tkasType")]
        public object TkasType { get; set; }

        [DataMember(Name = "hasData")]
        public bool HasData { get; set; }
    }

    [DataContract]
    public class Playlist
    {
        [DataMember(Name = "watchId")]
        public string WatchId { get; set; }

        [DataMember(Name = "referer")]
        public string Referer { get; set; }

        [DataMember(Name = "parameter")]
        public string Parameter { get; set; }
    }

    [DataContract]
    public class Owner
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }

        [DataMember(Name = "stampExp")]
        public string StampExp { get; set; }

        [DataMember(Name = "iconURL")]
        public string IconURL { get; set; }

        [DataMember(Name = "nicoliveInfo")]
        public object NicoliveInfo { get; set; }

        [DataMember(Name = "channelInfo")]
        public object ChannelInfo { get; set; }

        [DataMember(Name = "isUserVideoPublic")]
        public bool IsUserVideoPublic { get; set; }

        [DataMember(Name = "isUserMyVideoPublic")]
        public bool IsUserMyVideoPublic { get; set; }

        [DataMember(Name = "isUserOpenListPublic")]
        public bool IsUserOpenListPublic { get; set; }
    }

    [DataContract]
    public class Viewer
    {

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }

        [DataMember(Name = "prefecture")]
        public int? Prefecture { get; set; }

        [DataMember(Name = "sex")]
        public int? Sex { get; set; }

        [DataMember(Name = "age")]
        public int? Age { get; set; }

        [DataMember(Name = "isPremium")]
        public bool IsPremium { get; set; }

        [DataMember(Name = "isPrivileged")]
        public bool? IsPrivileged { get; set; }

        [DataMember(Name = "isPostLocked")]
        public bool? IsPostLocked { get; set; }

        [DataMember(Name = "isHtrzm")]
        public bool? IsHtrzm { get; set; }

        [DataMember(Name = "isTwitterConnection")]
        public bool? IsTwitterConnection { get; set; }
    }

    [DataContract]
    public class Channel
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "iconURL")]
        public string IconURL { get; set; }

        [DataMember(Name = "favoriteToken")]
        public string FavoriteToken { get; set; }

        [DataMember(Name = "favoriteTokenTime")]
        public int? FavoriteTokenTime { get; set; }

        [DataMember(Name = "isFavorited")]
        public bool IsFavorited { get; set; }

        [DataMember(Name = "ngList")]
        public IList<NgList> NgList { get; set; }

        [DataMember(Name = "threadType")]
        public string ThreadType { get; set; }

        [DataMember(Name = "globalId")]
        public string GlobalId { get; set; }
    }

    [DataContract]
    public class NgList
    {

        [DataMember(Name = "source")]
        public string Source { get; set; }

        [DataMember(Name = "destination")]
        public string Destination { get; set; }
    }

    [DataContract]
    public class Ad
    {

        [DataMember(Name = "vastMetaData")]
        public object VastMetaData { get; set; }
    }

    [DataContract]
    public class TagRelatedBanner
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "thumbnailURL")]
        public string ThumbnailURL { get; set; }
    }

    [DataContract]
    public class Lead
    {

        [DataMember(Name = "tagRelatedMarquee")]
        public WatchAPI.TagRelatedMarquee TagRelatedMarquee { get; set; }

        [DataMember(Name = "tagRelatedBanner")]
        public TagRelatedBanner TagRelatedBanner { get; set; }

        [DataMember(Name = "nicosdkApplicationBanner")]
        public object NicosdkApplicationBanner { get; set; }

        [DataMember(Name = "videoEndBannerIn")]
        public object VideoEndBannerIn { get; set; }

        [DataMember(Name = "videoEndOverlay")]
        public object VideoEndOverlay { get; set; }
    }

    [DataContract]
    public class OwnerNGList
    {

        [DataMember(Name = "source")]
        public string Source { get; set; }

        [DataMember(Name = "destination")]
        public string Destination { get; set; }
    }

    [DataContract]
    public class Context
    {

        [DataMember(Name = "playFrom")]
        public object PlayFrom { get; set; }

        [DataMember(Name = "initialPlaybackPosition")]
        public object InitialPlaybackPosition { get; set; }

        [DataMember(Name = "initialPlaybackType")]
        public object InitialPlaybackType { get; set; }

        [DataMember(Name = "playLength")]
        public object PlayLength { get; set; }

        [DataMember(Name = "returnId")]
        public object ReturnId { get; set; }

        [DataMember(Name = "returnTo")]
        public object ReturnTo { get; set; }

        [DataMember(Name = "returnMsg")]
        public object ReturnMsg { get; set; }

        [DataMember(Name = "watchId")]
        public string WatchId { get; set; }

        [DataMember(Name = "isNoMovie")]
        public object IsNoMovie { get; set; }

        [DataMember(Name = "isNoRelatedVideo")]
        public object IsNoRelatedVideo { get; set; }

        [DataMember(Name = "isDownloadCompleteWait")]
        public object IsDownloadCompleteWait { get; set; }

        [DataMember(Name = "isNoNicotic")]
        public object IsNoNicotic { get; set; }

        [DataMember(Name = "isNeedPayment")]
        public bool IsNeedPayment { get; set; }

        [DataMember(Name = "isAdultRatingNG")]
        public bool IsAdultRatingNG { get; set; }

        [DataMember(Name = "isPlayable")]
        public object IsPlayable { get; set; }

        [DataMember(Name = "isTranslatable")]
        public bool IsTranslatable { get; set; }

        [DataMember(Name = "isTagUneditable")]
        public bool IsTagUneditable { get; set; }

        [DataMember(Name = "isVideoOwner")]
        public bool IsVideoOwner { get; set; }

        [DataMember(Name = "isThreadOwner")]
        public bool IsThreadOwner { get; set; }

        [DataMember(Name = "isOwnerThreadEditable")]
        public object IsOwnerThreadEditable { get; set; }

        [DataMember(Name = "useChecklistCache")]
        public object UseChecklistCache { get; set; }

        [DataMember(Name = "isDisabledMarquee")]
        public object IsDisabledMarquee { get; set; }

        [DataMember(Name = "isDictionaryDisplayable")]
        public bool IsDictionaryDisplayable { get; set; }

        [DataMember(Name = "isDefaultCommentInvisible")]
        public bool IsDefaultCommentInvisible { get; set; }

        [DataMember(Name = "accessFrom")]
        public object AccessFrom { get; set; }

        [DataMember(Name = "csrfToken")]
        public string CsrfToken { get; set; }

        [DataMember(Name = "translationVersionJsonUpdateTime")]
        public int TranslationVersionJsonUpdateTime { get; set; }

        [DataMember(Name = "userkey")]
        public string Userkey { get; set; }

        [DataMember(Name = "watchAuthKey")]
        public string WatchAuthKey { get; set; }

        [DataMember(Name = "watchTrackId")]
        public string WatchTrackId { get; set; }

        [DataMember(Name = "watchPageServerTime")]
        public long WatchPageServerTime { get; set; }

        [DataMember(Name = "isAuthenticationRequired")]
        public bool IsAuthenticationRequired { get; set; }

        [DataMember(Name = "isPeakTime")]
        public object IsPeakTime { get; set; }

        [DataMember(Name = "ngRevision")]
        public int? NgRevision { get; set; }

        [DataMember(Name = "categoryName")]
        public string CategoryName { get; set; }

        [DataMember(Name = "categoryKey")]
        public string CategoryKey { get; set; }

        [DataMember(Name = "categoryGroupName")]
        public string CategoryGroupName { get; set; }

        [DataMember(Name = "categoryGroupKey")]
        public string CategoryGroupKey { get; set; }

        [DataMember(Name = "yesterdayRank")]
        public int? YesterdayRank { get; set; }

        [DataMember(Name = "highestRank")]
        public int? HighestRank { get; set; }

        [DataMember(Name = "isMyMemory")]
        public bool IsMyMemory { get; set; }

        [DataMember(Name = "isLiked")]
        public bool IsLiked { get; set; }

        [DataMember(Name = "highestRepresentedTagRanking")]
        public HighestRepresentedTagRanking[] HighestRepresentedTagRanking { get; set; }

        [DataMember(Name = "highestGenreRanking")]
        public HighestGenreRanking HighestGenreRanking { get; set; }

        [DataMember(Name = "ownerNGList")]
        public IList<OwnerNGList> OwnerNGList { get; set; }
    }

    public partial class HighestGenreRanking
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "rank")]
        public long Rank { get; set; }

        [DataMember(Name = "genre")]
        public string Genre { get; set; }

        [DataMember(Name = "dateTime")]
        public string DateTime { get; set; }
    }

    public partial class HighestRepresentedTagRanking
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "tag")]
        public string Tag { get; set; }

        [DataMember(Name = "regularizedTag")]
        public string RegularizedTag { get; set; }

        [DataMember(Name = "rank")]
        public long Rank { get; set; }

        [DataMember(Name = "genre")]
        public string Genre { get; set; }

        [DataMember(Name = "dateTime")]
        public string DateTime { get; set; }
    }

    public partial class EasyComment
    {
        [DataMember(Name = "phrases")]
        public Phrase[] Phrases { get; set; }
    }

    public partial class Phrase
    {
        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "nicodic")]
        public Nicodic Nicodic { get; set; }
    }

    public partial class Nicodic
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

    [DataContract]
    public class TopicItem
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "thumbnailURL")]
        public string ThumbnailURL { get; set; }

        [DataMember(Name = "point")]
        public int Point { get; set; }

        [DataMember(Name = "isHigh")]
        public bool IsHigh { get; set; }

        [DataMember(Name = "elapsedTimeM")]
        public int ElapsedTimeM { get; set; }

        [DataMember(Name = "communityId")]
        public string CommunityId { get; set; }

        [DataMember(Name = "communityName")]
        public string CommunityName { get; set; }
    }

    [DataContract]
    public class LiveTopics
    {

        [DataMember(Name = "items")]
        public IList<TopicItem> Items { get; set; }
    }

    [DataContract]
    public class Ids
    {

        [DataMember(Name = "default")]
        public string Default { get; set; }

        [DataMember(Name = "nicos")]
        public object Nicos { get; set; }

        [DataMember(Name = "community")]
        public string Community { get; set; }
    }

    [DataContract]
    public class Thread
    {

        [DataMember(Name = "commentCount")]
        public int CommentCount { get; set; }

        [DataMember(Name = "hasOwnerThread")]
        public string HasOwnerThread { get; set; }

        [DataMember(Name = "mymemoryLanguage")]
        public object MymemoryLanguage { get; set; }

        [DataMember(Name = "serverUrl")]
        public string ServerUrl { get; set; }

        [DataMember(Name = "subServerUrl")]
        public string SubServerUrl { get; set; }

        [DataMember(Name = "ids")]
        public Ids Ids { get; set; }
    }

    [DataContract]
    public class SeriesVideoMetaCount
    {
        [DataMember(Name = "view")]
        public int ViewCount { get; set; }

        [DataMember(Name = "comment")]
        public int CommentCount { get; set; }

        [DataMember(Name = "mylist")]
        public int MylistCount { get; set; }
    }


    [DataContract]
    public class SeriesVideoOwner
    {
        [DataMember(Name = "ownerType")]
        public string OwnerType { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "iconUrl")]
        public string IconUrl { get; set; }
    }


    [DataContract]
    public class SeriesVideoThumbnail
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "middleUrl")]
        public string MiddleUrl { get; set; }

        [DataMember(Name = "largeUrl")]
        public string LargeUrl { get; set; }
    }


    [DataContract]
    public class SeriesVideo
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "registeredAt")]
        public DateTime RegisteredAt { get; set; }

        [DataMember(Name = "count")]
        public SeriesVideoMetaCount MetaCount { get; set; }

        [DataMember(Name = "thumbnail")]
        public SeriesVideoThumbnail Thumbnail { get; set; }

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

        [DataMember(Name = "owner")]
        public SeriesVideoOwner owner { get; set; }

    }

    [DataContract]
    public class Series
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [DataMember(Name = "createdAt")]
        public DateTime CreatedAt { get; set; }

        [DataMember(Name = "updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [DataMember(Name = "prevVideo")]
        public SeriesVideo PrevVideo { get; set; }

        [DataMember(Name = "nextVideo")]
        public SeriesVideo NextVideo { get; set; }
    }



    [DataContract]
    public class DmcWatchResponse
    {

        [DataMember(Name = "video")]
        public Video Video { get; set; }

        /*
        [DataMember(Name = "player")]
        public Player Player { get; set; }
        */

        [DataMember(Name = "commentComposite")]
        public CommentComposite CommentComposite { get; set; }

        [DataMember(Name = "thread")]
        public Thread Thread { get; set; }

        [DataMember(Name = "tags")]
        public IList<Tag> Tags { get; set; }

        [DataMember(Name = "playlist")]
        public Playlist Playlist { get; set; }

        [DataMember(Name = "owner")]
        public Owner Owner { get; set; }

        [DataMember(Name = "viewer")]
        public Viewer Viewer { get; set; }

        [DataMember(Name = "community")]
        public object Community { get; set; }

        [DataMember(Name = "channel")]
        public Channel Channel { get; set; }

        [DataMember(Name = "ad")]
        public Ad Ad { get; set; }

        [DataMember(Name = "lead")]
        public Lead Lead { get; set; }

        [DataMember(Name = "maintenance")]
        public object Maintenance { get; set; }

        [DataMember(Name = "context")]
        public Context Context { get; set; }

        [DataMember(Name = "liveTopics")]
        public LiveTopics LiveTopics { get; set; }

        [DataMember(Name = "series")]
        public Series Series { get; set; }

        [DataMember(Name = "easyComment")]
        public EasyComment EasyComment { get; set; }
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
