namespace MySoftWareFrame
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
            this.userTb = new System.Windows.Forms.TextBox();
            this.passTb = new System.Windows.Forms.TextBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.permisionTb = new System.Windows.Forms.TextBox();
            this.permisionLb = new System.Windows.Forms.Label();
            this.accountTb = new System.Windows.Forms.TextBox();
            this.passKeyTb = new System.Windows.Forms.TextBox();
            this.passKeyLb = new System.Windows.Forms.Label();
            this.accountLb = new System.Windows.Forms.Label();
            this.addCountBtn = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            //
            // userTb
            //
            this.userTb.Location = new System.Drawing.Point(65, 12);
            this.userTb.Name = "userTb";
            this.userTb.Size = new System.Drawing.Size(126, 21);
            this.userTb.TabIndex = 0;
            //
            // passTb
            //
            this.passTb.Location = new System.Drawing.Point(65, 52);
            this.passTb.Name = "passTb";
            this.passTb.Size = new System.Drawing.Size(126, 21);
            this.passTb.TabIndex = 1;
            //
            // addBtn
            //
            this.addBtn.Location = new System.Drawing.Point(197, 12);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(75, 61);
            this.addBtn.TabIndex = 2;
            this.addBtn.Text = "添加";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "用户名:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "密码:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // tableLayoutPanel1
            //
            this.tableLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.94118F));
            this.tableLayoutPanel1.Controls.Add(this.permisionTb, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.permisionLb, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.accountTb, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.passKeyTb, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.passKeyLb, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.accountLb, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.addCountBtn, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(14, 79);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(258, 138);
            this.tableLayoutPanel1.TabIndex = 5;
            //
            // permisionTb
            //
            this.permisionTb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.permisionTb.Location = new System.Drawing.Point(50, 68);
            this.permisionTb.Name = "permisionTb";
            this.permisionTb.Size = new System.Drawing.Size(202, 21);
            this.permisionTb.TabIndex = 6;
            //
            // permisionLb
            //
            this.permisionLb.AutoSize = true;
            this.permisionLb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.permisionLb.Location = new System.Drawing.Point(6, 65);
            this.permisionLb.Name = "permisionLb";
            this.permisionLb.Size = new System.Drawing.Size(35, 29);
            this.permisionLb.TabIndex = 5;
            this.permisionLb.Text = "权限:";
            this.permisionLb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // accountTb
            //
            this.accountTb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accountTb.Location = new System.Drawing.Point(50, 6);
            this.accountTb.Name = "accountTb";
            this.accountTb.Size = new System.Drawing.Size(202, 21);
            this.accountTb.TabIndex = 0;
            //
            // passKeyTb
            //
            this.passKeyTb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.passKeyTb.Location = new System.Drawing.Point(50, 37);
            this.passKeyTb.Name = "passKeyTb";
            this.passKeyTb.Size = new System.Drawing.Size(202, 21);
            this.passKeyTb.TabIndex = 1;
            //
            // passKeyLb
            //
            this.passKeyLb.AutoSize = true;
            this.passKeyLb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.passKeyLb.Location = new System.Drawing.Point(6, 34);
            this.passKeyLb.Name = "passKeyLb";
            this.passKeyLb.Size = new System.Drawing.Size(35, 28);
            this.passKeyLb.TabIndex = 3;
            this.passKeyLb.Text = "密钥:";
            this.passKeyLb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // accountLb
            //
            this.accountLb.AutoSize = true;
            this.accountLb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accountLb.Location = new System.Drawing.Point(6, 3);
            this.accountLb.Name = "accountLb";
            this.accountLb.Size = new System.Drawing.Size(35, 28);
            this.accountLb.TabIndex = 2;
            this.accountLb.Text = "帐户:";
            this.accountLb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // addCountBtn
            //
            this.addCountBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addCountBtn.Location = new System.Drawing.Point(50, 100);
            this.addCountBtn.Name = "addCountBtn";
            this.addCountBtn.Size = new System.Drawing.Size(202, 32);
            this.addCountBtn.TabIndex = 4;
            this.addCountBtn.Text = "添加帐户";
            this.addCountBtn.UseVisualStyleBackColor = true;
            this.addCountBtn.Click += new System.EventHandler(this.addCountBtn_Click);
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.passTb);
            this.Controls.Add(this.userTb);
            this.Name = "Form1";
            this.Text = "三层架构模型演示";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        
        #endregion
        
        private System.Windows.Forms.TextBox userTb;
        private System.Windows.Forms.TextBox passTb;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox permisionTb;
        private System.Windows.Forms.Label permisionLb;
        private System.Windows.Forms.TextBox accountTb;
        private System.Windows.Forms.TextBox passKeyTb;
        private System.Windows.Forms.Label passKeyLb;
        private System.Windows.Forms.Label accountLb;
        private System.Windows.Forms.Button addCountBtn;
    }
}

