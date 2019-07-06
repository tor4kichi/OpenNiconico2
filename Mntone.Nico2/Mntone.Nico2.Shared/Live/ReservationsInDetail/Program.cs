using System;
using System.Linq;
using System.Xml.Linq;


namespace Mntone.Nico2.Live.ReservationsInDetail
{
	/// <summary>
	/// 画像の情報を格納するクラス
	/// </summary>
	public sealed class Program
	{
		internal Program( XElement reservedItemXml )
		{
			Id = "lv" + reservedItemXml.Element( "vid" ).Value;
			Title = reservedItemXml.Element( "title" ).Value;
			Status = reservedItemXml.Element( "status" ).Value;
			IsUnwatched = reservedItemXml.Element( "unwatch" ).Value.ToBooleanFrom1();

			var expire = reservedItemXml.Element( "expire" ).Value;
			ExpiredAt = expire != "0" ? expire.ToDateTimeOffsetFromUnixTime() : DateTimeOffset.MaxValue;
		}

		/// <summary>
		/// ID
		/// </summary>
		public string Id { get; private set; }

		/// <summary>
		/// 題名
		/// </summary>
		public string Title { get; private set; }

		/// <summary>
		/// 状態
		/// </summary>
		public string Status { get; private set; }

		/// <summary>
		/// 未視聴か
		/// </summary>
		public bool IsUnwatched { get; private set; }

		/// <summary>
		/// 有効期限日時
		/// </summary>
		public DateTimeOffset ExpiredAt { get; private set; }



        public ReservationStatus? GetReservationStatus()
        {
            return Enum.TryParse(Status, out ReservationStatus result) 
                ? new ReservationStatus?(result) 
                : default(ReservationStatus?)
                ;
        }

        ReservationStatus[] OutDatedStatusList = { ReservationStatus.PRODUCT_ARCHIVE_TIMEOUT, ReservationStatus.USER_TIMESHIFT_DATE_OUT, ReservationStatus.USE_LIMIT_DATE_OUT, ReservationStatus.LIMIT_DATE_OUT };
        public bool IsOutDated
        {
            get
            {
                var status = GetReservationStatus();
                if (status != null)
                {
                    return OutDatedStatusList.Contains(status.Value);
                }
                else
                {
                    return false;
                }
            }
        }

        ReservationStatus[] WatchAvairableStatusList = { ReservationStatus.WATCH, ReservationStatus.FIRST_WATCH, ReservationStatus.PRODUCT_ARCHIVE_WATCH, ReservationStatus.TSARCHIVE };
        public bool IsCanWatch
        {
            get
            {
                var status = GetReservationStatus();
                if (status != null)
                {
                    return WatchAvairableStatusList.Contains(status.Value);
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsReserved => GetReservationStatus() == ReservationStatus.RESERVED;
    }


    public enum ReservationStatus
    {
        FIRST_WATCH,
        WATCH,
        RESERVED,
        TSARCHIVE,
        PRODUCT_ARCHIVE_WATCH,
        PRODUCT_ARCHIVE_TIMEOUT,
        USER_TIMESHIFT_DATE_OUT,
        USE_LIMIT_DATE_OUT,
        LIMIT_DATE_OUT,
    }
}