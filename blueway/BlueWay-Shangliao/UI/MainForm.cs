using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueWay_Shangliao.UI
{
    using BlueWay_Shangliao.Tool;
    public partial class MainForm : Form
    {
        ScreenSelfAdaption mssa = new ScreenSelfAdaption();//声明自适应类实例
        public MainForm()
        {
            InitializeComponent();
        }
        
        private void myButton1_Click(object sender, EventArgs e)
        {
        }
        
        private void switchButton1_Click(object sender, EventArgs e)
        {
            bool res = false;
            res = switchButton1.IsCheck;
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            //mssa.ControlInitSize(this); //比例最大化的初始化
        }
        
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            //mssa.ControlAutoSize(this);
        }
        
        
        private void imageButton1_Click(object sender, EventArgs e)
        {
            int i = 0;
            i = i + 10;
        }
        
    }
}
