using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mntone.Nico2.Mylist.MylistGroup
{
    internal sealed class MylistGroupClient
    {

		

		public static async Task<string> GetMylistGroupListDataAsync(NiconicoContext context)
		{
			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistGroupListUrl}");
		}

		public static async Task<string> GetMylistGroupDataAsync(NiconicoContext context, string group_id)
		{
			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistGroupGetUrl}?{nameof(group_id)}={group_id}");
		}

		public static async Task<string> AddMylistGroupDataAsync(NiconicoContext context, string name, string description, bool is_public, MylistDefaultSort default_sort, IconType iconType)
		{
			var token = await CSRFTokenHelper.GetToken(context);

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistGroupAddUrl}?{nameof(name)}={name}&{nameof(description)}={description}&{nameof(is_public)}={is_public.ToString1Or0()}&{nameof(default_sort)}={(uint)default_sort}&{nameof(iconType)}={(uint)iconType}&{nameof(token)}={token}");
		}


		public static async Task<string> UpdateMylistGroupDataAsync(NiconicoContext context, string group_id, string name, string description, bool is_public, MylistDefaultSort default_sort, IconType iconType)
		{
			var token = await CSRFTokenHelper.GetMylistToken(context, group_id);

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistGroupUpdateUrl}?{nameof(group_id)}={group_id}&{nameof(name)}={name}&{nameof(description)}={description}&{nameof(is_public)}={is_public.ToString1Or0()}&{nameof(default_sort)}={(uint)default_sort}&{nameof(iconType)}={(uint)iconType}&{nameof(token)}={token}");
		}


		public static async Task<string> RemoveMylistGroupDataAsync(NiconicoContext context, string group_id)
		{
			var token = await CSRFTokenHelper.GetToken(context);

			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistDeflistRemoveUrl}?{nameof(group_id)}={group_id}&{nameof(token)}={token}");
		}




		public static async Task<string> GetMylistGroupDetailDataAsync(NiconicoContext context, string group_id)
		{
			return await context.GetClient()
				.GetStringAsync($"{NiconicoUrls.MylistGroupDetailApi}?{nameof(group_id)}={group_id}");
		}


		private static MylistGroup ParseMylistGroupDetailXml(string xml)
		{
			var serializer = new XmlSerializer(typeof(MylistGroupResponse));

			MylistGroupResponse response = null;
			using (var stream = new StringReader(xml))
			{
				response = (MylistGroupResponse)serializer.Deserialize(stream);
			}

			return response.Mylistgroup;
		}







		public static Task<List<MylistGroupData>> GetMylistGroupListAsync(NiconicoContext context)
		{
			return GetMylistGroupListDataAsync(context)
				.ContinueWith(prevTask => MylistJsonSerializeHelper.ParseMylistGroupListJson(prevTask.Result));
		}

		public static Task<ContentManageResult> AddMylistGroupAsync(NiconicoContext context, string name, string description, bool is_public, MylistDefaultSort default_sort, IconType iconType)
		{
			return AddMylistGroupDataAsync(context, name, description, is_public, default_sort, iconType)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}

		public static Task<ContentManageResult> UpdateMylistGroupAsync(NiconicoContext context, string group_id, string name, string description, bool is_public, MylistDefaultSort default_sort, IconType iconType)
		{
			return UpdateMylistGroupDataAsync(context, group_id, name, description, is_public, default_sort, iconType)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}


		public static Task<ContentManageResult> RemoveMylistGroupAsync(NiconicoContext context, string group_id)
		{
			return RemoveMylistGroupDataAsync(context, group_id)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}



		public static Task<MylistGroup> GetMylistGroupDetailAsync(NiconicoContext context, string group_id)
		{
			return GetMylistGroupDetailDataAsync(context, group_id)
				.ContinueWith(prevTask => ParseMylistGroupDetailXml(prevTask.Result));
		}
	}



}
