using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace BlueWay_Shangliao.Comm
{
    public class ReceiveDataEventArgs: EventArgs
    {
        public string recData;
        public ReceiveDataEventArgs(string m_DataReceived)
        {
            this.recData = m_DataReceived;
        }
    }
    
    
    class UdpComm
    {
        private string mIp = string.Empty;
        private int mPort = 0;
        private Socket socket = null;
        private Socket sendSocket = null;
        private Thread recThread;
        private EndPoint ep;
        private string recData = string.Empty;
        public event EventHandler<ReceiveDataEventArgs> onReceiveData;
        
        public UdpComm()
        {
        }
        
        public UdpComm(string ip, int port)
        {
            mIp = ip;
            mPort = port;
        }
        
        public void StartListen()
        {
            recThread = new Thread(SocketListen);
            recThread.IsBackground = true;
            recThread.Start();
        }
        
        public void StopListen()
        {
            socket.Close();
            recThread.Abort();
        }
        
        private void SocketListen()
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//初始化一个Scoket协议
                IPEndPoint iep = new IPEndPoint(IPAddress.Any, mPort);//初始化一个侦听局域网内部所有IP和指定端口:9095
                ep = (EndPoint)iep;
                socket.Bind(iep);//绑定这个实例
                
                while(true)
                {
                    byte[] buffer = new byte[1024]; //设置缓冲数据流
                    socket.ReceiveFrom(buffer, ref ep);//接收数据,并确把数据设置到缓冲流里面
                    recData = System.Text.Encoding.Unicode.GetString(buffer);
                    recData = recData.TrimEnd('\u0000');
                    onReceiveData(this, new ReceiveDataEventArgs(recData));
                }
            }
            catch
            {
            }
        }
        
        private bool UdpSend()
        {
            bool res = false;
            
            try
            {
                sendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                IPEndPoint iep = new IPEndPoint(IPAddress.Broadcast, 9000); //设置端口号
                sendSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);//允许套接字发送广播信息
                byte[] sendByte = new byte[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                string sendStr = "123345554";
                sendByte = Encoding.Unicode.GetBytes(sendStr);
                sendSocket.SendTo(sendByte, (EndPoint)iep);
                res = true;
            }
            catch
            {
                res = false;
            }
            finally
            {
                sendSocket.Close();
            }
            
            return res;
        }
        
    }
}
