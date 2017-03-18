namespace SDSS.ModelForms
{
    partial class MainFormMdi
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsm_Files = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TSM_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Option = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Definitions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Materials = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Profiles = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Project = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_ModelInfos = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_ShowResult = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Windows = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_Documentation = new System.Windows.Forms.ToolStripMenuItem();
            this.tsm_About = new System.Windows.Forms.ToolStripMenuItem();
            this.tst_abqWorkingDir = new System.Windows.Forms.ToolStripTextBox();
            this.button_CloseChildForm = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_Files,
            this.tsm_Definitions,
            this.tsm_Project,
            this.tsm_Windows,
            this.tsm_Help,
            this.tst_abqWorkingDir});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.tsm_Windows;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.ShowItemToolTips = true;
            this.menuStrip1.Size = new System.Drawing.Size(735, 27);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsm_Files
            // 
            this.tsm_Files.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_Open,
            this.tsm_Save,
            this.tsm_SaveAs,
            this.toolStripSeparator1,
            this.TSM_Exit,
            this.tsm_Option});
            this.tsm_Files.MergeIndex = 0;
            this.tsm_Files.Name = "tsm_Files";
            this.tsm_Files.Size = new System.Drawing.Size(59, 23);
            this.tsm_Files.Text = "文件(&F)";
            this.tsm_Files.DropDownOpening += new System.EventHandler(this.tsm_Files_DropDownOpening);
            // 
            // tsm_Open
            // 
            this.tsm_Open.Name = "tsm_Open";
            this.tsm_Open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsm_Open.Size = new System.Drawing.Size(185, 22);
            this.tsm_Open.Text = "打开(&O)";
            this.tsm_Open.Click += new System.EventHandler(this.tsm_Open_Click);
            // 
            // tsm_Save
            // 
            this.tsm_Save.Name = "tsm_Save";
            this.tsm_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tsm_Save.Size = new System.Drawing.Size(185, 22);
            this.tsm_Save.Text = "保存(&S)";
            this.tsm_Save.Click += new System.EventHandler(this.tsm_Save_Click);
            // 
            // tsm_SaveAs
            // 
            this.tsm_SaveAs.Name = "tsm_SaveAs";
            this.tsm_SaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.tsm_SaveAs.Size = new System.Drawing.Size(185, 22);
            this.tsm_SaveAs.Text = "另存为";
            this.tsm_SaveAs.Click += new System.EventHandler(this.tsm_SaveAs_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(182, 6);
            // 
            // TSM_Exit
            // 
            this.TSM_Exit.Name = "TSM_Exit";
            this.TSM_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.TSM_Exit.Size = new System.Drawing.Size(185, 22);
            this.TSM_Exit.Text = "退出";
            this.TSM_Exit.Click += new System.EventHandler(this.TSM_Exit_Click);
            // 
            // tsm_Option
            // 
            this.tsm_Option.Name = "tsm_Option";
            this.tsm_Option.Size = new System.Drawing.Size(185, 22);
            this.tsm_Option.Text = "选项(&O)";
            this.tsm_Option.Click += new System.EventHandler(this.TSM_Option_Click);
            // 
            // tsm_Definitions
            // 
            this.tsm_Definitions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_Materials,
            this.tsm_Profiles});
            this.tsm_Definitions.Name = "tsm_Definitions";
            this.tsm_Definitions.Size = new System.Drawing.Size(61, 23);
            this.tsm_Definitions.Text = "定义(&D)";
            // 
            // tsm_Materials
            // 
            this.tsm_Materials.Name = "tsm_Materials";
            this.tsm_Materials.Size = new System.Drawing.Size(119, 22);
            this.tsm_Materials.Text = "材料(&M)";
            this.tsm_Materials.Click += new System.EventHandler(this.tsm_Materials_Click);
            // 
            // tsm_Profiles
            // 
            this.tsm_Profiles.Name = "tsm_Profiles";
            this.tsm_Profiles.Size = new System.Drawing.Size(119, 22);
            this.tsm_Profiles.Text = "截面(&P)";
            this.tsm_Profiles.Click += new System.EventHandler(this.tsm_Profiles_Click);
            // 
            // tsm_Project
            // 
            this.tsm_Project.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_ModelInfos,
            this.tsm_ShowResult});
            this.tsm_Project.MergeIndex = 2;
            this.tsm_Project.Name = "tsm_Project";
            this.tsm_Project.Size = new System.Drawing.Size(45, 23);
            this.tsm_Project.Text = "项目";
            // 
            // tsm_ModelInfos
            // 
            this.tsm_ModelInfos.Name = "tsm_ModelInfos";
            this.tsm_ModelInfos.Size = new System.Drawing.Size(126, 22);
            this.tsm_ModelInfos.Text = "模型信息";
            this.tsm_ModelInfos.Click += new System.EventHandler(this.tsm_ModelInfos_Click);
            // 
            // tsm_ShowResult
            // 
            this.tsm_ShowResult.Name = "tsm_ShowResult";
            this.tsm_ShowResult.Size = new System.Drawing.Size(126, 22);
            this.tsm_ShowResult.Text = "计算结果";
            this.tsm_ShowResult.Click += new System.EventHandler(this.tsm_ShowResult_Click);
            // 
            // tsm_Windows
            // 
            this.tsm_Windows.Name = "tsm_Windows";
            this.tsm_Windows.Size = new System.Drawing.Size(45, 23);
            this.tsm_Windows.Text = "窗口";
            // 
            // tsm_Help
            // 
            this.tsm_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsm_Documentation,
            this.tsm_About});
            this.tsm_Help.MergeIndex = 12;
            this.tsm_Help.Name = "tsm_Help";
            this.tsm_Help.Size = new System.Drawing.Size(45, 23);
            this.tsm_Help.Text = "帮助";
            // 
            // tsm_Documentation
            // 
            this.tsm_Documentation.Name = "tsm_Documentation";
            this.tsm_Documentation.Size = new System.Drawing.Size(107, 22);
            this.tsm_Documentation.Text = "文档";
            // 
            // tsm_About
            // 
            this.tsm_About.Name = "tsm_About";
            this.tsm_About.Size = new System.Drawing.Size(107, 22);
            this.tsm_About.Text = "About";
            // 
            // tst_abqWorkingDir
            // 
            this.tst_abqWorkingDir.Enabled = false;
            this.tst_abqWorkingDir.Name = "tst_abqWorkingDir";
            this.tst_abqWorkingDir.Size = new System.Drawing.Size(150, 23);
            // 
            // button_CloseChildForm
            // 
            this.button_CloseChildForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_CloseChildForm.BackColor = System.Drawing.Color.SandyBrown;
            this.button_CloseChildForm.FlatAppearance.BorderSize = 0;
            this.button_CloseChildForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_CloseChildForm.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_CloseChildForm.Location = new System.Drawing.Point(710, 4);
            this.button_CloseChildForm.Name = "button_CloseChildForm";
            this.button_CloseChildForm.Size = new System.Drawing.Size(18, 18);
            this.button_CloseChildForm.TabIndex = 13;
            this.button_CloseChildForm.Text = "X";
            this.button_CloseChildForm.UseVisualStyleBackColor = false;
            this.button_CloseChildForm.Click += new System.EventHandler(this.button_CloseChildForm_Click);
            // 
            // MainFormMdi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 525);
            this.Controls.Add(this.button_CloseChildForm);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainFormMdi";
            this.Text = "地下车站抗震设计";
            this.Load += new System.EventHandler(this.MainFormMdi_Load);
            this.MdiChildActivate += new System.EventHandler(this.MainFormMdi_MdiChildActivate);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsm_Files;
        private System.Windows.Forms.ToolStripMenuItem tsm_Open;
        private System.Windows.Forms.ToolStripMenuItem tsm_SaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem TSM_Exit;
        private System.Windows.Forms.ToolStripMenuItem tsm_Option;
        private System.Windows.Forms.ToolStripMenuItem tsm_Help;
        private System.Windows.Forms.ToolStripMenuItem tsm_Documentation;
        private System.Windows.Forms.ToolStripMenuItem tsm_About;
        private System.Windows.Forms.ToolStripTextBox tst_abqWorkingDir;
        private System.Windows.Forms.ToolStripMenuItem tsm_Project;
        private System.Windows.Forms.ToolStripMenuItem tsm_ModelInfos;
        private System.Windows.Forms.ToolStripMenuItem tsm_ShowResult;
        private System.Windows.Forms.ToolStripMenuItem tsm_Definitions;
        private System.Windows.Forms.ToolStripMenuItem tsm_Materials;
        private System.Windows.Forms.ToolStripMenuItem tsm_Profiles;
        private System.Windows.Forms.ToolStripMenuItem tsm_Windows;
        private System.Windows.Forms.ToolStripMenuItem tsm_Save;
        private System.Windows.Forms.Button button_CloseChildForm;
    }
}