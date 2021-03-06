﻿using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Time;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using TimeZoneConverter;

namespace ASPCore.AllThatBTS.Crawler.Common
{
    public static class AppConfiguration
    {
        public static string SqlDataConnection { get { return GetSqlConnection(); } }
        public static string JwtSecret { get { return GetJwtSecret(); } }
        public static string NLogPath { get { return GetNLogPath(); } }
        
        #region Youtube Config
        public static string YoutubeAPIKey { get { return GetYoutubeAPIKey(); } }
        public static string YoutubeChannelId { get { return GetYoutubeChannelId(); } }
        public static int YoutubeLoopCount { get { return GetYoutubeLoopCount(); } }
        #endregion

        #region Twitter Config
        public static string TwitterConsumerKey { get { return GetTwitterConsumerKey(); } }
        public static string TwitterConsumerSecret { get { return GetTwitterConsumerSecret(); } }
        public static string TwitterUserAccessToken { get { return GetTwitterUserAccessToken(); } }
        public static string TwitterUserAccessSecret { get { return GetTwitterUserAccessSecret(); } }
        public static string TwitterIds { get { return GetTwitterIds(); } }
        #endregion

        #region Instagram Config
        public static string InstagramApiUserName { get { return GetInstagramApiUserName(); } }
        public static string InstagramApiPassword { get { return GetInstagramApiPassword(); } }
        public static string InstagramUserNameList { get { return GetTargetUserName(); } }
        #endregion

        private static IConfigurationSection AppSetting
        {
            get
            {
                var configurationBuilder = new ConfigurationBuilder();
                string path = string.Empty;
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Development.json");
                }
                else
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                }

                configurationBuilder.AddJsonFile(path, false);
                var root = configurationBuilder.Build();
                return root.GetSection("ConfigurationManager");
            }
        }

        private static IConfigurationSection AppSettingService
        {
            get
            {
                var configurationBuilder = new ConfigurationBuilder();
                string path = string.Empty;
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Development.json");
                }
                else
                {
                    path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                }

                configurationBuilder.AddJsonFile(path, false);
                var root = configurationBuilder.Build();
                return root.GetSection("ServiceManager");
            }
        }

        private static string GetSqlConnection()
        {
            return AppSetting["ConnectionString"];
        }
        private static string GetJwtSecret()
        {
            return AppSetting["JwtSecret"];
        }
        private static string GetNLogPath()
        {
            GlobalDiagnosticsContext.Set("configDir", Path.Combine(Directory.GetCurrentDirectory(), "NLogFile"));
            string logConfigPath = string.Empty;

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                logConfigPath = Path.Combine(Directory.GetCurrentDirectory(), "nlog.Development.config");
            }
            else
            {
                logConfigPath = Path.Combine(Directory.GetCurrentDirectory(), "nlog.config");
            }
            return logConfigPath;
        }
        private static string GetYoutubeAPIKey()
        {
            return AppSettingService.GetSection("YoutubeConfig")["APIKey"];
        }
        private static string GetYoutubeChannelId()
        {
            return AppSettingService.GetSection("YoutubeConfig")["ChannelID"];
        }
        private static int GetYoutubeLoopCount()
        {
            return Convert.ToInt16(AppSettingService.GetSection("YoutubeConfig")["loopCount"]);
        }
        private static string GetTwitterConsumerKey()
        {
            return AppSettingService.GetSection("TwitterConfig")["ConsumerKey"];
        }
        private static string GetTwitterConsumerSecret()
        {
            return AppSettingService.GetSection("TwitterConfig")["ConsumerSecret"];
        }
        private static string GetTwitterUserAccessToken()
        {
            return AppSettingService.GetSection("TwitterConfig")["UserAccessToken"];
        }
        private static string GetTwitterUserAccessSecret()
        {
            return AppSettingService.GetSection("TwitterConfig")["UserAccessSecret"];
        }
        private static string GetTwitterIds()
        {
            return AppSettingService.GetSection("TwitterConfig")["TwitterIds"];
        }
        private static string GetInstagramApiUserName()
        {
            return AppSettingService.GetSection("InstagramConfig")["ApiUserName"];
        }
        private static string GetInstagramApiPassword()
        {
            return AppSettingService.GetSection("InstagramConfig")["ApiUserPassword"];
        }
        private static string GetTargetUserName()
        {
            return AppSettingService.GetSection("InstagramConfig")["UserName"];
        }


    }
}

[TimeSource("CustomTimeZone")]
public class CustomTimeZoneTimeSource : TimeSource
{
    string ZoneName;
    TimeZoneInfo ZoneInfo;

    [Required]
    public string Zone
    {
        get { return ZoneName; }
        set
        {
            ZoneName = value;
            ZoneInfo
                = TZConvert.GetTimeZoneInfo(ZoneName);
        }
    }

    public override DateTime Time
    {
        get
        {
            return TimeZoneInfo.ConvertTimeFromUtc(
                DateTime.UtcNow, ZoneInfo);
        }
    }

    public override DateTime FromSystemTime(DateTime systemTime)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(systemTime, ZoneInfo);
    }
}
