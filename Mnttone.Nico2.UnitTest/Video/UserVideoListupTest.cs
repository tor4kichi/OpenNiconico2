using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mntone.Nico2;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mnttone.Nico2.UnitTest.Video
{
    [TestClass]
    public sealed class UserVideoListupTest
    {
        [TestMethod]
        [DataRow(421727, DisplayName = "ビームマンP")]
        public async Task GetUserVideosTest(int userId)
        {
            var niconicoContext = new NiconicoContext();
            var videos = await niconicoContext.Video.GetUserVideosAsync((uint)userId, 0);
        }
    }
}
