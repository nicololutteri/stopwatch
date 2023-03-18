using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Stopwatch
{
    public partial class MainForm : Form
    {
        public ListData events = new ListData();
        DateTime startlap;
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void Stopwatch1_finecorsa(object sender, EventArgs e)
        {
            stopwatchVisualizer.Reset();
        }

        private void StartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pauseToolStripMenuItem.Checked = false;

            stopwatchVisualizer.Reset();
            stopwatchVisualizer.Start();

            Start();
        }

        private void PauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pauseToolStripMenuItem.Checked = !pauseToolStripMenuItem.Checked;

            if (pauseToolStripMenuItem.Checked)
            {
                stopwatchVisualizer.Stop();
            }
            else
            {
                stopwatchVisualizer.Riprendi();
            }
        }

        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pauseToolStripMenuItem.Checked = false;

            stopwatchVisualizer.Stop();
            Stop();
            stopwatchVisualizer.Reset();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TakeLapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem lvitem;
            lvitem = listView1.Items.Add(TakeNumber().ToString());
            lvitem.SubItems.Add(stopwatchVisualizer.ValueNow());
            lvitem.ImageIndex = 0;
        }

        private void ResetGiriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string tmp = "";

                foreach (ListViewItem x in listView1.Items)
                {
                    tmp = ";" + x.SubItems[0].Text + "," + x.SubItems[1].Text;
                }

                if (tmp.Length != 0)
                {
                    tmp = tmp.Remove(0, 1);
                }

                try
                {
                    File.WriteAllText(saveFileDialog1.FileName, tmp);
                    MessageBox.Show("File saved correctly", "");
                }
                catch 
                {
                    MessageBox.Show("Error in writing the file", "");
                }
            }            
        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listView1.Items.Clear();

                string[] text = File.ReadAllLines(openFileDialog1.FileName);

                foreach (string x in text)
                {
                    string[] split = x.Split(',');

                    ListViewItem lvitem;
                    lvitem = listView1.Items.Add(split[0]);
                    lvitem.SubItems.Add(split[1]);
                    lvitem.ImageIndex = 0;
                }
            }
        }

        private void InformazioniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program was made by Nicolò Lutteri", "");
        }

        private int TakeNumber()
        {
            if (listView1.Items.Count != 0)
            {
                return listView1.Items.Count + 1;
            }
            else
            {
                return 1;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (File.Exists("data.xml"))
            {
                events.LoadXML("data.xml"); 
            }

            startToolStripMenuItem.Enabled = true;
            pauseToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = false;

            lapToolStripMenuItem.Enabled = false;
        }

        private void Stop()
        {
            startToolStripMenuItem.Enabled = true;
            pauseToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = false;

            lapToolStripMenuItem.Enabled = false;

            Insert f = new Insert();
            DialogResult r;

            r = f.ShowDialog();

            if (r == DialogResult.OK)
            {
                events.Add(new Lap(startlap, DateTime.Now, stopwatchVisualizer.ValueNow(), f.textBox1.Text));
            }
        }

        private void Start()
        {
            startToolStripMenuItem.Enabled = false;
            pauseToolStripMenuItem.Enabled = true;
            stopToolStripMenuItem.Enabled = true;

            lapToolStripMenuItem.Enabled = true;

            startlap = DateTime.Now;
        }

        private void ShowListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List f = new List();
            DialogResult r;

            foreach (Lap x in events.list)
            {
                ListViewItem lvitem;
                lvitem = f.listView1.Items.Add(x.StartDate.ToString());
                lvitem.SubItems.Add(x.EndDate.ToString());
                lvitem.SubItems.Add(x.Time);
                lvitem.SubItems.Add(x.Description);
            }

            f.listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            r = f.ShowDialog();
            if (r == DialogResult.Abort)
            {
                if (f.listView1.SelectedItems.Count >= 1)
                {
                    int tmp = 0;
                    while (f.listView1.SelectedItems.Count > tmp)
                    {
                        events.Delete(f.listView1.SelectedItems[tmp].Index - tmp);
                        tmp++;
                    }
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            events.SaveXml("data.xml");
        }
    }
}
