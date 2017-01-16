using eZstd.UserControls;

namespace SocketedShafts.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonExportToXML = new System.Windows.Forms.Button();
            this._pictureBoxSystem = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewShaft = new eZstd.UserControls.eZDataGridViewUIAdd();
            this.ColumnSegTop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSegBottom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSegment = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.buttonShaft = new System.Windows.Forms.Button();
            this.buttonSectionManager = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSoilManager = new System.Windows.Forms.Button();
            this.dataGridViewSoilLayers = new eZstd.UserControls.eZDataGridViewUIAdd();
            this.ColumnSoilTop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSoilBottom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSoil = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.buttonSystemProperty = new System.Windows.Forms.Button();
            this.buttonImportFromXML = new System.Windows.Forms.Button();
            this.buttonSavePicture = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._pictureBoxSystem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShaft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSoilLayers)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonExportToXML
            // 
            this.buttonExportToXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExportToXML.Location = new System.Drawing.Point(578, 442);
            this.buttonExportToXML.Name = "buttonExportToXML";
            this.buttonExportToXML.Size = new System.Drawing.Size(75, 23);
            this.buttonExportToXML.TabIndex = 1;
            this.buttonExportToXML.Text = "导出";
            this.buttonExportToXML.UseVisualStyleBackColor = true;
            this.buttonExportToXML.Click += new System.EventHandler(this.buttonExportToXML_Click);
            // 
            // _pictureBoxSystem
            // 
            this._pictureBoxSystem.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this._pictureBoxSystem.Location = new System.Drawing.Point(12, 12);
            this._pictureBoxSystem.Name = "_pictureBoxSystem";
            this._pictureBoxSystem.Size = new System.Drawing.Size(266, 416);
            this._pictureBoxSystem.TabIndex = 2;
            this._pictureBoxSystem.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(284, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridViewShaft);
            this.splitContainer1.Panel1.Controls.Add(this.buttonShaft);
            this.splitContainer1.Panel1.Controls.Add(this.buttonSectionManager);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.buttonSoilManager);
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewSoilLayers);
            this.splitContainer1.Size = new System.Drawing.Size(369, 416);
            this.splitContainer1.SplitterDistance = 208;
            this.splitContainer1.TabIndex = 4;
            // 
            // dataGridViewShaft
            // 
            this.dataGridViewShaft.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewShaft.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSegTop,
            this.ColumnSegBottom,
            this.ColumnSegment});
            this.dataGridViewShaft.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewShaft.KeyDelete = false;
            this.dataGridViewShaft.Location = new System.Drawing.Point(0, 42);
            this.dataGridViewShaft.Name = "dataGridViewShaft";
            this.dataGridViewShaft.RowTemplate.Height = 23;
            this.dataGridViewShaft.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewShaft.ShowRowNumber = false;
            this.dataGridViewShaft.Size = new System.Drawing.Size(369, 166);
            this.dataGridViewShaft.SupportPaste = false;
            this.dataGridViewShaft.TabIndex = 3;
            // 
            // ColumnSegTop
            // 
            this.ColumnSegTop.HeaderText = "顶部";
            this.ColumnSegTop.Name = "ColumnSegTop";
            // 
            // ColumnSegBottom
            // 
            this.ColumnSegBottom.HeaderText = "底部";
            this.ColumnSegBottom.Name = "ColumnSegBottom";
            // 
            // ColumnSegment
            // 
            this.ColumnSegment.HeaderText = "截面";
            this.ColumnSegment.Name = "ColumnSegment";
            // 
            // buttonShaft
            // 
            this.buttonShaft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonShaft.Location = new System.Drawing.Point(236, 10);
            this.buttonShaft.Name = "buttonShaft";
            this.buttonShaft.Size = new System.Drawing.Size(52, 23);
            this.buttonShaft.TabIndex = 5;
            this.buttonShaft.Text = "桩";
            this.buttonShaft.UseVisualStyleBackColor = true;
            this.buttonShaft.Click += new System.EventHandler(this.buttonShaft_Click);
            // 
            // buttonSectionManager
            // 
            this.buttonSectionManager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSectionManager.Location = new System.Drawing.Point(297, 10);
            this.buttonSectionManager.Name = "buttonSectionManager";
            this.buttonSectionManager.Size = new System.Drawing.Size(52, 23);
            this.buttonSectionManager.TabIndex = 1;
            this.buttonSectionManager.Text = "管理";
            this.buttonSectionManager.UseVisualStyleBackColor = true;
            this.buttonSectionManager.Click += new System.EventHandler(this.buttonSectionManager_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(137, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "桩截面信息 （单位：m）";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "土层信息 （单位：m）";
            // 
            // buttonSoilManager
            // 
            this.buttonSoilManager.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSoilManager.Location = new System.Drawing.Point(297, 9);
            this.buttonSoilManager.Name = "buttonSoilManager";
            this.buttonSoilManager.Size = new System.Drawing.Size(52, 23);
            this.buttonSoilManager.TabIndex = 1;
            this.buttonSoilManager.Text = "管理";
            this.buttonSoilManager.UseVisualStyleBackColor = true;
            this.buttonSoilManager.Click += new System.EventHandler(this.buttonSoilManager_Click);
            // 
            // dataGridViewSoilLayers
            // 
            this.dataGridViewSoilLayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSoilLayers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSoilTop,
            this.ColumnSoilBottom,
            this.ColumnSoil});
            this.dataGridViewSoilLayers.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridViewSoilLayers.KeyDelete = false;
            this.dataGridViewSoilLayers.Location = new System.Drawing.Point(0, 38);
            this.dataGridViewSoilLayers.Name = "dataGridViewSoilLayers";
            this.dataGridViewSoilLayers.RowTemplate.Height = 23;
            this.dataGridViewSoilLayers.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewSoilLayers.ShowRowNumber = false;
            this.dataGridViewSoilLayers.Size = new System.Drawing.Size(369, 166);
            this.dataGridViewSoilLayers.SupportPaste = false;
            this.dataGridViewSoilLayers.TabIndex = 3;
            // 
            // ColumnSoilTop
            // 
            this.ColumnSoilTop.HeaderText = "顶部";
            this.ColumnSoilTop.Name = "ColumnSoilTop";
            // 
            // ColumnSoilBottom
            // 
            this.ColumnSoilBottom.HeaderText = "底部";
            this.ColumnSoilBottom.Name = "ColumnSoilBottom";
            // 
            // ColumnSoil
            // 
            this.ColumnSoil.HeaderText = "土层";
            this.ColumnSoil.Name = "ColumnSoil";
            // 
            // buttonSystemProperty
            // 
            this.buttonSystemProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSystemProperty.Location = new System.Drawing.Point(12, 442);
            this.buttonSystemProperty.Name = "buttonSystemProperty";
            this.buttonSystemProperty.Size = new System.Drawing.Size(75, 23);
            this.buttonSystemProperty.TabIndex = 1;
            this.buttonSystemProperty.Text = "系统信息";
            this.buttonSystemProperty.UseVisualStyleBackColor = true;
            this.buttonSystemProperty.Click += new System.EventHandler(this.buttonSystemProperty_Click);
            // 
            // buttonImportFromXML
            // 
            this.buttonImportFromXML.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonImportFromXML.Location = new System.Drawing.Point(497, 442);
            this.buttonImportFromXML.Name = "buttonImportFromXML";
            this.buttonImportFromXML.Size = new System.Drawing.Size(75, 23);
            this.buttonImportFromXML.TabIndex = 6;
            this.buttonImportFromXML.Text = "导入";
            this.buttonImportFromXML.UseVisualStyleBackColor = true;
            this.buttonImportFromXML.Click += new System.EventHandler(this.buttonImportFromXML_Click);
            // 
            // buttonSavePicture
            // 
            this.buttonSavePicture.Location = new System.Drawing.Point(203, 442);
            this.buttonSavePicture.Name = "buttonSavePicture";
            this.buttonSavePicture.Size = new System.Drawing.Size(75, 23);
            this.buttonSavePicture.TabIndex = 7;
            this.buttonSavePicture.Text = "保存图片";
            this.buttonSavePicture.UseVisualStyleBackColor = true;
            this.buttonSavePicture.Click += new System.EventHandler(this.buttonSavePicture_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 477);
            this.Controls.Add(this.buttonSavePicture);
            this.Controls.Add(this.buttonImportFromXML);
            this.Controls.Add(this._pictureBoxSystem);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.buttonSystemProperty);
            this.Controls.Add(this.buttonExportToXML);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "水平受荷嵌岩桩";
            ((System.ComponentModel.ISupportInitialize)(this._pictureBoxSystem)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewShaft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSoilLayers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonExportToXML;
        private System.Windows.Forms.PictureBox _pictureBoxSystem;
        private eZDataGridViewUIAdd dataGridViewSoilLayers;
        private eZDataGridViewUIAdd dataGridViewShaft;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSoilManager;
        private System.Windows.Forms.Button buttonSectionManager;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSoilTop;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSoilBottom;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnSoil;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSegTop;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSegBottom;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnSegment;
        private System.Windows.Forms.Button buttonSystemProperty;
        private System.Windows.Forms.Button buttonImportFromXML;
        private System.Windows.Forms.Button buttonSavePicture;
        private System.Windows.Forms.Button buttonShaft;
    }
}

