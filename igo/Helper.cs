using System;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Igo
{
    class Helper
    {
        public static void Exec(string cmd)
        {
            try {
                string[] cmdArr = cmd.Split('|');
                string appPath = cmdArr[0];

                if (appPath.StartsWith("\"") && appPath.EndsWith("\"")) {
                    appPath = appPath.Substring(1, appPath.Length - 2);
                }

                appPath = replaceEnv(appPath);

                if (appPath.StartsWith("http", StringComparison.CurrentCultureIgnoreCase)) {
                    Process.Start(appPath);
                    return;
                }

                Debug.WriteLine(appPath);

                FileInfo fileInfo = new FileInfo(appPath);

                ProcessStartInfo startInfo = new ProcessStartInfo(appPath);
                startInfo.WorkingDirectory = fileInfo.DirectoryName;

                if (cmdArr.Length > 1) {
                    startInfo.Arguments = replaceEnv(cmdArr[1]);
                }
                Process.Start(startInfo);

            }
            catch (System.Exception e) {
                MessageBox.Show(e.ToString());
            }
        }

        private static string replaceEnv(string fileNameEnv)
        {
            var fileName = fileNameEnv;

            var rgx = new Regex("%\\S+?%");
            foreach (Match match in rgx.Matches(fileName)) {

                string ev = match.Value.Substring(1, match.Value.Length - 2);
                string evv = Environment.GetEnvironmentVariable(ev);

                Debug.WriteLine(ev);
                Debug.WriteLine(evv);

                fileName = fileName.Replace(match.Value, evv);
            }

            fileName = fileName.Replace("${cur_dir}", System.Environment.CurrentDirectory);

            return fileName;
        }

        public static bool FileNotExist(string path)
        {
            FileInfo fi = new FileInfo(path);

            if (!fi.Exists) {
                MessageBox.Show(path + "파일이 존재하지 않습니다.");
                return true;
            }

            return false;
        }
    }
}