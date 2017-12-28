using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueWay_Shangliao
{
    using BlueWay_Shangliao.Tool;
    using BlueWay_Shangliao.Comm;
    /// <summary>
    /// 控制模式枚举
    /// </summary>
    public enum ControlMode
    {
        AutoMode,
        ManualMode,
    }
    
    public partial class Form1 : Form
    {
        ScreenSelfAdaption mssa = new ScreenSelfAdaption();//声明自适应类实例
        ControlMode mCtrlMode = ControlMode.AutoMode;    //初始化控制模式为自动模式
        UdpComm udp;
        
        private void ReceiveUdpData(object sender, ReceiveDataEventArgs e)
        {
            string dat = e.recData;
        }
        public Form1()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }
        //为窗体添加Load事件，并在其方法Form1_Load中，调用类的初始化方法，记录窗体和其控件的初始位置和大小
        private void Form1_Load(object sender, EventArgs e)
        {
            mssa.ControlInitSize(this);
            skinEngine1.SkinFile = System.Environment.CurrentDirectory + "\\Skins\\CalmnessColor1.ssk";
            //skinEngine1.DisableTag = 9999;      //设置不需要被渲染的控件Tag值为9999
            InitUdp();//udp测试
        }
        
        private void InitUdp()
        {
            udp = new UdpComm();
            udp.onReceiveData += new EventHandler<ReceiveDataEventArgs>(ReceiveUdpData);
            udp.StartListen();
        }
        
        //为窗体添加SizeChanged事件，并在其方法Form1_SizeChanged中，调用类的自适应方法，完成自适应
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            mssa.ControlAutoSize(this);
        }
        private void systemSetBtn_Click(object sender, EventArgs e)
        {
        }
        
        private void selfCheckBtn_Click(object sender, EventArgs e)
        {
        }
        
        private void engineerBtn_Click(object sender, EventArgs e)
        {
        }
        
        private void devOpBtn_Click(object sender, EventArgs e)
        {
        }
        
        private void devMaintainBtn_Click(object sender, EventArgs e)
        {
        }
        
        private void devSpotCheckBtn_Click(object sender, EventArgs e)
        {
        }
        
        private void devMaintainHistoryBtn_Click(object sender, EventArgs e)
        {
        }
        
        private void quitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void clearBtn_Click(object sender, EventArgs e)
        {
        }
        
        private void startBtn_Click(object sender, EventArgs e)
        {
        }
        
        private void internalStopBtn_Click(object sender, EventArgs e)
        {
        }
        
        private void controlModeBtn_Click(object sender, EventArgs e)
        {
        }
        
        private void pauseBtn_Click(object sender, EventArgs e)
        {
        }
        
        private void alarmResetBtn_Click(object sender, EventArgs e)
        {
        }
        
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult res = MessageBox.Show("您确定要退出程序吗?", "退出确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            
            if(res == DialogResult.OK)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
