﻿namespace BlueWay_Shangliao.UI.MyControl
{
    partial class ImageButton
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
        
        #region 组件设计器生成的代码
        
        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            //
            // label1
            //
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Location = new System.Drawing.Point(129, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(512, 128);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // pictureBox1
            //
            this.pictureBox1.BackgroundImage = global::BlueWay_Shangliao.Properties.Resources.system_setting;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            //
            // ImageButton
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ImageButton";
            this.Size = new System.Drawing.Size(640, 128);
            this.Click += new System.EventHandler(this.ImageButton_Click);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImageButton_MouseDown);
            this.MouseEnter += new System.EventHandler(this.ImageButton_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ImageButton_MouseLeave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImageButton_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }
        
        #endregion
        
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
    }
}
