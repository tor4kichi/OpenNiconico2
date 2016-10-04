using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Users.Video
{
    public class UserVideoResponse
    {
		// user_id
		public uint UserId { get; set; }

		public string UserName { get; set; }

		// video items

		public List<VideoData> Items { get; set; }
    }


	public class VideoData
	{
		public string VideoId { get; set; }
		public string Title { get; set; }

		public DateTime SubmitTime { get; set; }
		public Uri ThumbnailUrl { get; set; }
		public string Description { get; set; }
		public TimeSpan Length { get; set; }
	}
}
