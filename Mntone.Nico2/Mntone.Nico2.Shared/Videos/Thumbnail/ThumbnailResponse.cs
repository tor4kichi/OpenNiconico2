using System;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Videos.Thumbnail
{
	/// <summary>
	/// サムネール情報
	/// </summary>
	public sealed class ThumbnailResponse
	{
#if WINDOWS_APP
		internal ThumbnailResponse( IXmlNode thumbXml )
#else
		internal ThumbnailResponse( XElement thumbXml )
#endif
		{
			Id = thumbXml.Element( "video_id" ).Value;
			Title = thumbXml.Element( "title" ).Value;
            Description = thumbXml.Element( "description" ).Value;
			ThumbnailUrl = thumbXml.Element( "thumbnail_url" ).Value.ToUri();
			PostedAt = thumbXml.Element( "first_retrieve" ).Value.ToDateTimeOffsetFromIso8601();
			Length = thumbXml.Element( "length" ).Value.ToTimeSpan();
			MovieType = thumbXml.Element( "movie_type" ).Value.ToMovieType();
			SizeHigh = thumbXml.Element( "size_high" ).Value.ToULong();
			SizeLow = thumbXml.Element( "size_low" ).Value.ToULong();
			ViewCount = thumbXml.Element( "view_counter" ).Value.ToUInt();
			CommentCount = thumbXml.Element( "comment_num" ).Value.ToUInt();
			MylistCount = thumbXml.Element( "mylist_counter" ).Value.ToUInt();
			LastCommentBody = thumbXml.Element( "last_res_body" ).Value;
			PageUrl = thumbXml.Element( "watch_url" ).Value.ToUri();
			ThumbnailType = thumbXml.Element( "thumb_type" ).Value.ToThumbnailType();
			IsEmbeddable = thumbXml.Element( "embeddable" ).Value.ToBooleanFrom1();
			CannotPlayInLive = thumbXml.Element( "no_live_play" ).Value.ToBooleanFrom1();

			Tags = new ThumbnailTags( thumbXml.Element( "tags" ) );

			var userIdXml = thumbXml.Element( "user_id" );
			if( userIdXml != null )
			{
				UserType = UserType.User;
				UserId = userIdXml.Value.ToUInt();
				UserName = thumbXml.Element( "user_nickname" ).Value;
				UserIconUrl = thumbXml.Element( "user_icon_url" ).Value.ToUri();
				return;
			}

			var chIdXml = thumbXml.Element( "ch_id" );
			if( chIdXml != null )
			{
				UserType = UserType.Channel;
				UserId = chIdXml.Value.ToUInt();
				UserName = thumbXml.Element( "ch_name" ).Value;
				UserIconUrl = thumbXml.Element( "ch_icon_url" ).Value.ToUri();
				return;
			}

			throw new ArgumentException();
		}

		/// <summary>
		/// ID
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// 題名
		/// </summary>
		public string Title { get; private set; }

		/// <summary>
		/// 説明
		/// </summary>
		public string Description { get; private set; }

		/// <summary>
		/// サムネールの URL
		/// </summary>
		public Uri ThumbnailUrl { get; private set; }

		/// <summary>
		/// 投稿日時
		/// </summary>
		public DateTimeOffset PostedAt { get; private set; }

		/// <summary>
		/// 長さ
		/// </summary>
		public TimeSpan Length { get; private set; }

		/// <summary>
		/// 動画の種類
		/// </summary>
		public MovieType MovieType { get; private set; }

		/// <summary>
		/// 高画質動画のサイズ
		/// </summary>
		public ulong SizeHigh { get; private set; }

		/// <summary>
		/// 低画質動画のサイズ
		/// </summary>
		public ulong SizeLow { get; private set; }

		/// <summary>
		/// 閲覧数
		/// </summary>
		public uint ViewCount { get; private set; }

		/// <summary>
		/// コメント数
		/// </summary>
		public uint CommentCount { get; private set; }

		/// <summary>
		/// マイリスト数
		/// </summary>
		public uint MylistCount { get; private set; }

		/// <summary>
		/// 最新コメントの一部
		/// </summary>
		public string LastCommentBody { get; private set; }

		/// <summary>
		/// 視聴ページ URL
		/// </summary>
		public Uri PageUrl { get; private set; }

		/// <summary>
		/// サムネール情報の種類
		/// </summary>
		public ThumbnailType ThumbnailType { get; private set; }

		/// <summary>
		/// 埋め込みが可能か
		/// </summary>
		public bool IsEmbeddable { get; private set; }

		/// <summary>
		/// 生放送で再生不可能か
		/// </summary>
		public bool CannotPlayInLive { get; private set; }

		/// <summary>
		/// タグ情報
		/// </summary>
		public ThumbnailTags Tags { get; private set; }

		/// <summary>
		/// ユーザーの種類
		/// </summary>
		public UserType UserType { get; private set; }

		/// <summary>
		/// ユーザー ID
		/// </summary>
		public uint UserId { get; private set; }

		/// <summary>
		/// ユーザー名
		/// </summary>
		public string UserName { get; private set; }

		/// <summary>
		/// ユーザー アイコン URL
		/// </summary>
		public Uri UserIconUrl { get; private set; }
	}
}