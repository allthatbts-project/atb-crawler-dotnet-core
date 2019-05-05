using ASPCore.AllThatBTS.Crawler.Common;
using ASPCore.AllThatBTS.Crawler.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASPCore.AllThatBTS.Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Crawler Start....");
            #region Youtube API 호출

            Console.WriteLine("Youtube Crawling Start....");
            YoutubeService youtubeService = new YoutubeService();
            List<string> channelIDList = AppConfiguration.YoutubeChannelId.Split(";").ToList();
            int loopCount = AppConfiguration.YoutubeLoopCount;

            List<string> videoIdList = youtubeService.SetVideoListInChannel(channelIDList, loopCount);
            youtubeService.SetInfoInVideoList(videoIdList);
            youtubeService.SyncronizeVideoList(AppConfiguration.YoutubeSyncToDays);

            Console.WriteLine("Youtube Crawling End!");
            #endregion

            #region Twitter API 호출
            //Console.WriteLine("Twitter Crawling Start....");
            //TwitterService twitterService = new TwitterService();
            //List<string> twitterIDList = twitterService.GetTwitterIdList();

            //twitterService.SetTwitterDataByIds(twitterIDList);

            //Console.WriteLine("Twitter Crawling End!");
            #endregion

            #region Instagram API 호출

            //Console.WriteLine("Instagram Crawling Start....");
            //InstagramService instagramService = new InstagramService();
            //List<string> userNameList = AppConfiguration.InstagramUserNameList.Split(";").ToList();

            //instagramService.SetMediaListByUserName(userNameList);

            //Console.WriteLine("Instagram Crawling End!");
            #endregion

            Console.ReadKey();
        }
    }
}
