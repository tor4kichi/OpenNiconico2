using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Users.Series
{
    public class UserSeriesList
    {
        public string UserId { get; set; }
        public List<Videos.Series.Series> Series { get; set; }
    }
}
