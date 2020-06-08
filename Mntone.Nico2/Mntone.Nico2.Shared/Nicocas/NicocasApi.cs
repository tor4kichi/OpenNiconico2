using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Nicocas
{
    public sealed class NicocasApi
    {
        private readonly NiconicoContext _context;

        public NicocasApi(NiconicoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// ユーザー名からユーザーを検索します。
        /// ユーザー名は読みから曖昧に検索されることもあります。（sinと検索した時に「シン」などカタカナ表記のユーザーも検索される）
        /// </summary>
        /// <param name="searchWord">検索キーワード</param>
        /// <param name="offset">検索結果の先頭位置</param>
        /// <param name="limit">取得する検索結果の最大数（デフォルトで５件）</param>
        /// <param name="sort">並び順</param>
        /// <param name="order">昇順・降順（デフォルトは降順）</param>
        /// <returns></returns>
        public Task<Search.UserSearchResponse> SearchUsersAsync(string searchWord, int? offset = null, int? limit = null, Search.UserSearchSortOrder? sort = null, Search.UserSearchSortOrder? order = null)
        {
            return Search.NicocasUserSearchClient.SearchUsersAsync(_context, searchWord, offset, limit, sort, order);
        }




        public Task<Live.NicoCasLiveProgramResponse> GetLiveProgramAsync(string liveId)
        {
            return Live.NicocasLiveClient.GetLiveProgramAsync(_context, liveId);
        }
    }
}
