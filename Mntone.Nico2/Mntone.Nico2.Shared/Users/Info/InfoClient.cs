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
			return context.GetConvertedStringAsync( NiconicoUrls.UserPageUrl + "/top" );
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

            for (int index = 0; index < splited.Length; index += 2)
            {
                var key = splited[index].Trim(' ', '\n'); ;
                var value = splited[index + 1].Trim('\'', ' ', '\n');

                if (key == "user.user_id")
                {
                    info.Id = new string(value.Skip("parseInt('".Length).TakeWhile(x => x != '\'').ToArray());
                }
                else if (key == "user.member_status")
                {
                    info.IsPremium = value == "premium";
                }
                else if (key == "user.birthday")
                {
                    info.Age = (int)(DateTime.Now - DateTime.Parse(value)).TotalDays / 365;
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