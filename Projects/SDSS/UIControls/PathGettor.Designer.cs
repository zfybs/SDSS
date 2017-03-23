namespace SDSS.UIControls
{
    partial class PathGettor
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
            this.textBox_Dir = new System.Windows.Forms.TextBox();
            this.button_Dir = new System.Windows.Forms.Button();
            this.button_Ok = new System.Windows.Forms.Button();
            this.label_tip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_Dir
            // 
            this.textBox_Dir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Dir.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_Dir.Location = new System.Drawing.Point(12, 12);
            this.textBox_Dir.Name = "textBox_Dir";
            this.textBox_Dir.Size = new System.Drawing.Size(279, 23);
            this.textBox_Dir.TabIndex = 3;
            // 
            // button_Dir
            // 
            this.button_Dir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Dir.Location = new System.Drawing.Point(297, 12);
            this.button_Dir.Name = "button_Dir";
            this.button_Dir.Size = new System.Drawing.Size(49, 23);
            this.button_Dir.TabIndex = 4;
            this.button_Dir.Text = "浏览";
            this.button_Dir.UseVisualStyleBackColor = true;
            this.button_Dir.Click += new System.EventHandler(this.button_Dir_Click);
            // 
            // button_Ok
            // 
            this.button_Ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Ok.Location = new System.Drawing.Point(297, 42);
            this.button_Ok.Name = "button_Ok";
            this.button_Ok.Size = new System.Drawing.Size(49, 23);
            this.button_Ok.TabIndex = 5;
            this.button_Ok.Text = "确定";
            this.button_Ok.UseVisualStyleBackColor = true;
            this.button_Ok.Click += new System.EventHandler(this.button_Ok_Click);
            // 
            // label_tip
            // 
            this.label_tip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_tip.AutoSize = true;
            this.label_tip.Location = new System.Drawing.Point(12, 52);
            this.label_tip.Name = "label_tip";
            this.label_tip.Size = new System.Drawing.Size(41, 12);
            this.label_tip.TabIndex = 6;
            this.label_tip.Text = "提示：";
            // 
            // PathGettor
            // 
            this.AcceptButton = this.button_Dir;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 77);
            this.Controls.Add(this.label_tip);
            this.Controls.Add(this.button_Ok);
            this.Controls.Add(this.textBox_Dir);
            this.Controls.Add(this.button_Dir);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(100000000, 116);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(16, 116);
            this.Name = "PathGettor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "文件夹路径";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_Dir;
        private System.Windows.Forms.Button button_Dir;
        private System.Windows.Forms.Button button_Ok;
        private System.Windows.Forms.Label label_tip;
    }
}