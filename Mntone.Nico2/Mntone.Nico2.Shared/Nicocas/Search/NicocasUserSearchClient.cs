using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Nicocas.Search
{
    public static class NicocasUserSearchClient
    {
        static Task<string> GetUserSearchResultAsync(NiconicoContext context, string searchWord, int? offset = null, int? limit = null, UserSearchSortOrder? sort = null, UserSearchSortOrder? order = null)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add(nameof(searchWord), searchWord);
            if (offset.HasValue)
            {
                dict.Add(nameof(offset), offset.Value.ToString());
            }
            if (limit.HasValue)
            {
                dict.Add(nameof(limit), limit.Value.ToString());
            }
            if (sort.HasValue)
            {
                dict.Add(nameof(sort), sort.GetDescription());
            }
            if (order.HasValue)
            {
                dict.Add(nameof(order), order.GetDescription());
            }

            return context.GetStringAsync(NicocasUrls.CasApiV1UserSearch, dict);
        }


        public static async Task<UserSearchResponse> SearchUsersAsync(NiconicoContext context, string searchWord, int? offset = null, int? limit = null, UserSearchSortOrder? sort = null, UserSearchSortOrder? order = null)
        {
            var json = await GetUserSearchResultAsync(context, searchWord, offset, limit, sort, order);
            return JsonSerializerExtensions.Load<UserSearchResponse>(json);
        }



    }
}
