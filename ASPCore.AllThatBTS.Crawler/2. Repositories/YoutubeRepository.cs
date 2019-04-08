using ASPCore.AllThatBTS.Crawler.Common;
using ASPCore.AllThatBTS.Crawler.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ASPCore.AllThatBTS.Crawler.Repository
{
    public class YoutubeRepository : BaseRepository
    {
        public int InsertYoutubeData(YoutubeT youtubeData)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                YOUTUBE_ID = youtubeData.YoutubeId,
                CHANNEL_NAME = youtubeData.ChannelName,
                TITLE = youtubeData.Title,
                PUBLISH_DT = youtubeData.PublishDatetime,
                THUMBNAIL_IMG_URL = youtubeData.ThumbnailImageUrl,
                URL = "https://www.youtube.com/watch?v=" + youtubeData.YoutubeId,
            };

            return Connection.Execute(sql, parameters);
        }

        public int UpdateYoutubeData(YoutubeT youtubeData)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                YOUTUBE_ID = youtubeData.YoutubeId,
                VIEW_CNT = youtubeData.ViewCount,
                LIKE_CNT = youtubeData.LikeCount,
                DISLIKE_CNT = youtubeData.DislikeCount,
                COMMENT_CNT = youtubeData.CommentCount,
            };

            return Connection.Execute(sql, parameters);
        }
    }
}
