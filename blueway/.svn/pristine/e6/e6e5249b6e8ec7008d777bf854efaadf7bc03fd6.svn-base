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
    public enum AlarmState
    {
        Off,    //关灯状态
        Alarm,  //闪烁状态
        On,     //开灯(常亮)状态
    };  //报警灯的三种样式
    public enum AlarmLevel
    {
        Level0, //白色或其它初始颜色，一般为绿色
        Level1, //一级报警  对应红色
        Level2, //二级报警  对应橙色
        Level3, //三级报警  对应黄色
        Level4, //四级报警  对应蓝色
        
    };  //报警级别，便于颜色的控制，级别越低越严重
    
    public partial class AlarmLed : UserControl
    {
        private AlarmState mState = AlarmState.Off;
        private AlarmLevel mLevel = AlarmLevel.Level0;
        private bool timerEnable = false;
        private int timerInterval = 100;
        private Timer timer = new Timer();
        private bool bleed = false;
        Image image = null;
        
        [Browsable(true), DefaultValue(0)]
        [Category("Appearance")]
        public AlarmState MState
        {
            get
            {
                return mState;
            }
            set
            {
                mState = value;
                UpdateState(mState);
                this.Invalidate();
            }
        }
        [Browsable(true), DefaultValue(0)]
        [Category("Appearance")]
        public AlarmLevel MLevel
        {
            get
            {
                return mLevel;
            }
            set
            {
                mLevel = value;
                this.Invalidate();
            }
        }
        //[Browsable(true), DefaultValue(false)]
        //[Category("Appearance")]
        //public bool TimerEnable
        //{
        //    get
        //    {
        //        return timerEnable;
        //    }
        //    set
        //    {
        //        timerEnable = value;
        //    }
        //}
        [Browsable(true), DefaultValue(100)]
        [Category("Appearance")]
        public int TimerInterval
        {
            get
            {
                return timerInterval;
            }
            set
            {
                timerInterval = value;
                this.Invalidate();
            }
        }
        
        public AlarmLed()
        {
            InitializeComponent();
            this.Size = new Size(100, 100);
            timer.Enabled = true;
            timer.Interval = 500;
            timer.Tick += new EventHandler(Timer_Tick);
            image = BlueWay_Shangliao.Properties.Resources.alarm_gray;
            //图片在更新的时候都会不断地闪烁，解决方法就是在这个窗体的构造函数中增加以下三行代码：
            SetStyle(ControlStyles.UserPaint, true);//如果为 true，控件将自行绘制，而不是通过操作系统来绘制。此样式仅适用于派生自 Control 的类
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
        }
        void Timer_Tick(object sender, EventArgs e)
        {
            bleed = !bleed;
            this.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rec = new Rectangle(0, 0, this.Size.Width, this.Size.Height);
            
            //Image image = null;
            if(MState == AlarmState.Off)
            {
                //这种写法就不会出现<控件xxx在设计器中引发了一个未经处理的异常，已被禁用>的错误
                image = BlueWay_Shangliao.Properties.Resources.alarm_gray;
                //这种写法就会出现<控件xxx在设计器中引发了一个未经处理的异常，已被禁用>的错误
                //image = Image.FromFile("image\\switch_auto.png");
            }
            else if(MState == AlarmState.Alarm)
            {
                if(MLevel == AlarmLevel.Level0)
                {
                    if(bleed)
                    {
                        image = BlueWay_Shangliao.Properties.Resources.alarm_green;
                    }
                    else
                    {
                        image = BlueWay_Shangliao.Properties.Resources.alarm_gray;
                    }
                }
                else if(MLevel == AlarmLevel.Level1)
                {
                    if(bleed)
                    {
                        image = BlueWay_Shangliao.Properties.Resources.alarm_red;
                    }
                    else
                    {
                        image = BlueWay_Shangliao.Properties.Resources.alarm_gray;
                    }
                }
                else if(MLevel == AlarmLevel.Level2)
                {
                    if(bleed)
                    {
                        image = BlueWay_Shangliao.Properties.Resources.alarm_orange;
                    }
                    else
                    {
                        image = BlueWay_Shangliao.Properties.Resources.alarm_gray;
                    }
                }
                else if(MLevel == AlarmLevel.Level3)
                {
                    if(bleed)
                    {
                        image = BlueWay_Shangliao.Properties.Resources.alarm_yellow;
                    }
                    else
                    {
                        image = BlueWay_Shangliao.Properties.Resources.alarm_gray;
                    }
                }
                else if(MLevel == AlarmLevel.Level4)
                {
                    if(bleed)
                    {
                        image = BlueWay_Shangliao.Properties.Resources.alarm_blue;
                    }
                    else
                    {
                        image = BlueWay_Shangliao.Properties.Resources.alarm_gray;
                    }
                }
            }
            else if(MState == AlarmState.On)
            {
                if(MLevel == AlarmLevel.Level0)
                {
                    image = BlueWay_Shangliao.Properties.Resources.alarm_green;
                }
                else if(MLevel == AlarmLevel.Level1)
                {
                    image = BlueWay_Shangliao.Properties.Resources.alarm_red;
                }
                else if(MLevel == AlarmLevel.Level2)
                {
                    image = BlueWay_Shangliao.Properties.Resources.alarm_orange;
                }
                else if(MLevel == AlarmLevel.Level3)
                {
                    image = BlueWay_Shangliao.Properties.Resources.alarm_yellow;
                }
                else if(MLevel == AlarmLevel.Level4)
                {
                    image = BlueWay_Shangliao.Properties.Resources.alarm_blue;
                }
            }
            
            g.DrawImage(image, rec);
        }
        
        
        private void UpdateState(AlarmState state)
        {
            switch(state)
            {
                case AlarmState.Off:
                case AlarmState.On:
                    timer.Stop();
                    timer.Enabled = false;
                    break;
                    
                case AlarmState.Alarm:
                    timer.Enabled = true;
                    timer.Start();
                    break;
            }
        }
    }
}
