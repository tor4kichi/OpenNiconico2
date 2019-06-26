using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

using Mntone.Nico2.Videos.Ranking;
using System.Threading.Tasks;
using System.Linq;

namespace Mnttone.Nico2.UnitTest
{
    [TestClass]
    public class RankingTest
    {
        [DataTestMethod]
        [DataRow(RankingGenre.Anime)]
        public async Task RankingGettingTestAsync(RankingGenre genre)
        {
            var data = await NiconicoRanking.GetRankingRssAsync(genre);
            Assert.IsTrue(data.IsOK);

            Assert.IsTrue(data.Items.Any());
            var sampleItems = data.Items.Take(1).ToArray();
            foreach (var sampleItem in sampleItems)
            {
                var videoid = sampleItem.GetVideoId();
                Assert.IsTrue(videoid.StartsWith("sm") || videoid.StartsWith("so") || videoid.StartsWith("nm"));
                System.Diagnostics.Debug.WriteLine(videoid);
                var title = sampleItem.GetRankTrimmingTitle();
                System.Diagnostics.Debug.WriteLine(title);

                var moreData = sampleItem.GetMoreData();

                Assert.IsNotNull(moreData.Title);
                Assert.IsTrue(moreData.Length > TimeSpan.Zero);
                Assert.IsTrue(moreData.WatchCount >= 0);
                Assert.IsTrue(moreData.CommentCount >= 0);
                Assert.IsTrue(moreData.MylistCount >= 0);

            }
        }
    }
}
