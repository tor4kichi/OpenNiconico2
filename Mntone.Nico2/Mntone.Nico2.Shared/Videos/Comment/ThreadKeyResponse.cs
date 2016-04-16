using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Videos.Comment
{
    internal class ThreadKeyResponse
    {
		public ThreadKeyResponse(string threadKey, string force184 = null)
		{
			ThreadKey = threadKey;
			Force184 = force184;
		}

		public string ThreadKey { get; set; }
		public string Force184 { get; set; }
	}
}
