using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;


[assembly: CLSCompliant(true)]
namespace Igo
{
    public partial class FormIGo : Form
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        Dictionary<string, string> cfg = new Dictionary<string, string>();

        int hotKeyId = 0;

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
            Trace.Assert(LoadSetting());
            dic.Clear();
            Trace.Assert(LoadCmd(ref dic, "igo"));
            Trace.Assert(LoadCmd(ref dic, "user"));

            //progressBar1.
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
                        e.SuppressKeyPress = true;
                        Hide();
                        break;
                    case Keys.Enter:
                        e.SuppressKeyPress = true;
                        callCmd();
                        break;
                    case Keys.Up:
                        e.SuppressKeyPress = true;
                        textCmd.SelectAll();
                        textCmd.Focus();
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
            string cmd = GetCmd();

            if (String.IsNullOrEmpty(cmd)) {
                textCmd.Text = "Cmd is Empty";
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
                textCmd.Text = "Cmd is Empty";
                return;
            }

            if (cmd.Substring(0, 1) == "/") {
                textCmd.Text = "IGO Cmd";
            } else {
                textCmd.Text = dic[cmd];
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
            //if (textBox1.ImeMode == ImeMode.Alpha) {
            //    textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            //} else {
            //    textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(80)))));
            //}

            this.Visible = true;
            this.Activate();
            this.textBox1.Focus();
            this.textBox1.SelectAll();
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
                    Helper.Exec(textCmd.Text);
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

            FCmdEditor fc;
            if (String.IsNullOrEmpty(cmd)) {
                fc = new FCmdEditor(dic);
            } else {
                fc = new FCmdEditor(dic, cmd, dic[cmd]);
            }

            fc.ShowDialog(this);
            if (fc.DialogResult == DialogResult.OK) {
                dic.Clear();
                Trace.Assert(LoadCmd(ref dic, "igo"));
                Trace.Assert(LoadCmd(ref dic, "user"));
            }
            textBox1.Text = fc.cmd;

            fc.Dispose();

            this.ActiveShow();
            return;
        }

        private void FormIGo_Paint(object sender, PaintEventArgs e)
        {
            // Create a new pen.
            //Pen skyBluePen = new Pen(Brushes.DeepSkyBlue);
            Pen skyBluePen = new Pen(Brushes.LightGray);

            // Set the pen's width.
            skyBluePen.Width = 0.1F;

            // Set the LineJoin property.
            skyBluePen.LineJoin = System.Drawing.Drawing2D.LineJoin.Bevel;

            // Draw a rectangle.
            e.Graphics.DrawRectangle(skyBluePen,
                new Rectangle(0, 0, this.Size.Width-1, this.Size.Height-1));

            // Draw a rectangle.
            //e.Graphics.DrawRectangle(skyBluePen,
            //    new Rectangle(0, 0, 800, 1));

            //Dispose of the pen.
            skyBluePen.Dispose();
        }

    }
}
