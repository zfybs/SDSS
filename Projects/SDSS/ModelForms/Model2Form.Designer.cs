using SDSS.Structures;

namespace SDSS.ModelForms
{
    partial class Model2Form
    {

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.frameConstructor1 = new SDSS.Structures.FrameConstructor();
            this.panel_SubmodelContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_SubmodelContainer
            // 
            this.panel_SubmodelContainer.Controls.Add(this.frameConstructor1);
            // 
            // frameConstructor1
            // 
            this.frameConstructor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.frameConstructor1.Location = new System.Drawing.Point(-1, -1);
            this.frameConstructor1.Name = "frameConstructor1";
            this.frameConstructor1.Size = new System.Drawing.Size(578, 352);
            this.frameConstructor1.TabIndex = 0;
            this.frameConstructor1.FramePointorChanged += new System.Action<SDSS.Structures.Frame>(this.FrameConstructor1OnFramePointorChanged);
            // 
            // Model2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(900, 624);
            this.Name = "Model2Form";
            this.panel_SubmodelContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private FrameConstructor frameConstructor1;
    }
}