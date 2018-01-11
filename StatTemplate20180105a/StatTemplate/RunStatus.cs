using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatTemplate
{
    public class RunStatus
    {
        public RunStatus()
        {
            GDHelper = new GlobalDataHelper();
        }

        public const string DBSource = "sys.db";
        public static SQLiteHelper DBManager = null;
        public static GlobalDataHelper GDHelper = null;

        /// <summary>
        /// 当前登录的用户名
        /// </summary>
        public static string User = "";
        /// <summary>
        /// 当前登录的用户权限
        /// </summary>
        public static int Role = 0;
        /// <summary>
        /// 扫描的次数
        /// </summary>
        public static int ScanCount = 0;

        
    }
}
