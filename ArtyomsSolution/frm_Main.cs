using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ArtyomsSolution
{
    public partial class frm_Main : Form
    {
        List<Vertex> vs = new List<Vertex>();
        int vs_ind = -1;

        public frm_Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.Items.Clear();
            toolStripComboBox1.Items.Add("Circle");
            toolStripComboBox1.Items.Add("Square");
            toolStripComboBox1.Items.Add("Triangle");
            toolStripComboBox1.SelectedIndex = 0;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Vertex v in vs)
                v.DrawFigure(e.Graphics);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            vs_ind = -1;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                foreach (Vertex v in vs)
                    if (v.Check(e.X, e.Y))
                    {
                        vs.Remove(v);
                        break;
                    }
            }

            if (e.Button == MouseButtons.Left)
            {
                foreach (Vertex v in vs)
                    if (v.Check(e.X, e.Y))
                    {
                        vs_ind = vs.IndexOf(v);
                        break;
                    }
                if (vs_ind == -1)
                {
                    switch (toolStripComboBox1.SelectedIndex)
                    {
                        case 0:
                            vs.Add(new Circle(e.X, e.Y));
                            vs_ind = vs.Count - 1;
                            break;
                        case 1:
                            vs.Add(new Square(e.X, e.Y));
                            vs_ind = vs.Count - 1;
                            break;
                        case 2:
                            vs.Add(new Triangle(e.X, e.Y));
                            vs_ind = vs.Count - 1;
                            break;
                    }
                }
            }
            Invalidate();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (vs_ind > -1)
                {
                    vs[vs_ind].SETX = e.X;
                    vs[vs_ind].SETY = e.Y;
                    Invalidate();
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}