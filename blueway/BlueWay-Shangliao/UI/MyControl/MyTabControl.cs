using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BlueWay_Shangliao.UI.MyControl
{
    public class MyTabControl: TabControl
    {
        Image itemBackImage = null;
        public MyTabControl()
        {
            this.SizeMode = TabSizeMode.Fixed;  // 大小模式为固定
            this.ItemSize = new Size(44, 55);   // 设定每个标签的尺寸
            itemBackImage = BlueWay_Shangliao.Properties.Resources.naviBtn;
            base.SetStyle(
                ControlStyles.UserPaint |                      // 控件将自行绘制，而不是通过操作系统来绘制
                ControlStyles.OptimizedDoubleBuffer |          // 该控件首先在缓冲区中绘制，而不是直接绘制到屏幕上，这样可以减少闪烁
                ControlStyles.AllPaintingInWmPaint |           // 控件将忽略 WM_ERASEBKGND 窗口消息以减少闪烁
                ControlStyles.ResizeRedraw |                   // 在调整控件大小时重绘控件
                ControlStyles.SupportsTransparentBackColor,    // 控件接受 alpha 组件小于 255 的 BackColor 以模拟透明
                true);                                         // 设置以上值为 true
            base.UpdateStyles();
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            for(int i = 0; i < this.TabCount; i++)
            {
                base.OnPaint(e);
                System.Drawing.Pen pen = new Pen(this.BackColor, 3);
                e.Graphics.DrawRectangle(pen, 0, 0, this.Width, this.Height);//填充
                //e.Graphics.DrawRectangle(Pens.Red, this.GetTabRect(i));
                
                if(this.SelectedIndex == i)
                {
                    e.Graphics.DrawImage(itemBackImage, this.GetTabRect(i));
                }
                
                // Calculate text position
                Rectangle bounds = this.GetTabRect(i);
                PointF textPoint = new PointF();
                SizeF textSize = TextRenderer.MeasureText(this.TabPages[i].Text, this.Font);
                // 注意要加上每个标签的左偏移量X
                textPoint.X
                    = bounds.X + (bounds.Width - textSize.Width) / 2;
                textPoint.Y
                    = bounds.Bottom - textSize.Height - this.Padding.Y;
                // Draw highlights
                e.Graphics.DrawString(
                    this.TabPages[i].Text,
                    this.Font,
                    SystemBrushes.ControlLightLight,    // 高光颜色
                    textPoint.X,
                    textPoint.Y);
                // 绘制正常文字
                textPoint.Y--;
                e.Graphics.DrawString(
                    this.TabPages[i].Text,
                    this.Font,
                    SystemBrushes.ControlText,    // 正常颜色
                    textPoint.X,
                    textPoint.Y);
                    
                // 绘制图标
                if(this.ImageList != null)
                {
                    int index = this.TabPages[i].ImageIndex;
                    string key = this.TabPages[i].ImageKey;
                    Image icon = new Bitmap(1, 1);
                    
                    if(index > -1)
                    {
                        icon = this.ImageList.Images[index];
                    }
                    
                    if(!string.IsNullOrEmpty(key))
                    {
                        icon = this.ImageList.Images[key];
                    }
                    
                    e.Graphics.DrawImage(
                        icon,
                        bounds.X + (bounds.Width - icon.Width) / 2,
                        bounds.Top + this.Padding.Y);
                }
                
                pen.Dispose();
            }
        }
    }
}
