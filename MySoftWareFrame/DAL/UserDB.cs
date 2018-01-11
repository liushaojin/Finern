using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Model;
using System.Configuration;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;


namespace DAL
{
    public class UserDB
    {
        static string server = "localhost";
        static string userid = "admin";
        static string password = "admin";
        static string database = "blueway";
        static string port = "3306";
        static string charset = "utf-8";
        static string connectStr = string.Format("Database='{0}';Data Source='{1}';User Id='{2}';Password='{3}';charset='utf8';pooling=true", database, server, userid, password);
        MysqlHelper mysql = MysqlHelper.GetMysqlInstance();
        
        /// <summary>
        /// 查找数据库中是否有相同的用户名
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool IsEqual(UserInfo userInfo)
        {
            bool res = false;
            string commandText = "select * from UserInfo where userName=?userName;";
            MySqlParameter paras = new MySqlParameter("?userName", userInfo.Username);
            MySqlDataReader mysqlread = MysqlHelper.ExecuteReader(connectStr, CommandType.Text, commandText, paras);//mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            
            while(mysqlread.HasRows)
            {
                if(mysqlread.Read())
                {
                    int feildNum = mysqlread.FieldCount;
                    res = true;//mysqlread["username"].ToString();
                }
                else
                {
                    break;
                }
            }
            
            return res;
        }
        
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public int AddUser(UserInfo userInfo)
        {
            //对数据库进添加一个用户操作
            string commandText = "insert into UserInfo(userName, passWord, permision) values(?userName, ?passWord, ?permision)";
            MySqlParameter[] paras = new MySqlParameter[]
            {
                new MySqlParameter("?userName", userInfo.Username),
                new MySqlParameter("?passWord", userInfo.Password),
                new MySqlParameter("?permision", userInfo.Permision)
            };
            return MysqlHelper.ExecuteNonQuery(connectStr, CommandType.Text, commandText, paras);
        }
        //添加其他对数据库操作
    }
}
