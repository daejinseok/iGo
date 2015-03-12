using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Permissions;

namespace Igo
{
    public partial class FProcessList : Form
    {
        [PermissionSetAttribute(SecurityAction.LinkDemand, Name = "FullTrust")]
        [PermissionSetAttribute(SecurityAction.InheritanceDemand, Name = "FullTrust")]
        public FProcessList() {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;

            Process[] allProc = Process.GetProcesses();
            foreach (Process p in allProc)
            {
                listBox1.Items.Add(p.StartInfo.FileName);
            }
        }
    }
}
