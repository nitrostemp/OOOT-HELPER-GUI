using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using IWshRuntimeLibrary;
using System.Security.Cryptography;

namespace OOOT_GUI
{
    public class Builder
    {
        // Builder Settings
        public static string InstallDir = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%"); // Directory containing 'ooot' folder.
        public static string TempDownloadDir = Path.Combine(GetBuilderPath(), "ooot_temp"); // Temporary Tools download folder.
        public static string CurrentBranch = "dev";

        // Valid ROM Hashes (.z64, .n64, .v64)
        public static string[] Md5HashesPal = { "e040de91a74b61e3201db0e2323f768a", "f8ef2f873df415fc197f4a9837d7e353", "9526b263b60577d8ed22fb7a33c2facd" };
        public static string[] Md5HashesEurMqd = { "f751d1a097764e2337b1ac9ba1e27699", "ce96bd52cb092d8145fb875d089fa925", "cbd40c8fb47404678b97cba50d2af495" };

        private static string GetSettingsSavePath()
        {
            return Path.Combine(GetBuilderPath(), "settings.txt");
        }

        public static string GetBuilderPath()
        {
            return Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        }

        public static string GetToolsPath()
        {
            return Path.Combine(GetBuilderPath(), "Tools");
        }

        public static string GetOootPath()
        {
            return Path.Combine(InstallDir, "ooot");
        }

        public static string GetOootExePath()
        {
            return Path.Combine(GetOootPath(), @"vs\Release\OOT.exe");
        }

        public static string GetTempDownloadPath(bool createFolder)
        {
            string path = TempDownloadDir;

            if (!Directory.Exists(path))
            {
                if (createFolder)
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                    }
                    catch { }
                }
            }

