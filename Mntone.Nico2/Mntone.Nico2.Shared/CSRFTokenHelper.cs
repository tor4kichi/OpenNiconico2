using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2
{
    public static class CSRFTokenHelper
    {
		public static async Task<string> GetMylistToken(this NiconicoContext context, string group_id)
		{
			var api = NiconicoUrls.MakeMylistCSRFTokenApiUrl(group_id);

			return await context.GetClient()
				.GetStringAsync(api)
				.ContinueWith(x => x.Result.Substring(x.Result.IndexOf("NicoAPI.token = \"") + 17, 60));
		}

		public static async Task<string> GetToken(this NiconicoContext context)
		{
			var api = NiconicoUrls.MylistMyPageUrl;

			return await context.GetClient()
				.GetStringAsync(api)
				.ContinueWith(x => x.Result.Substring(x.Result.IndexOf("NicoAPI.token = \"") + 17, 60));

		}
	}
}
