using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;


//进行Tcp编程，应该尽量使用TcpListener和TcpClient（传输消息使用NetworkStream类），而不是Socket类。
namespace BlueWay_Shangliao.Comm
{
    class MyTcpServer
    {
        private Socket client;
        private Socket server;
        private TcpListener tcpListen;
        private TcpClient tcpClient;
        private Thread tcpListenThread;
        
        private StreamWriter sw;
        
        
        private string serverSendStr;
        string connectStr = string.Empty;
        string listenThreadId = string.Empty;   //监听线程的ID
        
        List<string> clientsIpList = new List<string>();    //所有连接客户端的集合
        
        //二进制读写
        public BinaryReader br;
        public BinaryWriter bw;
        
        public MyTcpServer()
        {
            tcpListen = new TcpListener(IPAddress.Any, 3000);
            tcpListenThread = new Thread(new ThreadStart(ListenForClients));
            listenThreadId = "设备监听线程(" + Thread.CurrentThread.ManagedThreadId.ToString() + ')';
            tcpListenThread.Start();
        }
        
        private void ListenForClients()
        {
            tcpListen.Start();  //开始监听
            
            try
            {
                while(true)
                {
                    TcpClient client = tcpListen.AcceptTcpClient();//阻塞直到有客户端连接
                    Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));   //创建一个线程处理客户端的连接
                    string curClientIp = ((System.Net.IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();//获取当前客户端的IP地址
                    clientsIpList.Add(curClientIp);
                    string curThreadId = clientThread.ManagedThreadId.ToString();   //获取当前线程的ID
                    clientThread.Start(client);
                }
            }
            catch
            {
                tcpListen.Stop();
            }
        }
        
        private void HandleClientComm(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            NetworkStream clientStream = tcpClient.GetStream();
            byte[] message = new byte[4096];
            int bytesRead;
            
            //方式一
            //int i;
            //String data = null;
            
            //// 循环接收客户端发送的所有数据
            //while((i = clientStream.Read(message, 0, message.Length)) != 0)
            //{
            //
            //    data = System.Text.Encoding.ASCII.GetString(message, 0, i);//将字节型的数据转换成ASCII码的字符串形式
            //    Console.WriteLine("Received: {0}", data);
            //
            //    data = data.ToUpper();//处理客户端发来的数据
            //    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
            //
            //    clientStream.Write(msg, 0, msg.Length);//发送反馈信息
            //    Console.WriteLine("Sent: {0}", data);
            //}
            
            //方式二
            while(true)
            {
                bytesRead = 0;
                
                try
                {
                    bytesRead = clientStream.Read(message, 0, 4096);    //阻塞直到接收到客户端发来的数据
                }
                catch
                {
                    break;  //套接字发生错误
                }
                
                if(bytesRead == 0)
                {
                    break;  //同服务端的连接断开或是没有收到数据
                }
                
                //数据被成功接收
                ASCIIEncoding encoder = new ASCIIEncoding();
                //对接收数据折解析
                //System.Diagnostics.Debug.WriteLine(encoder.GetString(message, 0, bytesRead));
                //对接收到的客户端数据进行响应
                ServerSend(clientStream);
            }
            
            tcpClient.Close();
        }
        
        //服务端响应客户端要发送的消息
        private void ServerSend(NetworkStream clientStream)
        {
            //利用TcpClient对象GetStream方法得到网络流
            bw = new BinaryWriter(clientStream);
            serverSendStr = "服务端响应客户端";
            //发送数据
            bw.Write(serverSendStr);
            //serverSendStr += serverSendStr + "\r\n";
        }
        
        public void OpenTcp()
        {
        }
        
        public void CloseTcp()
        {
        }
        
        
    }
}