            return path;
        }

        public static void SetInstallDir(string path, bool save)
        {
            InstallDir = path;
            if (save)
                SaveSettings();
        }

        /// Clone Repository
        public static bool Clone()
        {
            if (!Directory.Exists(InstallDir))
            {
                try
                {
                    Directory.CreateDirectory(InstallDir);
                }
                catch
                {
                    MessageBox.Show("Error: Invalid installation directory.", "Error!");
                    return false;
                }
            }

            // default to master branch in case of empty string
            if (string.IsNullOrEmpty(CurrentBranch))
                CurrentBranch = "master";

            CMD($"/C git clone --recursive -b {CurrentBranch} https://github.com/blawar/ooot.git", InstallDir);

            return true;
        }

        /// Git Pull Repository
        public static void Update()
        {
            if (!DoesRepositoryExist())
            {
                if (MessageBox.Show("No repository found. Do you want to clone it?", "Error!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Clone();
                    return;
                }

                return;
            }

            CMD("/C git pull", GetOootPath(), false);
        }

        /// Delete 'ooot' Folder
        public static void DeleteRepo()
        {
            string oootPath = GetOootPath();
            if (!Directory.Exists(oootPath))
            {
                MessageBox.Show("No repository found!", "Error!");
                return;
            }

            // Confirmation window
            if (MessageBox.Show($"Do you really want to delete the repository? ({oootPath})", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // remove dir
                try
                {
                    DeleteDirectory(oootPath);
                }
                catch { }

                if (!Directory.Exists(oootPath)) // success
                    MessageBox.Show("Repository deleted!");
                else // failure
                    MessageBox.Show("Something went wrong when deleting the repository!", "Error!");
            }
        }

        private static void DeleteDirectory(string path)
        {
            string[] files = Directory.GetFiles(path);
            string[] dirs = Directory.GetDirectories(path);

            // need to uncheck "Read only" from files to be able to delete them
            foreach (string file in files)
            {
                System.IO.File.SetAttributes(file, FileAttributes.Normal);
                System.IO.File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(path, false);
        }

        public static void Build(bool isEurMqd)
        {
            // check if repo exists
            if (!DoesRepositoryExist())
            {
                // clone and continue building, or cancel and exit early
                DialogResult result = MessageBox.Show("No repository found. Do you want to clone it?", "Error!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    Clone();
                else
                    return;
            }

            // check if correct rom is present in 'ooot\roms' folder
            // if not, try to find and copy it from the Builder folder
            if (!IsRomInRomsFolder(isEurMqd))
            {
                string romFileName = GetRomFilename(isEurMqd);
                string romPath = Path.Combine(GetBuilderPath(), romFileName);

                CopyRom(romFileName, romPath);

                // no rom found from Builder folder, exit
                if (string.IsNullOrEmpty(romFileName) || !System.IO.File.Exists(romPath))
                {
                    MessageBox.Show("No valid ROM found from OOOT or builder folder!", "Error!");
                    return;
                }
            }

            string ootPath = GetOootPath();

            // build
            string driveLetter = ootPath[0].ToString() + ":";
            CMD($"/C compile.bat {driveLetter} {ootPath}", GetToolsPath());

            if (System.IO.File.Exists(GetOootExePath())) // compiled OK, create shortcut?
            {
                if (MessageBox.Show("OOOT compiled succesfully. Want to create shortcut?", "Finished", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    CreateShortcut();
            }
            else // compile FAILED
            {
                MessageBox.Show("Error: Something went wrong when building.", "Error!");
            }
        }

        public static bool ExtractAssets(string romVersion)
        {
            if (!DoesRepositoryExist())
            {
                // clone and continue extracting asset, or cancel and exit early
                DialogResult result = MessageBox.Show("No repository found. Do you want to clone it?", "Error!", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                    Clone();
                else
                    return false;
            }

            CMD($"/C setup.py -b {romVersion} -c", GetOootPath(), true);

            return true;
        }

        public static void LaunchGame()
        {
            string exePath = GetOootExePath();

            if (System.IO.File.Exists(exePath))
            {
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.WorkingDirectory = Path.GetDirectoryName(exePath);
                p.StartInfo.FileName = exePath;
                p.Start();
            }
            else
                MessageBox.Show($"No OOT.exe found! ({exePath})", "Error!");
        }

        public static void CreateShortcut() // Create shortcut to OOOT
        {
            string exePath = GetOootExePath();

            // show error and exit early, if no .exe found
            if (!System.IO.File.Exists(exePath))
            {
                MessageBox.Show("Can't create shortcut, no OOT.exe found!", "Error!");
                return;
            }

            // create shortcut
            try
            {
                object shDesktop = (object)"Desktop";
                WshShell shell = new WshShell();
                string shortcutPath = (string)shell.SpecialFolders.Item(ref shDesktop) + @"\OpenOcarina.lnk";
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
                shortcut.Description = "OpenOcarina";
                shortcut.TargetPath = exePath;
                shortcut.WorkingDirectory = Path.GetDirectoryName(exePath);
                shortcut.Save();

                if (System.IO.File.Exists(shortcutPath)) // succes, launch game?
                {
                    if (MessageBox.Show("Shortcut to OOOT created! Launch game?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        LaunchGame();
                }
                else // failure
                {
                    MessageBox.Show("Something went wrong, when trying to create shortcut!", "Error!");
                }
            }
            catch // even worse failure
            {
                MessageBox.Show("ERROR: Something went wrong, when trying to create shortcut!", "Error!");
            }
        }

        /// <summary>
        /// Copy ROM from Builder folder to 'ooot\roms\xxx_xxx\'
        /// </summary>
        public static bool CopyRom(string filename, string romVersion)
        {
            string romDirPath = Path.Combine(GetOootPath(), @"roms\" + romVersion + @"\");
            if (!Directory.Exists(romDirPath))
            {
                MessageBox.Show($"Can't copy ROM because OOOT roms directory is missing!", "Error!");
                return false;
            }

            string source = Path.Combine(GetBuilderPath(), filename); // TODO: custom rom path
            string destination = Path.Combine(romDirPath + @"baserom_original.n64");

            if (System.IO.File.Exists(source))
                System.IO.File.Copy(source, destination, true);

            return System.IO.File.Exists(source);
        }

        /// <summary>
        /// Return 'git show --summary' CMD output as string.
        /// </summary>
        public static string GetCommitSummary()
        {
            string output = "";
            if (Directory.Exists(GetOootPath()))
            {
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.FileName = "CMD.exe";
                p.StartInfo.WorkingDirectory = Builder.GetOootPath();
                p.StartInfo.Arguments = "/C git show --summary .";
                p.Start();

                output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
            }

            return output;
        }

        /// <summary>
        /// Download (and optionally install) build tools.
        /// </summary>
        public static void DownloadTools(bool installTools, bool showMessages)
        {
            bool git = IsGitInstalled();
            bool python = IsPythonInstalled();
            bool vsBuild = IsVsBuildToolsInstalled();

            // tools are already installed, show optional message and early exit
            if (git && python && vsBuild)
            {
                if (showMessages)
                    MessageBox.Show("All tools are already installed.");
                return;
            }

            string tmpDir = GetTempDownloadPath(true);
            if (!Directory.Exists(tmpDir))
            {
                MessageBox.Show($"Can't create temporary download directory for tools! ({tmpDir}");
                return;
            }

            // create download commands for tools
            string tool1 = "curl -LJO https://aka.ms/vs/17/release/vs_BuildTools.exe";
            string tool2 = "curl -LJO https://www.python.org/ftp/python/3.10.4/python-3.10.4-amd64.exe";
            string tool3 = "curl -LJO https://github.com/git-for-windows/git/releases/download/v2.36.0.windows.1/Git-2.36.0-64-bit.exe";

            // override commands, if files are already downloaded
            if (System.IO.File.Exists(Path.Combine(tmpDir, "vs_BuildTools.exe")))
                tool1 = "echo 'vs_BuildTools.exe' is already downloaded, skipping...";
            if (System.IO.File.Exists(Path.Combine(tmpDir, "python-3.10.4-amd64.exe")))
                tool2 = "echo 'python-3.10.4-amd64.exe' is already downloaded, skipping...";
            if (System.IO.File.Exists(Path.Combine(tmpDir, "vs_BuildTools.exe")))
                tool3 = "echo 'Git-2.36.0-64-bit.exe' is already downloaded, skipping...";

            // override commands, if tools are already installed
            if (IsGitInstalled())
                tool3 = "echo Git is already installed, skipping...";
            if (IsPythonInstalled())
                tool2 = "echo Python is already installed, skipping...";
            if (IsVsBuildToolsInstalled())
                tool1 = "echo Vs Build Tools is already installed, skipping...";

            // build download command(s)
            string command = $"/C echo Download directory: {tmpDir} && echo.";
            command += @"&& echo Downloading 'vs_BuildTools.exe'... && echo. && " + tool1;
            command += @"&& echo. && echo Dowloading 'python-3.10.4-amd64.exe' && echo. && " + tool2;
            command += @"&& echo. && echo Downloading 'Git-2.36.0-64-bit.exe' && echo. && " + tool3;

            // execute command(s)
            CMD(command, tmpDir, true);

            // install tool after download, if set so
            if (installTools)
                InstallTools(showMessages);
        }

        /// <summary>
        /// Install build tools.
        /// </summary>
        public static void InstallTools(bool showMessages)
        {
            bool git = IsGitInstalled();
            bool python = IsPythonInstalled();
            bool vsBuild = IsVsBuildToolsInstalled();

            // tools are already installed, show optional message and early exit
            if (git && python && vsBuild)
            {
                if (showMessages)
                    MessageBox.Show("All tools are already installed.");
                return;
            }

            // get temp download path
            string tmpDir = GetTempDownloadPath(false);
            if (!Directory.Exists(tmpDir))
            {
                MessageBox.Show($"Can't find temporary download directory for tools! ({tmpDir}");
                return;
            }

            string command = @"/C SET usermode=basic";
            command += @"&& echo Installing tools from: " + tmpDir;
            command += @"&& echo This can take a while. && echo.";

            if (!git)
                command += @"&& echo (1/3) Installing Git... && echo. && Git-2.36.0-64-bit.exe /VERYSILENT";
            else
                command += @"&& echo (1/3) Git is already installed, skipping... && echo.";

            if (!python)
                command += @"&& echo. && echo (2/3) Installing Python... && echo. && python-3.10.4-amd64 /quiet InstallAllUsers=1 PrependPath=1";
            else
                command += @"&& echo. && echo (2/3) Python is already installed, skipping... && echo.";

            if (!vsBuild)
                command += @"&& echo. && echo (3/3) Installing VS Build Tools... Follow the instructions on screen. && echo. && echo You might need to disable antivirus for the installation process! && echo. && vs_BuildTools.exe --add Microsoft.VisualStudio.Workload.VCTools;includeRecommended;includeOptional --passive --norestart";
            else
                command += @"&& echo. && echo (3/3) VS Build Tools is already installed, skipping... && echo.";

            CMD(command, tmpDir, true);
        }

        /// <summary>
        /// Download and extract HD textures (TODO: add check for invalid .7zip files (ie. canceled download))
        /// </summary>
        public static void DownloadAndInstallHdTextures()
        {
            // get paths and urls
            string oootReleasePath = Path.Combine(GetOootPath(), @"vs\Release");
            string toolPath = Path.Combine(GetBuilderPath(), @"Tools\7zr.exe");
            string texturePackFilename = "oot-reloaded-v10.2.0-uhts-1080p.7z";
            string texturePackPath = Path.Combine(oootReleasePath, texturePackFilename);
            string texturPackUrl = "https://evilgames.eu/texture-packs/files/" + texturePackFilename;
            string htsFilename = "THE LEGEND OF ZELDA_HIRESTEXTURES.hts";
            string htsPath = Path.Combine(oootReleasePath, htsFilename);

            // no release folder, exit
            if (!System.IO.Directory.Exists(oootReleasePath))
            {
                MessageBox.Show($"No OOOT 'Release' folder found! ({oootReleasePath})", "Error!");
                return;
            }

            // no 7zr.exe, exit
            if (!System.IO.File.Exists(toolPath))
            {
                MessageBox.Show($"No '7zr.exe' found from 'Builder/Tools' folder! ({toolPath})", "Error!");
                return;
            }

            // texture pack is already installed, exit
            if (System.IO.File.Exists(htsPath))
            {
                MessageBox.Show("Texture pack is already installed!");
                return;
            }

            // download texture pack
            if (!System.IO.File.Exists(texturePackPath))
                CMD(@"/C echo Downloading texture pack from " + texturPackUrl + " && echo Downloading to: %cd% && echo. && curl -LJO https://evilgames.eu/texture-packs/files/" + texturePackFilename, oootReleasePath);

            // extract texture pack
            if (System.IO.File.Exists(texturePackPath))
            {
                Process p = new Process();
                p.StartInfo.FileName = toolPath;
                p.StartInfo.Arguments = $"e {texturePackPath}";
                p.StartInfo.CreateNoWindow = false;
                p.StartInfo.WorkingDirectory = oootReleasePath;
                p.Start();
                p.WaitForExit();

                if (System.IO.File.Exists(htsPath)) // installed texture pack succesfully
                {
                    // delete .7zip file?
                    if (MessageBox.Show($"Texture pack installed! Do you want to delete '{texturePackFilename}'?", "Completed", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        try
                        {
                            System.IO.File.Delete(texturePackPath);
                            return;
                        }
                        catch
                        {
                            MessageBox.Show($"Error when deleting {texturePackPath}!", "Error!");
                        }
                    }
                }
                else // failed texture pack install
                {
                    MessageBox.Show($"No file found! ({texturePackPath})");
                }
            }
        }

        public static void CMD(string command, string workingDir = "", bool showWindow = true)
        {
            Process p = new Process();
            p.StartInfo.FileName = "CMD.exe";
            p.StartInfo.Arguments = command;
            if (Directory.Exists(workingDir))
                p.StartInfo.WorkingDirectory = workingDir;
            p.StartInfo.CreateNoWindow = !showWindow;
            p.Start();
            p.WaitForExit();
        }

        public static void CMD(string command, bool showWindow)
        {
            CMD(command, "", showWindow);
        }

        public static bool DoesRepositoryExist()
        {
            string oootPath = GetOootPath();

            if (string.IsNullOrEmpty(oootPath) || !Directory.Exists(oootPath)) // does 'ooot' folder exist?
                return false;

            // get output from git summary in 'ooot' folder
            Process p = new Process();
            p.StartInfo.FileName = "CMD.exe";
            p.StartInfo.Arguments = "/C git show --summary .";
            p.StartInfo.WorkingDirectory = oootPath;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            // is 'ooot' folder a valid git repo?
            if (string.IsNullOrEmpty(output) || output.StartsWith("fatal:"))
                return false;

            return true;
        }

        public static bool IsGitInstalled()
        {
            string output = GetCmdOutput(@"/C git version");
            if (string.IsNullOrEmpty(output) || output.StartsWith("fatal:"))
                return false;
            return true;
        }

        public static bool IsPythonInstalled()
        {
            string output = GetCmdOutput(@"/C python --version");
            if (string.IsNullOrEmpty(output) || output.StartsWith("fatal:"))
                return false;
            return true;
        }

        public static bool IsVsBuildToolsInstalled()
        {
            string output = GetCmdOutput(@"/C vstoolsinstalled.bat", GetToolsPath());
            if (string.IsNullOrEmpty(output) || output.StartsWith("fatal:"))
                return false;
            return true;
        }

        public static bool IsAllToolsInstalled()
        {
            return IsGitInstalled() && IsPythonInstalled() && IsVsBuildToolsInstalled();
        }

        private static string GetCmdOutput(string command, string workingDir = "")
        {
            Process p = new Process();
            p.StartInfo.FileName = "CMD.exe";
            p.StartInfo.Arguments = command;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            if (!string.IsNullOrEmpty(workingDir))
                p.StartInfo.WorkingDirectory = workingDir;
            p.Start();
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return output;
        }

        /// <summary>
        /// Is a valid ROM already copied to 'ooot\roms\XXX_XXX' folder?
        /// </summary>
        public static bool IsRomInRomsFolder(bool isEurMqd, bool showErrorMessage = true)
        {
            string romVersion = isEurMqd ? "EUR_MQD" : "PAL_1.0";
            string path = Path.Combine(GetOootPath(), @"roms\" + romVersion);

            if (!Directory.Exists(path))
            {
                if (showErrorMessage)
                    MessageBox.Show($"No roms folder found! ({path})", "Error!");
                return false;
            }

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

                    if (isEurMqd && Builder.Md5HashesEurMqd.Contains(md5Hash) || !isEurMqd && Builder.Md5HashesPal.Contains(md5Hash))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get filename of the first matching rom file in the Builder folder.
        /// </summary>
        public static string GetRomFilename(bool isEurMqd)
        {
            string path = GetBuilderPath();

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

                    if (isEurMqd && Md5HashesEurMqd.Contains(md5Hash) || !isEurMqd && Md5HashesPal.Contains(md5Hash))
                        return Path.GetFileName(file);
                }
            }

            return "";
        }

        public static string GetRomVersion(bool isEurMqd)
        {
            return isEurMqd ? "EUR_MQD" : "PAL_1.0";
        }

        public static string CalculateMD5(string filename)
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

        /// <summary>
        /// Parse all branch names from file 'ooot/git/packed-refs'
        /// </summary>
        public static List<string> GetAllBranches()
        {
            // exit early if no repo found
            if (!DoesRepositoryExist())
                return null;

            List<string> branches = new List<string>();
            string filepath = Path.Combine(GetOootPath(), @".git\packed-refs");
            if (!System.IO.File.Exists(filepath))
            {
                MessageBox.Show("Error: No file found: " + filepath, "Error!");
                return null;
            }

            List<string> data = System.IO.File.ReadAllLines(filepath).ToList();

            if (data == null || data.Count == 0)
            {
                MessageBox.Show("Error: When reading file: " + filepath, "Error!");
                return null;
            }

            foreach (string line in data)
            {
                if (line.Contains("refs/remotes/origin/"))
                {
                    string branchName = "";
                    try
                    {
                        branchName = System.Text.RegularExpressions.Regex.Split(line, "refs/remotes/origin/")[1];
                    }
                    catch
                    {
                        branchName = "";
                    }

                    if (!string.IsNullOrEmpty(branchName))
                        branches.Add(branchName);
                }
            }

            return branches;
        }

        /// <summary>
        /// Parse current branch name from file 'ooot\.git\HEAD'
        /// </summary>
        public static string GetCurrentBranchName()
        {
            string path = Path.Combine(GetOootPath(), @".git\HEAD");
            if (!System.IO.File.Exists(path))
                return "";

            string[] data = System.IO.File.ReadAllLines(path);
            if (data == null || data.Length == 0)
                return "";

            return data[0].Replace("ref: refs/heads/", "");
        }

        public static void LoadSettings()
        {
            string settingsFilepath = GetSettingsSavePath();

            if (!System.IO.File.Exists(settingsFilepath))
                return;

            string[] settings = System.IO.File.ReadAllLines(settingsFilepath);

            if (settings.Length > 0) // set install dir
                SetInstallDir(settings[0], false);

            if (settings.Length > 1) // set temp download dir
                TempDownloadDir = settings[1];

            if (settings.Length > 2 && Form1.form1 != null) // set rom version
                Form1.form1.SetRomVersion(settings[2] == "EUR_MQD");

            if (settings.Length > 3 && Form1.form1 != null) // set extract assets checkbox value
            {
                bool value = true;
                if (bool.TryParse(settings[3], out value))
                    Form1.form1.SetExtractAssetsCheckbox(value);
            }

            if (settings.Length > 4 && Form1.form1 != null) // set theme
            {
                int themeId = 0;
                if (int.TryParse(settings[4], out themeId))
                    Form1.form1.ChangeTheme((Form1.Theme)themeId);
                else
                    Form1.form1.ChangeTheme(Form1.form1.CurrentTheme);
            }
        }

        public static void SaveSettings(string romVersion = "PAL_1.0", string extractAssets = "True")
        {
            try
            {
                if (!string.IsNullOrEmpty(InstallDir))
                {
                    // get theme
                    string theme = "0";
                    if (Form1.form1 != null)
                        theme = Form1.form1.GetThemeID().ToString();

                    string[] saveData = { InstallDir, TempDownloadDir, romVersion, extractAssets, theme };
                    System.IO.File.WriteAllLines(GetSettingsSavePath(), saveData);
                }
            }
            catch
            {
                Debug.WriteLine("Error when saving settings!");
            }
        }
    }
}