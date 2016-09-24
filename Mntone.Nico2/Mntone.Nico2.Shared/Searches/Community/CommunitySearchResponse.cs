using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Searches.Community
{
	public class NicoCommynity
	{
		public string Name { get; set; }
		public string Id { get; set; }
		public Uri IconUrl { get; set; }
		public string DateTime { get; set; }
		public string ShortDescription { get; set; }
		public uint Level { get; set; }
		public uint MemberCount { get; set; }
		public uint VideoCount { get; set; }
	}

    public class CommunitySearchResponse
    {
		public bool IsStatusOK { get; set; }

		public uint TotalCount { get; set; }

		public uint DataCount { get; set; }

		public List<NicoCommynity> Communities { get; set; }


		public CommunitySearchResponse()
		{
			Communities = new List<NicoCommynity>();
		}
	}
}
