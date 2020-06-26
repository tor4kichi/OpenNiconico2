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
        const string UserSeriesUrlFormat = "https://www.nicovideo.jp/user/{0}/series";

        public static async Task<UserSeriesResponse> GetUserSeriesAsync(NiconicoContext context, string userId)
        {
            var html = await context.GetStringAsync(string.Format(UserSeriesUrlFormat, userId));

            const string headString = "window.initialState = ";
            var headPosition = html.IndexOf(headString);
            if (headPosition < 0) { throw new Exception(); }

            var endPosition = html.IndexOf("};", headPosition + headString.Length);

            var json = html.Substring(headPosition + headString.Length, endPosition + 1 - headPosition - headString.Length);

            return JsonSerializerExtensions.Load<UserSeriesResponse>(json);
        }






    }
}
