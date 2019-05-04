using System;
using System.Collections.Generic;
using System.Text;

#if WINDOWS_UWP
using Windows.UI;
#else
using System.Drawing;
#endif



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
#if WINDOWS_UWP
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
#else
        public static Color ToColor(this IconType iconType)
        {
            switch (iconType)
            {
                case IconType.Default:
                    return Color.LightYellow;
                case IconType.Cyan:
                    return Color.LightCyan;
                case IconType.SmokeWhite:
                    return Color.WhiteSmoke;
                case IconType.Dark:
                    return Color.DarkGray;
                case IconType.Red:
                    return Color.Red;
                case IconType.Orenge:
                    return Color.Orange;
                case IconType.Green:
                    return Color.Green;
                case IconType.SkyBlue:
                    return Color.SkyBlue;
                case IconType.Blue:
                    return Color.Blue;
                case IconType.Purple:
                    return Color.Purple;
                default:
                    throw new NotSupportedException($"not support {nameof(IconType)}.{iconType.ToString()}");
            }
        }
#endif


    }

}
