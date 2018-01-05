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

namespace ClientTest
{
    public enum ConnectState
    {
        UnConnect,  //未连接
        Connected,  //已连接
        DisConnect, //断开连接
    };
    public partial class Form1 : Form
    {
        string ip = "127.0.0.1";
        int port = 3000;
        Socket socketClient = null;
        Thread threadClient = null;
        ConnectState connectState = ConnectState.UnConnect;
        
        private delegate void ReceiveDataHandler(int pos, string dat);
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ipTb.Text = "127.0.0.1";
            portTb.Text = "3000";
            msgTb.Text = "客户端向服务端发送数据：0123456789";
        }
        private void ClientConnect()
        {
            ip = ipTb.Text.Trim();
            port = Convert.ToInt32(portTb.Text.Trim());//int.Parse(portTb.Text.Trim())
            
            try
            {
                //定义一个套字节监听  包含3个参数(IP4寻址协议,流式连接,TCP协议)
                socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //需要获取文本框中的IP地址
                IPAddress ipaddress = IPAddress.Parse(ip);
                //将获取的ip地址和端口号绑定到网络节点endpoint上
                IPEndPoint endpoint = new IPEndPoint(ipaddress, port);
                //这里客户端套接字连接到网络节点(服务端)用的方法是Connect 而不是Bind
                socketClient.Connect(endpoint);
                //创建一个线程 用于监听服务端发来的消息
                threadClient = new Thread(RecMsg);
                //将窗体线程设置为与后台同步
                threadClient.IsBackground = true;
                //启动线程
                threadClient.Start();
                listBox1.Items.Add("客户端成功连接服务器..." + "\r\n");
                connectState = ConnectState.Connected;
                connectBtn.Text = "断开";
            }
            catch
            {
                connectBtn.Text = "连接";
                connectState = ConnectState.UnConnect;
                MessageBox.Show("连接服务器失败!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        private void RecMsg()
        {
            while(true)  //持续监听服务端发来的消息
            {
                try
                {
                    //定义一个1024*200的内存缓冲区 用于临时性存储接收到的信息
                    byte[] arrRecMsg = new byte[1024 * 200];
                    //将客户端套接字接收到的数据存入内存缓冲区, 并获取其长度
                    int length = socketClient.Receive(arrRecMsg);
                    byte[] tem = new byte[24];
                    Array.Copy(arrRecMsg, 0, tem, 0, 24);
                    ReceiveDataHandle(1, Encoding.Default.GetString(arrRecMsg));
                    //ByteToObject bytetoobject = new ByteToObject();
                    ////MsgHead为一个自定义的结构体
                    //MsgHead msghead = (MsgHead)bytetoobject.BytesToStuct(tem, typeof(MsgHead));
                    //switch(msghead.MsgType)
                    //{
                    //        //根据自定义的TCP协议中规定的头部来决定后面的操作
                    //}
                }
                catch
                {
                    string errStr = "客户端接收数据出错!";
                    ReceiveDataHandle(1, errStr);
                    //MessageBox.Show("客户端接收数据出错!", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
        private void ReceiveDataHandle(int pos, string dat)
        {
            if(true == this.InvokeRequired)
            {
                this.Invoke(new ReceiveDataHandler(ShowReceiveData), new object[] {pos, dat});
            }
            else
            {
                ShowReceiveData(pos, dat);
            }
        }
        
        private void ShowReceiveData(int pos, string dat)
        {
            int len = dat.Length;
            
            if(len <= 0)
            {
                return;
            }
            
            if(pos == 0)
            {
                listBox1.Items.Add(dat);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
            else
            {
                listBox2.Items.Add(string.Format("接收数据为： {0}", dat));
                listBox2.SelectedIndex = listBox2.Items.Count - 1;
            }
        }
        private void connectBtn_Click(object sender, EventArgs e)
        {
            switch(connectState)
            {
                case ConnectState.UnConnect:
                    ClientConnect();
                    break;
                    
                case ConnectState.Connected:
                    ClientClose();
                    break;
                    
                case ConnectState.DisConnect:
                    ClientConnect();
                    break;
            }
        }
        
        private void sendBtn_Click(object sender, EventArgs e)
        {
            ClientSendMsg(msgTb.Text.Trim());
        }
        
        private void ClientSendMsg(string sendMsg)
        {
            //将输入的字符串转换成 机器可以识别的字节数组
            byte[] arrSendMsg = Encoding.Default.GetBytes(sendMsg);
            //向服务端发送字节数组信息
            socketClient.Send(arrSendMsg);
            string curTime = DateTime.Now.ToString("yyyy-MM-dd  hh:mm:ss");//获取格式化的当前时间
            //将发送的字符串信息附加到文本框txtMsg上
            listBox1.Items.Add(curTime + ":\r\n" + sendMsg + "\r\n");
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }
        
        private void ipTb_TextChanged(object sender, EventArgs e)
        {
            label3.Text = "当前连接服务器IP: " + ip;
        }
        
        private void ClientClose()
        {
            if(socketClient != null)
            {
                threadClient.Abort();
                socketClient.Close();
                threadClient = null;
                socketClient = null;
                connectBtn.Text = "连接";
                connectState = ConnectState.DisConnect;
                string curTime = DateTime.Now.ToString("yyyy-MM-dd  hh:mm:ss");
                listBox1.Items.Add(curTime + ":\r\n" + "客户端断开连接." + "\r\n");
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
        }
    }
}
