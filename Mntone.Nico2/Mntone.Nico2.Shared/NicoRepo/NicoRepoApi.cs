using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.NicoRepo
{
    public sealed class NicoRepoApi
    {
        #region field

        private NiconicoContext _context;

        #endregion

        #region Urls

        public static readonly string NicoRepoApiBaseUrl = $"{NiconicoUrls.VideoApiUrlBase}nicorepo/";
        public static readonly string NicoRepoTimelineApiUrl = $"{NicoRepoApi.NicoRepoApiBaseUrl}timeline/";

        public static string MakeNicoRepoUrl_LoginUser(NicoRepoTimelineType timelineType, string lastTimelineItemId = null)
        {
            var dict = new Dictionary<string, string>();

            if (lastTimelineItemId != null)
            {
                dict.Add("cursor", lastTimelineItemId);
            }
            dict.Add("client_app", "pc_myrepo");
            var unixTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            dict.Add("_", unixTime.ToString());

            return $"{NicoRepoTimelineApiUrl}my/{timelineType.ToString()}?{HttpQueryExtention.DictionaryToQuery(dict)}";
        }

        public static string MakeNicoRepoUrl_User(uint userId, string lastTimelineItemId = null)
        {
            var dict = new Dictionary<string, string>();

            if (lastTimelineItemId != null)
            {
                dict.Add("cursor", lastTimelineItemId);
            }
            dict.Add("client_app", "pc_profilerepo");
            var unixTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            dict.Add("_", unixTime.ToString());

            return $"{NicoRepoTimelineApiUrl}user/{userId.ToString()}?{HttpQueryExtention.DictionaryToQuery(dict)}";
        }

        #endregion

        internal NicoRepoApi(NiconicoContext context)
        {
            this._context = context;
        }


        public Task<NicoRepoResponse> GetLoginUserNicoRepo(NicoRepoTimelineType timelineType, string lastNicoRepoItemId = null)
        {
            return LoginUserNicoRepo.LoginUserNicoRepoClient.GetLoginUserNicoRepoAsync(_context, timelineType, lastNicoRepoItemId);
        }

        public Task<NicoRepoResponse> GetUserNicoRepo(uint userId, string lastNicoRepoItemId = null)
        {
            return UserNicoRepo.UserNicoRepoClient.GetUserNicoRepoAsync(_context, userId, lastNicoRepoItemId);
        }

        // TODO: ユーザーニコレポのミュート一覧取得
        // TODO: ユーザーニコレポのミュート追加
        // TODO: ユーザーニコレポのミュート削除
    }
}
