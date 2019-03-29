using ASPCore.AllThatBTS.Crawler.Common;
using ASPCore.AllThatBTS.Crawler.Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ASPCore.AllThatBTS.Crawler.Repository
{
    public class InstagramRepository : BaseRepository
    {
        public int UpsertInstagramData(InstagramT InstagramData)
        {
            string sql = SQLHelper.GetSqlByMethodName(MethodBase.GetCurrentMethod().Name);

            var parameters = new
            {
                INSTAGRAM_ID = InstagramData.InstagramId,
                USER_NAME = InstagramData.UserName,
                IMAGE_URL = InstagramData.ImageUrl,
                URL = InstagramData.Url
            };

            return Connection.Execute(sql, parameters);
        }
    }
}
