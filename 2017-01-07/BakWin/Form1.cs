using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace BakWin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 当前文件夹路径
        /// </summary>
        private static string path = Application.StartupPath;
        /// <summary>
        /// 备份路径列表
        /// </summary>
        private List<string> pathList = new List<string>();

        #region 备份文件
        /// <summary>
        /// 获取文件完整路径
        /// </summary>
        /// <returns></returns>
        private string GetFilePath()
        {
            string filePath = $"{path + "\\" + DateTime.Now.ToString("yyy-MM-dd hh.mm.ss") + ".bak"}";
            if (File.Exists(filePath))
            {
                filePath = $"{Guid.NewGuid()}.bak";
            }
            pathList.Add(filePath);
            return filePath;
        }

        /// <summary>
        /// 完整备份-默认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = ConnectionFactory.GetConnection())
                {
                    int i = SQLHelper.ExecuteNoQuery(conn, $"backup database MyBlog to disk=N'{GetFilePath()}'");
                    if (i != 0) { MessageBox.Show("备份成功！"); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 完整备份-压缩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn2_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = ConnectionFactory.GetConnection())
                {
                    int i = SQLHelper.ExecuteNoQuery(conn, $"backup database MyBlog to disk=N'{GetFilePath()}' with compression");
                    if (i != 0) { MessageBox.Show("备份成功！"); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 备份是否有效
        /// <summary>
        /// 备份是否有效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn4_Click(object sender, EventArgs e)
        {
            if (pathList == null || pathList.Count == 0)
            {
                MessageBox.Show("备份文件不能为空");
                return;
            }
            try
            {
                using (var conn = ConnectionFactory.GetConnection())
                {
                    //conn.StatisticsEnabled = true;
                    conn.InfoMessage += Conn_InfoMessage;//获取数据库提示
                    foreach (var item in pathList)
                    {
                        if (!File.Exists(item))
                        {
                            pathList.Remove(item);
                            continue;
                        }
                        SQLHelper.ExecuteNoQuery(conn, $"restore verifyonly from disk=N'{item}'");
                    }
                    //var dic = conn.RetrieveStatistics();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Conn_InfoMessage(object sender, System.Data.SqlClient.SqlInfoMessageEventArgs e)
        {
            MessageBox.Show(e.Message);
        }
        #endregion

        #region 备份信息
        /// <summary>
        /// 备份了啥文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn5_Click(object sender, EventArgs e)
        {
            CheckDBBak("restore filelistonly");
        }

        /// <summary>
        /// 备份文件信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn6_Click(object sender, EventArgs e)
        {
            CheckDBBak("restore headeronly");
        }

        /// <summary>
        /// 备份检查
        /// </summary>
        /// <param name="sql"></param>
        private void CheckDBBak(string sql)
        {
            if (pathList == null || pathList.Count == 0)
            {
                MessageBox.Show("备份文件不能为空");
                return;
            }
            try
            {
                using (var conn = ConnectionFactory.GetConnection())
                {
                    foreach (var item in pathList)
                    {
                        if (!File.Exists(item))
                        {
                            pathList.Remove(item);
                            continue;
                        }
                        var dt = SQLHelper.GetDataTable(conn, $"{sql} from disk=N'{item}'");
                        var form = new DTForm(dt);
                        form.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// 备份文件-还原
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn3_Click(object sender, EventArgs e)
        {
            #region 核心代码
            //using (var conn = ConnectionFactory.GetConnection(DBEnum.CommonDB))
            //{
            //    conn.InfoMessage += Conn_InfoMessage;//获取数据库提示
            //    SQLHelper.ExecuteNoQuery(conn, @"restore database MyBlog from disk=N'E:\Github\TempCode\2017-01-07\BakWin\bin\Debug\2017-01-08 02.11.22.bak'");
            //} 
            #endregion

            var task = System.Threading.Tasks.Task.Run(() => ReconverDB());
            MessageBox.Show("异步执行中，稍等片刻");
            task.ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    var ex = t.Exception.GetBaseException();
                    MessageBox.Show(ex.Message);
                }
            });
        }
        /// <summary>
        /// 还原数据库
        /// </summary>
        private void ReconverDB()
        {
            if (pathList == null || pathList.Count == 0)
            {
                MessageBox.Show("备份文件不能为空");
                return;
            }

            //还原的时候得切换数据库（不一定是Master）
            using (var conn = ConnectionFactory.GetConnection())
            {
                conn.InfoMessage += Conn_InfoMessage;//获取数据库提示
                foreach (var item in pathList)
                {
                    if (!File.Exists(item))
                    {
                        pathList.Remove(item);
                        continue;
                    }
                    SQLHelper.ExecuteNoQuery(conn, "use master if(Exists(select * from sys.databases where name=N'MyBlog')) drop database MyBlog");
                    SQLHelper.ExecuteNoQuery(conn, $"use master restore database MyBlog from disk=N'{item}'");
                    break;//一个就够了
                }
            }
        }
    }
}
