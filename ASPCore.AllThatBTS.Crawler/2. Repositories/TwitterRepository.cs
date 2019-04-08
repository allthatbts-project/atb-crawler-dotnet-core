using ASPCore.AllThatBTS.Crawler.Common;
using ASPCore.AllThatBTS.Crawler.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ASPCore.AllThatBTS.Crawler.Repository
{
    public class TwitterRepository : BaseRepository
    {
        public int InsertTwitterData(TwitterT twitterData)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                TWITTER_ID = twitterData.TwitterId,
                ACCOUNT_NAME = twitterData.AccountName,
                TWEET_TEXT = twitterData.TweetText,
                HASHTAGS = twitterData.hashTags,
                RETWEET_CNT = twitterData.RetweetCount,
                URL = twitterData.Url
            };

            return Connection.Execute(sql, parameters);
        }
    }
}
