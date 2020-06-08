using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Live.Watch.Crescendo
{
    [DataContract]
    public class TrackingParams
    {

        [DataMember(Name = "siteId")]
        public string SiteId { get; set; }

        [DataMember(Name = "pageId")]
        public string PageId { get; set; }

        [DataMember(Name = "mode")]
        public string Mode { get; set; }

        [DataMember(Name = "programStatus")]
        public string ProgramStatus { get; set; }
    }

    [DataContract]
    public class Account
    {

        [DataMember(Name = "loginPageUrl")]
        public string LoginPageUrl { get; set; }

        [DataMember(Name = "logoutPageUrl")]
        public string LogoutPageUrl { get; set; }

        [DataMember(Name = "accountRegistrationPageUrl")]
        public string AccountRegistrationPageUrl { get; set; }

        [DataMember(Name = "premiumMemberRegistrationPageUrl")]
        public string PremiumMemberRegistrationPageUrl { get; set; }

        [DataMember(Name = "trackingParams")]
        public TrackingParams TrackingParams { get; set; }
    }

    [DataContract]
    public class App
    {

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }
    }

    [DataContract]
    public class Atsumaru
    {

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }
    }

    [DataContract]
    public class Blomaga
    {

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }
    }

    [DataContract]
    public class Channel
    {

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }

        [DataMember(Name = "forOrganizationAndCompanyPageUrl")]
        public string ForOrganizationAndCompanyPageUrl { get; set; }
    }

    [DataContract]
    public class Commons
    {

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }
    }

    [DataContract]
    public class Community
    {

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }
    }

    [DataContract]
    public class Denfaminicogamer
    {

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }
    }

    [DataContract]
    public class Dic
    {

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }
    }

    [DataContract]
    public class Help
    {

        [DataMember(Name = "liveHelpPageUrl")]
        public string LiveHelpPageUrl { get; set; }

        [DataMember(Name = "systemRequirementsPageUrl")]
        public string SystemRequirementsPageUrl { get; set; }
    }

    [DataContract]
    public class Ichiba
    {

        [DataMember(Name = "configBaseUrl")]
        public string ConfigBaseUrl { get; set; }

        [DataMember(Name = "scriptUrl")]
        public string ScriptUrl { get; set; }

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }
    }

    [DataContract]
    public class Jk
    {

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }
    }

    [DataContract]
    public class Mastodon
    {

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }
    }

    [DataContract]
    public class Matome
    {

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }
    }

    [DataContract]
    public class News
    {

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }
    }

    [DataContract]
    public class Niconare
    {

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }
    }

    [DataContract]
    public class Niconico
    {

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }
    }

    [DataContract]
    public class Point
    {

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }

        [DataMember(Name = "purchasePageUrl")]
        public string PurchasePageUrl { get; set; }
    }

    [DataContract]
    public class Seiga
    {

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }
    }

    [DataContract]
    public class Site
    {

        [DataMember(Name = "serviceListPageUrl")]
        public string ServiceListPageUrl { get; set; }

        [DataMember(Name = "salesAdvertisingPageUrl")]
        public string SalesAdvertisingPageUrl { get; set; }
    }

    [DataContract]
    public class Solid
    {

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }
    }

    [DataContract]
    public class Uad
    {

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }
    }

    [DataContract]
    public class Video
    {

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }

        [DataMember(Name = "myPageUrl")]
        public string MyPageUrl { get; set; }
    }

    [DataContract]
    public class Bbs
    {

        [DataMember(Name = "requestPageUrl")]
        public string RequestPageUrl { get; set; }
    }

    [DataContract]
    public class RightsControlProgram
    {

        [DataMember(Name = "pageUrl")]
        public string PageUrl { get; set; }
    }

    [DataContract]
    public class LicenseSearch
    {

        [DataMember(Name = "pageUrl")]
        public string PageUrl { get; set; }
    }

    [DataContract]
    public class Info
    {

        [DataMember(Name = "warnForPhishingPageUrl")]
        public string WarnForPhishingPageUrl { get; set; }

        [DataMember(Name = "smartphoneSdkPageUrl")]
        public string SmartphoneSdkPageUrl { get; set; }

        [DataMember(Name = "nintendoGuidelinePageUrl")]
        public string NintendoGuidelinePageUrl { get; set; }
    }

    [DataContract]
    public class FamilyService
    {

        [DataMember(Name = "account")]
        public Account Account { get; set; }

        [DataMember(Name = "channel")]
        public Channel Channel { get; set; }

        [DataMember(Name = "community")]
        public Community Community { get; set; }

        [DataMember(Name = "denfaminicogamer")]
        public Denfaminicogamer Denfaminicogamer { get; set; }

        [DataMember(Name = "dic")]
        public Dic Dic { get; set; }

        [DataMember(Name = "help")]
        public Help Help { get; set; }

        [DataMember(Name = "ichiba")]
        public Ichiba Ichiba { get; set; }

        [DataMember(Name = "jk")]
        public Jk Jk { get; set; }

        [DataMember(Name = "mastodon")]
        public Mastodon Mastodon { get; set; }

        [DataMember(Name = "matome")]
        public Matome Matome { get; set; }

        [DataMember(Name = "news")]
        public News News { get; set; }

        [DataMember(Name = "niconare")]
        public Niconare Niconare { get; set; }

        [DataMember(Name = "niconico")]
        public Niconico Niconico { get; set; }

        [DataMember(Name = "point")]
        public Point Point { get; set; }

        [DataMember(Name = "seiga")]
        public Seiga Seiga { get; set; }

        [DataMember(Name = "site")]
        public Site Site { get; set; }

        [DataMember(Name = "solid")]
        public Solid Solid { get; set; }

        [DataMember(Name = "uad")]
        public Uad Uad { get; set; }

        [DataMember(Name = "video")]
        public Video Video { get; set; }

        [DataMember(Name = "bbs")]
        public Bbs Bbs { get; set; }

        [DataMember(Name = "rightsControlProgram")]
        public RightsControlProgram RightsControlProgram { get; set; }

        [DataMember(Name = "licenseSearch")]
        public LicenseSearch LicenseSearch { get; set; }

        [DataMember(Name = "info")]
        public Info Info { get; set; }
    }

    [DataContract]
    public class Environments
    {

        [DataMember(Name = "runningMode")]
        public string RunningMode { get; set; }
    }

    [DataContract]
    public class Relive
    {

        [DataMember(Name = "apiBaseUrl")]
        public string ApiBaseUrl { get; set; }

        [DataMember(Name = "webSocketUrl")]
        public string WebSocketUrl { get; set; }

        [DataMember(Name = "csrfToken")]
        public string CsrfToken { get; set; }
    }

    [DataContract]
    public class Information
    {

        [DataMember(Name = "html5PlayerInformationPageUrl")]
        public string Html5PlayerInformationPageUrl { get; set; }

        [DataMember(Name = "flashPlayerInstallInformationPageUrl")]
        public string FlashPlayerInstallInformationPageUrl { get; set; }
    }

    [DataContract]
    public class Rule
    {

        [DataMember(Name = "agreementPageUrl")]
        public string AgreementPageUrl { get; set; }

        [DataMember(Name = "guidelinePageUrl")]
        public string GuidelinePageUrl { get; set; }
    }

    [DataContract]
    public class Spec
    {

        [DataMember(Name = "watchUsageAndDevicePageUrl")]
        public string WatchUsageAndDevicePageUrl { get; set; }

        [DataMember(Name = "broadcastUsageDevicePageUrl")]
        public string BroadcastUsageDevicePageUrl { get; set; }
    }

    [DataContract]
    public class Ad
    {

        [DataMember(Name = "adsApiBaseUrl")]
        public string AdsApiBaseUrl { get; set; }
    }

    [DataContract]
    public class Thumbnail
    {

        [DataMember(Name = "imageUrl")]
        public string ImageUrl { get; set; }
    }

    [DataContract]
    public class NicopediaArticle
    {

        [DataMember(Name = "pageUrl")]
        public string PageUrl { get; set; }

        [DataMember(Name = "exists")]
        public bool Exists { get; set; }
    }

    [DataContract]
    public class Supplier
    {

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "pageUrl")]
        public string PageUrl { get; set; }

        [DataMember(Name = "nicopediaArticle")]
        public NicopediaArticle NicopediaArticle { get; set; }
    }

    [DataContract]
    public class Substitute
    {
    }

    [DataContract]
    public class List
    {

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "existsNicopediaArticle")]
        public bool ExistsNicopediaArticle { get; set; }

        [DataMember(Name = "nicopediaArticlePageUrlPath")]
        public string NicopediaArticlePageUrlPath { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "isLocked")]
        public bool IsLocked { get; set; }

        [DataMember(Name = "isDeletable")]
        public bool IsDeletable { get; set; }
    }

    [DataContract]
    public class ProgrammTag
    {

        [DataMember(Name = "list")]
        public IList<List> List { get; set; }

        [DataMember(Name = "apiUrl")]
        public string ApiUrl { get; set; }

        [DataMember(Name = "registerApiUrl")]
        public string RegisterApiUrl { get; set; }

        [DataMember(Name = "deleteApiUrl")]
        public string DeleteApiUrl { get; set; }

        [DataMember(Name = "apiToken")]
        public string ApiToken { get; set; }

        [DataMember(Name = "isLocked")]
        public bool IsLocked { get; set; }
    }

    [DataContract]
    public class Links
    {

        [DataMember(Name = "feedbackPageUrl")]
        public string FeedbackPageUrl { get; set; }

        [DataMember(Name = "commentReportPageUrl")]
        public string CommentReportPageUrl { get; set; }

        [DataMember(Name = "flashPlayerWatchPageUrl")]
        public string FlashPlayerWatchPageUrl { get; set; }

        [DataMember(Name = "html5PlayerWatchPageUrl")]
        public string Html5PlayerWatchPageUrl { get; set; }

        [DataMember(Name = "contentsTreePageUrl")]
        public string ContentsTreePageUrl { get; set; }

        [DataMember(Name = "programReportPageUrl")]
        public string ProgramReportPageUrl { get; set; }

        [DataMember(Name = "tagReportPageUrl")]
        public string TagReportPageUrl { get; set; }
    }

    [DataContract]
    public class ProgramPlayerBanner
    {

        [DataMember(Name = "apiUrl")]
        public string ApiUrl { get; set; }
    }

    [DataContract]
    public class ProgramPlayer
    {

        [DataMember(Name = "embedUrl")]
        public string EmbedUrl { get; set; }

        [DataMember(Name = "banner")]
        public ProgramPlayerBanner Banner { get; set; }
    }

    [DataContract]
    public class ProgramZapping
    {

        [DataMember(Name = "listApiUrl")]
        public string ListApiUrl { get; set; }

        [DataMember(Name = "listUpdateIntervalMs")]
        public int ListUpdateIntervalMs { get; set; }
    }

    [DataContract]
    public class Report
    {

        [DataMember(Name = "imageApiUrl")]
        public string ImageApiUrl { get; set; }
    }

    [DataContract]
    public class ActionComment
    {

        [DataMember(Name = "apiUrl")]
        public string ApiUrl { get; set; }
    }

    [DataContract]
    public class Program
    {

        [DataMember(Name = "nicoliveProgramId")]
        public string NicoliveProgramId { get; set; }

        [DataMember(Name = "reliveProgramId")]
        public string ReliveProgramId { get; set; }

        [DataMember(Name = "broadcastId")]
        public string BroadcastId { get; set; }

        [DataMember(Name = "providerType")]
        public string ProviderType { get; set; }

        [DataMember(Name = "visualProviderType")]
        public string VisualProviderType { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "thumbnail")]
        public Thumbnail Thumbnail { get; set; }

        [DataMember(Name = "supplier")]
        public Supplier Supplier { get; set; }

        [DataMember(Name = "openTime")]
        public long OpenTime { get; set; }

        [DataMember(Name = "beginTime")]
        public long BeginTime { get; set; }

        [DataMember(Name = "endTime")]
        public long EndTime { get; set; }

        [DataMember(Name = "scheduledEndTime")]
        public long ScheduledEndTime { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "substitute")]
        public Substitute Substitute { get; set; }

        [DataMember(Name = "tag")]
        public ProgrammTag Tag { get; set; }

        [DataMember(Name = "links")]
        public Links Links { get; set; }

        [DataMember(Name = "player")]
        public ProgramPlayer Player { get; set; }

        [DataMember(Name = "watchPageUrl")]
        public string WatchPageUrl { get; set; }

        [DataMember(Name = "mediaServerType")]
        public string MediaServerType { get; set; }

        [DataMember(Name = "isEnabledHtml5Player")]
        public bool IsEnabledHtml5Player { get; set; }

        [DataMember(Name = "isPrivate")]
        public bool IsPrivate { get; set; }

        [DataMember(Name = "zapping")]
        public ProgramZapping Zapping { get; set; }

        [DataMember(Name = "report")]
        public Report Report { get; set; }

        [DataMember(Name = "actionComment")]
        public ActionComment ActionComment { get; set; }

        [DataMember(Name = "isFollowerOnly")]
        public bool IsFollowerOnly { get; set; }
    }

    [DataContract]
    public class Tag
    {

        [DataMember(Name = "suggestionApiUrl")]
        public string SuggestionApiUrl { get; set; }

        [DataMember(Name = "revisionCheckIntervalMs")]
        public int RevisionCheckIntervalMs { get; set; }

        [DataMember(Name = "registerHelpPageUrl")]
        public string RegisterHelpPageUrl { get; set; }

        [DataMember(Name = "userRegistrableMax")]
        public int UserRegistrableMax { get; set; }

        [DataMember(Name = "textMaxLength")]
        public int TextMaxLength { get; set; }
    }

    [DataContract]
    public class Coe
    {

        [DataMember(Name = "resourcesBaseUrl")]
        public string ResourcesBaseUrl { get; set; }
    }

    [DataContract]
    public class CommonHeader
    {

        [DataMember(Name = "siteId")]
        public string SiteId { get; set; }

        [DataMember(Name = "apiKey")]
        public string ApiKey { get; set; }

        [DataMember(Name = "apiDate")]
        public string ApiDate { get; set; }

        [DataMember(Name = "apiVersion")]
        public string ApiVersion { get; set; }

        [DataMember(Name = "jsonpUrl")]
        public string JsonpUrl { get; set; }
    }

    [DataContract]
    public class Notify
    {

        [DataMember(Name = "unreadApiUrl")]
        public string UnreadApiUrl { get; set; }

        [DataMember(Name = "contentApiUrl")]
        public string ContentApiUrl { get; set; }

        [DataMember(Name = "updateUnreadIntervalMs")]
        public int UpdateUnreadIntervalMs { get; set; }
    }

    [DataContract]
    public class Timeshift
    {

        [DataMember(Name = "reservationDetailListApiUrl")]
        public string ReservationDetailListApiUrl { get; set; }
    }

    [DataContract]
    public class NiconicoLiveEncoder
    {

        [DataMember(Name = "downloadUrl")]
        public string DownloadUrl { get; set; }
    }

    [DataContract]
    public class Broadcast
    {

        [DataMember(Name = "usageHelpPageUrl")]
        public string UsageHelpPageUrl { get; set; }

        [DataMember(Name = "stableBroadcastHelpPageUrl")]
        public string StableBroadcastHelpPageUrl { get; set; }

        [DataMember(Name = "niconicoLiveEncoder")]
        public NiconicoLiveEncoder NiconicoLiveEncoder { get; set; }
    }

    [DataContract]
    public class Enquete
    {

        [DataMember(Name = "usageHelpPageUrl")]
        public string UsageHelpPageUrl { get; set; }
    }

    [DataContract]
    public class TrialWatch
    {

        [DataMember(Name = "usageHelpPageUrl")]
        public string UsageHelpPageUrl { get; set; }
    }

    [DataContract]
    public class VideoQuote
    {

        [DataMember(Name = "usageHelpPageUrl")]
        public string UsageHelpPageUrl { get; set; }
    }

    [DataContract]
    public class SiteRoot
    {

        [DataMember(Name = "locale")]
        public string Locale { get; set; }

        [DataMember(Name = "serverTime")]
        public long ServerTime { get; set; }

        [DataMember(Name = "apiBaseUrl")]
        public string ApiBaseUrl { get; set; }

        [DataMember(Name = "staticResourceBaseUrl")]
        public string StaticResourceBaseUrl { get; set; }

        [DataMember(Name = "topPageUrl")]
        public string TopPageUrl { get; set; }

        [DataMember(Name = "editstreamPageUrl")]
        public string EditstreamPageUrl { get; set; }

        [DataMember(Name = "historyPageUrl")]
        public string HistoryPageUrl { get; set; }

        [DataMember(Name = "myPageUrl")]
        public string MyPageUrl { get; set; }

        [DataMember(Name = "rankingPageUrl")]
        public string RankingPageUrl { get; set; }

        [DataMember(Name = "searchPageUrl")]
        public string SearchPageUrl { get; set; }

//        [DataMember(Name = "familyService")]
//        public FamilyService FamilyService { get; set; }

        [DataMember(Name = "environments")]
        public Environments Environments { get; set; }

        [DataMember(Name = "relive")]
        public Relive Relive { get; set; }

//        [DataMember(Name = "information")]
//        public Information Information { get; set; }

        [DataMember(Name = "rule")]
        public Rule Rule { get; set; }

//        [DataMember(Name = "spec")]
//        public Spec Spec { get; set; }

        [DataMember(Name = "ad")]
        public Ad Ad { get; set; }

//        [DataMember(Name = "program")]
//        public Program Program { get; set; }

        [DataMember(Name = "tag")]
        public Tag Tag { get; set; }

//        [DataMember(Name = "coe")]
//        public Coe Coe { get; set; }

        [DataMember(Name = "commonHeader")]
        public CommonHeader CommonHeader { get; set; }

        [DataMember(Name = "notify")]
        public Notify Notify { get; set; }

        [DataMember(Name = "timeshift")]
        public Timeshift Timeshift { get; set; }

//        [DataMember(Name = "broadcast")]
//        public Broadcast Broadcast { get; set; }

        [DataMember(Name = "enquete")]
        public Enquete Enquete { get; set; }

        [DataMember(Name = "trialWatch")]
        public TrialWatch TrialWatch { get; set; }

        [DataMember(Name = "videoQuote")]
        public VideoQuote VideoQuote { get; set; }
    }

    [DataContract]
    public class User
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "nickname")]
        public string Nickname { get; set; }

        [DataMember(Name = "isLoggedIn")]
        public bool IsLoggedIn { get; set; }

        [DataMember(Name = "accountType")]
        public string AccountType { get; set; }

        [DataMember(Name = "isOperator")]
        public bool IsOperator { get; set; }

        [DataMember(Name = "isBroadcaster")]
        public bool IsBroadcaster { get; set; }

        [DataMember(Name = "premiumOrigin")]
        public string PremiumOrigin { get; set; }

        [DataMember(Name = "permissions")]
        public IList<object> Permissions { get; set; }
    }

    [DataContract]
    public class SocialGroup
    {

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "broadcastHistoryPageUrl")]
        public string BroadcastHistoryPageUrl { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "socialGroupPageUrl")]
        public string SocialGroupPageUrl { get; set; }

        [DataMember(Name = "thumbnailImageUrl")]
        public string ThumbnailImageUrl { get; set; }

        [DataMember(Name = "thumbnailSmallImageUrl")]
        public string ThumbnailSmallImageUrl { get; set; }

        [DataMember(Name = "level")]
        public int Level { get; set; }
    }

    [DataContract]
    public class Wall
    {
    }

    [DataContract]
    public class ProgramEventState
    {

        [DataMember(Name = "commentLocked")]
        public bool CommentLocked { get; set; }

        [DataMember(Name = "audienceCommentLayout")]
        public string AudienceCommentLayout { get; set; }
    }

    [DataContract]
    public class Player
    {

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "audienceToken")]
        public string AudienceToken { get; set; }

        [DataMember(Name = "isJumpDisabled")]
        public bool IsJumpDisabled { get; set; }

        [DataMember(Name = "disablePlayVideoAd")]
        public bool DisablePlayVideoAd { get; set; }

        [DataMember(Name = "isRestrictedCommentPost")]
        public bool IsRestrictedCommentPost { get; set; }

        [DataMember(Name = "enableClientLog")]
        public bool EnableClientLog { get; set; }

        [DataMember(Name = "programEventState")]
        public ProgramEventState ProgramEventState { get; set; }

        [DataMember(Name = "streamAllocationType")]
        public string StreamAllocationType { get; set; }
    }

    [DataContract]
    public class BtnFeedback
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class Favicon
    {

        [DataMember(Name = "ico")]
        public string Ico { get; set; }
    }

    [DataContract]
    public class FooterArrow
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class Glass
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class FacebookIcon
    {

        [DataMember(Name = "svg")]
        public string Svg { get; set; }
    }

    [DataContract]
    public class FollowCheck
    {

        [DataMember(Name = "svg")]
        public string Svg { get; set; }
    }

    [DataContract]
    public class FollowCheckWhite
    {

        [DataMember(Name = "svg")]
        public string Svg { get; set; }
    }

    [DataContract]
    public class FollowWhite
    {

        [DataMember(Name = "svg")]
        public string Svg { get; set; }
    }

    [DataContract]
    public class LineIcon
    {

        [DataMember(Name = "svg")]
        public string Svg { get; set; }
    }

    [DataContract]
    public class SharesIcon
    {

        [DataMember(Name = "svg")]
        public string Svg { get; set; }
    }

    [DataContract]
    public class TwitterIcon
    {

        [DataMember(Name = "svg")]
        public string Svg { get; set; }
    }

    [DataContract]
    public class Icon
    {

        [DataMember(Name = "facebook_icon")]
        public FacebookIcon FacebookIcon { get; set; }

        [DataMember(Name = "follow_check")]
        public FollowCheck FollowCheck { get; set; }

        [DataMember(Name = "follow_check_white")]
        public FollowCheckWhite FollowCheckWhite { get; set; }

        [DataMember(Name = "follow_white")]
        public FollowWhite FollowWhite { get; set; }

        [DataMember(Name = "line_icon")]
        public LineIcon LineIcon { get; set; }

        [DataMember(Name = "shares_icon")]
        public SharesIcon SharesIcon { get; set; }

        [DataMember(Name = "twitter_icon")]
        public TwitterIcon TwitterIcon { get; set; }
    }

    [DataContract]
    public class LineButton
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class Logo
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }

        [DataMember(Name = "svg")]
        public string Svg { get; set; }
    }

    [DataContract]
    public class NotificationBarIcon
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class PagetopArrow
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class SnsSprite
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class Base
    {

        [DataMember(Name = "btn_feedback")]
        public BtnFeedback BtnFeedback { get; set; }

        [DataMember(Name = "favicon")]
        public Favicon Favicon { get; set; }

        [DataMember(Name = "footer_arrow")]
        public FooterArrow FooterArrow { get; set; }

        [DataMember(Name = "glass")]
        public Glass Glass { get; set; }

        [DataMember(Name = "icon")]
        public Icon Icon { get; set; }

        [DataMember(Name = "line_button")]
        public LineButton LineButton { get; set; }

        [DataMember(Name = "logo")]
        public Logo Logo { get; set; }

        [DataMember(Name = "notification-bar-icon")]
        public NotificationBarIcon NotificationBarIcon { get; set; }

        [DataMember(Name = "pagetop_arrow")]
        public PagetopArrow PagetopArrow { get; set; }

        [DataMember(Name = "sns_sprite")]
        public SnsSprite SnsSprite { get; set; }
    }

    [DataContract]
    public class Arrows
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class TagIcons
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class Module
    {

        [DataMember(Name = "arrows")]
        public Arrows Arrows { get; set; }

        [DataMember(Name = "tag_icons")]
        public TagIcons TagIcons { get; set; }
    }

    [DataContract]
    public class Background
    {

        [DataMember(Name = "jpg")]
        public string Jpg { get; set; }
    }

    [DataContract]
    public class FormMeterCover
    {

        [DataMember(Name = "svg")]
        public string Svg { get; set; }
    }

    [DataContract]
    public class FormSelectArrow
    {

        [DataMember(Name = "svg")]
        public string Svg { get; set; }
    }

    [DataContract]
    public class IconVolumeMic
    {

        [DataMember(Name = "svg")]
        public string Svg { get; set; }
    }

    [DataContract]
    public class NleBnDownload
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class NleBnStarting
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class ProgramProvider
    {

        [DataMember(Name = "background")]
        public Background Background { get; set; }

        [DataMember(Name = "form_meter_cover")]
        public FormMeterCover FormMeterCover { get; set; }

        [DataMember(Name = "form_select_arrow")]
        public FormSelectArrow FormSelectArrow { get; set; }

        [DataMember(Name = "icon_volume_mic")]
        public IconVolumeMic IconVolumeMic { get; set; }

        [DataMember(Name = "nle_bn_download")]
        public NleBnDownload NleBnDownload { get; set; }

        [DataMember(Name = "nle_bn_starting")]
        public NleBnStarting NleBnStarting { get; set; }
    }

    [DataContract]
    public class CountryRestricted
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class Resizable
    {

        [DataMember(Name = "country_restricted")]
        public CountryRestricted CountryRestricted { get; set; }
    }

    [DataContract]
    public class SelectArrow
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class TagReport
    {

        [DataMember(Name = "select_arrow")]
        public SelectArrow SelectArrow { get; set; }
    }

    [DataContract]
    public class MailIcon
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class AudienceMail
    {

        [DataMember(Name = "mail_icon")]
        public MailIcon MailIcon { get; set; }
    }

    [DataContract]
    public class BnrBroadcastSettingNleDownload
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class BnrBroadcastSettingNleStartup
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class BnrPremiumPlayerSp
    {

        [DataMember(Name = "gif")]
        public string Gif { get; set; }
    }

    [DataContract]
    public class Banner
    {

        [DataMember(Name = "bnr_broadcast-setting-nle-download")]
        public BnrBroadcastSettingNleDownload BnrBroadcastSettingNleDownload { get; set; }

        [DataMember(Name = "bnr_broadcast-setting-nle-startup")]
        public BnrBroadcastSettingNleStartup BnrBroadcastSettingNleStartup { get; set; }

        [DataMember(Name = "bnr_premium_player_sp")]
        public BnrPremiumPlayerSp BnrPremiumPlayerSp { get; set; }
    }

    [DataContract]
    public class BourbonBackground
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class Bourbon
    {

        [DataMember(Name = "bourbon_background")]
        public BourbonBackground BourbonBackground { get; set; }
    }

    [DataContract]
    public class IconBan
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class CommentBan
    {

        [DataMember(Name = "icon_ban")]
        public IconBan IconBan { get; set; }
    }

    [DataContract]
    public class CreatorBtnIcon
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class Creator
    {

        [DataMember(Name = "creator_btn_icon")]
        public CreatorBtnIcon CreatorBtnIcon { get; set; }
    }

    [DataContract]
    public class BgBlomaga
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class BgBlomagaArticle
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class BgBlomagaArticleNP
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class BgBlomagaNP
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class BgSubmit
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class BpnFkdV2
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class BpnNoimageBig
    {

        [DataMember(Name = "jpg")]
        public string Jpg { get; set; }
    }

    [DataContract]
    public class BpnNoimageSml
    {

        [DataMember(Name = "jpg")]
        public string Jpg { get; set; }
    }

    [DataContract]
    public class BpnRatingBig
    {

        [DataMember(Name = "jpg")]
        public string Jpg { get; set; }
    }

    [DataContract]
    public class BpnRatingSml
    {

        [DataMember(Name = "jpg")]
        public string Jpg { get; set; }
    }

    [DataContract]
    public class BtnGoIchiba
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class IconMat
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class IconPia
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class BpnTab0
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class BpnTab1
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class BpnTabBg
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class Search
    {

        [DataMember(Name = "bpn_tab_0")]
        public BpnTab0 BpnTab0 { get; set; }

        [DataMember(Name = "bpn_tab_1")]
        public BpnTab1 BpnTab1 { get; set; }

        [DataMember(Name = "bpn_tab_bg")]
        public BpnTabBg BpnTabBg { get; set; }
    }

    [DataContract]
    public class Ichiba2
    {

        [DataMember(Name = "bgBlomaga")]
        public BgBlomaga BgBlomaga { get; set; }

        [DataMember(Name = "bgBlomagaArticle")]
        public BgBlomagaArticle BgBlomagaArticle { get; set; }

        [DataMember(Name = "bgBlomagaArticleNP")]
        public BgBlomagaArticleNP BgBlomagaArticleNP { get; set; }

        [DataMember(Name = "bgBlomagaNP")]
        public BgBlomagaNP BgBlomagaNP { get; set; }

        [DataMember(Name = "bg_submit")]
        public BgSubmit BgSubmit { get; set; }

        [DataMember(Name = "bpn_fkd_v2")]
        public BpnFkdV2 BpnFkdV2 { get; set; }

        [DataMember(Name = "bpn_noimage_big")]
        public BpnNoimageBig BpnNoimageBig { get; set; }

        [DataMember(Name = "bpn_noimage_sml")]
        public BpnNoimageSml BpnNoimageSml { get; set; }

        [DataMember(Name = "bpn_rating_big")]
        public BpnRatingBig BpnRatingBig { get; set; }

        [DataMember(Name = "bpn_rating_sml")]
        public BpnRatingSml BpnRatingSml { get; set; }

        [DataMember(Name = "btn_go_ichiba")]
        public BtnGoIchiba BtnGoIchiba { get; set; }

        [DataMember(Name = "icon_mat")]
        public IconMat IconMat { get; set; }

        [DataMember(Name = "icon_pia")]
        public IconPia IconPia { get; set; }

        [DataMember(Name = "search")]
        public Search Search { get; set; }
    }

    [DataContract]
    public class OtayoriSplite
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class Otayori
    {

        [DataMember(Name = "otayori_splite")]
        public OtayoriSplite OtayoriSplite { get; set; }
    }

    [DataContract]
    public class AdobeFlashPlayer
    {

        [DataMember(Name = "svg")]
        public string Svg { get; set; }
    }

    [DataContract]
    public class Html5
    {

        [DataMember(Name = "svg")]
        public string Svg { get; set; }
    }

    [DataContract]
    public class TvchanFillWhite
    {

        [DataMember(Name = "svg")]
        public string Svg { get; set; }
    }

    [DataContract]
    public class Svg
    {

        [DataMember(Name = "Adobe_Flash_Player")]
        public AdobeFlashPlayer AdobeFlashPlayer { get; set; }

        [DataMember(Name = "html5")]
        public Html5 Html5 { get; set; }

        [DataMember(Name = "tvchan_fill_white")]
        public TvchanFillWhite TvchanFillWhite { get; set; }
    }

    [DataContract]
    public class ProviderIconSprite
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class SpriteColor
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class Title
    {

        [DataMember(Name = "provider_icon_sprite")]
        public ProviderIconSprite ProviderIconSprite { get; set; }

        [DataMember(Name = "sprite_color")]
        public SpriteColor SpriteColor { get; set; }
    }

    [DataContract]
    public class ZappingSprite
    {

        [DataMember(Name = "png")]
        public string Png { get; set; }
    }

    [DataContract]
    public class Zapping
    {

        [DataMember(Name = "zapping_sprite")]
        public ZappingSprite ZappingSprite { get; set; }
    }

    [DataContract]
    public class Watch
    {

        [DataMember(Name = "audience_mail")]
        public AudienceMail AudienceMail { get; set; }

        [DataMember(Name = "banner")]
        public Banner Banner { get; set; }

        [DataMember(Name = "bourbon")]
        public Bourbon Bourbon { get; set; }

        [DataMember(Name = "comment_ban")]
        public CommentBan CommentBan { get; set; }

        [DataMember(Name = "creator")]
        public Creator Creator { get; set; }

        [DataMember(Name = "ichiba_2")]
        public Ichiba2 Ichiba2 { get; set; }

        [DataMember(Name = "otayori")]
        public Otayori Otayori { get; set; }

        [DataMember(Name = "svg")]
        public Svg Svg { get; set; }

        [DataMember(Name = "tag")]
        public Tag Tag { get; set; }

        [DataMember(Name = "title")]
        public Title Title { get; set; }

        [DataMember(Name = "zapping")]
        public Zapping Zapping { get; set; }
    }

    [DataContract]
    public class Common
    {

        [DataMember(Name = "base")]
        public Base Base { get; set; }

        [DataMember(Name = "module")]
        public Module Module { get; set; }

        [DataMember(Name = "program_provider")]
        public ProgramProvider ProgramProvider { get; set; }

        [DataMember(Name = "resizable")]
        public Resizable Resizable { get; set; }

        [DataMember(Name = "tag_report")]
        public TagReport TagReport { get; set; }

        [DataMember(Name = "watch")]
        public Watch Watch { get; set; }
    }

    [DataContract]
    public class Colorbars
    {

        [DataMember(Name = "svg")]
        public string Svg { get; set; }
    }

    [DataContract]
    public class Nicoex
    {

        [DataMember(Name = "colorbars")]
        public Colorbars Colorbars { get; set; }
    }

    [DataContract]
    public class Images
    {

        [DataMember(Name = "common")]
        public Common Common { get; set; }

        [DataMember(Name = "nicoex")]
        public Nicoex Nicoex { get; set; }
    }

    [DataContract]
    public class Scripts
    {

        [DataMember(Name = "pc-watch")]
        public string PcWatch { get; set; }

        [DataMember(Name = "operator-tools")]
        public string OperatorTools { get; set; }

        [DataMember(Name = "pc-watch.all")]
        public string PcWatchAll { get; set; }

        [DataMember(Name = "common")]
        public string Common { get; set; }

        [DataMember(Name = "polyfill")]
        public string Polyfill { get; set; }

        [DataMember(Name = "nicoheader")]
        public string Nicoheader { get; set; }

        [DataMember(Name = "ichiba")]
        public string Ichiba { get; set; }
    }

    [DataContract]
    public class Stylesheets
    {

        [DataMember(Name = "pc-watch.all")]
        public string PcWatchAll { get; set; }
    }

    [DataContract]
    public class Swfs
    {

        [DataMember(Name = "common")]
        public Common Common { get; set; }
    }

    [DataContract]
    public class Vendor
    {

        [DataMember(Name = "common")]
        public Common Common { get; set; }
    }

    [DataContract]
    public class Assets
    {

        [DataMember(Name = "images")]
        public Images Images { get; set; }

        [DataMember(Name = "scripts")]
        public Scripts Scripts { get; set; }

        [DataMember(Name = "stylesheets")]
        public Stylesheets Stylesheets { get; set; }

        [DataMember(Name = "swfs")]
        public Swfs Swfs { get; set; }

        [DataMember(Name = "vendor")]
        public Vendor Vendor { get; set; }
    }

    [DataContract]
    public class NicoEnquete
    {

        [DataMember(Name = "isEnabled")]
        public bool IsEnabled { get; set; }
    }

    [DataContract]
    public class CommunityFollower
    {

        [DataMember(Name = "records")]
        public IList<object> Records { get; set; }
    }

    [DataContract]
    public class CrescendoLeoProps
    {

        [DataMember(Name = "site")]
        public SiteRoot Site { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "program")]
        public Program Program { get; set; }

        [DataMember(Name = "socialGroup")]
        public SocialGroup SocialGroup { get; set; }

        [DataMember(Name = "wall")]
        public Wall Wall { get; set; }

        [DataMember(Name = "player")]
        public Player Player { get; set; }

        [DataMember(Name = "ad")]
        public Ad Ad { get; set; }

//        [DataMember(Name = "assets")]
//        public Assets Assets { get; set; }

        [DataMember(Name = "nicoEnquete")]
        public NicoEnquete NicoEnquete { get; set; }

        [DataMember(Name = "ichiba")]
        public Ichiba Ichiba { get; set; }

        [DataMember(Name = "community")]
        public Community Community { get; set; }

        [DataMember(Name = "communityFollower")]
        public CommunityFollower CommunityFollower { get; set; }
    }


}


