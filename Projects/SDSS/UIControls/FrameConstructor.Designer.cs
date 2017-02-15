using eZstd.UserControls;

namespace SDSS.UIControls
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgv_Layers = new eZstd.UserControls.eZDataGridView();
            this.ColumnLayerHeight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_Spans = new eZstd.UserControls.eZDataGridView();
            this.ColumnSpanWidth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Layers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Spans)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgv_Layers);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgv_Spans);
            this.splitContainer1.Size = new System.Drawing.Size(350, 186);
            this.splitContainer1.SplitterDistance = 174;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgv_Layers
            // 
            this.dgv_Layers.AllowUserToAddRows = false;
            this.dgv_Layers.AllowUserToDeleteRows = false;
            this.dgv_Layers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Layers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnLayerHeight});
            this.dgv_Layers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Layers.Location = new System.Drawing.Point(0, 0);
            this.dgv_Layers.ManipulateRows = false;
            this.dgv_Layers.Margin = new System.Windows.Forms.Padding(2);
            this.dgv_Layers.Name = "dgv_Layers";
            this.dgv_Layers.RowTemplate.Height = 27;
            this.dgv_Layers.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Layers.Size = new System.Drawing.Size(174, 186);
            this.dgv_Layers.TabIndex = 0;
            this.dgv_Layers.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_DataError);
            // 
            // ColumnLayerHeight
            // 
            this.ColumnLayerHeight.HeaderText = "层高";
            this.ColumnLayerHeight.Name = "ColumnLayerHeight";
            this.ColumnLayerHeight.ToolTipText = "从下往上的每一层的高度，单位为m";
            // 
            // dgv_Spans
            // 
            this.dgv_Spans.AllowUserToAddRows = false;
            this.dgv_Spans.AllowUserToDeleteRows = false;
            this.dgv_Spans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Spans.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnSpanWidth});
            this.dgv_Spans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Spans.Location = new System.Drawing.Point(0, 0);
            this.dgv_Spans.ManipulateRows = false;
            this.dgv_Spans.Margin = new System.Windows.Forms.Padding(2);
            this.dgv_Spans.Name = "dgv_Spans";
            this.dgv_Spans.RowTemplate.Height = 27;
            this.dgv_Spans.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Spans.Size = new System.Drawing.Size(173, 186);
            this.dgv_Spans.TabIndex = 0;
            this.dgv_Spans.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_DataError);
            // 
            // ColumnSpanWidth
            // 
            this.ColumnSpanWidth.HeaderText = "跨度";
            this.ColumnSpanWidth.Name = "ColumnSpanWidth";
            this.ColumnSpanWidth.ToolTipText = "从左往右的每一跨的宽度，单位为m";
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(285, 190);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(2);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(56, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "确定";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(224, 190);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(56, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FrameConstructor
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 225);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(319, 264);
            this.Name = "FrameConstructor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "矩形框架构造";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrameConstructor_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrameConstructor_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Layers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Spans)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private eZDataGridView dgv_Layers;
        private eZDataGridView dgv_Spans;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLayerHeight;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSpanWidth;
    }
}