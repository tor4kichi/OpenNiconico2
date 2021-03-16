using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.NicoRepo
{
    public sealed class NicoRepoApi
    {
        
        private NiconicoContext _context;

        #region NicoRepo ver1

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


        #endregion


        const string NicorepoTimelineApiUrl = "https://public.api.nicovideo.jp/v1/timelines/nicorepo/last-1-month/my/pc/entries.json";


        public Task<NicoRepoEntriesResponse> GetLoginUserNicoRepoEntriesAsync(NicoRepoType type, NicoRepoDisplayTarget target, string untilId = null)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            if (type != NicoRepoType.All)
            {
                param.Add("object%5Btype%5D", type.ToString().ToCamelCase());

                param.Add("type", type switch
                {
                    NicoRepoType.Video => "upload",
                    NicoRepoType.Program => "onair",
                    NicoRepoType.Image => "add",
                    NicoRepoType.ComicStory => "add",
                    NicoRepoType.Article => "add",
                    NicoRepoType.Game => "add",
                    _ => throw new NotSupportedException()
                });
            }

            if (target != NicoRepoDisplayTarget.All)
            {
                param.Add("list", target switch
                {
                    NicoRepoDisplayTarget.Self => "self",
                    NicoRepoDisplayTarget.User => "followingUser",
                    NicoRepoDisplayTarget.Channel => "followingChannel",
                    NicoRepoDisplayTarget.Community => "followingCommunity",
                    NicoRepoDisplayTarget.Mylist => "followingMylist",
                    _ => throw new NotSupportedException(target.ToString()),
                });
            }

            if (untilId != null)
            {
                param.Add("untilId", untilId);
            }

            return _context.GetJsonAsAsync<NicoRepoEntriesResponse>(NicorepoTimelineApiUrl + "?" + HttpQueryExtention.DictionaryToQuery(param));
        }

        // TODO: ユーザーニコレポのミュート一覧取得
        // TODO: ユーザーニコレポのミュート追加
        // TODO: ユーザーニコレポのミュート削除
    }
}
