using System.Runtime.Serialization;

namespace Mntone.Nico2.Videos.WatchAPI
{
    [DataContract]
    public class TagRelatedMarquee
    {

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }
    }


}
