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
    using BlueWay_Shangliao.Data;
    public partial class LoginDlg : Form
    {
        MysqlHelper mysql = MysqlHelper.GetMysqlInstance();
        IniFileHelper iniFileOp = new IniFileHelper();
        string section = "UserInfo";
        string ukey = "user";
        string pkey = "pass";
        string userStr = string.Empty;
        string passStr = string.Empty;
        public LoginDlg()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            //this.groupBox1.Paint += groupBox1_Paint;
        }
        private void LoginDlg_Load(object sender, EventArgs e)
        {
            InitData();
        }
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            userStr = iniFileOp.ReadValue(section, ukey);
            passStr = iniFileOp.ReadValue(section, pkey);
            userTb.Text = userStr;
            passTb.Text = passStr;
        }
        
        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginBtn_Click(object sender, EventArgs e)
        {
            //检查用户密码是否错误
            if(CheckInputInfo())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private bool CheckInputInfo()
        {
            bool res = false;
            string user = userTb.Text.Trim();
            string pass = passTb.Text.Trim();
            
            //判断输入是否为空
            if(string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("用户密码不能为空,请输入...", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
            iniFileOp.WriteString(section, ukey, user);
            iniFileOp.WriteString(section, pkey, pass);
            
            //如果不为空,则检查是否正确
            if(mysql.OpenMysql())
            {
                string queryStr = string.Format("select * from User where name='{0}';", user);
                string restr = mysql.QueryOneRow(queryStr);
                
                if(!string.IsNullOrEmpty(restr.TrimEnd(',')))
                {
                    string[] userInfo = restr.Split(',');
                    
                    if(pass == userInfo[1])
                    {
                        res = true;
                    }
                }
                
                label2.Text = restr;
            }
            else
            {
                MessageBox.Show("数据库打开失败", "温馨提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
            return res;
        }
        
        
    }
}
