using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Nicocas.Live
{
    public static class NicocasLiveClient
    {
        public static async Task<NicoCasLiveProgramResponse> GetLiveProgramAsync(NiconicoContext context, string liveId)
        {
            const string NicocasLiveUrlFormat = @"https://api.cas.nicovideo.jp/v1/services/live/programs/{0}";

            var json = await context.GetStringAsync(string.Format(NicocasLiveUrlFormat, liveId));

            var response = JsonConvert.DeserializeObject<NicoCasLiveProgramResponse>(json);

            return response;
        }
    }
}
