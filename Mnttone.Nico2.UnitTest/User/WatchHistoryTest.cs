using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mntone.Nico2;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mnttone.Nico2.UnitTest.User
{
    [TestClass]
    public sealed class WatchHistoryTest
    {
        NiconicoContext _loginContext;
        [TestInitialize]
        public async Task Init()
        {
            _loginContext = new NiconicoContext(new NiconicoAuthenticationToken("tor4kichi.test@hotmail.com", "36yMhd"));
            await _loginContext.SignInAsync();
        }

        [TestMethod]
        public async Task GetUserHistory()
        {
            var histories = await _loginContext.Video.GetHistoriesAsync();
        }
    }
}
