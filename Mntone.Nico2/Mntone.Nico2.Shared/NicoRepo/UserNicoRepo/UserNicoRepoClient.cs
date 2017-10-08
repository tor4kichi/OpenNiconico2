using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.NicoRepo.UserNicoRepo
{
    public static class UserNicoRepoClient
    {
        public static Task<string> GetUserNicoRepoJsonAsync(NiconicoContext context, uint userId, string lastNicoRepoItemId)
        {
            return context.GetStringAsync(
                NicoRepoApi.MakeNicoRepoUrl_User(userId, lastNicoRepoItemId)
                );
        }

        public static Task<NicoRepoResponse> GetUserNicoRepoAsync(NiconicoContext context, uint userId, string lastNicoRepoItemId)
        {
            return GetUserNicoRepoJsonAsync(context, userId, lastNicoRepoItemId)
                .ContinueWith(prevTask => NicoRepoResponse.ParseNicoRepoJson(prevTask.Result));
        }
    }
}
