using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


[assembly: CLSCompliant(true)]
namespace Igo
{
    public partial class FormIGo : Form
    {


        Dictionary<string, string> dic = new Dictionary<string, string>();
        Dictionary<string, string> cfg = new Dictionary<string, string>();

        FCmdEditor fc = new FCmdEditor();

        int hotKeyId = 0;
        bool deactivate_isVisible = false;

        Color boderColor = Color.LightGray;
        float boderWidth = 0.1f;

        string cmdDesc = "마우스 따위 사용하지 않을 테다.";

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
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

            fc.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            fc.cmd_cid = dic;

            Trace.Assert(LoadSetting());
            dic.Clear();
            Trace.Assert(LoadCmd(ref dic, "igo"));
            Trace.Assert(LoadCmd(ref dic, "user"));
            this.ActiveShow();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private bool LoadSetting()
        {
            string filePath = System.Environment.CurrentDirectory + "\\igo.settings";
            FileInfo fi = new FileInfo(filePath);

            if (!fi.Exists) {
                MessageBox.Show(filePath + "파일이 존재하지 않습니다.");
                return false;
            }

            cfg.Clear();
            string[] lines = System.IO.File.ReadAllLines(filePath);

            for (int i = 0; i < lines.Length; i++) {

                if (String.IsNullOrEmpty(lines[i])) continue;
                if (lines[i][0] == ';' | lines[i][0] == '#') {
                    continue;
                }

                string[] fa = lines[i].Split('=');

                if (fa.Length > 1) {
                    cfg.Add(fa[0].Trim(), fa[1].Trim());
                }
            }

            if (cfg.ContainsKey("hotkey")) set_hotkey(cfg["hotkey"]);
            if (cfg.ContainsKey("theme"))  {
                set_theme(cfg["theme"]);
                if (cfg.ContainsKey("theme_is_watch")) set_theme_watch(cfg["theme_is_watch"], cfg["theme"]);
            }

            if (cfg.ContainsKey("deactivate_is_visible")) set_deactivate_is_visible(cfg["deactivate_is_visible"]);
            if (cfg.ContainsKey("top_most")) set_top_most(cfg["top_most"]);

            return true;
        }

        bool LoadCmd(ref Dictionary<string, string> dic, string cmdName)
        {
            string filePath = System.Environment.CurrentDirectory + "\\" + cmdName + ".cmds";
            FileInfo fi = new FileInfo(filePath);

            if (!fi.Exists) {
                MessageBox.Show(filePath + "파일이 존재하지 않습니다.");
                return false;
            }

            string[] lines = System.IO.File.ReadAllLines(filePath);

            for (int i = 0; i < lines.Length; i++) {

                if (lines[i][0] == ';' | lines[i][0] == '#') {
                    continue;
                }

                string[] fa = lines[i].Split('|');

                if (fa.Length > 1) {
                    string key = fa[0];
                    string val = lines[i].Substring(key.Length + 1);

                    CmdAdd(key, val);
                }
            }

            return true;
        }

        private void CmdAdd(string k, string v)
        {

            if (dic.ContainsKey(k)) {
                CmdAdd(k + "_igo", v);
            } else {
                dic.Add(k, v);
            }
        }

