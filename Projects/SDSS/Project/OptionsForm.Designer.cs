
namespace SDSS.Project
{
    partial class OptionsForm
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
            this.label_WorkDr = new System.Windows.Forms.Label();
            this.textBox_File = new System.Windows.Forms.TextBox();
            this.button_File = new System.Windows.Forms.Button();
            this.checkBox_Report = new System.Windows.Forms.CheckBox();
            this.label_Time = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBox_WaitingSec = new System.Windows.Forms.TextBox();
            this.comboBox_GUI = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_WT = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_WorkDr
            // 
            this.label_WorkDr.AutoSize = true;
            this.label_WorkDr.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_WorkDr.Location = new System.Drawing.Point(3, 10);
            this.label_WorkDr.Name = "label_WorkDr";
            this.label_WorkDr.Size = new System.Drawing.Size(101, 12);
            this.label_WorkDr.TabIndex = 0;
            this.label_WorkDr.Text = "默认工作文件夹：";
            // 
            // textBox_File
            // 
            this.textBox_File.Enabled = false;
            this.textBox_File.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_File.Location = new System.Drawing.Point(110, 5);
            this.textBox_File.Name = "textBox_File";
            this.textBox_File.Size = new System.Drawing.Size(260, 23);
            this.textBox_File.TabIndex = 1;
            this.textBox_File.TextChanged += new System.EventHandler(this.textBox_File_TextChanged);
            // 
            // button_File
            // 
            this.button_File.Location = new System.Drawing.Point(376, 4);
            this.button_File.Name = "button_File";
            this.button_File.Size = new System.Drawing.Size(49, 23);
            this.button_File.TabIndex = 2;
            this.button_File.Text = "浏览";
            this.button_File.UseVisualStyleBackColor = true;
            this.button_File.Click += new System.EventHandler(this.button_File_Click);
            // 
            // checkBox_Report
            // 
            this.checkBox_Report.AutoSize = true;
            this.checkBox_Report.Location = new System.Drawing.Point(3, 6);
            this.checkBox_Report.Name = "checkBox_Report";
            this.checkBox_Report.Size = new System.Drawing.Size(144, 16);
            this.checkBox_Report.TabIndex = 7;
            this.checkBox_Report.Text = "计算完成直接生成报告";
            this.checkBox_Report.UseVisualStyleBackColor = true;
            this.checkBox_Report.CheckedChanged += new System.EventHandler(this.checkBox_Report_CheckedChanged);
            // 
            // label_Time
            // 
            this.label_Time.AutoSize = true;
            this.label_Time.Location = new System.Drawing.Point(3, 11);
            this.label_Time.Name = "label_Time";
            this.label_Time.Size = new System.Drawing.Size(125, 12);
            this.label_Time.TabIndex = 9;
            this.label_Time.Text = "Abaqus计算等待时间：";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(373, 235);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 10;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBox_WaitingSec
            // 
            this.textBox_WaitingSec.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_WaitingSec.Location = new System.Drawing.Point(134, 3);
            this.textBox_WaitingSec.Name = "textBox_WaitingSec";
            this.textBox_WaitingSec.Size = new System.Drawing.Size(58, 23);
            this.textBox_WaitingSec.TabIndex = 11;
            this.textBox_WaitingSec.TextChanged += new System.EventHandler(this.textBox_WaitingSec_TextChanged);
            this.textBox_WaitingSec.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_WaitingSec_KeyPress);
            // 
            // comboBox_GUI
            // 
            this.comboBox_GUI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_GUI.FormattingEnabled = true;
            this.comboBox_GUI.Location = new System.Drawing.Point(108, 4);
            this.comboBox_GUI.Name = "comboBox_GUI";
            this.comboBox_GUI.Size = new System.Drawing.Size(137, 20);
            this.comboBox_GUI.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "Abaqus显示方式：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(198, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "秒(S)";
            // 
            // comboBox_WT
            // 
            this.comboBox_WT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_WT.FormattingEnabled = true;
            this.comboBox_WT.Location = new System.Drawing.Point(108, 4);
            this.comboBox_WT.Name = "comboBox_WT";
            this.comboBox_WT.Size = new System.Drawing.Size(137, 20);
            this.comboBox_WT.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "报告模板：";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(434, 213);
            this.tableLayoutPanel1.TabIndex = 18;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_WorkDr);
            this.panel1.Controls.Add(this.textBox_File);
            this.panel1.Controls.Add(this.button_File);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(426, 35);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label_Time);
            this.panel2.Controls.Add(this.textBox_WaitingSec);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(4, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(426, 35);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.comboBox_GUI);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(4, 130);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(426, 35);
            this.panel4.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.comboBox_WT);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(4, 172);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(426, 37);
            this.panel5.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.checkBox_Report);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(4, 88);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(426, 35);
            this.panel3.TabIndex = 5;
            // 
            // OptionsForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 270);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.buttonOK);
            this.Name = "OptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选项";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionsForm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_WorkDr;
        private System.Windows.Forms.TextBox textBox_File;
        private System.Windows.Forms.Button button_File;
        private System.Windows.Forms.CheckBox checkBox_Report;
        private System.Windows.Forms.Label label_Time;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBox_WaitingSec;
        private System.Windows.Forms.ComboBox comboBox_GUI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox_WT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
    }
}