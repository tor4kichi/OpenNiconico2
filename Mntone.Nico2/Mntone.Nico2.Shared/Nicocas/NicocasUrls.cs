using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Nicocas
{
    public static class NicocasUrls
    {
        public static readonly string NicocasApiDomain = @"https://api.cas.nicovideo.jp/";
        public static readonly string CasApiV1Base = $@"{NicocasApiDomain}/v1/";



        public static readonly string CasApiV1SearchApiBase = $@"{CasApiV1Base}search/";

        public static readonly string CasApiV1UserSearch = $@"{CasApiV1SearchApiBase}users";

        
    }
}
