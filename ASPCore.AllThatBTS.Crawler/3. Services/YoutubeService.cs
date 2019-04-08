using ASPCore.AllThatBTS.Crawler.Common;
using ASPCore.AllThatBTS.Crawler.Entities;
using ASPCore.AllThatBTS.Crawler.Repository;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace ASPCore.AllThatBTS.Crawler.Services
{
    public class YoutubeService
    {
        private readonly YoutubeRepository youtubeRepository;
        private readonly YouTubeService youtube;
        protected readonly Logger logger;
        private List<string> channelIdList;
        private List<string> videoIdList;
        private DateTime? lastSavedPublishDate;

        public YoutubeService()
        {
            youtubeRepository = new YoutubeRepository();
            youtube = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = AppConfiguration.YoutubeAPIKey
            });
            LogManager.Configuration = new XmlLoggingConfiguration(AppConfiguration.NLogPath);
            logger = LogManager.GetCurrentClassLogger();
        }

        #region Step 1. Channel의 Video 리스트 조회 및 저장
        public List<string> SetVideoListInChannel(List<string> channelIdList, int loopCount)
        {
            logger.Info("/-----Youtube Service Save VideoList In Channel Start-----/");
            int savedCount = 0;
            videoIdList = new List<string>();

            try
            {
                // 유튜브 채널 아이디 존재하면,
                if (channelIdList.Count > 0)
                {
                    foreach (string channelId in channelIdList)
                    {
                        logger.Info("/-----Channel ID : " + channelId + " Save Start-----/");
                        string pageToken = string.Empty;
                        for (int i = 0; i < loopCount; i++)
                        {
                            SearchResource.ListRequest listRequest = youtube.Search.List("snippet");
                            listRequest.ChannelId = channelId;
                            listRequest.MaxResults = 50;
                            listRequest.PageToken = pageToken;
                            listRequest.PublishedAfter = lastSavedPublishDate;

                            SearchListResponse searchResponse = listRequest.Execute();

                            foreach (SearchResult searchResult in searchResponse.Items)
                            {
                                // 동영상의 통계 자료를 넣기 위해 VideoId를 저장하는 변수
                                videoIdList.Add(searchResult.Id.VideoId);

                                switch (searchResult.Id.Kind)
                                {
                                    case "youtube#video":
                                        // 데이터 저장
                                        YoutubeT entity = new YoutubeT()
                                        {
                                            YoutubeId = searchResult.Id.VideoId,
                                            ChannelName = searchResult.Snippet.ChannelTitle,
                                            Title = searchResult.Snippet.Title,
                                            ThumbnailImageUrl = searchResult.Snippet.Thumbnails.High.Url,
                                            // Url은 DB Insert 할때 넣어주는 거로....
                                            /* Url = https://www.youtube.com/watch?v= + searchResult.Id.VideoId */
                                            PublishDatetime = searchResult.Snippet.PublishedAt
                                        };
                                        youtubeRepository.InsertYoutubeData(entity);

                                        savedCount++;
                                        break;
                                }
                            }
                            pageToken = searchResponse.NextPageToken;
                        }
                        logger.Info("/-----Channel ID : " + channelId + " Save End-----/");
                    }
                }
                logger.Info("/-----Youtube Service Save VideoList In Channel End! Saved Count : " + savedCount + " -----/");

            }
            catch (Exception e)
            {
                logger.Error(e, "Youtube Service Step 2 Error");
            }

            return videoIdList;
        }
        #endregion

        #region Step 2. Video 정보 Update
        public void SetInfoInVideoList(List<string> videoIdList)
        {
            int savedCount = 0;
            logger.Info("/-----Youtube Service Save Statistics Update Start-----/");
            try
            {
                if(videoIdList.Count > 0)
                {
                    foreach(string videoId in videoIdList)
                    {
                        VideosResource.ListRequest listRequest = youtube.Videos.List("statistics");
                        listRequest.Id = videoId;

                        VideoListResponse videoResponse = listRequest.Execute();

                        foreach (Video videoResult in videoResponse.Items)
                        {
                            switch (videoResult.Kind)
                            {
                                case "youtube#video":
                                    // 데이터 저장
                                    YoutubeT entity = new YoutubeT()
                                    {
                                        YoutubeId = videoResult.Id,
                                        ViewCount = videoResult.Statistics.ViewCount,
                                        LikeCount = videoResult.Statistics.LikeCount,
                                        DislikeCount = videoResult.Statistics.DislikeCount,
                                        CommentCount = videoResult.Statistics.CommentCount
                                    };
                                    youtubeRepository.UpdateYoutubeData(entity);
                                    savedCount++;
                                    break;
                            }
                        }
                    }

                }
                logger.Info("/-----Youtube Service Save Statistics Update End! Saved Count : " + savedCount + " -----/");
            }
            catch (Exception e)
            {
                logger.Error(e, "Youtube Service Step 2 Error");
            }
        }
        #endregion

    }
}
