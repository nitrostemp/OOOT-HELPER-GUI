using System;
using System.Windows.Forms;

namespace OOOT_GUI
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            textBox1.Text = Builder.InstallDir;
            textBox2.Text = Builder.TempDownloadDir;
        }

        private void button2_Click(object sender, EventArgs e) // save settings
        {
            Builder.SetInstallDir(textBox1.Text, true);
            Builder.TempDownloadDir = textBox2.Text;
            UpdateStatusLabel();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e) // browse install path
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
                textBox1.Text = fbd.SelectedPath;
        }

        private void button4_Click(object sender, EventArgs e) // browse temp download path
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
                textBox2.Text = fbd.SelectedPath;
        }

        private void UpdateStatusLabel() // update status label in Form1
        {
            if (Form1.form1 != null)
                Form1.form1.UpdateUI(null, null);
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // save unsaved changes
            if (textBox1.Text != Builder.InstallDir || textBox2.Text != Builder.TempDownloadDir)
            {
                if (MessageBox.Show("Save changes?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Builder.SetInstallDir(textBox1.Text, true);
                    Builder.TempDownloadDir = textBox2.Text;
                }
                    
            }

            textBox1.Text = Builder.InstallDir;
            textBox2.Text = Builder.TempDownloadDir;

            // update form1 status label
            if (Form1.form1 != null)
                Form1.form1.UpdateUI(null, null);

            // hide instead destroying for future use
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = Builder.InstallDir;
            textBox2.Text = Builder.TempDownloadDir;
        }

    }
}
