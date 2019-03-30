using System;
using System.Collections.Generic;
using System.Linq;
using ASPCore.AllThatBTS.Crawler.Common;
using ASPCore.AllThatBTS.Crawler.Services;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace ASPCore.AllThatBTS.Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Crawler Start....");
            #region Youtube API 호출

            //Console.WriteLine("Youtube Crawling Start....");
            //YoutubeService youtubeService = new YoutubeService();
            //List<string> channelIDList = AppConfiguration.YoutubeChannelId.Split(";").ToList();
            //int loopCount = AppConfiguration.YoutubeLoopCount;

            //List<string> videoIdList = youtubeService.SetVideoListInChannel(channelIDList, loopCount);
            //youtubeService.SetInfoInVideoList(videoIdList);

            //Console.WriteLine("Youtube Crawling End!");
            #endregion

            #region Twitter API 호출
            Console.WriteLine("Twitter Crawling Start....");
            TwitterService twitterService = new TwitterService();
            List<string> twitterIDList = twitterService.GetTwitterIdList();


            twitterService.SetTwitterDataByIds(twitterIDList);

            Console.WriteLine("Twitter Crawling End!");
            #endregion

            #region Instagram API

            //var api = InstaApiBuilder.CreateBuilder()
            //    // required
            //    .SetUser(new UserSessionData()
            //    {
            //        UserName = "frogmaster1231",
            //        Password = "0(@apadm@)",

            //    })
            //    // optional
            //    .Build();

            //var logInResult = api.LoginAsync().Result;
            //if (logInResult.Succeeded)
            //{
            //    Console.WriteLine("Connected");
            //}


            //IResult<InstaMediaList> collections = api.UserProcessor.GetUserMediaAsync("nidorinaa", InstagramApiSharp.PaginationParameters.MaxPagesToLoad(1)).Result;
            //Console.WriteLine($"Loaded {collections.Value.Count} collections for current user");
            //foreach (InstaMedia instaCollection in collections.Value)
            //{
            //    Console.WriteLine($"Collection: imageurl={instaCollection.Images[0].Uri}, id={instaCollection.User.UserName}");
            //}
            #endregion

            Console.ReadKey();
        }
    }
}
