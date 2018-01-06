using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace BlueWay_Shangliao.Comm
{
    class MyTcpClient
    {
        private string mIp; //服务器IP
        private int mPort = 0;//端口号
        private bool isConnect = false; //连接状态
        private bool enableClient = false;  //使能客户端标志
        private TcpClient tcpClient;
        private BinaryReader br;
        private BinaryWriter bw;
        //private NetworkStream stream;
        
        private Thread clientThread = null; //客户端线程对象的声明
        
        
        //连接状态
        public bool IsConnect
        {
            get
            {
                return isConnect;
            }
            set
            {
                isConnect = value;
            }
        }
        /// <summary>
        /// 不带参数的初始化并连接
        /// </summary>
        public MyTcpClient()
        {
        }
        
        #region 异步收发方式
        
        /// <summary>
        /// 带参数的客户端构造函数
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public MyTcpClient(string ip, int port)
        {
            mIp = ip;
            mPort = port;
            enableClient = true;
            clientThread = new Thread(ClientConnect);
            clientThread.IsBackground = true;
            clientThread.Start();
        }
        
        /// <summary>
        /// 客户端的连接处理线程
        /// </summary>
        private void ClientConnect()
        {
            tcpClient = new TcpClient(mIp, mPort);
            
            //跨线程的ui响应机制
            //label.Dispatcher.Invoke(()=> label.Content = "与服务器连接成功");
            while(enableClient)
            {
                try
                {
                    IsConnect = true;
                    NetworkStream stream = tcpClient.GetStream();     //获取网络流
                    br = new BinaryReader(stream);      //从流中读取数据
                    string receiveMsg = null;
                    receiveMsg = br.ReadString();
                    //接收数据的处理
                }
                catch
                {
                    IsConnect = false;
                }
            }
        }
        
        /// <summary>
        /// 以异步的方式发送二进制数据,当发送数据量较大时,最好采用线程的方式进行,
        /// </summary>
        /// <param name="dat"></param>
        public void SendOnBinary(string dat)
        {
            NetworkStream stream = tcpClient.GetStream();   //获取网路数据流
            bw = new BinaryWriter(stream);
            bw.Write(dat);  //以二进制的形式进行读写
            //dat += dat + "\r\n";
        }
        #endregion 异步收发方式
        
        #region 同步收发方式, 适用于主从模式
        public MyTcpClient(bool synFlag, string ip, int port)
        {
            mIp = ip;
            mPort = port;
            
            if(synFlag) //同步方式, 此方式时构造函数只负责建立连接
            {
                try
                {
                    IsConnect = true;
                    tcpClient = new TcpClient(mIp, mPort);
                }
                catch
                {
                    IsConnect = false;
                }
            }
            else    //异步方式, 此方式时开线程处理连接及接收
            {
                enableClient = true;
                clientThread = new Thread(ClientConnect);
                clientThread.IsBackground = true;
                clientThread.Start();
            }
        }
        
        
        /// <summary>
        /// 以同步的方式发送并接收数据
        /// </summary>
        /// <param name="sendDat"></param>
        /// <returns></returns>
        public string ClientSend(string dat)
        {
            NetworkStream stream = tcpClient.GetStream();   //获取网路数据流
            String responseData = String.Empty;
            byte[] data = System.Text.Encoding.ASCII.GetBytes(dat);//将ascii码的字符串数据转换在字节型数据
            stream.Write(data, 0, data.Length); //发送数据到服务端
            //接收数据
            data = new Byte[256];
            //int timeOut = 0;
            //while(stream.Length == 0 && timeOut < 1000)
            //{
            //    Thread.Sleep(1);
            //}
            Thread.Sleep(10);//延时10ms接收数据
            Int32 recLen = stream.Read(data, 0, data.Length);
            responseData = System.Text.Encoding.ASCII.GetString(data, 0, recLen);
            //CloseConnect();   //保持连接不关闭
            return responseData;
        }
        
        
        #endregion 同步收发方式
        
        public void ClientOpen()
        {
            enableClient = true;
        }
        
        /// <summary>
        /// 关闭客户端的处理
        /// </summary>
        public void ClientClose()
        {
            enableClient = false;
            
            if(tcpClient != null)
            {
                tcpClient.Close();
                tcpClient = null;
            }
            
            if(clientThread != null)
            {
                clientThread.Abort();
                clientThread = null;
            }
        }
    }
}
