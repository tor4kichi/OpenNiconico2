﻿using System;
using System.Globalization;
using System.Text;

namespace Mntone.Nico2
{
	internal static class SerializeInteropExtensions
	{
		public static bool ToBooleanFrom1( this string value )
		{
			return value != null && value.Length == 1 && value[0] == '1' ? true : false;
		}

		public static bool ToBooleanFromString( this string value )
		{
			return value.Length == 4 && value == "true" ? true : false;
		}

		public static ushort ToUShort( this string value )
		{
			return ushort.Parse( value );
		}

		public static uint ToUInt( this string value )
		{
			return uint.Parse( value );
		}

		public static ulong ToULong( this string value )
		{
			return ulong.Parse( value );
		}

		public static DateTimeOffset ToDateTimeOffsetFromUnixTime( this string value )
		{
			return DateTimeOffset.FromFileTime( 10000000 * long.Parse( value ) + 116444736000000000 );
		}

		public static DateTimeOffset ToDateTimeOffsetFromIso8601( this string value )
		{
			return DateTimeOffset.Parse( value );
		}

		public static TimeSpan ToTimeSpan( this string value )
		{
			var buf = value.Split( ':' );
			if( buf.Length == 3 )
			{
				return new TimeSpan( int.Parse( buf[0] ), int.Parse( buf[1] ), int.Parse( buf[2] ) );
			}
			else if( buf.Length == 2 )
			{
				return new TimeSpan( 0, int.Parse( buf[0] ), int.Parse( buf[1] ) );
			}
			else if( buf.Length == 1 )
			{
				return new TimeSpan( 0, 0, int.Parse( buf[1] ) );
			}
			throw new ArgumentException();
		}

		public static Uri ToUri( this string value )
		{
			return new Uri( value );
		}
	}
}