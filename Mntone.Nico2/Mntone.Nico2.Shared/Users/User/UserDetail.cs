using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Users.User
{
    public class UserDetail
    {
		public string UserId { get; set; }
		public string Nickname { get; set; }
		public string ThumbnailUri { get; set; }
		public bool IsPremium { get; set; }
		public Sex? Gender { get; set; }
		public string Region { get; set; }
		public string BirthDay { get; set; }
		public uint FavCount { get; set; }
		public uint StampCount { get; set; }

		public bool IsOwnerVideoPrivate { get; set; }

		/// <summary>
		/// 自己紹介コメント（HTML）
		/// </summary>
		public string Description { get; set; }


		/// <summary>
		/// 投稿動画件数
		/// </summary>
		public uint TotalVideoCount { get; set; }
    }
}
