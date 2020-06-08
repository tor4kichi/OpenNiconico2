using Mntone.Nico2.Live.Watch.Crescendo;
using Mntone.Nico2.Nicocas;
using Mntone.Nico2.NicoRepo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Searches.Live
{
	/// <summary>
	/// niconicoコンテンツ検索APIの生放送検索
	/// </summary>
	/// <see cref="https://site.nicovideo.jp/search-api-docs/search.html"/>
	public sealed class LiveSearchClient
    {
		public static Task<LiveSearchResponse> GetLiveSearchAsync(
			NiconicoContext context,
			string q,
			int offset,
			int limit,
			SearchTargetType targets = SearchTargetType.All,
			LiveSearchFieldType fields = LiveSearchFieldType.ContentId,
			LiveSearchSortType sortType = LiveSearchSortType.StartTime | LiveSearchSortType.SortDecsending,
			Expression<Func<SearchFilterField, bool>> filterExpression = null
			)
		{
			return GetLiveSearchAsync(
				context, 
				q, 
				offset, 
				limit, 
				targets, 
				fields, 
				sortType,
				searchFilter: filterExpression != null ? new ExpressionSearchFilter(filterExpression) : default(ISearchFilter)
				);
		}


		public static async Task<LiveSearchResponse> GetLiveSearchAsync(
			NiconicoContext context,
			string q,
			int offset,
			int limit,
			SearchTargetType targets = SearchTargetType.All,
			LiveSearchFieldType fields = LiveSearchFieldType.ContentId,
			LiveSearchSortType sortType = LiveSearchSortType.StartTime | LiveSearchSortType.SortDecsending,
			ISearchFilter searchFilter = null
			)
        {
			if (string.IsNullOrWhiteSpace(q))
			{
				throw new ArgumentException("q value must contains any character.");
			}
			if (offset < 0 || offset >= SearchConstants.MaxSearchOffset)
			{
				throw new ArgumentException("offset value out of bounds. (0 <= offset <= 1600)");
			}

			if (limit < 0 || limit >= SearchConstants.MaxSearchLimit)
			{
				throw new ArgumentException("limit value out of bounds. (0 <= limit <= 100)");
			}

			var dict = new Dictionary<string, string>()
			{
				{ SearchConstants.QuaryParameter, q },
				{ SearchConstants.OffsetParameter, offset.ToString() },
				{ SearchConstants.LimitParameter, limit.ToString() },
				{ SearchConstants.TargetsParameter, SearchHelpers.ToQueryString(targets) },				
			};

			if (context.AdditionalUserAgent != null)
            {
				dict.Add(SearchConstants.ContextParameter, context.AdditionalUserAgent);
            }

			if (fields != LiveSearchFieldType.None)
			{
				dict.Add(SearchConstants.FieldsParameter, SearchHelpers.ToQueryString(fields));
			}

			if (sortType != LiveSearchSortType.None)
			{
				dict.Add(SearchConstants.SortParameter, SearchHelpers.ToQueryString(sortType));
			}

			if (searchFilter != null)
            {
				var filters = searchFilter.GetFilterKeyValues();
				foreach (var f in filters)
                {
					dict.Add(f.Key, f.Value);
                }
			}

			var json = await context.GetStringAsync(SearchConstants.LiveSearchEndPoint, dict);

			return JsonSerializerExtensions.Load<LiveSearchResponse>(json);
		}
	}




    public class LiveSearchFilterSettings : ISearchFilter
	{
		List<ISearchFilter> _filters;

		public LiveSearchFilterSettings()
        {
			_filters = new List<ISearchFilter>();
		}		

		public LiveSearchFilterSettings AddCompareFilter(LiveSearchIntegerFilterFieldType filterFieldType, int value, SearchFilterCompareCondition condition)
        {
			_filters.Add(new CompareSearchFilter<int>((LiveSearchFilterType)filterFieldType, value, condition));
			return this;
		}

		public LiveSearchFilterSettings AddCompareFilter(LiveSearchDateTimeFilterFieldType filterFieldType, DateTime value, SearchFilterCompareCondition condition)
		{
			_filters.Add(new CompareSearchFilter<DateTime>((LiveSearchFilterType)filterFieldType, value, condition));
			return this;
		}

		public LiveSearchFilterSettings AddContainsFilter(LiveSearchIntegerFilterFieldType filterFieldType, int value)
        {
			_filters.Add(new ValueContainsSearchFilter<int>((LiveSearchFilterType)filterFieldType, value));
			return this;
		}

		public LiveSearchFilterSettings AddContainsFilter(LiveSearchDateTimeFilterFieldType filterFieldType, DateTime value)
		{
			_filters.Add(new ValueContainsSearchFilter<DateTime>((LiveSearchFilterType)filterFieldType, value));
			return this;
		}

		public LiveSearchFilterSettings AddContainsFilter(LiveSearchStringFilterFieldType filterFieldType, string value)
		{
			_filters.Add(new ValueContainsSearchFilter<string>((LiveSearchFilterType)filterFieldType, value));
			return this;
		}

		public LiveSearchFilterSettings AddContainsFilter(LiveSearchBooleanFilterFieldType filterFieldType, bool value)
		{
			_filters.Add(new ValueContainsSearchFilter<bool>((LiveSearchFilterType)filterFieldType, value));
			return this;
		}




		public IEnumerable<KeyValuePair<string, string>> GetFilterKeyValues()
        {
			return _filters.SelectMany(x => x.GetFilterKeyValues());
        }
    }

	public interface ISearchFilter
    {
		IEnumerable<KeyValuePair<string, string>> GetFilterKeyValues();
    }


	public static class SearchHelpers
    {
		public static string ToQueryString(SearchTargetType targets)
        {
			return targets switch
			{
				SearchTargetType.Title => "title",
				SearchTargetType.Tags => "tags",
				SearchTargetType.Description => "description",
				SearchTargetType.Title | SearchTargetType.Tags => "title,tags",
				SearchTargetType.Title | SearchTargetType.Description => "title,description",
				SearchTargetType.Tags | SearchTargetType.Description => "tags,description",
				SearchTargetType.All => "title,description,tags",
				_ => "title,description,tags"
			};
        }

		public static string ToQueryString(SearchFilterCompareCondition condition)
        {
			return condition switch
			{
				SearchFilterCompareCondition.EQ => "0",
				SearchFilterCompareCondition.GT => "gt",
				SearchFilterCompareCondition.GTE => "gte",
				SearchFilterCompareCondition.LT => "lt",
				SearchFilterCompareCondition.LTE => "lte",
				_ => throw new ArgumentException(condition.ToString())
			};
        }


		public static string GetParameterName(SearchFieldType searchFieldType)
        {
			return _fieldTypeToQueryParameterName[searchFieldType];

		}

		static readonly LiveSearchFieldType[] _liveSearchFieldTypes = Enum.GetValues(typeof(LiveSearchFieldType)).Cast<LiveSearchFieldType>()
			.Where(x => x != LiveSearchFieldType.None && x != LiveSearchFieldType.All).ToArray();

		static readonly IDictionary<SearchFieldType, string> _fieldTypeToQueryParameterName;
		static SearchHelpers()
        {
			_fieldTypeToQueryParameterName = 
				Enum.GetValues(typeof(SearchFieldType))
				.Cast<SearchFieldType>()
				.ToDictionary(x => x, x => x.GetDescription())
				;
		}

		public static string ToQueryString(LiveSearchFieldType fieldType)
        {
			return string.Join(",", 
				_liveSearchFieldTypes
					.Where(x => fieldType.HasFlag(x))
					.Select(x => (SearchFieldType)x)
					.Select(x => _fieldTypeToQueryParameterName[x])
					)
				;
        }

		public static string ToQueryString(LiveSearchSortType sortType)
        {
			if (sortType.HasFlag(LiveSearchSortType.SortAcsending))
            {
				return "+" + _fieldTypeToQueryParameterName[(SearchFieldType)(sortType - LiveSearchSortType.SortAcsending)];
			}
			else
            {
				return _fieldTypeToQueryParameterName[(SearchFieldType)sortType];
			}
        }
    }

	public static class SearchConstants
    {
		public static readonly string VideSearchEndPoint = "https://api.search.nicovideo.jp/api/v2/video/contents/search";
		public static readonly string LiveSearchEndPoint = "https://api.search.nicovideo.jp/api/v2/live/contents/search";
		public static readonly string SortOrder_Acsending = "+";
		public static readonly string SortOrder_Decsending = "-";

		public static readonly int MaxSearchOffset = 1600;
		public static readonly int MaxSearchLimit = 100;

		public static readonly string QuaryParameter = "q";
		public static readonly string TargetsParameter = "targets";
		public static readonly string FieldsParameter = "fields";
		public static readonly string FiltersParameter = "filters";
		public static readonly string JsonFilterParameter = "jsonFilter";

		public static readonly string SortParameter = "_sort";
		public static readonly string OffsetParameter = "_offset";
		public static readonly string LimitParameter = "_limit";
		public static readonly string ContextParameter = "_context";
	}

}
