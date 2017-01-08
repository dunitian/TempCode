using System.Data;
using System.Data.SqlClient;

namespace BakWin
{
    public partial class SQLHelper
    {
        /// <summary>
        /// 执行非查询SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteNoQuery(SqlConnection conn, string sql)
        {
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 返回单条记录
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(SqlConnection conn, string sql)
        {
            var dt = new DataTable();
            var adp = new SqlDataAdapter(sql, conn);
            adp.Fill(dt);
            return dt;
        }
    }
}
