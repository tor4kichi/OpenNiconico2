using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mntone.Nico2.Live.Recommend
{
    public sealed class LiveRecommendResponse
    {
        public IReadOnlyCollection<LiveRecommendData> RecommendItems => _RecommendItems;

        internal List<LiveRecommendData> _RecommendItems;
    }


    [DataContract]
    public sealed class LiveRecommendData
    {
        // Note: program_id は 文字列または整数値の場合が混在している
        // recommend_system_type_name が izanami と gyokuonがあって
        // izanamiの場合はprogram_idが文字列、gyokuon は 整数値 となっているように見える

        // 同様に

        [DataMember(Name = "program_id")]
        private object __ProgramId { get; set; }

        private int? _ProgramId;
        public int ProgramId
        {
            get
            {
                if (_ProgramId == null)
                {
                    if (__ProgramId is long longProgramId)
                    {
                        _ProgramId = (int)longProgramId;
                    }
                    else if (__ProgramId is string strProgramId)
                    {
                        _ProgramId = int.Parse(strProgramId);
                    }
                }

                return _ProgramId.Value;
            }
        }

        [DataMember(Name = "provider_type")]
        private string __ProviderType { get; set; }
        private CommunityType? _ProviderType;
        public CommunityType ProviderType
        {
            get
            {
                return (_ProviderType ?? (_ProviderType = CommunityTypeExtensions.ToCommunityType(__ProviderType))).Value;
            }
        }

        [DataMember(Name = "default_community")]
        public string DefaultCommunity { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "thumbnail_url")]
        public string ThumbnailUrl { get; set; }

        [DataMember(Name = "thumbnail_small_url")]
        public string ThumbnailSmallUrl { get; set; }

        [DataMember(Name = "open_time")]
        private string __OpenTime { get; set; }
        private DateTimeOffset? _OpenTime;

        public DateTimeOffset OpenTime
        {
            get
            {
                if (_OpenTime == null)
                {
                    if (DateTimeOffset.TryParse(__OpenTime, out var resultTime))
                    {
                        _OpenTime = resultTime;
                    }
                    if (DateTimeOffset.TryParseExact(__OpenTime, "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.AssumeLocal, out resultTime))
                    {
                        _OpenTime = resultTime;
                    }
                }

                return _OpenTime.Value;
            }
        }


        [DataMember(Name = "start_time")]
        private string __StartTime { get; set; }

        private DateTimeOffset? _StartTime;

        public DateTimeOffset StartTime
        {
            get
            {
                if (_StartTime == null)
                {
                    if (DateTimeOffset.TryParse(__StartTime, out var resultTime))
                    {
                        _StartTime = resultTime;
                    }
                    else if (DateTimeOffset.TryParseExact(__StartTime, "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.AssumeLocal, out resultTime))
                    {
                        _StartTime = resultTime;
                    }
                }

                return _StartTime.Value;
            }
        }

        [DataMember(Name = "current_status")]
        private string __CurrentStatus { get; set; }
        private Live.StatusType? _CurrentStatus;
        public StatusType CurrentStatus
        {
            get
            {
                return (_CurrentStatus ?? (_CurrentStatus = StatusTypeExtensions.ToStatusType(__CurrentStatus))).Value;
            }
        }



        [DataMember(Name = "recommend_system_type_name")]
        public string RecommendSystemTypeName { get; set; }


        
    }
}
