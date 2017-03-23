using System.Windows.Forms;

namespace SDSS.Structures
{
    partial class TunnelConstructor
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_GenerateTunnel = new System.Windows.Forms.Button();
            this.textBoxNum_Segments = new eZstd.UserControls.TextBoxNum();
            this.textBoxNum_Radius = new eZstd.UserControls.TextBoxNum();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxProfiles = new System.Windows.Forms.ComboBox();
            this.comboBoxCompMaterials = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button_assignCompMat = new System.Windows.Forms.Button();
            this.button_assignProfile = new System.Windows.Forms.Button();
            this.eZDataGridViewTunnel = new eZstd.UserControls.eZDataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eZDataGridViewTunnel)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_GenerateTunnel);
            this.panel1.Controls.Add(this.textBoxNum_Segments);
            this.panel1.Controls.Add(this.textBoxNum_Radius);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(388, 27);
            this.panel1.TabIndex = 0;
            // 
            // button_GenerateTunnel
            // 
            this.button_GenerateTunnel.Location = new System.Drawing.Point(311, 2);
            this.button_GenerateTunnel.Name = "button_GenerateTunnel";
            this.button_GenerateTunnel.Size = new System.Drawing.Size(75, 23);
            this.button_GenerateTunnel.TabIndex = 3;
            this.button_GenerateTunnel.Text = "生成";
            this.button_GenerateTunnel.UseVisualStyleBackColor = true;
            this.button_GenerateTunnel.Click += new System.EventHandler(this.button_GenerateTunnel_Click);
            // 
            // textBoxNum_Segments
            // 
            this.textBoxNum_Segments.Location = new System.Drawing.Point(229, 3);
            this.textBoxNum_Segments.Name = "textBoxNum_Segments";
            this.textBoxNum_Segments.Size = new System.Drawing.Size(40, 21);
            this.textBoxNum_Segments.TabIndex = 2;
            this.textBoxNum_Segments.Text = "6";
            this.textBoxNum_Segments.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxNum_Segments_KeyPress);
            // 
            // textBoxNum_Radius
            // 
            this.textBoxNum_Radius.Location = new System.Drawing.Point(119, 3);
            this.textBoxNum_Radius.Name = "textBoxNum_Radius";
            this.textBoxNum_Radius.Size = new System.Drawing.Size(40, 21);
            this.textBoxNum_Radius.TabIndex = 2;
            this.textBoxNum_Radius.Text = "3";
            this.textBoxNum_Radius.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxNum_Radius_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(186, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "管片数";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(272, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "片";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(162, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "m";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(88, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "半径";
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "圆形隧道：";
            // 
            // comboBoxProfiles
            // 
            this.comboBoxProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfiles.FormattingEnabled = true;
            this.comboBoxProfiles.Location = new System.Drawing.Point(177, 45);
            this.comboBoxProfiles.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxProfiles.Name = "comboBoxProfiles";
            this.comboBoxProfiles.Size = new System.Drawing.Size(60, 20);
            this.comboBoxProfiles.TabIndex = 31;
            // 
            // comboBoxCompMaterials
            // 
            this.comboBoxCompMaterials.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCompMaterials.FormattingEnabled = true;
            this.comboBoxCompMaterials.Location = new System.Drawing.Point(60, 45);
            this.comboBoxCompMaterials.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxCompMaterials.Name = "comboBoxCompMaterials";
            this.comboBoxCompMaterials.Size = new System.Drawing.Size(60, 20);
            this.comboBoxCompMaterials.TabIndex = 32;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Cursor = System.Windows.Forms.Cursors.Default;
            this.label7.Location = new System.Drawing.Point(7, 50);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 28;
            this.label7.Text = "构件参数";
            // 
            // button_assignCompMat
            // 
            this.button_assignCompMat.Location = new System.Drawing.Point(121, 44);
            this.button_assignCompMat.Margin = new System.Windows.Forms.Padding(2);
            this.button_assignCompMat.Name = "button_assignCompMat";
            this.button_assignCompMat.Size = new System.Drawing.Size(43, 23);
            this.button_assignCompMat.TabIndex = 29;
            this.button_assignCompMat.Text = "材料";
            this.button_assignCompMat.UseVisualStyleBackColor = true;
            this.button_assignCompMat.Click += new System.EventHandler(this.button_assignCompMat_Click);
            // 
            // button_assignProfile
            // 
            this.button_assignProfile.Location = new System.Drawing.Point(237, 44);
            this.button_assignProfile.Margin = new System.Windows.Forms.Padding(2);
            this.button_assignProfile.Name = "button_assignProfile";
            this.button_assignProfile.Size = new System.Drawing.Size(43, 23);
            this.button_assignProfile.TabIndex = 30;
            this.button_assignProfile.Text = "截面";
            this.button_assignProfile.UseVisualStyleBackColor = true;
            this.button_assignProfile.Click += new System.EventHandler(this.button_assignProfile_Click);
            // 
            // eZDataGridViewTunnel
            // 
            this.eZDataGridViewTunnel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eZDataGridViewTunnel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eZDataGridViewTunnel.KeyDelete = false;
            this.eZDataGridViewTunnel.Location = new System.Drawing.Point(-1, 72);
            this.eZDataGridViewTunnel.ManipulateRows = false;
            this.eZDataGridViewTunnel.Name = "eZDataGridViewTunnel";
            this.eZDataGridViewTunnel.RowTemplate.Height = 23;
            this.eZDataGridViewTunnel.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.eZDataGridViewTunnel.ShowRowNumber = false;
            this.eZDataGridViewTunnel.Size = new System.Drawing.Size(464, 240);
            this.eZDataGridViewTunnel.SupportPaste = false;
            this.eZDataGridViewTunnel.TabIndex = 33;
            // 
            // TunnelConstructor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.eZDataGridViewTunnel);
            this.Controls.Add(this.comboBoxProfiles);
            this.Controls.Add(this.comboBoxCompMaterials);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button_assignCompMat);
            this.Controls.Add(this.button_assignProfile);
            this.Controls.Add(this.panel1);
            this.Name = "TunnelConstructor";
            this.Size = new System.Drawing.Size(463, 311);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eZDataGridViewTunnel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private eZstd.UserControls.TextBoxNum textBoxNum_Segments;
        private eZstd.UserControls.TextBoxNum textBoxNum_Radius;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button_GenerateTunnel;
        private System.Windows.Forms.ComboBox comboBoxProfiles;
        private System.Windows.Forms.ComboBox comboBoxCompMaterials;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button_assignCompMat;
        private System.Windows.Forms.Button button_assignProfile;
        private eZstd.UserControls.eZDataGridView eZDataGridViewTunnel;

        private void textBoxNum_Segments_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 46 || e.KeyChar == 45)
            {
                e.Handled = true;
            }
        }
        private void textBoxNum_Radius_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 45)
            {
                e.Handled = true;
            }
        }
        
    }
}
