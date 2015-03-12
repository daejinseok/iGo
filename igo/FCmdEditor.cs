using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Igo
{
    public partial class FCmdEditor : Form
    {
        public string cmd {
            get {
                return textCmd.Text;
            }
        }

        Dictionary<string, string> cmdDic;
        string old_cmd;

        public FCmdEditor(Dictionary<string, string> pCmdDic, string cmd = "", string val = "") {
            InitializeComponent();
            cmdDic = pCmdDic;
            this.DialogResult = DialogResult.Cancel;

            if (!String.IsNullOrEmpty(cmd)) {

                this.old_cmd = cmd;
                this.lbHead.Text = "Edit \'" + cmd + "\' Cmd";
                textCmd.Text = cmd;

                string[] arr = val.Split('|');
                textPath.Text = arr[0];

                if (arr.Length > 1) {
                    textArg.Text = arr[1];
                } else {
                    textArg.Text = "";
                }
            } else {
                this.old_cmd = "";
                this.lbHead.Text = "Add New Cmd";
                textPath.Text = "";
                textArg.Text = "";
                textCmd.Text = "";
            }
            textPath.SelectAll();
            textPath.Focus();

        }

        protected override CreateParams CreateParams {
            get {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        private void textPath_TextChanged(object sender, EventArgs e) {

            var rgx = new Regex(@"(?<=\\)[a-zA-Z0-9 \-가-힣]+(?=\.[a-zA-Z1-9]+$)");

            MatchCollection mac = rgx.Matches(textPath.Text);
            Debug.WriteLine(mac.Count.ToString());
            if (mac.Count > 0) {
                Debug.WriteLine(mac[mac.Count - 1].Value);
                textCmd.Text = mac[mac.Count - 1].Value;
            }
        }

        private void text_Enter(object sender, EventArgs e) {
            System.Drawing.Color c = System.Drawing.Color.LightPink;

            if (sender.Equals(textPath)) {
                lbPath.ForeColor = c;
                lbLinePath.BackColor = c;
                lbPath.Text = "Path ( ex: " + System.Environment.CurrentDirectory + "\\iGo.exe )";
            } else if (sender.Equals(textArg)) {
                lbArg.ForeColor = c;
                lbLineArg.BackColor = c;
                lbArg.Text = "Argument ( ex: /O WelCome to iGo )";
            } else if (sender.Equals(textCmd)) {
                lbCmd.ForeColor = c;
                lbLineCmd.BackColor = c;
                lbCmd.Text = "Cmd ( ex: iGo )";
            }
        }

        private void text_Leave(object sender, EventArgs e) {
            System.Drawing.Color c = System.Drawing.Color.Gray;

            if (sender.Equals(textPath)) {
                lbPath.ForeColor = c;
                lbLinePath.BackColor = c;
                lbPath.Text = "Path";
            } else if (sender.Equals(textArg)) {
                lbArg.ForeColor = c;
                lbLineArg.BackColor = c;
                lbArg.Text = "Argument";
            } else if (sender.Equals(textCmd)) {
                lbCmd.ForeColor = c;
                lbLineCmd.BackColor = c;
                lbCmd.Text = "Cmd";
            }
        }

        private void text_KeyDown(object sender, KeyEventArgs e) {
            if (e.Modifiers == Keys.Control) {
                switch (e.KeyCode) {
                    case Keys.Enter:
                        CmdApply(true);
                        break;
                    case Keys.P:

                        FProcessList fp = new FProcessList();
                        fp.ShowDialog(this);
                        //if (fp.DialogResult == DialogResult.OK) {
                        //}
                        fp.Dispose();
                        e.SuppressKeyPress = true;
                        break;
                }
            } else {
                switch (e.KeyCode) {
                    case Keys.Escape:
                        this.Close();
                        break;
                    case Keys.Enter:
                        CmdApply();
                        break;
                }
            }
        }

        void CmdApply(bool bTop = false) {
            if (!CmdCheck()) {
                return;
            }

            if (String.IsNullOrEmpty(this.old_cmd)) {
                CmdAdd(bTop);
            } else {
                CmdUpdate();
            }

        }

        bool CmdCheck() {
            textPath.Text = textPath.Text.Trim();
            textArg.Text = textArg.Text.Trim();
            textCmd.Text = textCmd.Text.Trim();

            string path = textPath.Text;
            string arg = textArg.Text;
            string cmd = textCmd.Text;

            if (String.IsNullOrEmpty(path)) {
                MessageBox.Show("Path는 필수 값임!");
                textPath.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(cmd)) {
                MessageBox.Show("Cmd는 필수 값임!");
                textCmd.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(this.old_cmd)) {
                if (cmdDic.ContainsKey(cmd)) {
                    MessageBox.Show("[ " + cmd + " ] 라는 명령은 이미 아래의 값으로 등록되어 있습니다.\n\n" + cmdDic[cmd]);
                    return false;
                }
            }

            return true;
        }

        void CmdAdd(bool bTop = false) {
            string path = textPath.Text;
            string arg = textArg.Text;
            string cmd = textCmd.Text;

            string now = "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string userCmds = System.Environment.CurrentDirectory + "\\user.cmds";
            string backCmds = System.Environment.CurrentDirectory + "\\user.cmds" + now;
            string tempCmds = System.Environment.CurrentDirectory + "\\temp.cmds" + now;

            if (Helper.FileNotExist(userCmds)) return;

            List<string> lines = new List<string>(System.IO.File.ReadAllLines(userCmds));

            string addCmd = cmd + "|" + path + "|" + arg;
            if (bTop) {
                lines.Insert(0, addCmd);
            } else {
                lines.Add(addCmd);
            }

            System.IO.File.WriteAllLines(tempCmds, lines);

            File.Move(userCmds, backCmds);
            File.Move(tempCmds, userCmds);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        void CmdUpdate() {
            string path = textPath.Text;
            string arg = textArg.Text;
            string cmd = textCmd.Text;

            string now = "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string userCmds = System.Environment.CurrentDirectory + "\\user.cmds";
            string backCmds = System.Environment.CurrentDirectory + "\\user.cmds" + now;
            string tempCmds = System.Environment.CurrentDirectory + "\\temp.cmds" + now;

            if (Helper.FileNotExist(userCmds)) return;

            List<string> lines = new List<string>(System.IO.File.ReadAllLines(userCmds));

            string addCmd = cmd + "|" + path + "|" + arg;

            int i;
            for (i = 0; i < lines.Count; i++) {

                string filecmd = lines[i].Split('|')[0];

                if (filecmd == this.old_cmd) {
                    lines[i] = addCmd;
                    break;
                }
            }

            if (lines[i] != addCmd) {
                MessageBox.Show(userCmds + "파일에 " + this.old_cmd + "가 없어 수정하지 못 했습니다. 대신 맨 끝에 추가합니다.");
                lines.Add(addCmd);
            }

            System.IO.File.WriteAllLines(tempCmds, lines);

            File.Move(userCmds, backCmds);
            File.Move(tempCmds, userCmds);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FCmdEditor_Paint(object sender, PaintEventArgs e) {
            // Create a new pen.
            //Pen skyBluePen = new Pen(Brushes.DeepSkyBlue);
            Pen pen = new Pen(Brushes.LightGray);

            // Set the pen's width.
            pen.Width = 0.1F;

            // Set the LineJoin property.
            pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Bevel;

            // Draw a rectangle.
            e.Graphics.DrawRectangle(pen,
                new Rectangle(0, 0, this.Size.Width - 1, this.Size.Height - 1));

            //Dispose of the pen.
            pen.Dispose();
        }
    }
}
