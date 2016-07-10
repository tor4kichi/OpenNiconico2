using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2
{
    public enum ContentManageResult
    {
		Success,
		Exist,
		Failed,
    }

	[DataContract]
	public class ContentManageResultError
	{
		public string code { get; set; }
	}

	[DataContract]
	public class ContentManageResultData
	{
		[DataMember]
		public ContentManageResultError error { get; set; }

		[DataMember]
		public string status { get; set; }

		[DataMember]
		public string delete_count { get; set; }
	}


	public static class ContentManagerResultHelper
	{
		public static ContentManageResult ParseJsonResult(string json)
		{
			try
			{
				var result = Newtonsoft.Json.JsonConvert.DeserializeObject<ContentManageResultData>(json);

				if (result.status == "ok")
				{
					return ContentManageResult.Success;
				}

				if (result.error?.code == "EXIST")
				{
					return ContentManageResult.Exist;
				}
			}
			catch { }	

			return ContentManageResult.Failed;
		}
	}
}
