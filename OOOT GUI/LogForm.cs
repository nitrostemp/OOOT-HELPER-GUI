using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
