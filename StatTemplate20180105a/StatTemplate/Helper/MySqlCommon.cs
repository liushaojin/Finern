using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using MySql.Data.MySqlClient;

namespace StatTemplate
{
    public class MySqlCommon
    {
        string m_CnString = "";
        string m_IPAddress = "";
        string m_Database = "";
        string m_User = "";
        string m_Password = "";
        string m_pooling = "";

        object m_op_lock = new object();

        #region 属性
        /// <summary>
        /// 数据库所在计算机的ip地址
        /// </summary>
        public string IPAddress
        {
            get { return m_IPAddress; }
            set { m_IPAddress = value; }
        }

        /// <summary>
        /// 数据库名称
        /// </summary>
        public string Database
        {
            get { return m_Database; }
            set { m_Database = value; }
        }

        /// <summary>
        /// 登录用户
        /// </summary>
        public string User
        {
            get { return m_User; }
            set { m_User = value; }
        }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password
        {
            get { return m_Password; }
            set { m_Password = value; }
        }

        /// <summary>
        /// 连接池
        /// </summary>
        public string Pooling
        {
            get { return m_pooling; }
            set { m_pooling = value; }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 打开数据库然后关闭，用于测试数据库连接
        /// </summary>
        /// <param name="err"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public bool TestLink(ref string err, int timeout = 200)
        {
            err = "";
            bool flag = false;
            Monitor.TryEnter(m_op_lock, timeout, ref flag);
            if (!flag) { err = "timeout"; return false; }
            
            MySqlConnection cn = null;

            try
            {
                err = "";
                m_CnString = String.Format("server={0};user id={1}; password={2}; database={3}; pooling={4}",
                                        m_IPAddress,
                                        m_User,
                                        m_Password,
                                        m_Database,
                                        m_pooling);
                cn = new MySqlConnection(m_CnString);
                cn.Open();
                cn.Close();

                return true;
            }
            catch (MySqlException ex)
            {
                err = ex.Message;
                return false;
            }
            finally
            {
                Monitor.Exit(m_op_lock);
                if (cn != null)
                {
                    cn.Dispose();
                    cn = null;
                }
            }
        }

        public bool ExecNonQuery(string pCmd, ref string err, int timeout = -1)
        {
            err = "";
            bool flag = false;
            Monitor.TryEnter(m_op_lock, timeout, ref flag);
            if (!flag) { err = "timeout"; return false; }

            MySqlConnection cn = null;
            MySqlCommand sqlcmd = null;

            try
            {
                err = "";
                m_CnString = String.Format("server={0};user id={1}; password={2}; database={3}; pooling={4}",
                                        m_IPAddress,
                                        m_User,
                                        m_Password,
                                        m_Database,
                                        m_pooling);
                cn = new MySqlConnection(m_CnString);
                cn.Open();

                sqlcmd = new MySqlCommand(pCmd, cn);
                sqlcmd.ExecuteNonQuery();

                return true;
            }
            catch (MySqlException ex)
            {
                err = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
            finally
            {
                Monitor.Exit(m_op_lock);
                if (cn != null)
                {
                    cn.Close();
                    cn.Dispose();
                    cn = null;
                }
            }
        }

        public bool GetData(string pSqlcmd, ref DataTable pDt, ref string err, int timeout = -1)
        {
            err = "";
            bool flag = false;
            Monitor.TryEnter(m_op_lock, timeout, ref flag);
            if (!flag) { err = "timeout"; return false; }

            MySqlConnection cn = null;
            MySqlDataAdapter da = null;
            DataSet ds = null;
            DataTable dt = null;
            MySqlCommandBuilder cb = null;

            try
            {
                cn = new MySqlConnection();
                da = new MySqlDataAdapter();
                ds = new DataSet();

                cn.ConnectionString = m_CnString;
                da.SelectCommand = new MySqlCommand(pSqlcmd, cn);
                cb = new MySqlCommandBuilder(da);
                cn.Open();
                da.Fill(ds);
                dt = ds.Tables[0];

                pDt = dt;

                return true;
            }
            catch (MySqlException ex)
            {
                pDt = null;
                err = ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                pDt = null;
                err = ex.Message;
                return false;
            }
            finally
            {
                Monitor.Exit(m_op_lock);
                if (cn != null)
                {
                    cn.Close();
                    cn.Dispose();
                    cn = null;
                }
                if(da!=null) da.Dispose();
                if(ds!=null) ds.Dispose();
                if(dt!=null) dt.Dispose();
                if(cb!=null) cb.Dispose();
            }
        }

        /// <summary>
        /// 将一个对象序列化（对象必须先声明为可序列化）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pSerailObject"></param>
        /// <param name="obj"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool Serialization<T>(ref string pSerailObject, T obj, out string err)
        {
            err = "";
            BinaryFormatter serialzer = null;
            System.IO.MemoryStream stream = null;

            try
            {
                Type type = obj.GetType();

                serialzer = new BinaryFormatter();
                stream = new System.IO.MemoryStream();
                serialzer.Serialize(stream, obj);

                stream.Position = 0;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Flush();
                stream.Close();
                pSerailObject = Convert.ToBase64String(buffer);

                return true;
            }
            catch (Exception ex)
            {
                err = "序列化失败: \n"+ex.Message;
                return false;
            }
            finally
            {
                if (serialzer != null) serialzer = null;
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                    stream = null;
                }
            }
        }

        /// <summary>
        /// 反序列化一个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pSerailObject"></param>
        /// <param name="obj"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool Deserialization<T>(string pSerailObject, ref T obj, ref string err)
        {
            err = "";
            IFormatter formatter = null;
            System.IO.MemoryStream stream = null;

            try
            {
                obj = default(T);
                formatter = new BinaryFormatter();
                byte[] buffer = Convert.FromBase64String(pSerailObject);
                stream = new System.IO.MemoryStream(buffer);
                obj = (T)formatter.Deserialize(stream);
                stream.Flush();
                stream.Close();

                return true;
            }
            catch (Exception ex)
            {
                err = "反序列化失败: \n" + ex.Message;
                return false;
            }
            finally
            {
                if (formatter != null) formatter = null;
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                    stream = null;
                }
            }
        }
        #endregion
    }
}
