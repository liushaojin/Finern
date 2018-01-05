using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace ServerTest
{
    public enum ListenState
    {
        UnListen,
        Listening,
    };
    public partial class Form1 : Form
    {
        private bool listenFlag = true;//监听客户端连接的标志
        Thread threadWatch = null; //负责监听客户端的线程
        Socket socketWatch = null; //负责监听客户端的套接字
        //创建一个负责和客户端通信的套接字
        //public Dictionary<string, MySession> dic_ClientSocket = new Dictionary<string, MySession>();//tcp客户端字典
        //private Dictionary<string, Thread> dic_ClientThread = new Dictionary<string, Thread>();//线程字典,每新增一个连接就添加一条线程
        
        List<Socket> socConnections = new List<Socket>();//tcp客户端集合链表
        List<Thread> dictThread = new List<Thread>();//线程集合链表,每新增一个连接就添加一条线程
        
        ListenState listenState = ListenState.UnListen;
        string ip = string.Empty;
        int port = 3000;
        
        
        private delegate void ReceiveDataHandler(string ip, string dat);
        
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ipTb.Text = "127.0.0.1";
            portTb.Text = "3000";
            msgTb.Text = "服务端向客户端发送：9876543210";
        }
        
        private void ServerStart()
        {
            ip = ipTb.Text.Trim();
            port = Convert.ToInt32(portTb.Text.Trim());//int.Parse(portTb.Text.Trim())
            listenFlag = true;
            
            try
            {
                //定义一个套接字用于监听客户端发来的信息  包含3个参数(IP4寻址协议,流式连接,TCP协议)
                socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endPoint = null;
                
                //服务端发送信息 需要1个IP地址和端口号
                if(!string.IsNullOrEmpty(ip))
                {
                    IPAddress ipaddress = IPAddress.Parse(ip); //获取文本框输入的IP地址
                    endPoint = new IPEndPoint(ipaddress, port); //获取文本框上输入的端口号
                }
                else
                {
                    //将IP地址和端口号绑定到网络节点endpoint上
                    endPoint = new IPEndPoint(IPAddress.Any, port);
                }
                
                //监听绑定的网络节点
                socketWatch.Bind(endPoint);
                //将套接字的监听队列长度限制为20
                socketWatch.Listen(20);
                //创建一个监听线程
                threadWatch = new Thread(WatchConnecting);
                //将窗体线程设置为与后台同步
                threadWatch.IsBackground = true;
                //启动线程
                threadWatch.Start();
                //启动线程后 txtMsg文本框显示相应提示
                listBox1.Items.Add("服务器开始监听客户端..." + "\r\n");
                listenState = ListenState.Listening;
                startBtn.Text = "停止";
            }
            catch
            {
                listenState = ListenState.UnListen;
                startBtn.Text = "启动";
                MessageBox.Show("启动服务器失败!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        private void ServerStop()
        {
            CloseServer();
            listenState = ListenState.UnListen;
            startBtn.Text = "启动";
            string curTime = DateTime.Now.ToString("yyyy-MM-dd  hh:mm:ss");
            listBox1.Items.Add(curTime + ":\r\n" + "服务端停止监听." + "\r\n");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }
        private void startBtn_Click(object sender, EventArgs e)
        {
            switch(listenState)
            {
                case ListenState.UnListen:
                    ServerStart();
                    break;
                    
                case ListenState.Listening:
                    ServerStop();
                    break;
            }
        }
        
        /// <summary>
        /// 监听客户端发来的请求
        /// </summary>
        private void WatchConnecting()
        {
            while(listenFlag)   //持续不断监听客户端发来的请求
            {
                try
                {
                    Socket socConnection = socketWatch.Accept();
                    // 将与客户端连接的 套接字 对象添加到集合中；
                    string clientEndPoint = socConnection.RemoteEndPoint.ToString();
                    //判断同一个IP的连接是否已建立,避免重复连接
                    int index = FindClientSocketIndex(clientEndPoint);
                    
                    if(index < 0)
                    {
                        listBox1.Items.Add("客户端连接成功" + "\r\n");
                        label3.Text = "当前连接客户端IP: " + ((IPEndPoint)socConnection.RemoteEndPoint).Address.ToString();
                        //创建一个通信线程
                        //ParameterizedThreadStart pts = new ParameterizedThreadStart(ServerRecMsg);
                        //Thread thr = new Thread(pts);
                        Thread thr = new Thread(ServerRecMsg);
                        thr.IsBackground = true;
                        //启动线程
                        thr.Start(socConnection);
                        //将连接的客户端套接字与其线程添加到集合中去
                        socConnections.Add(socConnection);
                        dictThread.Add(thr);
                        listBox1.Items.Add("当前连接客户端个数：" + socConnections.Count.ToString() + "\r\n");
                    }
                    else
                    {
                        socConnection = null;
                    }
                }
                catch
                {
                }
                
                //Thread.Sleep(200);
            }
        }
        
        private void ServerRecMsg(object socConnected)
        {
            Socket socketClient = socConnected as Socket;
            bool receiveFlag = true;
            
            while(receiveFlag)
            {
                try
                {
                    IPEndPoint clientIpEp = (IPEndPoint)socketClient.RemoteEndPoint;
                    //获取客户端的ip地址
                    string socIp = ((IPEndPoint)socketClient.RemoteEndPoint).Address.ToString();
                    // 定义一个2M的缓存区；
                    byte[] arrRecMsg = new byte[1024 * 1024 * 2];
                    // 将接受到的数据存入到输入  arrMsgRec中；
                    int length = -1;
                    
                    try
                    {
                        length = socketClient.Receive(arrRecMsg); // 接收数据，并返回数据的长度；
                    }
                    catch
                    {
                        receiveFlag = false;
                        string socStr = socketClient.RemoteEndPoint.ToString();
                        // 从通信线程集合中删除被中断连接的通信线程对象
                        int index = FindClientSocketIndex(socStr);  //先查找所在的索引
                        
                        if(index >= 0)  //找到了就从集合中删除
                        {
                            socConnections.RemoveAt(index);
                            dictThread[index].Abort();
                            dictThread.RemoveAt(index);
                        }
                        
                        socketClient = null;
                        break;
                    }
                    
                    if(length <= 0)
                    {
                        //没有收到数据时的处理
                    }
                    else
                    {
                        byte[] buf = new byte[length];
                        Array.Copy(arrRecMsg, buf, length); //应其它需要而进行接收缓存的拷贝
                        //接收数据的处理
                        //ASCIIEncoding encoder = new ASCIIEncoding();
                        //System.Diagnostics.Debug.WriteLine(encoder.GetString(message, 0, bytesRead));
                        ReceiveDataHandle(socIp, Encoding.Default.GetString(arrRecMsg));
                    }
                }
                catch
                {
                }
                
                //socketClient.Close(); //关闭当前客户端连接
                //Thread.Sleep(100);
            }
        }
        
        private void ReceiveDataHandle(string ip, string dat)
        {
            if(true == this.InvokeRequired)
            {
                this.Invoke(new ReceiveDataHandler(ShowReceiveData), new object[] {ip, dat });
            }
            else
            {
                ShowReceiveData(ip, dat);
            }
        }
        
        private void ShowReceiveData(string ip, string dat)
        {
            listBox2.Items.Add(string.Format("收到IP地址为： {0}的客户端的数据", ip));
            listBox2.SelectedIndex = listBox2.Items.Count - 1;
            int len = dat.Length;
            string temp = string.Empty;
            
            if(len <= 0)
            {
                return;
            }
            
            listBox2.Items.Add(string.Format("接收数据： {0}", dat));
            listBox2.SelectedIndex = listBox2.Items.Count - 1;
            //for(int i = 0; i < len; i++)
            //{
            //    if(i != len - 1)
            //    {
            //        temp += dat[i] + " ";
            //    }
            //    else
            //    {
            //        temp += dat[i];
            //    }
            //    //每8个字符串的显示
            //    if((i / 20 == 0) || (i == len - 1))
            //    {
            //        listBox2.Items.Add(string.Format("第一帧数据为： {0}", temp));
            //        listBox2.SelectedIndex = listBox2.Items.Count - 1;
            //        temp = string.Empty;
            //    }
            //}
        }
        
        //根据套接字从套接字集合中找出其索引
        private int FindClientSocketIndex(string socEndPoint)
        {
            int res = -1;
            string socPoint = string.Empty;
            int i = 0;
            
            foreach(Socket soc in socConnections)
            {
                socPoint = soc.RemoteEndPoint.ToString();
                
                if(socPoint == socEndPoint)
                {
                    res = i;
                }
                
                i++;
            }
            
            return res;
        }
        
        
        
        //发送信息到客户端
        private void sendBtn_Click(object sender, EventArgs e)
        {
            //调用 ServerSendMsg方法  发送信息到客户端
            ServerSendMsg(msgTb.Text.Trim());
        }
        
        /// <summary>
        /// 发送信息到客户端的方法
        /// </summary>
        /// <param name="sendMsg">发送的字符串信息</param>
        private void ServerSendMsg(string sendMsg)
        {
            //将输入的字符串转换成 机器可以识别的字节数组
            byte[] arrSendMsg = Encoding.Default.GetBytes(sendMsg);
            
            //向客户端发送字节数组信息
            foreach(Socket socConnection in socConnections)
            {
                socConnection.Send(arrSendMsg);
            }
            
            string curTime = DateTime.Now.ToString("yyyy-MM-dd  hh:mm:ss");//获取格式化的当前时间
            //将发送的字符串信息附加到文本框txtMsg上
            listBox1.Items.Add(curTime + ":\r\n" + sendMsg + "\r\n");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }
        
        /// <summary>
        /// 关闭服务
        /// </summary>
        public void CloseServer()
        {
            lock(socConnections)
            {
                foreach(var item in socConnections)
                {
                    item.Close();//关闭每一个连接
                }
                
                socConnections.Clear();//清除字典
            }
            
            lock(dictThread)
            {
                foreach(var item in dictThread)
                {
                    item.Abort();//停止线程
                }
                
                dictThread.Clear();
            }
            
            listenFlag = false;
            
            //ServerSocket.Shutdown(SocketShutdown.Both);//服务端不能主动关闭连接,需要把监听到的连接逐个关闭
            if(socketWatch != null)
            {
                socketWatch.Close();
            }
        }
        
        
    }
}
