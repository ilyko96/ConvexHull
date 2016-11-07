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
        List<Line> ch = new List<Line>();
        int net_step = 1;

        public frm_Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripComboBox1.Items.Clear();
            toolStripComboBox1.Items.AddRange(new string[] { "Circle", "Square", "Triangle" });
            toolStripComboBox1.SelectedIndex = 0;
            toolStripComboBox2.SelectedIndex = 0;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (vs.Count > 2)
                foreach (Line l in ch)
                    l.Draw(e.Graphics);
            foreach (Vertex v in vs)
                v.DrawFigure(e.Graphics);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            foreach (Vertex v in vs)
                v.IsMoving = false;

            calculate_ch();
            delete_inside();

            Invalidate();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < vs.Count; i++)
                    if (vs[i].Check(e.X, e.Y))
                        vs.RemoveAt(i--);
            }

            if (e.Button == MouseButtons.Left)
            {
                bool flag = false;
                foreach (Vertex v in vs)
                {
                    if (v.Check(e.X, e.Y))
                    {
                        v.IsMoving = true;
                        v.DeltaMouse = new Point(e.X - v.X, e.Y - v.Y);
                        flag = true;
                    }
                }
                if (!flag)
                {
                    switch (toolStripComboBox1.SelectedIndex)
                    {
                        case 0:
                            vs.Add(new Circle(e.X, e.Y));
                            vs[vs.Count - 1].IsMoving = true;
                            break;
                        case 1:
                            vs.Add(new Square(e.X, e.Y));
                            vs[vs.Count - 1].IsMoving = true;
                            break;
                        case 2:
                            vs.Add(new Triangle(e.X, e.Y));
                            vs[vs.Count - 1].IsMoving = true;
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
                foreach (Vertex v in vs)
                    if (v.IsMoving)
                    {
                        v.X = e.X - v.DeltaMouse.X;
                        v.Y = e.Y - v.DeltaMouse.Y;
                        v.X = (v.X / net_step) * net_step;
                        v.Y = (v.Y / net_step) * net_step;
                    }

                calculate_ch();
                Invalidate();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void calculate_ch()
        {
            ch = new List<Line>();
            if (vs.Count < 3)
                return;
            for (int i = 0; i < vs.Count - 1; i++)
            {
                for (int j = i + 1; j < vs.Count; j++)
                {
                    bool first = true;
                    int val = 0;
                    bool flag = true;
                    for (int k = 0; k < vs.Count; k++)
                    {
                        if (k == i || k == j)
                            continue;
                        int v = Service.point_on_vector(vs[i].Location, vs[j].Location, vs[k].Location);
                        if (v == 0) continue;
                        if (first)
                        {
                            val = v;
                            first = false;
                            continue;
                        }
                        if (v != val)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                        ch.Add(new Line(vs[i], vs[j]));
                }
            }
        }
        private void delete_inside()
        {
            if (vs.Count < 3)
                return;

            List<Vertex> delList = new List<Vertex>(vs);
            foreach (Line l in ch)
            {
                if (vs.Contains(l.V1))
                    delList.Remove(l.V1);
                if (vs.Contains(l.V2))
                    delList.Remove(l.V2);
            }
            for (; delList.Count > 0;)
            {
                vs.Remove(delList[0]);
                delList.RemoveAt(0);
            }
        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            net_step = int.Parse(toolStripComboBox2.Text);
            toolStripComboBox2.Text = net_step.ToString();
        }
    }
}