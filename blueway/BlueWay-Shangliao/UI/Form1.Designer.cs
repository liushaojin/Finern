﻿namespace BlueWay_Shangliao
{
    partial class Form1
    {
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
        
        #region Windows 窗体设计器生成的代码
        
        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.skinEngine1 = new Sunisoft.IrisSkin.SkinEngine();
            this.panel1 = new System.Windows.Forms.Panel();
            this.bluewaylogo = new System.Windows.Forms.PictureBox();
            this.devSpotCheckBtn = new BlueWay_Shangliao.UI.MyControl.MyButton();
            this.quitBtn = new BlueWay_Shangliao.UI.MyControl.MyButton();
            this.devMaintainRecBtn = new BlueWay_Shangliao.UI.MyControl.MyButton();
            this.devMaintainBtn = new BlueWay_Shangliao.UI.MyControl.MyButton();
            this.devOpBtn = new BlueWay_Shangliao.UI.MyControl.MyButton();
            this.engineerBtn = new BlueWay_Shangliao.UI.MyControl.MyButton();
            this.selfCheckBtn = new BlueWay_Shangliao.UI.MyControl.MyButton();
            this.sysBtn = new BlueWay_Shangliao.UI.MyControl.MyButton();
            this.btnMain = new BlueWay_Shangliao.UI.MyControl.MyButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bluewaylogo)).BeginInit();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.devSpotCheckBtn);
            this.groupBox1.Controls.Add(this.quitBtn);
            this.groupBox1.Controls.Add(this.devMaintainRecBtn);
            this.groupBox1.Controls.Add(this.devMaintainBtn);
            this.groupBox1.Controls.Add(this.devOpBtn);
            this.groupBox1.Controls.Add(this.engineerBtn);
            this.groupBox1.Controls.Add(this.selfCheckBtn);
            this.groupBox1.Controls.Add(this.sysBtn);
            this.groupBox1.Controls.Add(this.btnMain);
            this.groupBox1.Controls.Add(this.bluewaylogo);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1002, 75);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            //
            // skinEngine1
            //
            this.skinEngine1.@__DrawButtonFocusRectangle = true;
            this.skinEngine1.DisabledButtonTextColor = System.Drawing.Color.Gray;
            this.skinEngine1.DisabledMenuFontColor = System.Drawing.SystemColors.GrayText;
            this.skinEngine1.InactiveCaptionColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.skinEngine1.SerialNumber = "";
            this.skinEngine1.SkinFile = null;
            //
            // panel1
            //
            this.panel1.Location = new System.Drawing.Point(3, 84);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1002, 644);
            this.panel1.TabIndex = 2;
            //
            // bluewaylogo
            //
            this.bluewaylogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bluewaylogo.BackgroundImage")));
            this.bluewaylogo.Location = new System.Drawing.Point(816, 10);
            this.bluewaylogo.Name = "bluewaylogo";
            this.bluewaylogo.Size = new System.Drawing.Size(177, 62);
            this.bluewaylogo.TabIndex = 8;
            this.bluewaylogo.TabStop = false;
            //
            // devSpotCheckBtn
            //
            this.devSpotCheckBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.devSpotCheckBtn.Clicked = false;
            this.devSpotCheckBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.devSpotCheckBtn.FlatAppearance.BorderSize = 0;
            this.devSpotCheckBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.devSpotCheckBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.devSpotCheckBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.devSpotCheckBtn.ForeColor = System.Drawing.Color.White;
            this.devSpotCheckBtn.HoldPress = true;
            this.devSpotCheckBtn.Location = new System.Drawing.Point(541, 21);
            this.devSpotCheckBtn.Name = "devSpotCheckBtn";
            this.devSpotCheckBtn.Size = new System.Drawing.Size(80, 40);
            this.devSpotCheckBtn.TabIndex = 15;
            this.devSpotCheckBtn.Text = "设备点检";
            this.devSpotCheckBtn.UseVisualStyleBackColor = true;
            this.devSpotCheckBtn.Click += new System.EventHandler(this.devSpotCheckBtn_Click);
            //
            // quitBtn
            //
            this.quitBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.quitBtn.Clicked = false;
            this.quitBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.quitBtn.FlatAppearance.BorderSize = 0;
            this.quitBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.quitBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.quitBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.quitBtn.ForeColor = System.Drawing.Color.White;
            this.quitBtn.HoldPress = true;
            this.quitBtn.Location = new System.Drawing.Point(729, 21);
            this.quitBtn.Name = "quitBtn";
            this.quitBtn.Size = new System.Drawing.Size(80, 40);
            this.quitBtn.TabIndex = 17;
            this.quitBtn.Text = "退出系统";
            this.quitBtn.UseVisualStyleBackColor = true;
            this.quitBtn.Click += new System.EventHandler(this.quitBtn_Click);
            //
            // devMaintainRecBtn
            //
            this.devMaintainRecBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.devMaintainRecBtn.Clicked = false;
            this.devMaintainRecBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.devMaintainRecBtn.FlatAppearance.BorderSize = 0;
            this.devMaintainRecBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.devMaintainRecBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.devMaintainRecBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.devMaintainRecBtn.ForeColor = System.Drawing.Color.White;
            this.devMaintainRecBtn.HoldPress = true;
            this.devMaintainRecBtn.Location = new System.Drawing.Point(630, 21);
            this.devMaintainRecBtn.Name = "devMaintainRecBtn";
            this.devMaintainRecBtn.Size = new System.Drawing.Size(90, 40);
            this.devMaintainRecBtn.TabIndex = 16;
            this.devMaintainRecBtn.Text = "设备保养记录";
            this.devMaintainRecBtn.UseVisualStyleBackColor = true;
            this.devMaintainRecBtn.Click += new System.EventHandler(this.devMaintainHistoryBtn_Click);
            //
            // devMaintainBtn
            //
            this.devMaintainBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.devMaintainBtn.Clicked = false;
            this.devMaintainBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.devMaintainBtn.FlatAppearance.BorderSize = 0;
            this.devMaintainBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.devMaintainBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.devMaintainBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.devMaintainBtn.ForeColor = System.Drawing.Color.White;
            this.devMaintainBtn.HoldPress = true;
            this.devMaintainBtn.Location = new System.Drawing.Point(452, 21);
            this.devMaintainBtn.Name = "devMaintainBtn";
            this.devMaintainBtn.Size = new System.Drawing.Size(80, 40);
            this.devMaintainBtn.TabIndex = 14;
            this.devMaintainBtn.Text = "设备保全书";
            this.devMaintainBtn.UseVisualStyleBackColor = true;
            this.devMaintainBtn.Click += new System.EventHandler(this.devMaintainBtn_Click);
            //
            // devOpBtn
            //
            this.devOpBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.devOpBtn.Clicked = false;
            this.devOpBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.devOpBtn.FlatAppearance.BorderSize = 0;
            this.devOpBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.devOpBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.devOpBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.devOpBtn.ForeColor = System.Drawing.Color.White;
            this.devOpBtn.HoldPress = true;
            this.devOpBtn.Location = new System.Drawing.Point(363, 21);
            this.devOpBtn.Name = "devOpBtn";
            this.devOpBtn.Size = new System.Drawing.Size(80, 40);
            this.devOpBtn.TabIndex = 13;
            this.devOpBtn.Text = "设备操作";
            this.devOpBtn.UseVisualStyleBackColor = true;
            this.devOpBtn.Click += new System.EventHandler(this.devOpBtn_Click);
            //
            // engineerBtn
            //
            this.engineerBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.engineerBtn.Clicked = false;
            this.engineerBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.engineerBtn.FlatAppearance.BorderSize = 0;
            this.engineerBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.engineerBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.engineerBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.engineerBtn.ForeColor = System.Drawing.Color.White;
            this.engineerBtn.HoldPress = true;
            this.engineerBtn.Location = new System.Drawing.Point(274, 21);
            this.engineerBtn.Name = "engineerBtn";
            this.engineerBtn.Size = new System.Drawing.Size(80, 40);
            this.engineerBtn.TabIndex = 12;
            this.engineerBtn.Text = "工程模式";
            this.engineerBtn.UseVisualStyleBackColor = true;
            this.engineerBtn.Click += new System.EventHandler(this.engineerBtn_Click);
            //
            // selfCheckBtn
            //
            this.selfCheckBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.selfCheckBtn.Clicked = false;
            this.selfCheckBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.selfCheckBtn.FlatAppearance.BorderSize = 0;
            this.selfCheckBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.selfCheckBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.selfCheckBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.selfCheckBtn.ForeColor = System.Drawing.Color.White;
            this.selfCheckBtn.HoldPress = true;
            this.selfCheckBtn.Location = new System.Drawing.Point(185, 21);
            this.selfCheckBtn.Name = "selfCheckBtn";
            this.selfCheckBtn.Size = new System.Drawing.Size(80, 40);
            this.selfCheckBtn.TabIndex = 11;
            this.selfCheckBtn.Text = "自检模式";
            this.selfCheckBtn.UseVisualStyleBackColor = true;
            this.selfCheckBtn.Click += new System.EventHandler(this.selfCheckBtn_Click);
            //
            // sysBtn
            //
            this.sysBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sysBtn.Clicked = false;
            this.sysBtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sysBtn.FlatAppearance.BorderSize = 0;
            this.sysBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.sysBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.sysBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sysBtn.ForeColor = System.Drawing.Color.White;
            this.sysBtn.HoldPress = true;
            this.sysBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sysBtn.Location = new System.Drawing.Point(96, 21);
            this.sysBtn.Name = "sysBtn";
            this.sysBtn.Size = new System.Drawing.Size(80, 40);
            this.sysBtn.TabIndex = 10;
            this.sysBtn.Text = "系统设置";
            this.sysBtn.UseVisualStyleBackColor = true;
            this.sysBtn.Click += new System.EventHandler(this.sysBtn_Click);
            //
            // btnMain
            //
            this.btnMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMain.Clicked = false;
            this.btnMain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMain.FlatAppearance.BorderSize = 0;
            this.btnMain.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMain.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMain.ForeColor = System.Drawing.Color.White;
            this.btnMain.HoldPress = true;
            this.btnMain.Location = new System.Drawing.Point(7, 21);
            this.btnMain.Name = "btnMain";
            this.btnMain.Size = new System.Drawing.Size(80, 40);
            this.btnMain.TabIndex = 9;
            this.btnMain.Text = "主页";
            this.btnMain.UseVisualStyleBackColor = true;
            this.btnMain.Click += new System.EventHandler(this.btnMain_Click);
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "上料工位控制系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bluewaylogo)).EndInit();
            this.ResumeLayout(false);
        }
        
        #endregion
        
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox bluewaylogo;
        private Sunisoft.IrisSkin.SkinEngine skinEngine1;
        private System.Windows.Forms.Panel panel1;
        private UI.MyControl.MyButton btnMain;
        private UI.MyControl.MyButton selfCheckBtn;
        private UI.MyControl.MyButton sysBtn;
        private UI.MyControl.MyButton devSpotCheckBtn;
        private UI.MyControl.MyButton quitBtn;
        private UI.MyControl.MyButton devMaintainRecBtn;
        private UI.MyControl.MyButton devMaintainBtn;
        private UI.MyControl.MyButton devOpBtn;
        private UI.MyControl.MyButton engineerBtn;
    }
}

