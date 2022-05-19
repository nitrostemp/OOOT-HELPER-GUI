using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;

namespace OOOT_GUI
{
    public partial class Form1 : Form
    {
        // Forms
        public static Form1 form1;
        private SettingsForm settingsForm;

        // Theme Settings
        public enum Theme { Bright, Dark, Custom };
        public Theme CurrentTheme = Theme.Bright;
        private Color ColorBack = Color.FromArgb(240, 240, 240);
        private Color ColorFore = Color.FromArgb(0, 0, 0);

        public void ChangeTheme(Theme newTheme)
        {
            CurrentTheme = newTheme;

            // get new colors by new theme
            switch (newTheme)
            {
                case Theme.Bright:
                    ColorBack = Color.FromArgb(240, 240, 240);
                    ColorFore = Color.FromArgb(0, 0, 0);
                    break;
                case Theme.Dark:
                    ColorBack = Color.FromArgb(32, 33, 36);
                    ColorFore = Color.FromArgb(177, 177, 177);
                    break;
                case Theme.Custom: // TODO: TEMP
                    ColorBack = Color.FromArgb(32, 33, 36);
                    ColorFore = Color.FromArgb(177, 177, 177);
                    break;
                default:
                    break;
            }

            // Update Form1 colors
            BackColor = ColorBack;
            ForeColor = ColorFore;

            foreach (Control c in this.Controls)
            {
                UpdateColorControls(c);
            }

            // Form1 Menustrip
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                foreach (ToolStripMenuItem item2 in item.DropDownItems)
                {
                    item2.BackColor = ColorBack;
                    item2.ForeColor = ColorFore;
                }
            }

            // set theme selection menu items color
            darkToolStripMenuItem.BackColor = ColorBack;
            brightToolStripMenuItem.BackColor = ColorBack;
            darkToolStripMenuItem.ForeColor = ColorFore;
            brightToolStripMenuItem.ForeColor = ColorFore;

            // Update SettingsForm colors
            if (settingsForm != null)
            {
                settingsForm.BackColor = ColorBack;
                settingsForm.ForeColor = ColorFore;

                foreach (Control c in settingsForm.Controls)
                {
                    UpdateColorControls(c);
                }
            }

            Builder.SaveSettings();
        }

        public void UpdateColorControls(Control control)
        {
            // Set FlatStyle to various elements (Bright = System, Dark = Flat)
            FlatStyle flatStyle = (CurrentTheme == Theme.Bright ? FlatStyle.System : FlatStyle.Flat);
            if (control is Button)
            {
                Button button = control as Button;
                button.FlatStyle = flatStyle;
            }
            else if (control is ComboBox)
            {
                ComboBox comboBox = control as ComboBox;
                comboBox.FlatStyle = flatStyle;
            }
            else if (control is CheckBox)
            {
                CheckBox checkBox = control as CheckBox;
                checkBox.FlatStyle = flatStyle;
            }

            // set black/white colors on bright theme to spesific elements
            if (CurrentTheme == Theme.Bright && (control is TextBox || control is ComboBox || control is CheckBox))
            {
                control.BackColor = Color.White;
                control.ForeColor = Color.Black;
            }
            // set global theme color
            else
            {
                control.BackColor = ColorBack;
                control.ForeColor = ColorFore;
            }

            // make dark theme buttons and combobox darker
            if (CurrentTheme == Theme.Dark)
            {
                int R = ColorBack.R - 7;
                int G = ColorBack.G - 7;
                int B = ColorBack.B - 7;

                if (R < 0) R = 0;
                if (G < 0) G = 0;
                if (B < 0) B = 0;

                Color darkerColor = Color.FromArgb(R, G, B);

                if (control is Button || control is ComboBox || control is MenuStrip)
                    control.BackColor = darkerColor;
            }

            foreach (Control subC in control.Controls)
            {
                UpdateColorControls(subC);
            }
        }

        public int GetThemeID()
        {
            return (int)CurrentTheme;
        }

        public Form1()
        {
            form1 = this;
            settingsForm = new SettingsForm();
            InitializeComponent();
            Builder.LoadSettings();
            UpdateUI();
        }

        private void button5_Click(object sender, EventArgs e) // Compile
        {
            // check if vs build tools install processes exist
            bool block = false;
            Process[] vsProcesses = Process.GetProcesses();
            foreach (Process p in vsProcesses)
            {
                if (p.MainWindowTitle.Contains("Visual Studio Installer"))
                    block = true;
                else if (p.ProcessName == "vs_BuildTools")
                    block = true;
                else if (p.ProcessName == "vs_setup_bootstrapper")
                    block = true;
            }

            // vs build tools processes detected, can't compile yet
            if (block)
            {
                DialogResult result = MessageBox.Show("Wait for VS Build Tools to install before compiling.", "Error!", MessageBoxButtons.AbortRetryIgnore);
                if (result == DialogResult.Abort)
                    return;
                if (result == DialogResult.Retry)
                {
                    button5_Click(sender, e);
                    return;
                }
            }

            // no rom found
            if (!IsValidRomAvailable(true))
                return;

            // (optional) extract assets
            if (checkBox1.Checked)
                if (!Builder.ExtractAssets(Builder.GetRomVersion(IsEurMqd())))
                    return;

            // build OOOT
            Builder.Build(IsEurMqd());
        }

