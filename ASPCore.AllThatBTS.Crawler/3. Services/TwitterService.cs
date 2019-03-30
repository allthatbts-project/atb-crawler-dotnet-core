using ASPCore.AllThatBTS.Crawler.Common;
using ASPCore.AllThatBTS.Crawler.Entities;
using ASPCore.AllThatBTS.Crawler.Repository;
using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace ASPCore.AllThatBTS.Crawler.Services
{
    public class TwitterService
    {
        private readonly TwitterRepository twitterRepository;
        protected readonly Logger logger;
        private List<string> twitterIds;

        public TwitterService()
        {
            twitterRepository = new TwitterRepository();
            Auth.SetUserCredentials(AppConfiguration.TwitterConsumerKey,
                                    AppConfiguration.TwitterConsumerSecret,
                                    AppConfiguration.TwitterUserAccessToken,
                                    AppConfiguration.TwitterUserAccessSecret);
            LogManager.Configuration = new XmlLoggingConfiguration(AppConfiguration.NLogPath);

            logger = LogManager.GetCurrentClassLogger();
        }

        #region Step 1. 트위터 멤버 계정 불러오기
        public List<string> GetTwitterIdList()
        {
            twitterIds = AppConfiguration.TwitterIds.Split(";").ToList();
            return twitterIds;
        }
        #endregion

        #region Step 2. 트위터 계정 타임라인 저장

        public void SetTwitterDataByIds(List<string> twitterIds)
        {
            logger.Info("/-----Twitter Service Save Timelines In Ids Start-----/");
            int savedCount = 0;
            try
            {
                if (twitterIds.Count > 0)
                {
                    foreach (string twitterId in twitterIds)
                    {
                        //var userIdentifier = User.GetUserFromScreenName(twitterId);
                        var userTimelineParameters = new UserTimelineParameters()
                        {
                            MaximumNumberOfTweetsToRetrieve = 10
                        };
                        var tweets = Timeline.GetUserTimeline(twitterId, userTimelineParameters);

                        foreach (var tweet in tweets)
                        {
                            /*
                             [Column("TWITTER_ID")]
                             [Column("ACCOUNT_NAME")]
                             [Column("TWEET_TEXT")]
                             [Column("HASHTAGS")]
                             [Column("RETWEET_CNT")]
                             [Column("URL")]
                             */
                            TwitterT entity = new TwitterT()
                            {
                                TwitterId = tweet.IdStr,
                                TweetText = tweet.FullText,
                                hashTags = tweet.Hashtags.ToString(),
                                AccountName = tweet.IdStr,
                                RetweetCount = tweet.RetweetCount,
                                Url = tweet.Url
                            };
                            twitterRepository.InsertTwitterData(entity);

                            savedCount++;
                            break;
                        }
                    }
                }
                logger.Info("/-----Twitter Service Save Timelines In Ids End! Saved Count : " + savedCount + " -----/");
                logger.Info("/-----Twitter Data Save End-----/");
            }
            catch (Exception e)
            {
                logger.Error(e, "Twitter Service Error");
            }
        } 
        #endregion
    }
}
