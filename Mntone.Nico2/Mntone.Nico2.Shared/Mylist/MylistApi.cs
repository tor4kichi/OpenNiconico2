using Mntone.Nico2.Mylist.MylistGroup;
using Mntone.Nico2.Searches.Video;
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


		/// <summary>
		/// マイリストグループの詳細を取得します
		/// </summary>
		/// <param name="group_id"></param>
		/// <param name="isNeedDetail"></param>
		/// <returns></returns>
		public Task<MylistGroupDetailResponse> GetMylistGroupDetailAsync(string group_id, bool isNeedDetail = true)
		{
			return MylistGroup.MylistGroupClient.GetMylistGroupDetailAsync(_context, group_id, isNeedDetail);
		}



		/// <summary>
		/// マイリストグループの動画リストを取得します
		/// </summary>
		/// <param name="group_id"></param>
		/// <param name="from"></param>
		/// <param name="limit"></param>
		/// <param name="sort"></param>
		/// <param name="order"></param>
		/// <returns></returns>
		public Task<MylistGroupVideoResponse> GetMylistGroupVideoAsync(
			string group_id
			, uint from = 0
			, uint limit = 30
			, Sort sort = Sort.FirstRetrieve
			, Order order = Order.Descending
			)
		{
			return MylistGroup.MylistGroupClient.GetMylistGroupVideoAsync(
				_context
				, group_id
				, from
				, limit
				, sort
				, order
				);
		}




		#region field

		private NiconicoContext _context;

		#endregion
	}
}
