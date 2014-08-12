﻿using Mntone.Nico2.UserInfo;
using System;

#if WINDOWS_APP
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endif

namespace Mntone.Nico2.Test
{
	[TestClass]
	public sealed class UserInfoUnitTest
	{
		[TestMethod]
		public void UserInfo_1日本語データ()
		{
			var ret = UserInfoClient.ParseUserInfoData( TestHelper.Load( @"UserInfo/repo-ja-jp.html" ) );
			Assert.AreEqual( "ℳກ੮ວܬ୧", ret.Name );
			Assert.AreEqual( 20929324u, ret.Id );
			Assert.AreEqual( "原宿", ret.JoinedVersion );
			Assert.IsTrue( ret.IsPremium );

			Assert.AreEqual( ( ushort )0, ret.FavoriteCount );
			Assert.AreEqual( ( ushort )85, ret.StampCount );
			Assert.AreEqual( ( ushort )2, ret.NicoruCount );
			Assert.AreEqual( 0u, ret.Points );
			Assert.AreEqual( 0u, ret.CreatorScore );
		}

		[TestMethod]
		public void UserInfo_2英語データ()
		{
			var ret = UserInfoClient.ParseUserInfoData( TestHelper.Load( @"UserInfo/repo-en-us.html" ) );
			Assert.AreEqual( "ℳກ੮ວܬ୧", ret.Name );
			Assert.AreEqual( 20929324u, ret.Id );
			Assert.AreEqual( "原宿", ret.JoinedVersion );
			Assert.IsTrue( ret.IsPremium );

			Assert.AreEqual( ( ushort )0, ret.FavoriteCount );
			Assert.AreEqual( ( ushort )85, ret.StampCount );
			Assert.AreEqual( ( ushort )2, ret.NicoruCount );
			Assert.AreEqual( 0u, ret.Points );
			Assert.AreEqual( 0u, ret.CreatorScore );
		}

		[TestMethod]
		public void UserInfo_3中国語データ()
		{
			var ret = UserInfoClient.ParseUserInfoData( TestHelper.Load( @"UserInfo/repo-zh-tw.html" ) );
			Assert.AreEqual( "ℳກ੮ວܬ୧", ret.Name );
			Assert.AreEqual( 20929324u, ret.Id );
			Assert.AreEqual( "原宿", ret.JoinedVersion );
			Assert.IsTrue( ret.IsPremium );

			Assert.AreEqual( ( ushort )0, ret.FavoriteCount );
			Assert.AreEqual( ( ushort )85, ret.StampCount );
			Assert.AreEqual( ( ushort )2, ret.NicoruCount );
			Assert.AreEqual( 0u, ret.Points );
			Assert.AreEqual( 0u, ret.CreatorScore );
		}
	}
}