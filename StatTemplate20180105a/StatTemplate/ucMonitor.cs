using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace StatTemplate
{
    public partial class ucMonitor : UserControl
    {
        MonitorElement Se = null;
        LogHelper Logger = null;
        ManualResetEvent ManualReset = new ManualResetEvent(false);
        object UILock = new object();
        Pin CollectionPin = null;
        PLCDEvMitsubishi PLCDEv = null;
        bool InitOK = false;

        #region 面板回调函数
        public ucMonitor()
        {
            InitializeComponent();
            this.tableLayoutPanel4.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            this.tableLayoutPanel2.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            this.tableLayoutPanel3.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            base.Load += new System.EventHandler(UILoad);
            base.Disposed += new System.EventHandler(this.UnLoad);

            Logger = new LogHelper(this.richTextBox1);
            Logger.ShowDate = true;
            Logger.ShowTime = true;
            Logger.SaveFolder = Environment.CurrentDirectory + "\\Log";
            Logger.Name = System.IO.Path.GetFileNameWithoutExtension(Application.ExecutablePath);
            Logger.Save = true; 
        }
        //加载面板
        private void UILoad(object sender, EventArgs e)
        {
            try
            {
                string err = "";
                Se = new MonitorElement();
                if (!RunStatus.DBManager.LoadClass("MonitorElement", ref Se, ref err))
                {
                    MessageBox.Show("加载设置失败." + err, "Tip", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.UITimer.Enabled = true;
                this.InitWorker.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Tip", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //销毁面板
        private void UnLoad(object sender, EventArgs e)
        {
            string err = "";

            this.InitWorker.Dispose();
            this.InitWorker = null;
            this.PatrolWorker.CancelAsync();
            this.PatrolWorker.Dispose();
            this.PatrolWorker = null;

            if (!RunStatus.DBManager.SaveClass("MonitorElement", Se, out err))
            {
                MessageBox.Show("保存设置失败." + err, "Tip", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //刷新页面
        private void UITimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Se != null)
                {
                    lock (UILock)
                    {
                        if (Se.NumTotal == 0)
                            this.nudYield.Value = (decimal)0.00;
                        else
                            this.nudYield.Value = (decimal)(Se.NumPass*100/Se.NumTotal);
                        this.nudNumPass.Value = (decimal)Se.NumPass;
                        this.nudNumTotal.Value = (decimal)Se.NumTotal;
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
        //复位统计数据
        private void btnClearData_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确定要清空统计数据?", "Tip", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr != DialogResult.Yes) return;
            if (Se == null) return;

            lock (UILock)
            {
                if (Se != null)
                {
                    Se.NumPass = 0;
                    Se.NumTotal = 0;
                }
            }
        }
        //启动
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!this.InitOK)
            { 
                
            }
            ManualReset.Set();
        }
        //暂停
        private void btnPause_Click(object sender, EventArgs e)
        {
            ManualReset.Reset();
        }
        //报警复位
        private void btnReset_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region 线程
        //初始化线程
        private void InitWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                TM("初始化...", TitleType.Important);
                Log("正在初始化系统", LogHelper.Font.Attention);


                InitOK = true;
            }
            catch (Exception ex)
            {
                Log("初始化异常: " + ex.Message, LogHelper.Font.Fail);
            }
        }
        private void InitWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (InitOK)
                {
                    TM("初始化成功", TitleType.Success);
                    Log("初始化OK.", LogHelper.Font.Pass);
                    this.PatrolWorker.RunWorkerAsync();
                }
                else
                {
                    TM("初始化失败", TitleType.Abnormal);
                    Log("初始化失败.", LogHelper.Font.Fail);
                }
            }
            catch (Exception ex)
            {
                Log("初始化异常: " + ex.Message, LogHelper.Font.Fail);
            }
        }

        private void PatrolWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            string err = "";

            while (!worker.CancellationPending)
            {
                try
                {
                    ManualReset.WaitOne();//如果ManualResetEvent的初始化为终止状态（true），那么该方法将一直工作，直到收到Reset信号。然后，直到收到Set信号，就继续工作

                    Delay(200);

                    if (!PLCDEv.read_io_status(ref CollectionPin, ref err))
                    {
                        Log("读取PLC状态失败." + err, LogHelper.Font.Fail);
                        continue;
                    }
                    if (!CollectionPin.AutoRun) continue;//如果停止运行，不做任何处理

                    RunStatus.ScanCount = RunStatus.ScanCount > 1000000 ? 0 : (RunStatus.ScanCount + 1);
                    if (RunStatus.ScanCount % 200 == 0) GC.Collect();//强制释放一次内存
                    
                    





                }
                catch (Exception ex)
                {
                    Log("轮询线程错误: " + ex.Message, LogHelper.Font.Fail);
                    continue;
                }
            }
        }
        #endregion

        #region 自定义函数
        void Log(string msg, LogHelper.Font font) { Logger.Log(msg, font); }
        void Delay(int pMilliseconds) { System.Threading.Thread.Sleep(pMilliseconds); }

        private enum TitleType {  Content, Success,
                                        Important,
                                        Abnormal,
                                        Warning};
        private delegate void TitleMessageHandler(string msg, TitleType pTitleType);
        private void TM(string msg, TitleType pTitleType)
        {
            if (this.InvokeRequired)
            {
                TitleMessageHandler d = new TitleMessageHandler(TM);
                this.Invoke(d, new object[] { msg, pTitleType });
                d = null;
            }
            else
            {
                this.lblTitleMessage.Text = msg;
                switch (pTitleType)
                { 
                    case TitleType.Content:
                        this.lblTitleMessage.ForeColor = Color.Black;
                        this.lblTitleMessage.BackColor = Color.White;
                        break;
                    case TitleType.Success:
                        this.lblTitleMessage.ForeColor = Color.White;
                        this.lblTitleMessage.BackColor = Color.Green;
                        break;
                    case TitleType.Important:
                        this.lblTitleMessage.ForeColor = Color.White;
                        this.lblTitleMessage.BackColor = Color.Blue;
                        break;
                    case TitleType.Abnormal:
                        this.lblTitleMessage.ForeColor = Color.Black;
                        this.lblTitleMessage.BackColor = Color.Red;
                        break;
                    case TitleType.Warning:
                        this.lblTitleMessage.ForeColor = Color.Black;
                        this.lblTitleMessage.BackColor = Color.Yellow;
                        break;
                }
            }
        }
        #endregion

        



    }

    [Serializable()]
    class MonitorElement
    {
        /// <summary>
        /// 产品总数
        /// </summary>
        public int NumTotal = 0;
        /// <summary>
        /// 良品数量
        /// </summary>
        public int NumPass = 0;
    }
}