        private bool Register_HotKey()
        {
            // Register Shift + A as global hotkey.
            UnregisterHotKey(this.Handle, hotKeyId);
            return RegisterHotKey(this.Handle, hotKeyId, (int)KeyModifier.Shift, Keys.A.GetHashCode());
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            int WM_HOTKEY = 0x0312;

            if (m.Msg == WM_HOTKEY) {
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
            displayCmd();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control) {
                switch (e.KeyCode) {
                    case Keys.Insert:
                        e.SuppressKeyPress = true;
                        Call_CmdEditor(true);
                        break;
                }
            } else {
                switch (e.KeyCode) {
                    case Keys.Escape:
                        Hide();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.Enter:
                        e.SuppressKeyPress = true;
                        callCmd();
                        break;
                    case Keys.Insert:
                        e.SuppressKeyPress = true;
                        Call_CmdEditor(false);
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
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control) {
                switch (e.KeyCode) {
                    case Keys.Insert:
                        e.SuppressKeyPress = true;
                        Call_CmdEditor(true);
                        break;
                }
            } else {
                switch (e.KeyCode) {
                    case Keys.Escape:
                        e.SuppressKeyPress = true;
                        Hide();
                        break;
                    case Keys.Enter:
                        e.SuppressKeyPress = true;
                        callCmd();
                        break;
                    case Keys.Insert:
                        e.SuppressKeyPress = true;
                        Call_CmdEditor(false);
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




        }

        private Regex makeRegEx()
        {
            StringBuilder sb = new StringBuilder();
            String al = ".*";

            sb.Append("^");

            for (int i = 0; i < textBox1.Text.Length; i++) {
                sb.Append(textBox1.Text[i]);
                sb.Append(al);
            }

            return new Regex(sb.ToString(), RegexOptions.IgnoreCase);
        }

        private bool callCmd()
        {
            string cmd = GetCmd();

            if (String.IsNullOrEmpty(cmd)) {
                lbCmdDesc.Text = "마우스 따위 사용하지 않을 테다.";
                return false;
            }

            if (cmd.Substring(0, 1) == "/") {

                switch (cmd) {
                    case "/Quit":
                    case "/Exit":
                        Close();
                        break;
                    case "/ReLoad":
                        Trace.Assert(LoadSetting());
                        dic.Clear();
                        Trace.Assert(LoadCmd(ref dic, "igo"));
                        Trace.Assert(LoadCmd(ref dic, "user"));
                        break;
                }

            } else {
                Helper.Exec(dic[cmd]);
            }

            textBox1.Text = cmd;
            this.Hide();

            return true;
        }

        void displayCmd()
        {
            string cmd = GetCmd();

            if (String.IsNullOrEmpty(cmd)) {
                lbCmdDesc.Text = this.cmdDesc;
                return;
            }

            if (cmd.Substring(0, 1) == "/") {
                lbCmdDesc.Text = "Cmd of IGO";
            } else {
                lbCmdDesc.Text = dic[cmd].Replace('|', ' ');
            }
        }

        string GetCmd()
        {
            if (listBox1.Items.Count < 1) {
                return "";
            }

            string cmd;
            if (listBox1.SelectedIndex == -1) {
                cmd = listBox1.Items[0].ToString();
            } else {
                cmd = listBox1.SelectedItem.ToString();
            }

            return cmd;
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.ActiveShow();
        }

        private void set_hotkey(string keys)
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

        void set_theme(string theme_file) {

            string filePath = System.Environment.CurrentDirectory + "\\" + theme_file;
            FileInfo fi = new FileInfo(filePath);

            if (!fi.Exists) {
                MessageBox.Show(filePath + "파일이 존재하지 않습니다.");
                return;
            }

            try {
                string[] lines = System.IO.File.ReadAllLines(filePath);


                for (int i = 0; i < lines.Length; i++) {

                    if (String.IsNullOrEmpty(lines[i])) {
                        continue;
                    }

                    if (lines[i][0] == ';' | lines[i][0] == '#') {
                        continue;
                    }

                    string[] fa = lines[i].Split('=');

                    if (fa.Length > 1) {
                        string key = fa[0].Trim();
                        string value = fa[1].Trim();

                        Helper.set_property(this, key, value);
                    }
                }

                return;
            //} catch (Exception e) {
            } catch {
                set_theme(theme_file);
            }
        }

        void set_theme_watch(string strbool, string theme_file) {
            if (strbool == "true") {

                FileInfo f = new FileInfo(System.Environment.CurrentDirectory + "\\" + theme_file);
                
                watcher.Path = f.DirectoryName;
                watcher.Filter = f.Name;
                watcher.EnableRaisingEvents = true;
            } else {
                watcher.EnableRaisingEvents = false;
            }
        }

        void set_deactivate_is_visible(string strbool){
            if (strbool == "true") {
                this.deactivate_isVisible = true;
            }else{
                this.deactivate_isVisible = false;
            }
        }

        void set_top_most(string strbool){
            if (strbool == "true") {
                this.TopMost = true;
            } else {
                this.TopMost = false;
            }
        }

        private void FormIGo_Deactivate(object sender, EventArgs e)
        {
            // 보여지고 있는데, 비활성화 되면, 설정에 따라서.
            if (this.Visible) {
                this.Visible = deactivate_isVisible;
            }
            // ESC키 등으로 이미 숨겨져 있으면 그대로
        }

        private void ActiveShow()
        {
            //if (textBox1.ImeMode == ImeMode.Alpha) {
            //    textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            //} else {
            //    textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(80)))));
            //}

            this.Visible = true;
            this.Activate();
            this.textBox1.Focus();
            //this.textBox1.SelectAll();
        }

        private void textBox1_ImeModeChanged(object sender, EventArgs e)
        {
            //if (textBox1.ImeMode == ImeMode.Alpha) {
            //    textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            //} else {
            //    textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(26)))), ((int)(((byte)(80)))));
            //}
        }

        private void linkLabel1_MouseHover(object sender, EventArgs e)
        {
            //linkLabel1.Text = "Daejin *_*";
        }

        private void linkLabel1_MouseLeave(object sender, EventArgs e)
        {
            //linkLabel1.Text = "Daejin =_=";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayCmd();
        }

        private void textCmd_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.Enter:
                    e.SuppressKeyPress = true;
                    Helper.Exec(lbCmdDesc.Text);
                    this.Hide();
                    break;
                case Keys.Escape:
                    e.SuppressKeyPress = true;
                    Hide();
                    break;
            }
        }

        void Call_CmdEditor(bool bEdit)
        {
            string cmd = "";

            if (bEdit) {
                cmd = GetCmd();
            }

            if (String.IsNullOrEmpty(cmd)) {
                this.fc.old_cmd = "";
            } else {
                fc.old_cmd = cmd;
                fc.old_val = dic[cmd];
            }

            fc.ShowDialog(this);
            if (fc.DialogResult == DialogResult.OK) {
                dic.Clear();
                Trace.Assert(LoadCmd(ref dic, "igo"));
                Trace.Assert(LoadCmd(ref dic, "user"));
            }
            textBox1.Text = fc.cmd;

            this.ActiveShow();
            return;
        }

        private void FormIGo_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(this.boderColor);

            pen.Width = this.boderWidth;
            pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Bevel;

            e.Graphics.DrawRectangle(pen,
                new Rectangle(0, 0, this.Size.Width-1, this.Size.Height-1));

            pen.Dispose();
        }

        private void watcher_Changed(object sender, FileSystemEventArgs e) {
            set_theme(cfg["theme"]);
        }
    }
}
