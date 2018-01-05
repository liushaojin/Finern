using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BlueWay_Shangliao.Data
{
    class MysqlHelper
    {
        //数据库连接字符串
        public static string Conn = "Database='blueway';Data Source='localhost';User Id='root';Password='finern';charset='utf8';pooling=true";
        
        // 用于缓存参数的HASH表
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());
        
        /// <summary>
        ///  给定连接的数据库用假设参数执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            
            using(MySqlConnection conn = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }
        
        /// <summary>
        /// 用现有的数据库连接执行一个sql命令（不返回数据集）
        /// </summary>
        /// <param name="connection">一个现有的数据库连接</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        
        /// <summary>
        ///使用现有的SQL事务执行一个sql命令（不返回数据集）
        /// </summary>
        /// <remarks>
        ///举例:
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">一个现有的事务</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>执行命令所影响的行数</returns>
        public static int ExecuteNonQuery(MySqlTransaction trans, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        
        /// <summary>
        /// 用执行的数据库连接执行一个返回数据集的sql命令
        /// </summary>
        /// <remarks>
        /// 举例:
        ///  MySqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>包含结果的读取器</returns>
        public static MySqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            //创建一个MySqlCommand对象
            MySqlCommand cmd = new MySqlCommand();
            //创建一个MySqlConnection对象
            MySqlConnection conn = new MySqlConnection(connectionString);
            
            //在这里我们用一个try/catch结构执行sql文本命令/存储过程，因为如果这个方法产生一个异常我们要关闭连接，因为没有读取器存在，
            //因此commandBehaviour.CloseConnection 就不会执行
            try
            {
                //调用 PrepareCommand 方法，对 MySqlCommand 对象设置参数
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                //调用 MySqlCommand  的 ExecuteReader 方法
                MySqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //清除参数
                cmd.Parameters.Clear();
                return reader;
            }
            catch
            {
                //关闭连接，抛出异常
                conn.Close();
                throw;
            }
        }
        
        /// <summary>
        /// 返回DataSet
        /// </summary>
        /// <param name="connectionString">一个有效的连接字符串</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns></returns>
        public static DataSet GetDataSet(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            //创建一个MySqlCommand对象
            MySqlCommand cmd = new MySqlCommand();
            //创建一个MySqlConnection对象
            MySqlConnection conn = new MySqlConnection(connectionString);
            
            //在这里我们用一个try/catch结构执行sql文本命令/存储过程，因为如果这个方法产生一个异常我们要关闭连接，因为没有读取器存在，
            
            try
            {
                //调用 PrepareCommand 方法，对 MySqlCommand 对象设置参数
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                //调用 MySqlCommand  的 ExecuteReader 方法
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                //清除参数
                cmd.Parameters.Clear();
                conn.Close();
                return ds;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        
        
        
        /// <summary>
        /// 用指定的数据库连接字符串执行一个命令并返回一个数据集的第一列
        /// </summary>
        /// <remarks>
        ///例如:
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        ///<param name="connectionString">一个有效的连接字符串</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            
            using(MySqlConnection connection = new MySqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }
        
        /// <summary>
        /// 用指定的数据库连接执行一个命令并返回一个数据集的第一列
        /// </summary>
        /// <remarks>
        /// 例如:
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connection">一个存在的数据库连接</param>
        /// <param name="cmdType">命令类型(存储过程, 文本, 等等)</param>
        /// <param name="cmdText">存储过程名称或者sql命令语句</param>
        /// <param name="commandParameters">执行命令所用参数的集合</param>
        /// <returns>用 Convert.To{Type}把类型转换为想要的 </returns>
        public static object ExecuteScalar(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }
        
        /// <summary>
        /// 将参数集合添加到缓存
        /// </summary>
        /// <param name="cacheKey">添加到缓存的变量</param>
        /// <param name="commandParameters">一个将要添加到缓存的sql参数集合</param>
        public static void CacheParameters(string cacheKey, params MySqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }
        
        /// <summary>
        /// 找回缓存参数集合
        /// </summary>
        /// <param name="cacheKey">用于找回参数的关键字</param>
        /// <returns>缓存的参数集合</returns>
        public static MySqlParameter[] GetCachedParameters(string cacheKey)
        {
            MySqlParameter[] cachedParms = (MySqlParameter[])parmCache[cacheKey];
            
            if(cachedParms == null)
            {
                return null;
            }
            
            MySqlParameter[] clonedParms = new MySqlParameter[cachedParms.Length];
            
            for(int i = 0, j = cachedParms.Length; i < j; i++)
            {
                clonedParms[i] = (MySqlParameter)((ICloneable)cachedParms[i]).Clone();
            }
            
            return clonedParms;
        }
        
        /// <summary>
        /// 准备执行一个命令
        /// </summary>
        /// <param name="cmd">sql命令</param>
        /// <param name="conn">OleDb连接</param>
        /// <param name="trans">OleDb事务</param>
        /// <param name="cmdType">命令类型例如 存储过程或者文本</param>
        /// <param name="cmdText">命令文本,例如:Select * from Products</param>
        /// <param name="cmdParms">执行命令的参数</param>
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] cmdParms)
        {
            if(conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            
            if(trans != null)
            {
                cmd.Transaction = trans;
            }
            
            cmd.CommandType = cmdType;
            
            if(cmdParms != null)
            {
                foreach(MySqlParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
        }
        
        
        #region 成员变量区
        string server = "localhost";
        string userid = "admin";
        string password = "admin";
        string database = "blueway";
        string port = "3306";
        string charset = "utf-8";
        string connectStr = "Database='blueway';Data Source='localhost';User Id='root';Password='finern';charset='utf8';pooling=true";
        MySqlConnection mysqlConnect = null;
        #endregion
        
        #region 单例模式
        private static MysqlHelper mysqlInstance = null;
        private MysqlHelper() { }
        public static MysqlHelper GetMysqlInstance()
        {
            if(mysqlInstance == null)
            {
                mysqlInstance = new MysqlHelper();
            }
            
            return mysqlInstance;
        }
        #endregion
        
        public void SetServer(string server)
        {
            this.server = server;
        }
        
        public void SetUserID(string userid)
        {
            this.userid = userid;
        }
        
        public void SetDataBase(string database)
        {
            this.database = database;
        }
        
        public void SetPassword(string password)
        {
            this.password = password;
        }
        public void SetPort(string port)
        {
            this.port = port;
        }
        public void SetCharset(string charset)
        {
            this.charset = charset;
        }
        public void SetCommInfo(string server, string user, string pass)
        {
            this.server = server;
            this.userid = user;
            this.password = pass;
        }
        
        #region  建立MySql数据库连接
        /// <summary>
        /// 建立数据库连接.
        /// </summary>
        /// <returns>返回MySqlConnection对象</returns>
        private MySqlConnection GetMysqlConnection()
        {
            //string sqlcon = string.Format("server={0};user id={1};password={2};database={3};port={4};Charset={5}", server, userid, password, database, port, charset);
            string connectStr = string.Format("Database='{0}';Data Source='{1}';User Id='{2}';Password='{3}';charset='utf8';pooling=true", database, server, userid, password);
            //sqlcon = "Database='blueway';Data Source='localhost';User Id='root';Password='finern';charset='utf8';pooling=true";
            MySqlConnection myCon = new MySqlConnection(connectStr);
            return myCon;
        }
        /// <summary>
        /// 打开数据库
        /// </summary>
        /// <returns></returns>
        public bool OpenMysql()
        {
            bool res = true;
            
            try
            {
                mysqlConnect = this.GetMysqlConnection();
                mysqlConnect.Open();
            }
            catch
            {
                res = false;
            }
            
            return res;
        }
        public void CloseMysql()
        {
            mysqlConnect.Close();
            mysqlConnect.Dispose();
        }
        #endregion
        
        #region  执行MySqlCommand命令
        /// <summary>
        /// 执行MySqlCommand
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        public void DoUpdate(string sqlstr)
        {
            MySqlConnection mysqlcon = this.GetMysqlConnection();
            mysqlcon.Open();
            MySqlCommand mysqlcom = new MySqlCommand(sqlstr, mysqlcon);
            mysqlcom.ExecuteNonQuery();
            mysqlcom.Dispose();
            mysqlcon.Close();
            mysqlcon.Dispose();
        }
        #endregion
        
        #region  创建MySqlDataReader对象
        /// <summary>
        /// 创建一个MySqlDataReader对象
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        /// <returns>返回MySqlDataReader对象</returns>
        public MySqlDataReader DoQuery(string sqlstr)
        {
            Console.WriteLine(sqlstr);
            MySqlConnection mysqlcon = this.GetMysqlConnection();
            MySqlCommand mysqlcom = new MySqlCommand(sqlstr, mysqlcon);
            mysqlcon.Open();
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            return mysqlread;
        }
        /// <summary>
        /// 查询一行数据
        /// </summary>
        /// <param name="M_str_sqlstr"></param>
        /// <returns></returns>
        public string QueryOneRow(string sqlstr)
        {
            string res = string.Empty;
            MySqlCommand mysqlcom = new MySqlCommand(sqlstr, mysqlConnect);
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            
            while(mysqlread.HasRows)
            {
                if(mysqlread.Read())
                {
                    int feildN = mysqlread.FieldCount;
                    res = mysqlread["name"].ToString();
                    res += ',';
                    res += mysqlread["pass"].ToString();
                }
                else
                {
                    break;
                }
            }
            
            mysqlread.Close();
            return res;
        }
        #endregion
    }
}
