using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Mylist
{
    public static class MylistCSRFTokenHelper
    {
		public static async Task<string> GetMylistToken(NiconicoContext context, string group_id)
		{
			var api = NiconicoUrls.MakeMylistCSRFTokenApiUrl(group_id);

			return await context.GetClient()
				.GetStringAsync(api)
				.ContinueWith(x => x.Result.Substring(x.Result.IndexOf("NicoAPI.token = \"") + 17, 60));
		}

		public static async Task<string> GetMylistToken(NiconicoContext context)
		{
			var api = NiconicoUrls.MylistMyPageUrl;

			return await context.GetClient()
				.GetStringAsync(api)
				.ContinueWith(x => x.Result.Substring(x.Result.IndexOf("NicoAPI.token = \"") + 17, 60));

		}
	}
}
