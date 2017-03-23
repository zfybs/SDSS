namespace SDSS.ModelForms
{
    partial class Model3Form
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
            this.tunnelConstructor1 = new SDSS.Structures.TunnelConstructor();
            this.panel_SubmodelContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_SubmodelContainer
            // 
            this.panel_SubmodelContainer.Controls.Add(this.tunnelConstructor1);
            // 
            // tunnelConstructor1
            // 
            this.tunnelConstructor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tunnelConstructor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tunnelConstructor1.Location = new System.Drawing.Point(1, -1);
            this.tunnelConstructor1.Name = "tunnelConstructor1";
            this.tunnelConstructor1.Size = new System.Drawing.Size(575, 311);
            this.tunnelConstructor1.TabIndex = 0;
            this.tunnelConstructor1.TunnelPointorChanged += new System.Action<SDSS.Structures.Tunnel>(this.TunnelConstructor1OnTunnelPointorChanged);
            // 
            // Model3Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 624);
            this.Name = "Model3Form";
            this.Text = "Model3Form";
            this.panel_SubmodelContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Structures.TunnelConstructor tunnelConstructor1;
    }
}