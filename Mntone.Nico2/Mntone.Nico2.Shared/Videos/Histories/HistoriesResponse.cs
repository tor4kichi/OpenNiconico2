using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Mntone.Nico2.Videos.Histories
{
	/// <summary>
	/// 視聴した動画の一覧を格納するクラス
	/// </summary>
	[DataContract]
	public sealed class HistoriesResponse
	{
		internal HistoriesResponse()
		{ }

        internal HistoriesResponse(string token, IEnumerable<History> histories)
        {
            Token = token;
            _Histories = histories is List<History> castedHistories ? castedHistories : histories.ToList();
        }

        [DataMember( Name = "status" )]
		private string StatusImpl
		{
			get { return string.Empty; }
			set
			{
				if( value != "ok" )
				{
					throw new Exception( "Parse Error." );
				}
			}
		}

		/// <summary>
		/// トークン
		/// </summary>
		[DataMember( Name = "token" )]
		public string Token { get; set; }

		/// <summary>
		/// 視聴した動画の一覧
		/// </summary>
		public IReadOnlyList<History> Histories
		{
			get { return this._Histories; }
		}
		private List<History> _Histories = null;

		[DataMember( Name = "history" )]
		private List<History> HistoriesImpl
		{
			get { return this._Histories ?? ( this._Histories = new List<History>() ); }
			set { this._Histories = value; }
		}
	}
}