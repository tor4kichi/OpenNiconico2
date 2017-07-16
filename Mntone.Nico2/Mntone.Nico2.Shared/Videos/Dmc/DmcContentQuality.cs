using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mntone.Nico2.Videos.Dmc
{
    // http://blog.nicovideo.jp/niconews/24412.html

    public enum DmcVideoQuality
    {
        High,   // 720p
        Midium, // 540p, 480p, 360p
        Low,    // 360p

        Mobile, // 360p 
    }

    public enum DmcAudioQuality
    {
        High,   // 192 kbps
        Midium, // 128 kbps
        Low,    //  64 kbps
    }

    public static class DmcContentQualityExtention
    {
        public static bool QualityIdSameVideoQuality(this DmcVideoQuality quality, string qualityId)
        {
            var items = qualityId.Split('_');
            var kbps = items[2];
            var qualitySuffix = items[3];
            switch (qualitySuffix)
            {
                case "720p":
                    return quality == DmcVideoQuality.High;
                case "540p":
                    return quality == DmcVideoQuality.Midium;
                case "480p":
                    return quality == DmcVideoQuality.Midium;
                case "360p":
                    return quality == DmcVideoQuality.Midium || quality == DmcVideoQuality.Low || quality == DmcVideoQuality.Mobile;
                default:
                    throw new NotSupportedException(qualityId + " のDmcサーバーの動画コンテンツはサポートしていません。");
            }
        }
    }
}
