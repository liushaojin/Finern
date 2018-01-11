using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace StatTemplate
{
    public class SQLiteHelper
    {
        /// <summary>
        /// 默认数据库名称
        /// </summary>
        string dbConnection = "Data Source=Data.db";
        SQLiteConnection cn = null;

        public SQLiteHelper()
        {
        
        }

        public bool Connection(string datasource, ref string err)
        {
            try
            {
                dbConnection = String.Format("Data Source={0}", datasource);
                cn = new SQLiteConnection(dbConnection);

                cn.Open(); 

                return true;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("试图加载格式不正确的程序"))
                    err = ex.Message + "...请查看项目属性-生成-目标平台，确保目标平台与引用的System.Data.SQLite.dll版本一致";
                else
                    err = ex.Message;
                return false;
            }
        }

        public void Close()
        {
            if (cn != null) cn.Dispose();

            cn = null;
        }

        public bool GetData(string sql, ref DataTable dt, ref string err)
        {
            err = "";
            SQLiteDataReader reader = null;
            SQLiteCommand sqlcmd = null;

            try
            {
                sqlcmd = new SQLiteCommand(cn);
                sqlcmd.CommandText = sql;
                reader = sqlcmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(reader);

                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
            finally
            {
                if (reader != null) reader.Dispose();

                reader = null;
                sqlcmd = null;
            }
        }

        public bool GetData(string sql, IList<SQLiteParameter> cmdparams, ref DataTable dt, ref string err)
        {
            dt = new DataTable();
            SQLiteCommand sqlcmd = null;
            SQLiteDataReader reader = null;

            try
            {
                sqlcmd = new SQLiteCommand(cn);
                sqlcmd.CommandText = sql;
                sqlcmd.Parameters.AddRange(cmdparams.ToArray());
                sqlcmd.CommandTimeout = 180;
                reader = sqlcmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(reader);

                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
            finally
            {
                if (reader != null) reader.Close();
                reader.Dispose();

                sqlcmd = null;
                reader = null;
            }
        }

        public bool ExecuteNonQuery(string sql, ref string err)
        {
            SQLiteCommand sqlcmd = null;
            SQLiteTransaction mytrans = null;

            using (mytrans = cn.BeginTransaction())
            {
                sqlcmd = new SQLiteCommand(sql, cn);
                try
                {
                    sqlcmd.CommandTimeout = 180;
                    sqlcmd.ExecuteNonQuery();
                    mytrans.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                    mytrans.Rollback();
                    return false;
                }
                finally
                {
                    sqlcmd.Dispose();
                    sqlcmd = null;
                }
            }
        }

        public bool ExecuteNonQuery(System.Collections.ArrayList sqlcmdset, ref string err)
        {
            SQLiteCommand sqlcmd = null;
            SQLiteTransaction mytrans = null;

            string sql = "";
            for (int count = 0; count < sqlcmdset.Count; count++)
            {
                if (!sqlcmdset[count].ToString().Contains(";"))
                    sql += sqlcmdset[count].ToString() + ";";
                else
                    sql += sqlcmdset[count];
            }

            using (mytrans = cn.BeginTransaction())
            {
                sqlcmd = new SQLiteCommand(sql, cn);
                try
                {
                    sqlcmd.CommandTimeout = 180;
                    sqlcmd.ExecuteNonQuery();
                    mytrans.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                    mytrans.Rollback();
                    return false;
                }
                finally
                {
                    sqlcmd.Dispose();
                    sqlcmd = null;
                }
            }
        }

        public bool ExecuteNonQuery(string sql, IList<SQLiteParameter> cmdparams, ref string err)
        {
            err = "";
            SQLiteCommand sqlcmd = null;
            SQLiteTransaction mytrans = null;

            using (mytrans = cn.BeginTransaction())
            {
                sqlcmd = new SQLiteCommand(sql, cn, mytrans);

                try
                {
                    sqlcmd.Parameters.AddRange(cmdparams.ToArray());
                    sqlcmd.CommandTimeout = 180;
                    sqlcmd.ExecuteNonQuery();
                    mytrans.Commit();
                   
                    return true;
                }
                catch (Exception ex)
                {
                    err = ex.Message;
                    mytrans.Rollback();
                    return false;
                }
                finally
                {
                    sqlcmd.Dispose();
                    sqlcmd = null;
                }
            }
        }

        public struct Storage
        {
            /// <summary>
            /// 该设置的名字
            /// </summary>
            public string Appellation;
            public string ClassName;
            public string Summary;
            public string ClassContent;
            public string SaveTime;
        }
        public bool SaveClass<T>(string aAppellation, T obj, out string err)
        {
            err = "";
            try
            {
                Storage st = new Storage();
                DataTable dt = null;
                Type type = obj.GetType();

                BinaryFormatter serialzer = new BinaryFormatter();
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                serialzer.Serialize(stream, obj);

                st.Appellation = aAppellation;
                st.ClassName = type.FullName;
                st.Summary = "";

                stream.Position = 0;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                stream.Flush();
                stream.Close();
                st.ClassContent = Convert.ToBase64String(buffer);
                st.SaveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                var sqlcmd = "";
                sqlcmd = "select * from storage where app='" + st.Appellation + "';";
                if (!GetData(sqlcmd, ref dt, ref err)) return false;
                if (dt == null) return false;

                switch (dt.Rows.Count)
                {
                    case 0:
                        sqlcmd = "insert into  storage values ('" + st.Appellation + "','" + st.ClassName + "','" + st.Summary + "','" + st.ClassContent + "','" + st.SaveTime + "');";
                        break;
                    case 1:
                        sqlcmd = "update storage set app='" + st.Appellation + "',classname='" + st.ClassName + "',summary='" + st.Summary + "',classcontent='" + st.ClassContent + "',savetime='" + st.SaveTime + "' where app='" + st.Appellation + "';";
                        break;
                    default:
                        return false;
                }

                if (!ExecuteNonQuery(sqlcmd, ref err)) return false;

                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        public bool LoadClass<T>(string aAppellation, ref T obj, ref string err)
        {
            err = "";
            try
            {
                var sqlcmd = "";
                DataTable dt = null;
                sqlcmd = "select * from storage where app='" + aAppellation + "';";
                if (!GetData(sqlcmd, ref dt, ref err)) return false;
                if (dt == null) return false;
                if (dt.Rows.Count != 1)
                {
                    err = "表中指定变量的数量不唯一或者不存在";
                    return false;
                }

                obj = default(T);
                IFormatter formatter = new BinaryFormatter();
                byte[] buffer = Convert.FromBase64String(dt.Rows[0]["classcontent"].ToString());
                System.IO.MemoryStream stream = new System.IO.MemoryStream(buffer);
                obj = (T)formatter.Deserialize(stream);
                stream.Flush();
                stream.Close();

                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
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
                err = ex.Message;
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
                err = ex.Message;
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
    }
}
