using System;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

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

                if (appPath.StartsWith("\\\\")) {
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


        public static Color strToColor( string strColor ){
            //this.textBox1.BackColor = System.Drawing.Color.White;

            string[] rgb = strColor.Split(',');
            if (rgb.Length == 3) {
                int red = Convert.ToInt32(rgb[0].Trim());
                int green = Convert.ToInt32(rgb[1].Trim());
                int blue = Convert.ToInt32(rgb[2].Trim());

                return Color.FromArgb(red, green, blue);
            } else {

                // 일단 정상동작 안함. 그냥 숫자 3개 쓰자...ㅠㅠ
                Color c;

                try {
                    if (Enum.TryParse(strColor, true, out c)) return c;
                } finally {
                    c = Color.Red;
                }

                return c;
            }
        }

        public static bool strToBool(string s) {
            return (s.ToLower() == "true");
        }

        public static void set_property(Form f, string ctrl_property, string value)
        {
            string[] k = ctrl_property.Split('.');
            if (k.Length < 2) return;

            string ctrl     = k[0];
            string property = k[1];

            Control c = get_control(f, ctrl);
            if (c == null) return;

            switch (property) {
                case "BackColor":
                    c.BackColor = Helper.strToColor(value);
                    break;
                case "ForeColor":
                    c.ForeColor = Helper.strToColor(value);
                    break;
            }

        }

        public static Control get_control(Form f, string ctrl_name)
        {
            if ((ctrl_name == "FormiGo") || (ctrl_name == "FCmdEditor")) {
                return f;
            }

            Control c = f.GetNextControl(f, true);

            while (c != null) {
                if (c.Name == ctrl_name) return c;
                c = f.GetNextControl(c, true);
            }

            return null;
        }
    }
}