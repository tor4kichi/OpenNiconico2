using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
#if WINDOWS_UWP
using Windows.Web.Http;
#else 
using System.Net.Http;
#endif

namespace Mntone.Nico2.Videos.Users
{
    internal static class UserVideosClient
    {
        public static async Task<UserVideosResponse> GetUserVideoResponse(NiconicoContext context, uint userId, uint page)
        {
            var url = $"https://nvapi.nicovideo.jp/v1/users/{userId}/videos?sortKey=registeredAt&sortOrder=desc&pageSize=25&page={page + 1}";
            return await context.GetJsonAsAsync<UserVideosResponse>(url);
        }
    }
}
