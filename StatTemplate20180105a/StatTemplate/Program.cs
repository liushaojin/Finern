using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace StatTemplate
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                string err = "";

                RunStatus test = new RunStatus();//实例化一下，执行构造函数便于成员变量的赋值
                RunStatus.DBManager = new SQLiteHelper();
                if (!RunStatus.DBManager.Connection(RunStatus.DBSource, ref err))
                {
                    MessageBox.Show("连接local数据库失败! 错误信息: " + err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //FrmLogin fl = new FrmLogin();
                //fl.ShowDialog();
                //if (!fl.Logined) return;

                Application.Run(new FrmMain());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "未知错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
