using System.Data.SqlClient;

namespace BakWin
{
    public partial class ConnectionFactory
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public static readonly string connStr = System.Configuration.ConfigurationManager.AppSettings["conn"];

        /// <summary>
        /// 获取Connection对象
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connStr);
        }
    }
}
