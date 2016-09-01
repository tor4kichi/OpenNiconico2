using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Mylist.UserMylist
{
    public class UserMylistResponse
    {
		public string UserId { get; set; }

		public List<MylistGroupData> MylistGroupItems { get; set; }
    }


}
