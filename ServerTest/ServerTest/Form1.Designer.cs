namespace ServerTest
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ipTb = new System.Windows.Forms.TextBox();
            this.portTb = new System.Windows.Forms.TextBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.msgTb = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // listBox1
            //
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(4, 25);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(577, 148);
            this.listBox1.TabIndex = 0;
            //
            // groupBox1
            //
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.listBox2);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Location = new System.Drawing.Point(-1, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(587, 357);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            //
            // label7
            //
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 181);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 19;
            this.label7.Text = "接收区：";
            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "发送区：";
            //
            // listBox2
            //
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(4, 195);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(577, 160);
            this.listBox2.TabIndex = 1;
            //
            // sendBtn
            //
            this.sendBtn.Location = new System.Drawing.Point(299, 66);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(75, 21);
            this.sendBtn.TabIndex = 2;
            this.sendBtn.Text = "发送";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP: ";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port: ";
            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "当前客户端IP: ";
            //
            // ipTb
            //
            this.ipTb.Location = new System.Drawing.Point(47, 6);
            this.ipTb.Name = "ipTb";
            this.ipTb.Size = new System.Drawing.Size(243, 21);
            this.ipTb.TabIndex = 6;
            //
            // portTb
            //
            this.portTb.Location = new System.Drawing.Point(47, 36);
            this.portTb.Name = "portTb";
            this.portTb.Size = new System.Drawing.Size(243, 21);
            this.portTb.TabIndex = 7;
            //
            // startBtn
            //
            this.startBtn.Location = new System.Drawing.Point(299, 6);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 51);
            this.startBtn.TabIndex = 8;
            this.startBtn.Text = "启动";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            //
            // msgTb
            //
            this.msgTb.Location = new System.Drawing.Point(73, 66);
            this.msgTb.Name = "msgTb";
            this.msgTb.Size = new System.Drawing.Size(217, 21);
            this.msgTb.TabIndex = 10;
            //
            // label6
            //
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 9;
            this.label6.Text = "待发消息：";
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.msgTb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.portTb);
            this.Controls.Add(this.ipTb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "服务器测试端";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        
        #endregion
        
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.TextBox ipTb;
        private System.Windows.Forms.TextBox portTb;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.TextBox msgTb;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
    }
}

