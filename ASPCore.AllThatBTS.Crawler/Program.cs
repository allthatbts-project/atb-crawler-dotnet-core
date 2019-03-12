using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace ASPCore.AllThatBTS.Crawler
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Youtube API 호출
            YouTubeService youtube = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyDRathtsgwHpclhV8FfsTv8TiDBO9dhVxk"
            });

            SearchResource.ListRequest listRequest = youtube.Search.List("snippet");
            listRequest.Q = "BTS";
            listRequest.Order = SearchResource.ListRequest.OrderEnum.Relevance;
            listRequest.MaxResults = 50;

            SearchListResponse searchResponse = listRequest.Execute();

            List<string> videos = new List<string>();
            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();

            foreach (SearchResult searchResult in searchResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        videos.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                        break;

                    case "youtube#channel":
                        channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                        break;

                    case "youtube#playlist":
                        playlists.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
                        break;
                }
            }

            Console.WriteLine(String.Format("Videos:\n{0}\n", String.Join("\n", videos.ToArray())));
            Console.WriteLine(String.Format("Channels:\n{0}\n", String.Join("\n", channels.ToArray())));
            Console.WriteLine(String.Format("Playlists:\n{0}\n", String.Join("\n", playlists.ToArray())));
            #endregion

            #region Twitter API 호출
            // Set up your credentials (https://apps.twitter.com)
            Auth.SetUserCredentials("Hoa9WW289WzEMAFbGFQrw9gLI",
                                    "KjMVnYH9AhHEhRV9VGwv8kY8DQLl5iT2VSryKzzzEgqbQtQ3N9",
                                    "800358379218616320-eCXlSIM8RR8GBZEQpQZfNvb5txUkUei",
                                    "KgB7jynItIR4qcSkkeFAEVxa4eMVxhP7Aycq6zqCWlEkL");

            // Publish the Tweet "Hello World" on your Timeline
            SearchTweetsParameters parameters = new SearchTweetsParameters("BTS_twt")
            {
                SearchType = SearchResultType.Recent,
                MaximumNumberOfResults = 100
            };


            List<ITweet> tweets = Search.SearchTweets(parameters).ToList();

            Console.WriteLine(String.Format("Twts:\n{0}\n", String.Join("\n", tweets)));
            #endregion

            #region Instagram API

            #endregion

            Console.ReadKey();
        }
    }
}
