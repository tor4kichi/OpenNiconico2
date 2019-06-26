using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Mntone.Nico2.Videos.Ranking
{
    public enum RankingTerm
    {
        [Description("hour")]
        Hour,
        [Description("24h")]
        Day,
        [Description("week")]
        Week,
        [Description("month")]
        Month,
        [Description("total")]
        Total,
    }
}
