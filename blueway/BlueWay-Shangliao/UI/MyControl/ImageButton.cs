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
    public partial class ImageButton : UserControl
    {
        private int width = 0;
        private int height = 0;
        
        
        /// <summary>
        /// 控件的默认图标
        /// </summary>
        private Image imageIcon = null;
        [Description("控件的默认图标")]
        public Image ImageIcon
        {
            get
            {
                return imageIcon;
            }
            set
            {
                imageIcon = value;
                pictureBox1.Image = imageIcon;
                this.Invalidate();
            }
        }
        /// <summary>
        /// 控件的默认图片
        /// </summary>
        private Image imageDefault = null;
        [Description("控件的默认图片")]
        public Image ImageDefault
        {
            get
            {
                return imageDefault;
            }
            set
            {
                imageDefault = value;
                this.BackgroundImage = imageDefault;
                this.Invalidate();
            }
        }
        
        /// <summary>
        /// 控件按下时的图片
        /// </summary>
        private Image imagePress = null;
        [Description("控件按下时的图片")]
        public Image ImagePress
        {
            get
            {
                return imagePress;
            }
            set
            {
                imagePress = value;
                this.BackgroundImage = imagePress;
                this.Invalidate();
            }
        }
        /// <summary>
        /// 光标移动到控件上方显示的图片
        /// </summary>
        private Image imageEnter = null;
        [Description("光标移动到控件上方显示的图片")]
        public Image ImageEnter
        {
            get
            {
                return imageEnter;
            }
            set
            {
                imageEnter = value;
                this.BackgroundImage = imageEnter;
                this.Invalidate();
            }
        }
        /// <summary>
        /// 光标离开控件显示的图片
        /// </summary>
        private Image imageLeave = null;
        [Description("光标离开控件显示的图片")]
        public Image ImageLeave
        {
            get
            {
                return imageLeave;
            }
            set
            {
                imageLeave = value;
                this.BackgroundImage = imageLeave;
                this.Invalidate();
            }
        }
        /// <summary>
        /// 控件的背景色
        /// </summary>
        private Color backColorM = Color.Transparent;
        [Description("控件的背景色")]
        public Color BackColorM
        {
            get
            {
                return backColorM;
            }
            set
            {
                backColorM = value;
                this.BackColor = backColorM;
                this.Invalidate();
            }
        }
        /// <summary>
        /// 光标移动到控件上方显示的颜色
        /// </summary>
        private Color backColorMove = Color.Transparent;
        [Description("光标移动到控件上方显示的颜色")]
        public Color BackColorMove
        {
            get
            {
                return backColorMove;
            }
            set
            {
                backColorMove = value;
                this.BackColor = backColorMove;
                this.Invalidate();
            }
        }
        /// <summary>
        /// 光标离开控件显示的背景色
        /// </summary>
        private Color backColorLeave = Color.Transparent;
        [Description("光标离开控件显示的背景色")]
        public Color BackColorLeave
        {
            get
            {
                return backColorLeave;
            }
            set
            {
                backColorLeave = value;
                this.BackColor = backColorLeave;
                this.Invalidate();
            }
        }
        /// <summary>
        /// 控件的文字提示
        /// </summary>
        private string textM = "";
        [Description("显示的文字")]
        public string TextM
        {
            get
            {
                return textM;
            }
            set
            {
                textM = value;
                label1.Text = textM;
                this.Invalidate();
            }
        }
        /// <summary>
        /// 文字的颜色
        /// </summary>
        private Color textColor = Color.Black;
        [Description("文字的颜色")]
        public Color TextColor
        {
            get
            {
                return textColor;
            }
            set
            {
                textColor = value;
                label1.ForeColor = textColor;
                this.Invalidate();
            }
        }
        /// <summary>
        /// 用于显示文本的字体
        /// </summary>
        private Font fontM = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        [Description("用于显示文本的字体")]
        public Font FontM
        {
            get
            {
                return fontM;
            }
            set
            {
                fontM = value;
                label1.Font = fontM;
                this.Invalidate();
            }
        }
        
        /// <summary>
        /// 单击事件
        /// </summary>
        public event EventHandler ButtonClick;
        public ImageButton()
        {
            InitializeComponent();
        }
        private void ImageButton_Click(object sender, EventArgs e)
        {
            if(ButtonClick != null)
            {
                ButtonClick(sender, e);
            }
        }
        
        
        private void ImageButton_MouseEnter(object sender, EventArgs e)
        {
        }
        
        private void ImageButton_MouseDown(object sender, MouseEventArgs e)
        {
        }
        
        private void ImageButton_MouseLeave(object sender, EventArgs e)
        {
        }
        
        private void ImageButton_MouseUp(object sender, MouseEventArgs e)
        {
        }
    }
}
