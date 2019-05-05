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
        /// <summary>
        /// 마지막으로 저장 Youtube 데이터의 발행일자 불러오기
        /// </summary>
        /// <returns></returns>
        public DateTime? SelectLastSavedPublishDate()
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            return Connection.ExecuteScalar<DateTime>(sql);
        }

        /// <summary>
        /// 저장된 Youtube 데이터의 업데이트
        /// </summary>
        /// <param name="days">업데이트 이후 days 일 전</param>
        /// <returns></returns>
        public List<YoutubeT> SelectOutOfDateVideoList(int days)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                DAYS = days
            };

            return Connection.Fetch<YoutubeT>(sql, parameters);
        }

        /// <summary>
        /// 이미 저장된 Youtube 인지 확인
        /// </summary>
        /// <param name="videoId">videoID</param>
        /// <returns></returns>
        public bool SelectExistsYoutubeData(string videoId)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                YOUTUBE_ID = videoId
            };

            return Connection.ExecuteScalar<bool>(sql, parameters);
        }

        /// <summary>
        /// Youtube 데이터 입력
        /// </summary>
        /// <param name="youtubeData"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 통계 데이터 업데이트
        /// </summary>
        /// <param name="youtubeData"></param>
        /// <returns></returns>
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
