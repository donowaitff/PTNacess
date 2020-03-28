namespace 阅卷评分系统
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinForm));
            this.btnReadBiaoZhun = new System.Windows.Forms.Button();
            this.btnReadStudent = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReadBiaoZhun
            // 
            this.btnReadBiaoZhun.Location = new System.Drawing.Point(36, 65);
            this.btnReadBiaoZhun.Name = "btnReadBiaoZhun";
            this.btnReadBiaoZhun.Size = new System.Drawing.Size(135, 23);
            this.btnReadBiaoZhun.TabIndex = 0;
            this.btnReadBiaoZhun.Text = "读取标准答案";
            this.btnReadBiaoZhun.UseVisualStyleBackColor = true;
            this.btnReadBiaoZhun.Click += new System.EventHandler(this.btnReadBiaoZhun_Click);
            // 
            // btnReadStudent
            // 
            this.btnReadStudent.Location = new System.Drawing.Point(36, 190);
            this.btnReadStudent.Name = "btnReadStudent";
            this.btnReadStudent.Size = new System.Drawing.Size(135, 27);
            this.btnReadStudent.TabIndex = 1;
            this.btnReadStudent.Text = "读取考生试卷";
            this.btnReadStudent.UseVisualStyleBackColor = true;
            this.btnReadStudent.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(267, 21);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(512, 372);
            this.dataGridView1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "题型及分值";
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnReadStudent);
            this.Controls.Add(this.btnReadBiaoZhun);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WinForm";
            this.Text = "阅卷评分系统";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReadBiaoZhun;
        private System.Windows.Forms.Button btnReadStudent;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
    }
}

