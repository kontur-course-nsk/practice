using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace Multithreading
{
    public class Program
    {
        static void Main(string[] args)
        {
            var tweets = new List<ITweet>();
            Auth.SetUserCredentials("CONSUMER_KEY", "CONSUMER_SECRET", "ACCESS_TOKEN", "ACCESS_TOKEN_SECRET");

            // Если у вас нет ключей для твиттера, то подключитесь к локальному сервису
            //var localSearcher = new LocalSearch("localhost", 5000);
            long maxId = 0;
            for (int i = 0; i < 15; i++)
            {
                var request = new SearchTweetsParameters("#Java,#C#")
                {
                    Lang = LanguageFilter.English,
                    SearchType = SearchResultType.Recent,
                    MaximumNumberOfResults = 40,
                    MaxId = maxId, // https://developer.twitter.com/en/docs/tweets/timelines/guides/working-with-timelines.html
                    Filters = TweetSearchFilters.Hashtags
                };

                var searchResult = Search.SearchTweets(request).ToList();
                //var searchResult = localSearcher.SearchTweets(request).ToList();//Search.SearchTweets(request).ToList();
                tweets.AddRange(i == 0 ? searchResult : searchResult.Skip(1));
                maxId = searchResult.Min(x => x.Id);
            }

            PrintResult(tweets);
        }

        private static void PrintResult(List<ITweet> tweets)
        {
            var countCSharpHashtag = 0;
            var countJavaHashtag = 0;
            foreach (var tweet in tweets)
            {
                var hashtags = tweet.Hashtags.Select(x => x.Text).ToList();
                if (hashtags.Contains("C#")
                    || hashtags.Contains("c#")
                    || hashtags.Contains("csharp")
                    || hashtags.Contains("Csharp")
                    || hashtags.Contains("CSharp")
                    || hashtags.Contains("CSHARP"))
                {
                    countCSharpHashtag++;
                }

                if (hashtags.Contains("Java") || hashtags.Contains("java") || hashtags.Contains("JAVA"))
                {
                    countJavaHashtag++;
                }

                if (countCSharpHashtag + countJavaHashtag != 0)
                {
                    Console.Write(
                        "\rC#:{0:P}    Java:{1:P}",
                        decimal.Divide(countCSharpHashtag, countCSharpHashtag + countJavaHashtag),
                        decimal.Divide(countJavaHashtag, countCSharpHashtag + countJavaHashtag));
                }
            }

            Console.ReadKey();
        }
    }
}
