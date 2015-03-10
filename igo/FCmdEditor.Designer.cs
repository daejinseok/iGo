namespace Igo
{
    partial class FCmdEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textPath = new System.Windows.Forms.TextBox();
            this.textArg = new System.Windows.Forms.TextBox();
            this.textCmd = new System.Windows.Forms.TextBox();
            this.lbPath = new System.Windows.Forms.Label();
            this.lbCmd = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbLinePath = new System.Windows.Forms.Label();
            this.lbLineArg = new System.Windows.Forms.Label();
            this.lbArg = new System.Windows.Forms.Label();
            this.lbLineCmd = new System.Windows.Forms.Label();
            this.lbHead = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textPath
            // 
            this.textPath.BackColor = System.Drawing.Color.White;
            this.textPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.textPath.Location = new System.Drawing.Point(50, 62);
            this.textPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textPath.Name = "textPath";
            this.textPath.Size = new System.Drawing.Size(745, 22);
            this.textPath.TabIndex = 0;
            this.textPath.Text = "c:\\windows\\Note";
            this.textPath.TextChanged += new System.EventHandler(this.textPath_TextChanged);
            this.textPath.Enter += new System.EventHandler(this.text_Enter);
            this.textPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.text_KeyDown);
            this.textPath.Leave += new System.EventHandler(this.text_Leave);
            // 
            // textArg
            // 
            this.textArg.BackColor = System.Drawing.Color.White;
            this.textArg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textArg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.textArg.Location = new System.Drawing.Point(80, 117);
            this.textArg.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textArg.Name = "textArg";
            this.textArg.Size = new System.Drawing.Size(715, 22);
            this.textArg.TabIndex = 1;
            this.textArg.Text = "/o daejin igo";
            this.textArg.Enter += new System.EventHandler(this.text_Enter);
            this.textArg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.text_KeyDown);
            this.textArg.Leave += new System.EventHandler(this.text_Leave);
            // 
            // textCmd
            // 
            this.textCmd.BackColor = System.Drawing.Color.White;
            this.textCmd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textCmd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.textCmd.Location = new System.Drawing.Point(67, 173);
            this.textCmd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textCmd.Name = "textCmd";
            this.textCmd.Size = new System.Drawing.Size(519, 22);
            this.textCmd.TabIndex = 2;
            this.textCmd.Text = "iCmd";
            this.textCmd.Enter += new System.EventHandler(this.text_Enter);
            this.textCmd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.text_KeyDown);
            this.textCmd.Leave += new System.EventHandler(this.text_Leave);
            // 
            // lbPath
            // 
            this.lbPath.AutoSize = true;
            this.lbPath.Font = new System.Drawing.Font("Malgun Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbPath.ForeColor = System.Drawing.Color.Gray;
            this.lbPath.Location = new System.Drawing.Point(49, 93);
            this.lbPath.Name = "lbPath";
            this.lbPath.Size = new System.Drawing.Size(31, 13);
            this.lbPath.TabIndex = 4;
            this.lbPath.Text = "Path";
            // 
            // lbCmd
            // 
            this.lbCmd.AutoSize = true;
            this.lbCmd.BackColor = System.Drawing.Color.Transparent;
            this.lbCmd.Font = new System.Drawing.Font("Malgun Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbCmd.ForeColor = System.Drawing.Color.Gray;
            this.lbCmd.Location = new System.Drawing.Point(65, 204);
            this.lbCmd.Name = "lbCmd";
            this.lbCmd.Size = new System.Drawing.Size(31, 13);
            this.lbCmd.TabIndex = 6;
            this.lbCmd.Text = "Cmd";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.label1.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(0, 268);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(800, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Enter : 수정 혹은 추가     Ctrl+Enter : 가장 앞에 추가     ESC : 화면 종료";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbLinePath
            // 
            this.lbLinePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lbLinePath.Location = new System.Drawing.Point(50, 89);
            this.lbLinePath.Name = "lbLinePath";
            this.lbLinePath.Size = new System.Drawing.Size(745, 2);
            this.lbLinePath.TabIndex = 9;
            this.lbLinePath.Text = "label2";
            // 
            // lbLineArg
            // 
            this.lbLineArg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lbLineArg.Location = new System.Drawing.Point(79, 144);
            this.lbLineArg.Name = "lbLineArg";
            this.lbLineArg.Size = new System.Drawing.Size(715, 2);
            this.lbLineArg.TabIndex = 11;
            this.lbLineArg.Text = "label3";
            // 
            // lbArg
            // 
            this.lbArg.AutoSize = true;
            this.lbArg.Font = new System.Drawing.Font("Malgun Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbArg.ForeColor = System.Drawing.Color.Gray;
            this.lbArg.Location = new System.Drawing.Point(79, 147);
            this.lbArg.Name = "lbArg";
            this.lbArg.Size = new System.Drawing.Size(60, 13);
            this.lbArg.TabIndex = 10;
            this.lbArg.Text = "Argument";
            // 
            // lbLineCmd
            // 
            this.lbLineCmd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lbLineCmd.Location = new System.Drawing.Point(64, 200);
            this.lbLineCmd.Name = "lbLineCmd";
            this.lbLineCmd.Size = new System.Drawing.Size(715, 2);
            this.lbLineCmd.TabIndex = 12;
            this.lbLineCmd.Text = "label5";
            // 
            // lbHead
            // 
            this.lbHead.AutoSize = true;
            this.lbHead.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHead.ForeColor = System.Drawing.Color.Black;
            this.lbHead.Location = new System.Drawing.Point(8, 5);
            this.lbHead.Name = "lbHead";
            this.lbHead.Size = new System.Drawing.Size(105, 23);
            this.lbHead.TabIndex = 14;
            this.lbHead.Text = "Add Cmd";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Gainsboro;
            this.label2.Location = new System.Drawing.Point(5, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(790, 1);
            this.label2.TabIndex = 15;
            // 
            // FCmdEditor
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 300);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbHead);
            this.Controls.Add(this.lbLineCmd);
            this.Controls.Add(this.lbLineArg);
            this.Controls.Add(this.lbArg);
            this.Controls.Add(this.lbLinePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbCmd);
            this.Controls.Add(this.lbPath);
            this.Controls.Add(this.textCmd);
            this.Controls.Add(this.textArg);
            this.Controls.Add(this.textPath);
            this.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FCmdEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cmd Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.TextBox textArg;
        private System.Windows.Forms.TextBox textCmd;
        private System.Windows.Forms.Label lbPath;
        private System.Windows.Forms.Label lbCmd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbLinePath;
        private System.Windows.Forms.Label lbLineArg;
        private System.Windows.Forms.Label lbArg;
        private System.Windows.Forms.Label lbLineCmd;
        private System.Windows.Forms.Label lbHead;
        private System.Windows.Forms.Label label2;
    }
}