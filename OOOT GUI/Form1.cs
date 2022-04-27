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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strCmdClone;
            strCmdClone = "/C git clone --recursive -b dev https://github.com/blawar/ooot.git";
            System.Diagnostics.Process.Start("CMD.exe", strCmdClone);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string strCmdDownload1;
            strCmdDownload1 = "/C curl -LJO https://aka.ms/vs/17/release/vs_BuildTools.exe --output buildtoolinstall.exe";
            string strCmdDownload2;
            strCmdDownload2 = "/C curl -LJO https://www.python.org/ftp/python/3.10.4/python-3.10.4-amd64.exe --output pythoninstall.exe";
            string strCmdDownload3;
            strCmdDownload3 = "/C curl -LJO https://github.com/git-for-windows/git/releases/download/v2.36.0.windows.1/Git-2.36.0-64-bit.exe";

            System.Diagnostics.Process.Start("CMD.exe", strCmdDownload1 + strCmdDownload2 + strCmdDownload3);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strCmdInstall1;
            strCmdInstall1 = "/C install.bat";

            System.Diagnostics.Process.Start("CMD.exe", strCmdInstall1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string strCmdPG;
            strCmdPG = "/C pullgit.bat";
         

            System.Diagnostics.Process.Start("CMD.exe", strCmdPG);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string strCmdBOOOT;
            strCmdBOOOT = "/C compile.bat";


            System.Diagnostics.Process.Start("CMD.exe", strCmdBOOOT);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string strCmdCR;
            strCmdCR = "/C copyrom.bat";


            System.Diagnostics.Process.Start("CMD.exe", strCmdCR);
        }
    }
}
