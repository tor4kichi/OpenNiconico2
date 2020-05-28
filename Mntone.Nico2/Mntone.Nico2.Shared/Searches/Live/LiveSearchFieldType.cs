﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Searches.Live
{
    [Flags]
    public enum LiveSearchFieldType
    {
        None = 0x0000_0000,
        All = 0x4FFF_FFFF,

        ContentId = SearchFieldType.ContentId,
        Title = SearchFieldType.Title,
        Description = SearchFieldType.Description,
        UserId = SearchFieldType.UserId,
        ChannelId = SearchFieldType.ChannelId,
        CommunityId = SearchFieldType.CommunityId,
        ProviderType = SearchFieldType.ProviderType,
        Tags = SearchFieldType.Tags,
        CategoryTags = SearchFieldType.CategoryTags,
        ViewCounter = SearchFieldType.ViewCounter,
        CommentCounter = SearchFieldType.CommentCounter,
        OpenTime = SearchFieldType.OpenTime,
        StartTime = SearchFieldType.StartTime,
        LiveEndTime = SearchFieldType.LiveEndTime,
        TimeshiftEnabled = SearchFieldType.TimeshiftEnabled,
        ScoreTimeshiftReserved = SearchFieldType.ScoreTimeshiftReserved,
        ThumbnailUrl = SearchFieldType.ThumbnailUrl,
        CommunityText = SearchFieldType.CommunityText,
        CommunityIcon = SearchFieldType.CommunityIcon,
        MemberOnly = SearchFieldType.MemberOnly,
        LiveStatus = SearchFieldType.LiveStatus,
    }
}
