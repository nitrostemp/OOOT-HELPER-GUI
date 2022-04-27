using System.Linq;

namespace OOOT_GUI
{
    public class Helpers
    {
        public static string ParseGitStatusString(string cmdOutput)
        {
            string errorString = "No OOOT repository found.";

            string[] lines = cmdOutput.Split('\n');
            if (lines == null || lines.Length == 0)
                return errorString;

            string commit = "";
            string date = "";

            commit = lines.Where(x => x.StartsWith("commit ")).FirstOrDefault();
            if (!string.IsNullOrEmpty(commit))
                commit = commit.Replace("commit ", "");

            date = lines.Where(x => x.StartsWith("Date:")).FirstOrDefault();

            if (string.IsNullOrEmpty(commit) || string.IsNullOrEmpty(date))
                return errorString;

            string result = "Commit: " + commit + "\n" + date;

            return result;
        }
    }
}
