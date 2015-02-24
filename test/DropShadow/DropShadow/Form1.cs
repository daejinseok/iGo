using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        DropShadow ds = new DropShadow();
        public Form1()
        {
            InitializeComponent();
            this.Shown += new EventHandler(Form1_Shown);
            this.Resize += new EventHandler(Form1_Resize);
            this.LocationChanged += new EventHandler(Form1_Resize);
        }
        void Form1_Shown(object sender, EventArgs e)
        {
            Rectangle rc = this.Bounds;
            rc.Inflate(1, 1);
            ds.Bounds = rc;
            ds.Show();
            this.BringToFront();
        }
        void Form1_Resize(object sender, EventArgs e)
        {
            ds.Visible = (this.WindowState == FormWindowState.Normal);
            if (ds.Visible)
            {
                Rectangle rc = this.Bounds;
                rc.Inflate(1, 1);
                ds.Bounds = rc;
            }
            this.BringToFront();
        }
    }

    public class DropShadow : Form
    {
        public DropShadow()
        {
            this.Opacity = 0.5;
            this.BackColor = Color.Gray;
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
        }
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int WS_EX_NOACTIVATE = 0x8000000;
        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | WS_EX_TRANSPARENT | WS_EX_NOACTIVATE;
                return cp;
            }
        }
    }
}
