using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mntone.Nico2;
using Mntone.Nico2.Searches.Live;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mnttone.Nico2.UnitTest
{
    [TestClass]
    public class LiveSearchApiTest
    {
        [DataTestMethod]
        [DataRow("Splatoon")]
        public async Task LiveSearchTestAsync(string keyword)
        {
            var context = new NiconicoContext() { AdditionalUserAgent = "OpenNiconico_Test@tor4kichi" };
            var response = await context.Search.LiveSearchAsync(keyword, 0, 10);
            Assert.IsTrue(response.IsOK);
            
        }

        [DataTestMethod]
        [DataRow("Splatoon", "ゲーム")]
        public async Task LiveSearchCategoryTagsFilterTestAsync(string keyword, string filterTagWord)
        {
            var context = new NiconicoContext() { AdditionalUserAgent = "OpenNiconico_Test@tor4kichi" };
            var response = await context.Search.LiveSearchAsync(
                keyword, 
                0, 
                10,  
                fields: LiveSearchFieldType.All,
                filterExpression:x => x.CategoryTags == filterTagWord);
            Assert.IsTrue(response.IsOK);
            Assert.IsTrue(response.Data.All(x => x.CategoryTags.Split(" ").Any(y => y == filterTagWord)));
        }

        [DataTestMethod]
        [DataRow("Splatoon", 100000)]
        public async Task LiveSearchViewCounterFilterTestAsync(string keyword, int viewCounter)
        {
            var context = new NiconicoContext() { AdditionalUserAgent = "OpenNiconico_Test@tor4kichi" };
            var response = await context.Search.LiveSearchAsync(
                keyword,
                0,
                10,
                fields: LiveSearchFieldType.All,
                filterExpression: x => x.ViewCounter >= viewCounter);
            Assert.IsTrue(response.IsOK);
            Assert.IsTrue(response.Data.All(x => x.ViewCounter >= viewCounter));
        }


        [DataTestMethod]
        [DataRow("Splatoon", 100000)]
        public async Task LiveSearchViewCounterFilterOperatorInverseTestAsync(string keyword, int viewCounter)
        {
            var context = new NiconicoContext() { AdditionalUserAgent = "OpenNiconico_Test@tor4kichi" };
            var response = await context.Search.LiveSearchAsync(
                keyword,
                0,
                10,
                fields: LiveSearchFieldType.All,
                filterExpression: x => viewCounter <= x.ViewCounter);
            Assert.IsTrue(response.IsOK);
            Assert.IsTrue(response.Data.All(x => x.ViewCounter >= viewCounter));
        }

        

        [DataTestMethod]
        [DataRow("Splatoon", SearchFilterField.LiveStatusPast)]
        [DataRow("Splatoon", SearchFilterField.LiveStatusOnAir)]
        [DataRow("Splatoon", SearchFilterField.LiveStatusReserved)]
        public async Task LiveSearchLiveStatusTestAsync(string keyword, string liveStatus)
        {
            var context = new NiconicoContext() { AdditionalUserAgent = "OpenNiconico_Test@tor4kichi" };
            var response = await context.Search.LiveSearchAsync(
                keyword,
                0,
                10,
                fields: LiveSearchFieldType.All,
                filterExpression: x => x.LiveStatus == liveStatus);
            Assert.IsTrue(response.IsOK);
            Assert.IsTrue(response.Data.All(x => x.LiveStatus == liveStatus));
        }


        [DataTestMethod]
        [DataRow("Splatoon")]
        public async Task LiveSearchSortTestAsync(string keyword)
        {
            var context = new NiconicoContext() { AdditionalUserAgent = "OpenNiconico_Test@tor4kichi" };
            var response = await context.Search.LiveSearchAsync(
                keyword,
                0,
                10,
                fields: LiveSearchFieldType.All,
                sortType: LiveSearchSortType.CommentCounter
                );
            Assert.IsTrue(response.IsOK);

            var copyList = response.Data.OrderByDescending(x => x.CommentCounter).ToList();
            Assert.IsTrue(Enumerable.SequenceEqual(response.Data, copyList));
        }

        [DataTestMethod]
        [DataRow("Splatoon", "ゲーム", SearchFilterField.ProviderTypeChannel, SearchFilterField.LiveStatusOnAir)]
        [DataRow("Splatoon", "ゲーム", SearchFilterField.ProviderTypeOfficial, SearchFilterField.LiveStatusReserved)]
        [DataRow("Splatoon", "ゲーム", SearchFilterField.ProviderTypeCommunity, SearchFilterField.LiveStatusPast)]
        public async Task LiveSearchCategoryTagsAndLiveStatusAndProviderTypeFilterTestAsync(string keyword, string filterTagWord, string providerType, string liveStatus)
        {
            var context = new NiconicoContext() { AdditionalUserAgent = "OpenNiconico_Test@tor4kichi" };
            var response = await context.Search.LiveSearchAsync(
                keyword,
                0,
                10,
                fields: LiveSearchFieldType.All,
                filterExpression: x => x.CategoryTags == filterTagWord && x.ProviderType == providerType && x.LiveStatus == liveStatus
                );
            Assert.IsTrue(response.IsOK);

            Assert.IsTrue(response.Data.All(x => x.CategoryTags.Split(" ").Any(y => y == filterTagWord)));
            Assert.IsTrue(response.Data.All(x => x.ProviderType == providerType));
            Assert.IsTrue(response.Data.All(x => x.LiveStatus == liveStatus));
        }



        [DataTestMethod]
        [DataRow("Splatoon")]
        public async Task LiveSearchStartTimeFilterTestAsync(string keyword)
        {
            // 現時点から24時間以内に始まった生放送中の放送を古いものを先頭にソートして検索
            var context = new NiconicoContext() { AdditionalUserAgent = "OpenNiconico_Test@tor4kichi" };

            var in24Hours = DateTime.Now - TimeSpan.FromHours(24);
            var response = await context.Search.LiveSearchAsync(
                keyword,
                0,
                10,
                fields: LiveSearchFieldType.All,
                sortType: LiveSearchSortType.StartTime | LiveSearchSortType.SortAcsending,
                filterExpression: x => x.StartTime > DateTime.Now - TimeSpan.FromHours(24) && x.LiveStatus == SearchFilterField.LiveStatusOnAir
                );

            Assert.IsTrue(response.IsOK);
            Assert.IsTrue(response.Data.All(x => x.StartTime >= in24Hours));
            Assert.IsTrue(response.Data.All(x => x.LiveStatus == SearchFilterField.LiveStatusOnAir));
        }

    }
}
