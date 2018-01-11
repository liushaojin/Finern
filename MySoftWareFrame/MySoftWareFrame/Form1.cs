using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySoftWareFrame
{
    using BLL;
    using Model;
    public partial class Form1 : Form
    {
        UserInfo userInfo;
        LoginManager lm = new LoginManager();
        public Form1()
        {
            InitializeComponent();
        }
        
        private void addBtn_Click(object sender, EventArgs e)
        {
            userInfo = new UserInfo();
            {
                userInfo.Username = userTb.Text.Trim();
                userInfo.Password = passTb.Text.Trim();
            };
            string messageStr = null;
            
            if(lm.Add(userInfo, out messageStr))
            {
                MessageBox.Show("添加成功");
            }
            else
            {
                MessageBox.Show(messageStr);
                userTb.Focus();
            }
        }
    }
}
