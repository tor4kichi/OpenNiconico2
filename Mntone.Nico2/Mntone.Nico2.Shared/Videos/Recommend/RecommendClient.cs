using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mntone.Nico2.Videos.Recommend
{
    internal sealed class RecommendClient
    {

        #region Get Recommend First

        /* 
         * オススメの取得は初回とそれ以降で別々のアクセスが必要になる
         * 最初はリコメンドページにアクセスして、ページに組み込まれたAPIアクセス用のデータを取り出す
         * 2回目以降はAPIを通して直接JSONデータを取得する
         **/ 


        private static Task<string> GetRecommendPageHtmlAsync(NiconicoContext context)
        {
            return context.GetStringAsync(NiconicoUrls.RecommendPageUrl);
        }

        private static RecommendResponse ParseRecommendPageHtml(string html)
        {
            const string Nico_RecommendationsParams = "Nico_RecommendationsParams=";

            var index = html.IndexOf(Nico_RecommendationsParams);
            int openBrakets = 0;
            int closeBrakets = 0;
            int cnt = 0;
            // {}の個数を数えて、イコールになった地点までをリコメンドパラメータJSONとして取得する
            foreach (var c in html.Skip(index + Nico_RecommendationsParams.Length))
            {
                if (c == '{')
                {
                    ++openBrakets;
                }
                else if (c == '}')
                {
                    ++closeBrakets;
                }

                ++cnt;

                if (openBrakets == closeBrakets)
                {
                    break;
                }
            }

            if (openBrakets != closeBrakets) { throw new Exception("Failed recommend page parse, page not contains <Nico_RecommendationsParams>"); }

            var recommendFirstParam = new string(html.Skip(index + Nico_RecommendationsParams.Length).Take(cnt).ToArray());

            // JSONのルートオブジェクトのキーがダブルクオーテーションで囲まれていないことへの対処
            var replaced = recommendFirstParam;
            replaced = replaced.Replace("page:", "\"page\":");
            replaced = replaced.Replace("seed:", "\"seed\":");
            replaced = replaced.Replace("user_tag_param:", "\"user_tag_param\":");
            replaced = replaced.Replace("compiled_tpl:", "\"compiled_tpl\":");
            replaced = replaced.Replace("first_data:", "\"first_data\":");

            return JsonSerializerExtensions.Load<RecommendResponse>(replaced);
        }


        public static Task<RecommendResponse> GetRecommendFirstAsync(NiconicoContext context)
        {
            return GetRecommendPageHtmlAsync(context)
                .ContinueWith(prevTask => ParseRecommendPageHtml(prevTask.Result));
        }

        #endregion



        #region

        private static Task<string> GetRecommendDataAsync(NiconicoContext context, string user_tags, int seed, int page)
        {
            var dict = new Dictionary<string, string>();
            dict.Add(nameof(user_tags), user_tags);
            dict.Add(nameof(seed), seed.ToString());
            dict.Add(nameof(page), page.ToString());

            return context.GetStringAsync(NiconicoUrls.RecommendApiUrl, dict);
        }

        private static RecommendContent ParseRecommendJson(string json)
        {
            return JsonSerializerExtensions.Load<RecommendContent>(json);
        }


        public static Task<RecommendContent> GetRecommendAsync(NiconicoContext context, string user_tags, int seed, int page)
        {
            return GetRecommendDataAsync(context, user_tags, seed, page)
                .ContinueWith(prevTask => ParseRecommendJson(prevTask.Result));
        }

        #endregion

    }
}
