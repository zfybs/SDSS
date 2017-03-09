namespace SDSS.PostProcess
{
    partial class ResultLister
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button_WriteReport = new System.Windows.Forms.Button();
            this.button_Close = new System.Windows.Forms.Button();
            this._bgw_Report = new System.ComponentModel.BackgroundWorker();
            this.flowLayoutPanel_Items = new System.Windows.Forms.FlowLayoutPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.checkBox_ChooseAll = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button_WriteReport
            // 
            this.button_WriteReport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_WriteReport.Location = new System.Drawing.Point(397, 326);
            this.button_WriteReport.Name = "button_WriteReport";
            this.button_WriteReport.Size = new System.Drawing.Size(75, 23);
            this.button_WriteReport.TabIndex = 0;
            this.button_WriteReport.Text = "生成报告";
            this.button_WriteReport.UseVisualStyleBackColor = true;
            this.button_WriteReport.Click += new System.EventHandler(this.button_WriteReport_Click);
            // 
            // button_Close
            // 
            this.button_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Close.Location = new System.Drawing.Point(316, 326);
            this.button_Close.Name = "button_Close";
            this.button_Close.Size = new System.Drawing.Size(75, 23);
            this.button_Close.TabIndex = 2;
            this.button_Close.Text = "关闭";
            this.button_Close.UseVisualStyleBackColor = true;
            // 
            // _bgw_Report
            // 
            this._bgw_Report.WorkerReportsProgress = true;
            this._bgw_Report.WorkerSupportsCancellation = true;
            this._bgw_Report.DoWork += new System.ComponentModel.DoWorkEventHandler(this._bgw_Report_DoWork);
            this._bgw_Report.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this._bgw_Report_ProgressChanged);
            this._bgw_Report.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this._bgw_Report_RunWorkerCompleted);
            // 
            // flowLayoutPanel_Items
            // 
            this.flowLayoutPanel_Items.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel_Items.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel_Items.Name = "flowLayoutPanel_Items";
            this.flowLayoutPanel_Items.Size = new System.Drawing.Size(460, 308);
            this.flowLayoutPanel_Items.TabIndex = 3;
            // 
            // checkBox_ChooseAll
            // 
            this.checkBox_ChooseAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_ChooseAll.AutoSize = true;
            this.checkBox_ChooseAll.Location = new System.Drawing.Point(253, 330);
            this.checkBox_ChooseAll.Name = "checkBox_ChooseAll";
            this.checkBox_ChooseAll.Size = new System.Drawing.Size(48, 16);
            this.checkBox_ChooseAll.TabIndex = 4;
            this.checkBox_ChooseAll.Text = "全选";
            this.checkBox_ChooseAll.UseVisualStyleBackColor = true;
            this.checkBox_ChooseAll.CheckedChanged += new System.EventHandler(this.checkBox_ChooseAll_CheckedChanged);
            // 
            // ResultLister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.checkBox_ChooseAll);
            this.Controls.Add(this.flowLayoutPanel_Items);
            this.Controls.Add(this.button_Close);
            this.Controls.Add(this.button_WriteReport);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "ResultLister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "计算结果";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_WriteReport;
        private System.Windows.Forms.Button button_Close;
        private System.ComponentModel.BackgroundWorker _bgw_Report;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Items;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBox_ChooseAll;
    }
}