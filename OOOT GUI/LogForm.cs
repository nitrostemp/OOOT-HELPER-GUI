using System;
using System.Windows.Forms;

namespace OOOT_GUI
{
    public partial class LogForm : Form
    {
        public static LogForm logForm;

        public LogForm()
        {
            InitializeComponent();
            logForm = this;
        }

        public void AddText(string msg)
        {
            richTextBox1.Text += msg;
        }

        public void ClearText()
        {
            richTextBox1.Text = "";
        }

        private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // hide instead destroying for future use
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save log to file";
            sfd.DefaultExt = "txt";
            sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            sfd.FilterIndex = 1;
            sfd.RestoreDirectory = true;
            sfd.FileName = "builder_log.txt";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.IO.File.WriteAllText(sfd.FileName, richTextBox1.Text);
                    MessageBox.Show("Saved log to: " + sfd.FileName);
                }
                catch
                {
                    MessageBox.Show("Error when saving log to file!", "Error!");
                }
            }
        }
    }

    public static class Log
    {
        public static bool LogEnabled = true;

        public static void Message(string message)
        {
            if (LogEnabled && LogForm.logForm != null)
                LogForm.logForm.AddText(message + "\n");
        }

        public static void Clear()
        {
            if (LogEnabled && LogForm.logForm != null)
                LogForm.logForm.ClearText();
        }
    }
}
