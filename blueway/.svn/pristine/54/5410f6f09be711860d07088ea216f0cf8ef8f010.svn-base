using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
/// <summary>
/// 窗口自适应屏幕的处理类
/// </summary>
namespace BlueWay_Shangliao.Tool
{
    class ScreenSelfAdaption
    {
        //声明结构,只记录窗体和其控件的初始位置和大小。
        public struct ControlRect
        {
            public int left;
            public int top;
            public int width;
            public int height;
        }
        
        //注意这里不能使用控件列表记录 List<Control> nCtrl;，因为控件的关联性，记录的始终是当前的大小。
        public List<ControlRect> oldCtrl;
        int ctrlNo = 0;
        
        //记录窗体和其控件的初始位置和大小,
        public void ControlInitSize(Form form)
        {
            oldCtrl = new List<ControlRect>();
            ControlRect cR;
            cR.left = form.Left;
            cR.top = form.Top;
            cR.width = form.Width;
            cR.height = form.Height;
            oldCtrl.Add(cR);
            AddControl(form);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
        }
        
        private void AddControl(Control form)
        {
            foreach(Control c in form.Controls)
            {
                ControlRect objCtrl;
                objCtrl.left = c.Left;
                objCtrl.top = c.Top;
                objCtrl.width = c.Width;
                objCtrl.height = c.Height;
                oldCtrl.Add(objCtrl);
                
                if(c.Controls.Count > 0)
                {
                    AddControl(c);    //窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
                }
            }
        }
        //控件自适应大小,
        public void ControlAutoSize(Form form)
        {
            if(ctrlNo == 0)
            {
                ControlRect cR;
                cR.left = 0;
                cR.top = 0;
                cR.width = form.PreferredSize.Width;
                cR.height = form.PreferredSize.Height;
                oldCtrl.Add(cR);//第一个为"窗体本身",只加入一次即可
                AddControl(form);//窗体内其余控件可能嵌套其它控件(比如panel),故单独抽出以便递归调用
            }
            
            float wScale = (float)form.Width / (float)oldCtrl[0].width;//新旧窗体之间的比例，与最早的旧窗体
            float hScale = (float)form.Height / (float)oldCtrl[0].height;//.Height;
            ctrlNo = 1;//第1个是窗体自身的 Left,Top,Width,Height，所以窗体控件从ctrlNo=1开始
            AutoScaleControl(form, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
        }
        private void AutoScaleControl(Control ctl, float wScale, float hScale)
        {
            int ctrLeft0, ctrTop0, ctrWidth0, ctrHeight0;
            
            foreach(Control c in ctl.Controls)
            {
                //放在这里，是先缩放控件的子控件，后缩放控件本身
                ctrLeft0 = oldCtrl[ctrlNo].left;
                ctrTop0 = oldCtrl[ctrlNo].top;
                ctrWidth0 = oldCtrl[ctrlNo].width;
                ctrHeight0 = oldCtrl[ctrlNo].height;
                c.Left = (int)((ctrLeft0) * wScale);//新旧控件之间的线性比例。控件位置只相对于窗体，所以不能加 + wLeft1
                c.Top = (int)((ctrTop0) * hScale);//
                c.Width = (int)(ctrWidth0 * wScale);//只与最初的大小相关，所以不能与现在的宽度相乘 (int)(c.Width * w);
                c.Height = (int)(ctrHeight0 * hScale);//
                ctrlNo++;//累加序号
                
                //放在这里，是先缩放控件本身，后缩放控件的子控件
                if(c.Controls.Count > 0)
                {
                    AutoScaleControl(c, wScale, hScale);    //窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
                }
                
                if(ctl is DataGridView)
                {
                    DataGridView dgv = ctl as DataGridView;
                    Cursor.Current = Cursors.WaitCursor;
                    int widths = 0;
                    
                    for(int i = 0; i < dgv.Columns.Count; i++)
                    {
                        dgv.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);  // 自动调整列宽
                        widths += dgv.Columns[i].Width;   // 计算调整列后单元列的宽度和
                    }
                    
                    if(widths >= ctl.Size.Width)   // 如果调整列的宽度大于设定列宽
                    {
                        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;    // 调整列的模式 自动
                    }
                    else
                    {
                        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;    // 如果小于 则填充
                    }
                    
                    Cursor.Current = Cursors.Default;
                }
            }
        }
        /// <summary>
        /// 窗口坐标和屏幕坐标间的相互转换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X; //相对form窗口的坐标，客户区坐标
            int y = e.Y;
            int x1 = Control.MousePosition.X;//相对显示器，屏幕的坐标
            int y1 = Control.MousePosition.Y;
        }
    }
}
