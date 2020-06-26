using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Users.Series
{
    public enum SeriesProviderType
    {
        User,
    }

    [DataContract]
    public class Owner
    {

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }


        public SeriesProviderType ProviderType => Type switch
        {
            "user" => SeriesProviderType.User,
            _ => throw new NotSupportedException(Type)
        };
    }

    [DataContract]
    public class UserSeries
    {

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "owner")]
        public Owner Owner { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "isListed")]
        public bool IsListed { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "thumbnailUrl")]
        public string ThumbnailUrl { get; set; }

        [DataMember(Name = "itemsCount")]
        public int ItemsCount { get; set; }
    }

    [DataContract]
    public class UserSeriesResponse
    {

        [DataMember(Name = "nicoVideoFrontendRootUri")]
        public string NicoVideoFrontendRootUri { get; set; }

        [DataMember(Name = "smileFrontendRootUri")]
        public string SmileFrontendRootUri { get; set; }

        [DataMember(Name = "language")]
        public string Language { get; set; }

        [DataMember(Name = "serieses")]
        public IList<UserSeries> Serieses { get; set; }
    }


}
