using ASPCore.AllThatBTS.Crawler.Common;
using ASPCore.AllThatBTS.Crawler.Entities;
using ASPCore.AllThatBTS.Crawler.Repository;
using InstagramApiSharp;
using InstagramApiSharp.API;
using InstagramApiSharp.API.Builder;
using InstagramApiSharp.Classes;
using InstagramApiSharp.Classes.Models;
using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;

namespace ASPCore.AllThatBTS.Crawler.Services
{
    public class InstagramService
    {
        private readonly InstagramRepository instagramRepository;
        protected readonly Logger logger;
        private readonly IInstaApi instaApi;

        public InstagramService()
        {
            instagramRepository = new InstagramRepository();
            LogManager.Configuration = new XmlLoggingConfiguration(AppConfiguration.NLogPath);
            logger = LogManager.GetCurrentClassLogger();

            instaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(new UserSessionData()
                {
                    UserName = AppConfiguration.InstagramApiUserName,
                    Password = AppConfiguration.InstagramApiPassword,

                })
                .Build();

            var logInResult = instaApi.LoginAsync().Result;
            if (logInResult.Succeeded)
            {
                logger.Info("/-----Instagram Service Login Success-----/");
            }
        }

        #region Step 1. 사용자 리스트로 User Media 조회 및 Insert
        public void SetMediaListByUserName(List<string> userNameList)
        {
            logger.Info("/-----Instagram Service Save Media From UserName Start-----/");
            int savedCount = 0;

            try
            {
                // 인스타 UserName 존재하면,
                if (userNameList.Count > 0)
                {
                    foreach (string userName in userNameList)
                    {
                        logger.Info("/-----UserName : " + userName + " Save Start-----/");
                        PaginationParameters paginationParameters = PaginationParameters.MaxPagesToLoad(5);


                        InstaMediaList collections =
                            instaApi.UserProcessor.GetUserMediaAsync(userName, paginationParameters).Result.Value;


                        foreach (InstaMedia instaMedia in collections)
                        {
                            string imgUri = string.Empty;
                            if(instaMedia.Carousel != null)
                            {
                                imgUri = instaMedia.Carousel[0].Images[0].Uri;
                            }
                            else
                            {
                                imgUri = instaMedia.Images[0].Uri;
                            }

                            InstagramT entity = new InstagramT()
                            {
                                InstagramId = instaMedia.InstaIdentifier,
                                UserName = instaMedia.User.UserName,
                                ImageUrl = imgUri,
                                Url = instaMedia.Code
                            };
                            instagramRepository.InsertInstagramData(entity);
                            savedCount++;
                        }

                        logger.Info("/-----UserName : " + userName + " Save End-----/");
                    }
                }
                logger.Info("/-----Instagram Service Save Media End! Saved Count : " + savedCount + " -----/");

            }
            catch (Exception e)
            {
                logger.Error(e, "Instagram Service Error");
            }
        }
        #endregion



        //IResult<InstaMediaList> collections = api.UserProcessor.GetUserMediaAsync("nidorinaa", InstagramApiSharp.PaginationParameters.MaxPagesToLoad(1)).Result;
        //Console.WriteLine($"Loaded {collections.Value.Count} collections for current user");
        //foreach (InstaMedia instaCollection in collections.Value)
        //{
        //    Console.WriteLine($"Collection: imageurl={instaCollection.Images[0].Uri}, id={instaCollection.User.UserName}");
        //}

    }
}
