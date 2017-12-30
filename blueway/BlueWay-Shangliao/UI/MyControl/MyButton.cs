using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace BlueWay_Shangliao.UI.MyControl
{
    public class MyButton : Button
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
        
        private MouseState mState = MouseState.Normal;
        public MyButton()
        {
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FlatStyle = FlatStyle.Flat;
            //this.FlatAppearance.BorderColor = Color.Aqua;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.BackgroundImage = BlueWay_Shangliao.Properties.Resources.btn90_40_normal;
        }
        
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    //在这里用自己的方法来绘制Button的外观(其实也就是几个框框)
        //    Graphics g = e.Graphics;
        //    g.Clear(this.BackColor);
        //    Rectangle rect = e.ClipRectangle;
        //    rect = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
        
        //    if(mState == MouseState.Enter)
        //    {
        //        if(Focused)
        //        {
        //            this.BackgroundImage = Image.FromFile("image\\btn90_40_focus.png");
        //        }
        //        else
        //        {
        //            this.BackgroundImage = Image.FromFile("image\\btn90_40_enter.png");
        //        }
        
        //        g.DrawImage(BackgroundImage, rect);
        //        return;
        //        //if (Focused)
        //        //{
        //        //    Util_GDI.DrawRoundButton(this.Text, g, rect, buttonStyle.ButtonFocuseAndMouseOver);
        //        //    return;
        //        //}
        //        //Util_GDI.DrawRoundButton(this.Text, g, rect, buttonStyle.ButtonMouseOver);
        //        //return;
        //    }
        //    else if(mState == MouseState.Hover)
        //    {
        //        if(Focused)
        //        {
        //            this.BackgroundImage = Image.FromFile("image\\btn90_40_focus.png");
        //        }
        //        else
        //        {
        //            this.BackgroundImage = Image.FromFile("image\\btn90_40_hover.png");
        //        }
        
        //        g.DrawImage(BackgroundImage, rect);
        //        return;
        //        //Util_GDI.DrawRoundButton(this.Text, g, rect, buttonStyle.ButtonFocuse);
        //        //return;
        //    }
        //    //Util_GDI.DrawRoundButton(this.Text, g, rect, buttonStyle.ButtonNormal);
        //    else if(mState == MouseState.Press)
        //    {
        //        if(Focused)
        //        {
        //            this.BackgroundImage = Image.FromFile("image\\btn90_40_focus.png");
        //        }
        //        else
        //        {
        //            this.BackgroundImage = Image.FromFile("image\\btn90_40_press.png");
        //        }
        
        //        g.DrawImage(BackgroundImage, rect);
        //        return;
        //    }
        //    else if(mState == MouseState.Release)
        //    {
        //        if(Focused)
        //        {
        //            this.BackgroundImage = Image.FromFile("image\\btn90_40_focus.png");
        //        }
        //        else
        //        {
        //            this.BackgroundImage = Image.FromFile("image\\btn90_40_release.png");
        //        }
        
        //        g.DrawImage(BackgroundImage, rect);
        //        return;
        //    }
        //    else if(mState == MouseState.Leave)
        //    {
        //        if(Focused)
        //        {
        //            this.BackgroundImage = Image.FromFile("image\\btn90_40_focus.png");
        //        }
        //        else
        //        {
        //            this.BackgroundImage = Image.FromFile("image\\btn90_40_leave.png");
        //        }
        
        //        g.DrawImage(BackgroundImage, rect);
        //        return;
        //    }
        //    else
        //    {
        //        this.BackgroundImage = Image.FromFile("image\\btn90_40_normal.png");
        //        g.DrawImage(BackgroundImage, rect);
        //        return;
        //    }
        //}
        
        protected override void OnMouseEnter(EventArgs e)
        {
            mState = MouseState.Enter;
            this.BackgroundImage = Image.FromFile("image\\btn90_40_enter.png");
            base.OnMouseEnter(e);
        }
        //protected override void OnMouseHover(EventArgs e)
        //{
        //    mState = MouseState.Hover;
        //    this.BackgroundImage = Image.FromFile("image\\btn90_40_enter.png");
        //    base.OnMouseHover(e);
        //}
        //protected override void OnClick(EventArgs e)
        //{
        //    mState = MouseState.Press;
        //    this.BackgroundImage = Image.FromFile("image\\btn90_40_press.png");
        //    base.OnClick(e);
        //}
        protected override void OnMouseDown(MouseEventArgs e)
        {
            mState = MouseState.Release;
            this.BackgroundImage = Image.FromFile("image\\btn90_40_press.png");
            base.OnMouseUp(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            mState = MouseState.Release;
            this.BackgroundImage = Image.FromFile("image\\btn90_40_enter.png");
            base.OnMouseUp(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            mState = MouseState.Leave;
            this.BackgroundImage = Image.FromFile("image\\btn90_40_normal.png");
            base.OnMouseLeave(e);
        }
        
        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // MyButton
            //
            this.Image = global::BlueWay_Shangliao.Properties.Resources.btn90_40_normal;
            this.ResumeLayout(false);
        }
    }
}
