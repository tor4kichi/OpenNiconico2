using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Searches
{
	[Flags]
	public enum SearchTargetType
	{
		Title = 0x01,
		Description = 0x02,
		Tags = 0x04,

		All = Title | Description | Tags
	}




}
