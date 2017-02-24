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
            this.comboBoxCompMaterials = new System.Windows.Forms.ComboBox();
            this.eZDataGridViewFrame = new eZstd.UserControls.eZDataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.button_assignCompMat = new System.Windows.Forms.Button();
            this.button_assignProfile = new System.Windows.Forms.Button();
            this.comboBoxSoilMaterials = new System.Windows.Forms.ComboBox();
            this.button_assignSoilMat = new System.Windows.Forms.Button();
            this.eZDataGridViewSoilLayers = new eZstd.UserControls.eZDataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonSavePic = new System.Windows.Forms.Button();
            this.buttonSysInfo = new System.Windows.Forms.Button();
            this.button_GenerateFrame = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.TSM_File = new System.Windows.Forms.ToolStripMenuItem();
            this.TSM_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.TSM_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TSM_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文档ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.modelDrawer1 = new SDSS.UIControls.ModelDrawer();
            this.项目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工作文件夹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.buttonSolve.Location = new System.Drawing.Point(782, 547);
            this.buttonSolve.Name = "buttonSolve";
            this.buttonSolve.Size = new System.Drawing.Size(75, 24);
            this.buttonSolve.TabIndex = 1;
            this.buttonSolve.Text = "求解";
            this.buttonSolve.UseVisualStyleBackColor = true;
            this.buttonSolve.Click += new System.EventHandler(this.buttonSolve_Click);
            // 
            // button_Materials
            // 
            this.button_Materials.Location = new System.Drawing.Point(404, 31);
            this.button_Materials.Margin = new System.Windows.Forms.Padding(2);
            this.button_Materials.Name = "button_Materials";
            this.button_Materials.Size = new System.Drawing.Size(56, 24);
            this.button_Materials.TabIndex = 2;
            this.button_Materials.Text = "材料";
            this.button_Materials.UseVisualStyleBackColor = true;
            this.button_Materials.Click += new System.EventHandler(this.button_Materials_Click);
            // 
            // button_Profiles
            // 
            this.button_Profiles.Location = new System.Drawing.Point(464, 31);
            this.button_Profiles.Margin = new System.Windows.Forms.Padding(2);
            this.button_Profiles.Name = "button_Profiles";
            this.button_Profiles.Size = new System.Drawing.Size(56, 24);
            this.button_Profiles.TabIndex = 2;
            this.button_Profiles.Text = "截面";
            this.button_Profiles.UseVisualStyleBackColor = true;
            this.button_Profiles.Click += new System.EventHandler(this.button_Profiles_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(315, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "基本定义管理：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(315, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "框架构造：";
            // 
            // textBoxNum_layers
            // 
            this.textBoxNum_layers.IntegerOnly = true;
            this.textBoxNum_layers.Location = new System.Drawing.Point(404, 67);
            this.textBoxNum_layers.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxNum_layers.Name = "textBoxNum_layers";
            this.textBoxNum_layers.PositiveOnly = true;
            this.textBoxNum_layers.Size = new System.Drawing.Size(57, 21);
            this.textBoxNum_layers.TabIndex = 4;
            this.textBoxNum_layers.Text = "3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(463, 72);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "层";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(544, 72);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "跨";
            // 
            // textBoxNum_spans
            // 
            this.textBoxNum_spans.IntegerOnly = true;
            this.textBoxNum_spans.Location = new System.Drawing.Point(485, 67);
            this.textBoxNum_spans.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxNum_spans.Name = "textBoxNum_spans";
            this.textBoxNum_spans.PositiveOnly = true;
            this.textBoxNum_spans.Size = new System.Drawing.Size(57, 21);
            this.textBoxNum_spans.TabIndex = 4;
            this.textBoxNum_spans.Text = "2";
            // 
            // button_Boundary
            // 
            this.button_Boundary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Boundary.Location = new System.Drawing.Point(561, 547);
            this.button_Boundary.Margin = new System.Windows.Forms.Padding(2);
            this.button_Boundary.Name = "button_Boundary";
            this.button_Boundary.Size = new System.Drawing.Size(75, 24);
            this.button_Boundary.TabIndex = 2;
            this.button_Boundary.Text = "边界参数";
            this.button_Boundary.UseVisualStyleBackColor = true;
            this.button_Boundary.Click += new System.EventHandler(this.button_Boundary_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Location = new System.Drawing.Point(314, 92);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxProfiles);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxCompMaterials);
            this.splitContainer1.Panel1.Controls.Add(this.eZDataGridViewFrame);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.button_assignCompMat);
            this.splitContainer1.Panel1.Controls.Add(this.button_assignProfile);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.comboBoxSoilMaterials);
            this.splitContainer1.Panel2.Controls.Add(this.button_assignSoilMat);
            this.splitContainer1.Panel2.Controls.Add(this.eZDataGridViewSoilLayers);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Size = new System.Drawing.Size(543, 450);
            this.splitContainer1.SplitterDistance = 263;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 7;
            // 
            // comboBoxProfiles
            // 
            this.comboBoxProfiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProfiles.FormattingEnabled = true;
            this.comboBoxProfiles.Location = new System.Drawing.Point(172, 5);
            this.comboBoxProfiles.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxProfiles.Name = "comboBoxProfiles";
            this.comboBoxProfiles.Size = new System.Drawing.Size(60, 20);
            this.comboBoxProfiles.TabIndex = 3;
            // 
            // comboBoxCompMaterials
            // 
            this.comboBoxCompMaterials.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCompMaterials.FormattingEnabled = true;
            this.comboBoxCompMaterials.Location = new System.Drawing.Point(55, 5);
            this.comboBoxCompMaterials.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxCompMaterials.Name = "comboBoxCompMaterials";
            this.comboBoxCompMaterials.Size = new System.Drawing.Size(60, 20);
            this.comboBoxCompMaterials.TabIndex = 3;
            // 
            // eZDataGridViewFrame
            // 
            this.eZDataGridViewFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eZDataGridViewFrame.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eZDataGridViewFrame.KeyDelete = false;
            this.eZDataGridViewFrame.Location = new System.Drawing.Point(2, 27);
            this.eZDataGridViewFrame.ManipulateRows = false;
            this.eZDataGridViewFrame.Margin = new System.Windows.Forms.Padding(2);
            this.eZDataGridViewFrame.Name = "eZDataGridViewFrame";
            this.eZDataGridViewFrame.RowTemplate.Height = 23;
            this.eZDataGridViewFrame.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.eZDataGridViewFrame.ShowRowNumber = false;
            this.eZDataGridViewFrame.Size = new System.Drawing.Size(539, 234);
            this.eZDataGridViewFrame.SupportPaste = false;
            this.eZDataGridViewFrame.TabIndex = 1;
            this.eZDataGridViewFrame.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.eZDataGridViewFrame_DataError);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Cursor = System.Windows.Forms.Cursors.Default;
            this.label7.Location = new System.Drawing.Point(2, 10);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "构件参数";
            // 
            // button_assignCompMat
            // 
            this.button_assignCompMat.Location = new System.Drawing.Point(116, 4);
            this.button_assignCompMat.Margin = new System.Windows.Forms.Padding(2);
            this.button_assignCompMat.Name = "button_assignCompMat";
            this.button_assignCompMat.Size = new System.Drawing.Size(43, 23);
            this.button_assignCompMat.TabIndex = 2;
            this.button_assignCompMat.Text = "材料";
            this.toolTip1.SetToolTip(this.button_assignCompMat, "为表格中选择的构件行指定材料");
            this.button_assignCompMat.UseVisualStyleBackColor = true;
            this.button_assignCompMat.Click += new System.EventHandler(this.button_assignCompMat_Click);
            // 
            // button_assignProfile
            // 
            this.button_assignProfile.Location = new System.Drawing.Point(232, 4);
            this.button_assignProfile.Margin = new System.Windows.Forms.Padding(2);
            this.button_assignProfile.Name = "button_assignProfile";
            this.button_assignProfile.Size = new System.Drawing.Size(43, 23);
            this.button_assignProfile.TabIndex = 2;
            this.button_assignProfile.Text = "截面";
            this.toolTip1.SetToolTip(this.button_assignProfile, "为表格中选择的构件行指定截面");
            this.button_assignProfile.UseVisualStyleBackColor = true;
            this.button_assignProfile.Click += new System.EventHandler(this.button_assignCompProfile_Click);
            // 
            // comboBoxSoilMaterials
            // 
            this.comboBoxSoilMaterials.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSoilMaterials.FormattingEnabled = true;
            this.comboBoxSoilMaterials.Location = new System.Drawing.Point(59, 3);
            this.comboBoxSoilMaterials.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxSoilMaterials.Name = "comboBoxSoilMaterials";
            this.comboBoxSoilMaterials.Size = new System.Drawing.Size(60, 20);
            this.comboBoxSoilMaterials.TabIndex = 5;
            // 
            // button_assignSoilMat
            // 
            this.button_assignSoilMat.Location = new System.Drawing.Point(120, 1);
            this.button_assignSoilMat.Margin = new System.Windows.Forms.Padding(2);
            this.button_assignSoilMat.Name = "button_assignSoilMat";
            this.button_assignSoilMat.Size = new System.Drawing.Size(43, 23);
            this.button_assignSoilMat.TabIndex = 4;
            this.button_assignSoilMat.Text = "材料";
            this.toolTip1.SetToolTip(this.button_assignSoilMat, "为表格中选择的土层行指定材料");
            this.button_assignSoilMat.UseVisualStyleBackColor = true;
            this.button_assignSoilMat.Click += new System.EventHandler(this.button_assignSoilMat_Click);
            // 
            // eZDataGridViewSoilLayers
            // 
            this.eZDataGridViewSoilLayers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eZDataGridViewSoilLayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eZDataGridViewSoilLayers.KeyDelete = false;
            this.eZDataGridViewSoilLayers.Location = new System.Drawing.Point(2, 27);
            this.eZDataGridViewSoilLayers.ManipulateRows = false;
            this.eZDataGridViewSoilLayers.Margin = new System.Windows.Forms.Padding(2);
            this.eZDataGridViewSoilLayers.Name = "eZDataGridViewSoilLayers";
            this.eZDataGridViewSoilLayers.RowTemplate.Height = 23;
            this.eZDataGridViewSoilLayers.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.eZDataGridViewSoilLayers.ShowRowNumber = false;
            this.eZDataGridViewSoilLayers.Size = new System.Drawing.Size(539, 162);
            this.eZDataGridViewSoilLayers.SupportPaste = false;
            this.eZDataGridViewSoilLayers.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Cursor = System.Windows.Forms.Cursors.Default;
            this.label8.Location = new System.Drawing.Point(2, 8);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "土层参数";
            // 
            // buttonSavePic
            // 
            this.buttonSavePic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSavePic.Location = new System.Drawing.Point(9, 547);
            this.buttonSavePic.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSavePic.Name = "buttonSavePic";
            this.buttonSavePic.Size = new System.Drawing.Size(75, 24);
            this.buttonSavePic.TabIndex = 2;
            this.buttonSavePic.Text = "保存图片";
            this.buttonSavePic.UseVisualStyleBackColor = true;
            // 
            // buttonSysInfo
            // 
            this.buttonSysInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSysInfo.Location = new System.Drawing.Point(219, 547);
            this.buttonSysInfo.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSysInfo.Name = "buttonSysInfo";
            this.buttonSysInfo.Size = new System.Drawing.Size(75, 24);
            this.buttonSysInfo.TabIndex = 2;
            this.buttonSysInfo.Text = "系统信息";
            this.buttonSysInfo.UseVisualStyleBackColor = true;
            // 
            // button_GenerateFrame
            // 
            this.button_GenerateFrame.Location = new System.Drawing.Point(566, 66);
            this.button_GenerateFrame.Margin = new System.Windows.Forms.Padding(0);
            this.button_GenerateFrame.Name = "button_GenerateFrame";
            this.button_GenerateFrame.Size = new System.Drawing.Size(56, 24);
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
            this.TSM_File,
            this.项目ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(868, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // TSM_File
            // 
            this.TSM_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSM_Import,
            this.TSM_Export,
            this.toolStripSeparator1,
            this.TSM_Exit});
            this.TSM_File.Name = "TSM_File";
            this.TSM_File.Size = new System.Drawing.Size(45, 20);
            this.TSM_File.Text = "文件";
            // 
            // TSM_Import
            // 
            this.TSM_Import.Name = "TSM_Import";
            this.TSM_Import.Size = new System.Drawing.Size(152, 22);
            this.TSM_Import.Text = "打开";
            this.TSM_Import.Click += new System.EventHandler(this.TSM_Import_Click);
            // 
            // TSM_Export
            // 
            this.TSM_Export.Name = "TSM_Export";
            this.TSM_Export.Size = new System.Drawing.Size(152, 22);
            this.TSM_Export.Text = "导出";
            this.TSM_Export.Click += new System.EventHandler(this.TSM_Export_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // TSM_Exit
            // 
            this.TSM_Exit.Name = "TSM_Exit";
            this.TSM_Exit.Size = new System.Drawing.Size(152, 22);
            this.TSM_Exit.Text = "退出";
            this.TSM_Exit.Click += new System.EventHandler(this.TSM_Exit_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文档ToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // 文档ToolStripMenuItem
            // 
            this.文档ToolStripMenuItem.Name = "文档ToolStripMenuItem";
            this.文档ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.文档ToolStripMenuItem.Text = "文档";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // _backgroundWorker
            // 
            this._backgroundWorker.WorkerSupportsCancellation = true;
            this._backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this._backgroundWorker_DoWork);
            this._backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this._backgroundWorker_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 582);
            this.progressBar1.MarqueeAnimationSpeed = 10;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(868, 5);
            this.progressBar1.TabIndex = 9;
            this.progressBar1.Visible = false;
            // 
            // modelDrawer1
            // 
            this.modelDrawer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.modelDrawer1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.modelDrawer1.Location = new System.Drawing.Point(10, 31);
            this.modelDrawer1.Name = "modelDrawer1";
            this.modelDrawer1.Size = new System.Drawing.Size(284, 510);
            this.modelDrawer1.TabIndex = 0;
            this.modelDrawer1.TabStop = false;
            this.modelDrawer1.Paint += new System.Windows.Forms.PaintEventHandler(this.modelDrawer1_Paint);
            this.modelDrawer1.Resize += new System.EventHandler(this.modelDrawer1_Resize);
            // 
            // 项目ToolStripMenuItem
            // 
            this.项目ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.工作文件夹ToolStripMenuItem});
            this.项目ToolStripMenuItem.Name = "项目ToolStripMenuItem";
            this.项目ToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.项目ToolStripMenuItem.Text = "项目";
            // 
            // 工作文件夹ToolStripMenuItem
            // 
            this.工作文件夹ToolStripMenuItem.Name = "工作文件夹ToolStripMenuItem";
            this.工作文件夹ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.工作文件夹ToolStripMenuItem.Text = "工作文件夹";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 587);
            this.Controls.Add(this.progressBar1);
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
            this.MinimumSize = new System.Drawing.Size(679, 528);
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
        private System.Windows.Forms.Button buttonSavePic;
        private System.Windows.Forms.Button buttonSysInfo;
        private System.Windows.Forms.Button button_assignCompMat;
        private System.Windows.Forms.Button button_assignProfile;
        private System.Windows.Forms.ComboBox comboBoxProfiles;
        private System.Windows.Forms.ComboBox comboBoxCompMaterials;
        private System.Windows.Forms.Button button_GenerateFrame;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TSM_File;
        private System.Windows.Forms.ToolStripMenuItem TSM_Import;
        private System.Windows.Forms.ToolStripMenuItem TSM_Export;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem TSM_Exit;
        private System.Windows.Forms.ComboBox comboBoxSoilMaterials;
        private System.Windows.Forms.Button button_assignSoilMat;
        private System.Windows.Forms.Label label8;
        private System.ComponentModel.BackgroundWorker _backgroundWorker;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 文档ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 项目ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 工作文件夹ToolStripMenuItem;
    }
}

