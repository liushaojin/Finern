using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StatTemplate
{
    public partial class ucConfiguration : UserControl
    {
        TestVar Tar = null;
        public ucConfiguration()
        {
            InitializeComponent();
           
            base.Load += new System.EventHandler(UILoad);
            base.Disposed += new System.EventHandler(this.UnLoad);

            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            this.cbbAddrScanner.Items.AddRange(ports);
        }

        private void UILoad(object sender, EventArgs e)
        {
            try
            {
                string err = "";
                if (!RunStatus.DBManager.LoadClass("TestVar", ref Tar, ref err))
                {
                    MessageBox.Show("加载设置错误." + err , "Tip", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tar = new TestVar();
                }

                set_params(Tar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Tip", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UnLoad(object sender, EventArgs e) 
        { 
        
        }

        private void ucConfiguration_Enter(object sender, EventArgs e)
        {

        }

        private void ucConfiguration_Leave(object sender, EventArgs e)
        {
            //DialogResult dr = MessageBox.Show("需要保存设置吗?", "Tip", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dr != DialogResult.Yes) return;

            //this.btnSave.PerformClick();
        }

        private void btnSOPBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                //ofd.InitialDirectory = Environment.SpecialFolder.MyComputer;
                ofd.Multiselect = false;
                ofd.ShowDialog();

                if (!System.IO.File.Exists(ofd.FileName)) return;

                this.tbSOP.Text = ofd.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Tip", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMaintainBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                //ofd.InitialDirectory = Environment.SpecialFolder.MyComputer;
                ofd.Multiselect = false;
                ofd.ShowDialog();

                if (!System.IO.File.Exists(ofd.FileName)) return;

                this.tbMainTain.Text = ofd.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Tip", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string err = "";
                TestVar tv = null;
                if (!get_params(ref tv, ref err))
                {
                    MessageBox.Show("获取设置失败. " + err, "Tip", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!RunStatus.DBManager.SaveClass("TestVar", tv, out err))
                {
                    MessageBox.Show("保存设置失败. " + err, "Tip", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("保存设置成功.", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Tip", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void set_params(TestVar tv)
        {
            try
            {
                this.tbServer.Text = tv.Server;
                this.cbbRoutingChartName.Text = tv.RoutingChartName;
                this.cbbLinkName.Text = tv.LinkName;
                this.cbbStationName.Text = tv.StationName;

                this.tbAddrPLC.Text = tv.AddrPLC;
                this.tbAddrCCD1.Text = tv.AddrCCD1;
                this.tbAddrCCD2.Text = tv.AddrCCD2;
                this.tbAddrCCD3.Text = tv.AddrCCD3;
                this.tbAddrCCD4.Text = tv.AddrCCD4;
                this.cbbAddrScanner.Text = tv.AddrScanner;
                this.cbbAddrPrinter.Text = tv.AddrPrinter;
                this.cbbAddrRobot.Text = tv.AddrRobot;

                this.tbSOP.Text = tv.FileSOP;
                this.tbMainTain.Text = tv.FileMainTain;

                this.numAlarmPersent.Value = (decimal)tv.AlarmPersent;
            }
            catch (Exception ex) { throw ex; }
        }

        private bool get_params(ref TestVar tv, ref string err)
        {
            err = "";
            try
            {
                tv = new TestVar();

                tv.Server = this.tbServer.Text.Trim();
                tv.RoutingChartName = this.cbbRoutingChartName.Text;
                tv.LinkName = this.cbbLinkName.Text;
                tv.StationName = this.cbbStationName.Text;

                tv.AddrPLC = this.tbAddrPLC.Text;
                tv.AddrCCD1 = this.tbAddrCCD1.Text;
                tv.AddrCCD2 = this.tbAddrCCD2.Text;
                tv.AddrCCD3 = this.tbAddrCCD3.Text;
                tv.AddrCCD4 = this.tbAddrCCD4.Text;
                tv.AddrScanner = this.cbbAddrScanner.Text;
                tv.AddrPrinter = this.cbbAddrPrinter.Text;
                tv.AddrRobot = this.cbbAddrRobot.Text;

                tv.FileSOP = this.tbSOP.Text;
                tv.FileMainTain = this.tbMainTain.Text;

                tv.AlarmPersent = (int)this.numAlarmPersent.Value;

                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        

        
    }

    [Serializable()]
    public class TestVar
    {
        /// <summary>
        /// 服务器IP地址
        /// </summary>
        public string Server = "";
        /// <summary>
        /// 所使用的流程名
        /// </summary>
        public string RoutingChartName = "";
        /// <summary>
        /// 所在工序的名称
        /// </summary>
        public string LinkName = "";
        /// <summary>
        /// 工站的名称
        /// </summary>
        public string StationName = "";

        /// <summary>
        /// PLC地址
        /// </summary>
        public string AddrPLC = "";
        /// <summary>
        /// CCD地址
        /// </summary>
        public string AddrCCD1 = "";
        public string AddrCCD2 = "";
        public string AddrCCD3 = "";
        public string AddrCCD4 = "";
        /// <summary>
        /// 打印机/镭射机地址
        /// </summary>
        public string AddrPrinter = "";
        public string AddrScanner = "";
        public string AddrRobot = "";

        /// <summary>
        /// SOP/操作指引
        /// </summary>
        public string FileSOP = "";
        /// <summary>
        /// 设备保养记录表
        /// </summary>
        public string FileMainTain = "";
        /// <summary>
        /// 良率低于这个值时告诉PLC报警
        /// </summary>
        public int AlarmPersent = 0;
    }
}
