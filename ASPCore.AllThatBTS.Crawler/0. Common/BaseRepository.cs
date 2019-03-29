using NPoco;

namespace ASPCore.AllThatBTS.Crawler.Common
{
    public class BaseRepository
    {
        public IDatabase Connection
        {
            get
            {
                return new Database(AppConfiguration.SqlDataConnection,
                                DatabaseType.MySQL,
                                MySql.Data.MySqlClient.MySqlClientFactory.Instance);
            }
        }
    }
}