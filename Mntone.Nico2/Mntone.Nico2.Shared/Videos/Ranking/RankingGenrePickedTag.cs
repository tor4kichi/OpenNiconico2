﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Videos.Ranking
{
    public sealed class RankingGenrePickedTag
    {
        public string DisplayName { get; set; }
        public string Tag { get; set; }

        // Note: hot_topicでのみ先頭デフォルトタグのTagに"all"が入ってくる
        public bool IsDefaultTag => string.IsNullOrEmpty(Tag) || Tag == "all";
    }
}
