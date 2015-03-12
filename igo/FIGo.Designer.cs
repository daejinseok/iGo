namespace Igo
{
    partial class FormIGo
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            if (disposing) {
                // Unregister hotkey with id 0 before closing the form. You might want to call this more than once with different id values if you are planning to register more than one hotkey.
                UnregisterHotKey(this.Handle, hotKeyId);
            }

            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormIGo));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.lbCmdDesc = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.watcher = new System.IO.FileSystemWatcher();
            this.listBox1 = new Igo.ListBoxNoScr();
            ((System.ComponentModel.ISupportInitialize)(this.watcher)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.AcceptsTab = true;
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.textBox1.Location = new System.Drawing.Point(10, 10);
            this.textBox1.Margin = new System.Windows.Forms.Padding(50);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(785, 30);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.textBox1.ImeModeChanged += new System.EventHandler(this.textBox1_ImeModeChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.linkLabel1.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.ForeColor = System.Drawing.Color.Gold;
            this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.linkLabel1.LinkColor = System.Drawing.Color.Gold;
            this.linkLabel1.Location = new System.Drawing.Point(1, 270);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(798, 25);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "새로운 맥북 갖고 싶당!";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkLabel1.MouseLeave += new System.EventHandler(this.linkLabel1_MouseLeave);
            this.linkLabel1.MouseHover += new System.EventHandler(this.linkLabel1_MouseHover);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "/Quit";
            this.notifyIcon1.BalloonTipTitle = "Quit";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "iGo";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // lbCmdDesc
            // 
            this.lbCmdDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCmdDesc.BackColor = System.Drawing.Color.White;
            this.lbCmdDesc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCmdDesc.ForeColor = System.Drawing.Color.DimGray;
            this.lbCmdDesc.Location = new System.Drawing.Point(10, 50);
            this.lbCmdDesc.Name = "lbCmdDesc";
            this.lbCmdDesc.Size = new System.Drawing.Size(785, 14);
            this.lbCmdDesc.TabIndex = 3;
            this.lbCmdDesc.Text = "마우스 따위 사용하지 않을 테다.";
            this.lbCmdDesc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textCmd_KeyDown);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(5, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(790, 1);
            this.label1.TabIndex = 4;
            // 
            // watcher
            // 
            this.watcher.EnableRaisingEvents = true;
            this.watcher.SynchronizingObject = this;
            this.watcher.Changed += new System.IO.FileSystemEventHandler(this.watcher_Changed);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.BackColor = System.Drawing.Color.White;
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(20, 79);
            this.listBox1.Margin = new System.Windows.Forms.Padding(0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(775, 180);
            this.listBox1.TabIndex = 1;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBox1_KeyDown);
            // 
            // FormIGo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 300);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbCmdDesc);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBox1);
            this.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.Margin = new System.Windows.Forms.Padding(6, 10, 6, 10);
            this.Name = "FormIGo";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "iGo";
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.FormIGo_Deactivate);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormIGo_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.watcher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label lbCmdDesc;
        private System.Windows.Forms.Label label1;
        private ListBoxNoScr listBox1;
        private System.IO.FileSystemWatcher watcher;        
    }
}

