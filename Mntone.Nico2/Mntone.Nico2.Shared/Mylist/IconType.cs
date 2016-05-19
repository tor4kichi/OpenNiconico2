using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI;

namespace Mntone.Nico2.Mylist
{
    public enum IconType
    {
		Default, 
		Cyan,
		SmokeWhite,
		Dark,
		Red,
		Orenge,
		Green,
		SkyBlue,
		Blue,
		Purple,
    }

	public static class IconTypeExtention
	{
		public static Color ToColor(this IconType iconType)
		{
			switch (iconType)
			{
				case IconType.Default:
					return Colors.LightYellow;
				case IconType.Cyan:
					return Colors.LightCyan;
				case IconType.SmokeWhite:
					return Colors.WhiteSmoke;
				case IconType.Dark:
					return Colors.DarkGray;
				case IconType.Red:
					return Colors.Red;
				case IconType.Orenge:
					return Colors.Orange;
				case IconType.Green:
					return Colors.Green;
				case IconType.SkyBlue:
					return Colors.SkyBlue;
				case IconType.Blue:
					return Colors.Blue;
				case IconType.Purple:
					return Colors.Purple;
				default:
					throw new NotSupportedException($"not support {nameof(IconType)}.{iconType.ToString()}");
			}
		}
	}

}
