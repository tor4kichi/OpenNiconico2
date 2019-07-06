using Mntone.Nico2.Mylist;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#if WINDOWS_UWP
using Windows.Foundation;
using Windows.Foundation.Metadata;
#else
#endif


namespace Mntone.Nico2.Embed
{
    public sealed class EmbedApi
    {
        private NiconicoContext _context { get; }

        internal EmbedApi(NiconicoContext context)
        {
            _context = context;
        }


        /// <summary>
        /// 動画や生放送等のニコニコ市場情報を取得します。
        /// </summary>
        /// <param name="contentId"></param>
        /// <returns></returns>
        public Task<Ichiba.IchibaResponse> GetIchiba(string contentId)
        {
            return Ichiba.IchibaClient.GetIchibaAsync(_context, contentId);
        }

    }
}
