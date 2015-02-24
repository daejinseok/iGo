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

        int hotKeyId = 0;
        int textBoxOldTextLen = 0;

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

            if (!Register_HotKey())
            {
                MessageBox.Show("HotKey 등록 오류");
                Application.Exit();
                return;
            }

            Trace.Assert(Load_iGo());

        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        void SetConfigFileWatcher()
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path   = System.Environment.CurrentDirectory;
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


        bool Load_iGo()
        {

            string[] igo_files = Directory.GetFiles(System.Environment.CurrentDirectory, "*.igo");
            List<string> igo_list = new List<string>();

            Boolean bigo = false;
            Boolean bcmd = false;
            foreach (string igo_file in igo_files)
            {
                Debug.WriteLine(igo_file);

                if (igo_file.EndsWith("igo.igo"))
                {
                    bigo = true;
                    continue;
                }

                if (igo_file.EndsWith("igo_cmd.igo"))
                {
                    bcmd = true;
                    igo_list.Add(igo_file);
                    continue;
                }

                igo_list.Add(igo_file);
            }

            Trace.Assert(bigo, "igo.igo file not found");
            Trace.Assert(bcmd, "igo_cmd.igo file not found");

            dic.Clear();
            //dic.Add("/Quit", "");
            //dic.Add("/ReLoad", "");

            foreach(string file_path in igo_list) {
                Load_iGo_Cmd(ref dic, file_path);
            }


            return true;
        }

        bool Load_iGo_Cmd(ref Dictionary<string, string> dic, string file_path)
        {
            // TODO: 읽지 못하는 파일 예외 처리

            string[] lines = System.IO.File.ReadAllLines(file_path);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] fa = lines[i].Split('|');

                if (fa.Length > 1)
                {
                    try
                    {
                        if (fa.Length == 2)
                        {
                            dic.Add(fa[0], fa[1]);
                        }
                        else if (fa.Length > 2)
                        {
                            dic.Add(fa[0], fa[1] + '|' + fa[2]);
                        }
                    }
                    catch
                    {
                        // TODO: dic.Add(fa[0]) 중복된 키가 들어갔을 때 예외 처리
                    }

                }
            }

            return true;
        }

        private bool Register_HotKey()
        {
            // Register Shift + A as global hotkey. 
            return RegisterHotKey(this.Handle, hotKeyId, (int)KeyModifier.Shift, Keys.A.GetHashCode());
            //return RegisterHotKey(this.Handle, hotKeyId, (int)KeyModifier.Alt, Keys.OemSemicolon.GetHashCode());
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // WM_HOTKEY = 0x0312
            if (m.Msg == 0x0312)
            {
                /* Note that the three lines below are not needed if you only want to register one hotkey.
                 * The below lines are useful in case you want to register multiple keys, which you can use a switch with the id as argument, or if you want to know which key/modifier was pressed for some particular reason. */

                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);                  // The key of the hotkey that was pressed.
                KeyModifier modifier = (KeyModifier)((int)m.LParam & 0xFFFF);       // The modifier of the hotkey that was pressed.
                int id = m.WParam.ToInt32();                                        // The id of the hotkey that was pressed.


                //MessageBox.Show("Hotkey has been pressed!");
                // do something
                this.Visible = !this.Visible;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int len = textBox1.Text.Length;
            int oldLen = textBoxOldTextLen;


            if (len == 0)
            {
                textBoxOldTextLen = 0;
                listBox1.BeginUpdate();				
                listBox1.Items.Clear();
                listBox1.EndUpdate();
            }
            else if (len != 1 && oldLen < len)
            {
                Regex re = makeRegEx();
                listBox1.BeginUpdate();
                for (int ri = listBox1.Items.Count - 1; ri >= 0; ri--)
                {
                    string item = listBox1.Items[ri].ToString();
                    if (re.Matches(item).Count == 0)
                    {
                        listBox1.Items.RemoveAt(ri);
                    }
                }
                listBox1.EndUpdate();
            }
            else
            {
                listBox1.BeginUpdate();
                listBox1.Items.Clear();

                Regex re = makeRegEx();
                Dictionary<string, string>.KeyCollection keys = dic.Keys;

                foreach (String k in keys)
                {
                    if (re.Matches(k).Count > 0)
                    {
                        listBox1.Items.Add(k);
                    }
                }
                listBox1.EndUpdate();
            }

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                Hide();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                callCmd();
            }
            else if ((e.KeyCode == Keys.Down) || (e.Modifiers == Keys.Alt && e.KeyCode == Keys.J))
            {
                if (listBox1.SelectedIndex == -1)
                {
                    listBox1.SelectedIndex = 0;
                }
                listBox1.Focus();
            }

        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                callCmd();
            }
            else if (e.Modifiers == Keys.Alt && e.KeyCode == Keys.J)
            {
                if (listBox1.SelectedIndex < listBox1.Items.Count - 1)
                {
                    listBox1.SelectedIndex++;
                }
            }
            else if (e.Modifiers == Keys.Alt && e.KeyCode == Keys.K)
            {
                if (listBox1.SelectedIndex > 0)
                {
                    listBox1.SelectedIndex--;
                }
                else
                {
                    textBox1.Focus();
                }

            }

        }

        private Regex makeRegEx()
        {
            StringBuilder sb = new StringBuilder();
            String al = ".*";

            sb.Append(al);

            for (int i = 0; i < textBox1.Text.Length; i++)
            {
                sb.Append(textBox1.Text[i]);
                sb.Append(al);
            }

            return new Regex(sb.ToString(), RegexOptions.IgnoreCase);
        }

        private bool callCmd()
        {

            if (listBox1.SelectedIndex == -1)
            {
                listBox1.SelectedIndex = 0;
            }

            string cmd = listBox1.SelectedItem.ToString();


            if (cmd.Substring(0, 1) == "/")
            {
                switch (cmd)
                {
                    case "/Quit":
                        Close();
                        break;
                    case "/ReLoad":
                        reload();
                        break;
                }
            }
            else
            {
                //MessageBox.Show(dic[cmd]);
                string[] dic_cmd = dic[cmd].Split('|');

                if (dic_cmd.Length == 1)
                {
                    System.Diagnostics.Process.Start(dic[cmd]);
                }
                else if( dic_cmd.Length == 2 )
                { 
                    System.Diagnostics.Process.Start(dic_cmd[0], dic_cmd[1]);
                }
            }

            textBox1.Text = "";
            this.Hide();

            return true;

        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Visible = !this.Visible;
        }

        Boolean reload()
        {

            return true;
        }
    }
}
