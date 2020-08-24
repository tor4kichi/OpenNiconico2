using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mntone.Nico2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Mnttone.Nico2.UnitTest.User
{
    [TestClass]
    public class GetUserDetailTest
    {
        NiconicoContext _context;
        [TestInitialize]
        public void Initialize()
        {
            _context = new NiconicoContext();
        }

        [TestMethod]
        [DataRow(16387883)]
        [DataRow(30856997)]
        public async Task GetUserDetailAsync(int userId)
        {
            var detail = await _context.User.GetUserDetailAsync(userId.ToString());

            Debug.WriteLine(detail?.User.Nickname);
            Assert.IsTrue(detail.User != null);
        }
    }
}
