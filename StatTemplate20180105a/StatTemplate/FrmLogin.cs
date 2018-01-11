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
    public partial class FrmLogin : Form
    {
        public bool Logined = false;
        public string User = "";
        LogHelper LHelper = null;
        FrmLoginVar Flv = new FrmLoginVar();

        public FrmLogin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            LHelper = new LogHelper(this.rtbLog);
            LHelper.Name = "Login";
            LHelper.Save = true;
            LHelper.ShowDate = false;
            LHelper.ShowTime = true;

            string err = "";
            if (RunStatus.DBManager.LoadClass("FrmLoginVar", ref Flv, ref err))
            {
                this.tbUser.Text = Flv.LastUser;
                this.tbIPAddress.Text = Flv.LastIP;
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            
        }
        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            string err = "";
            Flv.LastUser = this.tbUser.Text.Trim();
            Flv.LastIP = this.tbIPAddress.Text.Trim();
            if (!RunStatus.DBManager.SaveClass("FrmLoginVar", Flv, out err))
            {
                MessageBox.Show("保存本次登录信息失败!", "Tip", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F4)
                {
                    this.lblIPAddress.Visible = !this.lblIPAddress.Visible;
                    this.tbIPAddress.Visible = !this.tbIPAddress.Visible;
                    this.btnTest.Visible = !this.btnTest.Visible;
                }

            }
            catch (Exception ex) { throw ex; }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                string err = "";

                RunStatus.GDHelper.IPAddress = this.tbIPAddress.Text.Trim();
                if (!RunStatus.GDHelper.TestLink(ref err))
                {
                    Log("无法连接中央数据库. " + err, LogHelper.Font.Fail);
                    return;
                }
                else
                    Log("中央数据库连接成功.", LogHelper.Font.Pass);
            }
            catch (Exception ex) { throw ex; }
        }

        private void tbUser_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (this.tbUser.Text.Trim() != "")
                    {
                        this.tbPassword.Focus();
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode != Keys.Enter) return;

                if (this.DbWorker.IsBusy)
                {
                    Log("正在登录!", LogHelper.Font.Fail);
                    return;
                }

                this.tbUser.Enabled = false;
                this.tbPassword.Enabled = false;
                this.DbWorker.RunWorkerAsync(new object[]{this.tbUser.Text, this.tbPassword.Text, this.tbIPAddress.Text});
            }
            catch (Exception ex) { throw ex; }
        }

        private void DbWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                object[] arg = (object[])e.Argument;
                string user = arg[0].ToString();
                string password = arg[1].ToString();
                string ip = arg[2].ToString();
                string err = "";

                Log("用户["+ user + "]正在登录", LogHelper.Font.Content);

                RunStatus.GDHelper.IPAddress = ip;
                if (!RunStatus.GDHelper.TestLink(ref err))
                {
                    Log("无法连接数据库. " + err, LogHelper.Font.Fail);
                    return;
                }

                int role = 0;
                if(!RunStatus.GDHelper.Login(user,password, ref err, ref role))
                {
                    Log(err, LogHelper.Font.Fail);
                    return;
                }

                RunStatus.User = user;
                RunStatus.Role = role;

                this.Logined = true;
            }
            catch (Exception ex) 
            { 
                throw ex; 
            }
        }

        private void DbWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (this.Logined)
                {
                    this.User = this.tbUser.Text.Trim();
                    this.Close();
                }
                else
                {
                    this.tbUser.Enabled = true;
                    this.tbPassword.Enabled = true;
                    this.tbPassword.Focus();
                    this.tbPassword.SelectAll();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Log(string msg, LogHelper.Font font) { LHelper.Log(msg, font); }

       
    }

    [Serializable()]
    class FrmLoginVar
    {
        public string LastUser = "";
        public string LastIP = "";
    }
}
