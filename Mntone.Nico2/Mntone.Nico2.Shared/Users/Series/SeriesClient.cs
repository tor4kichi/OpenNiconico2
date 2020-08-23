using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Users.Series
{
    public static class SeriesClient
    {
        public static Task<UserSeriesResponse> GetUserSeriesAsync(NiconicoContext context, string userId, uint page, uint pageSize = 25)
        {
            return context.GetJsonAsAsync<UserSeriesResponse>($"https://nvapi.nicovideo.jp/v1/users/{userId}/series?page={page+1}&pageSize={pageSize}");
        }






    }
}
