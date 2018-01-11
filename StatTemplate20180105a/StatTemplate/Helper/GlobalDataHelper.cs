using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace StatTemplate
{
    public class GlobalDataHelper:MySqlCommon
    {
        public GlobalDataHelper()
        {
            base.Database = "pack_database";
            base.User = "marineou";
            base.Password = "Finern@0644";
        }

        private class TableName
        {
            /// <summary>
            /// "用户"表名
            /// </summary>
            public const string TABLE_USER = "tb_user";
            /// <summary>
            /// "流程图"表名
            /// </summary>
            public const string TABLE_ROUTINGCHART = "RoutingChart";
            /// <summary>
            /// "生产线"信息表名
            /// </summary>
            public const string TABLE_PRODUCTIONLINEINFO = "productionlineinfo";
        }
       
        #region "用户"操作
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="pUser">用户名</param>
        /// <param name="pSecretCode">密码</param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool Login(string pUser, string pSecretCode, ref string err, ref int pRole)
        {
            try
            {
                err = "";
                string sqlcmd = "";
                DataTable dt = null;

                sqlcmd = "select * from " + TableName.TABLE_USER + " where username='" + pUser + "'";
                if (!GetData(sqlcmd, ref dt, ref err)) return false;

                if (dt == null || dt.Rows.Count != 1) 
                {
                    err = "系统中不存在用户名 ["+pUser+"].";
                    return false;
                }

                sqlcmd = "select role from " + TableName.TABLE_USER + " where username='" + pUser + "' and secretcode='" + pSecretCode + "';";
                if (!GetData(sqlcmd, ref dt, ref err)) return false;

                if (dt == null || dt.Rows.Count != 1)
                {
                    err = "密码错误.";
                    return false;
                }

                pRole = (int)dt.Rows[0]["role"];

                string ip4 = "";
                GetIP4Address(ref ip4, ref err);
                sqlcmd = "update " + TableName.TABLE_USER + " set logintime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', loginip='" + ip4 + "' where username='" + pUser + "';";
                if (!ExecNonQuery(sqlcmd, ref err)) return false;

                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 增加用户
        /// </summary>
        /// <param name="pUser"></param>
        /// <param name="pSecretCode"></param>
        /// <param name="pPurview"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool AddUser(string pUser, string pSecretCode, int pPurview, ref string err)
        {
            try
            {
                err = "";
                string sqlcmd = "";
                DataTable dt = null;

                if (pUser.Trim() == "")
                {
                    err = "不能添加用户名为空的用户";
                    return false;
                }

                if (pSecretCode.Trim() == "")
                {
                    err = "不能添加密码为空的用户";
                    return false;
                }

                sqlcmd = "select * from tb_user where username='" + pUser + "';";
                if (!GetData(sqlcmd, ref dt, ref err)) return false;

                if (dt.Rows.Count > 0)
                {
                    err = "用户" + pUser +"已经存在";
                    return false;
                }

                sqlcmd = "insert into " + TableName.TABLE_USER + " (username,secretcode,role,logintime,loginip,createtime) values " +
                    "('" + pUser + "'," +
                    "'" + pSecretCode + "'," +
                    pPurview.ToString() + "," +
                    "'"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"',"+
                    "''," +
                    "'"+ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"')";
                if (!ExecNonQuery(sqlcmd, ref err)) return false;

                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="pUser"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool DelUser(string pUser, ref string err)
        {
            try
            {
                err = "";
                string sqlcmd = "";

                sqlcmd = "delete from " + TableName.TABLE_USER + " where username='" + pUser + "';";
                if (!ExecNonQuery(sqlcmd, ref err)) return false;

                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 根据用户名，获取用户密码、权限
        /// </summary>
        /// <param name="pUser"></param>
        /// <param name="pSecretCode"></param>
        /// <param name="pRole"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool GetUser(string pUser, ref string pSecretCode, ref int pRole, ref string err)
        {
            try 
	        {	        
		        err = "";
                string sqlcmd = "";
                DataTable dt = null;

                sqlcmd = "select secretcode, role from " + TableName.TABLE_USER + " where username='" + pUser + "';";
                if (!GetData(sqlcmd, ref dt, ref err)) return false;
                if (dt == null || dt.Rows.Count == 0)
                {
                    err = "没有查找到用户信息.";
                    return false;
                }

                if (dt.Rows.Count != 1)
                {
                    err = "查询到的记录数量不为1.";
                    return false;
                }

                pSecretCode = dt.Rows[0]["secretcode"].ToString();
                pRole = (int)dt.Rows[0]["role"];

                return true;
	        }
	        catch (Exception ex)
	        {
		        err = ex.Message;
		        return false;
	        }
        }

        /// <summary>
        /// 根据用户名，保存密码和权限
        /// </summary>
        /// <param name="pUser"></param>
        /// <param name="pSecretCode"></param>
        /// <param name="pRole"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool UpdUser(string pUser, string pSecretCode, int pRole, ref string err)
        {
            try
            {
                err = "";
                string sqlcmd = "";

                sqlcmd = "update " + TableName.TABLE_USER + " set secretcode='" + pSecretCode + "', role=" + pRole.ToString() + " where username='" + pUser + "';";
                if (!ExecNonQuery(sqlcmd, ref err)) return false;

                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <param name="pUsers"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool GetUsers(ref string[] pUsers, ref string[] pSecretCodes, ref int[] pRoles, ref string err)
        {
            try
            {
                err = "";
                pUsers = null;
                pSecretCodes = null;
                pRoles = null;

                string sqlcmd = "";
                DataTable dt = null;
                sqlcmd = "select username, secretcode, role from " + TableName.TABLE_USER + ";";
                if (!GetData(sqlcmd, ref dt, ref err)) return false;

                if (dt == null || dt.Rows.Count==0)
                {
                    err = "数据库中没有用户.";
                    return false;
                }

                pUsers = new string[dt.Rows.Count];
                pRoles = new int[dt.Rows.Count];
                pSecretCodes = new string[dt.Rows.Count];
                for (int count = 0; count < dt.Rows.Count; count++)
                {
                    pUsers[count] = dt.Rows[count]["username"].ToString();
                    pRoles[count] = (int)dt.Rows[count]["role"];
                    pSecretCodes[count] = dt.Rows[count]["secretcode"].ToString();
                }

                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        public enum Role { 操作员=0,工程师,管理者,开发者}
        /// <summary>
        /// 根据用户名，获取用户权限
        /// </summary>
        /// <param name="pUser"></param>
        /// <param name="pRole"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool GetRole(string pUser, ref int pRole, ref string err)
        {
            try
            {
                err = "";

                string sqlcmd = "";
                DataTable dt = null;
                sqlcmd = "select role from " + TableName.TABLE_USER + " where username='" + pUser + "'";
                if (!GetData(sqlcmd, ref dt, ref err)) return false;

                if (dt == null || dt.Rows.Count != 1)
                {
                    err = "记录为null或不唯一.";
                    return false;
                }

                pRole = (int)dt.Rows[0]["role"];
                
                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 添加一个用户
        /// </summary>
        /// <param name="pUser"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool AddUser(User pUser, ref string err)
        {
            try
            {
                err = "";
                string sqlcmd = "";
                DataTable dt = null;

                if (pUser.username.Trim() == "")
                {
                    err = "不能添加用户名为空的用户";
                    return false;
                }

                if (pUser.secretcode.Trim() == "")
                {
                    err = "不能添加密码为空的用户";
                    return false;
                }

                sqlcmd = "select * from tb_user where username='" + pUser + "';";
                if (!GetData(sqlcmd, ref dt, ref err)) return false;

                if (dt.Rows.Count > 0)
                {
                    err = "用户" + pUser + "已经存在";
                    return false;
                }

                sqlcmd = "insert into " + TableName.TABLE_USER + " (username,secretcode,role,logintime,loginip,createtime) values " +
                    "('" + pUser + "'," +
                    "'" + pUser.secretcode + "'," +
                    pUser.role.ToString() + "," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                    "''," +
                    "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                if (!ExecNonQuery(sqlcmd, ref err)) return false;

                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        public bool GetUser(string pName, ref User pUser, ref string err)
        {
            try
            {
                err = "";
                pUser = null;
                string sqlcmd = "";
                DataTable dt = null;

                sqlcmd = "select secretcode, role from " + TableName.TABLE_USER + " where username='" + pName + "';";
                if (!GetData(sqlcmd, ref dt, ref err)) return false;
                if (dt == null || dt.Rows.Count == 0)
                {
                    err = "没有查找到用户信息.";
                    return false;
                }

                if (dt.Rows.Count != 1)
                {
                    err = "查询到的记录数量不为1.";
                    return false;
                }

                pUser = new User();

                pUser.username = pName;
                pUser.secretcode = dt.Rows[0]["secretcode"].ToString();
                pUser.role = (int)dt.Rows[0]["role"];
                pUser.logintime = dt.Rows[0]["logintime"].ToString();
                pUser.loginip = dt.Rows[0]["loginip"].ToString();
                pUser.createtime = dt.Rows[0]["createtime"].ToString();

                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        public bool GetUsers(User[] pUsers, ref string err)
        {
            try
            {
                err = "";
                pUsers = null;

                string sqlcmd = "";
                DataTable dt = null;
                sqlcmd = "select username, secretcode, role from " + TableName.TABLE_USER + ";";
                if (!GetData(sqlcmd, ref dt, ref err)) return false;

                if (dt == null || dt.Rows.Count == 0)
                {
                    err = "数据库中没有用户.";
                    return false;
                }

                pUsers = new User[dt.Rows.Count];
                for (int count = 0; count < dt.Rows.Count; count++)
                {
                    pUsers[count].username = dt.Rows[count]["username"].ToString();
                    pUsers[count].secretcode = dt.Rows[count]["secretcode"].ToString();
                    pUsers[count].role = (int)dt.Rows[count]["role"];
                    pUsers[count].logintime = dt.Rows[count]["logintime"].ToString();
                    pUsers[count].loginip = dt.Rows[count]["loginip"].ToString();
                    pUsers[count].createtime = dt.Rows[count]["createtime"].ToString();
                }

                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }
        #endregion

        /// <summary>
        /// 获取IP4地址
        /// </summary>
        /// <param name="pIP4"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        private bool GetIP4Address(ref string pIP4, ref string err)
        {
            try
            {
                pIP4 = "";
                err = "";
                string hostName = System.Net.Dns.GetHostName();   //获取本机名
                System.Net.IPAddress[] addrs = System.Net.Dns.GetHostAddresses(hostName);
                //IPHostEntry localhost = Dns.GetHostEntry(hostName);   //获取IPv6地址
                for (int count = 0; count < addrs.Length; count++)
                {
                    //取第一个ip4地址
                    if (addrs[count].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        pIP4 = addrs[count].ToString();
                        return true;
                    }
                }

                err = "";
                return false;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }
    }
}
