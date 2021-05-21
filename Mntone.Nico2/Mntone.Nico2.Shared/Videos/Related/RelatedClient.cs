using Mntone.Nico2.Mylist;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mntone.Nico2.Videos.Related
{
    internal sealed class RelatedClient
    {

        public static Task<NicoVideoResponse> GetRelatedVideoAsync(NiconicoContext context, string videoId, uint from, uint limit, Sort sortMethod, Order sortDir)
        {
            var dict = new Dictionary<string, string>();

            dict.Add("v", videoId);
            dict.Add(nameof(from), from.ToString());
            dict.Add(nameof(limit), limit.ToString());
            dict.Add(nameof(sortMethod), sortMethod.ToShortString());
            dict.Add(nameof(sortDir), sortDir.ToShortString());

            var query = HttpQueryExtention.DictionaryToQuery(dict);

            return context
                .GetJsonAsAsync<NicoVideoResponse>($"{NiconicoUrls.RelatedVideoApiUrl}?{query}");
        }


        public static async Task<string> GetRelatedPlaylistDataAsync(NiconicoContext context, string videoId, string referer)
        {
            var dict = new Dictionary<string, string>();

            dict.Add("watch_id", videoId);
            dict.Add("referer", referer);
            dict.Add("continuous", "");
            dict.Add("playlist_type", "");

            var query = HttpQueryExtention.DictionaryToQuery(dict);

            return await context
                .GetStringAsync($"{NiconicoUrls.VideoPlaylistApiUrl}?{query}");
        }


        public static Task<VideoPlaylistResponse> GetRelatedPlaylistAsync(NiconicoContext context, string videoId, string referer)
        {
            return GetRelatedPlaylistDataAsync(context, videoId, referer)
                .ContinueWith(prevTask => JsonSerializerExtensions.Load<VideoPlaylistResponse>(prevTask.Result));
        }



    }


    


}