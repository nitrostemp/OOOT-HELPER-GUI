namespace OOOT_GUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oOOTFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oOOTReleaseFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.builderFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gitHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runOOOTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setThemeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.createShortcutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadHDTexturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.installToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyRomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractAssetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cloneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewCommitOnGitHubToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.button5 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 39);
            this.label1.TabIndex = 0;
            this.label1.Text = "Commit:\r\nDate:\r\nTitle:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(12, 32);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(391, 73);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Commit Info:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.settingsToolStripMenuItem,
            this.toolStripMenuItem3});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(415, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oOOTFolderToolStripMenuItem,
            this.oOOTReleaseFolderToolStripMenuItem,
            this.builderFolderToolStripMenuItem,
            this.gitHubToolStripMenuItem,
            this.runOOOTToolStripMenuItem});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.openToolStripMenuItem.Text = "File";
            // 
            // oOOTFolderToolStripMenuItem
            // 
            this.oOOTFolderToolStripMenuItem.Name = "oOOTFolderToolStripMenuItem";
            this.oOOTFolderToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.oOOTFolderToolStripMenuItem.Text = "OOOT Folder";
            this.oOOTFolderToolStripMenuItem.Click += new System.EventHandler(this.oOOTFolderToolStripMenuItem_Click);
            // 
            // oOOTReleaseFolderToolStripMenuItem
            // 
            this.oOOTReleaseFolderToolStripMenuItem.Name = "oOOTReleaseFolderToolStripMenuItem";
            this.oOOTReleaseFolderToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.oOOTReleaseFolderToolStripMenuItem.Text = "OOOT Release Folder";
            this.oOOTReleaseFolderToolStripMenuItem.Click += new System.EventHandler(this.oOOTReleaseFolderToolStripMenuItem_Click);
            // 
            // builderFolderToolStripMenuItem
            // 
            this.builderFolderToolStripMenuItem.Name = "builderFolderToolStripMenuItem";
            this.builderFolderToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.builderFolderToolStripMenuItem.Text = "Builder Folder";
            this.builderFolderToolStripMenuItem.Click += new System.EventHandler(this.builderFolderToolStripMenuItem_Click);
            // 
            // gitHubToolStripMenuItem
            // 
            this.gitHubToolStripMenuItem.Name = "gitHubToolStripMenuItem";
            this.gitHubToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.gitHubToolStripMenuItem.Text = "Open GitHub";
            this.gitHubToolStripMenuItem.Click += new System.EventHandler(this.gitHubToolStripMenuItem_Click);
            // 
            // runOOOTToolStripMenuItem
            // 
            this.runOOOTToolStripMenuItem.Name = "runOOOTToolStripMenuItem";
            this.runOOOTToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.runOOOTToolStripMenuItem.Text = "Run OOOT";
            this.runOOOTToolStripMenuItem.Click += new System.EventHandler(this.runOOOTToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pathToolStripMenuItem,
            this.setThemeToolStripMenuItem,
            this.viewLogToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(61, 20);
            this.toolStripMenuItem1.Text = "Settings";
            // 
            // pathToolStripMenuItem
            // 
            this.pathToolStripMenuItem.Name = "pathToolStripMenuItem";
            this.pathToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pathToolStripMenuItem.Text = "Paths";
            this.pathToolStripMenuItem.Click += new System.EventHandler(this.pathToolStripMenuItem_Click);
            // 
            // setThemeToolStripMenuItem
            // 
            this.setThemeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.brightToolStripMenuItem,
            this.darkToolStripMenuItem});
            this.setThemeToolStripMenuItem.Name = "setThemeToolStripMenuItem";
            this.setThemeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.setThemeToolStripMenuItem.Text = "Set Theme";
            // 
            // brightToolStripMenuItem
            // 
            this.brightToolStripMenuItem.Name = "brightToolStripMenuItem";
            this.brightToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.brightToolStripMenuItem.Text = "Bright";
            this.brightToolStripMenuItem.Click += new System.EventHandler(this.brightToolStripMenuItem_Click);
            // 
            // darkToolStripMenuItem
            // 
            this.darkToolStripMenuItem.Name = "darkToolStripMenuItem";
            this.darkToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.darkToolStripMenuItem.Text = "Dark";
            this.darkToolStripMenuItem.Click += new System.EventHandler(this.darkToolStripMenuItem_Click);
            // 
            // viewLogToolStripMenuItem
            // 
            this.viewLogToolStripMenuItem.Name = "viewLogToolStripMenuItem";
            this.viewLogToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.viewLogToolStripMenuItem.Text = "View Log";
            this.viewLogToolStripMenuItem.Click += new System.EventHandler(this.viewLogToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createShortcutToolStripMenuItem,
            this.downloadHDTexturesToolStripMenuItem,
            this.downloadToolsToolStripMenuItem,
            this.installToolsToolStripMenuItem,
            this.copyRomToolStripMenuItem,
            this.extractAssetsToolStripMenuItem,
            this.checkStatusToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(54, 20);
            this.toolStripMenuItem2.Text = "Scripts";
            // 
            // createShortcutToolStripMenuItem
            // 
            this.createShortcutToolStripMenuItem.Name = "createShortcutToolStripMenuItem";
            this.createShortcutToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.createShortcutToolStripMenuItem.Text = "Create Shortcut";
            this.createShortcutToolStripMenuItem.Click += new System.EventHandler(this.createShortcutToolStripMenuItem_Click);
            // 
            // downloadHDTexturesToolStripMenuItem
            // 
            this.downloadHDTexturesToolStripMenuItem.Name = "downloadHDTexturesToolStripMenuItem";
            this.downloadHDTexturesToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.downloadHDTexturesToolStripMenuItem.Text = "Download HD Textures";
            this.downloadHDTexturesToolStripMenuItem.Click += new System.EventHandler(this.downloadHDTexturesToolStripMenuItem_Click);
            // 
            // downloadToolsToolStripMenuItem
            // 
            this.downloadToolsToolStripMenuItem.Name = "downloadToolsToolStripMenuItem";
            this.downloadToolsToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.downloadToolsToolStripMenuItem.Text = "Download Tools";
            this.downloadToolsToolStripMenuItem.Click += new System.EventHandler(this.downloadToolsToolStripMenuItem_Click);
            // 
            // installToolsToolStripMenuItem
            // 
            this.installToolsToolStripMenuItem.Name = "installToolsToolStripMenuItem";
            this.installToolsToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.installToolsToolStripMenuItem.Text = "Install Tools";
            this.installToolsToolStripMenuItem.Click += new System.EventHandler(this.installToolsToolStripMenuItem_Click);
            // 
            // copyRomToolStripMenuItem
            // 
            this.copyRomToolStripMenuItem.Name = "copyRomToolStripMenuItem";
            this.copyRomToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.copyRomToolStripMenuItem.Text = "Copy Rom";
            this.copyRomToolStripMenuItem.Click += new System.EventHandler(this.copyRomToolStripMenuItem_Click);
            // 
            // extractAssetsToolStripMenuItem
            // 
            this.extractAssetsToolStripMenuItem.Name = "extractAssetsToolStripMenuItem";
            this.extractAssetsToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.extractAssetsToolStripMenuItem.Text = "Extract Assets";
            this.extractAssetsToolStripMenuItem.Click += new System.EventHandler(this.extractAssetsToolStripMenuItem_Click);
            // 
            // checkStatusToolStripMenuItem
            // 
            this.checkStatusToolStripMenuItem.Name = "checkStatusToolStripMenuItem";
            this.checkStatusToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.checkStatusToolStripMenuItem.Text = "Check Status";
            this.checkStatusToolStripMenuItem.Click += new System.EventHandler(this.checkStatusToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cloneToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.viewCommitOnGitHubToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.settingsToolStripMenuItem.Text = "Repository";
            // 
            // cloneToolStripMenuItem
            // 
            this.cloneToolStripMenuItem.Name = "cloneToolStripMenuItem";
            this.cloneToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.cloneToolStripMenuItem.Text = "Clone";
            this.cloneToolStripMenuItem.Click += new System.EventHandler(this.cloneToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.updateToolStripMenuItem.Text = "Update";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // viewCommitOnGitHubToolStripMenuItem
            // 
            this.viewCommitOnGitHubToolStripMenuItem.Name = "viewCommitOnGitHubToolStripMenuItem";
            this.viewCommitOnGitHubToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.viewCommitOnGitHubToolStripMenuItem.Text = "View Commit on GitHub";
            this.viewCommitOnGitHubToolStripMenuItem.Click += new System.EventHandler(this.viewCommitOnGitHubToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(81, 20);
            this.toolStripMenuItem3.Text = "Branch: dev";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // button5
            // 
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button5.Location = new System.Drawing.Point(183, 77);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(202, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "Compile OOOT";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button8
            // 
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button8.Location = new System.Drawing.Point(6, 19);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(379, 23);
            this.button8.TabIndex = 7;
            this.button8.Text = "Install Tools, Clone and Compile OOOT";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button9.Location = new System.Drawing.Point(6, 48);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(378, 23);
            this.button9.TabIndex = 8;
            this.button9.Text = "Clone and Compile OOOT";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.checkBox1.Location = new System.Drawing.Point(8, 81);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(99, 18);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "Extract Assets";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "PAL 1.0",
            "EUR MQD"});
            this.comboBox1.Location = new System.Drawing.Point(99, 79);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(78, 21);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.Text = "PAL 1.0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.button9);
            this.groupBox1.Controls.Add(this.button8);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Location = new System.Drawing.Point(12, 111);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(391, 115);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Builder:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(415, 238);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "OOOT BUILD GUI ver 0.6";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oOOTFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem builderFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cloneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem downloadToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem installToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyRomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractAssetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createShortcutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadHDTexturesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gitHubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewCommitOnGitHubToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem runOOOTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oOOTReleaseFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setThemeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewLogToolStripMenuItem;
    }
}

