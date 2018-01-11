using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace StatTemplate
{
    class LogHelper
    {
        public enum Font
        {
            Content,
            Pass,
            Fail,
            Attention,
            NoWrap,
            Result
        }

        private object _lockThis = new object();
        private RichTextBox _rtb = null;
        private string[] UUTNum = null;
        public const int MaxUUT = 20;//最大显示个数

        private bool _save = false;
        /// <summary>
        /// 是否即时保存
        /// </summary>
        public bool Save
        {
            set { _save = value; }
        }

        public bool _saveerror = false;
        /// <summary>
        /// 保存错误开关（当使用Fail字体时）
        /// </summary>
        public bool SaveError
        {
            set { _saveerror = value; }
        }

        private string _name = "record";
        /// <summary>
        /// 日志的名字（在保存的时候需要用到）
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _savefolder = Environment.CurrentDirectory+"\\Log\\";
        public string SaveFolder
        {
            get { return _savefolder; }
            set 
            { 
                if(value.Substring(value.Length-2,2) != "\\")
                    _savefolder = value + "\\"; 
                else
                    _savefolder = value; 
            }
        }

        private bool _showdate = true;
        /// <summary>
        /// 是否在log中显示日期
        /// </summary>
        public bool ShowDate
        {
            set { _showdate = value; }
        }

        private bool _showtime = false;
        /// <summary>
        /// 是否在log中显示时间
        /// </summary>
        public bool ShowTime
        {
            set { _showtime = value; }
        }

        public LogHelper(RichTextBox rtb)
        {
            _rtb = rtb;
            UUTNum = new string[20] { "①", "②", "③", "④", "⑤", "⑥", "⑦", "⑧", "⑨", "⑩", "⑪", "⑫", "⑬", "⑭", "⑮", "⑯", "⑰", "⑱", "⑲", "⑳" };
            init(_rtb);
        }

        private void init(RichTextBox rtb)
        {
            try
            {
                rtb.ReadOnly = true;
                if (!System.IO.Directory.Exists(_savefolder)) System.IO.Directory.CreateDirectory(_savefolder);
            }
            catch (Exception ex) { throw ex; }
        }

        public delegate void LogHandler(string msg, Font font);

        /// <summary>
        /// 写日志（与UI线程同步）
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="font"></param>
        public void Log(string msg, Font font)
        {
            if (this._rtb.InvokeRequired)
            {
                LogHandler d = new LogHandler(Log);
                this._rtb.Invoke(d, new object[] { msg, font });
                d = null;
            }
            else
            {
                lock (_lockThis)
                {

                    if (font == Font.NoWrap)
                    {
                        this._rtb.AppendText(msg);
                    }
                    else
                    {
                        string str_date = "";
                        if (_showdate)
                        {
                            str_date += DateTime.Now.ToString("yyyy-MM-dd");
                        }

                        if (_showtime)
                        {
                            if (str_date != "") str_date += " ";
                            str_date += DateTime.Now.ToString("HH:mm:ss.") + DateTime.Now.Millisecond.ToString("d3");
                        }

                        if(str_date!="") str_date += " ";

                        int q_len = this._rtb.Text.Length + str_date.Length;
                        int h_len = this._rtb.Text.Length + str_date.Length + msg.Length - 1;

                        switch (font)
                        {
                            case Font.Content:
                                this._rtb.AppendText(str_date + msg + "\r\n");
                                this._rtb.Select(q_len, h_len);
                                this._rtb.SelectionColor = Color.Black;
                                break;
                            case Font.Pass:
                                this._rtb.AppendText(str_date + msg + "\r\n");
                                this._rtb.Select(q_len, h_len);
                                this._rtb.SelectionColor = Color.Green;
                                this._rtb.SelectionFont = new System.Drawing.Font("宋体", 10.0f, FontStyle.Regular);
                                break;
                            case Font.Fail:
                                this._rtb.AppendText(str_date + msg + "\r\n");
                                this._rtb.Select(q_len, h_len);
                                this._rtb.SelectionColor = Color.Red;
                                this._rtb.SelectionFont = new System.Drawing.Font("宋体", 10.0f, FontStyle.Regular);

                                if(_saveerror)
                                    writelog(_savefolder + _name + "Error" + DateTime.Now.ToString("yyyyMMdd") + ".log", str_date + msg);
                                break;
                            case Font.Attention:
                                this._rtb.AppendText(str_date + msg + "\r\n");
                                this._rtb.Select(q_len, h_len);
                                this._rtb.SelectionColor = Color.Blue;
                                this._rtb.SelectionFont = new System.Drawing.Font("宋体", 10.0f, FontStyle.Bold);
                                break;
                            default:
                                break;
                        }

                        this._rtb.SelectionStart = this._rtb.TextLength;
                        this._rtb.ScrollToCaret();

                        if(_save)
                            writelog(_savefolder + _name + DateTime.Now.ToString("yyyyMMdd")+".log", str_date+msg);
                    }
                }
            }
        }
        /// <summary>
        /// 写日志（与UI线程异步执行）
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="font"></param>
        private void LogBeginInvoke(string msg, Font font)
        {
            LogHandler d = new LogHandler(Log);
            IAsyncResult ir = d.BeginInvoke(msg, font, new AsyncCallback(LogEndInvoke), null);//最后一个参数为回调函数的参数
        }
        /// <summary>
        /// 异步执行结束
        /// </summary>
        /// <param name="result"></param>
        private void LogEndInvoke(IAsyncResult result)
        {
            //result是LogAsync的返回值，应该为null
            LogHandler handler = (LogHandler)((System.Runtime.Remoting.Messaging.AsyncResult)result).AsyncDelegate;
            handler.EndInvoke(result);
        }

        /// <summary>
        /// 插入一张图片
        /// </summary>
        /// <param name="bmp"></param>
        public void InsertBitmap(Bitmap bmp)
        {
            this._rtb.ReadOnly = false;

            try
            {
                Clipboard.SetDataObject(bmp, false, 5, 10);
                DataFormats.Format dataFormat = DataFormats.GetFormat(DataFormats.Bitmap);
                if (this._rtb.CanPaste(dataFormat))
                {
                    this._rtb.SelectionStart = this._rtb.Text.Length;
                    this._rtb.Paste(dataFormat);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Clipboard.Clear();
                this._rtb.ReadOnly = true;
            }
        }

        public delegate void SetResultHandler(int[] aResults, bool aShowAllNum = false);
        /// <summary>
        /// 显示测试结果
        /// </summary>
        /// <param name="aResults">产品结果数组：0-NULL, 1-PASS, 2-FAIL</param>
        /// <param name="aShowAllNum">如果aResult[]长度小于MaxUUT，是否显示多余的UUT结果</param>
        public void SetResult(int[] aResults, bool aShowAllNum = false)
        {
            if (!this._rtb.IsHandleCreated) return;
            if (this._rtb.InvokeRequired)
            {
                SetResultHandler d = new SetResultHandler(SetResult);
                this._rtb.Invoke(d, new object[2] { aResults, aShowAllNum });
                d = null;
            }
            else
            {
                lock (_lockThis)
                {
                    if (aResults.Length > UUTNum.Length) throw new Exception("Result数量大于可显示UUT数量!");

                    for (int count = 0; count < UUTNum.Length; count++)
                    {
                        if (count == aResults.Length)
                        {
                            if (!aShowAllNum) break;
                        }

                        int q_len = this._rtb.Text.Length;
                        this._rtb.AppendText(UUTNum[count]);
                        int h_len = this._rtb.Text.Length;

                        this._rtb.Select(q_len, h_len);
                        if (count > aResults.Length - 1)
                        {
                            this._rtb.SelectionColor = Color.LightGray;
                            this._rtb.SelectionFont = new System.Drawing.Font("宋体", 48.0f, FontStyle.Bold);
                        }
                        else
                        {
                            switch (aResults[count])
                            {
                                case 0:
                                    this._rtb.SelectionColor = Color.LightGray;
                                    this._rtb.SelectionFont = new System.Drawing.Font("宋体", 48.0f, FontStyle.Bold);
                                    break;
                                case 1:
                                    this._rtb.SelectionColor = Color.Green;
                                    this._rtb.SelectionFont = new System.Drawing.Font("宋体", 48.0f, FontStyle.Bold);
                                    break;
                                case 2:
                                    this._rtb.SelectionColor = Color.Red;
                                    this._rtb.SelectionFont = new System.Drawing.Font("宋体", 48.0f, FontStyle.Bold);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    this._rtb.AppendText("\r\n");
                    this._rtb.SelectionStart = this._rtb.TextLength;
                    this._rtb.ScrollToCaret();
                }
            }
        }

        public delegate void RecoverHandler();
        public void Recover()
        {
            if (this._rtb.InvokeRequired)
            {
                RecoverHandler d = new RecoverHandler(Recover);
                this._rtb.Invoke(d);
                d = null;
            }
            else
            { this._rtb.Clear(); }
        }

        private object WriteErrLocker = new object();
        private void writelog(string aFilename, string aContent)
        {
            System.Threading.Monitor.TryEnter(WriteErrLocker, -1);

            FileStream fs = null;
            StreamWriter sw = null;

            try
            {
                fs = new FileStream(aFilename, System.IO.FileMode.Append, System.IO.FileAccess.Write);
                sw = new StreamWriter(fs, System.Text.Encoding.Default);
                sw.Write(aContent + "\r\n");
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                System.Threading.Monitor.Exit(WriteErrLocker);
                if (sw != null) sw.Dispose();
                if (fs != null) fs.Dispose();
            }
        }
    }
}
