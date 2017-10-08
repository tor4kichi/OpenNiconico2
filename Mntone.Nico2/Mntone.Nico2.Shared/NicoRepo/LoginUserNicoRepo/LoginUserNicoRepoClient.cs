using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.NicoRepo.LoginUserNicoRepo
{

    public static class LoginUserNicoRepoClient
    {
        public static Task<string> GetLoginUserNicoRepoJsonAsync(NiconicoContext context, NicoRepoTimelineType timelineType, string lastNicoRepoItemId)
        {
            return context.GetStringAsync(
                NicoRepoApi.MakeNicoRepoUrl_LoginUser(timelineType, lastNicoRepoItemId)
                );
        }

        public static Task<NicoRepoResponse> GetLoginUserNicoRepoAsync(NiconicoContext context, NicoRepoTimelineType timelineType, string lastNicoRepoItemId)
        {
            return GetLoginUserNicoRepoJsonAsync(context, timelineType, lastNicoRepoItemId)
                .ContinueWith(prevTask => NicoRepoResponse.ParseNicoRepoJson(prevTask.Result));
        }
    }
}
