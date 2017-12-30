using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace BlueWay_Shangliao.UI.MyControl
{
    public class SwitchButton : UserControl
    {
    
        public enum SwitchStyle
        {
            Style001,
            Style002,
            Style003,
        }   //开关按钮的样式
        
        bool isCheck = false;
        private SwitchStyle mStyle = SwitchStyle.Style001;
        //单击事件
        public event EventHandler SwitchClick;
        public SwitchButton()
        {
            InitializeComponent();
            //控件风格
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.BackColor = Color.Transparent;
            this.Cursor = Cursors.Hand;
            
            if(MStyle != SwitchStyle.Style001)
            {
                this.Size = new Size(100, 60);
            }
            else
            {
                this.Size = new Size(60, 120);
            }
        }
        
        public bool IsCheck
        {
            get
            {
                return isCheck;
            }
            set
            {
                isCheck = value;
                this.Invalidate();
            }
        }
        
        [Browsable(true), DefaultValue(0)]
        [Category("Appearance")]
        public SwitchStyle MStyle
        {
            get
            {
                return mStyle;
            }
            set
            {
                mStyle = value;
                this.Invalidate();
            }
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rec = new Rectangle(0, 0, this.Size.Width, this.Size.Height);
            //Bitmap bitMapOn = null;
            //Bitmap bitMapOff = null;
            //if (mStyle == SwitchStyle.Style003)
            //{
            //    bitMapOn = global::myAlarmSystem.Properties.Resources.btncheckon3;
            //    bitMapOff = global::myAlarmSystem.Properties.Resources.btncheckoff3;
            //}
            Image image_auto = null;
            Image image_manual = null;
            
            //if (mStyle == SwitchStyle.Style002)
            //{
            //    //这种写法就会出现<控件xxx在设计器中引发了一个未经处理的异常，已被禁用>的错误
            //    image_auto = Image.FromFile("image\\switch_auto.png");
            //    image_manual = Image.FromFile("image\\switch_manual.png");
            //}
            if(MStyle == SwitchStyle.Style001)
            {
                //这种写法就不会出现<控件xxx在设计器中引发了一个未经处理的异常，已被禁用>的错误
                image_auto = BlueWay_Shangliao.Properties.Resources.switch_auto;
                image_manual = BlueWay_Shangliao.Properties.Resources.switch_manual;
            }
            else if(MStyle == SwitchStyle.Style002)
            {
                //这种写法就会出现<控件xxx在设计器中引发了一个未经处理的异常，已被禁用>的错误
                image_auto = BlueWay_Shangliao.Properties.Resources.switch_up;
                image_manual = BlueWay_Shangliao.Properties.Resources.switch_down;
            }
            else if(MStyle == SwitchStyle.Style003)
            {
                image_auto = Image.FromFile("image\\switch_auto.png");
                image_manual = Image.FromFile("image\\switch_manual.png");
            }
            
            if(isCheck)
            {
                g.DrawImage(image_auto, rec);
            }
            else
            {
                g.DrawImage(image_manual, rec);
            }
        }
        
        
        
        private void SwitchButton_Click(object sender, EventArgs e)
        {
            isCheck = !isCheck;
            
            if(SwitchClick != null)
            {
                SwitchClick(sender, e);
            }
            
            this.Invalidate();
        }
        
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            
            base.Dispose(disposing);
        }
        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // SwitchButton
            //
            this.BackgroundImage = global::BlueWay_Shangliao.Properties.Resources.switch_manual;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "SwitchButton";
            this.Click += new System.EventHandler(this.SwitchButton_Click);
            this.ResumeLayout(false);
        }
    }
}
