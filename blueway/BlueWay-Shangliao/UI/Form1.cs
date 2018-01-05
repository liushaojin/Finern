﻿using System;
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
    using BlueWay_Shangliao.UI;
    
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
        private bool firstLoad = true;
        ScreenSelfAdaption mssa = new ScreenSelfAdaption();//声明自适应类实例
        ControlMode mCtrlMode = ControlMode.AutoMode;    //初始化控制模式为自动模式
        UdpComm udp;
        Form curForm = null;
        object lastSender = null;
        
        public Form1()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }
        //为窗体添加Load事件，并在其方法Form1_Load中，调用类的初始化方法，记录窗体和其控件的初始位置和大小
        private void Form1_Load(object sender, EventArgs e)
        {
            //skinEngine1.SkinFile = System.Environment.CurrentDirectory + "\\Skins\\CalmnessColor1.ssk"; //设置皮肤
            //skinEngine1.DisableTag = 9999;    //设置不需要被渲染的控件的Tag值为9999
            InitUdp();  //udp测试
            //添加主界面
            panel1.Controls.Clear();
            MainForm mForm = new MainForm();
            mForm.TopLevel = false;
            //mForm.FormBorderStyle = FormBorderStyle.None;
            mForm.Dock = DockStyle.Fill;
            mForm.Parent = this.panel1;
            panel1.Controls.Add(mForm);
            mForm.Show();
            curForm = mForm;
            lastSender = btnMain;
            btnMain.SetClicked(true);
            quitBtn.HoldPress = false;
            //InitSubForm();
            mssa.ControlInitSize(this); //比例最大化的初始化
        }
        
        private void InitSubForm()
        {
            panel1.Controls.Clear();
            MainForm mForm = new MainForm();
            mForm.TopLevel = false;
            mForm.Dock = DockStyle.Fill;
            mForm.Parent = this.panel1;
            panel1.Controls.Add(mForm);
            //mForm.Show();
            //mssa.ControlInitSize(this); //比例最大化的初始化
            //curForm = mForm;
            SysSetting sysSetForm = new SysSetting();
            sysSetForm.TopLevel = false;
            //mForm.FormBorderStyle = FormBorderStyle.None;
            sysSetForm.Dock = DockStyle.Fill;
            sysSetForm.Parent = this.panel1;
        }
        
        private void InitUdp()
        {
            udp = new UdpComm();
            udp.onReceiveData += new EventHandler<ReceiveDataEventArgs>(ReceiveUdpData);
            udp.StartListen();
        }
        private void ReceiveUdpData(object sender, ReceiveDataEventArgs e)
        {
            string dat = e.recData;
        }
        
        //为窗体添加SizeChanged事件，并在其方法Form1_SizeChanged中，调用类的自适应方法，完成自适应
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            mssa.ControlAutoSize(this);
        }
        
        private void selfCheckBtn_Click(object sender, EventArgs e)
        {
            if(lastSender == sender)
            {
                return;
            }
            
            if(curForm != null)
            {
                curForm.Close();
            }
            
            SelfTest form = new SelfTest();
            panel1.Controls.Clear();
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.Parent = this.panel1;
            panel1.Controls.Add(form);
            form.Show();
            mssa.ControlInitSize(this); //比例最大化的初始化
            curForm = form;
            lastSender = sender;
        }
        
        private void engineerBtn_Click(object sender, EventArgs e)
        {
            if(lastSender == sender)
            {
                return;
            }
            
            if(curForm != null)
            {
                curForm.Close();
            }
            
            EngineerMode form = new EngineerMode();
            panel1.Controls.Clear();
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.Parent = this.panel1;
            panel1.Controls.Add(form);
            form.Show();
            mssa.ControlInitSize(this); //比例最大化的初始化
            curForm = form;
            lastSender = sender;
        }
        
        private void devOpBtn_Click(object sender, EventArgs e)
        {
            if(lastSender == sender)
            {
                return;
            }
            
            if(curForm != null)
            {
                curForm.Close();
            }
            
            DevOperate form = new DevOperate();
            panel1.Controls.Clear();
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.Parent = this.panel1;
            panel1.Controls.Add(form);
            form.Show();
            mssa.ControlInitSize(this); //比例最大化的初始化
            curForm = form;
            lastSender = sender;
        }
        
        private void devMaintainBtn_Click(object sender, EventArgs e)
        {
            if(lastSender == sender)
            {
                return;
            }
            
            if(curForm != null)
            {
                curForm.Close();
            }
            
            DevMaintain form = new DevMaintain();
            panel1.Controls.Clear();
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.Parent = this.panel1;
            panel1.Controls.Add(form);
            form.Show();
            mssa.ControlInitSize(this); //比例最大化的初始化
            curForm = form;
            lastSender = sender;
        }
        
        private void devSpotCheckBtn_Click(object sender, EventArgs e)
        {
            if(lastSender == sender)
            {
                return;
            }
            
            if(curForm != null)
            {
                curForm.Close();
            }
            
            DevPointCheck form = new DevPointCheck();
            panel1.Controls.Clear();
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.Parent = this.panel1;
            panel1.Controls.Add(form);
            form.Show();
            mssa.ControlInitSize(this); //比例最大化的初始化
            curForm = form;
            lastSender = sender;
        }
        
        private void devMaintainHistoryBtn_Click(object sender, EventArgs e)
        {
            if(lastSender == sender)
            {
                return;
            }
            
            if(curForm != null)
            {
                curForm.Close();
            }
            
            DevMaintianHistory form = new DevMaintianHistory();
            panel1.Controls.Clear();
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.Parent = this.panel1;
            panel1.Controls.Add(form);
            form.Show();
            mssa.ControlInitSize(this); //比例最大化的初始化
            curForm = form;
            lastSender = sender;
        }
        
        private void quitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
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
        
        
        string startupPath = Application.StartupPath;
        
        private void btnMain_Click(object sender, EventArgs e)
        {
            if(lastSender == sender)
            {
                return;
            }
            
            if(curForm != null)
            {
                curForm.Close();
            }
            
            MainForm form = new MainForm();
            panel1.Controls.Clear();
            form.TopLevel = false;
            //mForm.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.Parent = this.panel1;
            panel1.Controls.Add(form);
            form.Show();
            mssa.ControlInitSize(this); //比例最大化的初始化
            curForm = form;
            lastSender = sender;
        }
        
        private void sysBtn_Click(object sender, EventArgs e)
        {
            if(lastSender == sender)
            {
                return;
            }
            
            if(curForm != null)
            {
                curForm.Close();
            }
            
            SysSetting form = new SysSetting();
            panel1.Controls.Clear();
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.Parent = this.panel1;
            panel1.Controls.Add(form);
            form.Show();
            mssa.ControlInitSize(this); //比例最大化的初始化
            curForm = form;
            lastSender = sender;
        }
        
        private void AddFormToPanel(ref object sender, ref Form form)
        {
            panel1.Controls.Clear();
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            form.Parent = this.panel1;
            panel1.Controls.Add(form);
            form.Show();
            mssa.ControlInitSize(this); //比例最大化的初始化
            curForm = form;
            lastSender = sender;
        }
        
        private static int igb = 0;
        private void comButton1_Click(object sender, EventArgs e)
        {
            igb = igb + 1;
        }
    }
}
