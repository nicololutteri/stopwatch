using System;
using System.Drawing;
using System.Windows.Forms;

public class TwoDots : UserControl
{
    readonly Timer t;

    public int Range
    {
        get { return t.Interval; }
        set 
        {
            t.Interval = value;
            Invalidate();
        }
    }

    bool show;
    public bool Show
    {
        get { return show; }
        set 
        {
            show = value;
            Invalidate();
        }
    }

    public TwoDots()
    {
        t = new Timer
        {
            Interval = 1000
        };
        t.Tick += t_Tick;

        Show = true;

        Paint += DuePunti_Paint;
    }

    void DuePunti_Paint(object sender, PaintEventArgs e)
    {
        Brush colore = new SolidBrush(ForeColor);
        
        if (!show)
        {
            colore = new SolidBrush(BackColor);   
        }

        e.Graphics.FillRectangle(colore, new Rectangle(0, Height / 5 * 1, Width, Height / 4));
        e.Graphics.FillRectangle(colore, new Rectangle(0, Height / 5 * 3, Width, Height / 4));
    }

    void t_Tick(object sender, EventArgs e)
    {
        Show = !Show;
    }

    public void Start()
    {
        t.Start();
    }

    public void Stop()
    {
        t.Stop();
    }

    private void InitializeComponent()
    {
            this.SuspendLayout();
            // 
            // TwoDots
            // 
            this.Name = "TwoDots";
            this.Load += new System.EventHandler(this.TwoDots_Load);
            this.ResumeLayout(false);

    }

    private void TwoDots_Load(object sender, EventArgs e)
    {

    }
}