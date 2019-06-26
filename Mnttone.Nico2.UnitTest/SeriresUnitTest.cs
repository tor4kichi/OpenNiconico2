using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mntone.Nico2;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mnttone.Nico2.UnitTest
{
    [TestClass]
    public class SeriresUnitTest
    {
        [DataTestMethod]
        [DataRow("2037")]
        [Ignore]
        public async Task GettingSeriesDetailsTestAsync(string seriesId)
        {
            var context = new NiconicoContext();
            var data = await context.User.GetSeriesDetailsAsync(seriesId);

            Assert.IsNotNull(data.Series.Id);
            Assert.IsNotNull(data.Series.Title);
            Assert.IsNotNull(data.Series.ThumbnailUrl);

            Assert.IsNotNull(data.Owner.Id);
            if (data.Videos.Any())
            {
                var first = data.Videos.First();
                Assert.IsNotNull(first.Title);
                Assert.IsNotNull(first.Id);
                Assert.AreNotEqual(first.Duration, default(TimeSpan));
                Assert.IsNotNull(first.ThumbnailUrl);
            }
        }

        [DataTestMethod]
        [DataRow("53842185", DisplayName = "User: tor4kichi")]
        public async Task GettingUserSeriesListupTestAsync(string userId)
        {
            var context = new NiconicoContext();

            var userSeriesList = await context.User.GetUserSeriesListAsync(userId);
            Assert.IsNotNull(userSeriesList.UserId);

            var series = userSeriesList.Series;
            if (!series.Any())
            {
                System.Diagnostics.Debug.WriteLine("Not contains series, userId:" + userId);
                return;
            }

            var sampleData = series.First();

            Assert.IsNotNull(sampleData.Id);
            Assert.IsNotNull(sampleData.Title);
            Assert.IsNotNull(sampleData.ThumbnailUrl);
        }
    }
}
