using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace StatTemplate
{
    class InstanceHelper
    {
        /// <summary>
        /// 设置指定对象的指定属性的值
        /// </summary>
        /// <param name="o">需要获取属性的对象</param>
        /// <param name="propertyName">对象的属性名</param>
        /// <param name="propertyValue">对象的属性值</param>
        /// <returns></returns>
        public static bool SetProperty(object o, string propertyName, object propertyValue, ref string err)
        {
            try
            {
                Type type = o.GetType();//获取类型
                System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);//获取指定名称的属性
                if (propertyInfo == null) throw new Exception("获取指定属性失败，请检查该对象是否有这个属性?");
                propertyInfo.SetValue(o, propertyValue, null);

                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 获取指定对象的指定属性的值
        /// </summary>
        /// <typeparam name="T">对象的类型</typeparam>
        /// <param name="o">需要获取属性的对象</param>
        /// <param name="propertyName">对象的属性名</param>
        /// <param name="propertyValue">获取到的属性值</param>
        /// <returns></returns>
        public static bool GetProperty<T>(object o, string propertyName, out T propertyValue, ref string err)
        {
            try
            {
                Type type = o.GetType();//获取类型
                System.Reflection.PropertyInfo propertyInfo = type.GetProperty(propertyName);//获取指定名称的属性
                if (propertyInfo == null) throw new Exception("获取指定属性失败，请检查该对象是否有这个属性?");
                propertyValue = (T)propertyInfo.GetValue(o, null);

                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                propertyValue = default(T);
                return false;
            }
        }

        /// <summary>
        /// 取得当前源码的哪一行
        /// </summary>
        /// <returns></returns>
        public static int GetLineNum()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);
            return st.GetFrame(0).GetFileLineNumber();
        }

        /// <summary>
        /// 取得当前源码的哪一行
        /// </summary>
        /// <returns></returns>
        public static int GetLineNum(Exception pEx)
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(pEx, true);
            return st.GetFrame(0).GetFileLineNumber();
        }

        /// <summary>
        /// 取当前源码的源文件名
        /// </summary>
        /// <returns></returns>
        public static string GetCurSourceFileName()
        {
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(1, true);

            return st.GetFrame(0).GetFileName();
        }

        /// <summary>
        /// 遍历一个对象
        /// </summary>
        /// <param name="o"></param>
        public static void Traversal(object o)
        {
            foreach (var mi in o.GetType().GetMembers())
            {
                switch (mi.MemberType)
                {
                    case System.Reflection.MemberTypes.Constructor:

                        break;
                    case System.Reflection.MemberTypes.Event:

                        break;
                    case System.Reflection.MemberTypes.Field:
                        Console.Write("Field:");
                        Console.WriteLine(mi.Name);
                        Console.Write("Value:");
                        FieldInfo fi = o.GetType().GetField(mi.Name);
                        object v1 = fi.GetValue(o);
                        if (v1 == null) v1 = "null";
                        Console.WriteLine(v1);
                        break;
                    case System.Reflection.MemberTypes.Method:

                        break;
                    case System.Reflection.MemberTypes.Property:
                        Console.Write("Property:");
                        Console.WriteLine(mi.Name);
                        Console.Write("Value:");
                        PropertyInfo pi = o.GetType().GetProperty(mi.Name);
                        object value = pi.GetValue(o, null);
                        if (value == null) value = "null";
                        Console.WriteLine(value);
                        break;
                    case System.Reflection.MemberTypes.TypeInfo:

                        break;
                    case System.Reflection.MemberTypes.Custom:

                        break;
                    case System.Reflection.MemberTypes.NestedType:

                        break;
                    case System.Reflection.MemberTypes.All:

                        break;
                    default:
                        throw new Exception("Invalid value for MemberTypes");
                }

            }

            Console.ReadKey(true);
        }
        /// <summary>
        /// 启动外部程序
        /// </summary>
        /// <param name="pExeName"></param>
        public static void RunExt(string pExeName, bool pWaitForExit=true)
        {
            try
            {
                System.Diagnostics.Process g = new System.Diagnostics.Process();
                g.StartInfo.UseShellExecute = false;
                g.StartInfo.RedirectStandardOutput = false;
                g.StartInfo.FileName = pExeName;
                g.StartInfo.CreateNoWindow = true;
                g.StartInfo.Arguments = "-R";          // Supposing there is a "-R" parameter
                g.Start();
                if(pWaitForExit) g.WaitForExit();
            }
            catch (Exception ex) { throw ex; }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        /// <summary>
        /// 调用外部程序，并设定外部程序和主程序并行，并非等待外部程序完成后才继续运行主程序
        /// </summary>
        /// <param name="pParentHandle"></param>
        /// <param name="pExeName"></param>
        public static bool RunExtSub(IntPtr pParentHandle,string pExeName, ref string err)
        {
            try
            {
                err = "";

                if (!System.IO.File.Exists(pExeName))
                {
                    err = "程序" + pExeName + "不存在于指定位置";
                    return false;
                }

                System.Diagnostics.Process p = System.Diagnostics.Process.Start(@pExeName);
                p.WaitForInputIdle();
                SetParent(p.MainWindowHandle, pParentHandle);
                ShowWindowAsync(p.MainWindowHandle, 3);

                return true;
            }
            catch (Exception ex) 
            {
                err = ex.Message;
                return false; 
            }
        }
        /// <summary>
        /// 根据进程名称，杀死相关进程
        /// </summary>
        /// <param name="pProcessName">进程名称，不带后缀(.exe等)</param>
        /// <param name="err"></param>
        /// <returns></returns>
        public static bool KillProcess(string pProcessName, ref string err)
        {
            try
            {
                err = "";
                System.Diagnostics.Process[] processes;
                processes = System.Diagnostics.Process.GetProcesses();
                System.Diagnostics.Process process;
                for (int count = 0; count < processes.Length - 1; count++)
                {
                    process = processes[count];
                    if (process.ProcessName == pProcessName)
                    {
                        process.Kill();
                        System.Windows.Forms.Application.DoEvents();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 获取IP4地址
        /// </summary>
        /// <param name="pIP4"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public static bool GetIP4Address(ref string pIP4, ref string err)
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

        List<string> ipList = new List<string>();
        /// <summary>
        /// 获取局域网内所有IP地址和对应的Mac地址、主机名
        /// </summary>
        /// <param name="err"></param>
        /// <returns></returns>
        public void GetLoaclAllAddress(ref string err)
        {
            try
            {
                //获取本地机器名 
                string _myHostName = System.Net.Dns.GetHostName();
                //获取本机IP 
                string _myHostIP = System.Net.Dns.GetHostEntry(_myHostName).AddressList[0].ToString();
                //截取IP网段
                string ipDuan = _myHostIP.Remove(_myHostIP.LastIndexOf('.'));
                //枚举网段计算机
                for (int i = 1; i <= 255; i++)
                {
                    System.Net.NetworkInformation.Ping myPing = new System.Net.NetworkInformation.Ping();
                    myPing.PingCompleted += new System.Net.NetworkInformation.PingCompletedEventHandler(Ping_PingCompleted);
                    string pingIP = ipDuan + "." + i.ToString();
                    myPing.SendAsync(pingIP, 1000, null);

                }
            }
            catch (Exception ex) { throw ex; }
        }

        public void Ping_PingCompleted(object sender, System.Net.NetworkInformation.PingCompletedEventArgs e)
        {
            if (e.Reply.Status == System.Net.NetworkInformation.IPStatus.Success)
            {
                ipList.Add(e.Reply.Address.ToString());
            }
        }
    }

    
}
