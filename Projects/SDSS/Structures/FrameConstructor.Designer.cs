namespace SDSS.Structures
{
    partial class FrameConstructor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_GenerateFrame = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNum_layers = new eZstd.UserControls.TextBoxNum();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNum_spans = new eZstd.UserControls.TextBoxNum();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxProfiles = new System.Windows.Forms.ComboBox();
            this.comboBoxCompMaterials = new System.Windows.Forms.ComboBox();
            this.eZDataGridViewFrame = new eZstd.UserControls.eZDataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.button_assignCompMat = new System.Windows.Forms.Button();
            this.button_assignProfile = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eZDataGridViewFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_GenerateFrame);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBoxNum_layers);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textBoxNum_spans);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(334, 27);
            this.panel1.TabIndex = 28;
            // 
            // button_GenerateFrame
            // 
            this.button_GenerateFrame.Location = new System.Drawing.Point(253, 2);
            this.button_GenerateFrame.Name = "button_GenerateFrame";
            this.button_GenerateFrame.Size = new System.Drawing.Size(75, 23);
            this.button_GenerateFrame.TabIndex = 4;
            this.button_GenerateFrame.Text = "生成";
            this.button_GenerateFrame.UseVisualStyleBackColor = true;
            this.button_GenerateFrame.Click += new System.EventHandler(this.button_GenerateFrame_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "框架构造：";
            // 
            // textBoxNum_layers
            // 
            this.textBoxNum_layers.IntegerOnly = true;
            this.textBoxNum_layers.Location = new System.Drawing.Point(91, 4);
            this.textBoxNum_layers.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxNum_layers.Name = "textBoxNum_layers";
            this.textBoxNum_layers.PositiveOnly = true;
            this.textBoxNum_layers.Size = new System.Drawing.Size(57, 21);
            this.textBoxNum_layers.TabIndex = 19;
            this.textBoxNum_layers.Text = "3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(150, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "层";
            // 
            // textBoxNum_spans
            // 
            this.textBoxNum_spans.IntegerOnly = true;
            this.textBoxNum_spans.Location = new System.Drawing.Point(172, 4);
            this.textBoxNum_spans.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxNum_spans.Name = "textBoxNum_spans";
            this.textBoxNum_spans.PositiveOnly = true;
            this.textBoxNum_spans.Size = new System.Drawing.Size(57, 21);
            this.textBoxNum_spans.TabIndex = 18;
            this.textBoxNum_spans.Text = "2";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(231, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "跨";
            // 
            // comboBoxProfiles
            // 
            this.comboBoxProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfiles.FormattingEnabled = true;
            this.comboBoxProfiles.Location = new System.Drawing.Point(175, 40);
            this.comboBoxProfiles.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxProfiles.Name = "comboBoxProfiles";
            this.comboBoxProfiles.Size = new System.Drawing.Size(60, 20);
            this.comboBoxProfiles.TabIndex = 26;
            // 
            // comboBoxCompMaterials
            // 
            this.comboBoxCompMaterials.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCompMaterials.FormattingEnabled = true;
            this.comboBoxCompMaterials.Location = new System.Drawing.Point(58, 40);
            this.comboBoxCompMaterials.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxCompMaterials.Name = "comboBoxCompMaterials";
            this.comboBoxCompMaterials.Size = new System.Drawing.Size(60, 20);
            this.comboBoxCompMaterials.TabIndex = 27;
            // 
            // eZDataGridViewFrame
            // 
            this.eZDataGridViewFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eZDataGridViewFrame.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eZDataGridViewFrame.KeyDelete = false;
            this.eZDataGridViewFrame.Location = new System.Drawing.Point(5, 62);
            this.eZDataGridViewFrame.ManipulateRows = false;
            this.eZDataGridViewFrame.Margin = new System.Windows.Forms.Padding(2);
            this.eZDataGridViewFrame.Name = "eZDataGridViewFrame";
            this.eZDataGridViewFrame.RowTemplate.Height = 23;
            this.eZDataGridViewFrame.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.eZDataGridViewFrame.ShowRowNumber = false;
            this.eZDataGridViewFrame.Size = new System.Drawing.Size(503, 290);
            this.eZDataGridViewFrame.SupportPaste = false;
            this.eZDataGridViewFrame.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Cursor = System.Windows.Forms.Cursors.Default;
            this.label7.Location = new System.Drawing.Point(5, 45);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "构件参数";
            // 
            // button_assignCompMat
            // 
            this.button_assignCompMat.Location = new System.Drawing.Point(119, 39);
            this.button_assignCompMat.Margin = new System.Windows.Forms.Padding(2);
            this.button_assignCompMat.Name = "button_assignCompMat";
            this.button_assignCompMat.Size = new System.Drawing.Size(43, 23);
            this.button_assignCompMat.TabIndex = 24;
            this.button_assignCompMat.Text = "材料";
            this.button_assignCompMat.UseVisualStyleBackColor = true;
            this.button_assignCompMat.Click += new System.EventHandler(this.button_assignCompMat_Click_1);
            // 
            // button_assignProfile
            // 
            this.button_assignProfile.Location = new System.Drawing.Point(235, 39);
            this.button_assignProfile.Margin = new System.Windows.Forms.Padding(2);
            this.button_assignProfile.Name = "button_assignProfile";
            this.button_assignProfile.Size = new System.Drawing.Size(43, 23);
            this.button_assignProfile.TabIndex = 25;
            this.button_assignProfile.Text = "截面";
            this.button_assignProfile.UseVisualStyleBackColor = true;
            this.button_assignProfile.Click += new System.EventHandler(this.button_assignProfile_Click_1);
            // 
            // FrameConstructor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.comboBoxProfiles);
            this.Controls.Add(this.comboBoxCompMaterials);
            this.Controls.Add(this.eZDataGridViewFrame);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button_assignCompMat);
            this.Controls.Add(this.button_assignProfile);
            this.Name = "FrameConstructor";
            this.Size = new System.Drawing.Size(510, 352);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eZDataGridViewFrame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_GenerateFrame;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private eZstd.UserControls.TextBoxNum textBoxNum_spans;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxProfiles;
        private System.Windows.Forms.ComboBox comboBoxCompMaterials;
        private eZstd.UserControls.eZDataGridView eZDataGridViewFrame;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_assignCompMat;
        private System.Windows.Forms.Button button_assignProfile;
        private eZstd.UserControls.TextBoxNum textBoxNum_layers;
    }
}