        private void button8_Click(object sender, EventArgs e) // All-in-one
        {
            DoFullSetup(true);
        }

        private void button9_Click(object sender, EventArgs e) // Clone and compile
        {
            DoFullSetup(false);
        }

        private void cloneToolStripMenuItem_Click(object sender, EventArgs e) // clone repo
        {
            Builder.Clone();
            UpdateUI();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e) // update repo
        {
            Builder.Update();
            UpdateUI();
        }

        private void oOOTFolderToolStripMenuItem_Click(object sender, EventArgs e) // open OOOT directory
        {
            OpenFolder(Builder.GetOootPath());
        }

        private void builderFolderToolStripMenuItem_Click(object sender, EventArgs e) // open Builder directory
        {
            OpenFolder(Builder.GetBuilderPath());
        }

        private void oOOTReleaseFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFolder(Path.GetDirectoryName(Builder.GetOootExePath()));
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) // delete repo
        {
            Builder.DeleteRepo();
            UpdateUI();
        }

        private void downloadToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Builder.DownloadTools(false, true);
        }

        private void installToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Builder.InstallTools(true);
        }

        private void copyRomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Builder.CopyRom(Builder.GetRomFilename(IsEurMqd()), Builder.GetRomVersion(IsEurMqd()));
        }

        private void extractAssetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Builder.ExtractAssets(Builder.GetRomVersion(IsEurMqd()));
        }

        private void createShortcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Builder.CreateShortcut();
        }

        private void downloadHDTexturesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Builder.DownloadAndInstallHdTextures();
        }

        private void pathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsForm.Show();
        }

        private void gitHubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/blawar/ooot");
        }

        private void runOOOTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Builder.LaunchGame();
        }

        private void viewCommitOnGitHubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Builder.DoesRepositoryExist())
            {
                MessageBox.Show("No OOOT repository found!", "Error!");
                return;
            }

            string commitInfo = Builder.GetCommitSummary();
            string commitId = ParseGitStatusString(commitInfo, true);

            if (string.IsNullOrEmpty(commitId))
                return;

            string url = $"https://github.com/blawar/ooot/commit/{commitId}";

            Process.Start(url);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (toolStripMenuItem3.DropDownItems.Count == 0)
                UpdateBranches();
        }

        /// <summary>
        /// Show status of installed Tools and available ROMs.
        /// </summary>
        private void checkStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = "Git installed: " + (Builder.IsGitInstalled() ? "OK" : "FAILED");
            text += "\nPython installed: " + (Builder.IsPythonInstalled() ? "OK" : "FAILED");
            text += "\nVS Build Tools installed: " + (Builder.IsVsBuildToolsInstalled() ? "OK" : "FAILED");
            text += "\n";
            text += "\nPAL 1.0 ROM: " + (IsValidRomAvailable(false, "PAL_1.0") ? "OK" : "FAILED");
            text += "\nEUR MQD ROM: " + (IsValidRomAvailable(false, "EUR_MQD") ? "OK" : "FAILED");

            MessageBox.Show(text, "Status");
        }

        private void brightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme(Theme.Bright);
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeTheme(Theme.Dark);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Builder.SaveSettings(Builder.GetRomVersion(IsEurMqd()), checkBox1.Checked.ToString());
        }

        /// <summary>
        /// Setup OOOT from scratch (Download/Install Tools, Clone repo, Copy and setup ROM, and build.)
        /// </summary>
        private void DoFullSetup(bool installTools)
        {
            // no rom found
            if (!IsValidRomAvailable(false))
                return;

            // download/install tools
            if (installTools)
                Builder.DownloadTools(true, true);

            // clone repo
            if (!Builder.Clone())
                return;

            // update UI
            UpdateUI();

            // get rom settings
            bool isEurMqd = IsEurMqd();
            string romVersion = Builder.GetRomVersion(isEurMqd);
            string romPath = Builder.GetRomFilename(isEurMqd);

            // copy rom
            Builder.CopyRom(romPath, romVersion);

            // extract assets
            Builder.ExtractAssets(romVersion);

            // build
            Builder.Build(isEurMqd);
        }

        public void UpdateUI(object sender, EventArgs e)
        {
            // update commit info
            string commitInfo = Builder.GetCommitSummary();
            label1.Text = ParseGitStatusString(commitInfo);

            // update branch
            UpdateBranches();
            string currentBranch = Builder.GetCurrentBranchName();
            if (!string.IsNullOrEmpty(currentBranch))
                Builder.CurrentBranch = currentBranch;
            toolStripMenuItem3.Text = $"Branch: {Builder.CurrentBranch}";
        }

        private void UpdateUI()
        {
            UpdateUI(null, null);
        }

        private string ParseGitStatusString(string cmdOutput, bool returnCommitIdOnly = false)
        {
            string errorString = "No OOOT repository found.";

            string[] lines = cmdOutput.Split('\n');
            if (lines == null || lines.Length == 0)
                return errorString;

            string commit = "";
            string date = "";
            string title = "";

            // get commit id
            commit = lines.Where(x => x.StartsWith("commit ")).FirstOrDefault();
            if (!string.IsNullOrEmpty(commit))
                commit = commit.Replace("commit ", "");

            // get commit date
            date = lines.Where(x => x.StartsWith("Date:")).FirstOrDefault();

            if (string.IsNullOrEmpty(commit) || string.IsNullOrEmpty(date))
                return errorString;

            // early commit id only return, if set so
            if (returnCommitIdOnly)
                return commit;

            // get commit title
            if (lines.Length > 3 && !string.IsNullOrEmpty(lines[4]))
                title = lines[4];

            // build string
            string result = "Commit: " + commit + "\n" + date;
            if (!string.IsNullOrEmpty(title))
                result += "\nTitle: " + title;

            return result;
        }

        private bool IsEurMqd()
        {
            return comboBox1.SelectedIndex == 1;
        }

        /// <summary>
        /// Is a valid ROM file in Builder or 'ooot/roms' folder?
        /// </summary>
        private bool IsValidRomAvailable(bool showErrorMessage, string romVersion = "")
        {
            bool isEurMqd = IsEurMqd();

            // (optional) override global rom version
            if (romVersion == "PAL_1.0")
                isEurMqd = false;
            else if (romVersion == "EUR_MQD")
                isEurMqd = true;

            // update rom version    
            romVersion = Builder.GetRomVersion(isEurMqd);
   
            // check if rom is in oot/roms folder, or copy from Builder if needed
            bool value = Builder.IsRomInRomsFolder(isEurMqd, false);
            if (!value)
            {
                string romFilename = Builder.GetRomFilename(isEurMqd);
                value = !string.IsNullOrEmpty(romFilename);
                if (value)
                    Builder.CopyRom(romFilename, romVersion, showErrorMessage);
            }

            if (!value && showErrorMessage)
                MessageBox.Show($"No valid ROM found from Builder or OOOT/roms/{romVersion} folders!", "Error!");

            return value;
        }

        private void OpenFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                MessageBox.Show("Error: Can't open folder: " + path);
                return;
            }

            Process.Start(path);
        }

        /// Change Branch
        private void MenuBranchClickHandler(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            string newBranch = clickedItem.Text;
            if (MessageBox.Show($"Do you want to switch branch from '{Builder.CurrentBranch}' to '{newBranch}'?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Builder.CMD($"/C git checkout {newBranch}", Builder.GetOootPath(), false);
                Builder.CurrentBranch = newBranch;
            }
            UpdateUI();
        }

        /// Create button for every branch in Branch menu.
        private void UpdateBranches()
        {
            List<string> branches = Builder.GetAllBranches();
            if (branches == null)
                branches = new List<string>();

            if (branches.Count == 0) // no real branches found, so create dummy ones
            {
                branches.Add("dev");
                branches.Add("master");
            }

            List<ToolStripMenuItem> items = new List<ToolStripMenuItem>();

            // create button for every branch
            foreach (string branch in branches)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Name = "buttonBranch" + branch;
                item.Text = branch;
                item.Click += new EventHandler(MenuBranchClickHandler);
                item.BackColor = ColorBack;
                item.ForeColor = ColorFore;
                items.Add(item);
            }

            // add buttons to menu
            toolStripMenuItem3.DropDownItems.Clear();
            toolStripMenuItem3.DropDownItems.AddRange(items.ToArray());
        }

        // used when loading 'settings.txt'
        public void SetRomVersion(bool isEurMqd)
        {
            if (isEurMqd)
            {
                comboBox1.SelectedIndex = 1;
                comboBox1.Text = "EUR_MQD";
            }
            else
            {
                comboBox1.SelectedIndex = 0;
                comboBox1.Text = "PAL_1.0";
            }
        }

        // used when loading 'settings.txt'
        public void SetExtractAssetsCheckbox(bool value)
        {
            checkBox1.Checked = value;
        }
    }
}