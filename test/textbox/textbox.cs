using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;

public class Form1 : Form
{
    private TextBox textBox1;
    private ListBox listbox;

    public Form1()
    {
        InitializeComponent();

        //this.Text = "Illustrating DrawXXX() methods";
        this.Size = new Size(1000,1000);
        //AllowTransparency = 
        Opacity = 0.8;
        //this.Paint += new PaintEventHandler(Draw_Graphics);        
    }

    public void Draw_Graphics(object sender,PaintEventArgs e) 
    {

    //FontFamily fontFamily = new FontFamily("Times New Roman");
    FontFamily fontFamily = new FontFamily("맑은 고딕");
    Font font = new Font(
       fontFamily,
       32,
       FontStyle.Regular,
       GraphicsUnit.Pixel);
    SolidBrush solidBrush = new SolidBrush(Color.FromArgb(255, 0, 0, 255));
    string string1 = "SingleBitPerPixel";
    string string2 = "AntiAlias";

    int h = 10;
    int al = 50;

    e.Graphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
    e.Graphics.DrawString(string1, font, solidBrush, new PointF(10, h));

    e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
    e.Graphics.DrawString(string2, font, solidBrush, new PointF(10, h+al));

    e.Graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
    e.Graphics.DrawString(string2, font, solidBrush, new PointF(10, h+al*2));

    e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
    e.Graphics.DrawString(string2, font, solidBrush, new PointF(10, h+al*3));        

    }    

    private void InitializeComponent()
    {
        this.textBox1 = new System.Windows.Forms.TextBox();
        //this.textBox1.Size = new Size(500,300);
        this.textBox1.BorderStyle = BorderStyle.None;
        this.textBox1.BorderStyle = BorderStyle.FixedSingle;
        this.textBox1.Font = new Font("맑은 고딕", 5);
        this.SuspendLayout();
        // 
        // textBox1
        // 
        this.textBox1.AcceptsReturn = true;
        this.textBox1.AcceptsTab = true;
        //this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.textBox1.Multiline = true;
        this.Paint += new PaintEventHandler(Draw_Graphics);
        //this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        // 
        // Form1
        // 
        //this.ClientSize = new System.Drawing.Size(284, 1000);
        //this.Controls.Add(this.textBox1);
        this.Text = "TextBox Example";
        this.ResumeLayout(false);
        this.PerformLayout();
        this.Font = new Font("맑은 고딕", 20);

        this.listbox    = new System.Windows.Forms.ListBox();
        this.listbox.Size = new Size(500,500);
        //this.listbox.Opacity = 1;
        //this.ClientSize = new System.Drawing.Size(500, 1000);
        //this.listbox.BorderStyle = BorderStyle.FixedSingle;
        this.listbox.BorderStyle = BorderStyle.None;
        //listview.GridLines = true;

        //listview.View = View.Details;        
        this.Controls.Add(this.listbox);
        this.listbox.Items.Add("daejin");
        this.listbox.Items.Add("daejin");
        this.listbox.Items.Add("daejin");
        this.listbox.Items.Add("daejin");
        this.listbox.Items.Add("daejin");

        

    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new Form1());
    }
}

