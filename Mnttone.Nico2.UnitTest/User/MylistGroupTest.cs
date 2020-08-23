using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mntone.Nico2;
using Mntone.Nico2.Users.Mylist;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mnttone.Nico2.UnitTest.User
{
    [TestClass]
    public sealed class MylistGroupTest
    {
        private NiconicoContext _loginContext;

        [TestInitialize]
        public async Task Initialize()
        {
            _loginContext = new NiconicoContext(new NiconicoAuthenticationToken("tor4kichi.test@hotmail.com", "36yMhd"));
            await _loginContext.SignInAsync();
        }

        [TestMethod]
        public async Task GetLoginUserMylistGroup()
        {
            var mylistRes = await _loginContext.User.GetLoginUserMylistGroupsAsync();

            var items = await _loginContext.User.GetLoginUserMylistGroupItemsAsync(mylistRes.Data.Mylists.First().Id, MylistSortKey.Duration, Mntone.Nico2.Users.Mylist.MylistSortOrder.Desc);

            foreach (var item in items.Data.Mylist.Items)
            {
                Debug.WriteLine(item.Video.Title);
            }
        }

        [TestMethod]
        [DataRow(33969435)]
        public async Task GetMylistGroup(int userId)
        {
            var mylistRes = await _loginContext.User.GetMylistGroupsAsync(userId);

            var items = await _loginContext.User.GetMylistGroupItemsAsync(mylistRes.Data.Mylists.First().Id, Mntone.Nico2.Users.Mylist.MylistSortKey.Duration, Mntone.Nico2.Users.Mylist.MylistSortOrder.Desc);

            foreach (var item in items.Data.Mylist.Items)
            {
                Debug.WriteLine(item.Video.Title);
            }
        }
    }
}
