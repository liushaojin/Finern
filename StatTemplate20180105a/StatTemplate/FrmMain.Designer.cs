namespace StatTemplate
{
    partial class FrmMain
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnEqOperation = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnEqCheck = new System.Windows.Forms.Button();
            this.btnOP = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnConfigurion = new System.Windows.Forms.Button();
            this.btnMaintain = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1184, 750);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel2.Controls.Add(this.btnEqOperation, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnExit, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnEqCheck, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnOP, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnView, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnHelp, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnConfigurion, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnMaintain, 4, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1182, 58);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btnEqOperation
            // 
            this.btnEqOperation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEqOperation.Font = new System.Drawing.Font("宋体", 16F);
            this.btnEqOperation.Location = new System.Drawing.Point(150, 3);
            this.btnEqOperation.Name = "btnEqOperation";
            this.btnEqOperation.Size = new System.Drawing.Size(141, 52);
            this.btnEqOperation.TabIndex = 6;
            this.btnEqOperation.Text = "设备操作";
            this.btnEqOperation.UseVisualStyleBackColor = true;
            this.btnEqOperation.Click += new System.EventHandler(this.Fun_Click);
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExit.Font = new System.Drawing.Font("宋体", 16F);
            this.btnExit.Location = new System.Drawing.Point(1032, 3);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(147, 52);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "退出系统";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.Fun_Click);
            // 
            // btnEqCheck
            // 
            this.btnEqCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEqCheck.Font = new System.Drawing.Font("宋体", 16F);
            this.btnEqCheck.Location = new System.Drawing.Point(297, 3);
            this.btnEqCheck.Name = "btnEqCheck";
            this.btnEqCheck.Size = new System.Drawing.Size(141, 52);
            this.btnEqCheck.TabIndex = 2;
            this.btnEqCheck.Text = "点检";
            this.btnEqCheck.UseVisualStyleBackColor = true;
            this.btnEqCheck.Click += new System.EventHandler(this.Fun_Click);
            // 
            // btnOP
            // 
            this.btnOP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOP.Font = new System.Drawing.Font("宋体", 16F);
            this.btnOP.Location = new System.Drawing.Point(444, 3);
            this.btnOP.Name = "btnOP";
            this.btnOP.Size = new System.Drawing.Size(141, 52);
            this.btnOP.TabIndex = 1;
            this.btnOP.Text = "保全书";
            this.btnOP.UseVisualStyleBackColor = true;
            this.btnOP.Click += new System.EventHandler(this.Fun_Click);
            // 
            // btnView
            // 
            this.btnView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnView.Font = new System.Drawing.Font("宋体", 16F);
            this.btnView.Location = new System.Drawing.Point(3, 3);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(141, 52);
            this.btnView.TabIndex = 0;
            this.btnView.Text = "状态查看";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.Fun_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHelp.Font = new System.Drawing.Font("宋体", 16F);
            this.btnHelp.Location = new System.Drawing.Point(885, 3);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(141, 52);
            this.btnHelp.TabIndex = 5;
            this.btnHelp.Text = "帮助";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.Fun_Click);
            // 
            // btnConfigurion
            // 
            this.btnConfigurion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConfigurion.Font = new System.Drawing.Font("宋体", 16F);
            this.btnConfigurion.Location = new System.Drawing.Point(738, 3);
            this.btnConfigurion.Name = "btnConfigurion";
            this.btnConfigurion.Size = new System.Drawing.Size(141, 52);
            this.btnConfigurion.TabIndex = 4;
            this.btnConfigurion.Text = "系统设置";
            this.btnConfigurion.UseVisualStyleBackColor = true;
            this.btnConfigurion.Click += new System.EventHandler(this.Fun_Click);
            // 
            // btnMaintain
            // 
            this.btnMaintain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMaintain.Font = new System.Drawing.Font("宋体", 16F);
            this.btnMaintain.Location = new System.Drawing.Point(591, 3);
            this.btnMaintain.Name = "btnMaintain";
            this.btnMaintain.Size = new System.Drawing.Size(141, 52);
            this.btnMaintain.TabIndex = 3;
            this.btnMaintain.Text = "保养记录";
            this.btnMaintain.UseVisualStyleBackColor = true;
            this.btnMaintain.Click += new System.EventHandler(this.Fun_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 60);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1184, 690);
            this.panel1.TabIndex = 1;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 750);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1200, 736);
            this.Name = "FrmMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMain_FormClosed);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnEqOperation;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnConfigurion;
        private System.Windows.Forms.Button btnMaintain;
        private System.Windows.Forms.Button btnEqCheck;
        private System.Windows.Forms.Button btnOP;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Panel panel1;
    }
}

