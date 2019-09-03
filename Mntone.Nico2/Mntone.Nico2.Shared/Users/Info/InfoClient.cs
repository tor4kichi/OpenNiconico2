using HtmlAgilityPack;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Mntone.Nico2.Users.Info
{
	public class UserMyPageJSInfo
	{
		public string Id { get; set; }

		public int Age { get; set; }

		public bool IsPremium { get; set; }
	}

	internal sealed class InfoClient
	{
		public static Task<string> GetInfoDataAsync( NiconicoContext context )
		{
			return context.GetClient().GetConvertedStringAsync( NiconicoUrls.UserPageUrl + "/top" );
		}


        /*
         * 
          try {
            window.NicoGoogleTagManagerDataLayer = [];

            var data = {
            };

    
            data.user = (function() {
              var user = {
              };

      
                    user.user_id = parseInt('0000000', 10) || null;
              user.login_status = 'login';
              user.member_status = 'normal';
              user.birthday = '1979-01-01';
              user.sex = 'male';
              user.country = 'Japan';
              user.prefecture = '○○県';
              user.ui_area = 'jp';
              user.ui_lang = 'ja-jp';

      
      
              return user;
            })();

            window.NicoGoogleTagManagerDataLayer.push(data);
          } catch(e) {
          }

         */

        // 未ログイン時は user.login_status = 'login'; のみ設定される

        public static InfoResponse ParseInfoData( string userInfoData )
		{
			var html = new HtmlDocument();
			html.LoadHtml( userInfoData );

			var htmlHtml = html.DocumentNode.Element( "html" );
			var language = htmlHtml.GetAttributeValue( "lang", "ja-jp" );


			var head = htmlHtml.Element("head");
			var userInfoJSStartString = "user.user_id = ";
			var userInfoStartPos = head.InnerHtml.IndexOf(userInfoJSStartString);
			
			var userInfoJsonCharArray = head.InnerHtml.Skip(userInfoStartPos).TakeWhile(x => x != '}').ToArray();
			var rawText = new string(userInfoJsonCharArray);

            var splited = rawText.Trim().Split(';', '=');

            UserMyPageJSInfo info = new UserMyPageJSInfo();


            var items = new (string id, Action<UserMyPageJSInfo, string> act)[] {
                ("user.user_id", (UserMyPageJSInfo data, string val) => data.Id = new string(val.Skip("parseInt('".Length).TakeWhile(x => x != '\'').ToArray())),
                ("user.login_status", (UserMyPageJSInfo data, string val) => { }),
                ("user.member_status", (UserMyPageJSInfo data, string val) => data.IsPremium = val == "premium"),
                ("user.birthday", (UserMyPageJSInfo data, string val) => data.Age = (int)(DateTime.Now - DateTime.Parse(val)).TotalDays / 365),
                ("user.sex", (UserMyPageJSInfo data, string val) => { }),
                ("user.country", (UserMyPageJSInfo data, string val) => { }),
                ("user.prefecture", (UserMyPageJSInfo data, string val) => { }),
                ("user.ui_area", (UserMyPageJSInfo data, string val) => { }),
                ("user.ui_lang", (UserMyPageJSInfo data, string val) => { }),
            }.ToDictionary(x => x.id);

            for (int index = 0; index < splited.Length; index += 2)
            {
                var key = splited[index].Trim(' ', '\n'); ;
                var value = splited[index + 1].Trim('\'', ' ', '\n');

                if (items.TryGetValue(key, out var item))
                {
                    item.act(info, value);
                }
            }

			return new InfoResponse(htmlHtml.Element("body"), language, info);
		}

		public static Task<InfoResponse> GetInfoAsync( NiconicoContext context )
		{
			return GetInfoDataAsync( context )
				.ContinueWith( prevTask => ParseInfoData( prevTask.Result ) );
		}
	}
}