using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.IO;
using IWshRuntimeLibrary;

namespace OOOT_GUI
{
    public partial class Form1 : Form
    {
        // Valid ROM Hashes (.z64, .n64, .v64)
        private string[] md5HashesPal = { "e040de91a74b61e3201db0e2323f768a", "f8ef2f873df415fc197f4a9837d7e353", "9526b263b60577d8ed22fb7a33c2facd" };
        private string[] md5HashesEurMqd = { "f751d1a097764e2337b1ac9ba1e27699", "ce96bd52cb092d8145fb875d089fa925", "cbd40c8fb47404678b97cba50d2af495" };

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
                RunProcess("/C extract_assets.bat" + GetRomVersionParameter());

            RunProcess("/C compile.bat");
        }

        private void button6_Click(object sender, EventArgs e) // Copy ROM
        {
            RunProcess("/C copyrom.bat" + GetRomVersionParameter() + GetRomFilename());
        }

        private void button7_Click(object sender, EventArgs e) // Extract assets
        {
            RunProcess("/C extract_assets.bat" + GetRomVersionParameter());
        }

        private void button8_Click(object sender, EventArgs e) // All-in-one
        {
            DoFullSetup(true);
        }

        private void button9_Click(object sender, EventArgs e) // Clone and compile
        {
            DoFullSetup(false);
        }

        private void button10_Click(object sender, EventArgs e) // Create shortcut to OOOT
        {
            string path = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%");
            string exePath = Path.Combine(path, @"ooot\vs\Release\OOT.exe");

            // show error and exit early, if no .exe found
            if (!System.IO.File.Exists(exePath))
            {
                MessageBox.Show("ERROR: Can't create shortcut, no OOT.exe found!");
                return;
            }

            try
            {
                object shDesktop = (object)"Desktop";
                WshShell shell = new WshShell();
                string shortcutPath = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\OpenOcarina.lnk";
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
                shortcut.Description = "OpenOcarina";
                shortcut.TargetPath = exePath;
                shortcut.Save();
            }
            catch
            {
                MessageBox.Show("ERROR: Something went wrong, when trying to create shortcut!");
            }
        }
        private void button11_Click(object sender, EventArgs e) // Download HD Texture Pack
        {
            RunProcess("/C hdtexutres.bat" + GetRomVersionParameter());
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

        private string GetRomFilename()
        {
            bool isEurMqd = IsEurMqd();
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            // get rom files (.z64 first, then .n64 and .v64)
            List<string> files = new List<string>();
            files.AddRange(Directory.GetFiles(path, "*.z64"));
            files.AddRange(Directory.GetFiles(path, "*.n64"));
            files.AddRange(Directory.GetFiles(path, "*.v64"));

            if (files.Count > 0)
            {
                foreach (string file in files)
                {
                    string md5Hash = CalculateMD5(file);

                    if (isEurMqd && md5HashesEurMqd.Contains(md5Hash) || !isEurMqd && md5HashesPal.Contains(md5Hash))
                        return " " + Path.GetFileName(file);
                }
            }

            return "";
        }

        private string GetRomVersionParameter()
        {
            return IsEurMqd() ? " EUR_MQD" : " PAL_1.0";
        }

        private bool IsEurMqd()
        {
            return comboBox1.SelectedIndex == 1;
        }

        private string CalculateMD5(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return "";

            try
            {
                using (var md5 = MD5.Create())
                {
                    using (var stream = System.IO.File.OpenRead(filename))
                    {
                        var hash = md5.ComputeHash(stream);
                        return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                    }
                }
            }
            catch
            {
                return "";
            }
        }
    }
}