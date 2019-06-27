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
        [DataRow(RankingGenre.All, RankingTerm.Hour, 2)]
        [DataRow(RankingGenre.Anime, RankingTerm.Week, 1)]
        public async Task RankingGettingTestAsync(RankingGenre genre, RankingTerm term, int page)
        {
            var data = await NiconicoRanking.GetRankingRssAsync(genre, term:term, page: page);
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

        [DataTestMethod]
        [DataRow(RankingGenre.All, "アニメ", RankingTerm.Hour, 1)]
        public async Task RankingGettingWithTagTestAsync(RankingGenre genre, string tag, RankingTerm term, int page)
        {
            var data = await NiconicoRanking.GetRankingRssAsync(genre, tag, term, page: page);
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


        [DataTestMethod]
        [DataRow(RankingTerm.Hour, 1)]
        public async Task RankingHotTopicGettingTestAsync(RankingTerm term, int page)
        {
            var data = await NiconicoRanking.GetRankingRssAsync(RankingGenre.HotTopic, term: term, page: page);
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

        [DataTestMethod]
        [DataRow(RankingTerm.Hour, 4)]
        public async Task RankingHotTopicFailTestAsync(RankingTerm term, int page)
        {
            await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(async () =>
                await NiconicoRanking.GetRankingRssAsync(RankingGenre.HotTopic, term: term, page: page)
            );
        }



        [DataTestMethod]
        [DataRow(RankingGenre.Anime)]
        public async Task RankingGenreTagsGettingsAsync(RankingGenre genre)
        {
            var tags = await NiconicoRanking.GetGenrePickedTagAsync(genre);

            foreach (var tag in tags)
            {
                Assert.IsNotNull(tag);

                if (tag.IsDefaultTag)
                {
                    System.Diagnostics.Debug.WriteLine("Default tag: " + tag.DisplayName);
                    Assert.IsNotNull(tag.DisplayName);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"{tag.DisplayName}");
                    System.Diagnostics.Debug.WriteLine($"{tag.Tag}");
                    Assert.IsNotNull(tag.DisplayName);
                    Assert.IsNotNull(tag.Tag);
                }
            }
        }

        [DataTestMethod]
        public async Task RankingHotTopicTagsGettingsAsync()
        {
            var tags = await NiconicoRanking.GetGenrePickedTagAsync(RankingGenre.HotTopic);

            foreach (var tag in tags)
            {
                Assert.IsNotNull(tag);

                if (tag.IsDefaultTag)
                {
                    System.Diagnostics.Debug.WriteLine("Default tag: " + tag.DisplayName);
                    Assert.IsNotNull(tag.DisplayName);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"{tag.DisplayName}");
                    System.Diagnostics.Debug.WriteLine($"{tag.Tag}");
                    Assert.IsNotNull(tag.DisplayName);
                    Assert.IsNotNull(tag.Tag);
                }
            }
        }

    }
}
