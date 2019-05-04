using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Nicocas
{
    [DataContract]
    public abstract class NicocasResponseBase<META, DATA>
        where META : Meta
    {
        [DataMember(Name = "meta")]
        public META Meta { get; set; }

        [DataMember(Name = "data")]
        public DATA Data { get; set; }

        public bool IsOK => ((System.Net.HttpStatusCode)Meta.Status) == System.Net.HttpStatusCode.OK;
    }

    [DataContract]
    public abstract class NicocasSearchResponseBase<DATA> : NicocasResponseBase<SearchMeta, DATA>
    {
    }

    [DataContract]
    public class SearchMeta : Meta
    {
        [DataMember(Name = "totalCount")]
        public int TotalCount { get; set; }

        [DataMember(Name = "ssId")]
        public string SsId { get; set; }
    }


    [DataContract]
    public class Meta
    {
        [DataMember(Name = "status")]
        public int Status { get; set; }
    }

}
