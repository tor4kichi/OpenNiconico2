using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Web.Http;



namespace Mntone.Nico2.Users.Follow
{
	// お気に入りAPIのクライアント


    internal sealed class FollowClient
    {
		public static Task<string> GetFavUsersDataAsync(NiconicoContext context)
		{
			return context.GetClient()
				.GetStringAsync($"{NiconicoUrls.UserFavListApiUrl}");
		}




		public static Task<string> ExistFollowDataAsync(NiconicoContext context, NiconicoItemType item_type, string item_id)
		{
			return context.GetClient()
				.GetStringAsync($"{NiconicoUrls.UserFavExistApiUrl}?{nameof(item_type)}={(uint)item_type}&{nameof(item_id)}={item_id}");
		}

		public static Task<string> AddFollowDataAsync(NiconicoContext context, NiconicoItemType item_type, string item_id)
		{
			var formData = new Dictionary<string, string>
			{
				{ nameof(item_type), ((uint)item_type).ToString() },
				{ nameof(item_id), item_id },
			};

			return context.PostAsync(NiconicoUrls.UserFavAddApiUrl, formData);
		}

		
		public static Task<string> RemoveFollowDataAsync(NiconicoContext context, NiconicoItemType itemType, string item_id)
		{
			var key = NiconicoQueryHelper.Make_idlist_QueryKeyString(itemType);
			var val = NiconicoQueryHelper.RemoveIdPrefix(item_id);
			var formData = new Dictionary<string, string>
			{
				{ key, val },
			};


			return context.PostAsync(NiconicoUrls.UserFavRemoveApiUrl, formData);
		}



		public static Task<string> GetFollowTagsDataAsync(NiconicoContext context)
		{
			var formData = new Dictionary<string, string>
			{
				{ "cache", "false" },
				{ "dataType", "json"}
			};

			return context.PostAsync(NiconicoUrls.UserFavTagListUrl, formData, withToken:false);
		}

		public static Task<string> AddFollowTagDataAsync(NiconicoContext context, string tag)
		{
			var formData = new Dictionary<string, string>
			{
				{ nameof(tag), TagStringHelper.ToEnsureHankakuNumberTagString(tag) },
			};

			return context.PostAsync(NiconicoUrls.UserFavTagAddUrl, formData);
		}


		public static Task<string> RemoveFollowTagDataAsync(NiconicoContext context, string tag)
		{
			var formData = new Dictionary<string, string>
			{
				{ nameof(tag), TagStringHelper.ToEnsureHankakuNumberTagString(tag) },
			};

			return context.PostAsync(NiconicoUrls.UserFavTagRemoveUrl, formData);
		}



		public static Task<string> GetFollowMylistDataAsync(NiconicoContext context)
		{
			return context.GetStringAsync(NiconicoUrls.UserFavMylistPageUrl);
		}







		public static List<FollowData> ParseWatchItemFollowData(string json)
		{
			var response = JsonSerializerExtensions.Load<WatchItemResponse>(json);

			if (response.status == "ok")
			{
				return response.watchitem.Select(x => new FollowData()
				{
					ItemType = NiconicoItemType.User,
					ItemId = x.item_id,
					Title = x.item_data.nickname
				})
				.ToList();
			}
			else
			{
				return new List<FollowData>();
			}
		}


		public static List<FollowData> ParseFollowPageHtml(string html, NiconicoItemType item_type)
		{
			var doc = new HtmlDocument();
			doc.LoadHtml(html);

			var contentNode = doc.DocumentNode.Descendants("div").Single(x =>
			{
				return x.Attributes.Contains("class") && x.Attributes["class"].Value == "content";
			});
			var articleBodyNode = contentNode.GetElementByClassName("articleBody");
			var outerNode = articleBodyNode.GetElementsByClassName("outer");


			return outerNode.Select(x => 
				{
					var favData = new FollowData();
					var h5 = x.ChildNodes["h5"];
					var a = h5.ChildNodes["a"];
					var href = a.GetAttributeValue("href", "");
					favData.ItemId = href.Split('/').Last();
					favData.Title = a.InnerText;
					favData.ItemType = item_type;
					return favData;
				})
				.ToList();

		}


		public static List<string> ParseFollowTagJson(string json)
		{
			var response = JsonSerializerExtensions.Load<FollowTagResponse>(json);

			if (response.status == "ok")
			{
				return response.favtag_items.Select(x => TagStringHelper.ToEnsureHankakuNumberTagString(x.tag))
				.ToList();
			}
			else
			{
				return new List<string>();
			}
		}



		public static Task<List<FollowData>> GetFollowUsersAsync(NiconicoContext context)
		{
			return GetFavUsersDataAsync(context)
				.ContinueWith(prevTask => ParseWatchItemFollowData(prevTask.Result));
		}



		public static Task<List<string>> GetFollowTagsAsync(NiconicoContext context)
		{
			return GetFollowTagsDataAsync(context)
				.ContinueWith(prevTask => ParseFollowTagJson(prevTask.Result));
		}


		public static Task<List<FollowData>> GetFollowMylistAsync(NiconicoContext context)
		{
			return GetFollowMylistDataAsync(context)
				.ContinueWith(prevTask => ParseFollowPageHtml(prevTask.Result, NiconicoItemType.Mylist));
		}




		public static Task<ContentManageResult> ExistFollowAsync(NiconicoContext context, NiconicoItemType item_type, string item_id)
		{
			return ExistFollowDataAsync(context, item_type, item_id)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}

		public static Task<ContentManageResult> AddFollowAsync(NiconicoContext context, NiconicoItemType item_type, string item_id)
		{
			return AddFollowDataAsync(context, item_type, item_id)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}


		public static Task<ContentManageResult> RemoveFollowAsync(NiconicoContext context, NiconicoItemType item_type, string item_id)
		{
			return RemoveFollowDataAsync(context, item_type, item_id)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}




		public static Task<ContentManageResult> AddFollowTagAsync(NiconicoContext context, string tag)
		{
			return AddFollowTagDataAsync(context, tag)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}


		/// <param name="context"></param>
		/// <param name="tag"></param>
		/// <returns></returns>
		/// <remarks>Tagの文字列に全角の数字が含まれる場合は、すべて半角に変換して扱う必要があります。</remarks>
		public static Task<ContentManageResult> RemoveFollowTagAsync(NiconicoContext context, string tag)
		{
			return RemoveFollowTagDataAsync(context, tag)
				.ContinueWith(prevTask => ContentManagerResultHelper.ParseJsonResult(prevTask.Result));
		}

		
	}
}
