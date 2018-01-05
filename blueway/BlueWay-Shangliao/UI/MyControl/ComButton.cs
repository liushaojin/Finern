using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueWay_Shangliao.UI.MyControl
{
    public enum MouseState
    {
        Normal,
        Enter,  //光标进入
        Hover,  //光标悬停
        Press,  //光标按下
        Release,//光标释放
        Leave,  //光标离开
        
    }
    public partial class ComButton : Button //UserControl
    {
        public static bool pressed = false;    //按下次数，以便记录状态
        private static Control control = null;
        private MouseState mState = MouseState.Normal;
        public ComButton()
        {
            InitializeComponent();
            //this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FlatStyle = FlatStyle.Flat;
            //this.FlatAppearance.BorderColor = Color.Aqua;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.BackgroundImage = BlueWay_Shangliao.Properties.Resources.btn_normal_40_15;
        }
        
        protected override void OnMouseEnter(EventArgs e)
        {
            mState = MouseState.Enter;
            
            if(!pressed)
            {
                this.BackgroundImage = Image.FromFile("image\\btn_enter_40_15.png");
            }
            
            base.OnMouseEnter(e);
        }
        
        //protected override void OnClick(EventArgs e)
        //{
        //    mState = MouseState.Press;
        //    //this.BackgroundImage = Image.FromFile("image\\btn90_40_press.png");
        //    //GetButton(this, e);
        //    base.OnClick(e);
        //}
        protected override void OnMouseDown(MouseEventArgs e)
        {
            mState = MouseState.Release;
            
            if(pressed)
            {
                this.BackgroundImage = Image.FromFile("image\\btn_press_40_15.png");
            }
            else
            {
                this.BackgroundImage = Image.FromFile("image\\btn_enter_40_15.png");
            }
            
            if(control != null && control != this)
            {
                control.BackgroundImage = Image.FromFile("image\\btn_normal_40_15.png");
            }
            else
            {
                pressed = !pressed;
            }
            
            control = this;
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            mState = MouseState.Release;
            
            if(pressed)
            {
                this.BackgroundImage = Image.FromFile("image\\btn_press_40_15.png");
            }
            else
            {
                this.BackgroundImage = Image.FromFile("image\\btn_enter_40_15.png");
            }
            
            base.OnMouseUp(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            mState = MouseState.Leave;
            
            if(!pressed)
            {
                this.BackgroundImage = Image.FromFile("image\\btn_normal_40_15.png");
            }
            
            base.OnMouseLeave(e);
        }
    }
}
