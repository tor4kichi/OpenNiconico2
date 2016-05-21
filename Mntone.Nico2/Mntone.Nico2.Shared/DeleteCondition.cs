using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2
{
    public enum DeleteStatus
    {
		NotDeleted = 0,
		DeleteFromUser = 1,

		DeleteFromLisenceAuther = 3,
    }


	public static class DeleteStatusExtention
	{
		public static bool IsDeleted(this DeleteStatus deleteStatus)
		{
			return DeleteStatus.NotDeleted != deleteStatus;
		}
	}
}
