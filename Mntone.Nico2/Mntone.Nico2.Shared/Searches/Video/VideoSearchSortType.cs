using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Searches.Video
{
    public enum VideoSearchSortType
    {
        UserId = SearchFieldType.UserId,
        ViewCounter = SearchFieldType.ViewCounter,
        MylistCounter = SearchFieldType.MylistCounter,
        LengthSeconds = SearchFieldType.LengthSeconds,
        StartTime = SearchFieldType.StartTime,
        ThreadId = SearchFieldType.ThreadId,
        CommentCounter = SearchFieldType.CommentCounter,
        LastCommentTime = SearchFieldType.LastCommentTime,
        ChannelId = SearchFieldType.ChannelId,
    }
}
