using System;
using System.Drawing;
using System.Windows.Forms;

public class StopwatchFinal : UserControl
{
    readonly Timer updateTime;
    readonly Display micro1;
    readonly Display micro2;
    readonly Dot dot1;
    readonly Display seconds1;
    readonly Display seconds2;
    readonly TwoDots twodots1;
    readonly Display minute1;
    readonly Display minute2;
    readonly TwoDots twodots2;
    readonly Display hour1;
    readonly Display ore2;

    int distance;
    public int Distance
    {
        get { return distance; }
        set
        {
            distance = value;
            Invalidate();
        }
    }

    Point dot;
    private Point Dot
    {
        get { return dot; }
        set
        {
            dot = value;
            Invalidate();
        }
    }

    public override Color ForeColor
    {
        get
        {
            return base.ForeColor;
        }
        set
        {
            base.ForeColor = value;
            Invalidate();
        }
    }

    public StopwatchFinal()
    {
        DoubleBuffered = true;

        distance = 10;
        dot = new Point(0, 0);

        updateTime = new Timer
        {
            Interval = 1
        };
        updateTime.Tick += UpdateTimer_Tick;

        Resize += StopWatch_Resize;

        micro1 = new Display();
        micro2 = new Display();

        dot1 = new Dot();

        seconds1 = new Display();
        seconds2 = new Display();

        twodots1 = new TwoDots();

        minute1 = new Display();
        minute2 = new Display();

        twodots2 = new TwoDots();

        hour1 = new Display();
        ore2 = new Display();

        Adapt();
        Draw();

        micro1.Parent = this;
        micro2.Parent = this;
        seconds1.Parent = this;
        seconds2.Parent = this;
        minute1.Parent = this;
        minute2.Parent = this;
        hour1.Parent = this;
        ore2.Parent = this;

        twodots1.Parent = this;
        twodots2.Parent = this;

        dot1.Parent = this;
    }

    DateTime startDate;
    void UpdateTimer_Tick(object sender, EventArgs e)
    {
        TimeSpan tmp = DateTime.Now.Subtract(startDate);

        if (tmp.Milliseconds.ToString().Length >= 2)
        {
            micro1.Number = Convert.ToInt32(tmp.Milliseconds.ToString().Substring(1, 1));
            micro2.Number = Convert.ToInt32(tmp.Milliseconds.ToString().Substring(0, 1));
        }
        else
        {
            micro1.Number = Convert.ToInt32(tmp.Milliseconds.ToString());
            micro2.Number = 0;
        }

        if (tmp.Seconds.ToString().Length == 2)
        {
            seconds1.Number = Convert.ToInt32(tmp.Seconds.ToString().Remove(0, 1));
            seconds2.Number = Convert.ToInt32(tmp.Seconds.ToString().Substring(0, 1));
        }
        else
        {
            seconds1.Number = Convert.ToInt32(tmp.Seconds.ToString());
            seconds2.Number = 0;
        }

        if (tmp.Minutes.ToString().Length == 2)
        {
            minute1.Number = Convert.ToInt32(tmp.Minutes.ToString().Remove(0, 1));
            minute2.Number = Convert.ToInt32(tmp.Minutes.ToString().Substring(0, 1));
        }
        else
        {
            minute1.Number = Convert.ToInt32(tmp.Minutes.ToString());
            minute2.Number = 0;
        }

        if (tmp.Hours.ToString().Length == 2)
        {
            hour1.Number = Convert.ToInt32(tmp.Hours.ToString().Remove(0, 1));
            ore2.Number = Convert.ToInt32(tmp.Hours.ToString().Substring(0, 1));
        }
        else
        {
            hour1.Number = Convert.ToInt32(tmp.Hours.ToString());
            ore2.Number = 0;
        }
    }

    void StopWatch_Resize(object sender, EventArgs e)
    {
        Adapt();
        Draw();
    }

