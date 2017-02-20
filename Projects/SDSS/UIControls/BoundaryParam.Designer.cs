namespace SDSS.UIControls
{
    partial class BoundaryParam
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
            this.label_overSoil = new System.Windows.Forms.Label();
            this.textBox_overSoil = new System.Windows.Forms.TextBox();
            this.label_soilWidth = new System.Windows.Forms.Label();
            this.textBox_soilWidth = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.button_OK.Location = new System.Drawing.Point(126, 212);
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
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
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
            // label_overSoil
            // 
            this.label_overSoil.AutoSize = true;
            this.label_overSoil.Location = new System.Drawing.Point(25, 128);
            this.label_overSoil.Name = "label_overSoil";
            this.label_overSoil.Size = new System.Drawing.Size(77, 12);
            this.label_overSoil.TabIndex = 4;
            this.label_overSoil.Text = "上覆土厚度：";
            // 
            // textBox_overSoil
            // 
            this.textBox_overSoil.Location = new System.Drawing.Point(102, 125);
            this.textBox_overSoil.Name = "textBox_overSoil";
            this.textBox_overSoil.Size = new System.Drawing.Size(53, 21);
            this.textBox_overSoil.TabIndex = 5;
            this.textBox_overSoil.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Kx_KeyPress);
            // 
            // label_soilWidth
            // 
            this.label_soilWidth.AutoSize = true;
            this.label_soilWidth.Location = new System.Drawing.Point(25, 167);
            this.label_soilWidth.Name = "label_soilWidth";
            this.label_soilWidth.Size = new System.Drawing.Size(65, 12);
            this.label_soilWidth.TabIndex = 4;
            this.label_soilWidth.Text = "土层宽度：";
            // 
            // textBox_soilWidth
            // 
            this.textBox_soilWidth.Location = new System.Drawing.Point(102, 164);
            this.textBox_soilWidth.Name = "textBox_soilWidth";
            this.textBox_soilWidth.Size = new System.Drawing.Size(53, 21);
            this.textBox_soilWidth.TabIndex = 5;
            this.textBox_soilWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_Kx_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(161, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 14);
            this.label1.TabIndex = 4;
            this.label1.Text = "m";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(161, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 14);
            this.label2.TabIndex = 4;
            this.label2.Text = "m";
            // 
            // BoundaryParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 250);
            this.Controls.Add(this.textBox_soilWidth);
            this.Controls.Add(this.label_soilWidth);
            this.Controls.Add(this.textBox_overSoil);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_overSoil);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_OK);
            this.Name = "BoundaryParam";
            this.Text = "参数设定";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Kx;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_Ky;
        private System.Windows.Forms.TextBox textBox_Ky;
        private System.Windows.Forms.Label label_overSoil;
        private System.Windows.Forms.TextBox textBox_overSoil;
        private System.Windows.Forms.Label label_soilWidth;
        private System.Windows.Forms.TextBox textBox_soilWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_Kx;
    }
}