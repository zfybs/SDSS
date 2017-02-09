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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonMaterials = new System.Windows.Forms.Button();
            this.button_Profile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNum_layers = new eZstd.UserControls.TextBoxNum();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxNum_spans = new eZstd.UserControls.TextBoxNum();
            this.button_Boundary = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.eZDataGridViewFrame = new eZstd.UserControls.eZDataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.eZDataGridViewSoilLayers = new eZstd.UserControls.eZDataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonSavePic = new System.Windows.Forms.Button();
            this.buttonSysInfo = new System.Windows.Forms.Button();
            this.modelDrawer1 = new SDSS.UIControls.ModelDrawer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eZDataGridViewFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eZDataGridViewSoilLayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.modelDrawer1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(713, 484);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(4);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(100, 30);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "确定";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonMaterials
            // 
            this.buttonMaterials.Location = new System.Drawing.Point(538, 19);
            this.buttonMaterials.Name = "buttonMaterials";
            this.buttonMaterials.Size = new System.Drawing.Size(75, 30);
            this.buttonMaterials.TabIndex = 2;
            this.buttonMaterials.Text = "材料";
            this.buttonMaterials.UseVisualStyleBackColor = true;
            // 
            // button_Profile
            // 
            this.button_Profile.Location = new System.Drawing.Point(619, 19);
            this.button_Profile.Name = "button_Profile";
            this.button_Profile.Size = new System.Drawing.Size(75, 30);
            this.button_Profile.TabIndex = 2;
            this.button_Profile.Text = "截面";
            this.button_Profile.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(420, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "基本定义管理：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(420, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "框架构造：";
            // 
            // textBoxNum_layers
            // 
            this.textBoxNum_layers.IntegerOnly = true;
            this.textBoxNum_layers.Location = new System.Drawing.Point(538, 64);
            this.textBoxNum_layers.Name = "textBoxNum_layers";
            this.textBoxNum_layers.PositiveOnly = true;
            this.textBoxNum_layers.Size = new System.Drawing.Size(75, 25);
            this.textBoxNum_layers.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(619, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "层";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(728, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "跨";
            // 
            // textBoxNum_spans
            // 
            this.textBoxNum_spans.IntegerOnly = true;
            this.textBoxNum_spans.Location = new System.Drawing.Point(647, 64);
            this.textBoxNum_spans.Name = "textBoxNum_spans";
            this.textBoxNum_spans.PositiveOnly = true;
            this.textBoxNum_spans.Size = new System.Drawing.Size(75, 25);
            this.textBoxNum_spans.TabIndex = 4;
            // 
            // button_Boundary
            // 
            this.button_Boundary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Boundary.Location = new System.Drawing.Point(419, 484);
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
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.splitContainer1.Location = new System.Drawing.Point(419, 95);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.eZDataGridViewFrame);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.button1);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.eZDataGridViewSoilLayers);
            this.splitContainer1.Panel2.Controls.Add(this.label8);
            this.splitContainer1.Size = new System.Drawing.Size(395, 383);
            this.splitContainer1.SplitterDistance = 220;
            this.splitContainer1.TabIndex = 7;
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
            this.eZDataGridViewFrame.Size = new System.Drawing.Size(389, 183);
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(76, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 2;
            this.button1.Text = "材料";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(157, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 2;
            this.button2.Text = "截面";
            this.button2.UseVisualStyleBackColor = true;
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
            this.eZDataGridViewSoilLayers.Size = new System.Drawing.Size(389, 130);
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
            this.buttonSavePic.Location = new System.Drawing.Point(12, 484);
            this.buttonSavePic.Name = "buttonSavePic";
            this.buttonSavePic.Size = new System.Drawing.Size(100, 30);
            this.buttonSavePic.TabIndex = 2;
            this.buttonSavePic.Text = "保存图片";
            this.buttonSavePic.UseVisualStyleBackColor = true;
            // 
            // buttonSysInfo
            // 
            this.buttonSysInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSysInfo.Location = new System.Drawing.Point(292, 484);
            this.buttonSysInfo.Name = "buttonSysInfo";
            this.buttonSysInfo.Size = new System.Drawing.Size(100, 30);
            this.buttonSysInfo.TabIndex = 2;
            this.buttonSysInfo.Text = "系统信息";
            this.buttonSysInfo.UseVisualStyleBackColor = true;
            // 
            // modelDrawer1
            // 
            this.modelDrawer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.modelDrawer1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.modelDrawer1.Location = new System.Drawing.Point(13, 27);
            this.modelDrawer1.Margin = new System.Windows.Forms.Padding(4);
            this.modelDrawer1.Name = "modelDrawer1";
            this.modelDrawer1.Size = new System.Drawing.Size(379, 450);
            this.modelDrawer1.TabIndex = 0;
            this.modelDrawer1.TabStop = false;
            this.modelDrawer1.Paint += new System.Windows.Forms.PaintEventHandler(this.modelDrawer1_Paint);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 526);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.textBoxNum_spans);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxNum_layers);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_Profile);
            this.Controls.Add(this.buttonSysInfo);
            this.Controls.Add(this.buttonSavePic);
            this.Controls.Add(this.button_Boundary);
            this.Controls.Add(this.buttonMaterials);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.modelDrawer1);
            this.Margin = new System.Windows.Forms.Padding(4);
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
            ((System.ComponentModel.ISupportInitialize)(this.modelDrawer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ModelDrawer modelDrawer1;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonMaterials;
        private System.Windows.Forms.Button button_Profile;
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

