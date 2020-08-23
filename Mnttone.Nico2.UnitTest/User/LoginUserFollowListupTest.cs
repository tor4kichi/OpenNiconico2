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
    public sealed class LoginUserFollowListupTest
    {
        private NiconicoContext _loginContext;

        [TestInitialize]
        public async Task Initialize()
        {
            _loginContext = new NiconicoContext(new NiconicoAuthenticationToken("tor4kichi.test@hotmail.com", "36yMhd"));
            await _loginContext.SignInAsync();
        }

        [TestMethod]
        public async Task GetFollowingTags()
        {
            var followTagsResponse = await _loginContext.User.GetFollowTagsAsync();
            foreach (var tag in followTagsResponse.Data.Tags)
            {
                Debug.WriteLine(tag.Name);
            }
        }


        [TestMethod]
        public async Task GetFollowingUsers()
        {
            var followTagsResponse = await _loginContext.User.GetFollowUsersAsync();
            foreach (var userItem in followTagsResponse.Data.Items)
            {
                Debug.WriteLine(userItem.Nickname);
            }
        }


        [TestMethod]
        public async Task GetFollowingMylists()
        {
            var followTagsResponse = await _loginContext.User.GetFollowMylistsAsync();
            foreach (var mylist in followTagsResponse.Data.Mylists)
            {
                Debug.WriteLine(mylist.Detail.Name);
            }
        }


        [TestMethod]
        public async Task GetFollowingCommunities()
        {
            var followTagsResponse = await _loginContext.User.GetFollowCommunityAsync();
            foreach (var comm in followTagsResponse.Data)
            {
                Debug.WriteLine(comm.Name);
            }
        }

        [TestMethod]
        public async Task GetFollowingChannel()
        {
            var followTagsResponse = await _loginContext.User.GetFollowChannelAsync();
            foreach (var channel in followTagsResponse.Data)
            {
                Debug.WriteLine(channel.Name);
            }
        }
    }



}
