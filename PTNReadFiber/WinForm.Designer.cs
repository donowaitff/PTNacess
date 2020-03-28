namespace PTNReadFiber
{
    partial class PTN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PTN));
            this.btn_ReadExcel = new System.Windows.Forms.Button();
            this.btn_Ptn = new System.Windows.Forms.Button();
            this.bt_ReadNe = new System.Windows.Forms.Button();
            this.bt_ReadLiuLiang = new System.Windows.Forms.Button();
            this.bt_ReadFiber = new System.Windows.Forms.Button();
            this.btn_CreatROle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_ReadExcel
            // 
            this.btn_ReadExcel.Location = new System.Drawing.Point(37, 147);
            this.btn_ReadExcel.Name = "btn_ReadExcel";
            this.btn_ReadExcel.Size = new System.Drawing.Size(195, 23);
            this.btn_ReadExcel.TabIndex = 0;
            this.btn_ReadExcel.Text = "读取路径信息报表";
            this.btn_ReadExcel.UseVisualStyleBackColor = true;
            this.btn_ReadExcel.Click += new System.EventHandler(this.btn_ReadExcel_Click);
            // 
            // btn_Ptn
            // 
            this.btn_Ptn.Location = new System.Drawing.Point(37, 353);
            this.btn_Ptn.Name = "btn_Ptn";
            this.btn_Ptn.Size = new System.Drawing.Size(195, 24);
            this.btn_Ptn.TabIndex = 1;
            this.btn_Ptn.Text = "新查询方式";
            this.btn_Ptn.UseVisualStyleBackColor = true;
            this.btn_Ptn.Visible = false;
            this.btn_Ptn.Click += new System.EventHandler(this.button1_Click);
            // 
            // bt_ReadNe
            // 
            this.bt_ReadNe.Location = new System.Drawing.Point(37, 30);
            this.bt_ReadNe.Name = "bt_ReadNe";
            this.bt_ReadNe.Size = new System.Drawing.Size(195, 22);
            this.bt_ReadNe.TabIndex = 2;
            this.bt_ReadNe.Text = "读取网元信息报表";
            this.bt_ReadNe.UseVisualStyleBackColor = true;
            this.bt_ReadNe.Click += new System.EventHandler(this.bt_ReadNe_Click);
            // 
            // bt_ReadLiuLiang
            // 
            this.bt_ReadLiuLiang.Location = new System.Drawing.Point(37, 325);
            this.bt_ReadLiuLiang.Name = "bt_ReadLiuLiang";
            this.bt_ReadLiuLiang.Size = new System.Drawing.Size(195, 22);
            this.bt_ReadLiuLiang.TabIndex = 3;
            this.bt_ReadLiuLiang.Text = "读取流量信息报表";
            this.bt_ReadLiuLiang.UseVisualStyleBackColor = true;
            this.bt_ReadLiuLiang.Visible = false;
            this.bt_ReadLiuLiang.Click += new System.EventHandler(this.bt_ReadLiuLiang_Click);
            // 
            // bt_ReadFiber
            // 
            this.bt_ReadFiber.Location = new System.Drawing.Point(37, 85);
            this.bt_ReadFiber.Name = "bt_ReadFiber";
            this.bt_ReadFiber.Size = new System.Drawing.Size(195, 23);
            this.bt_ReadFiber.TabIndex = 4;
            this.bt_ReadFiber.Text = "读取尾纤连接报表";
            this.bt_ReadFiber.UseVisualStyleBackColor = true;
            this.bt_ReadFiber.Click += new System.EventHandler(this.bt_ReadFiber_Click);
            // 
            // btn_CreatROle
            // 
            this.btn_CreatROle.Location = new System.Drawing.Point(37, 210);
            this.btn_CreatROle.Name = "btn_CreatROle";
            this.btn_CreatROle.Size = new System.Drawing.Size(195, 24);
            this.btn_CreatROle.TabIndex = 5;
            this.btn_CreatROle.Text = "输出SDH环路信息";
            this.btn_CreatROle.UseVisualStyleBackColor = true;
            this.btn_CreatROle.Click += new System.EventHandler(this.btn_CreatROle_Click);
            // 
            // PTN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 295);
            this.Controls.Add(this.btn_CreatROle);
            this.Controls.Add(this.bt_ReadFiber);
            this.Controls.Add(this.bt_ReadLiuLiang);
            this.Controls.Add(this.bt_ReadNe);
            this.Controls.Add(this.btn_Ptn);
            this.Controls.Add(this.btn_ReadExcel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PTN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SDH网络优化分析工具";
            this.Load += new System.EventHandler(this.PTN_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ReadExcel;
        private System.Windows.Forms.Button btn_Ptn;
        private System.Windows.Forms.Button bt_ReadNe;
        private System.Windows.Forms.Button bt_ReadLiuLiang;
        private System.Windows.Forms.Button bt_ReadFiber;
        private System.Windows.Forms.Button btn_CreatROle;
    }
}

