using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;


namespace Mntone.Nico2.Live.Reservation
{

    [DataContract]
    public sealed class ReservationOverwrite
    {
        [DataMember(Name = "vid")]
        public string Vid { get; private set; }

        [DataMember(Name = "title")]
        public string Title { get; private set; }
    }

    [DataContract]
    public sealed class ReservationMeta
    {
        [DataMember(Name = "status")]
        public int Status { get; private set; }

        [DataMember(Name = "errorCode")]
        public string ErrorCode { get; private set; }

    }

    [DataContract]
    public sealed class ReservationData
    {
        [DataMember(Name = "description")]
        public string Description { get; private set; }

        [DataMember(Name = "uid")]
        public string Uid { get; private set; }

        [DataMember(Name = "vid")]
        public string Vid { get; private set; }

        [DataMember(Name = "overwrite")]
        public ReservationOverwrite Overwrite { get; private set; }
    }

    [DataContract]
    public sealed class ReservationResponse
    {
        [DataMember(Name = "meta")]
        public ReservationMeta Meta { get; private set; }

        [DataMember(Name = "data")]
        public ReservationData Data { get; private set; }



        public bool IsOK => Meta.Status == (int)System.Net.HttpStatusCode.OK;

        public bool IsReservationDeuplicated => Data.Description?.EndsWith("duplicated") ?? false;

        public bool IsReservationExpired => Data.Description?.EndsWith("expired general") ?? false;

        public bool IsCanOverwrite => Data.Description?.EndsWith("can overwrite") ?? false;



    }


}
