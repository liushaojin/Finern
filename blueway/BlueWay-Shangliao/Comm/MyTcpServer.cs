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
    /// <summary>
    /// 自定义数据类型
    /// </summary>
    class SocketMessage
    {
        public bool msgType = false;
        public int id = 0;
        public string name = null;
        public string msg = null;
        public DateTime time;
        
        public SocketMessage()
        {
        }
        
    }
    
    class MyTcpServer
    {
        private TcpListener tcpListen;
        private Thread tcpListenThread;
        //private StreamWriter sw;
        
        
        private string serverSendStr;
        string connectStr = string.Empty;
        string listenThreadId = string.Empty;   //监听线程的ID
        private bool enableServer = false;      //服务端使能标志
        
        List<TcpClient> clients = new List<TcpClient>();    //连接的客户端集合
        List<Thread> clientThreads = new List<Thread>();    //处理客户端连接的线程集合
        List<string> clientsIpList = new List<string>();    //所有连接客户端的集合
        
        //二进制读写
        public BinaryReader br;
        public BinaryWriter bw;
        
        public MyTcpServer()
        {
            enableServer = true;    //使能服务端
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
                while(enableServer)
                {
                    TcpClient client = tcpListen.AcceptTcpClient();//阻塞直到有客户端连接
                    //将当前连接的客户端套接字及其线程分别加入到对应的集合中去
                    string curClientIp = ((System.Net.IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();//获取当前客户端的IP地址
                    
                    //检查是否有相同的IP已经连接了
                    if(clientsIpList.Contains(curClientIp))
                    {
                        //有则释放本次连接所用资源
                        client.Close();
                        client = null;
                    }
                    else
                    {
                        Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));   //创建一个线程处理客户端的连接
                        string curThreadId = clientThread.ManagedThreadId.ToString();   //获取当前线程的ID
                        clientThread.Start(client);
                        //无则添加对应的数据
                        clientsIpList.Add(curClientIp);
                        clients.Add(client);
                        clientThreads.Add(clientThread);
                    }
                }
            }
            catch
            {
                tcpListen.Stop();
                tcpListenThread.Abort();
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
            //while(true)
            //{
            //    try
            //    {
            //        br = new BinaryReader(clientStream);
            //        string receive = null;
            //        receive = br.ReadString();//读取
            //        textBox.Dispatcher.Invoke(() => textBox.Text += receive + "\r\n");
            //    }
            //    catch
            //    {
            //        MessageBox.Show("接收失败！");
            //    }
            //}
            
            //方式三
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
            
            //tcpClient.Close();
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
        
        //private Dictionary<Socket, ClientInfo> clientPool = new Dictionary<Socket, ClientInfo>();
        private List<SocketMessage> msgPool = new List<SocketMessage>();
        /// <summary>
        /// 广播消息到各个客户端
        /// </summary>
        private void Broadcast()
        {
            Thread broadcast = new Thread(() =>
            {
                while(true)
                {
                    if(clients.Count > 0)
                    {
                        byte[] msg = PackageMessage(msgPool[0]);
                        
                        //foreach(KeyValuePair<Socket, ClientInfo> cs in clientPool)
                        foreach(TcpClient item in clients)
                        {
                            //Socket client = cs.Key;
                            if(item.Connected)
                            {
                                NetworkStream stream = item.GetStream();
                                bw = new BinaryWriter(stream);
                                //发送数据
                                bw.Write(Encoding.Default.GetString(msg));
                            }
                        }
                        
                        msgPool.RemoveAt(0);//广播完首条信息后就从缓存中移除
                    }
                }
            });
            broadcast.Start();
        }
        /// <summary>
        /// 根据协议打包消息,SocketMessage为自定义数据类型
        /// </summary>
        /// <param name="sm"></param>
        /// <returns></returns>
        private byte[] PackageMessage(SocketMessage sm)
        {
            StringBuilder packagedMsg = new StringBuilder();
            
            if(!sm.msgType)  //消息是login信息
            {
                packagedMsg.AppendFormat("{0} @ {1}:\r\n    ", sm.name, sm.time.ToShortTimeString());
                packagedMsg.Append(sm.msg);
            }
            else //处理普通消息
            {
                packagedMsg.AppendFormat("{0} login @ {1}", sm.name, sm.time.ToShortTimeString());
            }
            
            return Encoding.UTF8.GetBytes(packagedMsg.ToString());
        }
        /// <summary>
        /// 开启服务
        /// </summary>
        public void OpenServer()
        {
            enableServer = true;
        }
        
        /// <summary>
        /// 关闭服务
        /// </summary>
        public void CloseServer()
        {
            enableServer = false;
            
            lock(clients)
            {
                foreach(TcpClient item in clients)
                {
                    item.Close();
                }
                
                clients.Clear();
            }
            
            lock(clientThreads)
            {
                foreach(Thread item in clientThreads)
                {
                    item.Abort();
                }
                
                clientThreads.Clear();
            }
            
            if(tcpListen != null)
            {
                tcpListen.Stop();
                tcpListen = null;
            }
            
            if(tcpListenThread != null)
            {
                tcpListenThread.Abort();
                tcpListenThread = null;
            }
        }
        
        
    }
}
