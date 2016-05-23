using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2
{
    public enum ContentManageResult
    {
		Success,
		Exist,
		Failed,
    }



	public static class ContentManagerResultHelper
	{
		public static ContentManageResult ParseJsonResult(string json)
		{
			dynamic parsedJson = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

			if (parsedJson.error != null)
			{
				if (parsedJson.error.code == "EXIST")
				{
					return ContentManageResult.Exist;
				}
			}

			if (parsedJson.status != null)
			{
				if (parsedJson.status == "ok")
				{
					return ContentManageResult.Success;
				}
			}

			return ContentManageResult.Failed;
		}
	}
}
