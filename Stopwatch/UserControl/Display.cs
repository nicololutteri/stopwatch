using System;
using System.Drawing;
using System.Windows.Forms;

class Display : UserControl
{
    public event EventHandler error;
    public event EventHandler end;

    private int number;
    public int Number
    {
        get { return number; }
        set
        {
            number = value;
            Invalidate();
        }
    }

    private int thickness;
    public int Thickness
    {
        get { return thickness; }
        set
        {
            thickness = value;
            Invalidate();
        }
    }

    private bool incrementare;
    public bool Incrementare
    {
        get { return incrementare; }
        set
        {
            incrementare = value;
            Invalidate();
        }
    }

    readonly Timer t;

    public Display()
    {
        t = new Timer
        {
            Interval = 1000
        };
        t.Tick += t_Tick;

        Paint += Display_Paint;

        Number = 0;
        Thickness = 20;

        DoubleBuffered = true;
    }

    void Display_Paint(object sender, PaintEventArgs e)
    {
        switch (Number)
        {
            case 0:
                DrawA(e);
                DrawB(e);
                DrawC(e);
                DrawD(e);
                DrawE(e);
                DrawF(e);
                break;
            case 1:
                DrawB(e);
                DrawC(e);
                break;
            case 2:
                DrawA(e);
                DrawB(e);
                DrawG(e);
                DrawE(e);
                DrawD(e);
                break;
            case 3:
                DrawA(e);
                DrawB(e);
                DrawG(e);
                DrawC(e);
                DrawD(e);
                break;
            case 4:
                DrawB(e);
                DrawF(e);
                DrawG(e);
                DrawC(e);
                break;
            case 5:
                DrawA(e);
                DrawF(e);
                DrawG(e);
                DrawC(e);
                DrawD(e);
                break;
            case 6:
                DrawA(e);
                DrawF(e);
                DrawC(e);
                DrawE(e);
                DrawC(e);
                DrawD(e);
                DrawG(e);
                break;
            case 7:
                DrawA(e);
                DrawB(e);
                DrawC(e);
                break;
            case 8:
                DrawA(e);
                DrawB(e);
                DrawC(e);
                DrawD(e);
                DrawE(e);
                DrawF(e);
                DrawG(e);
                break;
            case 9:
                DrawA(e);
                DrawF(e);
                DrawB(e);
                DrawC(e);
                DrawG(e);
                DrawD(e);
                break;
            default:
                //ERROR
                DrawA(e);
                DrawF(e);
                DrawG(e);
                DrawE(e);
                DrawD(e);

                if (error != null)
                {
                    error(null, e);
                }

                break;
        }
    }

    void t_Tick(object sender, EventArgs e)
    {
        if (Incrementare == true)
        {
            Increment();
        }
        else
        {
            Decrement();
        }
    }

    public void Start()
    {
        t.Start();
    }

    public void Stop()
    {
        t.Stop();
    }

    public void DrawA(PaintEventArgs e)
    {
        e.Graphics.FillRectangle(new SolidBrush(ForeColor), Thickness, 0, Width - 2 * Thickness, Thickness);
    }

    public void DrawB(PaintEventArgs e)
    {
        e.Graphics.FillRectangle(new SolidBrush(ForeColor), Width - Thickness, Thickness, Thickness, (Height - 3 * Thickness) / 2);
    }

    public void DrawC(PaintEventArgs e)
    {
        e.Graphics.FillRectangle(new SolidBrush(ForeColor), Width - Thickness, (Height + Thickness) / 2, Thickness, (Height - 3 * Thickness) / 2);
    }

    public void DrawD(PaintEventArgs e)
    {
        e.Graphics.FillRectangle(new SolidBrush(ForeColor), Thickness, Height - Thickness, Width - 2 * Thickness, Thickness);
    }

    public void DrawE(PaintEventArgs e)
    {
        e.Graphics.FillRectangle(new SolidBrush(ForeColor), 0, (Height + Thickness) / 2, Thickness, (Height - 3 * Thickness) / 2);
    }

    public void DrawF(PaintEventArgs e)
    {
        e.Graphics.FillRectangle(new SolidBrush(ForeColor), 0, Thickness, Thickness, (Height - 3 * Thickness) / 2);
    }

    public void DrawG(PaintEventArgs e)
    {
        e.Graphics.FillRectangle(new SolidBrush(ForeColor), Thickness, (Height - Thickness) / 2, Width - 2 * Thickness, Thickness);
    }

    public void Increment()
    {
        if (Number >= 9)
        {
            end?.Invoke(null, null);

            Number = 0;
        }
        else
        {
            Number++;
        }
    }

    public void Decrement()
    {
        if (Number <= 0)
        {
            end?.Invoke(null, null);

            Number = 9;
        }
        else
        {
            Number--;
        }
    }

    private void InitializeComponent()
    {
            SuspendLayout();
            // 
            // Display
            // 
            Name = "Display";
            Load += new System.EventHandler(Display_Load);
            ResumeLayout(false);

    }

    private void Display_Load(object sender, EventArgs e)
    {

    }
}