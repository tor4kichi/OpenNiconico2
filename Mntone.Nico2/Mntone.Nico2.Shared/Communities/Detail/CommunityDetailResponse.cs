using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Communities.Detail
{
	public class CommunityLiveInfo
	{
		public string LiveTitle { get; set; }

		public string LiveId { get; set; }
	}

	public class CommunityNews
	{
		public string Title { get; set; }
		public string ContentHtml { get; set; }
		public DateTime PostDate { get; set; }
		public string PostAuthor { get; set; }
	}

	public class CommunityOption
	{
		/// <summary>
		/// 登録申請を自動で承認
		/// </summary>
		public bool IsJoinAutoAccept { get; set; }

		/// <summary>
		/// 登録時に個人情報公開不要
		/// </summary>
		public bool IsJoinWithoutPrivacyInfo { get; set; }

		/// <summary>
		/// 新参メンバー動画投稿可
		/// </summary>
		public bool IsCanSubmitVideoOnlyPrivilege { get; set; }

		/// <summary>
		/// 新参メンバー登録承認可
		/// </summary>
		public bool IsCanAcceptJoinOnlyPrivilege { get; set; }

		/// <summary>
		/// 特権メンバーのみ生放送可
		/// </summary>
		public bool IsCanLiveOnlyPrivilege { get; set; }
	}

	public class CommunityDetail : Searches.Community.NicoCommynity
	{
		// オーナー
		public string OwnerUserName { get; set; }
		public string OwnerUserId { get; set; }

		public uint FollowerMaxCount { get; set; }

		public uint VideoMaxCount { get; set; }

		public string ProfielHtml { get; set; }

		/// <summary>
		/// コミュニティに設定されたタグの一覧
		/// </summary>
		/// <remarks>本家ではこのタグをUIから選択した場合、コミュニティに対するタグ検索を行っている</remarks>
		public List<string> Tags { get; set; } = new List<string>();

		
		public List<CommunityNews> NewsList { get; private set; } = new List<CommunityNews>();

		public List<CommunityLiveInfo> CurrentLiveList { get; private set; } = new List<CommunityLiveInfo>();


		public List<LiveInfo> RecentLiveList { get; private set; } = new List<LiveInfo>();

		public List<LiveInfo> FutureLiveList { get; private set; } = new List<LiveInfo>();

		public List<CommunityVideo> VideoList { get; private set; } = new List<CommunityVideo>();

		public List<CommunityMember> SampleFollwers { get; private set; } = new List<CommunityMember>();

		//		public CommunityOption Option { get; private set; } = new CommunityOption();

		public string PrivilegeDescription { get; set; }
	}

	public class LiveInfo
	{
		public DateTime StartTime { get; set; }
		public string Title { get; set; }
		public string LiveId { get; set; }
		public string StreamerName { get; set; }
	}


	public class CommunityMember
	{
		public uint UserId { get; set; }
		public string Name { get; set; }
		public Uri IconUrl { get; set; }
	}

	public class CommunityVideo
	{
		public string Title { get; set; }
		public string VideoId { get; set; }
		public string ThumbnailUrl { get; set; }
	}

	

	public class CommunitySammary
	{
		public CommunityDetail CommunityDetail { get; set; }


	}

    public class CommunityDetailResponse
    {
		public CommunitySammary CommunitySammary { get; set; }

		public bool IsStatusOK { get; set; } = false;
    }
}
