using System;
using System.Windows.Forms;
//using System.Windows.Input;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;

public class Drawgra:Form 
{
  private string str = "daejin_";

  public Drawgra() 
  {
    this.Text = "Illustrating DrawXXX() methods";
    this.Size = new Size(450,400);
    this.Paint += new PaintEventHandler(Draw_Graphics);
    this.KeyDown += new KeyEventHandler(Form1_KeyDown);
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
    e.Graphics.DrawString(str, font, solidBrush, new PointF(10, h+al*3));        

  }

  private void Form1_KeyDown(object sender, KeyEventArgs e)
  {

    str += e.KeyData.ToString();
    this.Invalidate();


  }

  public static void Main() 
  {
    Application.Run(new Drawgra());
  }
// End of class
}