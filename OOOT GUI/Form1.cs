using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;

namespace OOOT_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            UpdateStatusLabel();
        }

        private void button1_Click(object sender, EventArgs e) // Clone repository
        {
            Process p = new Process();
            p.StartInfo.FileName = "CMD.exe";
            p.StartInfo.Arguments = "/C gitclone.bat";
            p.EnableRaisingEvents = true;
            p.Exited += new EventHandler(UpdateStatusLabel);
            p.Start();
            p.WaitForExit();
        }

        private void button3_Click(object sender, EventArgs e) // Download tools
        {
            string strCmdDownload1 = "/C curl -LJO https://aka.ms/vs/17/release/vs_BuildTools.exe --output buildtoolinstall.exe";
            string strCmdDownload2 = "/C curl -LJO https://www.python.org/ftp/python/3.10.4/python-3.10.4-amd64.exe --output pythoninstall.exe";
            string strCmdDownload3 = "/C curl -LJO https://github.com/git-for-windows/git/releases/download/v2.36.0.windows.1/Git-2.36.0-64-bit.exe --output gitinstall.exe";
            RunProcess(strCmdDownload1 + strCmdDownload2 + strCmdDownload3);
        }

        private void button2_Click(object sender, EventArgs e) // Install tools
        {
            RunProcess("/C install.bat");
        }

        private void button4_Click(object sender, EventArgs e) // Update repository
        {
            RunProcess("/C pullgit.bat");
            UpdateStatusLabel();
        }

        private void button5_Click(object sender, EventArgs e) // Compile
        {
            if (checkBox1.Checked)
                RunProcess("/C extract_assets.bat");

            RunProcess("/C compile.bat");
        }

        private void button6_Click(object sender, EventArgs e) // Copy ROM
        {
            RunProcess("/C copyrom.bat");
        }

        private void button7_Click(object sender, EventArgs e) // Extract assets
        {
            RunProcess("/C extract_assets.bat");
        }

        private void button8_Click(object sender, EventArgs e) // All-in-one
        {
            DoFullSetup(true);
        }

        private void button9_Click(object sender, EventArgs e) // Clone and compile
        {
            DoFullSetup(false);
        }

        private void DoFullSetup(bool installTools)
        {
            if (installTools)
            {
                button3_Click(null, null); // download tools
                button2_Click(null, null); // install tools
            }

            button1_Click(null, null); // clone repo
            button6_Click(null, null); // copy rom
            button7_Click(null, null); // extract assets

            RunProcess("/C compile.bat"); // compile
        }

        private void UpdateStatusLabel(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "CMD.exe";
            p.StartInfo.Arguments = "/C gitstatus.bat";
            p.Start();

            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            label1.Text = ParseGitStatusString(output);
        }

        private void UpdateStatusLabel()
        {
            UpdateStatusLabel(null, null);
        }

        private void RunProcess(string command)
        {
            Process p = new Process();
            p.StartInfo.FileName = "CMD.exe";
            p.StartInfo.Arguments = command;
            p.Start();
            p.WaitForExit();
        }

        private string ParseGitStatusString(string cmdOutput)
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

            // get commit title
            if (lines.Length > 3 && !string.IsNullOrEmpty(lines[4]))
                title = lines[4];

            // build string
            string result = "Commit: " + commit + "\n" + date;
            if (!string.IsNullOrEmpty(title))
                result += "\nTitle: " + title;

            return result;
        }
    }
}
