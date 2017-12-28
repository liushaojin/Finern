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
        private TcpClient tcpClient;
        private NetworkStream stream;
        
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
            ConnectToServer();
        }
        /// <summary>
        /// 带参数的初始化并连接
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public MyTcpClient(string ip, int port)
        {
            mIp = ip;
            mPort = port;
            ConnectToServer();
        }
        
        /// <summary>
        /// 不带连接状态返回的连接
        /// </summary>
        private void ConnectToServer()
        {
            try
            {
                tcpClient = new TcpClient(mIp, mPort);    //向远程计算机提出连接申请
                stream = tcpClient.GetStream();    //如果连接申请建立，则获得用以传送数据的数据流
                //发送数据
                IsConnect = true;   //connectStr = "成功连接远程计算机！";
            }
            catch(Exception)
            {
                IsConnect = false;   //connectStr = "目标计算机拒绝连接请求！";
            }
        }
        
        /// <summary>
        /// 带连接状态返回的连接
        /// </summary>
        /// <returns></returns>
        private bool ConnectServer()
        {
            try
            {
                tcpClient = new TcpClient(mIp, mPort);    //向远程计算机提出连接申请
                stream = tcpClient.GetStream();    //如果连接申请建立，则获得用以传送数据的数据流
                //发送数据
                IsConnect = true;   //connectStr = "成功连接远程计算机！";
            }
            catch(Exception)
            {
                IsConnect = false;   //connectStr = "目标计算机拒绝连接请求！";
            }
            
            return IsConnect;
        }
        
        /// <summary>
        /// 发送并接收反馈数据
        /// </summary>
        /// <param name="sendDat"></param>
        /// <returns></returns>
        public string SendToServer(string sendDat)
        {
            String responseData = String.Empty;
            byte[] data = System.Text.Encoding.ASCII.GetBytes(sendDat);//将ascii码的字符串数据转换在字节型数据
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
        
        //关闭连接
        public void CloseConnect()
        {
            //关闭所有
            stream.Close();
            tcpClient.Close();
        }
    }
}
