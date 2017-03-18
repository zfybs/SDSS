using SDSS.UIControls;

namespace SDSS.ModelForms
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
            this.buttonSavePic = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this._bgw_Solver = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button_Terminate = new System.Windows.Forms.Button();
            this.label_elapsedTime = new System.Windows.Forms.Label();
            this.panel_SubmodelContainer = new System.Windows.Forms.Panel();
            this.modelDrawer1 = new SDSS.UIControls.ModelDrawer();
            ((System.ComponentModel.ISupportInitialize)(this.modelDrawer1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSolve
            // 
            this.buttonSolve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSolve.Location = new System.Drawing.Point(814, 589);
            this.buttonSolve.Name = "buttonSolve";
            this.buttonSolve.Size = new System.Drawing.Size(75, 24);
            this.buttonSolve.TabIndex = 1;
            this.buttonSolve.Text = "求解";
            this.buttonSolve.UseVisualStyleBackColor = true;
            this.buttonSolve.Click += new System.EventHandler(this.buttonSolve_Click);
            // 
            // button_Materials
            // 
            this.button_Materials.Location = new System.Drawing.Point(403, 12);
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
            this.button_Profiles.Location = new System.Drawing.Point(463, 12);
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
            this.label1.Location = new System.Drawing.Point(314, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "基本定义管理：";
            // 
            // buttonSavePic
            // 
            this.buttonSavePic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSavePic.Location = new System.Drawing.Point(10, 590);
            this.buttonSavePic.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSavePic.Name = "buttonSavePic";
            this.buttonSavePic.Size = new System.Drawing.Size(75, 24);
            this.buttonSavePic.TabIndex = 2;
            this.buttonSavePic.Text = "保存图片";
            this.buttonSavePic.UseVisualStyleBackColor = true;
            // 
            // _bgw_Solver
            // 
            this._bgw_Solver.WorkerReportsProgress = true;
            this._bgw_Solver.WorkerSupportsCancellation = true;
            this._bgw_Solver.DoWork += new System.ComponentModel.DoWorkEventHandler(this._bgw_Solver_DoWork);
            this._bgw_Solver.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this._bgw_Solve_ProgressChanged);
            this._bgw_Solver.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this._backgroundWorker_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 619);
            this.progressBar1.MarqueeAnimationSpeed = 10;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(900, 5);
            this.progressBar1.TabIndex = 9;
            this.progressBar1.Visible = false;
            // 
            // button_Terminate
            // 
            this.button_Terminate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Terminate.Location = new System.Drawing.Point(733, 590);
            this.button_Terminate.Name = "button_Terminate";
            this.button_Terminate.Size = new System.Drawing.Size(75, 23);
            this.button_Terminate.TabIndex = 10;
            this.button_Terminate.Text = "终止计算";
            this.button_Terminate.UseVisualStyleBackColor = true;
            this.button_Terminate.Visible = false;
            this.button_Terminate.Click += new System.EventHandler(this.ForceSolverShutDown);
            // 
            // label_elapsedTime
            // 
            this.label_elapsedTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_elapsedTime.AutoSize = true;
            this.label_elapsedTime.BackColor = System.Drawing.Color.Transparent;
            this.label_elapsedTime.Location = new System.Drawing.Point(170, 606);
            this.label_elapsedTime.Name = "label_elapsedTime";
            this.label_elapsedTime.Size = new System.Drawing.Size(95, 12);
            this.label_elapsedTime.TabIndex = 11;
            this.label_elapsedTime.Text = "Elapsed time : ";
            // 
            // panel_SubmodelContainer
            // 
            this.panel_SubmodelContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_SubmodelContainer.BackColor = System.Drawing.SystemColors.Control;
            this.panel_SubmodelContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_SubmodelContainer.Location = new System.Drawing.Point(310, 41);
            this.panel_SubmodelContainer.Name = "panel_SubmodelContainer";
            this.panel_SubmodelContainer.Size = new System.Drawing.Size(579, 542);
            this.panel_SubmodelContainer.TabIndex = 12;
            // 
            // modelDrawer1
            // 
            this.modelDrawer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.modelDrawer1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.modelDrawer1.Location = new System.Drawing.Point(10, 12);
            this.modelDrawer1.Name = "modelDrawer1";
            this.modelDrawer1.Size = new System.Drawing.Size(284, 573);
            this.modelDrawer1.TabIndex = 0;
            this.modelDrawer1.TabStop = false;
            this.modelDrawer1.Paint += new System.Windows.Forms.PaintEventHandler(this.modelDrawer1_Paint);
            this.modelDrawer1.Resize += new System.EventHandler(this.modelDrawer1_Resize);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 624);
            this.Controls.Add(this.panel_SubmodelContainer);
            this.Controls.Add(this.label_elapsedTime);
            this.Controls.Add(this.modelDrawer1);
            this.Controls.Add(this.button_Terminate);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_Profiles);
            this.Controls.Add(this.buttonSavePic);
            this.Controls.Add(this.button_Materials);
            this.Controls.Add(this.buttonSolve);
            this.MinimumSize = new System.Drawing.Size(679, 528);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "地铁车站抗震设计";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.modelDrawer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonSolve;
        private System.Windows.Forms.Button button_Materials;
        private System.Windows.Forms.Button button_Profiles;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSavePic;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button_Terminate;
        private System.Windows.Forms.Label label_elapsedTime;
        protected System.ComponentModel.BackgroundWorker _bgw_Solver;
        private System.Windows.Forms.ToolTip toolTip1;
        protected System.Windows.Forms.Panel panel_SubmodelContainer;
        private ModelDrawer modelDrawer1;
    }
}

