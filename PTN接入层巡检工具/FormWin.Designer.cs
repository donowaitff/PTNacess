namespace PTNAccessOp
{
    partial class FormWin
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
            this.btnReadFile = new System.Windows.Forms.Button();
            this.btn_ReadEXcel = new System.Windows.Forms.Button();
            this.btn_ReadNeBase = new System.Windows.Forms.Button();
            this.btn_ReadFiber = new System.Windows.Forms.Button();
            this.btn_CreatReport = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReadFile
            // 
            this.btnReadFile.Location = new System.Drawing.Point(36, 135);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(124, 23);
            this.btnReadFile.TabIndex = 0;
            this.btnReadFile.Text = "读取光功率(*.csv)";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // btn_ReadEXcel
            // 
            this.btn_ReadEXcel.Location = new System.Drawing.Point(36, 323);
            this.btn_ReadEXcel.Name = "btn_ReadEXcel";
            this.btn_ReadEXcel.Size = new System.Drawing.Size(124, 23);
            this.btn_ReadEXcel.TabIndex = 1;
            this.btn_ReadEXcel.Text = "读取LTE业务";
            this.btn_ReadEXcel.UseVisualStyleBackColor = true;
            this.btn_ReadEXcel.Click += new System.EventHandler(this.btn_ReadEXcel_Click);
            // 
            // btn_ReadNeBase
            // 
            this.btn_ReadNeBase.Location = new System.Drawing.Point(36, 38);
            this.btn_ReadNeBase.Name = "btn_ReadNeBase";
            this.btn_ReadNeBase.Size = new System.Drawing.Size(124, 23);
            this.btn_ReadNeBase.TabIndex = 2;
            this.btn_ReadNeBase.Text = "读取网元信息报表";
            this.btn_ReadNeBase.UseVisualStyleBackColor = true;
            this.btn_ReadNeBase.Click += new System.EventHandler(this.btn_ReadNeBase_Click);
            // 
            // btn_ReadFiber
            // 
            this.btn_ReadFiber.Location = new System.Drawing.Point(36, 84);
            this.btn_ReadFiber.Name = "btn_ReadFiber";
            this.btn_ReadFiber.Size = new System.Drawing.Size(124, 23);
            this.btn_ReadFiber.TabIndex = 3;
            this.btn_ReadFiber.Text = "读取纤缆连接关系";
            this.btn_ReadFiber.UseVisualStyleBackColor = true;
            this.btn_ReadFiber.Click += new System.EventHandler(this.btn_ReadFiber_Click);
            // 
            // btn_CreatReport
            // 
            this.btn_CreatReport.Location = new System.Drawing.Point(36, 191);
            this.btn_CreatReport.Name = "btn_CreatReport";
            this.btn_CreatReport.Size = new System.Drawing.Size(124, 19);
            this.btn_CreatReport.TabIndex = 4;
            this.btn_CreatReport.Text = "生成数据报表";
            this.btn_CreatReport.UseVisualStyleBackColor = true;
            this.btn_CreatReport.Click += new System.EventHandler(this.btn_CreatReport_Click);
            // 
            // FormWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_CreatReport);
            this.Controls.Add(this.btn_ReadFiber);
            this.Controls.Add(this.btn_ReadNeBase);
            this.Controls.Add(this.btn_ReadEXcel);
            this.Controls.Add(this.btnReadFile);
            this.Name = "FormWin";
            this.Text = "PTN接入层分析工具";
            this.Load += new System.EventHandler(this.FormWin_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReadFile;
        private System.Windows.Forms.Button btn_ReadEXcel;
        private System.Windows.Forms.Button btn_ReadNeBase;
        private System.Windows.Forms.Button btn_ReadFiber;
        private System.Windows.Forms.Button btn_CreatReport;
    }
}

