using System;
using TimeZoneConverter;

namespace ASPCore.AllThatBTS.Crawler.Common
{
    public static class CommonHelper
    {
        /// <summary>
        /// 서버 시간 대신 KST로 DateTime 조회
        /// </summary>
        public static DateTime GetDateTimeNow
        {
            get
            {
                return TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now, TZConvert.GetTimeZoneInfo("Korea Standard Time"));
            }
        }

        public static DateTime GetDateTime(DateTime targetDateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(targetDateTime, TZConvert.GetTimeZoneInfo("Korea Standard Time"));
        }
    }
}
