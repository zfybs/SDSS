namespace SDSS
{
    partial class Form1
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
            this.modelDrawer1 = new SDSS.UIControls.ModelDrawer();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.modelDrawer1)).BeginInit();
            this.SuspendLayout();
            // 
            // modelDrawer1
            // 
            this.modelDrawer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.modelDrawer1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.modelDrawer1.Location = new System.Drawing.Point(12, 12);
            this.modelDrawer1.Name = "modelDrawer1";
            this.modelDrawer1.Size = new System.Drawing.Size(307, 514);
            this.modelDrawer1.TabIndex = 0;
            this.modelDrawer1.TabStop = false;
            this.modelDrawer1.Paint += new System.Windows.Forms.PaintEventHandler(this.modelDrawer1_Paint);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(344, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 538);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.modelDrawer1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.modelDrawer1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UIControls.ModelDrawer modelDrawer1;
        private System.Windows.Forms.Button button1;
    }
}