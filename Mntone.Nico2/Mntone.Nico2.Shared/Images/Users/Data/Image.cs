using Mntone.Nico2.Images.Illusts;
using System;

#if WINDOWS_APP
using Windows.Data.Xml.Dom;
#else
using System.Xml.Linq;
#endif

namespace Mntone.Nico2.Images.Users.Data
{
	/// <summary>
	/// 画像の情報を格納するクラス
	/// </summary>
	public sealed class Image
	{
#if WINDOWS_APP
		internal Image( IXmlNode imageXml )
#else
		internal Image( XElement imageXml )
#endif
		{
			Id = "im" + imageXml.Element( "id" ).Value;
			UserId = imageXml.Element( "user_id" ).Value.ToUInt();
			Title = imageXml.Element( "title" ).Value;
			Description = imageXml.Element( "description" ).Value;
			ViewCount = imageXml.Element( "view_count" ).Value.ToUInt();
			CommentCount = imageXml.Element( "comment_count" ).Value.ToUInt();
			ClipCount = imageXml.Element( "clip_count" ).Value.ToUInt();
			LastCommentBody = imageXml.Element( "summary" ).Value;
			Genre = ( Genre )imageXml.Element( "genre" ).Value.ToInt();
#if DEBUG
			Site = ( Site )imageXml.Element( "category" ).Value.ToInt();
#endif
			ImageType = imageXml.Element( "image_type" ).Value.ToUShort();
			IllustType = imageXml.Element( "illust_type" ).Value.ToUShort();
			InspectionStatus = imageXml.Element( "inspection_status" ).Value.ToUShort();
			IsAnonymous = imageXml.Element( "anonymous_flag" ).Value.ToBooleanFrom1();
			PublicStatus = imageXml.Element( "public_status" ).Value.ToUShort();
			IsDeleted = imageXml.Element( "delete_flag" ).Value.ToBooleanFrom1();
			DeleteType = imageXml.Element( "delete_type" ).Value.ToUShort();
			//CacheTime = ( imageNode.Element( "cache_time" ) + "+09:00" ).ToDateTimeOffsetFromIso8601();
			PostedAt = ( imageXml.Element( "created" ).Value + "+09:00" ).ToDateTimeOffsetFromIso8601();
		}

		/// <summary>
		/// ID
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// ユーザー ID
		/// </summary>
		public uint UserId { get; private set; }

		/// <summary>
		/// 題名
		/// </summary>
		public string Title { get; private set; }

		/// <summary>
		/// 説明
		/// </summary>
		public string Description { get; private set; }

		/// <summary>
		/// 閲覧数
		/// </summary>
		public uint ViewCount { get; private set; }

		/// <summary>
		/// コメント数
		/// </summary>
		public uint CommentCount { get; private set; }

		/// <summary>
		/// クリップ数
		/// </summary>
		public uint ClipCount { get; private set; }

		/// <summary>
		/// 最新コメントの一部
		/// </summary>
		public string LastCommentBody { get; private set; }

		/// <summary>
		/// ジャンル
		/// </summary>
		public Genre Genre { get; private set; }

#if DEBUG
		/// <summary>
		/// サイト
		/// </summary>
		public Site Site { get; private set; }
#endif

		/// <summary>
		/// 匿名か
		/// </summary>
		public bool IsAnonymous { get; private set; }

		/// <summary>
		/// 画像の種類 (?)
		/// </summary>
		public ushort ImageType { get; private set; }

		/// <summary>
		/// イラストの種類 (?)
		/// </summary>
		public ushort IllustType { get; private set; }

		/// <summary>
		/// ?
		/// </summary>
		public ushort InspectionStatus { get; private set; }

		/// <summary>
		/// 公開状態 (?)
		/// </summary>
		public ushort PublicStatus { get; private set; }

		/// <summary>
		/// 削除されたか
		/// </summary>
		public bool IsDeleted { get; private set; }

		/// <summary>
		/// 削除タイプ
		/// </summary>
		public ushort DeleteType { get; private set; }

		///// <summary>
		///// キャッシュ時間 (?)
		///// </summary>
		//public DateTimeOffset CacheTime { get; private set; }

		/// <summary>
		/// 投稿日時
		/// </summary>
		public DateTimeOffset PostedAt { get; private set; }
	}
}