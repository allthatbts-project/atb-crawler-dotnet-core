using NPoco;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASPCore.AllThatBTS.Crawler.Entities
{
    [TableName("TB_YOUTUBE_DATA")]
    public class YoutubeT
    {
        [Column("ID")]
        public string Id { get; set; }
        [Column("YOUTUBE_ID")]
        public string YoutubeId { get; set; }
        [Column("CHANNEL_NAME")]
        public string ChannelName { get; set; }
        [Column("TITLE")]
        public string Title { get; set; }
        [Column("VIEW_CNT")]
        public ulong? ViewCount { get; set; }
        [Column("LIKE_CNT")]
        public ulong? LikeCount { get; set; }
        [Column("DISLIKE_CNT")]
        public ulong? DislikeCount { get; set; }
        [Column("COMMENT_CNT")]
        public ulong? CommentCount { get; set; }
        [Column("PUBLISH_DT")]
        public DateTime? PublishDatetime { get; set; }
        [Column("URL")]
        public string Url { get; set; }
        [Column("DELETED_YN")]
        public string DeleteYN { get; set; }
        [Column("DELETED_DT")]
        public string DeletedDateTime { get; set; }
        [Column("CREATE_DT")]
        public DateTime CreateDateTime { get; set; }
        [Column("UPDATE_DT")]
        public DateTime UpdateDateTime { get; set; }

    }
    [TableName("TB_TWITTER_DATA")]
    public class TwitterT
    {
        [Column("ID")]
        public string Id { get; set; }
        [Column("TWITTER_ID")]
        public string TwitterId { get; set; }
        [Column("ACCOUNT_NAME")]
        public string AccountName { get; set; }
        [Column("TWEET_TEXT")]
        public string TweetText { get; set; }
        [Column("HASHTAGS")]
        public string hashTags { get; set; }
        [Column("RETWEET_CNT")]
        public int RetweetCount { get; set; }
        [Column("URL")]
        public string Url { get; set; }
        [Column("DELETED_YN")]
        public string DeleteYN { get; set; }
        [Column("DELETED_DT")]
        public string DeletedDateTime { get; set; }
        [Column("CREATE_DT")]
        public DateTime CreateDateTime { get; set; }
        [Column("UPDATE_DT")]
        public DateTime UpdateDateTime { get; set; }
    }
    [TableName("TB_INSTAGRAM_DATA")]
    public class InstagramT
    {
        [Column("ID")]
        public string Id { get; set; }
        [Column("INSTAGRAM_ID")]
        public string InstagramId { get; set; }
        [Column("USER_NAME")]
        public string UserName { get; set; }
        [Column("IMAGE_URL")]
        public string ImageUrl { get; set; }
        [Column("URL")]
        public string Url { get; set; }
        [Column("DELETED_YN")]
        public string DeleteYN { get; set; }
        [Column("DELETED_DT")]
        public string DeletedDateTime { get; set; }
        [Column("CREATE_DT")]
        public DateTime CreateDateTime { get; set; }
        [Column("UPDATE_DT")]
        public DateTime UpdateDateTime { get; set; }
    }
}
