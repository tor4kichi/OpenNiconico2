using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Searches.Video
{
    [Flags]
    public enum VideoSearchFieldType
    {
        ContentId = SearchFieldType.ContentId,
        Title = SearchFieldType.Title,
        Description = SearchFieldType.Description,
        UserId = SearchFieldType.UserId,
        ViewCounter = SearchFieldType.ViewCounter,
        MylistCounter = SearchFieldType.MylistCounter,
        LengthSeconds = SearchFieldType.LengthSeconds,
        ThumbnailUrl = SearchFieldType.ThumbnailUrl,
        StartTime = SearchFieldType.StartTime,
        ThreadId = SearchFieldType.ThreadId,
        CommentCounter = SearchFieldType.CommentCounter,
        LastCommentTime = SearchFieldType.LastCommentTime,
        CategoryTags = SearchFieldType.CategoryTags,
        ChannelId = SearchFieldType.ChannelId,
        Tags = SearchFieldType.Tags,
        TagsExact = SearchFieldType.TagsExact,
        LockTagsExact = SearchFieldType.LockTagsExact,
        Genre = SearchFieldType.Genre,
        GenreKeyword = SearchFieldType.GenreKeyword,
    }
}
