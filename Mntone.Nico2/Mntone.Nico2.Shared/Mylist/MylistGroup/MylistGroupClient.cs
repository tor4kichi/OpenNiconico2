using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mntone.Nico2.Mylist.MylistGroup
{
    internal sealed class MylistGroupClient
    {

		

		public static Task<string> GetMylistGroupListDataAsync(NiconicoContext context)
		{
			return context.PostAsync(NiconicoUrls.MylistGroupListUrl);
		}

		public static Task<string> GetMylistGroupDataAsync(NiconicoContext context, string group_id)
		{
			var dict = new Dictionary<string, string>();
			dict.Add(nameof(group_id), group_id);
			return context.PostAsync(NiconicoUrls.MylistGroupGetUrl, dict);
		}

		public static Task<string> AddMylistGroupDataAsync(NiconicoContext context, string name, string description, bool is_public, MylistDefaultSort default_sort, IconType icon_id)
		{
			var dict = new Dictionary<string, string>();

			dict.Add(nameof(name), name);
			dict.Add(nameof(description), description);
			dict.Add(nameof(is_public), is_public.ToString1Or0());
			dict.Add(nameof(default_sort), ((uint)default_sort).ToString());
			dict.Add(nameof(icon_id), ((uint)icon_id).ToString());

			return context.PostAsync(NiconicoUrls.MylistGroupAddUrl, dict);
		}


		public static Task<string> UpdateMylistGroupDataAsync(NiconicoContext context, string group_id, string name, string description, bool is_public, MylistDefaultSort default_sort, IconType icon_id)
		{
			var dict = new Dictionary<string, string>();
			dict.Add(nameof(group_id), group_id);
			dict.Add(nameof(name), name);
			dict.Add(nameof(description), description);
			dict.Add(nameof(is_public), is_public.ToString1Or0());
			dict.Add(nameof(default_sort), ((uint)default_sort).ToString());
			dict.Add(nameof(icon_id), ((uint)icon_id).ToString());

			return context.PostAsync(NiconicoUrls.MylistGroupUpdateUrl, dict);
		}


		public static Task<string> RemoveMylistGroupDataAsync(NiconicoContext context, string group_id)
		{
			var dict = new Dictionary<string, string>();
			dict.Add(nameof(group_id), group_id);

			return context.PostAsync(NiconicoUrls.MylistGroupRemoveUrl, dict);
		}


		



		public static Task<string> GetMylistGroupDetailDataAsync(NiconicoContext context, string group_id)
		{
			var dict = new Dictionary<string, string>();
			dict.Add(nameof(group_id), group_id);
			return context.GetStringAsync(NiconicoUrls.MylistGroupDetailApi, dict);
		}


		private static MylistGroupDetail ParseMylistGroupDetailXml(string xml)
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



		public static Task<MylistGroupDetail> GetMylistGroupDetailAsync(NiconicoContext context, string group_id)
		{
			return GetMylistGroupDetailDataAsync(context, group_id)
				.ContinueWith(prevTask => ParseMylistGroupDetailXml(prevTask.Result));
		}
	}



}
