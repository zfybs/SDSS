namespace SDSS.UIControls
{
    partial class MainForm
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
            this.buttonSolve = new System.Windows.Forms.Button();
            this.button_Materials = new System.Windows.Forms.Button();
            this.button_Profiles = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNum_layers = new eZstd.UserControls.TextBoxNum();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxNum_spans = new eZstd.UserControls.TextBoxNum();
            this.button_Boundary = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.comboBoxProfiles = new System.Windows.Forms.ComboBox();
            this.comboBoxMaterials = new System.Windows.Forms.ComboBox();
            this.eZDataGridViewFrame = new eZstd.UserControls.eZDataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.button_assignMat = new System.Windows.Forms.Button();
            this.button_assignProfile = new System.Windows.Forms.Button();
            this.eZDataGridViewSoilLayers = new eZstd.UserControls.eZDataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonSavePic = new System.Windows.Forms.Button();
            this.buttonSysInfo = new System.Windows.Forms.Button();
            this.button_GenerateFrame = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modelDrawer1 = new SDSS.UIControls.ModelDrawer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eZDataGridViewFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eZDataGridViewSoilLayers)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modelDrawer1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSolve
            // 
            this.buttonSolve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSolve.Location = new System.Drawing.Point(769, 561);
            this.buttonSolve.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSolve.Name = "buttonSolve";
            this.buttonSolve.Size = new System.Drawing.Size(100, 30);
            this.buttonSolve.TabIndex = 1;
            this.buttonSolve.Text = "求解";
            this.buttonSolve.UseVisualStyleBackColor = true;
            // 
            // button_Materials
            // 
            this.button_Materials.Location = new System.Drawing.Point(538, 39);
            this.button_Materials.Name = "button_Materials";
            this.button_Materials.Size = new System.Drawing.Size(75, 30);
            this.button_Materials.TabIndex = 2;
            this.button_Materials.Text = "材料";
            this.button_Materials.UseVisualStyleBackColor = true;
            this.button_Materials.Click += new System.EventHandler(this.button_Materials_Click);
            // 
            // button_Profiles
            // 
            this.button_Profiles.Location = new System.Drawing.Point(619, 39);
            this.button_Profiles.Name = "button_Profiles";
            this.button_Profiles.Size = new System.Drawing.Size(75, 30);
            this.button_Profiles.TabIndex = 2;
            this.button_Profiles.Text = "截面";
            this.button_Profiles.UseVisualStyleBackColor = true;
            this.button_Profiles.Click += new System.EventHandler(this.button_Profiles_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(420, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "基本定义管理：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(420, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "框架构造：";
            // 
            // textBoxNum_layers
            // 
            this.textBoxNum_layers.IntegerOnly = true;
            this.textBoxNum_layers.Location = new System.Drawing.Point(538, 84);
            this.textBoxNum_layers.Name = "textBoxNum_layers";
            this.textBoxNum_layers.PositiveOnly = true;
            this.textBoxNum_layers.Size = new System.Drawing.Size(75, 25);
            this.textBoxNum_layers.TabIndex = 4;
            this.textBoxNum_layers.Text = "3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(617, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "层";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(726, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "跨";
            // 
            // textBoxNum_spans
            // 
            this.textBoxNum_spans.IntegerOnly = true;
            this.textBoxNum_spans.Location = new System.Drawing.Point(647, 84);
            this.textBoxNum_spans.Name = "textBoxNum_spans";
            this.textBoxNum_spans.PositiveOnly = true;
            this.textBoxNum_spans.Size = new System.Drawing.Size(75, 25);
            this.textBoxNum_spans.TabIndex = 4;
            this.textBoxNum_spans.Text = "2";
            // 
            // button_Boundary
            // 
            this.button_Boundary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Boundary.Location = new System.Drawing.Point(475, 561);
            this.button_Boundary.Name = "button_Boundary";
            this.button_Boundary.Size = new System.Drawing.Size(100, 30);
            this.button_Boundary.TabIndex = 2;
            this.button_Boundary.Text = "边界参数";
            this.button_Boundary.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Location = new System.Drawing.Point(419, 115);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxProfiles);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxMaterials);
            this.splitContainer1.Panel1.Controls.Add(this.eZDataGridViewFrame);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.button_assignMat);
            this.splitContainer1.Panel1.Controls.Add(this.button_assignProfile);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.eZDataGridViewSoilLayers);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Size = new System.Drawing.Size(451, 440);
            this.splitContainer1.SplitterDistance = 299;
            this.splitContainer1.TabIndex = 7;
            // 
            // comboBoxProfiles
            // 
            this.comboBoxProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfiles.FormattingEnabled = true;
            this.comboBoxProfiles.Location = new System.Drawing.Point(230, 6);
            this.comboBoxProfiles.Name = "comboBoxProfiles";
            this.comboBoxProfiles.Size = new System.Drawing.Size(78, 23);
            this.comboBoxProfiles.TabIndex = 3;
            // 
            // comboBoxMaterials
            // 
            this.comboBoxMaterials.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMaterials.FormattingEnabled = true;
            this.comboBoxMaterials.Location = new System.Drawing.Point(73, 6);
            this.comboBoxMaterials.Name = "comboBoxMaterials";
            this.comboBoxMaterials.Size = new System.Drawing.Size(78, 23);
            this.comboBoxMaterials.TabIndex = 3;
            // 
            // eZDataGridViewFrame
            // 
            this.eZDataGridViewFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eZDataGridViewFrame.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eZDataGridViewFrame.KeyDelete = false;
            this.eZDataGridViewFrame.Location = new System.Drawing.Point(3, 34);
            this.eZDataGridViewFrame.ManipulateRows = false;
            this.eZDataGridViewFrame.Name = "eZDataGridViewFrame";
            this.eZDataGridViewFrame.RowTemplate.Height = 23;
            this.eZDataGridViewFrame.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.eZDataGridViewFrame.ShowRowNumber = false;
            this.eZDataGridViewFrame.Size = new System.Drawing.Size(445, 262);
            this.eZDataGridViewFrame.SupportPaste = false;
            this.eZDataGridViewFrame.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Cursor = System.Windows.Forms.Cursors.Default;
            this.label7.Location = new System.Drawing.Point(3, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "构件参数";
            // 
            // button_assignMat
            // 
            this.button_assignMat.Location = new System.Drawing.Point(154, 5);
            this.button_assignMat.Name = "button_assignMat";
            this.button_assignMat.Size = new System.Drawing.Size(57, 25);
            this.button_assignMat.TabIndex = 2;
            this.button_assignMat.Text = "材料";
            this.button_assignMat.UseVisualStyleBackColor = true;
            // 
            // button_assignProfile
            // 
            this.button_assignProfile.Location = new System.Drawing.Point(310, 5);
            this.button_assignProfile.Name = "button_assignProfile";
            this.button_assignProfile.Size = new System.Drawing.Size(57, 25);
            this.button_assignProfile.TabIndex = 2;
            this.button_assignProfile.Text = "截面";
            this.button_assignProfile.UseVisualStyleBackColor = true;
            // 
            // eZDataGridViewSoilLayers
            // 
            this.eZDataGridViewSoilLayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eZDataGridViewSoilLayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eZDataGridViewSoilLayers.KeyDelete = false;
            this.eZDataGridViewSoilLayers.Location = new System.Drawing.Point(3, 26);
            this.eZDataGridViewSoilLayers.ManipulateRows = false;
            this.eZDataGridViewSoilLayers.Name = "eZDataGridViewSoilLayers";
            this.eZDataGridViewSoilLayers.RowTemplate.Height = 23;
            this.eZDataGridViewSoilLayers.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.eZDataGridViewSoilLayers.ShowRowNumber = false;
            this.eZDataGridViewSoilLayers.Size = new System.Drawing.Size(445, 108);
            this.eZDataGridViewSoilLayers.SupportPaste = false;
            this.eZDataGridViewSoilLayers.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Cursor = System.Windows.Forms.Cursors.Default;
            this.label8.Location = new System.Drawing.Point(3, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "土层参数";
            // 
            // buttonSavePic
            // 
            this.buttonSavePic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSavePic.Location = new System.Drawing.Point(12, 561);
            this.buttonSavePic.Name = "buttonSavePic";
            this.buttonSavePic.Size = new System.Drawing.Size(100, 30);
            this.buttonSavePic.TabIndex = 2;
            this.buttonSavePic.Text = "保存图片";
            this.buttonSavePic.UseVisualStyleBackColor = true;
            // 
            // buttonSysInfo
            // 
            this.buttonSysInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSysInfo.Location = new System.Drawing.Point(292, 561);
            this.buttonSysInfo.Name = "buttonSysInfo";
            this.buttonSysInfo.Size = new System.Drawing.Size(100, 30);
            this.buttonSysInfo.TabIndex = 2;
            this.buttonSysInfo.Text = "系统信息";
            this.buttonSysInfo.UseVisualStyleBackColor = true;
            // 
            // button_GenerateFrame
            // 
            this.button_GenerateFrame.Location = new System.Drawing.Point(754, 83);
            this.button_GenerateFrame.Margin = new System.Windows.Forms.Padding(0);
            this.button_GenerateFrame.Name = "button_GenerateFrame";
            this.button_GenerateFrame.Size = new System.Drawing.Size(75, 30);
            this.button_GenerateFrame.TabIndex = 2;
            this.button_GenerateFrame.Text = "生成";
            this.toolTip1.SetToolTip(this.button_GenerateFrame, "生成车站框架");
            this.button_GenerateFrame.UseVisualStyleBackColor = true;
            this.button_GenerateFrame.Click += new System.EventHandler(this.button_GenerateFrame_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(882, 28);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导出ToolStripMenuItem,
            this.导出ToolStripMenuItem1,
            this.toolStripSeparator1,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(51, 24);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 导出ToolStripMenuItem
            // 
            this.导出ToolStripMenuItem.Name = "导出ToolStripMenuItem";
            this.导出ToolStripMenuItem.Size = new System.Drawing.Size(114, 26);
            this.导出ToolStripMenuItem.Text = "导入";
            // 
            // 导出ToolStripMenuItem1
            // 
            this.导出ToolStripMenuItem1.Name = "导出ToolStripMenuItem1";
            this.导出ToolStripMenuItem1.Size = new System.Drawing.Size(114, 26);
            this.导出ToolStripMenuItem1.Text = "导出";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(111, 6);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(114, 26);
            this.退出ToolStripMenuItem.Text = "退出";
            // 
            // modelDrawer1
            // 
            this.modelDrawer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.modelDrawer1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.modelDrawer1.Location = new System.Drawing.Point(13, 39);
            this.modelDrawer1.Margin = new System.Windows.Forms.Padding(4);
            this.modelDrawer1.Name = "modelDrawer1";
            this.modelDrawer1.Size = new System.Drawing.Size(379, 515);
            this.modelDrawer1.TabIndex = 0;
            this.modelDrawer1.TabStop = false;
            this.modelDrawer1.Paint += new System.Windows.Forms.PaintEventHandler(this.modelDrawer1_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 603);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.textBoxNum_spans);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxNum_layers);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_GenerateFrame);
            this.Controls.Add(this.button_Profiles);
            this.Controls.Add(this.buttonSysInfo);
            this.Controls.Add(this.buttonSavePic);
            this.Controls.Add(this.button_Boundary);
            this.Controls.Add(this.button_Materials);
            this.Controls.Add(this.buttonSolve);
            this.Controls.Add(this.modelDrawer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(900, 650);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地铁车站抗震设计";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eZDataGridViewFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eZDataGridViewSoilLayers)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modelDrawer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ModelDrawer modelDrawer1;
        private System.Windows.Forms.Button buttonSolve;
        private System.Windows.Forms.Button button_Materials;
        private System.Windows.Forms.Button button_Profiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private eZstd.UserControls.TextBoxNum textBoxNum_layers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private eZstd.UserControls.TextBoxNum textBoxNum_spans;
        private System.Windows.Forms.Button button_Boundary;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label7;
        private eZstd.UserControls.eZDataGridView eZDataGridViewFrame;
        private eZstd.UserControls.eZDataGridView eZDataGridViewSoilLayers;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonSavePic;
        private System.Windows.Forms.Button buttonSysInfo;
        private System.Windows.Forms.Button button_assignMat;
        private System.Windows.Forms.Button button_assignProfile;
        private System.Windows.Forms.ComboBox comboBoxProfiles;
        private System.Windows.Forms.ComboBox comboBoxMaterials;
        private System.Windows.Forms.Button button_GenerateFrame;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
    }
}

