using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2.Users.Series
{
    public class SeriesOwner
    {
        public string Id { get; set; }
        public string Nickname { get; set; }
        public string IconUrl { get; set; }
    }

    public class SeriresVideo
    {
        public Uri ThumbnailUrl { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
    }

    public class SeriesSimple
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }

    public class SeriesDetails
    {
        public Series Series { get; set; }
        public string DescriptionHTML { get; set; }
        public SeriesOwner Owner { get; set; }
        public List<SeriresVideo> Videos { get; set; }
        public List<SeriesSimple> OwnerSeries { get; set; }
    }


    public class Series
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int? Count { get; set; }
        public Uri ThumbnailUrl { get; set; }
    }


    public class SeriesList
    {
        public string UserId { get; set; }
        public List<Series> Series { get; set; }
    }
}
