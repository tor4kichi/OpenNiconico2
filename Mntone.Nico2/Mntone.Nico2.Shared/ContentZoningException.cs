using System;
using System.Collections.Generic;
using System.Text;

namespace Mntone.Nico2
{
	// Note: ニコニコサービスにログインしているアカウントによってはコンテンツアクセスに年齢確認が必要とされます
	// 視聴を続けるには http://www.nicovideo.jp/watch/sm29303572?watch_harmful=1 のようにアクセスします。
	// 次回から年齢確認をしない場合にはwatch_harmful=2 のようにアクセスします。

    public class ContentZoningException : Exception
    {
		public ContentZoningException() { }

		public ContentZoningException(string message)
			: base(message)
		{ }

		public ContentZoningException(string message, Exception exception)
			: base(message, exception)
		{ }
	}

}
