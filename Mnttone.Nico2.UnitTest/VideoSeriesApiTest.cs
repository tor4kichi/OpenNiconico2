using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mntone.Nico2;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mnttone.Nico2.UnitTest
{
    [TestClass]
    public sealed class VideoSeriesApiTest
    {
        [DataTestMethod]
        [DataRow("1594318")]
        public async Task UserSeriesTestAsync(string userId)
        {
            var context = new NiconicoContext() { AdditionalUserAgent = "OpenNiconico_Test@tor4kichi" };
            var response = await context.User.GetUserSeiresAsync(userId);
        }


        [DataTestMethod]
        [DataRow("134071")]
        public async Task SeriesVideosTestAsync(string seriesId)
        {
            var context = new NiconicoContext() { AdditionalUserAgent = "OpenNiconico_Test@tor4kichi" };
            var response = await context.Video.GetSeriesVideosAsync(seriesId);
        }

    }
}
