using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;


namespace igo
{
    class Helper
    {
        public static void Exec(string cmd)
        {
            string[] cmdArr = cmd.Split('|');
            string appPath = cmdArr[0];

            if (appPath.StartsWith("\"") && appPath.EndsWith("\"")) {
                appPath = appPath.Substring(1, appPath.Length - 2);
            }

            //if (appPath.StartsWith("http", StringComparison.CurrentCultureIgnoreCase)) {
            //    //return ( (Process.Start(appPath)).ExitCode == 0);
            //    Process.Start(appPath);
            //    return;
            //}

            appPath = replaceEnv(appPath);
            FileInfo fileInfo = new FileInfo(appPath);

            if (fileInfo.Exists) {
                ProcessStartInfo startInfo = new ProcessStartInfo(appPath);
                startInfo.WorkingDirectory = fileInfo.DirectoryName;

                if (cmdArr.Length == 2) {
                    startInfo.Arguments = replaceEnv(cmdArr[1]);
                }
                //return ((Process.Start(startInfo)).ExitCode == 0);
                try {
                    Process.Start(startInfo);
                } catch (System.Exception e) {
                    MessageBox.Show(e.ToString());
                }
            } else {
                try {
                    Process.Start(appPath);
                //} catch (System.ComponentModel.Win32Exception e) {
                //    MessageBox.Show(e.ToString());
                //} catch (System.ObjectDisposedException e) {

                //} catch (System.IO.FileNotFoundException e) {
                } catch (System.Exception e){
                    MessageBox.Show(e.ToString());
                }
                
            }


            return;
            
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
    }
}