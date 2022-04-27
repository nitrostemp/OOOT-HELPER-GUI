using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace OOOT_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            UpdateStatusLabel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "CMD.exe";
            p.StartInfo.Arguments = "/C gitclone.bat";
            p.EnableRaisingEvents = true;
            p.Exited += new EventHandler(UpdateStatusLabel);
            p.Start();
            p.WaitForExit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strCmdDownload1 = "/C curl -LJO https://aka.ms/vs/17/release/vs_BuildTools.exe --output buildtoolinstall.exe";
            string strCmdDownload2 = "/C curl -LJO https://www.python.org/ftp/python/3.10.4/python-3.10.4-amd64.exe --output pythoninstall.exe";
            string strCmdDownload3 = "/C curl -LJO https://github.com/git-for-windows/git/releases/download/v2.36.0.windows.1/Git-2.36.0-64-bit.exe --output gitinstall.exe";
            Process.Start("CMD.exe", strCmdDownload1 + strCmdDownload2 + strCmdDownload3);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("CMD.exe", "/C install.bat");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start("CMD.exe", "/C pullgit.bat");
            UpdateStatusLabel();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process.Start("CMD.exe", "/C compile.bat");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Process.Start("CMD.exe", "/C copyrom.bat");
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

            label1.Text = Helpers.ParseGitStatusString(output);
        }

        private void UpdateStatusLabel()
        {
            UpdateStatusLabel(null, null);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Process.Start("CMD.exe", "/C extract_assets.bat");
        }
    }
}
