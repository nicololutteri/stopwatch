using System;
using System.Drawing;
using System.Windows.Forms;

public class Dot : UserControl
{
    public Dot()
    {
        Paint += Dot_Paint;
    }

    void Dot_Paint(object sender, PaintEventArgs e)
    {
        e.Graphics.FillRectangle(new SolidBrush(ForeColor), new Rectangle(0, Height / 5 * 4, Width, Height / 5));
    }
}