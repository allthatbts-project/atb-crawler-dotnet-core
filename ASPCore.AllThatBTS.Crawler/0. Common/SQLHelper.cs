using System.IO;

namespace ASPCore.AllThatBTS.Crawler.Common
{
    public static class SQLHelper
    {
        public static string GetSqlByMethodName(string methodName)
        {
            string sql = string.Empty;
            string currentDirectory = Directory.GetCurrentDirectory();

            //Path.Combine은 Linux 환경에서도 사용이 가능!
            string path = Path.Combine(Directory.GetCurrentDirectory(), "SQL", methodName + ".sql");
            if (File.Exists(path))
            {
                sql = File.ReadAllText(path);
            }
            
            return sql;
        }
    }
}
