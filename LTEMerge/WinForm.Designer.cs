namespace LTEMerge
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
            this.btn_ReadLte = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_ReadLte
            // 
            this.btn_ReadLte.Location = new System.Drawing.Point(22, 24);
            this.btn_ReadLte.Name = "btn_ReadLte";
            this.btn_ReadLte.Size = new System.Drawing.Size(156, 23);
            this.btn_ReadLte.TabIndex = 0;
            this.btn_ReadLte.Text = "点我读取LTE业务";
            this.btn_ReadLte.UseVisualStyleBackColor = true;
            this.btn_ReadLte.Click += new System.EventHandler(this.btn_ReadLte_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(196, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(449, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "选取需要合并U2000导出LTE业务表即可生成业务合并表，使用中如有问题，请联系！";
            // 
            // WinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 364);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_ReadLte);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WinForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LTE业务合并工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ReadLte;
        private System.Windows.Forms.Label label1;
    }
}

