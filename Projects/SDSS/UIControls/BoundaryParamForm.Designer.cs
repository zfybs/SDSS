namespace SDSS.UIControls
{
    partial class BoundaryParamForm
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
            this.label_Kx = new System.Windows.Forms.Label();
            this.textBox_Kx = new System.Windows.Forms.TextBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_Ky = new System.Windows.Forms.Label();
            this.textBox_Ky = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_Kx
            // 
            this.label_Kx.AutoSize = true;
            this.label_Kx.Location = new System.Drawing.Point(6, 26);
            this.label_Kx.Name = "label_Kx";
            this.label_Kx.Size = new System.Drawing.Size(71, 12);
            this.label_Kx.TabIndex = 0;
            this.label_Kx.Text = "水平系数Kx:";
            // 
            // textBox_Kx
            // 
            this.textBox_Kx.Location = new System.Drawing.Point(83, 20);
            this.textBox_Kx.Name = "textBox_Kx";
            this.textBox_Kx.Size = new System.Drawing.Size(64, 21);
            this.textBox_Kx.TabIndex = 1;
            this.textBox_Kx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Kx_KeyPress);
            // 
            // button_OK
            // 
            this.button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_OK.Location = new System.Drawing.Point(115, 120);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 2;
            this.button_OK.Text = "确定";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_Ky);
            this.groupBox1.Controls.Add(this.label_Kx);
            this.groupBox1.Controls.Add(this.textBox_Ky);
            this.groupBox1.Controls.Add(this.textBox_Kx);
            this.groupBox1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(20, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 93);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "基床系数";
            // 
            // label_Ky
            // 
            this.label_Ky.AutoSize = true;
            this.label_Ky.Location = new System.Drawing.Point(6, 60);
            this.label_Ky.Name = "label_Ky";
            this.label_Ky.Size = new System.Drawing.Size(71, 12);
            this.label_Ky.TabIndex = 0;
            this.label_Ky.Text = "水平系数Ky:";
            // 
            // textBox_Ky
            // 
            this.textBox_Ky.Location = new System.Drawing.Point(83, 54);
            this.textBox_Ky.Name = "textBox_Ky";
            this.textBox_Ky.Size = new System.Drawing.Size(64, 21);
            this.textBox_Ky.TabIndex = 1;
            this.textBox_Ky.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Kx_KeyPress);
            // 
            // BoundaryParamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(202, 155);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_OK);
            this.Name = "BoundaryParamForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "参数设定";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_Kx;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_Ky;
        private System.Windows.Forms.TextBox textBox_Ky;
        private System.Windows.Forms.TextBox textBox_Kx;
    }
}