using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mntone.Nico2.Users.User
{
    internal sealed class UserClient
    {
		public static Task<string> GetUserDataAsync(NiconicoContext context, string user_id)
		{
			return context.GetClient()
				.GetStringAsync($"{NiconicoUrls.UserPageUrl}?{nameof(user_id )}={user_id}");
		}



		public static User ParseUserData(string xml)
		{
			var serializer = new XmlSerializer(typeof(UserResponse));

			using(var stream  = new StringReader(xml))
			{
				return (User)serializer.Deserialize(stream);
			}
		}

		public static Task<User> GetUserAsync(NiconicoContext context, string user_id)
		{
			return GetUserDataAsync(context, user_id)
				.ContinueWith(prevTask => ParseUserData(prevTask.Result));
		}
	}
}
