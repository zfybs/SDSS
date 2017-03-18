namespace SDSS.ModelForms
{
    partial class ModelOptions
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
            this.textBox_ModelName = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.button_WkDir = new System.Windows.Forms.Button();
            this.textBox_WkDir = new System.Windows.Forms.TextBox();
            this.label_WorkDr = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_ModelName
            // 
            this.textBox_ModelName.Location = new System.Drawing.Point(208, 15);
            this.textBox_ModelName.Name = "textBox_ModelName";
            this.textBox_ModelName.Size = new System.Drawing.Size(141, 21);
            this.textBox_ModelName.TabIndex = 0;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(331, 81);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "确定";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // button_WkDir
            // 
            this.button_WkDir.Location = new System.Drawing.Point(355, 46);
            this.button_WkDir.Name = "button_WkDir";
            this.button_WkDir.Size = new System.Drawing.Size(49, 23);
            this.button_WkDir.TabIndex = 5;
            this.button_WkDir.Text = "浏览";
            this.button_WkDir.UseVisualStyleBackColor = true;
            this.button_WkDir.Click += new System.EventHandler(this.button_WkDir_Click);
            // 
            // textBox_WkDir
            // 
            this.textBox_WkDir.Enabled = false;
            this.textBox_WkDir.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_WkDir.Location = new System.Drawing.Point(108, 46);
            this.textBox_WkDir.Name = "textBox_WkDir";
            this.textBox_WkDir.Size = new System.Drawing.Size(241, 23);
            this.textBox_WkDir.TabIndex = 4;
            this.textBox_WkDir.TextChanged += new System.EventHandler(this.textBox_WkDir_TextChanged);
            // 
            // label_WorkDr
            // 
            this.label_WorkDr.AutoSize = true;
            this.label_WorkDr.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_WorkDr.Location = new System.Drawing.Point(12, 51);
            this.label_WorkDr.Name = "label_WorkDr";
            this.label_WorkDr.Size = new System.Drawing.Size(101, 12);
            this.label_WorkDr.TabIndex = 3;
            this.label_WorkDr.Text = "当前工作文件夹：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(185, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "当前模型名称（不能出现中文）：";
            // 
            // ModelOptions
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 114);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_WkDir);
            this.Controls.Add(this.textBox_WkDir);
            this.Controls.Add(this.label_WorkDr);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBox_ModelName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModelOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ModelOptions";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ModelOptions_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_ModelName;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button button_WkDir;
        private System.Windows.Forms.TextBox textBox_WkDir;
        private System.Windows.Forms.Label label_WorkDr;
        private System.Windows.Forms.Label label1;
    }
}