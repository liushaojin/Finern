using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StatTemplate
{
    public partial class FrmMain : Form
    {
        /// <summary>
        /// 状态保存
        /// </summary>
        MainUIElement Se = null;
        Button[] FunButtonGroup = null;

        ucMonitor PanelView = null;
        ucSOP PanelSOP = null;
        ucMaintenanceBook PanelMaintenanceBook = null;
        ucConfiguration PanelConfig = null;
        ucHelp PanelHelp = null;
        ucEqCheck PanelEqck = null;
        ucEqOperation PanelEqop = null;
        public FrmMain()
        {
            InitializeComponent();
            //使用指定的绑定约束搜索指定的属性
            this.tableLayoutPanel1.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            this.tableLayoutPanel2.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            //导航菜单按键集的初始化
            FunButtonGroup = new Button[] { this.btnView,this.btnEqOperation,this.btnOP,this.btnEqCheck, this.btnMaintain, this.btnConfigurion, this.btnHelp, this.btnExit };
            this.WindowState = FormWindowState.Maximized;
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            try
            {
                string err = "";
                if (!RunStatus.DBManager.LoadClass("MainUIElement", ref Se, ref err))
                {
                    MessageBox.Show("获取主页面设置失败，错误信息: " + err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Se = new MainUIElement();
                }

                InitForm();
            }
            catch (Exception ex) { throw ex; }
        }
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("确定要退出程序吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == System.Windows.Forms.DialogResult.No)
                e.Cancel = true;
        }
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            string err = "";
            try
            {
                RunStatus.DBManager.SaveClass("MainUIElement", Se, out err);

                DisposeResources();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            { 
                default:
                    break;
            }
        }
        /// <summary>
        /// 主页面各功能按键回调函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fun_Click(object sender, EventArgs e)
        {
            try
            {
                Button bt = (Button)sender;
                HighLightButton(bt);
            }
            catch (Exception ex) { throw ex; }
        }

        private void InitForm()
        {
            try
            {
                SetUser(RunStatus.User, RunStatus.Role);
                HighLightButton(FunButtonGroup[Se.HighLightButtonIdx]);

            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// 程序关闭时释放各种资源
        /// </summary>
        private void DisposeResources()
        {
            try
            {
                //string err = "";

                UserControl[] Ucs = new UserControl[] { PanelView, PanelSOP, PanelMaintenanceBook, PanelConfig, PanelHelp, PanelEqop, PanelEqck};
                for (int count = 0; count < Ucs.Length; count++)
                {
                    if (Ucs[count] != null && !Ucs[count].IsDisposed)
                    {
                        Ucs[count].Dispose();
                        Ucs[count] = null;
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// 主页面加载不同的组件
        /// </summary>
        /// <param name="pComponent"></param>
        private void LoadComponent(object pComponent)
        {
            try
            {
                string err = "";
                if (this.panel1.Controls.Contains((System.Windows.Forms.Control)pComponent))
                { }
                else
                {
                    try
                    {
                        this.SuspendLayout();
                        while (this.panel1.Controls.Count > 0)
                        {
                            Type type = this.panel1.Controls[this.panel1.Controls.Count - 1].GetType();
                            switch (type.Name)
                            { 
                                case "ucRemote":
                                    if (!InstanceHelper.KillProcess("RemoteDesktops", ref err))
                                    {
                                        MessageBox.Show(err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                    break;
                            }
                            this.panel1.Controls.RemoveAt(this.panel1.Controls.Count - 1);
                        }
                        this.panel1.Controls.Add((System.Windows.Forms.Control)pComponent);
                        ((System.Windows.Forms.Control)pComponent).Dock = DockStyle.Fill;
                    }
                    catch (Exception ex) { throw ex; }
                    finally{ this.ResumeLayout(false); }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// 高亮显示当前使用的功能Button
        /// </summary>
        /// <param name="pButton"></param>
        private void HighLightButton(Button pButton)
        {
            try
            {
                switch (pButton.Text)
                {
                    case "状态查看":
                        if (PanelView == null) PanelView = new ucMonitor();
                        LoadComponent(PanelView);
                        break;
                    case "设备操作":
                        if (PanelEqop == null) PanelEqop = new ucEqOperation();
                        LoadComponent(PanelEqop);
                        break;
                    case "保全书":
                        if (PanelSOP == null) PanelSOP = new  ucSOP();
                        LoadComponent(PanelSOP);
                        break;
                    case "点检":
                        if (PanelEqck == null) PanelEqck = new ucEqCheck();
                        LoadComponent(PanelEqck);
                        break;
                    case "保养记录":
                        if (PanelMaintenanceBook == null) PanelMaintenanceBook = new ucMaintenanceBook();
                        LoadComponent(PanelMaintenanceBook);
                        break;
                    case "系统设置":
                        if (PanelConfig == null) PanelConfig = new ucConfiguration();
                        LoadComponent(PanelConfig);
                        break;
                    case "帮助":
                        if (PanelHelp == null) PanelHelp = new ucHelp();
                        LoadComponent(PanelHelp);
                        break;
                    case "退出系统":
                        this.Close();
                        break;
                    default:
                        return;
                }

                for (int count = 0; count < FunButtonGroup.Length; count++)
                {
                    if (FunButtonGroup[count].Equals(pButton))
                    {
                        FunButtonGroup[count].BackColor = System.Drawing.SystemColors.GradientActiveCaption;
                    }
                    else
                    {
                        FunButtonGroup[count].BackColor = System.Drawing.Color.White;
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// 获取当前高亮显示的Button
        /// </summary>
        /// <returns></returns>
        private int GetHighLightButton()
        {
            for (int count = 0; count < FunButtonGroup.Length; count++)
                if (FunButtonGroup[count].BackColor == System.Drawing.SystemColors.GradientActiveCaption) return count;
            return 0;
        }

        private delegate void SetUserHandler(string pUser, int pRole);
        /// <summary>
        /// 设定用户名
        /// </summary>
        /// <param name="pUser"></param>
        private void SetUser(string pUser, int pRole)
        {
            if (this.InvokeRequired)
                this.Invoke(new SetUserHandler(SetUser), new object[] { pUser, pRole });
            else
            {
                this.Text = "用户: " + pUser + "-" + ((GlobalDataHelper.Role)pRole).ToString();
            }
        }   
    }

    [Serializable()]
    public class MainUIElement
    {
        /// <summary>
        /// 保存Log
        /// </summary>
        public bool SaveRunLog = false;
        /// <summary>
        /// 上一次显示的功能部件
        /// </summary>
        public int HighLightButtonIdx = 0;
        /// <summary>
        /// 是否显示运行日志
        /// </summary>
        public bool ShowRunLog = false;
    }
}
