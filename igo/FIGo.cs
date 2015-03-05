using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace igo
{
    public partial class FormIGo : Form
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        Dictionary<string, string> cfg = new Dictionary<string, string>();

        int hotKeyId = 0;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        enum KeyModifier
        {
            None = 0,
            Alt = 1,
            Control = 2,
            Shift = 4,
            WinKey = 8,
            NOREPEAT = 0x4000
        }

        public FormIGo()
        {
            InitializeComponent();
            SetConfigFileWatcher();

            Debug.WriteLine("FormIGo");

            //if (!Register_HotKey())
            //{
            //    MessageBox.Show("HotKey 등록 오류");
            //    Application.Exit();
            //    return;
            //}

            Trace.Assert(Load_iGos());

        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        void SetConfigFileWatcher()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = System.Environment.CurrentDirectory;
            Debug.WriteLine(watcher.Path);
            watcher.Filter = "*.ini";
            //watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
            //| NotifyFilters.FileName | NotifyFilters.DirectoryName;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += new FileSystemEventHandler(OnChangedWatcher);
            watcher.EnableRaisingEvents = true;
        }

        private static void OnChangedWatcher(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            //Debug.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
        }


        bool Load_iGos()
        {

            string[] igo_files = Directory.GetFiles(System.Environment.CurrentDirectory, "*.igo");
            List<string> igo_list = new List<string>();

            string igo_igo = "";
            Boolean bcmd = false;
            foreach (string igo_file in igo_files) {
                Debug.WriteLine(igo_file);

                if (igo_file.EndsWith("igo.igo")) {
                    igo_igo = igo_file;
                    continue;
                }

                if (igo_file.EndsWith("igo_cmd.igo")) {
                    bcmd = true;
                }

                igo_list.Add(igo_file);
            }

            Trace.Assert(igo_igo.Length > 0, "igo.igo file not found");
            Trace.Assert(bcmd, "igo_cmd.igo file not found");

            Load_igo_igo(igo_igo);

            dic.Clear();
            foreach (string file_path in igo_list) {
                Load_iGo_Cmd(ref dic, file_path);
            }

            return true;
        }

        bool Load_iGo_Cmd(ref Dictionary<string, string> dic, string file_path)
        {
            // TODO: 읽지 못하는 파일 예외 처리

            string[] lines = System.IO.File.ReadAllLines(file_path);

            for (int i = 0; i < lines.Length; i++) {
                string[] fa = lines[i].Split('|');

                if (fa.Length > 1) {
                    try {
                        if (fa.Length == 2) {
                            dic.Add(fa[0], fa[1]);
                        } else if (fa.Length > 2) {
                            dic.Add(fa[0], fa[1] + '|' + fa[2]);
                        }
                    } catch {
                        Debug.WriteLine("키 중복");
                        // TODO: dic.Add(fa[0]) 중복된 키가 들어갔을 때 예외 처리
                    }

                }
            }

            return true;
        }

        private bool Register_HotKey()
        {
            // Register Shift + A as global hotkey.
            UnregisterHotKey(this.Handle, hotKeyId);
            return RegisterHotKey(this.Handle, hotKeyId, (int)KeyModifier.Shift, Keys.A.GetHashCode());
            //return RegisterHotKey(this.Handle, hotKeyId, (int)KeyModifier.Alt, Keys.OemSemicolon.GetHashCode());
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // WM_HOTKEY = 0x0312
            if (m.Msg == 0x0312) {
                this.ActiveShow();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int len = textBox1.Text.Length;

            listBox1.BeginUpdate();
            listBox1.Items.Clear();

            if (textBox1.Text.Length > 0) {
                Regex re = makeRegEx();
                Dictionary<string, string>.KeyCollection keys = dic.Keys;

                foreach (String k in keys) {
                    if (re.Matches(k).Count > 0) {
                        listBox1.Items.Add(k);
                    }
                }
            }

            listBox1.EndUpdate();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.Escape:
                    e.SuppressKeyPress = true;
                    Hide();
                    break;
                case Keys.Enter:
                    e.SuppressKeyPress = true;
                    callCmd();
                    break;
                case Keys.Space:
                case Keys.Down:
                    e.SuppressKeyPress = true;
                    if (listBox1.Items.Count > 0) {
                        if (listBox1.SelectedIndex == -1) {
                            listBox1.SelectedIndex = 0;
                        }
                        listBox1.Focus();
                    }
                    break;
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.Escape:
                    e.SuppressKeyPress = true;
                    Hide();
                    break;
                case Keys.Enter:
                    e.SuppressKeyPress = true;
                    callCmd();
                    break;
                case Keys.Space:

                    int idx = listBox1.SelectedIndex;
                    int cnt = listBox1.Items.Count;

                    if (e.Modifiers == Keys.Shift) {
                        if (idx > -1) {
                            listBox1.SelectedIndex--;
                        }

                        if (listBox1.SelectedIndex == -1) {
                            textBox1.Focus();
                            e.SuppressKeyPress = true;
                        }
                    } else {
                        if (idx < (cnt - 1)) {
                            listBox1.SelectedIndex++;
                        }
                    }
                    break;
                case Keys.Up:
                    if (listBox1.SelectedIndex == 0) {
                        listBox1.SelectedIndex = -1;
                        textBox1.Focus();
                        e.SuppressKeyPress = true;
                    }
                    break;
            }
        }

        private Regex makeRegEx()
        {
            StringBuilder sb = new StringBuilder();
            String al = ".*";

            //sb.Append(al);
            sb.Append("^");

            for (int i = 0; i < textBox1.Text.Length; i++) {
                sb.Append(textBox1.Text[i]);
                sb.Append(al);
            }

            return new Regex(sb.ToString(), RegexOptions.IgnoreCase);
        }

        private bool callCmd()
        {
            if (listBox1.Items.Count < 1) {
                return false;
            }

            if (listBox1.SelectedIndex == -1) {
                listBox1.SelectedIndex = 0;
            }

            string cmd = listBox1.SelectedItem.ToString();


            if (cmd.Substring(0, 1) == "/") {
                switch (cmd) {
                    case "/Quit":
                        Close();
                        break;
                    case "/ReLoad":
                        Trace.Assert(Load_iGos());
                        break;
                }
            } else {
                string[] dic_cmd = dic[cmd].Split('|');
                string app_path = dic_cmd[0];

                Regex rgx = new Regex("%\\S+?%");

                foreach (Match match in rgx.Matches(app_path)) {

                    string ev = match.Value.Substring(1, match.Value.Length - 2);
                    string evv = Environment.GetEnvironmentVariable(ev);

                    Console.WriteLine(ev);
                    Console.WriteLine(evv);

                    app_path = app_path.Replace(match.Value, evv);
                }

                if (app_path.StartsWith("http", StringComparison.CurrentCultureIgnoreCase)) {
                    Process.Start(app_path);
                } else {
                    FileInfo fileInfo = new FileInfo(app_path);

                    ProcessStartInfo startInfo = new ProcessStartInfo(app_path);
                    startInfo.WorkingDirectory = fileInfo.DirectoryName;

                    if (dic_cmd.Length == 2) {
                        startInfo.Arguments = dic_cmd[1];
                    }

                    Process.Start(startInfo);
                }


            }

            textBox1.Text = "";
            this.Hide();

            return true;

        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.ActiveShow();
        }

        private void Load_igo_igo(string file_path)
        {
            cfg.Clear();
            string[] lines = System.IO.File.ReadAllLines(file_path);

            for (int i = 0; i < lines.Length; i++) {
                if (lines[i][0] == ';' | lines[i][0] == '#') {
                    continue;
                }
                string[] fa = lines[i].Split('=');

                if (fa.Length > 1) {
                    Debug.WriteLine(fa[0].Trim());
                    Debug.WriteLine(fa[1].Trim());
                    cfg.Add(fa[0].Trim(), fa[1].Trim());
                }
            }


            Set_Hotkey(cfg["hotkey"]);
        }

        private void Set_Hotkey(string keys)
        {
            string[] keyArray = keys.Split('+');

            KeyModifier keyModifier = 0;
            int keyHash = 0;

            for (int i = 0; i < keyArray.Length; i++) {
                string key = keyArray[i].Trim().ToLower();

                switch (key) {
                    case "alt":
                        keyModifier = keyModifier | KeyModifier.Alt;
                        break;
                    case "ctrl":
                        keyModifier = keyModifier | KeyModifier.Control;
                        break;
                    case "shift":
                        keyModifier = keyModifier | KeyModifier.Shift;
                        break;
                    case "win":
                        keyModifier = keyModifier | KeyModifier.WinKey;
                        break;
                    case ";":
                        keyHash = Keys.OemSemicolon.GetHashCode();
                        break;
                    default:
                        Keys tk;
                        if (Enum.TryParse<Keys>(key, true, out tk)) {
                            keyHash = tk.GetHashCode();
                        }

                        break;
                }
            }

            Debug.WriteLine(keyModifier.ToString());
            Debug.WriteLine(keyHash.ToString());

            UnregisterHotKey(this.Handle, hotKeyId);
            Boolean bReg = RegisterHotKey(this.Handle, hotKeyId, (int)keyModifier, keyHash);

            if (!bReg) {
                MessageBox.Show("RegisterHotKey Error, ReLoad!!!");
            }
        }

        private void FormIGo_Deactivate(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void ActiveShow()
        {
            if (textBox1.ImeMode == ImeMode.Alpha) {
                textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            } else {
                textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(80)))));
            }

            this.Visible = true;
            this.Activate();
            this.textBox1.Focus();
            this.textBox1.SelectAll();
        }

        private void textBox1_ImeModeChanged(object sender, EventArgs e)
        {
            if (textBox1.ImeMode == ImeMode.Alpha) {
                textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            } else {
                textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(26)))), ((int)(((byte)(80)))));
            }
        }

        private void linkLabel1_MouseHover(object sender, EventArgs e)
        {
            linkLabel1.Text = "Daejin *_*";
        }

        private void linkLabel1_MouseLeave(object sender, EventArgs e)
        {
            linkLabel1.Text = "Daejin =_=";
        }
    }
}
