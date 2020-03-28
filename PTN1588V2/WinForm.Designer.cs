namespace PTN1588V2
{
    partial class WinForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_ReadNebase = new System.Windows.Forms.Button();
            this.btn_readErCeng = new System.Windows.Forms.Button();
            this.btn_readIP = new System.Windows.Forms.Button();
            this.btn_readPw = new System.Windows.Forms.Button();
            this.btn_WriteData = new System.Windows.Forms.Button();
            this.btn_test = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_ReadNebase
            // 
            this.btn_ReadNebase.Location = new System.Drawing.Point(51, 42);
            this.btn_ReadNebase.Name = "btn_ReadNebase";
            this.btn_ReadNebase.Size = new System.Drawing.Size(122, 23);
            this.btn_ReadNebase.TabIndex = 0;
            this.btn_ReadNebase.Text = "读取网元基本信息";
            this.btn_ReadNebase.UseVisualStyleBackColor = true;
            this.btn_ReadNebase.Click += new System.EventHandler(this.btn_ReadNebase_Click);
            // 
            // btn_readErCeng
            // 
            this.btn_readErCeng.Location = new System.Drawing.Point(51, 93);
            this.btn_readErCeng.Name = "btn_readErCeng";
            this.btn_readErCeng.Size = new System.Drawing.Size(122, 23);
            this.btn_readErCeng.TabIndex = 1;
            this.btn_readErCeng.Text = "读取二层链路";
            this.btn_readErCeng.UseVisualStyleBackColor = true;
            this.btn_readErCeng.Click += new System.EventHandler(this.btn_readErCeng_Click);
            // 
            // btn_readIP
            // 
            this.btn_readIP.Location = new System.Drawing.Point(51, 141);
            this.btn_readIP.Name = "btn_readIP";
            this.btn_readIP.Size = new System.Drawing.Size(122, 23);
            this.btn_readIP.TabIndex = 2;
            this.btn_readIP.Text = "读取地址规划表";
            this.btn_readIP.UseVisualStyleBackColor = true;
            this.btn_readIP.Click += new System.EventHandler(this.btn_readIP_Click);
            // 
            // btn_readPw
            // 
            this.btn_readPw.Location = new System.Drawing.Point(51, 189);
            this.btn_readPw.Name = "btn_readPw";
            this.btn_readPw.Size = new System.Drawing.Size(122, 23);
            this.btn_readPw.TabIndex = 3;
            this.btn_readPw.Text = "读取现网业务表";
            this.btn_readPw.UseVisualStyleBackColor = true;
            this.btn_readPw.Click += new System.EventHandler(this.btn_readPw_Click);
            // 
            // btn_WriteData
            // 
            this.btn_WriteData.Location = new System.Drawing.Point(51, 252);
            this.btn_WriteData.Name = "btn_WriteData";
            this.btn_WriteData.Size = new System.Drawing.Size(75, 23);
            this.btn_WriteData.TabIndex = 4;
            this.btn_WriteData.Text = "生成配置数据表";
            this.btn_WriteData.UseVisualStyleBackColor = true;
            this.btn_WriteData.Click += new System.EventHandler(this.btn_WriteData_Click);
            // 
            // btn_test
            // 
            this.btn_test.Location = new System.Drawing.Point(378, 112);
            this.btn_test.Name = "btn_test";
            this.btn_test.Size = new System.Drawing.Size(75, 23);
            this.btn_test.TabIndex = 5;
            this.btn_test.Text = "测试";
            this.btn_test.UseVisualStyleBackColor = true;
            this.btn_test.Click += new System.EventHandler(this.btn_test_Click);
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_test);
            this.Controls.Add(this.btn_WriteData);
            this.Controls.Add(this.btn_readPw);
            this.Controls.Add(this.btn_readIP);
            this.Controls.Add(this.btn_readErCeng);
            this.Controls.Add(this.btn_ReadNebase);
            this.Name = "WinForm";
            this.Text = "PTN1588V2优化软件";
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ReadNebase;
        private System.Windows.Forms.Button btn_readErCeng;
        private System.Windows.Forms.Button btn_readIP;
        private System.Windows.Forms.Button btn_readPw;
        private System.Windows.Forms.Button btn_WriteData;
        private System.Windows.Forms.Button btn_test;
    }
}

