using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Mylist
{
    public sealed class MylistApi
    {
		internal MylistApi(NiconicoContext context)
		{
			this._context = context;
		}



		


		

		


		/// <summary>
		/// ユーザーのマイリストグループ一覧を取得します
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public Task<List<MylistGroupData>> GetUserMylistGroupAsync(string userId)
		{
			return UserMylist.UserMylistClient.GetUserMylistAsync(_context, userId);
		}



		


		#region field

		private NiconicoContext _context;

		#endregion
	}
}