    public void Adapt()
    {
        int height = Height * 3 / 8;
        int lenght = Width / 13;
        int thickness = Width / 60;

        hour1.Height = height;
        ore2.Height = height;
        minute1.Height = height;
        minute2.Height = height;
        seconds1.Height = height;
        seconds2.Height = height;
        micro1.Height = height;
        micro2.Height = height;

        hour1.Width = lenght;
        ore2.Width = lenght;
        minute1.Width = lenght;
        minute2.Width = lenght;
        seconds1.Width = lenght;
        seconds2.Width = lenght;
        micro1.Width = lenght;
        micro2.Width = lenght;

        twodots1.Height = height;
        twodots2.Height = height;
        dot1.Height = height;
        twodots1.Width = lenght / 3;
        twodots2.Width = lenght / 3;
        dot1.Width = lenght / 3;

        hour1.Thickness = thickness;
        ore2.Thickness = thickness;
        minute1.Thickness = thickness;
        minute2.Thickness = thickness;
        seconds1.Thickness = thickness;
        seconds2.Thickness = thickness;
        micro1.Thickness = thickness;
        micro2.Thickness = thickness;
    }

    public void Draw()
    {
        Dot = new Point(Width / 2 - (hour1.Width
                                       + ore2.Width
                                       + twodots1.Width
                                       + twodots2.Width
                                       + dot1.Width
                                       + minute1.Width
                                       + minute2.Width
                                       + seconds1.Width
                                       + seconds2.Width
                                       + micro1.Width
                                       + micro2.Width
                                       + Distance
                                       * 11) / 2, Height / 2 - hour1.Height / 2);

        ore2.Left = Dot.X;
        ore2.Top = Dot.Y;

        hour1.Left = ore2.Location.X + ore2.Width + Distance;
        hour1.Top = Dot.Y;

        twodots2.Left = hour1.Location.X + hour1.Width + Distance;
        twodots2.Top = Dot.Y;
        
        minute2.Left = twodots2.Location.X + twodots2.Width + Distance;
        minute2.Top = Dot.Y;
        minute1.Left = minute2.Location.X + minute2.Width + Distance;
        minute1.Top = Dot.Y;

        twodots1.Left = minute1.Location.X + minute1.Width + Distance;
        twodots1.Top = Dot.Y;
        
        seconds2.Left = twodots1.Location.X + twodots1.Width + Distance;
        seconds2.Top = Dot.Y;
        seconds1.Left = seconds2.Location.X + seconds2.Width + Distance;
        seconds1.Top = Dot.Y;

        dot1.Left = seconds1.Location.X + seconds1.Width + Distance;
        dot1.Top = Dot.Y;

        micro2.Left = dot1.Location.X + dot1.Width + Distance;
        micro2.Top = Dot.Y;
        micro1.Left = micro2.Location.X + micro2.Width + Distance;
        micro1.Top = Dot.Y;
    }

    DateTime pause;
    public void Stop()
    {
        pause = DateTime.Now;
        updateTime.Stop();

        twodots1.Stop();
        twodots2.Stop();
    }

    public void Riprendi()
    {
        TimeSpan timePause = DateTime.Now.Subtract(pause);
        startDate = startDate.Add(timePause);
        updateTime.Start();

        twodots1.Start();
        twodots2.Start();
    }

    public void Start()
    {
        startDate = DateTime.Now;
        updateTime.Start();

        twodots1.Start();
        twodots2.Start();
    }

    public void Reset()
    {
        Settings(0, 0, 0, 0, 0, 0, 0, 0);
    }

    public void Settings(int micro1v, int micro2v, int seconds1v, int seconds2v, int minutes1v, int minutes2v, int hour1v, int hour2v)
    {
        micro1.Number = micro1v;
        micro2.Number = micro2v;

        seconds1.Number = seconds1v;
        seconds2.Number = seconds2v;

        minute1.Number = minutes1v;
        minute2.Number = minutes2v;

        hour1.Number = hour1v;
        ore2.Number = hour2v;
    }

    public string ValueNow()
    {
        return ore2.Number + hour1.Number + ":" + minute2.Number + minute1.Number + ":" + seconds2.Number + seconds1.Number + "." + micro2.Number + micro1.Number;
    }

    private void InitializeComponent()
    {
            this.SuspendLayout();
            // 
            // StopwatchFinal
            // 
            this.Name = "StopwatchFinal";
            this.Load += new System.EventHandler(this.StopwatchFinal_Load);
            this.ResumeLayout(false);

    }

    private void StopwatchFinal_Load(object sender, EventArgs e)
    {

    }
}