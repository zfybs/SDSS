namespace SDSS.ModelForms
{
    partial class Model1Form
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsm_Project = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_ReportTest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.frameConstructor1 = new SDSS.Structures.FrameConstructor();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxNum_OverLaying = new eZstd.UserControls.TextBoxNum();
            this.textBoxNum_topEle = new eZstd.UserControls.TextBoxNum();
            this.eZDataGridViewSoilLayers = new eZstd.UserControls.eZDataGridView();
            this.button_Boundary = new System.Windows.Forms.Button();
            this.panel_SubmodelContainer.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eZDataGridViewSoilLayers)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_SubmodelContainer
            // 
            this.panel_SubmodelContainer.Controls.Add(this.splitContainer1);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightCoral;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_Project});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(53, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsm_Project
            // 
            this.tsm_Project.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_ReportTest});
            this.tsm_Project.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.tsm_Project.MergeIndex = 3;
            this.tsm_Project.Name = "tsm_Project";
            this.tsm_Project.Size = new System.Drawing.Size(45, 20);
            this.tsm_Project.Text = "项目";
            // 
            // tsm_ReportTest
            // 
            this.tsm_ReportTest.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.tsm_ReportTest.MergeIndex = 3;
            this.tsm_ReportTest.Name = "tsm_ReportTest";
            this.tsm_ReportTest.Size = new System.Drawing.Size(152, 22);
            this.tsm_ReportTest.Text = "测试生成报告";
            this.tsm_ReportTest.Click += new System.EventHandler(this.tsm_TestShowResult_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Location = new System.Drawing.Point(2, 2);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.frameConstructor1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Panel2.Controls.Add(this.label6);
            this.splitContainer1.Panel2.Controls.Add(this.label5);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxNum_OverLaying);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxNum_topEle);
            this.splitContainer1.Panel2.Controls.Add(this.eZDataGridViewSoilLayers);
            this.splitContainer1.Size = new System.Drawing.Size(573, 536);
            this.splitContainer1.SplitterDistance = 343;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 13;
            // 
            // frameConstructor1
            // 
            this.frameConstructor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frameConstructor1.Location = new System.Drawing.Point(0, 0);
            this.frameConstructor1.Name = "frameConstructor1";
            this.frameConstructor1.Size = new System.Drawing.Size(573, 343);
            this.frameConstructor1.TabIndex = 0;
            this.frameConstructor1.FramePointorChanged += new System.Action<SDSS.Structures.Frame>(this.frameConstructor1_FramePointorChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(133, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "(m)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(291, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "(m)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "上覆土厚度";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(175, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "地表标高";
            // 
            // textBoxNum_OverLaying
            // 
            this.textBoxNum_OverLaying.Location = new System.Drawing.Point(77, 4);
            this.textBoxNum_OverLaying.Name = "textBoxNum_OverLaying";
            this.textBoxNum_OverLaying.PositiveOnly = true;
            this.textBoxNum_OverLaying.Size = new System.Drawing.Size(50, 21);
            this.textBoxNum_OverLaying.TabIndex = 6;
            this.textBoxNum_OverLaying.Text = "2";
            this.textBoxNum_OverLaying.ValueNumberChanged += new System.Action<object, double>(this.textBoxNum_OverLaying_ValueNumberChanged);
            // 
            // textBoxNum_topEle
            // 
            this.textBoxNum_topEle.Location = new System.Drawing.Point(235, 4);
            this.textBoxNum_topEle.Name = "textBoxNum_topEle";
            this.textBoxNum_topEle.PositiveOnly = true;
            this.textBoxNum_topEle.Size = new System.Drawing.Size(50, 21);
            this.textBoxNum_topEle.TabIndex = 6;
            this.textBoxNum_topEle.Text = "2.00";
            this.textBoxNum_topEle.ValueNumberChanged += new System.Action<object, double>(this.textBoxNum_topEle_ValueNumberChanged);
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
            this.eZDataGridViewSoilLayers.Size = new System.Drawing.Size(571, 162);
            this.eZDataGridViewSoilLayers.SupportPaste = false;
            this.eZDataGridViewSoilLayers.TabIndex = 1;
            // 
            // button_Boundary
            // 
            this.button_Boundary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Boundary.Location = new System.Drawing.Point(653, 589);
            this.button_Boundary.Margin = new System.Windows.Forms.Padding(2);
            this.button_Boundary.Name = "button_Boundary";
            this.button_Boundary.Size = new System.Drawing.Size(75, 24);
            this.button_Boundary.TabIndex = 20;
            this.button_Boundary.Text = "边界参数";
            this.button_Boundary.UseVisualStyleBackColor = true;
            this.button_Boundary.Click += new System.EventHandler(this.button_Boundary_Click);
            // 
            // Model1Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 624);
            this.Controls.Add(this.button_Boundary);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Model1Form";
            this.Text = "矩形车站惯性力法";
            this.Controls.SetChildIndex(this.menuStrip1, 0);
            this.Controls.SetChildIndex(this.panel_SubmodelContainer, 0);
            this.Controls.SetChildIndex(this.button_Boundary, 0);
            this.panel_SubmodelContainer.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eZDataGridViewSoilLayers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsm_Project;
        private System.Windows.Forms.ToolStripMenuItem tsm_ReportTest;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private eZstd.UserControls.TextBoxNum textBoxNum_OverLaying;
        private eZstd.UserControls.TextBoxNum textBoxNum_topEle;
        private eZstd.UserControls.eZDataGridView eZDataGridViewSoilLayers;
        private System.Windows.Forms.Button button_Boundary;
        private Structures.FrameConstructor frameConstructor1;
    }
}