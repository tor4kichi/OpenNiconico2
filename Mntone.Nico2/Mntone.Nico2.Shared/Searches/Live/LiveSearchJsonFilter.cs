using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Searches.Live
{
	public interface IJsonSearchFilter : ISearchFilter
	{
		IJsonSearchFilterData GetJsonFilterData();
    }

	public interface IJsonSearchFilterData
    {

    }

	public sealed class EqaulJsonFilter<T> : IJsonSearchFilter
	{
		private readonly SearchFieldType _filterType;
		private readonly T _value;
        private string _json;

        public EqaulJsonFilter(LiveSearchFilterType filterType, T value)
		{
			_filterType = (SearchFieldType)filterType;
			_value = value;
		}



		public IEnumerable<KeyValuePair<string, string>> GetFilterKeyValues()
		{
			var data = GetJsonFilterData();

			var json = _json ?? JsonConvert.SerializeObject(data);

			return new[] { new KeyValuePair<string, string>(SearchConstants.JsonFilterParameter, json) };
		}

        public IJsonSearchFilterData GetJsonFilterData()
        {
			return new EqualJsonFilterData()
			{
				Field = SearchHelpers.GetParameterName(_filterType),
				Value = _value.ToString()
			};
		}

		[DataContract]
		class EqualJsonFilterData : IJsonSearchFilterData
		{
			[DataMember(Name = "type")]
			public string Type => "equal";

			[DataMember(Name = "field")]
			public string Field { get; set; }

			[DataMember(Name = "value")]
			public string Value { get; set; }
		}
	}

	public sealed class RangeJsonFilter<T> : IJsonSearchFilter
    {
        private RangeJsonFilterData _rangeJsonFilterData;
        private string _json;

        public RangeJsonFilter(LiveSearchFilterType filterType, T from, T to, bool include_lower = true, bool include_upper = true)
        {
			_rangeJsonFilterData = new RangeJsonFilterData()
			{
				Field = SearchHelpers.GetParameterName((SearchFieldType)filterType),
				From = from.ToString(),
				To = to.ToString(),
				IncludeLower = include_lower == false ? false : default(bool?),
				IncludeUpper = include_upper == false ? false : default(bool?),
			};
		}

		

        public IEnumerable<KeyValuePair<string, string>> GetFilterKeyValues()
        {
			var data = GetJsonFilterData();

			var json = JsonConvert.SerializeObject(data);

			return new[] { new KeyValuePair<string, string>(SearchConstants.JsonFilterParameter, json) };
		}

        public IJsonSearchFilterData GetJsonFilterData()
        {
            return _rangeJsonFilterData;
		}

        [DataContract]
		class RangeJsonFilterData : IJsonSearchFilterData
		{
			[DataMember(Name = "type")]
			public string Type => "range";

			[DataMember(Name = "field")]
			public string Field { get; set; }

			[DataMember(Name = "from")]
			public string From { get; set; }

			[DataMember(Name = "to")]
			public string To { get; set; }

			[DataMember(Name = "include_lower")]
			public bool? IncludeLower { get; set; }

			[DataMember(Name = "include_upper")]
			public bool? IncludeUpper { get; set; }
		}
	}

    public sealed class OrJsonFilter : IJsonSearchFilter
    {
        private readonly IList<IJsonSearchFilter> _filters;

        public OrJsonFilter(IEnumerable<IJsonSearchFilter> filters)
        {
            _filters = filters.ToList();
        }

		public IEnumerable<KeyValuePair<string, string>> GetFilterKeyValues()
        {
			var data = GetJsonFilterData();

			var json = JsonConvert.SerializeObject(data);

			return new[] { new KeyValuePair<string, string>(SearchConstants.JsonFilterParameter, json) };
        }

		public IJsonSearchFilterData GetJsonFilterData()
		{
			return new OrJsonFilterData()
			{
				Filters = _filters.Select(x => x.GetJsonFilterData()).ToList()
			};
		}


		[DataContract]
		class OrJsonFilterData : IJsonSearchFilterData
		{
			[DataMember(Name = "type")]
			public string Type => "or";

			[DataMember(Name = "filters")]
			public List<IJsonSearchFilterData> Filters { get; set; }
		}
    }


	public sealed class AndJsonFilter : IJsonSearchFilter
	{
		private readonly IList<IJsonSearchFilter> _filters;

		public AndJsonFilter(IEnumerable<IJsonSearchFilter> filters)
		{
			_filters = filters.ToList();
		}

		public IEnumerable<KeyValuePair<string, string>> GetFilterKeyValues()
		{
			var data = GetJsonFilterData();

			var json = JsonConvert.SerializeObject(data);

			return new[] { new KeyValuePair<string, string>(SearchConstants.JsonFilterParameter, json) };
		}

		public IJsonSearchFilterData GetJsonFilterData()
		{
			return new AndJsonFilterData()
			{
				Filters = _filters.Select(x => x.GetJsonFilterData()).ToList()
			};
		}


		[DataContract]
		class AndJsonFilterData : IJsonSearchFilterData
		{
			[DataMember(Name = "type")]
			public string Type => "and";

			[DataMember(Name = "filters")]
			public List<IJsonSearchFilterData> Filters { get; set; }
		}
	}


	public sealed class NotJsonFilter : IJsonSearchFilter
	{
		private readonly IJsonSearchFilter _filter;

		public NotJsonFilter(IJsonSearchFilter filter)
		{
			_filter = filter;
		}

		public IEnumerable<KeyValuePair<string, string>> GetFilterKeyValues()
		{
			var data = GetJsonFilterData();

			var json = JsonConvert.SerializeObject(data);

			return new[] { new KeyValuePair<string, string>(SearchConstants.JsonFilterParameter, json) };
		}

		public IJsonSearchFilterData GetJsonFilterData()
		{
			return new NotJsonFilterData()
			{
				Filter = _filter.GetJsonFilterData()
			};
		}


		[DataContract]
		class NotJsonFilterData : IJsonSearchFilterData
		{
			[DataMember(Name = "type")]
			public string Type => "not";

			[DataMember(Name = "filter")]
			public IJsonSearchFilterData Filter { get; set; }
		}
	}


	public enum JsonFilterType
	{
		Equal,
		Range,
		Or,
		And,
		Not,
	}
}
